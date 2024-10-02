using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LEA_ProyectoMultimedia2024_V2_.Models.DTOs
{
    public class ClienteDTO
    {

        [Key]
        [Column("ClienteID")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Falta el campo Nombre")]
        [StringLength(10)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Falta el campo Apellido")]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Falta el campo CorreoElectronico")]
        [StringLength(100)]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "Falta el campo Direccion")]
        [StringLength(100)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Falta el campo Telefono")]
        public int Telefono { get; set; }

        [Required(ErrorMessage = "Falta el campo Sexo")]
        [StringLength(15)]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Falta el campo Edad")]
        public int Edad { get; set; }

        [InverseProperty("Cliente")]
        public virtual ICollection<DireccionEnvio> DireccionEnvio { get; set; } = new List<DireccionEnvio>();

        [InverseProperty("Cliente")]
        public virtual ICollection<MetodoPago> MetodoPago { get; set; } = new List<MetodoPago>();

        [InverseProperty("Cliente")]
        public virtual ICollection<Orden> Orden { get; set; } = new List<Orden>();

        [InverseProperty("Cliente")]
        public virtual ICollection<ReseñaProducto> ReseñaProducto { get; set; } = new List<ReseñaProducto>();
    }
}
