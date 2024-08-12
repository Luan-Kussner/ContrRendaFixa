using ContrRendaFixa.Models;
using ContrRendaFixa.ViewModel;

namespace ContrRendaFixa.Services.Interfaces
{
    public interface IPagamentoService
    {
        Task<PagamentoModel> CreatePagamentoAsync(PagamentoPostViewModel pagamentoViewModel);
        Task<PagamentoModel> GetPagamentoByIdAsync(int id);
    }
}
