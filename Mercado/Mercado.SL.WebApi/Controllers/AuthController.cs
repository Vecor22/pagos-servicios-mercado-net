using Mercado.BL.BC;
using Mercado.SL.WebApi.DTO.Request;
using Mercado.SL.WebApi.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Mercado.SL.WebApi.Helpers;

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
                {
                    var usuarioExistente = usuarioBC.BuscarPorUsername(request.Username);

                    if (usuarioExistente != null &&
                        string.Equals(usuarioExistente.Estado, "INACTIVO", StringComparison.OrdinalIgnoreCase))
                    {
                        return Unauthorized("Este usuario ha sido inactivado, por lo que deberá comunicarse con el administrador.");
                    }

                    return Unauthorized("Usuario no encontrado.");
                }

                if (string.Equals(usuario.Estado, "INACTIVO", StringComparison.OrdinalIgnoreCase))
                {
                    return Unauthorized("Este usuario ha sido inactivado, por lo que deberá comunicarse con el administrador.");
                }

                var hash = PasswordHelper.HashPassword(request.Password);

                if (usuario.PasswordHash != hash)
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
