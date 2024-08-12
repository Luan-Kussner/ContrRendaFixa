using ContrRendaFixa.Models;
using ContrRendaFixa.Repository.Interfaces;
using ContrRendaFixa.Services.Interfaces;
using ContrRendaFixa.ViewModel;

namespace ContrRendaFixa.Services
{
    public class TipoProdutoService : ITipoProdutoService
    {
        private readonly ITipoProdutoRepository _tipoProdutoRepository;

        public TipoProdutoService(ITipoProdutoRepository tipoProdutoRepository)
        {
            _tipoProdutoRepository = tipoProdutoRepository;
        }

        public async Task<IEnumerable<TipoProdutoViewModel>> GetTiposProdutosAsync()
        {
            var tiposProdutos = await _tipoProdutoRepository.GetTiposProdutosAsync();
            return tiposProdutos.Select(tp => new TipoProdutoViewModel
            {
                Id = tp.Id,
                Descricao = tp.Descricao
            });
        }

        public async Task<TipoProdutoViewModel> GetTipoProdutoByIdAsync(int id)
        {
            var tipoProduto = await _tipoProdutoRepository.GetTipoProdutoByIdAsync(id);
            if (tipoProduto == null)
            {
                return null;
            }

            return new TipoProdutoViewModel
            {
                Id = tipoProduto.Id,
                Descricao = tipoProduto.Descricao
            };
        }

        public async Task<TipoProdutoViewModel> CreateTipoProdutoAsync(TipoProdutoViewModel tipoProdutoViewModel)
        {
            // Validações
            if (string.IsNullOrEmpty(tipoProdutoViewModel.Descricao) || tipoProdutoViewModel.Descricao.Length < 5 || tipoProdutoViewModel.Descricao.Length > 50)
            {
                throw new ArgumentException(TipoProdutoViewModel.MensagensErros.DescricaoInvalida);
            }

            var existingTipoProduto = await _tipoProdutoRepository.GetTipoProdutoByDescricaoAsync(tipoProdutoViewModel.Descricao);
            if (existingTipoProduto != null)
            {
                throw new InvalidOperationException(TipoProdutoViewModel.MensagensErros.TipoProdutoExistente);
            }

            var tipoProdutoModel = new TipoProdutoModel
            {
                Descricao = tipoProdutoViewModel.Descricao
            };

            var createdTipoProduto = await _tipoProdutoRepository.CreateTipoProdutoAsync(tipoProdutoModel);

            return new TipoProdutoViewModel
            {
                Id = createdTipoProduto.Id,
                Descricao = createdTipoProduto.Descricao
            };
        }

        public async Task<bool> UpdateTipoProdutoAsync(int id, TipoProdutoViewModel tipoProdutoViewModel)
        {
            // Validações
            if (id != tipoProdutoViewModel.Id)
            {
                throw new ArgumentException(TipoProdutoViewModel.MensagensErros.IdIncompativel);
            }

            if (string.IsNullOrEmpty(tipoProdutoViewModel.Descricao) || tipoProdutoViewModel.Descricao.Length < 5 || tipoProdutoViewModel.Descricao.Length > 50)
            {
                throw new ArgumentException(TipoProdutoViewModel.MensagensErros.DescricaoInvalida);
            }

            var tipoProdutoModel = new TipoProdutoModel
            {
                Id = tipoProdutoViewModel.Id,
                Descricao = tipoProdutoViewModel.Descricao
            };

            return await _tipoProdutoRepository.UpdateTipoProdutoAsync(id, tipoProdutoModel);
        }
    }
}
