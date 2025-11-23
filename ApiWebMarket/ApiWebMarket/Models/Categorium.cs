using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class Categorium
{
    public string IdCategoria { get; set; } = null!;

    public string NombreCategoria { get; set; } = null!;

    public bool CategoriaActiva { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
