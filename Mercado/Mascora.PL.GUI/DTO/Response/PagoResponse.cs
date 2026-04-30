namespace Mercado.PL.GUI.DTO.Response
{
    public class PagoResponse
    {
        public long PagoID { get; set; }
        public string CodigoPago { get; set; }
        public decimal MontoPagado { get; set; }
        public string MedioPago { get; set; }
        public string? NumeroOperacion { get; set; }
        public string Estado { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime? FechaAnulacion { get; set; }
        public string? MotivoAnulacion { get; set; }

        public string CodigoDeuda { get; set; }
        public string? CodigoPuesto { get; set; }
        public string? Socio { get; set; }
        public string? Concepto { get; set; }
        public string? UsuarioRegistro { get; set; }
        public string? UsuarioAnulacion { get; set; }
    }
}
