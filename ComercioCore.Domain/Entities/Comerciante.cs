using System;
using System.Collections.Generic;

namespace ComercioCore.Domain.Entities;

public partial class Comerciante
{
    public int Id { get; set; }

    public string RazonSocial { get; set; } = null!;

    public int MunicipioId { get; set; }

    public string? Telefono { get; set; }

    public string? CorreoElectronico { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime? FechaActualizacion { get; set; }

    public string? UsuarioActualizacion { get; set; }

    public virtual ICollection<Establecimiento> Establecimientos { get; set; } = new List<Establecimiento>();

    public virtual Municipio Municipio { get; set; } = null!;
}
