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
    public class MetodoPagoesController : Controller
    {
        private readonly GimnasioContext _context;

        public MetodoPagoesController(GimnasioContext context)
        {
            _context = context;
        }

        // GET: MetodoPagoes
        public async Task<IActionResult> Index()
        {
            var gimnasioContext = _context.MetodoPago.Include(m => m.Cliente);
            return View(await gimnasioContext.ToListAsync());
        }

        // GET: MetodoPagoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodoPago = await _context.MetodoPago
                .Include(m => m.Cliente)
                .FirstOrDefaultAsync(m => m.MetodoPagoId == id);
            if (metodoPago == null)
            {
                return NotFound();
            }

            return View(metodoPago);
        }

        // GET: MetodoPagoes/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId");
            return View();
        }

        // POST: MetodoPagoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MetodoPagoId,ClienteId,TipoTarjeta,NumeroTarjeta,FechaExpiracion,Cvv")] MetodoPago metodoPago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metodoPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", metodoPago.ClienteId);
            return View(metodoPago);
        }

        // GET: MetodoPagoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodoPago = await _context.MetodoPago.FindAsync(id);
            if (metodoPago == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", metodoPago.ClienteId);
            return View(metodoPago);
        }

        // POST: MetodoPagoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MetodoPagoId,ClienteId,TipoTarjeta,NumeroTarjeta,FechaExpiracion,Cvv")] MetodoPago metodoPago)
        {
            if (id != metodoPago.MetodoPagoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metodoPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetodoPagoExists(metodoPago.MetodoPagoId))
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", metodoPago.ClienteId);
            return View(metodoPago);
        }

        // GET: MetodoPagoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodoPago = await _context.MetodoPago
                .Include(m => m.Cliente)
                .FirstOrDefaultAsync(m => m.MetodoPagoId == id);
            if (metodoPago == null)
            {
                return NotFound();
            }

            return View(metodoPago);
        }

        // POST: MetodoPagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metodoPago = await _context.MetodoPago.FindAsync(id);
            if (metodoPago != null)
            {
                _context.MetodoPago.Remove(metodoPago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetodoPagoExists(int id)
        {
            return _context.MetodoPago.Any(e => e.MetodoPagoId == id);
        }
    }
}
