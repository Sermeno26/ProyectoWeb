using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class PayoutEstado
{
    public string IdPayoutEstado { get; set; } = null!;

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Payout> Payouts { get; set; } = new List<Payout>();
}
