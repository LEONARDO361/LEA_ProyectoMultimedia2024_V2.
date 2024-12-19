using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NBO_ProyectoMultimedia2024.Models.ViewModels;
using Newtonsoft.Json;
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
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            try
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

                return View(canasta.DetalleCanasta.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el método Index: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                ViewBag.Mensaje = "Ocurrió un error al cargar la canasta.";
                return View(new List<DetalleCanasta>());
            }
        }


        [HttpPost("GenerarFactura")]
        public async Task<IActionResult> GenerarFactura([FromBody] List<DetalleCanasta> detallesCanasta)
        {
            if (detallesCanasta == null || !detallesCanasta.Any())
            {
                return Json(new { success = false, message = "El carrito está vacío. Agregue productos antes de continuar." });
            }

            try
            {
                // Simular la generación de la factura temporal
                var facturaTemporal = new FacturaViewModel
                {
                    DetalleProductos = detallesCanasta.Select(detalle => new DetalleFacturaViewModel
                    {
                        ProductoId = detalle.ProductoId,
                        Cantidad = detalle.Cantidad,
                        PrecioUnitario = detalle.PrecioUni,
                        PrecioTotal = detalle.SubTotal
                    }).ToList(),
                    PrecioTotal = detallesCanasta.Sum(d => d.SubTotal)
                };

                // Guardar temporalmente la factura en la sesión (o pasarla al siguiente paso)
                TempData["FacturaTemporal"] = JsonConvert.SerializeObject(facturaTemporal);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al generar la factura: {ex.Message}" });
            }
        }

        private int GetClienteId()
        {
            var clienteIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (clienteIdClaim != null && int.TryParse(clienteIdClaim.Value, out int clienteId))
            {
                return clienteId;
            }
            throw new UnauthorizedAccessException("No se pudo obtener el ID del cliente.");
        }
    }
}
