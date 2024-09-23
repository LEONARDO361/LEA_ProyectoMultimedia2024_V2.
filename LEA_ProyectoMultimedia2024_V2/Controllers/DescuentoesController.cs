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
    public class DescuentoesController : Controller
    {
        private readonly GimnasioContext _context;

        public DescuentoesController(GimnasioContext context)
        {
            _context = context;
        }

        // GET: Descuentoes
        public async Task<IActionResult> Index()
        {
            var gimnasioContext = _context.Descuento.Include(d => d.Producto);
            return View(await gimnasioContext.ToListAsync());
        }

        // GET: Descuentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descuento = await _context.Descuento
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.DescuentoId == id);
            if (descuento == null)
            {
                return NotFound();
            }

            return View(descuento);
        }

        // GET: Descuentoes/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Producto, "ProductoId", "ProductoId");
            return View();
        }

        // POST: Descuentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DescuentoId,ProductoId,PorcentajeDescuento,FechaInicio,FechaFin,TipoDescuento")] Descuento descuento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(descuento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "ProductoId", "ProductoId", descuento.ProductoId);
            return View(descuento);
        }

        // GET: Descuentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descuento = await _context.Descuento.FindAsync(id);
            if (descuento == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "ProductoId", "ProductoId", descuento.ProductoId);
            return View(descuento);
        }

        // POST: Descuentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DescuentoId,ProductoId,PorcentajeDescuento,FechaInicio,FechaFin,TipoDescuento")] Descuento descuento)
        {
            if (id != descuento.DescuentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(descuento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescuentoExists(descuento.DescuentoId))
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
            ViewData["ProductoId"] = new SelectList(_context.Producto, "ProductoId", "ProductoId", descuento.ProductoId);
            return View(descuento);
        }

        // GET: Descuentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descuento = await _context.Descuento
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.DescuentoId == id);
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
            var descuento = await _context.Descuento.FindAsync(id);
            if (descuento != null)
            {
                _context.Descuento.Remove(descuento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescuentoExists(int id)
        {
            return _context.Descuento.Any(e => e.DescuentoId == id);
        }
    }
}
