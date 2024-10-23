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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _producto.GetProductoByIdAsync(id.Value);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productoes/Create
        public async Task< IActionResult> Create()
        {
            ViewData["CategoriaId"] = new SelectList(await _producto.GetProductosAsync(), "CategoriaId", "Nombre");
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoId,Nombre,Descripcion,Precio,Cantidad,Procedencia,Estado,Marca,CategoriaId")] ProductoDTO producto)
        {
            if (ModelState.IsValid)
            {
                var DTO = producto.toOriginal();
                
                await _producto.CreateProductoAsync(DTO);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(await _producto.GetProductosAsync(), "CategoriaId", "CategoriaId", producto.CategoriaId);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _producto.BuscadorProduct(id.Value);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(await _producto.GetProductosAsync(), "CategoriaId", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,Nombre,Descripcion,Precio,Cantidad,Procedencia,Estado,Marca,CategoriaId")] Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _producto.UpdateProductoAsync(producto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _producto.ProductExists(producto.ProductoId))
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
            ViewData["CategoriaId"] = new SelectList( await _producto.GetProductosAsync(), "CategoriaId", "CategoriaId", producto.CategoriaId);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _producto.GetProductoByIdAsync(id.Value);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {


            await _producto.DeleteProductoAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private Task<bool> ProductoExists(int id)
        {
            return _producto.ProductExists(id);
        }
    }
}
