using interfacesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace interfacesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {

        private readonly InterfacesContext _context;

        public GeneroController(InterfacesContext context)
        {
            _context = context;
        }
        // GET: api/<GeneroiController>
        [HttpGet]
        public async Task <ActionResult<IEnumerable<Genero>>> GetGeneros()
        {
            return await _context.Generos.ToListAsync();
        }

        // GET api/<GeneroiController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> GetGenero(int id)
        {
            var tuModelo = await _context.Generos.FindAsync(id);

            if (tuModelo == null)
            {
                return NotFound();
            }

            return tuModelo;
        }

        // POST api/<GeneroiController>
        [HttpPost]
        public async Task<ActionResult<Genero>> PostGenero(Genero genero)
        {
            _context.Generos.Add(genero);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGenero), new { id = genero.IdGenero }, genero);
        }

        // PUT api/<GeneroiController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<GeneroiController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
