using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Models.Tables;

public partial class LogUsuario
{
    [Key]
    [Column("LogUsuarioID")]
    public int LogUsuarioId { get; set; }

    public int TipoUsuario { get; set; }

    [Required]
    [StringLength(8)]
    public string Contrasena { get; set; }

    [InverseProperty("LogUsuario")]
    public virtual ICollection<Cliente> Cliente { get; set; } = new List<Cliente>();
}
