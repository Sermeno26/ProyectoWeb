namespace ApiWebMarket.DTO
{
    public class CarritoItemDto
    {
        public string IdProducto { get; set; } = string.Empty;  
        public string TituloProducto { get; set; } = string.Empty;
        public string? FotoBase64 { get; set; }                   
        public double? Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
