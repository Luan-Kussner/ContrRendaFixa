using ContrRendaFixa.Repository.Interfaces;
using ContrRendaFixa.Services.Interfaces;
using ContrRendaFixa.ViewModel;

namespace ContrRendaFixa.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<ProdutoGetViewModel>> GetProdutosAsync()
        {
            return await _produtoRepository.GetProdutosAsync();
        }

        public async Task<ProdutoGetViewModel> GetProdutoByIdAsync(int id)
        {
            var produto = await _produtoRepository.GetProdutoByIdAsync(id);
            if (produto == null)
            {
                throw new KeyNotFoundException(MensagensErros.ProdutoNotFound);
            }

            return produto;
        }

        public async Task<ProdutoGetViewModel> CreateProdutoAsync(ProdutoPostViewModel produtoViewModel)
        {
            // Validações
            if (string.IsNullOrEmpty(produtoViewModel.Descricao) || produtoViewModel.Descricao.Length < 20 || produtoViewModel.Descricao.Length > 50)
            {
                throw new ArgumentException(MensagensErros.DescricaoInvalida);
            }

            var produtos = await _produtoRepository.GetProdutosAsync();
            if (produtos.Any(p => p.Descricao == produtoViewModel.Descricao))
            {
                throw new InvalidOperationException(MensagensErros.ProdutoExistente);
            }

            var produto = await _produtoRepository.CreateProdutoAsync(produtoViewModel);
            return new ProdutoGetViewModel
            {
                Id = produto.Id,
                Descricao = produto.Descricao,
                Tipo = produto.Tipo,
                Preco = produto.Preco,
                Quantidade = produto.Quantidade,
                Bloqueado = produto.Bloqueado
            };
        }

        public async Task<bool> UpdateProdutoAsync(int id, ProdutoPostViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id)
            {
                throw new ArgumentException(MensagensErros.ProdutoNotFound);
            }

            // Validações
            if (string.IsNullOrEmpty(produtoViewModel.Descricao) || produtoViewModel.Descricao.Length < 20 || produtoViewModel.Descricao.Length > 50)
            {
                throw new ArgumentException(MensagensErros.DescricaoInvalida);
            }

            var result = await _produtoRepository.UpdateProdutoAsync(id, produtoViewModel);
            if (!result)
            {
                throw new KeyNotFoundException(MensagensErros.ProdutoNotFound);
            }

            return result;
        }

        public async Task<bool> UpdateProdutoPatchAsync(int id, ProdutoPatchViewModel produtoViewModel)
        {
            if (produtoViewModel == null)
            {
                throw new ArgumentException(MensagensErros.PatchInvalido);
            }

            var produtoExistente = await _produtoRepository.GetProdutoByIdAsync(id);
            if (produtoExistente == null)
            {
                throw new KeyNotFoundException(MensagensErros.ProdutoNotFound);
            }

            if (!string.IsNullOrEmpty(produtoViewModel.Descricao) && (produtoViewModel.Descricao.Length < 20 || produtoViewModel.Descricao.Length > 50) && produtoViewModel.Descricao != "string")
            {
                throw new ArgumentException(MensagensErros.DescricaoInvalida);
            }

            return await _produtoRepository.UpdateProdutoPatchAsync(id, produtoViewModel);
        }

    }
}
