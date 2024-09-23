using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Models.Tables;

public partial class DireccionEnvio
{
    [Key]
    [Column("DireccionID")]
    public int DireccionId { get; set; }

    [Column("ClienteID")]
    public int ClienteId { get; set; }

    [StringLength(100)]
    public string Dirección { get; set; } = null!;

    [StringLength(50)]
    public string Ciudad { get; set; } = null!;

    [StringLength(10)]
    public string Provincia { get; set; } = null!;

    public int CodigoPostal { get; set; }

    [StringLength(20)]
    public string Pais { get; set; } = null!;

    [ForeignKey("ClienteId")]
    [InverseProperty("DireccionEnvio")]
    public virtual Cliente Cliente { get; set; } = null!;
}
