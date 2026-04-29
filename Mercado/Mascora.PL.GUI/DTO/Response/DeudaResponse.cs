namespace Mercado.PL.GUI.DTO.Response
{
    public class DeudaResponse
    {
        public long DeudaID { get; set; }
        public string CodigoDeuda { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        public string TipoGeneracion { get; set; }
        public string? Observacion { get; set; }
        public DateTime FechaGeneracion { get; set; }

        public string Concepto { get; set; }
        public string? CodigoPuesto { get; set; }
        public string? Socio { get; set; }

        public DateTime? FechaExoneracion { get; set; }
        public string? MotivoExoneracion { get; set; }
    }
}
