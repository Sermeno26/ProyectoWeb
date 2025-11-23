using ApiWebMarket.Models;                     // IMPORTA EL DTO EXTERNO
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiWebMarket.DTO;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace ApiWebMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class CarritoController : ControllerBase
    {
        private readonly MercaUcaContext _context;

        public CarritoController(MercaUcaContext context)
        {
            _context = context;
        }

        // GET: api/Carrito/{usuario}
        [HttpGet("{usuario}")]
        public async Task<ActionResult<IEnumerable<CarritoItemDto>>> GetCarritoPorUsuario(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return BadRequest("El usuario es requerido.");

            var items = await (
                from ac in _context.ArticulosCarritos
                join c in _context.Carritos on ac.IdCarrito equals c.IdCarrito
                join vp in _context.VariantesProductos on ac.IdProductoVariante equals vp.IdProductoVariante
                join p in _context.Productos on vp.IdProducto equals p.IdProducto
                where c.IdComprador == usuario
                select new CarritoItemDto
                {
                    IdProducto = p.IdProducto,
                    TituloProducto = p.TituloProducto,
                    FotoBase64 = p.Foto != null
                        ? "data:image/jpeg;base64," + Convert.ToBase64String(p.Foto)
                        : null,

                   
                    Precio = p.Precio != null ? (double?)p.Precio : null,
                    Cantidad = ac.Cantidad
                }
            ).ToListAsync();

            return Ok(items);
        }


        [HttpPost("agregar-articulo")]
        public async Task<ActionResult> AgregarArticulo([FromBody] AgregarArticuloCarritoDto dto)
        {
            if (dto.Cantidad <= 0)
                return BadRequest("La cantidad debe ser mayor que cero.");

            // Ver si ya exite el carrito
            var carrito = await _context.Carritos
                .Include(c => c.ArticulosCarritos)
                .FirstOrDefaultAsync(c => c.IdComprador == dto.IdUsuario);

            // Crear carrito si aun no esta creado
            if (carrito == null)
            {
                carrito = new Carrito
                {
                    IdCarrito = Guid.NewGuid().ToString(), 
                    IdComprador = dto.IdUsuario,
                    FechaCreacion = DateTime.UtcNow,
                    FechaModificacion = DateTime.UtcNow
                };

                _context.Carritos.Add(carrito);
            }
            else
            {
                carrito.FechaModificacion = DateTime.UtcNow;
            }

            // 3) Insertar artículo en ArticulosCarrito y uso de GUID para el id
            var articulo = new ArticulosCarrito
            {
                IdArticulosCarrito = Guid.NewGuid().ToString(),
                IdCarrito = carrito.IdCarrito,
                IdProductoVariante = dto.IdProductoVariante,
                Cantidad = dto.Cantidad,
                Precio = dto.Precio,
                Moneda = string.IsNullOrWhiteSpace(dto.Moneda) ? "$" : dto.Moneda
            };

            _context.ArticulosCarritos.Add(articulo);

            await _context.SaveChangesAsync();

            // Mensaje de retiorno mostrado en Popup
            return Ok(new { mensaje = "Artículo agregado correctamente." });
        }

        
        [HttpPost("finalizar-compra/{idUsuario}")]
        public async Task<IActionResult> FinalizarCompra(string idUsuario)
        {
            if (string.IsNullOrWhiteSpace(idUsuario))
                return BadRequest("El id de usuario es requerido.");

            // Budsqueda del correo del usuario Debe de tener debido a la validacion del registro
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);

            if (usuario == null)
                return NotFound("Usuario no encontrado.");

            if (string.IsNullOrWhiteSpace(usuario.EmailUsuario))
                return BadRequest("El usuario no tiene correo registrado.");

            string correoDestino = usuario.EmailUsuario;

            // Carrito del usuario
            var itemsCarrito = await (
                from ac in _context.ArticulosCarritos
                join c in _context.Carritos on ac.IdCarrito equals c.IdCarrito
                join vp in _context.VariantesProductos on ac.IdProductoVariante equals vp.IdProductoVariante
                join p in _context.Productos on vp.IdProducto equals p.IdProducto
                where c.IdComprador == idUsuario
                select new
                {
                    p.TituloProducto,
                    ac.Cantidad,
                    ac.Precio,
                    ac.Moneda,
                    c.IdCarrito
                }
            ).ToListAsync();

            if (!itemsCarrito.Any())
                return BadRequest("El carrito está vacío, no hay nada que finalizar.");

            var idCarrito = itemsCarrito.First().IdCarrito;

            double total = itemsCarrito.Sum(i => i.Precio);

            var sb = new StringBuilder();
            sb.AppendLine("Hola,");
            sb.AppendLine();
            sb.AppendLine("Gracias por tu compra en MercaUca. Este es el resumen de tu pedido:");
            sb.AppendLine();

            foreach (var item in itemsCarrito)
            {
                sb.AppendLine(
                    $"- {item.TituloProducto}  x{item.Cantidad}  = {item.Precio:0.00} {item.Moneda}"
                );
            }

            sb.AppendLine();
            sb.AppendLine($"TOTAL: {total:0.00} {itemsCarrito.First().Moneda}");
            sb.AppendLine();
            sb.AppendLine("¡Gracias por tu preferencia!");
            sb.AppendLine("Equipo de MercaUca");

            string cuerpoCorreo = sb.ToString();
            //Correo generado con SmtpClient
            try
            {
                using (var clienteSmtp = new SmtpClient("smtp.gmail.com"))
                {
                    clienteSmtp.Port = 587;
                    clienteSmtp.EnableSsl = true;
                    clienteSmtp.UseDefaultCredentials = false;

                    // Correo y clave de aplicacion
                    clienteSmtp.Credentials = new NetworkCredential(
                        "fertica.prueba@gmail.com",
                        "pctf eaci omfj qlwf"
                    );

                    using (var correo = new MailMessage())
                    {
                        correo.From = new MailAddress("fertica.prueba@gmail.com", "MercaUca");
                        correo.To.Add(correoDestino);
                        correo.Subject = "Confirmación de compra - MercaUca";
                        correo.Body = cuerpoCorreo;
                        correo.IsBodyHtml = false; 

                        await clienteSmtp.SendMailAsync(correo);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"No se pudo enviar el correo: {ex.Message}");
            }

            var articulos = _context.ArticulosCarritos
                .Where(a => a.IdCarrito == idCarrito);

            _context.ArticulosCarritos.RemoveRange(articulos);

            var carrito = await _context.Carritos
                .FirstOrDefaultAsync(c => c.IdCarrito == idCarrito);

            if (carrito != null)
            {
                _context.Carritos.Remove(carrito);
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Compra finalizada. Se envió un correo de confirmación y se vació el carrito."
            });
        }
    }
}

