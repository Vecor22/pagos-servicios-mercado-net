using Mercado.BL.BC;
using Mercado.BL.BE;

namespace Mercado.PL.GUI.Models
{
    public class PuestoModel
    {
        private readonly PuestoBC puestoBC = new PuestoBC();

        public List<PuestoBE> Listar()
        {
            return puestoBC.Listar();
        }

        public PuestoBE? BuscarPorId(long puestoId)
        {
            return puestoBC.BuscarPorId(puestoId);
        }

        public PuestoBE? BuscarPorCodigo(string codigoPuesto)
        {
            return puestoBC.BuscarPorCodigo(codigoPuesto);
        }

        public void Crear()
        {
            puestoBC.Crear();
        }
    }
}
