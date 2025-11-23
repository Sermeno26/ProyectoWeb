using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class ProveedorPayment
{
    public string IdProveedorPago { get; set; } = null!;

    public string NombreProveedor { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
