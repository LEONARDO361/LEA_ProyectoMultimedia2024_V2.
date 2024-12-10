using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LEA_ProyectoMultimedia2024_V2_.Controllers
{
    public class Mantenedores : Controller
    {
        // GET: Mantenedores
        [Authorize(Policy = "Mantenedor")]
        public ActionResult Index()
        {
            return View();
        }
        // GET: Mantenedores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mantenedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mantenedores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Mantenedores/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Mantenedores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Mantenedores/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mantenedores/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
