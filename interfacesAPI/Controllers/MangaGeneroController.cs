using interfacesAPI.dtos.DtosSalida;
using interfacesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace interfacesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangaGeneroController: ControllerBase
    {
        private readonly InterfacesContext _context;

        public MangaGeneroController(InterfacesContext context)
        {
            _context = context;
        }

        // GET: api/MangaGenero
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MangaGeneroDto>>> GetMangaGeneros()
        {
            var mangaGeneros = await _context.MangaGeneros
                .Include(mg => mg.GeneroNavigation)
                .Include(mg => mg.MangaNavigation)
                .Select(mg => new MangaGeneroDto
                {
                    GeneroId = mg.Genero,
                    MangaId = mg.Manga,
                    Genero = mg.GeneroNavigation != null ? new GeneroDto
                    {
                        IdGenero = mg.GeneroNavigation.IdGenero,
                        Genero1 = mg.GeneroNavigation.Genero1
                    } : null,
                    Manga = mg.MangaNavigation != null ? new MangaByGeneroDto
                    {
                        IdManga = mg.MangaNavigation.IdManga,
                        Nombre = mg.MangaNavigation.Nombre
                    } : null
                })
                .ToListAsync();

            return Ok(mangaGeneros);
        }
        // GET: api/MangaGenero/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MangaGeneroDto>> GetMangaGenero(int id)
        {
            var mangaGenero = await _context.MangaGeneros
                .Include(mg => mg.GeneroNavigation)
                .Include(mg => mg.MangaNavigation)
                .Select(mg => new MangaGeneroDto
                {
                    GeneroId = mg.Genero,
                    MangaId = mg.Manga,
                    Genero = mg.GeneroNavigation != null ? new GeneroDto
                    {
                        IdGenero = mg.GeneroNavigation.IdGenero,
                        Genero1 = mg.GeneroNavigation.Genero1
                    } : null,
                    Manga = mg.MangaNavigation != null ? new MangaByGeneroDto
                    {
                        IdManga = mg.MangaNavigation.IdManga,
                        Nombre = mg.MangaNavigation.Nombre
                    } : null
                })
                .FirstOrDefaultAsync(mg => mg.MangaId == id);

            if (mangaGenero == null)
            {
                return NotFound();
            }

            return mangaGenero;
        }

        [HttpPost]
        public async Task<ActionResult<MangaGeneroDto>> PostMangaGenero([FromBody] MangaGeneroDto mangaGeneroDto)
        {
            if (mangaGeneroDto == null)
            {
                return BadRequest("MangaGeneroDto is null.");
            }

            var mangaGenero = new MangaGenero
            {
                Genero = mangaGeneroDto.GeneroId,
                Manga = mangaGeneroDto.MangaId
            };

            _context.MangaGeneros.Add(mangaGenero);
            await _context.SaveChangesAsync();

            mangaGeneroDto.GeneroId = mangaGenero.Genero;
            mangaGeneroDto.MangaId = mangaGenero.Manga;

            return CreatedAtAction(nameof(GetMangaGenero), new { id = mangaGenero.IdMangagenero }, mangaGeneroDto);
        }
    }
}
