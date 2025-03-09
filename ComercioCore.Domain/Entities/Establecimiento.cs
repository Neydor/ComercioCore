using System;
using System.Collections.Generic;

namespace ComercioCore.Domain.Entities;

public partial class Establecimiento
{
    public int Id { get; set; }

    public string NombreEstablecimiento { get; set; } = null!;

    public decimal Ingresos { get; set; }

    public int NumeroEmpleados { get; set; }

    public int ComercianteId { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public string? UsuarioActualizacion { get; set; }

    public virtual Comerciante Comerciante { get; set; } = null!;
}
