using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Repository
{
    public class DireccionEnviosRepository : IDireccionEnvios
    {
        private readonly GimnasioContext _context;

        public DireccionEnviosRepository(GimnasioContext context)
        {
            _context = context;
        }

        public async Task<List<DireccionEnvio>> GetAllDireccionesAsync()
        {
            return await _context.DireccionEnvio.Include(d => d.Cliente).ToListAsync();
        }

        public async Task<DireccionEnvio> GetDireccionByIdAsync(int id)
        {
            return await _context.DireccionEnvio.Include(d => d.Cliente)
                .FirstOrDefaultAsync(m => m.DireccionId == id);
        }

        public async Task CreateDireccionAsync(DireccionEnvio direccionEnvio)
        {
            _context.Add(direccionEnvio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDireccionAsync(DireccionEnvio direccionEnvio)
        {
            _context.Update(direccionEnvio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDireccionAsync(int id)
        {
            var direccionEnvio = await _context.DireccionEnvio.FindAsync(id);
            if (direccionEnvio != null)
            {
                _context.DireccionEnvio.Remove(direccionEnvio);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DireccionEnvioExistsAsync(int id)
        {
            return await _context.DireccionEnvio.AnyAsync(e => e.DireccionId == id);
        }
    }
}
