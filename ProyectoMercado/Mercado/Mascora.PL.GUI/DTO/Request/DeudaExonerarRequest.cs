namespace Mercado.PL.GUI.DTO.Request
{
    public class DeudaExonerarRequest
    {
        public long DeudaID { get; set; }
        public string Motivo { get; set; }
        public long UsuarioID { get; set; }
    }
}
