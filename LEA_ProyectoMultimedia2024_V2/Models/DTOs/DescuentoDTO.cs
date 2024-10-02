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
        [Column(TypeName = "decimal(18, 0)")]
        public decimal PorcentajeDescuento { get; set; }

        [Required(ErrorMessage = "Falta el campo FechaInicio")]
        public DateOnly FechaInicio { get; set; }

        [Required(ErrorMessage = "Falta el campo FechaFin")]
        public DateOnly FechaFin { get; set; }

        [Required(ErrorMessage = "Falta el campo TipoDescuento")]

        [StringLength(50)]
        public string TipoDescuento { get; set; }


        [InverseProperty("Descuento")]
        public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
    }
}
