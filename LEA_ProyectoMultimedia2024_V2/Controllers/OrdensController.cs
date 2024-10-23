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
    public class OrdensController : Controller
    {

        private readonly IOrden _Orden;

        public OrdensController(GimnasioContext context, IOrden orden)
        {

            _Orden = orden;
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _Orden.GetOrdenByIdAsync(id.Value);

            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // GET: Ordens/Create
        public async Task <IActionResult> Create()
        {
            ViewData["ClienteId"] = new SelectList(await _Orden.GetAllOrdensAsync(), "ClienteId", "ClienteId");
            return View();
        }

        // POST: Ordens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrdenId,FechaOrden,Total,Estado,ClienteId")] OrdenDTO orden)
        {
            if (ModelState.IsValid)
            {
                var DTO = orden.toOriginal();
                await _Orden.CreateOrdenAsync(DTO);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(await _Orden.GetAllOrdensAsync(), "ClienteId", "ClienteId", orden.ClienteId);
            return View(orden);
        }

        // GET: Ordens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _Orden.BuscOrden(id.Value);
            if (orden == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(await _Orden.GetAllOrdensAsync(), "ClienteId", "ClienteId", orden.ClienteId);
            return View(orden);
        }

        // POST: Ordens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrdenId,FechaOrden,Total,Estado,ClienteId")] Orden orden)
        {
            if (id != orden.OrdenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _Orden.UpdateOrdenAsync(orden);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _Orden.OrdenExistsAsync(orden.OrdenId))
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
            ViewData["ClienteId"] = new SelectList(await _Orden.GetAllOrdensAsync(), "ClienteId", "ClienteId", orden.ClienteId);
            return View(orden);
        }

        // GET: Ordens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _Orden.GetOrdenByIdAsync(id.Value);

            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // POST: Ordens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          

            await _Orden.DeleteOrdenAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private Task <bool> OrdenExists(int id)
        {
            return _Orden.OrdenExistsAsync(id);
        }
    }
}
