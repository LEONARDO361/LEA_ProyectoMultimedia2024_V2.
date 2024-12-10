using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Models.Tables;

public partial class DetalleCanasta
{
    [Key]
    [Column("DetalleCanastaID")]
    public int DetalleCanastaId { get; set; }

    [Column("CanastaID")]
    public int CanastaId { get; set; }

    [Column("ProductoID")]
    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    public int PrecioUni { get; set; }

    public int SubTotal { get; set; }

    [ForeignKey("CanastaId")]
    [InverseProperty("DetalleCanasta")]
    public virtual Canasta Canasta { get; set; }

    [ForeignKey("ProductoId")]
    [InverseProperty("DetalleCanasta")]
    public virtual Producto Producto { get; set; }
}
