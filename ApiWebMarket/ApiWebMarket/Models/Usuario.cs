using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class Usuario
{
    public string IdUsuario { get; set; } = null!;

    public string EmailUsuario { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public string TelefonoUsuario { get; set; } = null!;

    public string IdEstado { get; set; } = null!;

    public string IdRol { get; set; } = null!;

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<Direccione> Direcciones { get; set; } = new List<Direccione>();

    public virtual EstadoUsuario IdEstadoNavigation { get; set; } = null!;

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Orden> Ordens { get; set; } = new List<Orden>();

    public virtual ICollection<Payout> Payouts { get; set; } = new List<Payout>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
