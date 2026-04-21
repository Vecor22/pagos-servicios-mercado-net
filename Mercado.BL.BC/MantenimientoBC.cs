using Mercado.BL.BE;
using Mercado.DL.DALC;

namespace Mercado.BL.BC
{
    public class MantenimientoBC
    {
        MantenimientoDALC dal = new();

        public List<MantenimientoBE> listar()                => dal.listar();
        public MantenimientoBE?      buscar(int id)           => dal.buscar(id);
        public int                   insertar(MantenimientoBE m) => dal.insertar(m);
        public bool                  actualizar(MantenimientoBE m) => dal.actualizar(m);
        public bool                  eliminar(int id)         => dal.eliminar(id);
        public List<MantenimientoBE> listarPorAnio(int anio)  => dal.listarPorAnio(anio);
    }
}
