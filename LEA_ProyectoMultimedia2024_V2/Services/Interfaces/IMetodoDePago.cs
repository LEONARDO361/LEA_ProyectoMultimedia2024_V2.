using LEA_ProyectoMultimedia2024_V2_.Models.Tables;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Interfaces
{
    public interface IMetodoDePago
    {
        Task<List<MetodoPago>> GetAllMetodosPagoAsync();
        Task<MetodoPago> GetMetodoPagoByIdAsync(int id);
        Task CreateMetodoPagoAsync(MetodoPago metodoPago);
        Task UpdateMetodoPagoAsync(MetodoPago metodoPago);
        Task DeleteMetodoPagoAsync(int id);
        Task<bool> MetodoPagoExistsAsync(int id);
    }

}
