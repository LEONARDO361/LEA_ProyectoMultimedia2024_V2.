using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Repository
{
    public class MetodoDePagoRepository : IMetodoDePago
    {
        private readonly GimnasioContext _context;

        public MetodoDePagoRepository(GimnasioContext context)
        {
            _context = context;
        }

        public async Task<List<MetodoPago>> GetAllMetodosPagoAsync()
        {
            return await _context.MetodoPago.Include(m => m.Cliente).ToListAsync();
        }

        public async Task<MetodoPago> GetMetodoPagoByIdAsync(int id)
        {
            return await _context.MetodoPago.Include(m => m.Cliente)
                .FirstOrDefaultAsync(m => m.MetodoPagoId == id);
        }

        public async Task CreateMetodoPagoAsync(MetodoPago metodoPago)
        {
            _context.Add(metodoPago);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMetodoPagoAsync(MetodoPago metodoPago)
        {
            _context.Update(metodoPago);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMetodoPagoAsync(int id)
        {
            var metodoPago = await _context.MetodoPago.FindAsync(id);
            if (metodoPago != null)
            {
                _context.MetodoPago.Remove(metodoPago);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> MetodoPagoExistsAsync(int id)
        {
            return await _context.MetodoPago.AnyAsync(e => e.MetodoPagoId == id);
        }
    }

}
