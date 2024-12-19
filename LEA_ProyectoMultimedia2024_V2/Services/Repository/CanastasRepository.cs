using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Repository
{
    public class CanastasRepository : ICanastas
    {
        private readonly GimnasioContext _context;

        public CanastasRepository(GimnasioContext context)
        {
            _context = context;
        }
        public async Task<bool> GuardarCanasta(Canasta canasta, List<DetalleCanasta> detalles)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Guardar la canasta
                    _context.Canasta.Add(canasta);
                    await _context.SaveChangesAsync();

                    // Asociar los detalles con la canasta recién creada
                    foreach (var detalle in detalles)
                    {
                        detalle.CanastaId = canasta.CanastaId;
                        _context.DetalleCanasta.Add(detalle);
                    }

                    // Guardar los detalles
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error al guardar la canasta: {ex.Message}");
                    return false;
                }
            }
        }


        public async Task<IEnumerable<DetalleCanasta>> ObtenerCanastaPorClienteIdAsync(int clienteId)
        {
            return await _context.DetalleCanasta
                .Include(dc => dc.Producto)
                .Where(dc => dc.Canasta.ClienteId == clienteId && dc.Canasta.Estado == "Activa")
                .ToListAsync();
        }

        public async Task<Canasta> ObtenerCanastaActiva(int clienteId)
        {
            return await _context.Canasta
                .Include(c => c.DetalleCanasta)
                .ThenInclude(dc => dc.Producto)
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId && c.Estado == "Activa");
        }

        public async Task AgregarProductoACanastaAsync(DetalleCanasta detalle)
        {
            _context.DetalleCanasta.Add(detalle);
            await _context.SaveChangesAsync();
        }

        public async Task CrearCanastaAsync(Canasta canasta)
        {
            _context.Canasta.Add(canasta);
            await _context.SaveChangesAsync();
        }

        public async Task VaciarCanastaAsync(int clienteId)
        {
            var detalles = _context.DetalleCanasta.Where(dc => dc.Canasta.ClienteId == clienteId);
            _context.DetalleCanasta.RemoveRange(detalles);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarProductoDeCanastaAsync(int clienteId, int productoId)
        {
            var detalle = await _context.DetalleCanasta
                .FirstOrDefaultAsync(dc => dc.Canasta.ClienteId == clienteId && dc.ProductoId == productoId);
            if (detalle != null)
            {
                _context.DetalleCanasta.Remove(detalle);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ActualizarCantidadProductoAsync(int clienteId, int productoId, int cantidad)
        {
            var detalle = await _context.DetalleCanasta
                .FirstOrDefaultAsync(dc => dc.Canasta.ClienteId == clienteId && dc.ProductoId == productoId);
            if (detalle != null && cantidad > 0)
            {
                detalle.Cantidad = cantidad;
                detalle.SubTotal = cantidad * detalle.PrecioUni;
                _context.DetalleCanasta.Update(detalle);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal> ObtenerPrecioProductoAsync(int productoId)
        {
            var producto = await _context.Producto.FirstOrDefaultAsync(p => p.ProductoId == productoId);
            if (producto == null)
            {
                throw new KeyNotFoundException($"Producto con ID {productoId} no encontrado.");
            }
            return producto.Precio;
        }

        public async Task<bool> ExisteCanastaActiva(int clienteId)
        {
            return await _context.Canasta.AnyAsync(c => c.ClienteId == clienteId && c.Estado == "Activa");
        }
    }
}
