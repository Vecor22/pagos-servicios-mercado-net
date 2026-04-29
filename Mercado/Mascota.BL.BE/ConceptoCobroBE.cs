using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercado.BL.BE
{
    public class ConceptoCobroBE
    {
        private long _conceptoCobroID;
        private string _nombre;
        private string _descripcion;
        private string _tipoCobro;
        private DateTime _fechaCreacion;
        private DateTime? _fechaActualizacion;

        public long ConceptoCobroID { get => _conceptoCobroID; set => _conceptoCobroID = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public string TipoCobro { get => _tipoCobro; set => _tipoCobro = value; }
        public DateTime FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public DateTime? FechaActualizacion { get => _fechaActualizacion; set => _fechaActualizacion = value; }
    }
}
