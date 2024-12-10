//using LEA_ProyectoMultimedia2024.Models;
using LEA_ProyectoMultimedia2024_V2_.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace LEA_ProyectoMultimedia2024.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController>_logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Mantenedores()
        {
            return View();
        }

        public IActionResult About_Us()
        {
            return View();
        }
        public IActionResult Factura()
        {
            return View();
        }
        public IActionResult Rechazo()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Recividor(int id)
            {
                var claims = new List<Claim> { };

                Claim Name = new Claim(ClaimTypes.Name, "pepito");//permisos[0].Nombre);
                Claim NameIdentifier = new Claim(ClaimTypes.NameIdentifier, "5");
                claims.Add(Name);
                claims.Add(NameIdentifier);

                int tipoUsuario = id;

                switch (tipoUsuario)
                {
                    case 1:
                        {
                            Claim roleClaim = new Claim(ClaimTypes.Role, "Cliente");
                            claims.Add(roleClaim);
                            break;
                        }
                    case 2:
                        {
                            Claim roleClaim = new Claim(ClaimTypes.Role, "Vendedor");
                            claims.Add(roleClaim);
                            break;
                        }
                    case 3:
                        {
                            Claim roleClaim = new Claim(ClaimTypes.Role, "Mantenedor");
                            claims.Add(roleClaim);
                            break;
                        }
                    case 4:
                        {
                            Claim roleClaim = new Claim(ClaimTypes.Role, "Admin");
                            claims.Add(roleClaim);
                            break;
                        }
                    default:
                        {
                            return RedirectToAction("Index", "Home");
                        }

                }



                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    // Puedes configurar propiedades de autenticación adicionales aquí
                    ExpiresUtc = DateTime.UtcNow.AddHours(5),
                    IsPersistent = true,
                    AllowRefresh = true
                };

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties).Wait();

                return RedirectToAction("Index", "Home");
            }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}