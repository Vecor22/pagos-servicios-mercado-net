using Mercado.BL.BE;
using Mercado.DL.DALC;

namespace Mercado.BL.BC
{
    public class UsuarioBC
    {
        private readonly UsuarioDALC usuarioDALC = new UsuarioDALC();

        public List<UsuarioBE> Listar()
        {
            return usuarioDALC.Listar();
        }

        public UsuarioBE? BuscarPorId(long usuarioId)
        {
            return usuarioDALC.BuscarPorId(usuarioId);
        }

        public UsuarioBE? BuscarPorUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("El username es obligatorio.");

            return usuarioDALC.BuscarPorUsername(username);
        }

        public UsuarioBE? Login(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("El username es obligatorio.");

            return usuarioDALC.Login(username);
        }

        public void Crear(UsuarioBE usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Username))
                throw new Exception("El username es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.PasswordHash))
                throw new Exception("La contraseña es obligatoria.");

            if (string.IsNullOrWhiteSpace(usuario.NombreCompleto))
                throw new Exception("El nombre completo es obligatorio.");

            if (usuario.Rol == null || usuario.Rol.RolID <= 0)
                throw new Exception("Debe asignar un rol válido.");

            usuarioDALC.Crear(usuario);
        }

        public void Actualizar(UsuarioBE usuario)
        {
            if (usuario.UsuarioID <= 0)
                throw new Exception("Usuario inválido.");

            if (string.IsNullOrWhiteSpace(usuario.NombreCompleto))
                throw new Exception("El nombre completo es obligatorio.");

            if (usuario.Rol == null || usuario.Rol.RolID <= 0)
                throw new Exception("Debe asignar un rol válido.");

            usuarioDALC.Actualizar(usuario);
        }

        public void CambiarEstado(long usuarioId, string estado)
        {
            if (usuarioId <= 0)
                throw new Exception("Usuario inválido.");

            if (string.IsNullOrWhiteSpace(estado))
                throw new Exception("Estado inválido.");

            usuarioDALC.CambiarEstado(usuarioId, estado);
        }

        public void CambiarPassword(long usuarioId, string passwordHash)
        {
            if (usuarioId <= 0)
                throw new Exception("Usuario inválido.");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new Exception("La contraseña es obligatoria.");

            usuarioDALC.CambiarPassword(usuarioId, passwordHash);
        }
    }
}
