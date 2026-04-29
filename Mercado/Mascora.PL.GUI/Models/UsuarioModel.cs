using Mercado.BL.BC;
using Mercado.BL.BE;

namespace Mercado.PL.GUI.Models
{
    public class UsuarioModel
    {
        private readonly UsuarioBC usuarioBC = new UsuarioBC();

        public List<UsuarioBE> Listar()
        {
            return usuarioBC.Listar();
        }

        public UsuarioBE? BuscarPorId(long usuarioId)
        {
            return usuarioBC.BuscarPorId(usuarioId);
        }

        public UsuarioBE? BuscarPorUsername(string username)
        {
            return usuarioBC.BuscarPorUsername(username);
        }

        public UsuarioBE? Login(string username)
        {
            return usuarioBC.Login(username);
        }

        public void Crear(UsuarioBE usuario)
        {
            usuarioBC.Crear(usuario);
        }

        public void Actualizar(UsuarioBE usuario)
        {
            usuarioBC.Actualizar(usuario);
        }

        public void CambiarEstado(long usuarioId, string estado)
        {
            usuarioBC.CambiarEstado(usuarioId, estado);
        }

        public void CambiarPassword(long usuarioId, string passwordHash)
        {
            usuarioBC.CambiarPassword(usuarioId, passwordHash);
        }
    }
}
