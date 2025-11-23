using System.ComponentModel.DataAnnotations;

namespace ApiWebMarket.DTO
{
    public class RegisterRequest
    {
        // 👇 username único (se guardará en Usuario.IdUsuario)
        [Required, MinLength(3), MaxLength(64)]
        public string IdUsuario { get; set; } = default!;

        // 👇 nombre completo (se guardará en Usuario.NombreUsuario)
        [Required, MinLength(3), MaxLength(120)]
        public string NombreUsuario { get; set; } = default!;

        [Required, EmailAddress, MaxLength(160)]
        public string EmailUsuario { get; set; } = default!;

        [Required, MaxLength(32)]
        public string TelefonoUsuario { get; set; } = default!;

        [Required, MinLength(6), MaxLength(128)]
        public string Password { get; set; } = default!;

        // opcionales (si no vienen, se ponen por defecto en el controller)
        public string? IdRol { get; set; }
        public string? IdEstado { get; set; }
    }
}
