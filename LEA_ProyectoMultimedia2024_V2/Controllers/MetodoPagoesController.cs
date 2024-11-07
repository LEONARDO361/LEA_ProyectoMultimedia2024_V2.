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
        public async Task<IActionResult> ListaMetodoDePago()
        {
            var metodosPago = await _metodoDePago.GetAllMetodosPagoAsync();
            return PartialView(metodosPago);
        }

        // GET: MetodoPagoes/Details/5
        public async Task<IActionResult> PVDetails(int? id)
        {
          

            var metodoPago = await _metodoDePago.GetMetodoPagoByIdAsync(id.Value);
            return PartialView(metodoPago);
        }

        // GET: MetodoPagoes/Create
        public async Task<IActionResult> PVCreateAsync()
        {
            var clientes = await _cliente.GetAllClientesAsync();    
            ViewData["ClienteId"] = new SelectList(clientes, "ClienteId", "Nombre");
            return PartialView();
        }

        // POST: MetodoPagoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVCreate([Bind("MetodoPagoId,ClienteId,TipoTarjeta,NumeroTarjeta,FechaExpiracion,Cvv")] MetodoPagoDTO metodoPago)
        {
          
                    var DTO = metodoPago.toOriginal(); // dto
                    await _metodoDePago.CreateMetodoPagoAsync(DTO);
                    return RedirectToAction("Index","Mantenedores");
        }

        // GET: MetodoPagoes/Edit/5
        public async Task<IActionResult> PVEdit(int? id)
        {
         

            var metodoPago = await _metodoDePago.GetMetodoPagoByIdAsync(id.Value);
            var metodoPagoDTO = metodoPago.toDto();
            return PartialView(metodoPagoDTO);
        }

        // POST: MetodoPagoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVEdit(int id, [Bind("MetodoPagoId,ClienteId,TipoTarjeta,NumeroTarjeta,FechaExpiracion,Cvv")] MetodoPagoDTO metodoPago)
        {
            var MetodoPagoOriginal = metodoPago.toOriginal();
                    await _metodoDePago.UpdateMetodoPagoAsync(MetodoPagoOriginal);
               
                return RedirectToAction("Index", "Mantenedores");
           
        }

        // GET: MetodoPagoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
           

            var metodoPago = await _metodoDePago.GetMetodoPagoByIdAsync(id.Value);

            return PartialView(metodoPago);
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
