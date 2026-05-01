namespace Mercado.PL.GUI.DTO.Response
{
    public class ReporteResumenDeudasResponse
    {
        public decimal TotalPagableMonto { get; set; }
        public int TotalPagableCantidad { get; set; }
        public decimal TotalPagablePorcentaje { get; set; }
        public decimal PendienteMonto { get; set; }
        public int PendienteCantidad { get; set; }
        public decimal PendientePorcentaje { get; set; }
        public decimal PagadaMonto { get; set; }
        public int PagadaCantidad { get; set; }
        public decimal PagadaPorcentaje { get; set; }
        public int ExoneradaCantidad { get; set; }
        public int DistribuidaCantidad { get; set; }
    }
}
