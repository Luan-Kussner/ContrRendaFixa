using ContrRendaFixa.Models;
using ContrRendaFixa.ViewModel;

namespace ContrRendaFixa.Repository.Interfaces
{
    public interface IContratanteRepository
    {
        Task<IEnumerable<ContratanteModel>> GetContratantesAsync();
        Task<ContratanteModel> GetContratanteByIdAsync(int id);
        Task<ContratanteModel> CreateContratanteAsync(ContratanteModel contratante);
        Task<bool> UpdateContratanteAsync(int id, ContratantePatchViewModel contratante);
        Task<bool> ContratanteExistsAsync(int id);
        Task<bool> ContratanteExistsByNameAsync(string nome);
        Task<ContratanteModel> GetContratanteByNameAsync(string nome);
        Task<bool> BloqueadoAsync(int id);
    }
}
