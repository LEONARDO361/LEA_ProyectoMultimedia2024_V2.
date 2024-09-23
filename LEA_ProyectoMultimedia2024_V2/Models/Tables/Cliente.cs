using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Models.Tables;

public partial class Cliente
{
    [Key]
    [Column("ClienteID")]
    public int ClienteId { get; set; }

    [StringLength(10)]
    public string Nombre { get; set; } = null!;

    [StringLength(100)]
    public string Apellido { get; set; } = null!;

    [StringLength(100)]
    public string CorreoElectronico { get; set; } = null!;

    [StringLength(100)]
    public string Direccion { get; set; } = null!;

    public int Telefono { get; set; }

    [StringLength(15)]
    public string Sexo { get; set; } = null!;

    public int Edad { get; set; }

    [InverseProperty("Cliente")]
    public virtual ICollection<DireccionEnvio> DireccionEnvio { get; set; } = new List<DireccionEnvio>();

    [InverseProperty("Cliente")]
    public virtual ICollection<MetodoPago> MetodoPago { get; set; } = new List<MetodoPago>();

    [InverseProperty("Cliente")]
    public virtual ICollection<Orden> Orden { get; set; } = new List<Orden>();

    [InverseProperty("Cliente")]
    public virtual ICollection<ReseñaProducto> ReseñaProducto { get; set; } = new List<ReseñaProducto>();
}
