using LEA_ProyectoMultimedia2024_V2_.Models.Tables;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Interfaces
{
    public interface IOrden
    {
        Task<List<Orden>> GetAllOrdenesAsync();
        Task<Orden> GetOrdenByIdAsync(int id);
        Task CreateOrdenAsync(Orden orden);
        Task UpdateOrdenAsync(Orden orden);
        Task DeleteOrdenAsync(int id);
        Task<bool> OrdenExistsAsync(int id);

        Task<List<Cliente>> GetAllOrdensAsync();

        Task<Orden> BuscOrden(int id);

        Task CreateOrdenAsync(Orden orden, List<DetalleOrden> detalles);
        Task ActualizarCanastaAsync(Canasta canasta);


    }

}
