namespace ApiWebMarket.DTO
{
    public class Producto_card
    {
            public string IdProducto { get; set; } = default!;
            public string TituloProducto { get; set; } = default!;
            public decimal Precio { get; set; }          
            public string? FotoBase64 { get; set; }      
    }
}
