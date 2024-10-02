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
        public int Calificación { get; set; }

        [Required(ErrorMessage = "Falta el campo Comentario")]
        [StringLength(500)]
        public string Comentario { get; set; }
        [Required(ErrorMessage = "Falta el campo FechaReseña")]

        [Column(TypeName = "datetime")]
        public DateTime FechaReseña { get; set; }


        [ForeignKey("ClienteId")]
        [InverseProperty("ReseñaProducto")]
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("ProductoId")]
        [InverseProperty("ReseñaProducto")]
        public virtual Producto Producto { get; set; }
    }
}
