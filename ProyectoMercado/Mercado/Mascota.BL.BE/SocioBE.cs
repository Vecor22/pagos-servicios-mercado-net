using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercado.BL.BE
{
    public class SocioBE
    {
        private long _socioID;
        private string _codigoSocio;
        private string _nombres;
        private string _apellidos;
        private string _dni;
        private string _correo;
        private string _telefono;
        private string _estado;
        private DateTime _fechaCreacion;
        private DateTime? _fechaActualizacion;

        public long SocioID { get => _socioID; set => _socioID = value; }
        public string CodigoSocio { get => _codigoSocio; set => _codigoSocio = value; }
        public string Nombres { get => _nombres; set => _nombres = value; }
        public string Apellidos { get => _apellidos; set => _apellidos = value; }
        public string Dni { get => _dni; set => _dni = value; }
        public string Correo { get => _correo; set => _correo = value; }
        public string Telefono { get => _telefono; set => _telefono = value; }
        public string Estado { get => _estado; set => _estado = value; }
        public DateTime FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public DateTime? FechaActualizacion { get => _fechaActualizacion; set => _fechaActualizacion = value; }
    }
}
