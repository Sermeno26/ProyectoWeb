using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class CondicionProducto
{
    public string IdCondicion { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
