using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercado.BL.BE
{
    public class DeudaBE
    {
        private long _deudaID;
        private string _codigoDeuda;
        private decimal _monto;
        private string _estado;
        private string _tipoGeneracion;
        private string _observacion;
        private DateTime _fechaGeneracion;
        private DateTime? _fechaExoneracion;
        private string _motivoExoneracion;
        private ConceptoCobroBE _conceptoCobro;
        private PuestoBE _puesto;
        private SocioBE _socio;
        private DeudaBE _deudaOrigen;
        private UsuarioBE _creadoPor;
        private UsuarioBE _exoneradoPor;

        public long DeudaID { get => _deudaID; set => _deudaID = value; }
        public string CodigoDeuda { get => _codigoDeuda; set => _codigoDeuda = value; }
        public decimal Monto { get => _monto; set => _monto = value; }
        public string Estado { get => _estado; set => _estado = value; }
        public string TipoGeneracion { get => _tipoGeneracion; set => _tipoGeneracion = value; }
        public string Observacion { get => _observacion; set => _observacion = value; }
        public DateTime FechaGeneracion { get => _fechaGeneracion; set => _fechaGeneracion = value; }
        public DateTime? FechaExoneracion { get => _fechaExoneracion; set => _fechaExoneracion = value; }
        public string MotivoExoneracion { get => _motivoExoneracion; set => _motivoExoneracion = value; }
        public ConceptoCobroBE ConceptoCobro { get => _conceptoCobro; set => _conceptoCobro = value; }
        public PuestoBE Puesto { get => _puesto; set => _puesto = value; }
        public SocioBE Socio { get => _socio; set => _socio = value; }
        public DeudaBE DeudaOrigen { get => _deudaOrigen; set => _deudaOrigen = value; }
        public UsuarioBE CreadoPor { get => _creadoPor; set => _creadoPor = value; }
        public UsuarioBE ExoneradoPor { get => _exoneradoPor; set => _exoneradoPor = value; }
    }
}
