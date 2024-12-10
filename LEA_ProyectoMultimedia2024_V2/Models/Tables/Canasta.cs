using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Models.Tables;

public partial class Canasta
{
    [Key]
    [Column("CanastaID")]
    public int CanastaId { get; set; }

    [Column("ClienteID")]
    public int ClienteId { get; set; }

    public DateOnly FechaC { get; set; }

    [Required]
    [StringLength(50)]
    public string Estado { get; set; }

    public int Total { get; set; }

    [ForeignKey("ClienteId")]
    [InverseProperty("Canasta")]
    public virtual Cliente Cliente { get; set; }

    [InverseProperty("Canasta")]
    public virtual ICollection<DetalleCanasta> DetalleCanasta { get; set; } = new List<DetalleCanasta>();
}
