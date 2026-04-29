namespace Mercado.PL.GUI.DTO.Response
{
    public class UsuarioResponse
    {
        public long UsuarioID { get; set; }
        public string Username { get; set; }
        public string NombreCompleto { get; set; }
        public string? FotoUrl { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public string Rol { get; set; }
    }
}
