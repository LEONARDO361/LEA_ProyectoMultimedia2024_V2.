using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using LEA_ProyectoMultimedia2024_V2_.Models.DTOs;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;

namespace LEA_ProyectoMultimedia2024_V2_.Controllers
{
    public class DetalleOrdensController : Controller
    {
        private readonly IDetalleOrden _DetalleOrden;

        public DetalleOrdensController(IDetalleOrden detalleOrden)
        {
            _DetalleOrden = detalleOrden;
        }

        // GET: DetalleOrdens
        public async Task<IActionResult> Index()
        {
            var detalles = await _DetalleOrden.GetDetalleOrdensAsync();
            return View(detalles);
        }

        // GET: DetalleOrdens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleOrden = await _DetalleOrden.GetDetalleOrdenByIdAsync(id.Value);

            if (detalleOrden == null)
            {
                return NotFound();
            }

            return View(detalleOrden);
        }

        // GET: DetalleOrdens/Create
        public async Task<IActionResult> Create()
        {

            ViewData["OrdenId"] = new SelectList(await _DetalleOrden.GetOrdenesAsync(), "OrdenId", "OrdenId");
            ViewData["ProductoId"] = new SelectList(await _DetalleOrden.GetProductosAsync(), "ProductoId", "ProductoId");
            return View();
        }

        // POST: DetalleOrdens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetalleId,OrdenId,ProductoId,Cantidad,PrecioTotal")] DetalleOrdenDTO detalleOrden)
        {
            if (ModelState.IsValid)
            {
                var DTO = detalleOrden.toOriginal();
                await _DetalleOrden.CreateDetalleOrdenAsync(DTO);
                return RedirectToAction(nameof(Index));
            }

            ViewData["OrdenId"] = new SelectList(await _DetalleOrden.GetOrdenesAsync(), "OrdenId", "OrdenId", detalleOrden.OrdenId);
            ViewData["ProductoId"] = new SelectList(await _DetalleOrden.GetProductosAsync(), "ProductoId", "ProductoId", detalleOrden.ProductoId);
            return View(detalleOrden);
        }

        // GET: DetalleOrdens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleOrden = await _DetalleOrden.GetDetalleOrdenByIdAsync(id.Value);
            if (detalleOrden == null)
            {
                return NotFound();
            }

            ViewData["OrdenId"] = new SelectList(await _DetalleOrden.GetOrdenesAsync(), "OrdenId", "OrdenId", detalleOrden.OrdenId);
            ViewData["ProductoId"] = new SelectList(await _DetalleOrden.GetProductosAsync(), "ProductoId", "ProductoId", detalleOrden.ProductoId);
            return View(detalleOrden);
        }

        // POST: DetalleOrdens/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetalleId,OrdenId,ProductoId,Cantidad,PrecioTotal")] DetalleOrden detalleOrden)
        {
            if (id != detalleOrden.DetalleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _DetalleOrden.UpdateDetalleOrdenAsync(detalleOrden);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await DetalleOrdenExists(detalleOrden.DetalleId))
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

            ViewData["OrdenId"] = new SelectList(await _DetalleOrden.GetOrdenesAsync(), "OrdenId", "OrdenId", detalleOrden.OrdenId);
            ViewData["ProductoId"] = new SelectList(await _DetalleOrden.GetProductosAsync(), "ProductoId", "ProductoId", detalleOrden.ProductoId);
            return View(detalleOrden);
        }

        // GET: DetalleOrdens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleOrden = await _DetalleOrden.GetDetalleOrdenByIdAsync(id.Value);
            if (detalleOrden == null)
            {
                return NotFound();
            }

            return View(detalleOrden);
        }

        // POST: DetalleOrdens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _DetalleOrden.DeleteDetalleOrdenAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DetalleOrdenExists(int id)
        {
            return (await _DetalleOrden.GetDetalleOrdenByIdAsync(id)) != null;
        }
    }
}
