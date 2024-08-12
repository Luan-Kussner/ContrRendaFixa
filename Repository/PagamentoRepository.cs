using ContrRendaFixa.Data;
using ContrRendaFixa.Models;
using ContrRendaFixa.Repository.Interfaces;
using ContrRendaFixa.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ContrRendaFixa.Repository
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly ApplicationDbContext _context;

        public PagamentoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PagamentoModel>> GetPagamentosAsync()
        {
            return await _context.Pagamentos
                .Include(p => p.Contratacao)
                .ToListAsync();
        }

        public async Task<PagamentoModel> GetPagamentoByIdAsync(int id)
        {
            return await _context.Pagamentos
                .Include(p => p.Contratacao)  
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PagamentoModel> CreatePagamentoAsync(PagamentoModel pagamento)
        {
            if (pagamento.Valor < 0)
            {
                throw new ArgumentException(MensagensErrosPagamento.ValorPositivo);
            }

            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();
            return pagamento;
        }

        public async Task<bool> PagamentoExistsAsync(int id)
        {
            return await _context.Pagamentos.AnyAsync(p => p.Id == id);
        }

        public async Task<decimal> GetTotalPagoByContratacaoIdAsync(int contratacaoId)
        {
            return await _context.Pagamentos
                .Where(p => p.ContratacaoId == contratacaoId)
                .SumAsync(p => p.Valor);
        }
    }
}
