using ContrRendaFixa.ViewModel;

namespace ContrRendaFixa.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoGetViewModel>> GetProdutosAsync();
        Task<ProdutoGetViewModel> GetProdutoByIdAsync(int id);
        Task<ProdutoGetViewModel> CreateProdutoAsync(ProdutoPostViewModel produto);
        Task<bool> UpdateProdutoAsync(int id, ProdutoPostViewModel produto);
        Task<bool> UpdateProdutoPatchAsync(int id, ProdutoPatchViewModel produto);
    }
}
