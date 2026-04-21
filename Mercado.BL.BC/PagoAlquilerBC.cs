using Mercado.BL.BE;
using Mercado.DL.DALC;

namespace Mercado.BL.BC
{
    public class PagoAlquilerBC
    {
        PagoAlquilerDALC dal = new();

        public List<PagoAlquilerBE> listar()              => dal.listar();
        public PagoAlquilerBE?      buscar(int id)         => dal.buscar(id);
        public int                  insertar(PagoAlquilerBE p) => dal.insertar(p);
        public bool                 actualizar(PagoAlquilerBE p) => dal.actualizar(p);
        public bool                 eliminar(int id)       => dal.eliminar(id);
        public List<PagoAlquilerBE> listarPorAnio(int anio) => dal.listarPorAnio(anio);
    }
}
