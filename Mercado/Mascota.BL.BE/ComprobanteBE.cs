using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercado.BL.BE
{
    public class ComprobanteBE
    {
        private long _comprobanteID;
        private string _numeroComprobante;
        private string _tipoComprobante;
        private string _estado;
        private DateTime _fechaEmision;
        private DateTime? _fechaAnulacion;
        private string _motivoAnulacion;
        private PagoBE _pago;
        private UsuarioBE _anuladoPor;

        public long ComprobanteID { get => _comprobanteID; set => _comprobanteID = value; }
        public string NumeroComprobante { get => _numeroComprobante; set => _numeroComprobante = value; }
        public string TipoComprobante { get => _tipoComprobante; set => _tipoComprobante = value; }
        public string Estado { get => _estado; set => _estado = value; }
        public DateTime FechaEmision { get => _fechaEmision; set => _fechaEmision = value; }
        public DateTime? FechaAnulacion { get => _fechaAnulacion; set => _fechaAnulacion = value; }
        public string MotivoAnulacion { get => _motivoAnulacion; set => _motivoAnulacion = value; }
        public PagoBE Pago { get => _pago; set => _pago = value; }
        public UsuarioBE AnuladoPor { get => _anuladoPor; set => _anuladoPor = value; }
    }
}
