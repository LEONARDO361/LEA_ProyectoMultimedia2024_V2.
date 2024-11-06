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
        public async Task<IActionResult> PVDetails(int? id)
        {
            

            var reseñaProducto = await _reseñaproducto.GetReseñaByIdAsync(id.Value);

           

            return PartialView(reseñaProducto);
        }

        // GET: ReseñaProducto/Create
        public async Task<IActionResult> PVCreate()
        {
            var clientes = await _reseñaproducto.GetAllClientesAsync(); 
            var productos = await _reseñaproducto.GetAllProductosAsync();
            ViewData["ClienteId"] = new SelectList(clientes, "ClienteId", "ClienteId");
            ViewData["ProductoId"] = new SelectList(productos, "ProductoId", "ProductoId");
            return PartialView();
        }

        // POST: ReseñaProducto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVCreate([Bind("ReseñaId,ProductoId,ClienteId,Calificación,Comentario,FechaReseña")] ReseñaProductoDTO reseñaProducto)
        {

                var DTO = reseñaProducto.toOriginal();
                await _reseñaproducto.CreateReseñaAsync(DTO);
                return RedirectToAction("Index","Mantenedores");
        }

        // GET: ReseñaProducto/Edit/5
        public async Task<IActionResult> PVEdit(int? id)
        {
           

            var reseñaProducto = await _reseñaproducto.GetReseñaByIdAsync(id.Value);
 
            
            var reseñaDto = reseñaProducto.ToDto();
            ViewData["ClienteId"] = new SelectList(await _reseñaproducto.GetAllClientesAsync(), "ClienteId", "ClienteId", reseñaDto.ClienteId);
            ViewData["ProductoId"] = new SelectList(await _reseñaproducto.GetAllProductosAsync(), "ProductoId", "ProductoId", reseñaDto.ProductoId);
            return PartialView(reseñaDto);
        }

        // POST: ReseñaProducto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVEdit(int id, [Bind("ReseñaId,ProductoId,ClienteId,Calificación,Comentario,FechaReseña")] ReseñaProductoDTO reseñaProducto)
        {



            
                
                var reseñaOriginal = reseñaProducto.toOriginal();
                await _reseñaproducto.UpdateReseñaAsync(reseñaOriginal);              
                return RedirectToAction("Index","Mantenedores");
            

        }

        // GET: ReseñaProducto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {


            var reseñaProducto = await _reseñaproducto.GetReseñaByIdAsync(id.Value);

            return PartialView(reseñaProducto);
        }

        // POST: ReseñaProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reseñaProducto = await _reseñaproducto.GetReseñaByIdAsync(id);

            return RedirectToAction("Index","Mantenedores");
        }
    }
}
