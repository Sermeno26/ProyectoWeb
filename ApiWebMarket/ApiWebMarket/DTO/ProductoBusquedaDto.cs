namespace ApiWebMarket.DTO
{
    public class ProductoBusquedaDto
    {
        public string IdProducto { get; set; }
        public string TituloProducto { get; set; } = "";
        public double? Precio { get; set; }
        public string? FotoBase64 { get; set; }
    }
}
