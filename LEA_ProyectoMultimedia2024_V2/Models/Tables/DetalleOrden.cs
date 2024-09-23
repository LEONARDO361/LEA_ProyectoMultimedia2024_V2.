using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Models.Tables;

public partial class DetalleOrden
{
    [Key]
    [Column("DetalleID")]
    public int DetalleId { get; set; }

    [Column("OrdenID")]
    public int OrdenId { get; set; }

    [Column("ProductoID")]
    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal PrecioTotal { get; set; }

    [ForeignKey("OrdenId")]
    [InverseProperty("DetalleOrden")]
    public virtual Orden Orden { get; set; } = null!;

    [ForeignKey("ProductoId")]
    [InverseProperty("DetalleOrden")]
    public virtual Producto Producto { get; set; } = null!;
}
