
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;

namespace CGS_ProyectoMultimedia2024.Controllers
{
    public class FacturaController : Controller
    {
        private readonly ICliente _clientes;
        private readonly IDireccionEnvios _direccionEnvio;
        private readonly IProducto _productos;

        public FacturaController(ICliente clientes, IDireccionEnvios direccionEnvio, IProducto productos)
        {
            _clientes = clientes;
            _direccionEnvio = direccionEnvio;
            _productos = productos;
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _clientes.GetAllClientesAsync();
            var productos = await _productos.GetProductosAsync();

            ViewBag.ClienteId = new SelectList(clientes, "ID_Cliente", "Nombre");
            ViewBag.ProductoId = new SelectList(productos, "ID_Producto", "Nombre");

            return View();
        }

        public async Task<JsonResult> GetDireccionesByCliente(int clienteId)
        {
            var direcciones = await _direccionEnvio.GetAllDireccionesAsync();
            return Json(direcciones.Select(d => new
            {
                value = d.Dirección,
                text = d.Provincia
            }));
        }

        public async Task<JsonResult> GetClienteDetails(int clienteId)
        {
            var cliente = await _clientes.GetClienteByIdAsync(clienteId);
            return Json(new
            {
                nombreCompleto = $"{cliente.Nombre} {cliente.Apellido}",
                correo = cliente.CorreoElectronico,
                telefono = cliente.Telefono
            });
        }

        public async Task<JsonResult> GetDireccionesDetails(int direccionId)
        {
            var direccion = await _direccionEnvio.GetDireccionByIdAsync(direccionId);
            return Json(new
            {
                direccion = direccion.Provincia,
                ciudad = direccion.Ciudad,
                codigoPostal = direccion.CodigoPostal
            });
        }

        public async Task<JsonResult> GetProductosDetails(int productoId)
        {
            var producto = await _productos.GetProductoByIdAsync(productoId);
            return Json(new
            {
                nombreProducto = producto.Nombre,
                precio = producto.Precio
            });
        }
    }
}
