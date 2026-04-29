using Mercado.BL.BE;
using Mercado.DL.DALC;

namespace Mercado.BL.BC
{
    public class PuestoBC
    {
        private readonly PuestoDALC puestoDALC = new PuestoDALC();

        public List<PuestoBE> Listar()
        {
            return puestoDALC.Listar();
        }

        public PuestoBE? BuscarPorId(long puestoId)
        {
            return puestoDALC.BuscarPorId(puestoId);
        }

        public PuestoBE? BuscarPorCodigo(string codigoPuesto)
        {
            if (string.IsNullOrWhiteSpace(codigoPuesto))
                throw new Exception("El código del puesto es obligatorio.");

            return puestoDALC.BuscarPorCodigo(codigoPuesto);
        }

        public void Crear()
        {
            puestoDALC.Crear();
        }
    }
}
