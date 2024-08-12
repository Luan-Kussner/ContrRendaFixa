using ContrRendaFixa.Models;
using ContrRendaFixa.ViewModel;

namespace ContrRendaFixa.Services.Interfaces
{
    public interface IContratacaoService
    {
        Task<IEnumerable<ContratacaoModel>> GetContratacoesAsync();
        Task<ContratacaoModel> GetContratacaoByIdAsync(int id);
        Task<ContratacaoModel> CreateContratacaoAsync(ContratacaoPostViewModel contratacaoViewModel);
        Task<bool> CancelarContratacaoAsync(int contratacaoId);
        Task LimpezaContratacaoNaoPaga();
        Task<ContratacaoModel> DesistirContratacaoAsync(int id);
    }
}
