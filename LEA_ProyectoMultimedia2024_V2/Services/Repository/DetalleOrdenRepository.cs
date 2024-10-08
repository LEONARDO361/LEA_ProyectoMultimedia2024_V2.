using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Repository
{
    public class DetalleOrdenRepository : IDetalleOrden
    {
        private readonly GimnasioContext _context;

        public DetalleOrdenRepository(GimnasioContext context)
        {
            _context = context;
        }

        public async Task<List<DetalleOrden>> GetDetalleOrdensAsync()
        {
            return await _context.DetalleOrden.Include(d => d.Orden).Include(d => d.Producto).ToListAsync();
        }

        public async Task<DetalleOrden> GetDetalleOrdenByIdAsync(int id)
        {
            return await _context.DetalleOrden.Include(d => d.Orden).Include(d => d.Producto).FirstOrDefaultAsync(m => m.DetalleId == id); 
        }

        public async Task<bool> CreateDetalleOrdenAsync(DetalleOrden detalleOrden)
        {
            _context.Add(detalleOrden); 
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateDetalleOrdenAsync(DetalleOrden detalleOrden)
        {
            _context.Update(detalleOrden); 
            await _context.SaveChangesAsync(); 
        }

        public async Task<List<Orden>> GetOrdenesAsync()
        {
            return await _context.Orden.ToListAsync();
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            return await _context.Producto.ToListAsync();
        }


        public async Task DeleteDetalleOrdenAsync(int id)
        {
            var detalleOrden = await _context.DetalleOrden.FindAsync(id); 
            if (detalleOrden != null)
            {
                _context.DetalleOrden.Remove(detalleOrden); 
                await _context.SaveChangesAsync(); 
            }
        }

    }

}
