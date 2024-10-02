using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LEA_ProyectoMultimedia2024_V2_.Models.DTOs
{
    public class DetalleOrdenDTO
    {

        [Key]
        [Column("DetalleID")]
        public int DetalleId { get; set; }


        [Column("OrdenID")]
        public int OrdenId { get; set; }


        [Column("ProductoID")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "Falta el campo Cantidad")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Falta el campo PrecioTotal")]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal PrecioTotal { get; set; }

        [ForeignKey("OrdenId")]
        [InverseProperty("DetalleOrden")]
        public virtual Orden Orden { get; set; }


        [ForeignKey("ProductoId")]
        [InverseProperty("DetalleOrden")]
        public virtual Producto Producto { get; set; }
    }
}
