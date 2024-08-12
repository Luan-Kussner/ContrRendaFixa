using ContrRendaFixa.Data;
using ContrRendaFixa.Models;
using ContrRendaFixa.Repository.Interfaces;
using ContrRendaFixa.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContrRendaFixa.Repository
{
    public class ContratacaoRepository : IContratacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public ContratacaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContratacaoModel>> GetContratacoesAsync()
        {
            return await _context.Contratacoes
                .Include(c => c.Contratante)
                .Include(c => c.Produto)
                .ToListAsync();
        }

        public async Task<ContratacaoModel> GetContratacaoByIdAsync(int id)
        {
            return await _context.Contratacoes
                .Include(c => c.Contratante)
                .Include(c => c.Produto)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ContratacaoModel> CreateContratacaoAsync(ContratacaoModel contratacao)
        {
            _context.Contratacoes.Add(contratacao);
            await _context.SaveChangesAsync();
            return contratacao;
        }

        public async Task UpdateContratacaoAsync(ContratacaoModel contratacao)
        {
            _context.Entry(contratacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContratacaoAsync(int id)
        {
            var contratacao = await _context.Contratacoes.FindAsync(id);
            if (contratacao == null)
            {
                throw new ArgumentException(MensagensErrosContratacao.ContratacaoNotFound);
            }

            _context.Contratacoes.Remove(contratacao);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<ContratacaoModel, bool>> predicate)
        {
            return await _context.Contratacoes.AnyAsync(predicate);
        }

        public async Task<ContratacaoModel> GetExistingContratacaoAsync(int contratanteId, int produtoId, DateTime date)
        {
            return await _context.Contratacoes
                .FirstOrDefaultAsync(c => c.ContratanteId == contratanteId
                    && c.ProdutoId == produtoId
                    && c.DataContratacao.Date == date.Date);
        }

        public async Task<IEnumerable<ContratacaoModel>> GetContratacoesNaoPagasAsync(DateTime date)
        {
            return await _context.Contratacoes
                .Where(c => !c.Pago && c.DataContratacao.Date == date.Date)
                .ToListAsync();
        }
    }
}
