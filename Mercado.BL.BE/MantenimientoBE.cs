namespace Mercado.BL.BE
{
    public class MantenimientoBE
    {
        public int      IdMantenimiento { get; set; }
        public string   Tipo            { get; set; } = string.Empty; // Luz, Agua, Vigilancia, Limpieza, Otro
        public string   Descripcion     { get; set; } = string.Empty;
        public DateTime Fecha           { get; set; }
        public decimal  Monto           { get; set; }
        public string   Proveedor       { get; set; } = string.Empty;
        public string   Mes             { get; set; } = string.Empty;
        public int      Anio            { get; set; }
    }
}
