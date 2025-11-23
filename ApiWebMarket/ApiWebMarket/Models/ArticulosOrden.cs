using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class ArticulosOrden
{
    public string IdArticuloOrden { get; set; } = null!;

    public string IdOrden { get; set; } = null!;

    public string IdVendedor { get; set; } = null!;

    public string IdProductoVariante { get; set; } = null!;

    public string IdEstadoArticuloOrden { get; set; } = null!;

    public int Cantidad { get; set; }

    public double SubTotal { get; set; }

    public double PorcentajeComision { get; set; }

    public double SubtotalComision { get; set; }

    public string TrackingNumber { get; set; } = null!;

    public DateTime? FechaEntrega { get; set; }

    public virtual EstadoArticulosOrden IdEstadoArticuloOrdenNavigation { get; set; } = null!;

    public virtual Orden IdOrdenNavigation { get; set; } = null!;

    public virtual VariantesProducto IdProductoVarianteNavigation { get; set; } = null!;
}
