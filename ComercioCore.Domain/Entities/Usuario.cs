using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComercioCore.Domain.Entities;

public partial class Usuario
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    [Required]
    [EmailAddress]
    public string CorreoElectronico { get; set; }

    [Required]
    public string Contrasena { get; set; } // Almacenará el hash

    [Required]
    public string Rol { get; set; }
}
