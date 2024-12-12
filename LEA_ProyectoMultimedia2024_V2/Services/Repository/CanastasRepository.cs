using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Repositories;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Repository
{
    public class CanastasRepository : ICanastas
    {
        private readonly GimnasioContext _context;

        public CanastasRepository(GimnasioContext context)
        {
            _context = context;
        }

        public async Task<Canasta> ObtenerCanastaActiva(int clienteId)
        {
            return await _context.Canasta
                .Include(c => c.DetalleCanasta) // Incluir los detalles
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId && c.Estado == "Activa");
        }

        // Implementación de los otros métodos (agregar, actualizar, eliminar detalles y canasta)
        public async Task AgregarCanasta(Canasta canasta)
        {
            await _context.Canasta.AddAsync(canasta);
            await _context.SaveChangesAsync();
        }

        public async Task<DetalleCanasta> ObtenerDetalleCanasta(int canastaId, int productoId)
        {
            return await _context.DetalleCanasta
                .FirstOrDefaultAsync(dc => dc.CanastaId == canastaId && dc.ProductoId == productoId);
        }

        public async Task<DetalleCanasta> ObtenerDetalleCanastaPorId(int detalleCanastaId)
        {
            return await _context.DetalleCanasta
                .FirstOrDefaultAsync(dc => dc.DetalleCanastaId == detalleCanastaId);
        }

        public async Task<List<DetalleCanasta>> ObtenerDetallesPorCanastaId(int canastaId)
        {
            return await _context.DetalleCanasta
                .Where(dc => dc.CanastaId == canastaId)
                .ToListAsync();
        }

        public async Task AgregarDetalleCanasta(DetalleCanasta detalle)
        {
            await _context.DetalleCanasta.AddAsync(detalle);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarDetalleCanasta(DetalleCanasta detalle)
        {
            _context.DetalleCanasta.Update(detalle);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarCanasta(Canasta canasta)
        {
            _context.Canasta.Update(canasta);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarDetalleCanasta(DetalleCanasta detalle)
        {
            _context.DetalleCanasta.Remove(detalle);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarDetallesCanasta(IEnumerable<DetalleCanasta> detalles)
        {
            _context.DetalleCanasta.RemoveRange(detalles);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Producto>> ObtenerProductosDisponibles()
        {
            return await _context.Producto.Where(p => p.Cantidad > 0).ToListAsync();
        }

        public async Task<Producto> ObtenerProductoPorId(int productoId)
        {
            return await _context.Producto.FirstOrDefaultAsync(p => p.ProductoId == productoId);
        }
    }
}
