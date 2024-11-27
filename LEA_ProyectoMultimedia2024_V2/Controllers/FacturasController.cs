using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using NBO_ProyectoMultimedia2024.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using LEA_ProyectoMultimedia2024_V2_.Services.Repository;
using Microsoft.AspNetCore.Authorization;

public class FacturasController : Controller
{
    private readonly ICliente _clientes;
    private readonly IDireccionEnvios _direcciones;
    private readonly IProducto _productos;
    private readonly IOrdenRepository _Compra;

    public FacturasController(ICliente clientes, IDireccionEnvios direcciones, IProducto productos, IOrdenRepository compra)
    {
        _clientes = clientes;
        _direcciones = direcciones;
        _productos = productos;
        _Compra = compra;
    }

    public async Task<IActionResult> GuardarFactura([FromBody] FacturaViewModel Compra)
    {
        // Validar si el modelo es válido
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Datos de la factura inválidos. Verifica los campos." });
        }

        if (Compra == null || !Compra.DetalleProductos.Any())
        {
            return Json(new { success = false, message = "La factura no contiene detalles." });
        }
        if (Compra == null)
        {
            return Json(new { success = false, message = "El modelo de factura es nulo." });
        }

        var nuevaOrden = new Orden
        {
            FechaOrden = DateOnly.FromDateTime(DateTime.Now),
            Total = (int)Compra.PrecioTotal, // Asegúrate de hacer una conversión si es necesario
            Estado = "Pendiente",
            ClienteId = Compra.IdCliente
        };


        // Crear detalles de la orden
        var detalleOrdens = Compra.DetalleProductos.Select(detalle => new DetalleOrden
        {
            ProductoId = detalle.ProductoId,
            Cantidad = detalle.Cantidad,
            PrecioTotal = detalle.PrecioUnitario * detalle.Cantidad
        }).ToList();

        try
        {
            // Guardar la orden y los detalles en la base de datos
            await  _Compra.CreateOrdenAsync(nuevaOrden, detalleOrdens);

            return Json(new { success = true, message = "Factura guardada correctamente." });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Error al guardar la factura: {ex.Message}" });
        }
    }

    [Authorize(Policy = "Vendedor")]
    public async Task<IActionResult> Index()
    {
        var clientes = await _clientes.GetAllClientesAsync() ?? new List<Cliente>();
        var productos = await _productos.GetProductosAsync() ?? new List<Producto>();

        // Verificar si las listas no están vacías para evitar errores en la vista
        if (!clientes.Any())
        {
            ModelState.AddModelError("", "No hay clientes disponibles.");
        }

        if (!productos.Any())
        {
            ModelState.AddModelError("", "No hay productos disponibles.");
        }

        // Filtrar clientes y productos para evitar elementos con propiedades nulas
        clientes = clientes.Where(c => c != null && c.ClienteId != 0 && !string.IsNullOrEmpty(c.Nombre)).ToList();
        productos = productos.Where(p => p != null && p.ProductoId != 0 && !string.IsNullOrEmpty(p.Nombre)).ToList();

        // Pasar datos a la vista usando ViewData
        ViewData["ClienteId"] = new SelectList(clientes, "ClienteId", "Nombre");  
        ViewData["Productos"] = new SelectList(productos, "ProductoId", "Nombre"); 

        var viewModel = new FacturaViewModel
        {
            Clientes = clientes,
            Productos = productos
        };

        return View(viewModel);
    }

    public async Task<JsonResult> GetDireccionesByCliente(int clienteId)
    {
        var direcciones = await _direcciones.GetDireccionesByClienteAsync(clienteId);
        return Json(direcciones.Select(d => new
        {
            value = d.DireccionId,
            text = $"{d.Dirección} - {d.Ciudad}"
        }));
    }

    public async Task<JsonResult> GetClienteDetails(int clienteId)
    {
        var cliente = await _clientes.GetClienteByIdAsync(clienteId);
        return Json(new
        {
            nombreCompleto = $"{cliente?.Nombre} {cliente?.Apellido}", // Concatenar nombre completo
            correo = cliente?.CorreoElectronico,
            telefono = cliente?.Telefono
        });
    }

    public async Task<JsonResult> GetDireccionesDetails(int direccionId)
    {
        var direccion = await _direcciones.GetDireccionByIdAsync(direccionId);
        return Json(new
        {
            direccion = direccion?.Dirección,
            ciudad = direccion?.Ciudad,
            codigoPostal = direccion?.CodigoPostal
        });
    }

    public async Task<JsonResult> GetProductosDetails(int productoId)
    {
        var producto = await _productos.GetProductoByIdAsync(productoId);
        return Json(new
        {
            nombreProducto = producto?.Nombre,
            precio = producto?.Precio
        });
    }
}
