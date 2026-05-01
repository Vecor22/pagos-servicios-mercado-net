using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercado.BL.BE
{
    public class RolBE
    {
        private long _rolID;
        private string _nombre;
        private string _descripcion;
        private bool _activo;
        private DateTime _fechaCreacion;
        private DateTime? _fechaActualizacion;

        public long RolID { get => _rolID; set => _rolID = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public bool Activo { get => _activo; set => _activo = value; }
        public DateTime FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public DateTime? FechaActualizacion { get => _fechaActualizacion; set => _fechaActualizacion = value; }
    }
}
