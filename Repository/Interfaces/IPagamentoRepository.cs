using ContrRendaFixa.Models;

namespace ContrRendaFixa.Repository.Interfaces
{
    public interface IPagamentoRepository
    {
        Task<IEnumerable<PagamentoModel>> GetPagamentosAsync();
        Task<PagamentoModel> GetPagamentoByIdAsync(int id);
        Task<PagamentoModel> CreatePagamentoAsync(PagamentoModel pagamento);
        Task<bool> PagamentoExistsAsync(int id);
        Task<decimal> GetTotalPagoByContratacaoIdAsync(int contratacaoId);
    }
}
