using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercado.BL.BE
{
    public class UsuarioBE
    {
        private long _usuarioID;
        private string _username;
        private string _passwordHash;
        private string _nombreCompleto;
        private string _fotoUrl;
        private string _estado;
        private DateTime _fechaCreacion;
        private DateTime? _fechaActualizacion;
        private RolBE _rol;

        public long UsuarioID { get => _usuarioID; set => _usuarioID = value; }
        public string Username { get => _username; set => _username = value; }
        public string PasswordHash { get => _passwordHash; set => _passwordHash = value; }
        public string NombreCompleto { get => _nombreCompleto; set => _nombreCompleto = value; }
        public string FotoUrl { get => _fotoUrl; set => _fotoUrl = value; }
        public string Estado { get => _estado; set => _estado = value; }
        public DateTime FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public DateTime? FechaActualizacion { get => _fechaActualizacion; set => _fechaActualizacion = value; }
        public RolBE Rol { get => _rol; set => _rol = value; }
    }
}
