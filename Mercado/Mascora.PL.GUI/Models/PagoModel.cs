using Mercado.BL.BC;
using Mercado.BL.BE;

namespace Mercado.PL.GUI.Models
{
    public class PagoModel
    {
        private readonly PagoBC pagoBC = new PagoBC();

        public List<PagoBE> Listar()
        {
            return pagoBC.Listar();
        }

        public PagoBE? BuscarPorId(long pagoId)
        {
            return pagoBC.BuscarPorId(pagoId);
        }

        public PagoBE? BuscarPorCodigo(string codigoPago)
        {
            return pagoBC.BuscarPorCodigo(codigoPago);
        }

        public PagoBE? BuscarPorCodigoDeuda(string codigoDeuda)
        {
            return pagoBC.BuscarPorCodigoDeuda(codigoDeuda);
        }

        public List<PagoBE> ListarPorCodigoPuesto(string codigoPuesto)
        {
            return pagoBC.ListarPorCodigoPuesto(codigoPuesto);
        }

        public List<PagoBE> ListarPorFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return pagoBC.ListarPorFechas(fechaInicio, fechaFin);
        }

        public void Crear(string codigoDeuda, string medioPago, string? numeroOperacion, long usuarioId)
        {
            pagoBC.Crear(codigoDeuda, medioPago, numeroOperacion, usuarioId);
        }

        public void Anular(long pagoId, string motivo, long usuarioId)
        {
            pagoBC.Anular(pagoId, motivo, usuarioId);
        }
    }
}
