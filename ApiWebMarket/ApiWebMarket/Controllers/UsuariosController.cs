using ApiWebMarket.DTO;
using ApiWebMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiWebMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly MercaUcaContext _db;
        private readonly IPasswordHasher<Usuario> _hasher;
        private readonly IConfiguration _config;

        public UsuariosController(MercaUcaContext db, IPasswordHasher<Usuario> hasher, IConfiguration config)
        {
            _db = db;
            _hasher = hasher;
            _config = config;
        }
        // POST: api/usuarios/registro
        [HttpPost("registro")]
        public async Task<IActionResult> Registrar([FromBody] RegisterRequest req)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

           
            if (await _db.Usuarios.AnyAsync(u => u.IdUsuario == req.IdUsuario.Trim()))
                return Conflict(new { message = "El nombre de usuario ya está en uso." });

            if (await _db.Usuarios.AnyAsync(u => u.EmailUsuario == req.EmailUsuario.Trim()))
                return Conflict(new { message = "El correo ya está registrado." });

            var user = new Usuario
            {
                IdUsuario = req.IdUsuario.Trim(),
                NombreUsuario = req.NombreUsuario.Trim(),
                EmailUsuario = req.EmailUsuario.Trim().ToLowerInvariant(),
                TelefonoUsuario = req.TelefonoUsuario.Trim(),
                IdRol = string.IsNullOrWhiteSpace(req.IdRol) ? "CMP" : req.IdRol!,
                IdEstado = string.IsNullOrWhiteSpace(req.IdEstado) ? "ACT" : req.IdEstado!
            };

            // Hash: (PBKDF2 + salt + versionado interno)
            user.PasswordHash = _hasher.HashPassword(user, req.Password);

            _db.Usuarios.Add(user);
            await _db.SaveChangesAsync();

            // Hash no visible
            var resp = new
            {
                user.IdUsuario,      
                user.NombreUsuario,  
                user.EmailUsuario,
                user.TelefonoUsuario,
                user.IdRol,
                user.IdEstado
            };


            return CreatedAtAction(nameof(GetById), new { idUsuario = user.IdUsuario }, resp);
        }

        // GET: api/usuarios/{idUsuario}
        [HttpGet("{idUsuario}")]
        public async Task<IActionResult> GetById(string idUsuario)
        {
            var u = await _db.Usuarios.AsNoTracking()
                .Where(x => x.IdUsuario == idUsuario)
                .Select(x => new {
                    x.IdUsuario,
                    x.NombreUsuario,
                    x.EmailUsuario,
                    x.TelefonoUsuario,
                    x.IdRol,
                    x.IdEstado
                })
                .SingleOrDefaultAsync();

            return u is null ? NotFound() : Ok(u);
        }

        // POST: api/usuarios/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.IdUsuario) || string.IsNullOrWhiteSpace(req.Password))
                return BadRequest(new { message = "Debe ingresar usuario y contraseña." });

            // Buscar usuario
            var user = await _db.Usuarios.SingleOrDefaultAsync(u => u.IdUsuario == req.IdUsuario.Trim());
            if (user is null)
                return Unauthorized(new { message = "Usuario o contraseña incorrectos." });

            // Verificar contraseña con hash
            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, req.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized(new { message = "Usuario o contraseña incorrectos." });

            // Crear los claims del token 
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.IdUsuario),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.IdUsuario),
                new Claim("nombre", user.NombreUsuario),
                new Claim("rol", user.IdRol),
                new Claim("email", user.EmailUsuario)
            };

            //  Configurar JWT 
            var jwtSection = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSection["ExpiresMinutes"] ?? "1"));

            var token = new JwtSecurityToken(
                issuer: jwtSection["Issuer"],
                audience: jwtSection["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            // cadena final del token
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            // Respuesta al cliente
            var response = new AuthResponse
            {
                Token = jwt,
                ExpiresAtUtc = expires,
                User = new
                {
                    user.IdUsuario,
                    user.NombreUsuario,
                    user.EmailUsuario,
                    user.TelefonoUsuario,
                    user.IdRol,
                    user.IdEstado
                }
            };

            return Ok(response);
        }

        
        //     PERFIL PROTEGIDO
        [HttpGet("perfil")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Perfil()
        {
            var idUsuario = User.Identity?.Name ?? User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            var u = await _db.Usuarios.AsNoTracking()
                .Where(x => x.IdUsuario == idUsuario)
                .Select(x => new
                {
                    x.IdUsuario,
                    x.NombreUsuario,
                    x.EmailUsuario,
                    x.TelefonoUsuario,
                    x.IdRol,
                    x.IdEstado
                })
                .SingleOrDefaultAsync();

            return u is null ? NotFound() : Ok(u);
        }
        [HttpGet("verify")]
        [Authorize] // Requiere token JWT
        public IActionResult VerifyToken()
        {
            return Ok(new { message = "Token válido" });
        }

    }
}
