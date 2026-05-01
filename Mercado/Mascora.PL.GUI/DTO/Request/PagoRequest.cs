namespace Mercado.PL.GUI.DTO.Request
{
    public class PagoRequest
    {
        public string CodigoDeuda { get; set; }
        public string MedioPago { get; set; }
        public string? NumeroOperacion { get; set; }
        public long UsuarioID { get; set; }
    }
}
