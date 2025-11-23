using ApiWebMarket.DTO;
using ApiWebMarket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiWebMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly MercaUcaContext _context;

        public SearchController(MercaUcaContext context)
        {
            _context = context;
        }

        // GET: api/Search/buscar?query=p
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<ProductoBusquedaDto>>> BuscarProductos([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Debe ingresar un término de búsqueda.");

            string q = query.ToLower();

            var productos = await _context.Productos
                .Where(p => p.TituloProducto.ToLower().StartsWith(q))   //  🔥 Case-insensitive
                .Select(p => new ProductoBusquedaDto
                {
                    IdProducto = p.IdProducto,
                    TituloProducto = p.TituloProducto,
                    Precio = p.Precio,

                    FotoBase64 = p.Foto != null
                        ? "data:image/jpeg;base64," + Convert.ToBase64String(p.Foto)
                        : null
                })
                .ToListAsync();

            return Ok(productos);
        }
    }
}
