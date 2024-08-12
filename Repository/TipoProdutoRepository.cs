using ContrRendaFixa.Data;
using ContrRendaFixa.Models;
using ContrRendaFixa.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContrRendaFixa.Repository
{
    public class TipoProdutoRepository : ITipoProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public TipoProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoProdutoModel>> GetTiposProdutosAsync()
        {
            return await _context.TiposProdutos.ToListAsync();
        }

        public async Task<TipoProdutoModel> GetTipoProdutoByIdAsync(int id)
        {
            return await _context.TiposProdutos.FindAsync(id);
        }

        public async Task<TipoProdutoModel> CreateTipoProdutoAsync(TipoProdutoModel tipoProduto)
        {
            if (await _context.TiposProdutos.AnyAsync(tp => tp.Descricao == tipoProduto.Descricao))
            {
                return null;
            }

            _context.TiposProdutos.Add(tipoProduto);
            await _context.SaveChangesAsync();

            return tipoProduto;
        }

        public async Task<bool> UpdateTipoProdutoAsync(int id, TipoProdutoModel tipoProduto)
        {
            if (id != tipoProduto.Id)
            {
                return false;
            }

            _context.Entry(tipoProduto).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TipoProdutoExistsAsync(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> TipoProdutoExistsAsync(int id)
        {
            return await _context.TiposProdutos.AnyAsync(e => e.Id == id);
        }

        public async Task<TipoProdutoModel> GetTipoProdutoByDescricaoAsync(string descricao)
        {
            return await _context.TiposProdutos.FirstOrDefaultAsync(tp => tp.Descricao == descricao);
        }
    }
}
