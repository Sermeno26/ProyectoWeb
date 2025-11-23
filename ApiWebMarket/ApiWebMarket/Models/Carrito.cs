using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class Carrito
{
    public string IdCarrito { get; set; } = null!;

    public string IdComprador { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<ArticulosCarrito> ArticulosCarritos { get; set; } = new List<ArticulosCarrito>();

    public virtual Usuario IdCompradorNavigation { get; set; } = null!;
}
