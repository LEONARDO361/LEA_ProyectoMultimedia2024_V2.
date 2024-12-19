using LEA_ProyectoMultimedia2024_V2_.Models.Tables;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Interfaces
{
    public interface IProducto
    {
        Task<List<Producto>> GetProductosAsync();            
        Task<Producto> GetProductoByIdAsync(int id);        
        Task CreateProductoAsync(Producto producto);        
        Task UpdateProductoAsync(Producto producto);        
        Task DeleteProductoAsync(int id);
        Task<bool> ProductExists(int id);
        Task<Producto> BuscadorProduct(int id);
        Task<List<Categoria>> GetCategoriasAsync();
    }

}
