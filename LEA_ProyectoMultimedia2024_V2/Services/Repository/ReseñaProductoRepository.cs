using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Repository
{
    public class ReseñaProductoRepository : IReseñaProducto
    {
        private readonly GimnasioContext _context;

        public ReseñaProductoRepository(GimnasioContext context)
        {
            _context = context;
        }

        public async Task<List<ReseñaProducto>> GetAllReseñasAsync()
        {
            return await _context.ReseñaProducto.Include(r => r.Cliente).Include(r => r.Producto).ToListAsync();
        }

        public async Task<ReseñaProducto> GetReseñaByIdAsync(int id)
        {
            return await _context.ReseñaProducto
                .Include(r => r.Cliente)
                .Include(r => r.Producto)
                .FirstOrDefaultAsync(m => m.ReseñaId == id);
        }

        public async Task CreateReseñaAsync(ReseñaProducto reseña)
        {
            _context.Add(reseña);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReseñaAsync(ReseñaProducto reseña)
        {
            _context.Update(reseña);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReseñaAsync(int id)
        {
            var reseña = await _context.ReseñaProducto.FindAsync(id);
            if (reseña != null)
            {
                _context.ReseñaProducto.Remove(reseña);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ReseñaExistsAsync(int id)
        {
            return await _context.ReseñaProducto.AnyAsync(e => e.ReseñaId == id);
        }
    }

}
