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
    public class ProductoController : Controller


    {

        private readonly IProducto _producto;


        public ProductoController(GimnasioContext context, IProducto producto)
        {

            _producto = producto;
        }

        // GET: Productoes
        public async Task<IActionResult> Index()
        {
            var gimnasioContext = await _producto.GetProductosAsync();
            return View(gimnasioContext);
        }
        public async Task<IActionResult> listaProducto()
        {
            var gimnasioContext = await _producto.GetProductosAsync();
            return PartialView(gimnasioContext);
        }

        // GET: Productoes/Details/5
        public async Task<IActionResult> PVDetails(int? id)
        {
          var producto = await _producto.GetProductoByIdAsync(id.Value);
           return PartialView(producto);
        }

        // GET: Productoes/Create
        public async Task< IActionResult> PVCreate()
        {
            ViewData["CategoriaId"] = new SelectList(await _producto.GetProductosAsync(), "CategoriaId", "Nombre");
            return PartialView();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVCreate([Bind("ProductoId,Nombre,Descripcion,Precio,Cantidad,Procedencia,Estado,Marca,CategoriaId")] ProductoDTO producto)
        {
          
                var DTO = producto.toOriginal();
                await _producto.CreateProductoAsync(DTO);
                return RedirectToAction("Index","Mantenedores");

        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> PVEdit(int? id)
        {


            var producto = await _producto.BuscadorProduct(id.Value);
            var ProductoDto = producto.toDto();
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVEdit(int id, [Bind("ProductoId,Nombre,Descripcion,Precio,Cantidad,Procedencia,Estado,Marca,CategoriaId")] ProductoDTO producto)
        {
                 var ProductoOriginal = producto.toOriginal();
                 await _producto.UpdateProductoAsync(ProductoOriginal);
                 return RedirectToAction("Index","Mantenedores");
            


        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {


            var producto = await _producto.GetProductoByIdAsync(id.Value);

            return PartialView(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {


            await _producto.DeleteProductoAsync(id);
            return RedirectToAction("Index","Mantenedores");
        }


    }
}
