using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LEA_ProyectoMultimedia2024_V2_.Models.DTOs
{
    public class ReseñaProductoDTO
    {
        [Key]
        [Column("ReseñaID")]
        public int ReseñaId { get; set; }

        [Column("ProductoID")]
        public int ProductoId { get; set; }

        [Column("ClienteID")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "Falta el campo Calificación")]
        [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5")]
        public int? Calificación { get; set; }


        [Required(ErrorMessage = "Falta el campo Comentario")]
        [StringLength(500)]
        public string Comentario { get; set; }
        [Required(ErrorMessage = "Falta el campo FechaReseña")]
        [Column(TypeName = "datetime")]
        public DateTime? FechaReseña { get; set; } // Cambiado a nullable para permitir la validación


        [ForeignKey("ClienteId")]
        [InverseProperty("ReseñaProducto")]
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("ProductoId")]
        [InverseProperty("ReseñaProducto")]
        public virtual Producto Producto { get; set; }
    }
}
