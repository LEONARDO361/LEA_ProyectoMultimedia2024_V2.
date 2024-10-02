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
        public DateOnly FechaOrden { get; set; }

        [Required(ErrorMessage = "Falta el campo Total")]
        public int Total { get; set; }

        [Required(ErrorMessage = "Falta el campo Estado")]

        [StringLength(50)]

        public string Estado { get; set; }

        [Required(ErrorMessage = "Falta el campo ClienteId")]
        [Column("ClienteID")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        [InverseProperty("Orden")]
        [Required(ErrorMessage = "Falta el campo Cliente")]
        public virtual Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Falta el campo DetalleOrden")]

        [InverseProperty("Orden")]
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; } = new List<DetalleOrden>();
    }
}
