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
        private readonly GimnasioContext _context;
        private readonly IReseñaProducto _reseñaproducto;

        public ReseñaProductoController(GimnasioContext context, IReseñaProducto reseñaproducto)
        {
            _context = context;
            _reseñaproducto = reseñaproducto;
        }



        // GET: ReseñaProducto
        public async Task<IActionResult> Index()
        {
            var gimnasioContext = _context.ReseñaProducto.Include(r => r.Cliente).Include(r => r.Producto);
            return View(await gimnasioContext.ToListAsync());
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
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId");
            ViewData["ProductoId"] = new SelectList(_context.Producto, "ProductoId", "ProductoId");
            return View();
        }

        // POST: ReseñaProducto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", reseñaProducto.ClienteId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "ProductoId", "ProductoId", reseñaProducto.ProductoId);
            return View(reseñaProducto);
        }

        // GET: ReseñaProducto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reseñaProducto = await _context.ReseñaProducto.FindAsync(id);
            if (reseñaProducto == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", reseñaProducto.ClienteId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "ProductoId", "ProductoId", reseñaProducto.ProductoId);
            return View(reseñaProducto);
        }

        // POST: ReseñaProducto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReseñaId,ProductoId,ClienteId,Calificación,Comentario,FechaReseña")] ReseñaProducto reseñaProducto)
        {
            if (id != reseñaProducto.ReseñaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _reseñaproducto.UpdateReseñaAsync(reseñaProducto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReseñaProductoExists(reseñaProducto.ReseñaId))
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", reseñaProducto.ClienteId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "ProductoId", "ProductoId", reseñaProducto.ProductoId);
            return View(reseñaProducto);
        }

        // GET: ReseñaProducto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reseñaProducto = await _context.ReseñaProducto
                .Include(r => r.Cliente)
                .Include(r => r.Producto)
                .FirstOrDefaultAsync(m => m.ReseñaId == id);
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
            var reseñaProducto = await _context.ReseñaProducto.FindAsync(id);
            if (reseñaProducto != null)
            {
                _context.ReseñaProducto.Remove(reseñaProducto);
            }

            await _reseñaproducto.DeleteReseñaAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ReseñaProductoExists(int id)
        {
            return _context.ReseñaProducto.Any(e => e.ReseñaId == id);
        }
    }
}
