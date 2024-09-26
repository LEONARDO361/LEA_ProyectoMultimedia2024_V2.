using LEA_ProyectoMultimedia2024_V2_.Models.Tables;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Interfaces
{
    public interface IReseñaProducto
    {
        Task<List<ReseñaProducto>> GetAllReseñasAsync();
        Task<ReseñaProducto> GetReseñaByIdAsync(int id);
        Task CreateReseñaAsync(ReseñaProducto reseña);
        Task UpdateReseñaAsync(ReseñaProducto reseña);
        Task DeleteReseñaAsync(int id);
        Task<bool> ReseñaExistsAsync(int id);
    }

}
