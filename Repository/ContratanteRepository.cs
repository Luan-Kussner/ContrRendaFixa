using ContrRendaFixa.Data;
using ContrRendaFixa.Models;
using ContrRendaFixa.Repository.Interfaces;
using ContrRendaFixa.ViewModel;
using Microsoft.EntityFrameworkCore;

public class ContratanteRepository : IContratanteRepository
{
    private readonly ApplicationDbContext _context;

    public ContratanteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ContratanteModel>> GetContratantesAsync()
    {
        return await _context.Contratantes.ToListAsync();
    }

    public async Task<ContratanteModel> GetContratanteByIdAsync(int id)
    {
        return await _context.Contratantes.FindAsync(id);
    }

    public async Task<ContratanteModel> CreateContratanteAsync(ContratanteModel contratante)
    {
        _context.Contratantes.Add(contratante);
        await _context.SaveChangesAsync();

        return contratante;
    }

    public async Task<bool> UpdateContratanteAsync(int id, ContratantePatchViewModel contratante)
    {
        var existingContratante = await _context.Contratantes.FindAsync(id);
        if (existingContratante == null)
        {
            return false;
        }

        existingContratante.Bloqueado = contratante.Bloqueado;

        _context.Entry(existingContratante).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ContratanteExistsAsync(id))
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

    public async Task<bool> ContratanteExistsAsync(int id)
    {
        return await _context.Contratantes.AnyAsync(e => e.Id == id);
    }

    public async Task<bool> ContratanteExistsByNameAsync(string nome)
    {
        return await _context.Contratantes.AnyAsync(e => e.Nome == nome);
    }

    public async Task<ContratanteModel> GetContratanteByNameAsync(string nome)
    {
        return await _context.Contratantes.FirstOrDefaultAsync(e => e.Nome == nome);
    }

    public async Task<bool> BloqueadoAsync(int id)
    {
        var contratante = await _context.Contratantes.FindAsync(id);
        return contratante?.Bloqueado ?? false;
    }
}
