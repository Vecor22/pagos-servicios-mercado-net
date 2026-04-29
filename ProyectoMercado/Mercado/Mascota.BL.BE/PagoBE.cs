using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercado.BL.BE
{
    public class PagoBE
    {
        private long _pagoID;
        private string _codigoPago;
        private decimal _montoPagado;
        private string _medioPago;
        private string _numeroOperacion;
        private string _estado;
        private DateTime _fechaPago;
        private DateTime? _fechaAnulacion;
        private string _motivoAnulacion;
        private DeudaBE _deuda;
        private UsuarioBE _registradoPor;
        private UsuarioBE _anuladoPor;

        public long PagoID { get => _pagoID; set => _pagoID = value; }
        public string CodigoPago { get => _codigoPago; set => _codigoPago = value; }
        public decimal MontoPagado { get => _montoPagado; set => _montoPagado = value; }
        public string MedioPago { get => _medioPago; set => _medioPago = value; }
        public string NumeroOperacion { get => _numeroOperacion; set => _numeroOperacion = value; }
        public string Estado { get => _estado; set => _estado = value; }
        public DateTime FechaPago { get => _fechaPago; set => _fechaPago = value; }
        public DateTime? FechaAnulacion { get => _fechaAnulacion; set => _fechaAnulacion = value; }
        public string MotivoAnulacion { get => _motivoAnulacion; set => _motivoAnulacion = value; }
        public DeudaBE Deuda { get => _deuda; set => _deuda = value; }
        public UsuarioBE RegistradoPor { get => _registradoPor; set => _registradoPor = value; }
        public UsuarioBE AnuladoPor { get => _anuladoPor; set => _anuladoPor = value; }
    }
}
