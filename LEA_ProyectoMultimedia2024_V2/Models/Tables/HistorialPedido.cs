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

    [Required]
    [StringLength(10)]
    public string EstadoAnterior { get; set; }

    [Required]
    [StringLength(10)]
    public string NuevoEstado { get; set; }

    public DateOnly FechaCambio { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string Notas { get; set; }

    [ForeignKey("OrdenId")]
    [InverseProperty("HistorialPedido")]
    public virtual Orden Orden { get; set; }
}
