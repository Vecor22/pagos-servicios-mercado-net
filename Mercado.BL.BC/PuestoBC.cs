using Mercado.BL.BE;
using Mercado.DL.DALC;

namespace Mercado.BL.BC
{
    public class PuestoBC
    {
        PuestoDALC puestoDALC = new PuestoDALC();

        public List<PuestoBE> listar()
        {
            return puestoDALC.listar();
        }

        public PuestoBE buscar(int idPuesto)
        {
            return puestoDALC.buscar(idPuesto);
        }

        public int insertar(PuestoBE puestoBE)
        {
            return puestoDALC.insertar(puestoBE);
        }

        public bool actualizar(PuestoBE puestoBE)
        {
            return puestoDALC.actualizar(puestoBE);
        }

        public bool eliminar(int idPuesto)
        {
            return puestoDALC.eliminar(idPuesto);
        }
    }
}
