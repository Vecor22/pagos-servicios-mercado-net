using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercado.BL.BE
{
    public class PuestoBE
    {
        private long _puestoID;
        private string _codigoPuesto;
        private DateTime _fechaCreacion;

        public long PuestoID { get => _puestoID; set => _puestoID = value; }
        public string CodigoPuesto { get => _codigoPuesto; set => _codigoPuesto = value; }
        public DateTime FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
    }
}
