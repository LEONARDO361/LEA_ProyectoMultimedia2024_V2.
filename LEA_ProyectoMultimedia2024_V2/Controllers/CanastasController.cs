using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LEA_ProyectoMultimedia2024_V2_.Controllers
{
    [Authorize]
    [Route("Canastas")]
    public class CanastasController : Controller
    {
        private readonly ICanastas _canastas;

        public CanastasController(ICanastas canastasRepository)
        {
            _canastas = canastasRepository;
        }

        private int GetClienteId()
        {
            var clienteIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (clienteIdClaim != null && int.TryParse(clienteIdClaim.Value, out int clienteId))
            {
                return clienteId;
            }
            throw new UnauthorizedAccessException("No se pudo obtener el ID del cliente. Por favor, inicie sesión nuevamente.");
        }

        [HttpPost("ActualizarCanasta")]
        public async Task<IActionResult> ActualizarCanasta([FromBody] DetalleCanasta detalle)
        {
            if (detalle == null || detalle.ProductoId == 0 || detalle.Cantidad <= 0 || detalle.PrecioUni <= 0)
            {
                return Json(new { success = false, message = "Datos del producto no válidos." });
            }

            try
            {
                int clienteId = 1; // Simulación de clienteId, ajusta esto según tu autenticación.

                // Verificar si existe una canasta activa
                var canasta = await _canastas.ObtenerCanastaActiva(clienteId);

                if (canasta == null)
                {
                    // Crear nueva canasta
                    canasta = new Canasta
                    {
                        ClienteId = clienteId,
                        FechaC = DateOnly.FromDateTime(DateTime.Now),
                        Estado = "Activa",
                        Total = 0
                    };
                    await _canastas.AgregarCanasta(canasta);
                }

                // Verificar si el producto ya existe en la canasta
                var detalleExistente = await _canastas.ObtenerDetalleCanasta(canasta.CanastaId, detalle.ProductoId);

                if (detalleExistente == null)
                {
                    // Agregar nuevo detalle
                    detalle.CanastaId = canasta.CanastaId;
                    detalle.SubTotal = detalle.Cantidad * detalle.PrecioUni;
                    await _canastas.AgregarDetalleCanasta(detalle);
                }
                else
                {
                    // Actualizar detalle existente
                    detalleExistente.Cantidad += detalle.Cantidad;
                    detalleExistente.SubTotal = detalleExistente.Cantidad * detalleExistente.PrecioUni;
                    await _canastas.ActualizarDetalleCanasta(detalleExistente);
                }

                // Actualizar el total de la canasta
                var detallesActualizados = await _canastas.ObtenerDetallesPorCanastaId(canasta.CanastaId);
                canasta.Total = detallesActualizados.Sum(dc => dc.SubTotal);
                await _canastas.ActualizarCanasta(canasta);

                return Json(new { success = true, message = "Canasta actualizada correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al actualizar la canasta: {ex.Message}" });
            }
        }



        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            int clienteId = GetClienteId(); // Obtener el cliente logueado
            var canasta = await _canastas.ObtenerCanastaActiva(clienteId);

            if (canasta == null)
            {
                ViewBag.Mensaje = "No se encontró una canasta activa.";
                return View(new List<DetalleCanasta>());
            }

            if (canasta.DetalleCanasta == null || !canasta.DetalleCanasta.Any())
            {
                ViewBag.Mensaje = "La canasta está vacía.";
                return View(new List<DetalleCanasta>());
            }

            // Depuración: muestra los detalles en la consola del servidor.
            foreach (var detalle in canasta.DetalleCanasta)
            {
                Console.WriteLine($"Producto ID: {detalle.ProductoId}, Cantidad: {detalle.Cantidad}, SubTotal: {detalle.SubTotal}");
            }

            return View(canasta.DetalleCanasta.ToList());
        }


        [HttpPost("QuitarProducto")]
        public async Task<IActionResult> QuitarProducto(int detalleCanastaId)
        {
            var detalle = await _canastas.ObtenerDetalleCanastaPorId(detalleCanastaId);

            if (detalle != null)
            {
                if (detalle.Cantidad > 1)
                {
                    detalle.Cantidad--;
                    detalle.SubTotal = detalle.Cantidad * detalle.PrecioUni;
                    await _canastas.ActualizarDetalleCanasta(detalle);
                }
                else
                {
                    // Usar el método que elimina un solo detalle
                    await _canastas.EliminarDetalleCanasta(detalle);
                }

                var canasta = await _canastas.ObtenerCanastaActiva(detalle.CanastaId);
                if (canasta != null)
                {
                    canasta.Total = canasta.DetalleCanasta.Sum(dc => dc.SubTotal);
                    await _canastas.ActualizarCanasta(canasta);
                }

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost("VaciarCanasta")]
        public async Task<IActionResult> VaciarCanasta()
        {
            int clienteId = GetClienteId(); // Obtener el cliente logueado
            var canasta = await _canastas.ObtenerCanastaActiva(clienteId);

            if (canasta != null)
            {
                await _canastas.EliminarDetallesCanasta(canasta.DetalleCanasta);
                canasta.Estado = "Vacía";
                canasta.Total = 0;
                await _canastas.ActualizarCanasta(canasta);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
