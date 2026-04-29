using Mercado.BL.BE;
using Mercado.DL.DALC;
using System.Data;

namespace Mercado.BL.BC
{
    public class RolBC
    {
        private readonly RolDALC rolDALC = new RolDALC();

        public List<RolBE> Listar()
        {
            return rolDALC.Listar();
        }

        public RolBE? BuscarPorId(long rolId)
        {
            return rolDALC.BuscarPorId(rolId);
        }

        public void Crear(RolBE rol)
        {
            if (string.IsNullOrWhiteSpace(rol.Nombre))
                throw new Exception("El nombre del rol es obligatorio.");

            rolDALC.Crear(rol);
        }

        public void Actualizar(RolBE rol)
        {
            if (rol.RolID <= 0)
                throw new Exception("Rol inválido.");

            if (string.IsNullOrWhiteSpace(rol.Nombre))
                throw new Exception("El nombre del rol es obligatorio.");

            rolDALC.Actualizar(rol);
        }
    }
}
