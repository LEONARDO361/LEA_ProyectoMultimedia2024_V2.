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
            return View (cl);
        }
        public async Task<IActionResult> ListaDescuento ()
        {
            var cl = await _Descuento.GetDescuentoAsync();
            return PartialView(cl);
        }

        // GET: Descuentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        


            var descuento = await _Descuento.GetDetalleDescuento(id.Value);
            if (descuento == null)
            {
                return NotFound();
            }

            return View(descuento);
        }

        // GET: Descuentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Descuentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DescuentoId,PorcentajeDescuento,FechaInicio,FechaFin,TipoDescuento")] DescuentoDTO descuento)
        {
            if (ModelState.IsValid)
            {
                var DTO = descuento.toOriginal();
                await _Descuento.CreateDescuentoAsync(DTO);
                return RedirectToAction(nameof(Index));
            }
            return View(descuento);
        }

        // GET: Descuentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descuento = await _Descuento.GetDescuentoByIdAsync(id.Value);
            if (descuento == null)
            {
                return NotFound();
            }
            return View(descuento);
        }

        // POST: Descuentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DescuentoId,PorcentajeDescuento,FechaInicio,FechaFin,TipoDescuento")] Descuento descuento)
        {
            if (id != descuento.DescuentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _Descuento.UpdateDescuentoAsync(descuento);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _Descuento.DescuentoExist(descuento.DescuentoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(descuento);
        }

        // GET: Descuentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descuento = await _Descuento.GetDetails(id.Value);

            if (descuento == null)
            {
                return NotFound();
            }

            return View(descuento);
        }

        // POST: Descuentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _Descuento.DeleteDescuentoAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private Task <bool> DescuentoExists(int id)
        {
            return _Descuento.DescuentoExist(id);
        }
    }
}
