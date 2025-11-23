using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class Orden
{
    public string IdOrden { get; set; } = null!;

    public string IdComprador { get; set; } = null!;

    public string IdEstadoOrden { get; set; } = null!;

    public double MontoTotal { get; set; }

    public string Moneda { get; set; } = null!;

    public string IdDireccion { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaPago { get; set; }

    public DateTime? FechaCierre { get; set; }

    public virtual ICollection<ArticulosOrden> ArticulosOrdens { get; set; } = new List<ArticulosOrden>();

    public virtual Usuario IdCompradorNavigation { get; set; } = null!;

    public virtual EstadosOrden IdEstadoOrdenNavigation { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Payout> Payouts { get; set; } = new List<Payout>();
}
