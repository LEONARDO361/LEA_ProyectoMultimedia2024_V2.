using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Repository
{
    public class FacturaRepository : IOrdenRepository
    {
        private readonly GimnasioContext _context;

        public FacturaRepository(GimnasioContext context)
        {
            _context = context;
        }

        public async Task<List<Orden>> GetAllOrdenesAsync()
        {
            return await _context.Orden
                .Include(o => o.DetalleOrden)
                .ToListAsync();
        }

        public async Task<Orden> GetOrdenByIdAsync(int ordenId)
        {
            return await _context.Orden
                .Include(o => o.DetalleOrden)
                .FirstOrDefaultAsync(o => o.OrdenId == ordenId);
        }

        public async Task<bool> CreateOrdenAsync(Orden orden, List<DetalleOrden> detalles)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Agregar la orden principal
                    _context.Orden.Add(orden);
                    await _context.SaveChangesAsync();

                    // Agregar los detalles de la orden
                    foreach (var detalle in detalles)
                    {
                        detalle.OrdenId = orden.OrdenId; // Vincular con la orden recién creada
                        _context.DetalleOrden.Add(detalle);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }

        public async Task<bool> OrdenExistsAsync(int ordenId)
        {
            return await _context.Orden.AnyAsync(o => o.OrdenId == ordenId);
        }

        public async Task DeleteOrdenAsync(int ordenId)
        {
            var orden = await _context.Orden
                .Include(o => o.DetalleOrden)
                .FirstOrDefaultAsync(o => o.OrdenId == ordenId);

            if (orden != null)
            {
                // Eliminar los detalles de la orden
                _context.DetalleOrden.RemoveRange(orden.DetalleOrden);

                // Eliminar la orden principal
                _context.Orden.Remove(orden);
                await _context.SaveChangesAsync();
            }
        }
    }
}
