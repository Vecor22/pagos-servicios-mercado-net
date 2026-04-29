namespace Mercado.PL.GUI.DTO.Request
{
    public class DeudaRequest
    {
        public long ConceptoCobroID { get; set; }
        public string? CodigoPuesto { get; set; } // solo si es INDIVIDUAL
        public decimal Monto { get; set; }
        public string TipoGeneracion { get; set; } // INDIVIDUAL | REPARTIBLE
        public string? Observacion { get; set; }
        public long UsuarioID { get; set; }
    }
}
