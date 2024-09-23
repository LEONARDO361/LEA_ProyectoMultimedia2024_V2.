using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Models.Tables;

public partial class HistorialPedido
{
    [Key]
    [Column("HistorialID")]
    public int HistorialId { get; set; }

    [Column("OrdenID")]
    public int OrdenId { get; set; }

    [StringLength(10)]
    public string EstadoAnterior { get; set; } = null!;

    [StringLength(10)]
    public string NuevoEstado { get; set; } = null!;

    public DateOnly FechaCambio { get; set; }

    [Column(TypeName = "text")]
    public string Notas { get; set; } = null!;

    [ForeignKey("OrdenId")]
    [InverseProperty("HistorialPedido")]
    public virtual Orden Orden { get; set; } = null!;
}
