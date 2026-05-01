using Mercado.BL.BC;
using Mercado.BL.BE;

namespace Mercado.PL.GUI.Models
{
    public class DeudaModel
    {
        private readonly DeudaBC deudaBC = new DeudaBC();

        public List<DeudaBE> Listar()
        {
            return deudaBC.Listar();
        }

        public DeudaBE? BuscarPorId(long deudaId)
        {
            return deudaBC.BuscarPorId(deudaId);
        }

        public DeudaBE? BuscarPorCodigo(string codigoDeuda)
        {
            return deudaBC.BuscarPorCodigo(codigoDeuda);
        }

        public List<DeudaBE> ListarPorEstado(string estado)
        {
            return deudaBC.ListarPorEstado(estado);
        }

        public List<DeudaBE> ListarPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            return deudaBC.ListarPorFecha(fechaInicio, fechaFin);
        }

        public List<DeudaBE> ListarPorCodigoPuesto(string codigoPuesto)
        {
            return deudaBC.ListarPorCodigoPuesto(codigoPuesto);
        }

        public void Crear(DeudaBE deuda)
        {
            deudaBC.Crear(deuda);
        }

        public void Exonerar(long deudaId, string motivo, long usuarioId)
        {
            deudaBC.Exonerar(deudaId, motivo, usuarioId);
        }
    }
}
