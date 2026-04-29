namespace Mercado.SL.WebApi.DTO.Response
{
    public class LoginResponse
    {
        public long UsuarioID { get; set; }
        public string Username { get; set; }
        public string NombreCompleto { get; set; }
        public string Rol { get; set; }
    }
}
