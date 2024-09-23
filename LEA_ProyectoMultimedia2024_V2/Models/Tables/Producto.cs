using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Models.Tables;

public partial class Producto
{
    [Key]
    [Column("ProductoID")]
    public int ProductoId { get; set; }

    [StringLength(500)]
    public string Nombre { get; set; } = null!;

    [StringLength(500)]
    public string Descripcion { get; set; } = null!;

    public int Precio { get; set; }

    public int Cantidad { get; set; }

    [StringLength(500)]
    public string Procedencia { get; set; } = null!;

    [StringLength(100)]
    public string Estado { get; set; } = null!;

    [StringLength(500)]
    public string Marca { get; set; } = null!;

    [Column("CategoriaID")]
    public int CategoriaId { get; set; }

    [ForeignKey("CategoriaId")]
    [InverseProperty("Producto")]
    public virtual Categoria Categoria { get; set; } = null!;

    [InverseProperty("Producto")]
    public virtual ICollection<Descuento> Descuento { get; set; } = new List<Descuento>();

    [InverseProperty("Producto")]
    public virtual ICollection<DetalleOrden> DetalleOrden { get; set; } = new List<DetalleOrden>();

    [InverseProperty("Producto")]
    public virtual ICollection<ReseñaProducto> ReseñaProducto { get; set; } = new List<ReseñaProducto>();
}
