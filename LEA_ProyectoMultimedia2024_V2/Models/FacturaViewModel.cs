using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using System;
using System.Collections.Generic;

namespace NBO_ProyectoMultimedia2024.Models.ViewModels
{
    public class FacturaViewModel
    {
        public int IdCliente { get; set; } // ID del cliente asociado a la factura
        public IEnumerable<Cliente> Clientes { get; set; }
        public int DireccionId { get; set; } // ID de la dirección seleccionada
        public IEnumerable<DireccionEnvio> Direcciones { get; set; }
        public int ProductoId { get; set; } // ID del producto seleccionado (temporal para la vista)
        public IEnumerable<Producto> Productos { get; set; }

        // Propiedades adicionales para los totales de la factura
        public decimal Subtotal { get; set; } // Subtotal calculado de los productos
        public decimal IVA { get; set; } // IVA aplicado
        public decimal PrecioTotal { get; set; }


        // Lista de detalles de la factura (productos agregados)
        public List<DetalleFacturaViewModel> DetalleProductos { get; set; } = new List<DetalleFacturaViewModel>();
    }

    // Modelo para los detalles de la factura (cada producto en la factura)
    public class DetalleFacturaViewModel
    {
        public int ProductoId { get; set; } // ID del producto
        public string NombreProducto { get; set; } // Nombre del producto
        public int Cantidad { get; set; } // Cantidad de producto
        public decimal PrecioUnitario { get; set; } // Precio unitario del producto
        public decimal PrecioTotal { get; set; }

    }
}
