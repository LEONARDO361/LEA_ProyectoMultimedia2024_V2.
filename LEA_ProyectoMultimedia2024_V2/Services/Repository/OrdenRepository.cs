using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Repository
{
    public class OrdenRepository : IOrden
    {
        private readonly GimnasioContext _context;

        public OrdenRepository(GimnasioContext context)
        {
            _context = context;
        }
        public async Task ActualizarCanastaAsync(Canasta canasta)
        {
            _context.Canasta.Update(canasta);
            await _context.SaveChangesAsync();
        }


        public async Task<List<Orden>> GetAllOrdenesAsync()
        {
            return await _context.Orden.Include(o => o.Cliente).ToListAsync();
        }

        public async Task<Orden> GetOrdenByIdAsync(int id)
        {
            return await _context.Orden.Include(o => o.Cliente)
                .FirstOrDefaultAsync(o => o.OrdenId == id);
        }

        public async Task CreateOrdenAsync(Orden orden)
        {
            _context.Add(orden);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrdenAsync(Orden orden)
        {
            _context.Update(orden);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrdenAsync(int id)
        {
            var orden = await _context.Orden.FindAsync(id);
            if (orden != null)
            {
                _context.Orden.Remove(orden);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> OrdenExistsAsync(int id)
        {
            return await _context.Orden.AnyAsync(o => o.OrdenId == id);
        }
        public async Task<List<Cliente>> GetAllOrdensAsync()
        {
            return await _context.Cliente.ToListAsync();
        }


        public async Task<Orden> BuscOrden(int id)
        {
            return await _context.Orden.FindAsync(id);
        }

        public async Task CreateOrdenAsync(Orden orden, List<DetalleOrden> detalles)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Guardar la orden
                _context.Orden.Add(orden);
                await _context.SaveChangesAsync();

                // Guardar los detalles de la orden
                foreach (var detalle in detalles)
                {
                    detalle.OrdenId = orden.OrdenId; // Relacionar con la orden creada
                    _context.DetalleOrden.Add(detalle);
                }
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

}
