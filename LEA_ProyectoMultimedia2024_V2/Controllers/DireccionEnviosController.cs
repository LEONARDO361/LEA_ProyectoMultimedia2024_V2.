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
    public class DireccionEnviosController : Controller
    {
        private readonly IDireccionEnvios _direccionEnvios;

        public DireccionEnviosController(IDireccionEnvios direccionEnvios)
        {
            _direccionEnvios = direccionEnvios;
        }

        // GET: DireccionEnvios
        public async Task<IActionResult> Index()
        {
            var direcciones = await _direccionEnvios.GetAllDireccionesAsync();
            return View(direcciones);
        }

        // GET: DireccionEnvios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccionEnvio = await _direccionEnvios.GetDireccionByIdAsync(id.Value);

            if (direccionEnvio == null)
            {
                return NotFound();
            }

            return View(direccionEnvio);
        }

        // GET: DireccionEnvios/Create
        public async Task <IActionResult> Create()
        {
            ViewData["ClienteId"] = new SelectList(await _direccionEnvios.GetClientesAsync(), "ClienteId", "ClienteId");
            return View();
        }

        // POST: DireccionEnvios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DireccionId,ClienteId,Dirección,Ciudad,Provincia,CodigoPostal,Pais")] DireccionEnviosDTO direccionEnvio)
        {
            if (ModelState.IsValid)
            {
                var DTO = direccionEnvio.toOriginal();
                await _direccionEnvios.CreateDireccionAsync(DTO);
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClienteId"] = new SelectList(await _direccionEnvios.GetClientesAsync(), "ClienteId", "ClienteId", direccionEnvio.ClienteId);
            return View(direccionEnvio);
        }



        // GET: DireccionEnvios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccionEnvio = await _direccionEnvios.GetDireccionByIdAsync(id.Value);
            if (direccionEnvio == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(await _direccionEnvios.GetClientesAsync(), "ClienteId", "ClienteId", direccionEnvio.ClienteId);
            return View(direccionEnvio);
        }

        // POST: DireccionEnvios/Edit/5
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
                    await _direccionEnvios.UpdateDireccionAsync(direccionEnvio);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _direccionEnvios.DireccionEnvioExistsAsync(direccionEnvio.DireccionId))
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
            ViewData["ClienteId"] = new SelectList( await _direccionEnvios.GetClientesAsync(), "ClienteId", "ClienteId", direccionEnvio.ClienteId);
            return View(direccionEnvio);
        }

        // GET: DireccionEnvios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccionEnvio = await _direccionEnvios.GetDireccionByIdAsync(id.Value);
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
            await _direccionEnvios.DeleteDireccionAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
