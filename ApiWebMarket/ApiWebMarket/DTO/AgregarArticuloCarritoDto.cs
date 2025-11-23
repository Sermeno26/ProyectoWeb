namespace ApiWebMarket.DTO
{
    public class AgregarArticuloCarritoDto
    {
        public string IdUsuario { get; set; } = null!;
        public string IdProductoVariante { get; set; } = null!;
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public string Moneda { get; set; } = "$";
    }
}
