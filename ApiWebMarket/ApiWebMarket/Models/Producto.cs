using System;
using System.Collections.Generic;

namespace ApiWebMarket.Models;

public partial class Producto
{
    public string IdProducto { get; set; } = null!;

    public string IdVendedor { get; set; } = null!;

    public string TituloProducto { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public string IdCondicion { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public byte[] Foto { get; set; } = null!;

    public string IdCategoria { get; set; } = null!;

    public double? Precio { get; set; }

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;

    public virtual CondicionProducto IdCondicionNavigation { get; set; } = null!;

    public virtual Usuario IdVendedorNavigation { get; set; } = null!;

    public virtual ICollection<ProductoMedium> ProductoMedia { get; set; } = new List<ProductoMedium>();

    public virtual ICollection<VariantesProducto> VariantesProductos { get; set; } = new List<VariantesProducto>();
}
