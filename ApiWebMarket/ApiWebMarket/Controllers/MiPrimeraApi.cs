using ApiWebMarket.DTO;
using ApiWebMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiWebMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiPrimeraApi : ControllerBase
    {
        private readonly MercaUcaContext _context;

        public MiPrimeraApi(MercaUcaContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/nombre")]
        public async Task<IActionResult> GetNombreUsuario(string id)
        {
            var nombre = await _context.Usuarios
                                       .Where(u => u.IdUsuario == id)
                                       .Select(u => u.NombreUsuario)
                                       .FirstOrDefaultAsync();

            if (nombre == null)
                return NotFound();

            return Ok(new { Nombre = nombre });
        }

        [HttpGet("{id}/email")]
        public async Task<IActionResult> GetEmailUsuario(string id)
        {
            var email = await _context.Usuarios
                                      .Where(u => u.IdUsuario == id)
                                      .Select(u => u.EmailUsuario)
                                      .FirstOrDefaultAsync();

            if (email == null)
                return NotFound();

            return Ok(new { Email = email });
        }

        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet("productosSlider/{idUsuario}")]
        public async Task<IActionResult> GetProducto(string idUsuario)
        {
            var imagenes = await _context.Productos
       .Where(p => p.IdVendedor == idUsuario)
       .Select(p => new
       {
           FotoBase64 = Convert.ToBase64String(p.Foto)
       })
       .ToListAsync();

            if (imagenes == null || !imagenes.Any())
                return NotFound($"No se encontraron productos para el vendedor {idUsuario}");

            return Ok(imagenes);
        }

        // GET: api/productos/novedades
        [HttpGet("novedades")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetNovedades()
        {
            
            var raw = await (from p in _context.Productos
                             join pm in _context.ProductoMedia
                                 on p.IdProducto equals pm.IdProducto
                             where pm.Novedades == true
                             select new
                             {
                                 p.IdProducto,
                                 p.TituloProducto,
                                 p.Precio,   
                                 p.Foto     
                             })
                            .AsNoTracking()
                            .ToListAsync();

            // Conversión a Base64 en memoria (EF no traduce Convert.ToBase64String a SQL)
            var result = raw.Select(x => new Producto_card
            {
                IdProducto = x.IdProducto,
                TituloProducto = x.TituloProducto,
                Precio = Convert.ToDecimal(x.Precio),
                FotoBase64 = Convert.ToBase64String(x.Foto)
            }).ToList();

            return Ok(result);
        }

    }
    }
