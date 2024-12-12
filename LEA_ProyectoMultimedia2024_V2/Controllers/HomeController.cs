//using LEA_ProyectoMultimedia2024.Models;
using LEA_ProyectoMultimedia2024_V2_.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;

namespace LEA_ProyectoMultimedia2024.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController>_logger;
        private readonly GimnasioContext _context;

        // Constructor con inyección de dependencias
        public HomeController(ILogger<HomeController> logger, GimnasioContext context)
        {
            _logger = logger;
            _context = context;
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

        public IActionResult ListaProductos()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string correoElectronico, string contrasena)
        {
            // Buscar al cliente en la base de datos por correo electrónico
            var cliente = await _context.Cliente
                .Include(c => c.LogUsuario) // Incluir la relación con LogUsuario
                .FirstOrDefaultAsync(c => c.CorreoElectronico.Trim() == correoElectronico.Trim());

            // Validar si el cliente existe y la contraseña coincide
            if (cliente == null || cliente.LogUsuario.Contrasena.Trim() != contrasena.Trim())
            {
                ModelState.AddModelError("", "Correo o contraseña incorrectos. Por favor, inténtalo de nuevo.");
                return View();
            }

            // Crear lista de claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, cliente.Nombre.Trim()), // Nombre del cliente
        new Claim(ClaimTypes.NameIdentifier, cliente.ClienteId.ToString()), // ID del cliente
        new Claim("Correo", cliente.CorreoElectronico.Trim()) // Correo como claim adicional
    };

            // Asignar roles según el TipoUsuario
            switch (cliente.LogUsuario.TipoUsuario)
            {
                case 1:
                    claims.Add(new Claim(ClaimTypes.Role, "Comprador"));
                    break;
                case 2:
                    claims.Add(new Claim(ClaimTypes.Role, "Vendedor"));
                    break;
                case 3:
                    claims.Add(new Claim(ClaimTypes.Role, "Mantenedor"));
                    break;
                case 4:
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    break;
                default:
                    throw new Exception("Tipo de usuario desconocido.");
            }

            // Crear identidad y principal
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddHours(5), // Sesión válida por 5 horas
                IsPersistent = true, // Sesión persistente
                AllowRefresh = true // Permitir refrescar la sesión
            };

            // Firmar cookie de autenticación
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // Redirigir al usuario a la página principal
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}