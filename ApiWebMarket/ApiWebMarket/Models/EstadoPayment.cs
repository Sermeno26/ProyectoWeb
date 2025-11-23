using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class EstadoPayment
{
    public string IdEstadoPayment { get; set; } = null!;

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
