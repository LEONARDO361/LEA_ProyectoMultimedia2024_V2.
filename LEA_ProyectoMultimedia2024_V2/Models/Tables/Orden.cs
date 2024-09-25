using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Models.Tables;

public partial class Orden
{
    [Key]
    [Column("OrdenID")]
    public int OrdenId { get; set; }

    public DateOnly FechaOrden { get; set; }

    public int Total { get; set; }

    [Required]
    [StringLength(50)]
    public string Estado { get; set; }

    [Column("ClienteID")]
    public int ClienteId { get; set; }

    [ForeignKey("ClienteId")]
    [InverseProperty("Orden")]
    public virtual Cliente Cliente { get; set; }

    [InverseProperty("Orden")]
    public virtual ICollection<DetalleOrden> DetalleOrden { get; set; } = new List<DetalleOrden>();
}
