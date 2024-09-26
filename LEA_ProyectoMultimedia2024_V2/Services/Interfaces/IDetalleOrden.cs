using LEA_ProyectoMultimedia2024_V2_.Models.Tables;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Interfaces
{
    public interface IDetalleOrden
    {
        Task<List<DetalleOrden>> GetDetalleOrdensAsync();             
        Task<DetalleOrden> GetDetalleOrdenByIdAsync(int id);         
        Task <bool> CreateDetalleOrdenAsync(DetalleOrden detalleOrden);      
        Task UpdateDetalleOrdenAsync(DetalleOrden detalleOrden);    
        Task DeleteDetalleOrdenAsync(int id);                        
    }

}
