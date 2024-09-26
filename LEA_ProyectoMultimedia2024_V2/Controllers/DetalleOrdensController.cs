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

namespace LEA_ProyectoMultimedia2024_V2_.Controllers
{
    public class DetalleOrdensController : Controller
    {
        private readonly GimnasioContext _context;
        private readonly IDetalleOrden _DetalleOrden;

        public DetalleOrdensController(GimnasioContext context, IDetalleOrden detalleOrden)
        {
            _context = context;
            _DetalleOrden = detalleOrden;
        }


        // GET: DetalleOrdens
        public async Task<IActionResult> Index()
        {
            var gimnasioContext = _context.DetalleOrden.Include(d => d.Orden).Include(d => d.Producto);
            return View(await gimnasioContext.ToListAsync());
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
        public IActionResult Create()
        {
            ViewData["OrdenId"] = new SelectList(_context.Orden, "OrdenId", "OrdenId");
            ViewData["ProductoId"] = new SelectList(_context.Producto, "ProductoId", "ProductoId");
            return View();
        }

        // POST: DetalleOrdens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetalleId,OrdenId,ProductoId,Cantidad,PrecioTotal")] DetalleOrden detalleOrden)
        {
            if (ModelState.IsValid)
            {
    
                await _DetalleOrden.CreateDetalleOrdenAsync(detalleOrden);
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrdenId"] = new SelectList(_context.Orden, "OrdenId", "OrdenId", detalleOrden.OrdenId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "ProductoId", "ProductoId", detalleOrden.ProductoId);
            return View(detalleOrden);
        }

        // GET: DetalleOrdens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleOrden = await _context.DetalleOrden.FindAsync(id);
            if (detalleOrden == null)
            {
                return NotFound();
            }
            ViewData["OrdenId"] = new SelectList(_context.Orden, "OrdenId", "OrdenId", detalleOrden.OrdenId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "ProductoId", "ProductoId", detalleOrden.ProductoId);
            return View(detalleOrden);
        }

        // POST: DetalleOrdens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

                    _context.Update(detalleOrden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleOrdenExists(detalleOrden.DetalleId))
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
            ViewData["OrdenId"] = new SelectList(_context.Orden, "OrdenId", "OrdenId", detalleOrden.OrdenId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "ProductoId", "ProductoId", detalleOrden.ProductoId);
            return View(detalleOrden);
        }

        // GET: DetalleOrdens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleOrden = await _context.DetalleOrden
                .Include(d => d.Orden)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.DetalleId == id);
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

        private bool DetalleOrdenExists(int id)
        {
            return _context.DetalleOrden.Any(e => e.DetalleId == id);
        }
    }
}
