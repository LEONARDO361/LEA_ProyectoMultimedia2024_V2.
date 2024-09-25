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

    [Required]
    [StringLength(500)]
    public string Nombre { get; set; }

    [Required]
    [StringLength(500)]
    public string Descripcion { get; set; }

    public int Precio { get; set; }

    public int Cantidad { get; set; }

    [Required]
    [StringLength(500)]
    public string Procedencia { get; set; }

    [Required]
    [StringLength(100)]
    public string Estado { get; set; }

    [Required]
    [StringLength(500)]
    public string Marca { get; set; }

    [Column("CategoriaID")]
    public int CategoriaId { get; set; }

    [Column("DescuentoID")]
    public int? DescuentoId { get; set; }

    [ForeignKey("CategoriaId")]
    [InverseProperty("Producto")]
    public virtual Categoria Categoria { get; set; }

    [ForeignKey("DescuentoId")]
    [InverseProperty("Producto")]
    public virtual Descuento Descuento { get; set; }

    [InverseProperty("Producto")]
    public virtual ICollection<DetalleOrden> DetalleOrden { get; set; } = new List<DetalleOrden>();

    [InverseProperty("Producto")]
    public virtual ICollection<ReseñaProducto> ReseñaProducto { get; set; } = new List<ReseñaProducto>();
}
