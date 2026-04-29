using Mercado.BL.BC;
using Mercado.SL.WebApi.DTO.Request;
using Mercado.SL.WebApi.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace Mercado.SL.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioBC usuarioBC = new UsuarioBC();

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var usuario = usuarioBC.Login(request.Username);

                if (usuario == null)
                    return Unauthorized("Usuario no encontrado.");

                // Aquí deberías validar el password (hash)
                if (usuario.PasswordHash != request.Password)
                    return Unauthorized("Credenciales incorrectas.");

                var response = new LoginResponse
                {
                    UsuarioID = usuario.UsuarioID,
                    Username = usuario.Username,
                    NombreCompleto = usuario.NombreCompleto,
                    Rol = usuario.Rol.Nombre
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
