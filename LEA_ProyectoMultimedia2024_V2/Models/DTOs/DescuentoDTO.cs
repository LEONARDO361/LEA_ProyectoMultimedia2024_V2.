using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LEA_ProyectoMultimedia2024_V2_.Models.DTOs
{
    public class DescuentoDTO
    {
        [Key]
        [Column("DescuentoID")]
        public int DescuentoId { get; set; }

        [Required(ErrorMessage = "Falta el campo PorcentajeDescuento")]
        [Range(0.01, 100, ErrorMessage = "El porcentaje de descuento debe estar entre 0.01 y 100")]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? PorcentajeDescuento { get; set; } // Nullable para permitir validación con mensajes personalizados

        [Required(ErrorMessage = "Falta el campo FechaInicio")]
        [DataType(DataType.Date, ErrorMessage = "Fecha de inicio inválida")]
        public DateOnly? FechaInicio { get; set; } // Nullable y mensaje personalizado para fecha

        [Required(ErrorMessage = "Falta el campo FechaFin")]
        [DataType(DataType.Date, ErrorMessage = "Fecha de fin inválida")]
        public DateOnly? FechaFin { get; set; } // Nullable y mensaje personalizado para fecha

        [Required(ErrorMessage = "Falta el campo TipoDescuento")]
        [StringLength(50)]
        public string TipoDescuento { get; set; }

        [InverseProperty("Descuento")]
        public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
    }

}
