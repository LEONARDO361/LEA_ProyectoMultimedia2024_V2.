using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Interfaces
{
    public interface IOrdenRepository
    {
        Task<List<Orden>> GetAllOrdenesAsync();
        Task<Orden> GetOrdenByIdAsync(int ordenId);
        Task<bool> CreateOrdenAsync(Orden orden, List<DetalleOrden> detalles);
        Task<bool> OrdenExistsAsync(int ordenId);
        Task DeleteOrdenAsync(int ordenId);
    }
}