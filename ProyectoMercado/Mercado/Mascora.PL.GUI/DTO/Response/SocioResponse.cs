namespace Mercado.PL.GUI.DTO.Response
{
    public class SocioResponse
    {
        public long SocioID { get; set; }
        public string CodigoSocio { get; set; }
        public string NombreCompleto { get; set; }
        public string Dni { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
