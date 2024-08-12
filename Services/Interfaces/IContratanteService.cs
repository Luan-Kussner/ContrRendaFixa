using ContrRendaFixa.Models;
using ContrRendaFixa.ViewModel;

namespace ContrRendaFixa.Services.Interfaces
{
    public interface IContratanteService
    {
        Task<IEnumerable<ContratanteGetViewModel>> GetContratantesAsync();
        Task<ContratanteGetViewModel> GetContratanteByIdAsync(int id);
        Task<ContratanteModel> CreateContratanteAsync(ContratantePostViewModel contratanteViewModel);
        Task<bool> UpdateContratanteAsync(int id, ContratantePatchViewModel contratanteViewModel);
    }
}
