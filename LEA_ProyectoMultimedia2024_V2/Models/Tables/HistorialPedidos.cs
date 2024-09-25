using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Models.Tables;

public partial class HistorialPedidos
{
    [Key]
    public int HistorialD { get; set; }

    [Required]
    [StringLength(50)]
    public string EstadoAnt { get; set; }

    [Required]
    [StringLength(50)]
    public string NuevoEst { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaCambio { get; set; }

    [Required]
    [StringLength(100)]
    public string Notas { get; set; }
}
