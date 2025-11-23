using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class Direccione
{
    public string IdDireccion { get; set; } = null!;

    public string? NombreDireccion { get; set; }

    public string IdUsuario { get; set; } = null!;

    public string? NombreReceptor { get; set; }

    public string? TelefonoReceptor { get; set; }

    public bool DireccionDefault { get; set; }

    public string IdPais { get; set; } = null!;

    public virtual Pai IdPaisNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
