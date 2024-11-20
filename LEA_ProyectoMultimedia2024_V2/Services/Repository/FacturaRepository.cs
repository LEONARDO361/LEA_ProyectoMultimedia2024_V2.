using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Repository
{
    public class FacturaRepository: IOrdenRepository
    {
        private readonly GimnasioContext _context;

        public async Task<bool> CreateOrdenAsync(Orden orden, List<DetalleOrden> detalles)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Orden.Add(orden);
                    await _context.SaveChangesAsync();

                    foreach (var detalle in detalles)
                    {
                        detalle.OrdenId = orden.OrdenId;
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
        public async Task<List<Orden>> GetAllOrdenesAsync()
        {
            // Devuelve todas las órdenes con sus detalles
            return await _context.Orden
                .Include(o => o.DetalleOrden)
                .ToListAsync();
        }

        public async Task<Orden> GetOrdenByIdAsync(int ordenId)
        {
            // Busca una orden específica por ID, incluyendo sus detalles
            return await _context.Orden
                .Include(o => o.DetalleOrden)
                .FirstOrDefaultAsync(o => o.OrdenId == ordenId);
        }

        public async Task<bool> OrdenExistsAsync(int ordenId)
        {
            // Verifica si existe una orden con el ID proporcionado
            return await _context.Orden.AnyAsync(o => o.OrdenId == ordenId);
        }

        public async Task DeleteOrdenAsync(int ordenId)
        {
            // Busca la orden y elimina tanto la orden como sus detalles
            var orden = await _context.Orden
                .Include(o => o.DetalleOrden)
                .FirstOrDefaultAsync(o => o.OrdenId == ordenId);

            if (orden != null)
            {
                _context.DetalleOrden.RemoveRange(orden.DetalleOrden); // Elimina los detalles
                _context.Orden.Remove(orden); // Elimina la orden principal
                await _context.SaveChangesAsync();
            }
        }


    }
}
