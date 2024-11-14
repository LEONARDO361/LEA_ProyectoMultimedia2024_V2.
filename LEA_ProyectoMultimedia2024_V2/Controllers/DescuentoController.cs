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
    public class DescuentoController : Controller
    {

        private readonly IDescuento _Descuento;

        public DescuentoController(GimnasioContext context, IDescuento descuento)
        {

            _Descuento = descuento;
        }






        // GET: Descuentoes
        public async Task<IActionResult> Index()
        {
            var cl = await _Descuento.GetDescuentoAsync();
            return View(cl);
        }
        public async Task<IActionResult> ListaDescuento()
        {
            var cl = await _Descuento.GetDescuentoAsync();
            return PartialView(cl);
        }

        // GET: Descuentoes/Details/5
        public async Task<IActionResult> PVDetails(int? id)
        {
            var descuento = await _Descuento.GetDetalleDescuento(id.Value);
            return  PartialView(descuento);
        }

        // GET: Descuentoes/Create
        public IActionResult PVCreate()
        {
            return PartialView();
        }

        // POST: Descuentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVCreate([Bind("DescuentoId,PorcentajeDescuento,FechaInicio,FechaFin,TipoDescuento")] DescuentoDTO descuento)
        {
            if (ModelState.IsValid)
            {
                var DTO = descuento.toOriginal();
                await _Descuento.CreateDescuentoAsync(DTO);

                TempData["SuccessMessage"] = "Descuento creado exitosamente";
                return RedirectToAction("Index", "Mantenedores");
            }
            else
            {
                ModelState.AddModelError("", "Hay errores en el formulario. Por favor, revísalos.");
                return View(descuento);
            }
        }


        // GET: Descuentoes/Edit/5
        public async Task<IActionResult> PVEdit(int? id)
        {
          

            var descuento = await _Descuento.GetDescuentoByIdAsync(id.Value);
           var descuentoDto = descuento.toDto();
            return PartialView(descuento);
        }

        // POST: Descuentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVEdit(int id, [Bind("DescuentoId,PorcentajeDescuento,FechaInicio,FechaFin,TipoDescuento")] DescuentoDTO descuento)
        {
            if (ModelState.IsValid)
            {
                var DescuentoOriginal = descuento.toOriginal();
                await _Descuento.UpdateDescuentoAsync(DescuentoOriginal);

                // Manejo de solicitud AJAX
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = "Descuento actualizado exitosamente" });
                }
                else
                {
                    TempData["SuccessMessage"] = "Descuento actualizado exitosamente";
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
                    return View(descuento);
                }
            }
        }

        // POST: Descuentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _Descuento.DeleteDescuentoAsync(id);
            return RedirectToAction("Index","Mantenedores");
        }

    }
}