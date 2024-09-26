using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Repository
{
    public class ProductoRepository : IProducto
    {
        private readonly GimnasioContext _gamnasioContext;

        public ProductoRepository(GimnasioContext gimnasioContext)
        {
            _gamnasioContext = gimnasioContext;
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            return await _gamnasioContext.Producto.Include(p => p.Categoria).Include(p => p.Descuento).ToListAsync(); 
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            return await _gamnasioContext.Producto.Include(p => p.Categoria).FirstOrDefaultAsync(m => m.ProductoId == id); 
        }

        public async Task CreateProductoAsync(Producto producto)
        {
            _gamnasioContext.Add(producto); 
            await _gamnasioContext.SaveChangesAsync(); 
        }

        public async Task UpdateProductoAsync(Producto producto)
        {
            _gamnasioContext.Update(producto); 
            await _gamnasioContext.SaveChangesAsync(); 
        }

        public async Task DeleteProductoAsync(int id)
        {
            var producto = await _gamnasioContext.Producto.FindAsync(id); 
            if (producto != null)
            {
                _gamnasioContext.Producto.Remove(producto); 
                await _gamnasioContext.SaveChangesAsync();
            }
        }
    }



}
