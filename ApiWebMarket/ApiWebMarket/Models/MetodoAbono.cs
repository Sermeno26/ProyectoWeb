using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class MetodoAbono
{
    public string IdMetodoAbono { get; set; } = null!;

    public string NombreMetodo { get; set; } = null!;

    public virtual ICollection<Payout> Payouts { get; set; } = new List<Payout>();
}
