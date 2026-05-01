namespace Mercado.PL.GUI.DTO.Response
{
    public class ConceptoCobroResponse
    {
        public long ConceptoCobroID { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string TipoCobro { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
