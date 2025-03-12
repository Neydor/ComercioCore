using System;
using System.Collections.Generic;

namespace ComercioCore.Domain.Entities;

public partial class Municipio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Comerciante> Comerciantes { get; set; } = new List<Comerciante>();
}
