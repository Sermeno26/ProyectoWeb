using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class VariantesProducto
{
    public string IdProductoVariante { get; set; } = null!;

    public string IdProducto { get; set; } = null!;

    public string? Talla { get; set; }

    public string? Color { get; set; }

    public int Stock { get; set; }

    public virtual ICollection<ArticulosCarrito> ArticulosCarritos { get; set; } = new List<ArticulosCarrito>();

    public virtual ICollection<ArticulosOrden> ArticulosOrdens { get; set; } = new List<ArticulosOrden>();

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
