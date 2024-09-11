using interfacesAPI.dtos.DtosEntrada;
using interfacesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace interfacesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly InterfacesContext _context;

        public UsuariosController(InterfacesContext context)
        {
            _context = context;
        }

        // GET: api/<UsuariosController>
        [HttpGet]
        public async Task <ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }
        

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var tuModelo = await _context.Usuarios.FindAsync(id);

            if (tuModelo == null)
            {
                return NotFound();
            }

            return tuModelo;
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(NuevoUsuario nuevoUsuario)
        {
            Usuario usuario = new Usuario
            {
                Email = nuevoUsuario.Email,
                Contraseña = nuevoUsuario.Contrasenia,
                Img = nuevoUsuario.Img
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuarios }, usuario);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] LoginDto loginDto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado");
            }

            if (usuario.Contraseña != loginDto.Password) // Para un mejor manejo de contraseñas, usa un método de hashing seguro
            {
                return Unauthorized("Contraseña incorrecta");
            }

            return Ok(usuario);
        }
    }
}
