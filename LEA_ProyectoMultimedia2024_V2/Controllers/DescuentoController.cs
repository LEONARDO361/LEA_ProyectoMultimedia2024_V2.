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
    public class DescuentoController : Controller
    {

        private readonly IDescuento _Descuento;

        public DescuentoController(GimnasioContext context, IDescuento descuento)
        {

            _Descuento = descuento;
        }






        // GET: Descuentoes
        public async Task<IActionResult> Index()
        {
            var cl = await _Descuento.GetDescuentoAsync();
            return View(cl);
        }
        public async Task<IActionResult> ListaDescuento()
        {
            var cl = await _Descuento.GetDescuentoAsync();
            return PartialView(cl);
        }

        // GET: Descuentoes/Details/5
        public async Task<IActionResult> PVDetails(int? id)
        {
            var descuento = await _Descuento.GetDetalleDescuento(id.Value);
            return  PartialView(descuento);
        }

        // GET: Descuentoes/Create
        public IActionResult PVCreate()
        {
            return PartialView();
        }

        // POST: Descuentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVCreate([Bind("DescuentoId,PorcentajeDescuento,FechaInicio,FechaFin,TipoDescuento")] DescuentoDTO descuento)
        {
           
                var DTO = descuento.toOriginal();
                await _Descuento.CreateDescuentoAsync(DTO);
                return RedirectToAction("Index","Mantenedores");

        }

        // GET: Descuentoes/Edit/5
        public async Task<IActionResult> PVEdit(int? id)
        {
          

            var descuento = await _Descuento.GetDescuentoByIdAsync(id.Value);
           var descuentoDto = descuento.toDto();
            return View(descuento);
        }

        // POST: Descuentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVEdit(int id, [Bind("DescuentoId,PorcentajeDescuento,FechaInicio,FechaFin,TipoDescuento")] DescuentoDTO descuento)
        {

            var DescuentoOriginal = descuento.toOriginal();
            await _Descuento.UpdateDescuentoAsync(DescuentoOriginal);
            return RedirectToAction("Index", "Mantenedores");
        }

        // GET: Descuentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            var descuento = await _Descuento.GetDetails(id.Value);

        
            return PartialView(descuento);
        }

        // POST: Descuentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _Descuento.DeleteDescuentoAsync(id);
            return RedirectToAction("Index","Mantenedores");
        }

    }
}