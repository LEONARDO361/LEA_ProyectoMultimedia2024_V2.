using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LEA_ProyectoMultimedia2024_V2_.Models.DTOs
{
    public class DireccionEnviosDTO
    {
        [Key]
        [Column("DireccionID")]
        public int DireccionId { get; set; }

        [Column("ClienteID")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Falta el campo Direccion")]
        
        [StringLength(100)]
        public string Dirección { get; set; }

        [Required(ErrorMessage = "Falta el campo Ciudad")]

        [StringLength(50)]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Falta el campo Provincia")]

        [StringLength(10)]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "Falta el campo CodigoPostal")]
        public int CodigoPostal { get; set; }

        [Required(ErrorMessage = "Falta el campo Pais")]

        [StringLength(20)]
        public string Pais { get; set; }

        [Required(ErrorMessage = "Falta el campo Cliente")]
        [ForeignKey("ClienteId")]
        [InverseProperty("DireccionEnvio")]
        public virtual Cliente Cliente { get; set; }
    }
}
