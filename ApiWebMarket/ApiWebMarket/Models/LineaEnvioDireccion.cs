using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class LineaEnvioDireccion
{
    public string IdDireccion { get; set; } = null!;

    public string LineaDireccion { get; set; } = null!;

    public virtual Direccione IdDireccionNavigation { get; set; } = null!;
}
