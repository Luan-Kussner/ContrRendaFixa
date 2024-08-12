using ContrRendaFixa.ViewModel;

namespace ContrRendaFixa.Services.Interfaces
{
    public interface ITipoProdutoService
    {
        Task<IEnumerable<TipoProdutoViewModel>> GetTiposProdutosAsync();
        Task<TipoProdutoViewModel> GetTipoProdutoByIdAsync(int id);
        Task<TipoProdutoViewModel> CreateTipoProdutoAsync(TipoProdutoViewModel tipoProdutoViewModel);
        Task<bool> UpdateTipoProdutoAsync(int id, TipoProdutoViewModel tipoProdutoViewModel);
    }
}
