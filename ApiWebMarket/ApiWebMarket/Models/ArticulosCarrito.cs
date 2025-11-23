using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class ArticulosCarrito
{
    public string IdArticulosCarrito { get; set; } = null!;

    public string IdCarrito { get; set; } = null!;

    public string IdProductoVariante { get; set; } = null!;

    public int Cantidad { get; set; }

    public double Precio { get; set; }

    public string Moneda { get; set; } = null!;

    public virtual Carrito IdCarritoNavigation { get; set; } = null!;

    public virtual VariantesProducto IdProductoVarianteNavigation { get; set; } = null!;
}
