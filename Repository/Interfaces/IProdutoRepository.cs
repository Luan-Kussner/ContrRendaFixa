using ContrRendaFixa.Models;
using ContrRendaFixa.ViewModel;

namespace ContrRendaFixa.Repository.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<ProdutoGetViewModel>> GetProdutosAsync();
        Task<ProdutoGetViewModel> GetProdutoByIdAsync(int id);
        Task<ProdutoModel> CreateProdutoAsync(ProdutoPostViewModel produto);
        Task<bool> UpdateProdutoAsync(int id, ProdutoPostViewModel produto);
        Task<bool> UpdateProdutoPatchAsync(int id, ProdutoPatchViewModel produto);
        Task<bool> ProdutoExistsAsync(int id);
        Task<bool> BloqueadoAsync(int id);
    }
}
