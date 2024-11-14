using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LEA_ProyectoMultimedia2024_V2_.Models.DTOs
{
    public class OrdenDTO
    {
        [Required(ErrorMessage = "Falta el campo OrdenId")]
        [Key]
        [Column("OrdenID")]
        public int OrdenId { get; set; }

        [Required(ErrorMessage = "Falta el campo FechaOrden")]
        [DataType(DataType.Date, ErrorMessage = "Fecha de orden inválida")]
        public DateOnly? FechaOrden { get; set; } // Cambiado a nullable con mensaje de error personalizado

        [Required(ErrorMessage = "Falta el campo Total")]
        [Range(1, int.MaxValue, ErrorMessage = "El total debe ser un valor positivo")]
        public int? Total { get; set; } // Cambiado a nullable con un rango para evitar el valor 0 o negativo

        [Required(ErrorMessage = "Falta el campo Estado")]
        [StringLength(50)]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Falta el campo ClienteId")]
        [Column("ClienteID")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        [InverseProperty("Orden")]
        public virtual Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Falta el campo DetalleOrden")]
        [InverseProperty("Orden")]
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; } = new List<DetalleOrden>();
    }

}
