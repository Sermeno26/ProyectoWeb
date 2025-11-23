using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class Payment
{
    public string PaymentId { get; set; } = null!;

    public string IdOrden { get; set; } = null!;

    public string IdProveedorPago { get; set; } = null!;

    public double Monto { get; set; }

    public string Moneda { get; set; } = null!;

    public string IdEstadoPayment { get; set; } = null!;

    public string RefrenciaTransaccion { get; set; } = null!;

    public DateTime? FechaRealizacion { get; set; }

    public DateTime? FechaAutorizacion { get; set; }

    public virtual EstadoPayment IdEstadoPaymentNavigation { get; set; } = null!;

    public virtual Orden IdOrdenNavigation { get; set; } = null!;

    public virtual ProveedorPayment IdProveedorPagoNavigation { get; set; } = null!;
}
