using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LEA_ProyectoMultimedia2024_V2_.Repositories
{
    public interface ICanastas
    {
        Task<Canasta> ObtenerCanastaActiva(int clienteId);
        Task<List<Producto>> ObtenerProductosDisponibles();
        Task AgregarCanasta(Canasta canasta);
        Task<DetalleCanasta> ObtenerDetalleCanasta(int canastaId, int productoId);
        Task<DetalleCanasta> ObtenerDetalleCanastaPorId(int detalleCanastaId);
        Task<List<DetalleCanasta>> ObtenerDetallesPorCanastaId(int canastaId);
        Task AgregarDetalleCanasta(DetalleCanasta detalle);
        Task ActualizarDetalleCanasta(DetalleCanasta detalle);
        Task EliminarDetalleCanasta(DetalleCanasta detalle);
        Task EliminarDetallesCanasta(IEnumerable<DetalleCanasta> detalles);
        Task ActualizarCanasta(Canasta canasta);
        Task<Producto> ObtenerProductoPorId(int productoId);
    }



}
