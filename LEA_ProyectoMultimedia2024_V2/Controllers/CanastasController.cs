using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using Microsoft.AspNetCore.Authorization;
using LEA_ProyectoMultimedia2024_V2_.Repositories; 

namespace LEA_ProyectoMultimedia2024_V2_.Controllers
{
    public class CanastasController : Controller
    {
        private readonly ICanastas _canastas;

        public CanastasController(ICanastas canastasRepository)
        {
            _canastas = canastasRepository;
        }

        // GET: Mostrar las canastas
        public async Task<IActionResult> Index(int clienteId)
        {
            var canasta = await _canastas.ObtenerCanastaActiva(clienteId);
            var productos = await _canastas.ObtenerProductosDisponibles();

            ViewBag.Productos = productos;

            if (canasta == null || canasta.DetalleCanasta == null || !canasta.DetalleCanasta.Any())
            {
                ViewBag.Mensaje = "La canasta está vacía.";
                return View(new List<DetalleCanasta>());
            }

            return View(canasta.DetalleCanasta.ToList());
        }

        // POST: Agregar un producto a la canasta
        [HttpPost]
        public async Task<IActionResult> AgregarACanasta(int clienteId, int productoId, int cantidad = 1)
        {
            var canasta = await _canastas.ObtenerCanastaActiva(clienteId);

            if (canasta == null)
            {
                canasta = new Canasta
                {
                    ClienteId = clienteId,
                    FechaC = DateOnly.FromDateTime(DateTime.Now),
                    Estado = "Activa",
                    Total = 0
                };

                await _canastas.AgregarCanasta(canasta);
            }

            var detalle = await _canastas.ObtenerDetalleCanasta(canasta.CanastaId, productoId);

            if (detalle == null)
            {
                var producto = await _canastas.ObtenerProductoPorId(productoId);
                if (producto == null) return NotFound("Producto no encontrado.");

                detalle = new DetalleCanasta
                {
                    CanastaId = canasta.CanastaId,
                    ProductoId = productoId,
                    Cantidad = cantidad,
                    PrecioUni = producto.Precio,
                    SubTotal = cantidad * producto.Precio
                };

                await _canastas.AgregarCanasta(canasta);
            }
            else
            {
                detalle.Cantidad += cantidad;
                detalle.SubTotal = detalle.Cantidad * detalle.PrecioUni;
                await _canastas.ActualizarDetalleCanasta(detalle);
            }

            canasta.Total = canasta.DetalleCanasta.Sum(dc => dc.SubTotal);
            await _canastas.ActualizarCanasta(canasta);

            return RedirectToAction(nameof(Index), new { clienteId });
        }

        // POST: Quitar una unidad de un producto de la canasta
        [HttpPost]
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
                    await _canastas.EliminarDetalleCanasta(detalle);
                }

                var canasta = await _canastas.ObtenerCanastaActiva(detalle.CanastaId);
                if (canasta != null)
                {
                    canasta.Total = canasta.DetalleCanasta.Sum(dc => dc.SubTotal);
                    await _canastas.ActualizarCanasta(canasta);
                }

                return RedirectToAction(nameof(Index), new { clienteId = canasta.ClienteId });
            }

            return RedirectToAction(nameof(Index), new { clienteId = 0 }); // O maneja el error de otra manera
        }

        // POST: Vaciar la canasta
        [HttpPost]
        public async Task<IActionResult> VaciarCanasta(int clienteId)
        {
            var canasta = await _canastas.ObtenerCanastaActiva(clienteId);

            if (canasta != null)
            {
                await _canastas.EliminarDetallesCanasta(canasta.DetalleCanasta);
                canasta.Estado = "Vacía";
                canasta.Total = 0;
                await _canastas.ActualizarCanasta(canasta);
            }

            return RedirectToAction(nameof(Index), new { clienteId });
        }
    }
}