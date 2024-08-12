using ContrRendaFixa.Models;
using ContrRendaFixa.Repository.Interfaces;
using ContrRendaFixa.Services.Interfaces;
using ContrRendaFixa.ViewModel;

namespace ContrRendaFixa.Services
{
    public class ContratacaoService : IContratacaoService
    {
        private readonly IContratacaoRepository _contratacaoRepository;
        private readonly IContratanteRepository _contratanteRepository;
        private readonly IProdutoRepository _produtoRepository;

        public ContratacaoService(IContratacaoRepository contratacaoRepository, IContratanteRepository contratanteRepository, IProdutoRepository produtoRepository)
        {
            _contratacaoRepository = contratacaoRepository;
            _contratanteRepository = contratanteRepository;
            _produtoRepository = produtoRepository;
        }

        private bool HorarioComercial(DateTime dateTime)
        {
            var startHour = new TimeSpan(10, 30, 0); // 10:30 AM
            var endHour = new TimeSpan(16, 0, 0); // 4:00 PM
            var currentTime = dateTime.TimeOfDay;

            return currentTime >= startHour && currentTime <= endHour;
        }

        private bool ProdutoValidoPorSegmento(string segmento, string tipoProduto, decimal valorTotal)
        {
            var produtoPermitidoPorVarejo = new HashSet<string> { "**DEB**", "**CRI**", "**CRA**" };
            var produtoPermitidoPorAtacado = new HashSet<string> { "**LCI**", "**LCA**" };

            if (segmento == "Varejo")
            {
                return produtoPermitidoPorVarejo.Contains(tipoProduto);
            }
            else if (segmento == "Atacado")
            {
                return produtoPermitidoPorAtacado.Contains(tipoProduto);
            }
            else if (segmento == "Especial")
            {
                return valorTotal > 20000.00m; // R$ 20.000,00
            }

            return false;
        }

        private async Task<bool> ContratanteBloqueado(int contratanteId)
        {
            var contratante = await _contratanteRepository.GetContratanteByIdAsync(contratanteId);
            return contratante?.Bloqueado ?? false;
        }

        private async Task<bool> ProdutoBloqueado(int produtoId)
        {
            var produto = await _produtoRepository.GetProdutoByIdAsync(produtoId);
            return produto?.Bloqueado ?? false;
        }

        public async Task<IEnumerable<ContratacaoModel>> GetContratacoesAsync()
        {
            return await _contratacaoRepository.GetContratacoesAsync();
        }

        public async Task<ContratacaoModel> GetContratacaoByIdAsync(int id)
        {
            return await _contratacaoRepository.GetContratacaoByIdAsync(id);
        }

        public async Task<ContratacaoModel> CreateContratacaoAsync(ContratacaoPostViewModel contratacaoPostViewModel)
        {
            if (contratacaoPostViewModel.Quantidade < 0)
            {
                throw new ArgumentException(MensagensErrosContratacao.QuantidadeNegativa);
            }

            if (contratacaoPostViewModel.PrecoUnitario < 0)
            {
                throw new ArgumentException(MensagensErrosContratacao.ValorUnitario);
            }

            decimal valorTotal = contratacaoPostViewModel.Quantidade * contratacaoPostViewModel.PrecoUnitario;

            if (contratacaoPostViewModel.Desconto > valorTotal)
            {
                throw new ArgumentException(MensagensErrosContratacao.DescontoMaior);
            }

            //if (!HorarioComercial(DateTime.UtcNow))
            //{
            //    throw new ArgumentException(MensagensErrosContratacao.ForaDeHorario);
            //}

            var contratante = await _contratanteRepository.GetContratanteByIdAsync(contratacaoPostViewModel.ContratanteId);
            var produto = await _produtoRepository.GetProdutoByIdAsync(contratacaoPostViewModel.ProdutoId);

            if (await ContratanteBloqueado(contratacaoPostViewModel.ContratanteId))
            {
                throw new ArgumentException(MensagensErrosContratacao.ContratanteBloqueado);
            }

            if (await ProdutoBloqueado(contratacaoPostViewModel.ProdutoId))
            {
                throw new ArgumentException(MensagensErrosContratacao.ProdutoBloqueado);
            }

            if (!ProdutoValidoPorSegmento(contratante.Segmento.ToString(), produto.Tipo.Descricao, valorTotal))
            {
                throw new ArgumentException(MensagensErrosContratacao.SegmentoInvalido);
            }

            bool existeContratacao = await _contratacaoRepository.ExistsAsync(c =>
                c.ContratanteId == contratacaoPostViewModel.ContratanteId &&
                c.ProdutoId == contratacaoPostViewModel.ProdutoId &&
                c.DataContratacao.Date == DateTime.UtcNow.Date);

            if (existeContratacao)
            {
                var contratacaoExistente = await _contratacaoRepository.GetExistingContratacaoAsync(
                    contratacaoPostViewModel.ContratanteId, contratacaoPostViewModel.ProdutoId, DateTime.UtcNow);

                contratacaoExistente.Quantidade += contratacaoPostViewModel.Quantidade;
                contratacaoExistente.PrecoUnitario = contratacaoPostViewModel.PrecoUnitario;

                await _contratacaoRepository.UpdateContratacaoAsync(contratacaoExistente);
                return contratacaoExistente;
            }
            else
            {
                var novaContratacao = new ContratacaoModel
                {
                    ContratanteId = contratacaoPostViewModel.ContratanteId,
                    ProdutoId = contratacaoPostViewModel.ProdutoId,
                    Quantidade = contratacaoPostViewModel.Quantidade,
                    PrecoUnitario = contratacaoPostViewModel.PrecoUnitario,
                    Desconto = contratacaoPostViewModel.Desconto,
                    DataContratacao = DateTime.UtcNow,
                    Pago = false
                };

                return await _contratacaoRepository.CreateContratacaoAsync(novaContratacao);
            }
        }

        public async Task<bool> CancelarContratacaoAsync(int contratacaoId)
        {
            var contratacao = await _contratacaoRepository.GetContratacaoByIdAsync(contratacaoId);

            if (contratacao == null)
            {
                throw new ArgumentException(MensagensErrosContratacao.ContratacaoNotFound);
            }

            if (contratacao.Pago)
            {
                throw new ArgumentException(MensagensErrosContratacao.CancelarContratacaoPaga);
            }

            await _contratacaoRepository.DeleteContratacaoAsync(contratacaoId);
            return true;
        }

        public async Task LimpezaContratacaoNaoPaga()
        {
            var naoPagas = await _contratacaoRepository.GetContratacoesNaoPagasAsync(DateTime.UtcNow);

            foreach (var contratacao in naoPagas)
            {
                await _contratacaoRepository.DeleteContratacaoAsync(contratacao.Id);
            }
        }
        public async Task<ContratacaoModel> DesistirContratacaoAsync(int id)
        {
            var contratacao = await _contratacaoRepository.GetContratacaoByIdAsync(id);

            if (contratacao == null)
            {
                throw new ArgumentException(MensagensErrosContratacao.ContratacaoNotFound);
            }

            if (contratacao.Pago)
            {
                throw new InvalidOperationException(MensagensErrosContratacao.ContratacaoPaga);
            }

            await _contratacaoRepository.DeleteContratacaoAsync(id);
            return contratacao;
        }
    }
}
