namespace Mercado.BL.BE
{
    public class PagoAlquilerBE
    {
        public int      IdPago      { get; set; }
        public int      IdPuesto    { get; set; }
        public string   CodigoPuesto{ get; set; } = string.Empty;
        public string   NombreSocio { get; set; } = string.Empty;
        public DateTime FechaPago   { get; set; }
        public decimal  Monto       { get; set; }
        public string   Mes         { get; set; } = string.Empty;
        public int      Anio        { get; set; }
        public string   Estado      { get; set; } = "Pagado";
    }
}
