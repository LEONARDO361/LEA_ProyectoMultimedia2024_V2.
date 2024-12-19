using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using LEA_ProyectoMultimedia2024_V2_.Models.DTOs;
using LEA_ProyectoMultimedia2024_V2_.Repositories;
using LEA_ProyectoMultimedia2024_V2_.Services.Repository;
using System.Security.Claims;

namespace LEA_ProyectoMultimedia2024_V2_.Controllers
{
    public class OrdensController : Controller
    {

        private readonly IOrden _Orden;
        private readonly ICanastas _canastas;

        public OrdensController(GimnasioContext context, IOrden orden, ICanastas canastas)
        {

            _Orden = orden;
            _canastas = canastas;
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

        // GET: Ordens
        public async Task<IActionResult> Index()
        {
            var gimnasioContext =await _Orden.GetAllOrdenesAsync();
            return View( gimnasioContext);
        }
        public async Task<IActionResult> listaOrden()
        {
            var gimnasioContext = await _Orden.GetAllOrdenesAsync();
            return PartialView(gimnasioContext);
        }

        // GET: Ordens/Details/5
        public async Task<IActionResult> PVDetails(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("ID inválido.");
                }

                // Obtén la orden específica por su ID
                var orden = await _Orden.BuscOrden(id.Value);
                if (orden == null)
                {
                    return NotFound("Orden no encontrada.");
                }

                // Asegúrate de que GetAllClientesAsync devuelva una lista válida
                var clientes = await _Orden.GetAllOrdensAsync();
                if (clientes == null || !clientes.Any())
                {
                    return StatusCode(500, "No se pudieron cargar los clientes.");
                }

                // Configura el SelectList para ClienteId
                ViewBag.ClienteId = new SelectList(clientes, "ClienteId", "Nombre");

                return PartialView(orden);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en PVDetails: {ex.Message}");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // GET: Ordens/Create
        public async Task <IActionResult> PVCreate()
        {
            ViewData["ClienteId"] = new SelectList(await _Orden.GetAllOrdensAsync(), "ClienteId", "Nombre");
            return PartialView();
        }

        // POST: Ordens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVCreate([Bind("OrdenId,FechaOrden,Total,Estado,ClienteId")] OrdenDTO orden)
        {
            if (ModelState.IsValid)
            {
                var DTO = orden.toOriginal();
                await _Orden.CreateOrdenAsync(DTO);

                TempData["SuccessMessage"] = "Orden creada exitosamente";
                return RedirectToAction("Index", "Mantenedores");
            }
            else
            {
                ViewData["ClienteId"] = new SelectList(await _Orden.GetAllOrdensAsync(), "ClienteId", "Nombre");
                ModelState.AddModelError("", "Hay errores en el formulario. Por favor, revísalos.");
                return View(orden);
            }
        }

        // GET: Ordens/Edit/5
        public async Task<IActionResult> PVEdit(int? id)
        {
           
            var orden = await _Orden.BuscOrden(id.Value);
            ViewData["ClienteId"] = (await _Orden.BuscOrden(id.Value) , "ClienteId", "Nombre");
            var OrdenDTO = orden.toDto;
            return PartialView(OrdenDTO);
        }

        // POST: Ordens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVEdit(int id, [Bind("OrdenId,FechaOrden,Total,Estado,ClienteId")] OrdenDTO orden)
        {
            if (ModelState.IsValid)
            {
                var OrdenOriginal = orden.toOriginal();
                await _Orden.UpdateOrdenAsync(OrdenOriginal);

                // Manejo de solicitud AJAX
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = "Orden actualizada exitosamente" });
                }
                else
                {
                    TempData["SuccessMessage"] = "Orden actualizada exitosamente";
                    return RedirectToAction("Index", "Mantenedores");
                }
            }
            else
            {
                // Obtener los errores de ModelState
                var errors = ModelState
                    .Where(a => a.Value.Errors.Any())
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    );

                // Manejo de solicitud AJAX en caso de error
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, errors });
                }
                else
                {
                    // Regresar la vista con el modelo y los errores de validación
                    ModelState.AddModelError("", "Hay errores en el formulario. Por favor, revísalos.");
                    return View(orden);
                }
            }
        }


        // POST: Ordens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          

            await _Orden.DeleteOrdenAsync(id);
            return RedirectToAction("Index","Mantenedores");
        }

        [HttpPost]
        [Route("Ordenes/GenerarPedido")]
        public async Task<IActionResult> GenerarPedido()
        {
            try
            {
                int clienteId = GetClienteId(); // Obtener el cliente logueado

                // Obtener la canasta activa del cliente
                var canasta = await _canastas.ObtenerCanastaActiva(clienteId);
                if (canasta == null || !canasta.DetalleCanasta.Any())
                {
                    return Json(new { success = false, message = "No hay productos en la canasta para generar un pedido." });
                }

                // Crear la orden
                var nuevaOrden = new Orden
                {
                    FechaOrden = DateOnly.FromDateTime(DateTime.Now),
                    Total = canasta.Total,
                    Estado = "Pendiente",
                    ClienteId = clienteId
                };

                // Transformar los detalles de la canasta en detalles de la orden
                var detallesOrden = canasta.DetalleCanasta.Select(detalle => new DetalleOrden
                {
                    ProductoId = detalle.ProductoId,
                    Cantidad = detalle.Cantidad,
                    PrecioTotal = detalle.SubTotal
                }).ToList();

                // Guardar la orden y sus detalles
                await _Orden.CreateOrdenAsync(nuevaOrden, detallesOrden);

                // Finalizar la canasta
                canasta.Estado = "Finalizada";
                canasta.Total = 0;
                await _Orden.ActualizarCanastaAsync(canasta);

                return Json(new { success = true, message = "Pedido generado correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al generar el pedido: {ex.Message}" });
            }
        }


    }
}
