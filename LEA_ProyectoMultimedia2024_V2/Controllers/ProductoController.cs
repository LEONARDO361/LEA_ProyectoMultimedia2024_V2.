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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Policy = "Cliente")]
        public async Task<IActionResult> Index()
        {
            var gimnasioContext = await _producto.GetProductosAsync();
            return View(gimnasioContext);
        }

        // GET: Productoes
        
        public async Task<IActionResult> CatalogoProductos() 
        { 
            var productos = await _producto.GetProductosAsync(); 
            return View("CatalogoProductos", productos); 
        }

        public async Task<IActionResult> listaProducto()
        {
            var gimnasioContext = await _producto.GetProductosAsync();
            return PartialView(gimnasioContext);
        }

        // GET: Productoes/Details/5
        [Authorize(Policy = "Cliente")]
        public async Task<IActionResult> PVDetails(int? id)
        {
            ViewData["CategoriaId"] = new SelectList(await _producto.GetProductosAsync(), "CategoriaId", "Nombre");
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
            if (ModelState.IsValid)
            {
                var DTO = producto.toOriginal();
                await _producto.CreateProductoAsync(DTO);

                // Responder con JSON solo si la solicitud es AJAX
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = "Producto creado exitosamente" });
                }
                else
                {
                    TempData["SuccessMessage"] = "Producto creado exitosamente";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewData["CategoriaId"] = new SelectList(await _producto.GetProductosAsync(), "CategoriaId", "Nombre");
                var errors = ModelState
                    .Where(a => a.Value.Errors.Any())
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    );

                // Responder con JSON solo si la solicitud es AJAX
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, errors });
                }
                else
                {
                    // Agregar un mensaje de error genérico y devolver la vista con los errores de validación
                    ModelState.AddModelError("", "Hay errores en el formulario. Por favor, revísalos.");
                    return View(producto);
                }
            }
        }


        // GET: Productoes/Edit/5
        public async Task<IActionResult> PVEdit(int? id)
        {
            var producto = await _producto.BuscadorProduct(id.Value);
            ViewData["CategoriaId"] = new SelectList(await _producto.GetProductosAsync(), "CategoriaId", "Nombre", producto.CategoriaId);
 
            var ProductoDto = producto.toDto();
            return PartialView(ProductoDto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVEdit(int id, [Bind("ProductoId,Nombre,Descripcion,Precio,Cantidad,Procedencia,Estado,Marca,CategoriaId")] ProductoDTO producto)
        {
            if (ModelState.IsValid)
            {
                var ProductoOriginal = producto.toOriginal();
                await _producto.UpdateProductoAsync(ProductoOriginal);

                // Responder con JSON solo si la solicitud es AJAX
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = "Producto actualizado exitosamente" });
                }
                else
                {
                    TempData["SuccessMessage"] = "Producto actualizado exitosamente";
                    return RedirectToAction("Index", "Mantenedores");
                }
            }
            else
            {
                // Cargar los datos necesarios para la vista en caso de error
                ViewData["CategoriaId"] = new SelectList(await _producto.GetProductosAsync(), "CategoriaId", "Nombre", producto.CategoriaId);

                var errors = ModelState
                    .Where(a => a.Value.Errors.Any())
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    );

                // Responder con JSON solo si la solicitud es AJAX
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, errors });
                }
                else
                {
                    // Agregar un mensaje de error genérico y devolver la vista con los errores de validación
                    ModelState.AddModelError("", "Hay errores en el formulario. Por favor, revísalos.");
                    return View(producto);
                }
            }
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
