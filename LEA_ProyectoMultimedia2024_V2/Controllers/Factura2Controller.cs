using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NBO_ProyectoMultimedia2024.Models.ViewModels;
using Newtonsoft.Json;

namespace LEA_ProyectoMultimedia2024_V2_.Controllers
{
    public class Factura2Controller : Controller
    {
        private readonly IOrdenRepository _ordenRepository;

        public Factura2Controller(IOrdenRepository ordenRepository)
        {
            _ordenRepository = ordenRepository;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            // Cargar la factura temporal
            var facturaTemporalJson = TempData["FacturaTemporal"] as string;
            if (string.IsNullOrEmpty(facturaTemporalJson))
            {
                ViewBag.Mensaje = "No se encontró una factura generada.";
                return View(new FacturaViewModel());
            }

            var facturaTemporal = JsonConvert.DeserializeObject<FacturaViewModel>(facturaTemporalJson);
            return View(facturaTemporal);
        }

        [HttpPost("FinalizarCompra")]
        public async Task<IActionResult> FinalizarCompra([FromBody] FacturaViewModel factura)
        {
            if (!ModelState.IsValid || factura == null || !factura.DetalleProductos.Any())
            {
                return Json(new { success = false, message = "Los datos de la factura no son válidos." });
            }

            // Crear la orden
            var nuevaOrden = new Orden
            {
                FechaOrden = DateOnly.FromDateTime(DateTime.Now),
                Total = (int)factura.PrecioTotal,
                Estado = "Completada",
                ClienteId = factura.IdCliente
            };

            var detalleOrden = factura.DetalleProductos.Select(d => new DetalleOrden
            {
                ProductoId = d.ProductoId,
                Cantidad = d.Cantidad,
                PrecioTotal = d.PrecioTotal
            }).ToList();

            try
            {
                await _ordenRepository.CreateOrdenAsync(nuevaOrden, detalleOrden);

                // Limpiar el carrito después de guardar la orden
                TempData.Remove("FacturaTemporal");
                return Json(new { success = true, message = "Compra finalizada correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al finalizar la compra: {ex.Message}" });
            }
        }
    }
}
