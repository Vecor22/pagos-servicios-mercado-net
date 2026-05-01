namespace Mercado.PL.GUI.DTO.Request
{
    public class PagoAnularRequest
    {
        public long PagoID { get; set; }
        public string Motivo { get; set; }
        public long UsuarioID { get; set; }
    }
}
