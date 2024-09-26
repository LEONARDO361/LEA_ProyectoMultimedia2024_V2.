using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace LEA_ProyectoMultimedia2024_V2_.Services.Repository;


public class DescuentosRepository : IDescuento
{
    private readonly GimnasioContext _context;

    public DescuentosRepository(GimnasioContext context)
    {
        _context = context;
    }

    public async Task<List<Descuento>> GetDescuentoAsync()
    {
        return await _context.Descuento.ToListAsync(); 
    }

    public async Task<Descuento> GetDescuentoByIdAsync(int id)
    {
        return await _context.Descuento.FindAsync(id); 
    }

    public async Task CreateDescuentoAsync(Descuento descuento)
    {
        _context.Add(descuento); 
        await _context.SaveChangesAsync(); 
    }

    public async Task UpdateDescuentoAsync(Descuento descuento)
    {
        _context.Update(descuento); 
        await _context.SaveChangesAsync();
    }
    public async Task<Descuento> GetDetalleDescuento(int id)
    {
        return await _context.Descuento
       .FirstOrDefaultAsync(m => m.DescuentoId == id);

    }

    public async Task DeleteDescuentoAsync(int id)
    {
        var descuento = await _context.Descuento.FindAsync(id); 
        if (descuento != null)
        {
            _context.Descuento.Remove(descuento); 
            await _context.SaveChangesAsync(); 
        }
    }
}
