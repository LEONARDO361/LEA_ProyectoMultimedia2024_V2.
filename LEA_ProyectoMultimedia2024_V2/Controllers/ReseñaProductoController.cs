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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reseñaProducto = await _reseñaproducto.GetReseñaByIdAsync(id.Value);

            if (reseñaProducto == null)
            {
                return NotFound();
            }

            return View(reseñaProducto);
        }

        // GET: ReseñaProducto/Create
        public async Task<IActionResult> Create()
        {
            var clientes = await _reseñaproducto.GetAllClientesAsync(); 
            var productos = await _reseñaproducto.GetAllProductosAsync();
            ViewData["ClienteId"] = new SelectList(clientes, "ClienteId", "ClienteId");
            ViewData["ProductoId"] = new SelectList(productos, "ProductoId", "ProductoId");
            return View();
        }

        // POST: ReseñaProducto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReseñaId,ProductoId,ClienteId,Calificación,Comentario,FechaReseña")] ReseñaProductoDTO reseñaProducto)
        {
            if (ModelState.IsValid)
            {
                var DTO = reseñaProducto.toOriginal();
                await _reseñaproducto.CreateReseñaAsync(DTO);
                return RedirectToAction(nameof(Index));
            }
            var clientes = await _reseñaproducto.GetAllClientesAsync(); 
            var productos = await _reseñaproducto.GetAllProductosAsync(); 
            ViewData["ClienteId"] = new SelectList(clientes, "ClienteId", "ClienteId", reseñaProducto.ClienteId);
            ViewData["ProductoId"] = new SelectList(productos, "ProductoId", "ProductoId", reseñaProducto.ProductoId);
            return View(reseñaProducto);
        }

        // GET: ReseñaProducto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reseñaProducto = await _reseñaproducto.GetReseñaByIdAsync(id.Value);
            if (reseñaProducto == null)
            {
                return NotFound();
            }
            
            var reseñaDto = reseñaProducto.ToDto();
            ViewData["ClienteId"] = new SelectList(await _reseñaproducto.GetAllClientesAsync(), "ClienteId", "ClienteId", reseñaDto.ClienteId);
            ViewData["ProductoId"] = new SelectList(await _reseñaproducto.GetAllProductosAsync(), "ProductoId", "ProductoId", reseñaDto.ProductoId);
            return View(reseñaDto);
        }

        // POST: ReseñaProducto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReseñaId,ProductoId,ClienteId,Calificación,Comentario,FechaReseña")] ReseñaProductoDTO reseñaProducto)
        {
            if (id != reseñaProducto.ReseñaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var reseñaOriginal = reseñaProducto.toOriginal();
                    await _reseñaproducto.UpdateReseñaAsync(reseñaOriginal);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _reseñaproducto.ReseñaExistsAsync(reseñaProducto.ReseñaId))
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
            var clientes = await _reseñaproducto.GetAllClientesAsync(); 
            var productos = await _reseñaproducto.GetAllProductosAsync(); 
            ViewData["ClienteId"] = new SelectList(clientes, "ClienteId", "ClienteId", reseñaProducto.ClienteId);
            ViewData["ProductoId"] = new SelectList(productos, "ProductoId", "ProductoId", reseñaProducto.ProductoId);
            return View(reseñaProducto);
        }

        // GET: ReseñaProducto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reseñaProducto = await _reseñaproducto.GetReseñaByIdAsync(id.Value);
            if (reseñaProducto == null)
            {
                return NotFound();
            }

            return View(reseñaProducto);
        }

        // POST: ReseñaProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reseñaProducto = await _reseñaproducto.GetReseñaByIdAsync(id);
            if (reseñaProducto != null)
            {
                await _reseñaproducto.DeleteReseñaAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
