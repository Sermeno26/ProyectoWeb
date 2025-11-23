using System.ComponentModel.DataAnnotations;

namespace ApiWebMarket.DTO
{
    public class LoginRequest
    {
        [Required] public string IdUsuario { get; set; } = default!;
        [Required] public string Password { get; set; } = default!;
    }
}
