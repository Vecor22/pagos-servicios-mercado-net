namespace Mercado.PL.GUI.DTO.Request
{
    public class UsuarioCambioPasswordRequest
    {
        public long UsuarioID { get; set; }
        public string PasswordActual { get; set; }
        public string PasswordNuevo { get; set; }
    }
}
