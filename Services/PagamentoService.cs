using ContrRendaFixa.Models;
using ContrRendaFixa.Repository.Interfaces;
using ContrRendaFixa.Services.Interfaces;
using ContrRendaFixa.ViewModel;

namespace ContrRendaFixa.Services
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IContratacaoRepository _contratacaoRepository;

        public PagamentoService(IPagamentoRepository pagamentoRepository, IContratacaoRepository contratacaoRepository)
        {
            _pagamentoRepository = pagamentoRepository;
            _contratacaoRepository = contratacaoRepository;
        }

        public async Task<PagamentoModel> GetPagamentoByIdAsync(int id)
        {
            return await _pagamentoRepository.GetPagamentoByIdAsync(id);
        }

        public async Task<PagamentoModel> CreatePagamentoAsync(PagamentoPostViewModel pagamentoViewModel)
        {
            var contratacao = await _contratacaoRepository.GetContratacaoByIdAsync(pagamentoViewModel.ContratacaoId);

            if (contratacao == null)
            {
                throw new ArgumentException(MensagensErrosContratacao.ContratacaoNotFound);
            }

            if (pagamentoViewModel.Valor <= 0)
            {
                throw new ArgumentException(MensagensErrosPagamento.ValorPositivo);
            }

            if (contratacao.Pago)
            {
                throw new ArgumentException(MensagensErrosContratacao.ContratacaoPaga);
            }

            var totalPago = await _pagamentoRepository.GetTotalPagoByContratacaoIdAsync(pagamentoViewModel.ContratacaoId) + pagamentoViewModel.Valor;
            var valorTotal = contratacao.Quantidade * contratacao.PrecoUnitario - contratacao.Desconto;

            if (totalPago >= valorTotal)
            {
                contratacao.Pago = true;
                await _contratacaoRepository.UpdateContratacaoAsync(contratacao);
            }

            var pagamento = new PagamentoModel
            {
                ContratacaoId = pagamentoViewModel.ContratacaoId,
                Valor = pagamentoViewModel.Valor,
                DataPagamento = DateTime.UtcNow
            };

            return await _pagamentoRepository.CreatePagamentoAsync(pagamento);
        }
    }
}
