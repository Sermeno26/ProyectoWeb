namespace ApiWebMarket.DTO
{
    public class AuthResponse
    {
        public string Token { get; set; } = default!;
        public DateTime ExpiresAtUtc { get; set; }
        public object User { get; set; } = default!;
    }
}
