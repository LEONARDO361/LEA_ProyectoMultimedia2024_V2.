using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LEA_ProyectoMultimedia2024_V2_.Models.DTOs
{
    public class CategoriaDTO
    {
        [Key]
        [Column("CategoriaID")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Falta el campo Nombre")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Falta el campo Descripcion")]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Falta el campo Peso")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Pesokg { get; set; }

        [InverseProperty("Categoria")]
        public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
    }
}
