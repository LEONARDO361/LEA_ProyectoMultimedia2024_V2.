﻿using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LEA_ProyectoMultimedia2024_V2_.Models.DTOs
{
    public class MetodoPagoDTO
    {
        [Key]
        [Column("MetodoPagoID")]
        public int MetodoPagoId { get; set; }

        [Column("ClienteID")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Falta el campo TipoTarjeta")]

        [StringLength(50)]
        public string TipoTarjeta { get; set; }

        [Required(ErrorMessage = "Falta el campo NumeroTarjeta")]
        public int? NumeroTarjeta { get; set; }

        [Required(ErrorMessage = "Falta el campo FechaExpiracion")]
        public DateOnly? FechaExpiracion { get; set; } // Cambiado a nullable para manejar validación

        [Required(ErrorMessage = "Falta el campo Cvv")]
        [Range(100, 999, ErrorMessage = "El CVV debe ser un número de 3 dígitos")]
        [Column("CVV")]
        public int? Cvv { get; set; }


        [ForeignKey("ClienteId")]
        [InverseProperty("MetodoPago")]
        public virtual Cliente Cliente { get; set; }
    }
}
