using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class Role
{
    public string IdRol { get; set; } = null!;

    public string? NombreRol { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
