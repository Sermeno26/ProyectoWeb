using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class EstadoUsuario
{
    public string IdEstado { get; set; } = null!;

    public string? NombreEstado { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
