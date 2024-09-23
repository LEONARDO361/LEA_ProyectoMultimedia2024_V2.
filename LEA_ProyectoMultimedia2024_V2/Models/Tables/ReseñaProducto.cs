using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Models.Tables;

public partial class ReseñaProducto
{
    [Key]
    [Column("ReseñaID")]
    public int ReseñaId { get; set; }

    [Column("ProductoID")]
    public int ProductoId { get; set; }

    [Column("ClienteID")]
    public int ClienteId { get; set; }

    public int Calificación { get; set; }

    [StringLength(500)]
    public string Comentario { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime FechaReseña { get; set; }

    [ForeignKey("ClienteId")]
    [InverseProperty("ReseñaProducto")]
    public virtual Cliente Cliente { get; set; } = null!;

    [ForeignKey("ProductoId")]
    [InverseProperty("ReseñaProducto")]
    public virtual Producto Producto { get; set; } = null!;
}
