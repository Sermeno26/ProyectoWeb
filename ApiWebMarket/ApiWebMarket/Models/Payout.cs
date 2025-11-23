using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class Payout
{
    public string PayoutId { get; set; } = null!;

    public string IdVendedor { get; set; } = null!;

    public string IdOrden { get; set; } = null!;

    public double PorcentajeComision { get; set; }

    public double MontoNeto { get; set; }

    public string IdPayoutEstado { get; set; } = null!;

    public string IdMetodoAbono { get; set; } = null!;

    public DateTime? FechaRealizacion { get; set; }

    public virtual MetodoAbono IdMetodoAbonoNavigation { get; set; } = null!;

    public virtual Orden IdOrdenNavigation { get; set; } = null!;

    public virtual PayoutEstado IdPayoutEstadoNavigation { get; set; } = null!;

    public virtual Usuario IdVendedorNavigation { get; set; } = null!;
}
