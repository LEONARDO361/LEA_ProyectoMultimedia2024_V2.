using LEA_ProyectoMultimedia2024_V2_.Models.Tables;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Interfaces
{
    public interface IDireccionEnvios
    {
        Task<List<DireccionEnvio>> GetAllDireccionesAsync();
        Task<DireccionEnvio> GetDireccionByIdAsync(int id);
        Task CreateDireccionAsync(DireccionEnvio direccionEnvio);
        Task UpdateDireccionAsync(DireccionEnvio direccionEnvio);
        Task DeleteDireccionAsync(int id);
        Task<bool> DireccionEnvioExistsAsync(int id);
    }
}
