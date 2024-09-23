using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Models.Tables;

public partial class MetodoPago
{
    [Key]
    [Column("MetodoPagoID")]
    public int MetodoPagoId { get; set; }

    [Column("ClienteID")]
    public int ClienteId { get; set; }

    [StringLength(50)]
    public string TipoTarjeta { get; set; } = null!;

    public int NumeroTarjeta { get; set; }

    public DateOnly FechaExpiracion { get; set; }

    [Column("CVV")]
    public int Cvv { get; set; }

    [ForeignKey("ClienteId")]
    [InverseProperty("MetodoPago")]
    public virtual Cliente Cliente { get; set; } = null!;
}
