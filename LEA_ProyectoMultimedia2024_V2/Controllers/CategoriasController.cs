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
    public class CategoriasController : Controller

       
    {

        private readonly ICategorias _categorias;

        public CategoriasController(GimnasioContext context, ICategorias categorias)
        {

         
            _categorias = categorias;
        }



        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            var a = await _categorias.GetCategoriasAsync();
            return View(a);
        }
        public async Task<IActionResult> ListaCategoria()
        {
            var a = await _categorias.GetCategoriasAsync();
            return PartialView(a);
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> PVDetails(int? id)
        {

            var categoria = await _categorias.GetDetails(id.Value);
            return PartialView (categoria);
        }

        // GET: Categorias/Create
        public IActionResult PVCreate()
        {
            return PartialView();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVCreate([Bind("CategoriaId,Nombre,Descripcion,Pesokg")] CategoriaDTO categoria)
        {
          

            if (ModelState.IsValid)
            {
                var DTO = categoria.toOriginal();
                await _categorias.AddCategoriaAsync(DTO);
                return Json(new { success = true, message = "Cliente creado exitosamente" });
            }
            else
            {
                var errors = ModelState
                    .Where(a => a.Value.Errors.Any())
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    );

                return Json(new
                {
                    succes = false,
                    errors


                });

            }
        }


        // GET: Categorias/Edit/5
        public async Task<IActionResult> PVEdit(int? id)
        {


            var categoria = await _categorias.GetCategoriaByIdAsync(id.Value);
            var CategoriaDto = categoria.ToDto();
            return PartialView(CategoriaDto);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PVEdit(int id, [Bind("CategoriaId,Nombre,Descripcion,Pesokg")] CategoriaDTO categoria)
        {



            if (ModelState.IsValid)
            {
                var CategoriaOriginal = categoria.toOriginal();
                await _categorias.UpdateCategoriaAsync(CategoriaOriginal);
                return Json(new { success = true, message = "Cliente creado exitosamente" });
            }
            else
            {
                var errors = ModelState
                    .Where(a => a.Value.Errors.Any())
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    );

                return Json(new
                {
                    succes = false,
                    errors


                });
            }
        }
        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {


            var categoria = await _categorias.GetDetails(id.Value);

            return PartialView(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmede(int id)
        {
           await _categorias.DeleteCategoriaAsync (id);
            return RedirectToAction("Index","Mantenedores");
        }

    }
}
