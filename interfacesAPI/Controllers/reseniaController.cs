using interfacesAPI.dtos.DtosEntrada;
using interfacesAPI.Models;
using interfacesAPI.NewFolder.dtoSalida;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace interfacesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReseniaController : ControllerBase
    {
        private readonly InterfacesContext _context;

        public ReseniaController(InterfacesContext context)
        {
            _context = context;
        }
        // GET: api/<reseniaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReseniumDto>>> GetResenias()
        {
            var resenias = await _context.Resenia
                .Include(r => r.MangaNavigation)
                .Include(r => r.UsuarioNavigation)
                .Select(r => new ReseniumDto
                {
                    IdResenia = r.IdResenia,
                    Reseña = r.Reseña,
                    Likes = r.Likes,
                    Dislike = r.Dislike,
                    UsuarioId = r.Usuario,
                    MangaId = r.Manga,
                    Manga = r.MangaNavigation != null ? new MangaDto
                    {
                        IdManga = r.MangaNavigation.IdManga,
                        Nombre = r.MangaNavigation.Nombre
                    } : null,
                    Usuario = r.UsuarioNavigation != null ? new UsuarioDto
                    {
                        IdUsuarios = r.UsuarioNavigation.IdUsuarios,
                        Email = r.UsuarioNavigation.Email,
                        Img = r.UsuarioNavigation.Img
                    } : null
                })
                .ToListAsync();

            return Ok(resenias);
        }

        // GET: api/Resenium/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReseniumDto>> GetResenium(int id)
        {
            var resenium = await _context.Resenia
                .Include(r => r.MangaNavigation)
                .Include(r => r.UsuarioNavigation)
                .Select(r => new ReseniumDto
                {
                    IdResenia = r.IdResenia,
                    Reseña = r.Reseña,
                    Likes = r.Likes,
                    Dislike = r.Dislike,
                    UsuarioId = r.Usuario,
                    MangaId = r.Manga,
                    Manga = r.MangaNavigation != null ? new MangaDto
                    {
                        IdManga = r.MangaNavigation.IdManga,
                        Nombre = r.MangaNavigation.Nombre
                    } : null,
                    Usuario = r.UsuarioNavigation != null ? new UsuarioDto
                    {
                        IdUsuarios = r.UsuarioNavigation.IdUsuarios,
                        Email = r.UsuarioNavigation.Email,
                        Img = r.UsuarioNavigation.Img
                    } : null
                })
                .FirstOrDefaultAsync(r => r.IdResenia == id);

            if (resenium == null)
            {
                return NotFound();
            }

            return resenium;
        }

        // POST api/<reseniaController>
        [HttpPost]
        public async Task<ActionResult<Resenium>> PostResenium(CrearReseniaDTO crearReseniaDto)
        {
            // Verificar si el usuario y el manga existen
            var usuario = await _context.Usuarios.FindAsync(crearReseniaDto.Usuario);
            if (usuario == null)
            {
                return BadRequest("El usuario no existe.");
            }

            var manga = await _context.Mangas.FindAsync(crearReseniaDto.Manga);
            if (manga == null)
            {
                return BadRequest("El manga no existe.");
            }

            // Crear la nueva reseña
            var resenium = new Resenium
            {
                Reseña = crearReseniaDto.Resenia,
                Likes = crearReseniaDto.Likes,
                Dislike = crearReseniaDto.Dislike,
                Usuario = crearReseniaDto.Usuario,
                Manga = crearReseniaDto.Manga
            };

            _context.Resenia.Add(resenium);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResenium), new { id = resenium.IdResenia }, resenium);
        }



        //PUT api/<reseniaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //// DELETE api/<reseniaController>/5
        ///        //[HttpDelete("{id}")]
        ///               //public void Delete(int id)
        ///                      //{
        ///                             //}
    }
}
