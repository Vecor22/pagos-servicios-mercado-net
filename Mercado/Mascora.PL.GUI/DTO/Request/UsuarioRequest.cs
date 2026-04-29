namespace Mercado.PL.GUI.DTO.Request
{
    public class UsuarioRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string NombreCompleto { get; set; }
        public string? FotoUrl { get; set; }
        public long RolID { get; set; }
    }
}
