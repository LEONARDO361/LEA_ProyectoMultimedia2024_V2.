using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Models.Tables;

public partial class Descuento
{
    [Key]
    [Column("DescuentoID")]
    public int DescuentoId { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal PorcentajeDescuento { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    [Required]
    [StringLength(50)]
    public string TipoDescuento { get; set; }

    [InverseProperty("Descuento")]
    public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
}
