using LEA_ProyectoMultimedia2024_V2_.Models.Tables;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Interfaces
{
    public interface IDescuento
    {
        Task<List<Descuento>> GetDescuentoAsync();          
        Task<Descuento> GetDescuentoByIdAsync(int id);      // Obtener 
        Task CreateDescuentoAsync(Descuento descuento);      // Crear 
        Task UpdateDescuentoAsync(Descuento descuento);      // Actualizar 
        Task DeleteDescuentoAsync(int id);                   // Eliminar 

        Task <Descuento>GetDetalleDescuento(int id);

        Task<Descuento> GetDetails(int id);
        Task<bool> DescuentoExist(int id);
    }
}
