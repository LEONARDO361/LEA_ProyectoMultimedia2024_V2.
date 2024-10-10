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
    public class MetodoPagoesController : Controller
    {
        private readonly IMetodoDePago _metodoDePago;
        private readonly ICliente _cliente;

        public MetodoPagoesController(IMetodoDePago metodoDePago, ICliente cliente)
        {
            _metodoDePago = metodoDePago;
            _cliente = cliente;
        }

        // GET: MetodoPagoes
        public async Task<IActionResult> Index()
        {
            var metodosPago = await _metodoDePago.GetAllMetodosPagoAsync();
            return View(metodosPago);
        }

        // GET: MetodoPagoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodoPago = await _metodoDePago.GetMetodoPagoByIdAsync(id.Value);

            if (metodoPago == null)
            {
                return NotFound();
            }

            return View(metodoPago);
        }

        // GET: MetodoPagoes/Create
        public async Task<IActionResult> Create()
        {
            var clientes = await _cliente.GetAllClientesAsync();
            ViewData["ClienteId"] = new SelectList(clientes, "ClienteId", "ClienteId");
            return View();
        }

        // POST: MetodoPagoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MetodoPagoId,ClienteId,TipoTarjeta,NumeroTarjeta,FechaExpiracion,Cvv")] MetodoPagoDTO metodoPago)
        {
            if (ModelState.IsValid)
            {
                    var DTO = metodoPago.toOriginal(); // Asegúrate de que este método convierta correctamente el DTO
                    await _metodoDePago.CreateMetodoPagoAsync(DTO);
                    return RedirectToAction(nameof(Index));
            }

            
            var clientes = await _cliente.GetAllClientesAsync();
            ViewData["ClienteId"] = new SelectList(clientes, "ClienteId", "ClienteId", metodoPago.ClienteId);
            return View(metodoPago);
        }

        // GET: MetodoPagoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodoPago = await _metodoDePago.GetMetodoPagoByIdAsync(id.Value);
            if (metodoPago == null)
            {
                return NotFound();
            }

            var clientes = await _cliente.GetAllClientesAsync();
            ViewData["ClienteId"] = new SelectList(clientes, "ClienteId", "ClienteId", metodoPago.ClienteId);
            return View(metodoPago);
        }

        // POST: MetodoPagoes/Edit/5
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
                    await _metodoDePago.UpdateMetodoPagoAsync(metodoPago);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _metodoDePago.MetodoPagoExistsAsync(metodoPago.MetodoPagoId))
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

            var clientes = await _cliente.GetAllClientesAsync();
            ViewData["ClienteId"] = new SelectList(clientes, "ClienteId", "ClienteId", metodoPago.ClienteId);
            return View(metodoPago);
        }

        // GET: MetodoPagoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodoPago = await _metodoDePago.GetMetodoPagoByIdAsync(id.Value);
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
            await _metodoDePago.DeleteMetodoPagoAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
