namespace Mercado.PL.GUI.DTO.Response
{
    public class ComprobanteResponse
    {
        public long ComprobanteID { get; set; }
        public string NumeroComprobante { get; set; }
        public string TipoComprobante { get; set; }
        public string Estado { get; set; }
        public DateTime FechaEmision { get; set; }

        public DateTime? FechaAnulacion { get; set; }
        public string? MotivoAnulacion { get; set; }

        public string CodigoPago { get; set; }
        public decimal MontoPagado { get; set; }
        public string MedioPago { get; set; }
        public string? NumeroOperacion { get; set; }
        public DateTime FechaPago { get; set; }
        public string CodigoDeuda { get; set; }
        public string? CodigoPuesto { get; set; }
        public string? Socio { get; set; }
        public string? RegistradoPor { get; set; }
        public string? AnuladoPor { get; set; }
    }
}
