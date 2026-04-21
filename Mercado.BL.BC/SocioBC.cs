using Mercado.BL.BE;
using Mercado.DL.DALC;

namespace Mercado.BL.BC
{
    public class SocioBC
    {
        SocioDALC socioDALC = new SocioDALC();

        public List<SocioBE> listar()
        {
            return socioDALC.listar();
        }

        public SocioBE buscar(int idSocio)
        {
            return socioDALC.buscar(idSocio);
        }

        public int insertar(SocioBE socioBE)
        {
            return socioDALC.insertar(socioBE);
        }

        public bool actualizar(SocioBE socioBE)
        {
            return socioDALC.actualizar(socioBE);
        }

        public bool eliminar(int idSocio)
        {
            return socioDALC.eliminar(idSocio);
        }
    }
}
