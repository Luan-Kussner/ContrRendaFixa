using ContrRendaFixa.Models;

namespace ContrRendaFixa.Repository.Interfaces
{
    public interface ITipoProdutoRepository
    {
        Task<IEnumerable<TipoProdutoModel>> GetTiposProdutosAsync();
        Task<TipoProdutoModel> GetTipoProdutoByIdAsync(int id);
        Task<TipoProdutoModel> CreateTipoProdutoAsync(TipoProdutoModel tipoProduto);
        Task<bool> UpdateTipoProdutoAsync(int id, TipoProdutoModel tipoProduto);
        Task<bool> TipoProdutoExistsAsync(int id);
        Task<TipoProdutoModel> GetTipoProdutoByDescricaoAsync(string descricao);
    }
}
