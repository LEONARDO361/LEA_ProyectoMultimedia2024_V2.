using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LEA_ProyectoMultimedia2024_V2_.Repositories
{
    public interface ICanastas
    {
        /// <summary>
        /// Obtiene los detalles de la canasta activa de un cliente.
        /// </summary>
        /// <param name="clienteId">El ID del cliente.</param>
        /// <returns>Una lista de detalles de la canasta activa.</returns>
        Task<IEnumerable<DetalleCanasta>> ObtenerCanastaPorClienteIdAsync(int clienteId);

        /// <summary>
        /// Obtiene la canasta activa de un cliente.
        /// </summary>
        /// <param name="clienteId">El ID del cliente.</param>
        /// <returns>La canasta activa o null si no existe.</returns>
        Task<Canasta> ObtenerCanastaActiva(int clienteId);

        /// <summary>
        /// Agrega un producto a la canasta del cliente.
        /// </summary>
        /// <param name="detalle">Detalle de la canasta a agregar.</param>
        Task AgregarProductoACanastaAsync(DetalleCanasta detalle);

        /// <summary>
        /// Crea una nueva canasta para un cliente.
        /// </summary>
        /// <param name="canasta">La canasta a crear.</param>
        Task CrearCanastaAsync(Canasta canasta);

        /// <summary>
        /// Vacía todos los productos de la canasta de un cliente.
        /// </summary>
        /// <param name="clienteId">El ID del cliente.</param>
        Task VaciarCanastaAsync(int clienteId);

        /// <summary>
        /// Elimina un producto específico de la canasta del cliente.
        /// </summary>
        /// <param name="clienteId">El ID del cliente.</param>
        /// <param name="productoId">El ID del producto a eliminar.</param>
        Task EliminarProductoDeCanastaAsync(int clienteId, int productoId);

        /// <summary>
        /// Actualiza la cantidad de un producto en la canasta del cliente.
        /// </summary>
        /// <param name="clienteId">El ID del cliente.</param>
        /// <param name="productoId">El ID del producto a actualizar.</param>
        /// <param name="cantidad">La nueva cantidad.</param>
        Task ActualizarCantidadProductoAsync(int clienteId, int productoId, int cantidad);

        /// <summary>
        /// Obtiene el precio de un producto por su ID.
        /// </summary>
        /// <param name="productoId">El ID del producto.</param>
        /// <returns>El precio del producto.</returns>
        Task<decimal> ObtenerPrecioProductoAsync(int productoId);

        /// <summary>
        /// Verifica si existe una canasta activa para un cliente.
        /// </summary>
        /// <param name="clienteId">El ID del cliente.</param>
        /// <returns>True si existe una canasta activa, false de lo contrario.</returns>
        Task<bool> ExisteCanastaActiva(int clienteId);
        Task<bool> GuardarCanasta(Canasta canasta, List<DetalleCanasta> detalles);

    }
}
