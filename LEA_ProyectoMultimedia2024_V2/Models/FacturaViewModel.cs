using LEA_ProyectoMultimedia2024_V2_.Models.Tables;

using System.Collections.Generic;

namespace NBO_ProyectoMultimedia2024.Models.ViewModels
{
    public class FacturaViewModel
    {
        public int IdCliente { get; set; }
        public IEnumerable<Cliente> Clientes { get; set; }
        public int DireccionId { get; set; }
        public IEnumerable<DireccionEnvio> Direcciones { get; set; } // Adaptado a DireccionEnvio
        public int ProductoId { get; set; }
        public IEnumerable<Producto> Productos { get; set; }
    }
}