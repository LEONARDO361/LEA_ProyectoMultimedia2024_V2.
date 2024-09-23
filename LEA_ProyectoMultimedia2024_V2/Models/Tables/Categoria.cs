using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Models.Tables;

public partial class Categoria
{
    [Key]
    [Column("CategoriaID")]
    public int CategoriaId { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [StringLength(100)]
    public string Descripcion { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Pesokg { get; set; }

    [InverseProperty("Categoria")]
    public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
}
