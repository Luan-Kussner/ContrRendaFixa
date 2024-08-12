using ContrRendaFixa.Data;
using ContrRendaFixa.Models;
using ContrRendaFixa.Repository.Interfaces;
using ContrRendaFixa.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ContrRendaFixa.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProdutoGetViewModel>> GetProdutosAsync()
        {
            return await _context.Produtos
                .Include(p => p.Tipo)
                .Select(p => new ProdutoGetViewModel
                {
                    Id = p.Id,
                    Descricao = p.Descricao,
                    Tipo = p.Tipo,
                    Preco = p.Preco,
                    Quantidade = p.Quantidade,
                    Bloqueado = p.Bloqueado
                })
                .ToListAsync();
        }

        public async Task<ProdutoGetViewModel> GetProdutoByIdAsync(int id)
        {
            return await _context.Produtos
                .Include(p => p.Tipo)
                .Where(p => p.Id == id)
                .Select(p => new ProdutoGetViewModel
                {
                    Id = p.Id,
                    Descricao = p.Descricao,
                    Tipo = p.Tipo,
                    Preco = p.Preco,
                    Quantidade = p.Quantidade,
                    Bloqueado = p.Bloqueado
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ProdutoModel> CreateProdutoAsync(ProdutoPostViewModel produto)
        {
            var newProduto = new ProdutoModel
            {
                Descricao = produto.Descricao,
                TipoId = produto.TipoId,
                Preco = produto.Preco,
                Quantidade = produto.Quantidade,
                Bloqueado = produto.Bloqueado
            };

            _context.Produtos.Add(newProduto);
            await _context.SaveChangesAsync();

            return newProduto;
        }

        public async Task<bool> UpdateProdutoAsync(int id, ProdutoPostViewModel produto)
        {
            var existingProduto = await _context.Produtos.FindAsync(id);
            if (existingProduto == null)
            {
                return false;
            }

            existingProduto.Descricao = produto.Descricao;
            existingProduto.TipoId = produto.TipoId;
            existingProduto.Preco = produto.Preco;
            existingProduto.Quantidade = produto.Quantidade;
            existingProduto.Bloqueado = produto.Bloqueado;

            _context.Entry(existingProduto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProdutoExistsAsync(id))
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

        public async Task<bool> UpdateProdutoPatchAsync(int id, ProdutoPatchViewModel produto)
        {
            var existingProduto = await _context.Produtos.FindAsync(id);
            if (existingProduto == null)
            {
                return false;
            }

            if (produto.Descricao != null && produto.Descricao != "string")
            {
                existingProduto.Descricao = produto.Descricao;
            }

            if (produto.TipoId != 0)
            {
                existingProduto.TipoId = produto.TipoId;
            }

            if (produto.Preco != 0)
            {
                existingProduto.Preco = produto.Preco;
            }

            if (produto.Quantidade != 0)
            {
                existingProduto.Quantidade = produto.Quantidade;
            }

            existingProduto.Bloqueado = produto.Bloqueado;

            _context.Entry(existingProduto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProdutoExistsAsync(id))
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

        public async Task<bool> ProdutoExistsAsync(int id)
        {
            return await _context.Produtos.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> BloqueadoAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            return produto?.Bloqueado ?? false;
        }
    }
}
