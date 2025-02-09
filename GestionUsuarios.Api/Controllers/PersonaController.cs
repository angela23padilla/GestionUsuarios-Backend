using Azure;
using GestionUsuarios.Application.DTOs;
using GestionUsuarios.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionUsuarios.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonaController : ControllerBase
    {

        public readonly IPersonaService _personaService;
        private readonly ILogger<PersonaController> _logger;


        public PersonaController(IPersonaService personaService, ILogger<PersonaController> logger)
        {
            _personaService = personaService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerPersonas(
         [FromQuery] int page = 1,
         [FromQuery] int size = 10,
         [FromQuery] string sortBy = "name",
         [FromQuery] string search = "")
        {
            _logger.LogInformation("Metodo para obtener personas");

            if (page < 1 || size < 1)
                return BadRequest("Page and size must be greater than 0.");

            var personas = await _personaService.ObtenerPersonasPaginadosAsync(page, size, sortBy, search);
            return Ok(personas);
        }

       

        [HttpPost]
        public async Task<IActionResult> AgregarPersonas([FromBody] PersonaDto persona)
        {
            _logger.LogInformation("Metodo para agregar personas");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productId = await _personaService.AgregarPersonasAsync(persona);

            persona.Id = productId;

            return CreatedAtAction(nameof(ObtenerPersonasID), new { id = productId }, persona);
        }


        [HttpGet("{id?}")]
        public async Task<IActionResult> ObtenerPersonasID( int id=0)
        {
            _logger.LogInformation("Metodo para obtener productos por ID");
            var product = await _personaService.ObtenerPersonasAsync(id);
            if (product == null)
                return NotFound($"No se encontró una persona con ID {id}.");

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarPersona(int id, [FromBody] PersonaDto persona)
        {
            _logger.LogInformation("Metodo para actualizar personas");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _personaService.ModificarPersonaAsync(id, persona);
            if (!updated)
                return NotFound($"No se encontró una persona con ID {id} para actualizar.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            _logger.LogInformation("Metodo para eliminar personas");
            var deleted = await _personaService.EliminarPersonaAsync(id);
            if (!deleted)
                return NotFound($"No se encontró una persona con ID {id} para eliminar.");

            return NoContent();
        }
    }
}
