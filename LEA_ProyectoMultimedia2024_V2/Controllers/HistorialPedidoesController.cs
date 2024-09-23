using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;

namespace LEA_ProyectoMultimedia2024_V2_.Controllers
{
    public class HistorialPedidoesController : Controller
    {
        private readonly GimnasioContext _context;

        public HistorialPedidoesController(GimnasioContext context)
        {
            _context = context;
        }

        // GET: HistorialPedidoes
        public async Task<IActionResult> Index()
        {
            var gimnasioContext = _context.HistorialPedido.Include(h => h.Orden);
            return View(await gimnasioContext.ToListAsync());
        }

        // GET: HistorialPedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialPedido = await _context.HistorialPedido
                .Include(h => h.Orden)
                .FirstOrDefaultAsync(m => m.HistorialId == id);
            if (historialPedido == null)
            {
                return NotFound();
            }

            return View(historialPedido);
        }

        // GET: HistorialPedidoes/Create
        public IActionResult Create()
        {
            ViewData["OrdenId"] = new SelectList(_context.Orden, "OrdenId", "OrdenId");
            return View();
        }

        // POST: HistorialPedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistorialId,OrdenId,EstadoAnterior,NuevoEstado,FechaCambio,Notas")] HistorialPedido historialPedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historialPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrdenId"] = new SelectList(_context.Orden, "OrdenId", "OrdenId", historialPedido.OrdenId);
            return View(historialPedido);
        }

        // GET: HistorialPedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialPedido = await _context.HistorialPedido.FindAsync(id);
            if (historialPedido == null)
            {
                return NotFound();
            }
            ViewData["OrdenId"] = new SelectList(_context.Orden, "OrdenId", "OrdenId", historialPedido.OrdenId);
            return View(historialPedido);
        }

        // POST: HistorialPedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistorialId,OrdenId,EstadoAnterior,NuevoEstado,FechaCambio,Notas")] HistorialPedido historialPedido)
        {
            if (id != historialPedido.HistorialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historialPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialPedidoExists(historialPedido.HistorialId))
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
            ViewData["OrdenId"] = new SelectList(_context.Orden, "OrdenId", "OrdenId", historialPedido.OrdenId);
            return View(historialPedido);
        }

        // GET: HistorialPedidoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialPedido = await _context.HistorialPedido
                .Include(h => h.Orden)
                .FirstOrDefaultAsync(m => m.HistorialId == id);
            if (historialPedido == null)
            {
                return NotFound();
            }

            return View(historialPedido);
        }

        // POST: HistorialPedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historialPedido = await _context.HistorialPedido.FindAsync(id);
            if (historialPedido != null)
            {
                _context.HistorialPedido.Remove(historialPedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialPedidoExists(int id)
        {
            return _context.HistorialPedido.Any(e => e.HistorialId == id);
        }
    }
}
