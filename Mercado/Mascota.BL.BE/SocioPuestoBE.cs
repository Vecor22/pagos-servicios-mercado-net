using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercado.BL.BE
{
    public class SocioPuestoBE
    {
        private long _socioPuestoID;
        private SocioBE _socio;
        private PuestoBE _puesto;
        private UsuarioBE _asignadoPor;
        private DateTime _fechaAsignacion;

        public long SocioPuestoID { get => _socioPuestoID; set => _socioPuestoID = value; }
        public SocioBE Socio { get => _socio; set => _socio = value; }
        public PuestoBE Puesto { get => _puesto; set => _puesto = value; }
        public UsuarioBE AsignadoPor { get => _asignadoPor; set => _asignadoPor = value; }
        public DateTime FechaAsignacion { get => _fechaAsignacion; set => _fechaAsignacion = value; }
    }
}
