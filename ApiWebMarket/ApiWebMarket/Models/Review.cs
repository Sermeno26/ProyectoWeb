using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class Review
{
    public string ReviewId { get; set; } = null!;

    public string IdArticuloOrden { get; set; } = null!;

    public string IdVendedor { get; set; } = null!;

    public string IdComprador { get; set; } = null!;

    public string IdProducto { get; set; } = null!;

    public int Puntuacion { get; set; }

    public string Comentario { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public bool Publico { get; set; }
}
