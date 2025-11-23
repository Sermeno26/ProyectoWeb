using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class EstadoArticulosOrden
{
    public string IdEstadoArticuloOrden { get; set; } = null!;

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<ArticulosOrden> ArticulosOrdens { get; set; } = new List<ArticulosOrden>();
}
