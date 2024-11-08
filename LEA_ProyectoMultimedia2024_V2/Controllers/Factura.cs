//using CGS_ProyectoMultimedia2024.Models.Tables;
//using CGS_ProyectoMultimedia2024.Services.Interfaces;
//using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CGS_ProyectoMultimedia2024.Controllers
//{
//    public class FacturaController : Controller
//    {
//        private readonly ICliente
//        private readonly IDireccionEnvios _direccionEnvio;
//        private readonly IProducto _producto;

//        public FacturaController(IDireccionEnvios direccionEnvio, IProducto producto)
//        {
//            this.direccionEnvio = direccionEnvio;
//            _producto = producto;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var clientes = await _clientes.GetClientesAsync();
//            var productos = await _productos.GetProductosAsync();

//            ViewBag.ClienteId = new SelectList(clientes, "ID_Cliente", "Nombre");
//            ViewBag.ProductoId = new SelectList(productos, "ID_Producto", "Nombre");

//            return View();
//        }

//        public async Task<JsonResult> GetDireccionesByCliente(int clienteId)
//        {
//            var direcciones = await _direccionEnvio.GetDireccionesByClienteAsync(clienteId);
//            return Json(direcciones.Select(d => new
//            {
//                value = d.ID_Direccion,
//                text = d.Calle
//            }));
//        }

//        public async Task<JsonResult> GetClienteDetails(int clienteId)
//        {
//            var cliente = await _clientes.GetClienteDetailsAsync(clienteId);
//            return Json(new
//            {
//                nombreCompleto = $"{cliente.Nombre} {cliente.Apellido}",
//                correo = cliente.Email,
//                telefono = cliente.Telefono
//            });
//        }

//        public async Task<JsonResult> GetDireccionesDetails(int direccionId)
//        {
//            var direccion = await _direcciones.GetDireccionDetailsAsync(direccionId);
//            return Json(new
//            {
//                direccion = direccion.Calle,
//                ciudad = direccion.Ciudad,
//                codigoPostal = direccion.Codigo_Postal
//            });
//        }

//        public async Task<JsonResult> GetProductosDetails(int productoId)
//        {
//            var producto = await _productos.GetProductoDetailsAsync(productoId);
//            return Json(new
//            {
//                nombreProducto = producto.Nombre,
//                precio = producto.Precio
//            });
//        }
//    }
//}
