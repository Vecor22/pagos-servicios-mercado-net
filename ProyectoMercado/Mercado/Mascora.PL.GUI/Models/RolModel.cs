using Mercado.BL.BC;
using Mercado.BL.BE;

namespace Mercado.PL.GUI.Models
{
    public class RolModel
    {
        private readonly RolBC rolBC = new RolBC();

        public List<RolBE> Listar()
        {
            return rolBC.Listar();
        }

        public RolBE? BuscarPorId(long rolId)
        {
            return rolBC.BuscarPorId(rolId);
        }

        public void Crear(RolBE rol)
        {
            rolBC.Crear(rol);
        }

        public void Actualizar(RolBE rol)
        {
            rolBC.Actualizar(rol);
        }
    }
}
