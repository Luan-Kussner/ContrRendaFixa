using ContrRendaFixa.Models;
using System.Linq.Expressions;

namespace ContrRendaFixa.Repository.Interfaces
{
    public interface IContratacaoRepository
    {
        Task<IEnumerable<ContratacaoModel>> GetContratacoesAsync();
        Task<ContratacaoModel> GetContratacaoByIdAsync(int id);
        Task<ContratacaoModel> CreateContratacaoAsync(ContratacaoModel contratacaoModel);
        Task UpdateContratacaoAsync(ContratacaoModel contratacaoModel);
        Task DeleteContratacaoAsync(int id);
        Task<bool> ExistsAsync(Expression<Func<ContratacaoModel, bool>> predicate);
        Task<ContratacaoModel> GetExistingContratacaoAsync(int contratanteId, int produtoId, DateTime dataContratacao);
        Task<IEnumerable<ContratacaoModel>> GetContratacoesNaoPagasAsync(DateTime data);
    }
}
