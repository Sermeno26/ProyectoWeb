using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class Pai
{
    public string IdPais { get; set; } = null!;

    public string NombrePais { get; set; } = null!;

    public virtual ICollection<Direccione> Direcciones { get; set; } = new List<Direccione>();
}
