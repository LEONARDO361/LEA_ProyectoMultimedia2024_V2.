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
    public class DireccionEnviosController : Controller
    {
        private readonly GimnasioContext _context;

        public DireccionEnviosController(GimnasioContext context)
        {
            _context = context;
        }

        // GET: DireccionEnvios
        public async Task<IActionResult> Index()
        {
            var gimnasioContext = _context.DireccionEnvio.Include(d => d.Cliente);
            return View(await gimnasioContext.ToListAsync());
        }

        // GET: DireccionEnvios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccionEnvio = await _context.DireccionEnvio
                .Include(d => d.Cliente)
                .FirstOrDefaultAsync(m => m.DireccionId == id);
            if (direccionEnvio == null)
            {
                return NotFound();
            }

            return View(direccionEnvio);
        }

        // GET: DireccionEnvios/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId");
            return View();
        }

        // POST: DireccionEnvios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DireccionId,ClienteId,Dirección,Ciudad,Provincia,CodigoPostal,Pais")] DireccionEnvio direccionEnvio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(direccionEnvio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", direccionEnvio.ClienteId);
            return View(direccionEnvio);
        }

        // GET: DireccionEnvios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccionEnvio = await _context.DireccionEnvio.FindAsync(id);
            if (direccionEnvio == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", direccionEnvio.ClienteId);
            return View(direccionEnvio);
        }

        // POST: DireccionEnvios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DireccionId,ClienteId,Dirección,Ciudad,Provincia,CodigoPostal,Pais")] DireccionEnvio direccionEnvio)
        {
            if (id != direccionEnvio.DireccionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(direccionEnvio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DireccionEnvioExists(direccionEnvio.DireccionId))
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", direccionEnvio.ClienteId);
            return View(direccionEnvio);
        }

        // GET: DireccionEnvios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccionEnvio = await _context.DireccionEnvio
                .Include(d => d.Cliente)
                .FirstOrDefaultAsync(m => m.DireccionId == id);
            if (direccionEnvio == null)
            {
                return NotFound();
            }

            return View(direccionEnvio);
        }

        // POST: DireccionEnvios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var direccionEnvio = await _context.DireccionEnvio.FindAsync(id);
            if (direccionEnvio != null)
            {
                _context.DireccionEnvio.Remove(direccionEnvio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DireccionEnvioExists(int id)
        {
            return _context.DireccionEnvio.Any(e => e.DireccionId == id);
        }
    }
}
