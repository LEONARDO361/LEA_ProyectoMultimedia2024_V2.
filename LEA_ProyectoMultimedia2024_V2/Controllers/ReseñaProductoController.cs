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

namespace LEA_ProyectoMultimedia2024_V2_.Controllers
{
    public class ReseñaProductoController : Controller
    {
        private readonly IReseñaProducto _reseñaproducto;

        public ReseñaProductoController(IReseñaProducto reseñaproducto)
        {
            _reseñaproducto = reseñaproducto;
        }

        // GET: ReseñaProducto
        public async Task<IActionResult> Index()
        {
            var reseñas = await _reseñaproducto.GetAllReseñasAsync();
            return View(reseñas);
        }
        public async Task<IActionResult> ListaReseña()
        {
            var reseñas = await _reseñaproducto.GetAllReseñasAsync();
            return PartialView(reseñas);
        }

        // GET: ReseñaProducto/Details/5
        public async Task<IActionResult> PVDetails(int? id)
        {

            ViewData["ClienteId"] = new SelectList(await _reseñaproducto.GetAllClientesAsync(), "ClienteId", "Nombre");
            ViewData["ProductoId"] = new SelectList(await _reseñaproducto.GetAllProductosAsync(), "ProductoId", "Nombre");
            var reseñaProducto = await _reseñaproducto.GetReseñaByIdAsync(id.Value);



            return PartialView(reseñaProducto);
        }

        // GET: ReseñaProducto/Create
        public async Task<IActionResult> PVCreate()
        {
            var clientes = await _reseñaproducto.GetAllClientesAsync();
            var productos = await _reseñaproducto.GetAllProductosAsync();
            ViewData["ClienteId"] = new SelectList(clientes, "ClienteId", "Nombre");
            ViewData["ProductoId"] = new SelectList(productos, "ProductoId", "Nombre");
            return PartialView();
        }

        // POST: ReseñaProducto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVCreate([Bind("ReseñaId,ProductoId,ClienteId,Calificación,Comentario,FechaReseña")] ReseñaProductoDTO reseñaProducto)
        {
            if (ModelState.IsValid)
            {

                var DTO = reseñaProducto.toOriginal();
                await _reseñaproducto.CreateReseñaAsync(DTO);

                // Responder con JSON solo si la solicitud es AJAX
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = "Reseña creada exitosamente" });
                }
                else
                {
                    TempData["SuccessMessage"] = "Reseña creada exitosamente";
                    return RedirectToAction("Index", "Mantenedores");
                }
            }
            else
            {
                var clientes = await _reseñaproducto.GetAllClientesAsync();
                var productos = await _reseñaproducto.GetAllProductosAsync();
                // Cargar los datos necesarios para la vista en caso de error (ejemplo: lista de productos y clientes)
                ViewData["ClienteId"] = new SelectList(clientes, "ClienteId", "Nombre");
                ViewData["ProductoId"] = new SelectList(productos, "ProductoId", "Nombre");
                var errors = ModelState
                    .Where(a => a.Value.Errors.Any())
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    );

                // Responder con JSON solo si la solicitud es AJAX
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, errors });
                }
                else
                {
                    // Agregar un mensaje de error genérico y devolver la vista con los errores de validación
                    ModelState.AddModelError("", "Hay errores en el formulario. Por favor, revísalos.");
                    return View(reseñaProducto);
                }
            }
        }


        // GET: ReseñaProducto/Edit/5
        public async Task<IActionResult> PVEdit(int? id)
        {


            var reseñaProducto = await _reseñaproducto.GetReseñaByIdAsync(id.Value);


            var reseñaDto = reseñaProducto.ToDto();
            ViewData["ClienteId"] = new SelectList(await _reseñaproducto.GetAllClientesAsync(), "ClienteId", "Nombre", reseñaDto.ClienteId);
            ViewData["ProductoId"] = new SelectList(await _reseñaproducto.GetAllProductosAsync(), "ProductoId", "Nombre", reseñaDto.ProductoId);
            return PartialView(reseñaDto);
        }

        // POST: ReseñaProducto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVEdit(int id, [Bind("ReseñaId,ProductoId,ClienteId,Calificación,Comentario,FechaReseña")] ReseñaProductoDTO reseñaProducto)
        {
            if (ModelState.IsValid)
            {
                var reseñaOriginal = reseñaProducto.toOriginal();
                await _reseñaproducto.UpdateReseñaAsync(reseñaOriginal);

                // Responder con JSON solo si la solicitud es AJAX
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = "Reseña actualizada exitosamente" });
                }
                else
                {
                    TempData["SuccessMessage"] = "Reseña actualizada exitosamente";
                    return RedirectToAction("Index", "Mantenedores");
                }
            }
            else
            {
                ViewData["ClienteId"] = new SelectList(await _reseñaproducto.GetAllClientesAsync(), "ClienteId", "Nombre");
                ViewData["ProductoId"] = new SelectList(await _reseñaproducto.GetAllProductosAsync(), "ProductoId", "Nombre");
                var errors = ModelState
                    .Where(a => a.Value.Errors.Any())
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    );

                // Responder con JSON solo si la solicitud es AJAX
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, errors });
                }
                else
                {
                    // Agregar un mensaje de error genérico y devolver la vista con los errores de validación
                    ModelState.AddModelError("", "Hay errores en el formulario. Por favor, revísalos.");
                    return View(reseñaProducto);
                }
            }
        }


        // GET: ReseñaProducto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            var reseñaProducto = await _reseñaproducto.GetReseñaByIdAsync(id.Value);


            return PartialView(reseñaProducto);
        }

        // POST: ReseñaProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            
                await _reseñaproducto.DeleteReseñaAsync(id); // Método para eliminar la reseña en la base de datos
            
            return RedirectToAction("Index", "Mantenedores");
        }

    }
}