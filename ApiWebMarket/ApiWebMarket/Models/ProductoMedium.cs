using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class ProductoMedium
{
    public string IdMedia { get; set; } = null!;

    public string IdProducto { get; set; } = null!;

    public int PosicionGaleria { get; set; }

    public bool ProductoPrimario { get; set; }

    public bool Novedades { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
