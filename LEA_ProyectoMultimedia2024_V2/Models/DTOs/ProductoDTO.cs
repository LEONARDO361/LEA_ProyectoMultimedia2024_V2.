using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LEA_ProyectoMultimedia2024_V2_.Models.DTOs
{
    public class ProductoDTO
    {
        [Key]
        [Column("ProductoID")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "Falta el campo Nombre")]
        [StringLength(500)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Falta el campo Descripcion")]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Falta el campo Precio")]
        [Range(1, int.MaxValue, ErrorMessage = "El precio debe ser un valor positivo")]
        public int? Precio { get; set; }

        [Required(ErrorMessage = "Falta el campo Cantidad")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un valor positivo")]
        public int? Cantidad { get; set; }

        [Required(ErrorMessage = "Falta el campo Procedencia")]
        [StringLength(500)]
        public string Procedencia { get; set; }

        [Required(ErrorMessage = "Falta el campo Estado")]
        [StringLength(100)]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Falta el campo Marca")]
        [StringLength(500)]
        public string Marca { get; set; }
        public IFormFile Imagen { get; set; } // Campo para manejar la imagen cargada

        [Column("CategoriaID")]
        public int CategoriaId { get; set; }

        [Column("DescuentoID")]
        public int? DescuentoId { get; set; }

        [ForeignKey("CategoriaId")]
        [InverseProperty("Producto")]
        public virtual Categoria Categoria { get; set; }

        [ForeignKey("DescuentoId")]
        [InverseProperty("Producto")]
        public virtual Descuento Descuento { get; set; }

        [InverseProperty("Producto")]
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; } = new List<DetalleOrden>();

        [InverseProperty("Producto")]
        public virtual ICollection<ReseñaProducto> ReseñaProducto { get; set; } = new List<ReseñaProducto>();
    }

}
