using GestionUsuarios.Application.DTOs;
using GestionUsuarios.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionUsuarios.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly IUsuarioService _credencialesService;


        public UsuarioController(IUsuarioService credencialesService)
        {
            _credencialesService = credencialesService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDto loginDto)
        {
            var token = await _credencialesService.ValidarCredencialesAsync(loginDto.NombreUsuario, loginDto.Pass);

            if (token == null)
                return Unauthorized("Usuario o contraseña incorrectos.");

            return Ok(new { Token = token });
        }
    }
}
