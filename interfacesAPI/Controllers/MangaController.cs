using interfacesAPI.dtos.DtosEntrada;
using interfacesAPI.dtos.DtosSalida;
using interfacesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace interfacesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangaController: ControllerBase
    {

        private readonly InterfacesContext _context;

        public MangaController(InterfacesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MangasLDto>> GetMangasConGeneros()
        {
            var mangas = _context.Mangas
                .Select(m => new MangasLDto
                {
                    IdManga = m.IdManga,
                    Nombre = m.Nombre,
                    Portada = m.Portada,
                    Tomo = m.Tomo,
                    Estado = m.Estado,
                    AnioSalida = m.AñoSalida,
                    Sinopsis = m.Sinopsis,
                    Likes = m.Likes,
                    Dislikes = m.Dislikes,
               
                })
                .ToList();

            return mangas;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Manga>> GetManga(int id)
        {
            var tuModelo = await _context.Mangas.FindAsync(id);

            if (tuModelo == null)
            {
                return NotFound();
            }

            return tuModelo;
        }

        [HttpGet("info/{id}")]
        public ActionResult<MangasLDto> GetMangaById(int id)
        {
            var manga = _context.Mangas
                .Include(m => m.MangaGeneros)
                .Where(m => m.IdManga == id)
                .Select(m => new MangasLDto
                {
                    IdManga = m.IdManga,
                    Nombre = m.Nombre,
                    Portada = m.Portada,
                    Tomo = m.Tomo,
                    Estado = m.Estado,
                    AnioSalida = m.AñoSalida,
                    Sinopsis = m.Sinopsis,
                    Likes = m.Likes,
                    Dislikes = m.Dislikes,
                })
                .FirstOrDefault();

            if (manga == null)
            {
                return NotFound();
            }

            return manga;
        }

        [HttpPost]
        public async Task<ActionResult<Manga>> PostManga(NuevoManga nuevoManga)
        {
            Manga manga = new Manga
            {
                Nombre = nuevoManga.Nombre,
                Portada = nuevoManga.Portada,
                Tomo = nuevoManga.Tomo,
                Estado = nuevoManga.Estado,
                AñoSalida = nuevoManga.AnioSalida,
                Sinopsis = nuevoManga.Sinopsis,
                Likes = nuevoManga.Likes,
                Dislikes = nuevoManga.Dislikes
            };

            _context.Mangas.Add(manga);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetManga), new { id = manga.IdManga }, manga);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<MangasLDto>>> GetMangaByName(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return BadRequest("El parámetro nombre está vacío.");
            }

            var mangas = await _context.Mangas
                .Include(m => m.MangaGeneros)
                .Where(m => m.Nombre.Contains(nombre))
                .Select(m => new MangasLDto
                {
                    IdManga = m.IdManga,
                    Nombre = m.Nombre,
                    Portada = m.Portada,
                    Tomo = m.Tomo,
                    Estado = m.Estado,
                    AnioSalida = m.AñoSalida,
                    Sinopsis = m.Sinopsis,
                    Likes = m.Likes,
                    Dislikes = m.Dislikes,
                })
                .ToListAsync();

            if (mangas == null || !mangas.Any())
            {
                return NotFound($"No se ha encontrado el manga con nombre '{nombre}'.");
            }

            return mangas;
        }



        [HttpGet("{id}/resenias")]
        public async Task<ActionResult<IEnumerable<ReseniaConUsuarioDto>>> GetMangaResenias(int id)
        {
            var manga = await _context.Mangas
                .Include(m => m.Resenia)
                .ThenInclude(r => r.UsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdManga == id);

            if (manga == null)
            {
                return NotFound();
            }

            var reseniasConUsuarios = manga.Resenia.Select(r => new ReseniaConUsuarioDto
            {
                IdResenia = r.IdResenia,
                Resenia = r.Reseña,
                Likes = r.Likes ?? 0,
                Dislike = r.Dislike ?? 0,
                UsuarioId = r.UsuarioNavigation.IdUsuarios,
                UsuarioEmail = r.UsuarioNavigation.Email,
                UsuarioImg = r.UsuarioNavigation.Img
            }).ToList();

            return Ok(reseniasConUsuarios);
        }

        [HttpGet("{id}/generos")]
        public async Task<ActionResult<IEnumerable<GenerosDto>>> GetMangaGeneros(int id)
        {
            var manga = await _context.Mangas
                .Include(m => m.MangaGeneros)
                .ThenInclude(mg => mg.GeneroNavigation)
                .FirstOrDefaultAsync(m => m.IdManga == id);

            if (manga == null)
            {
                return NotFound();
            }

            var generos = manga.MangaGeneros.Select(mg => new GenerosDto
            {
                IdGenero = mg.GeneroNavigation.IdGenero,
                Generos = mg.GeneroNavigation.Genero1
            }).ToList();

            return Ok(generos);
        }
    }
}
