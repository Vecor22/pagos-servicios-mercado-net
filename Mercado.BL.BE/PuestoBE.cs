namespace Mercado.BL.BE
{
    public class PuestoBE
    {
        public int     IdPuesto    { get; set; }
        public string  Codigo      { get; set; }
        public string  Sector      { get; set; }
        public SocioBE Socio       { get; set; }
        public string  NombreSocio { get; set; }
    }
}
