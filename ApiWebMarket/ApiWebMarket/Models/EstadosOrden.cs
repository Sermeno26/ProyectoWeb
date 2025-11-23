using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class EstadosOrden
{
    public string IdEstadoOrden { get; set; } = null!;

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Orden> Ordens { get; set; } = new List<Orden>();
}
