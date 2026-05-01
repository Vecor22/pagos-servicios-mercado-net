using Mercado.BL.BE;
using Mercado.DL.DALC;

namespace Mercado.BL.BC
{
    public class PagoBC
    {
        private readonly PagoDALC pagoDALC = new PagoDALC();

        public List<PagoBE> Listar()
        {
            return pagoDALC.Listar();
        }

        public PagoBE? BuscarPorId(long pagoId)
        {
            return pagoDALC.BuscarPorId(pagoId);
        }

        public PagoBE? BuscarPorCodigo(string codigoPago)
        {
            if (string.IsNullOrWhiteSpace(codigoPago))
                throw new Exception("El código de pago es obligatorio.");

            return pagoDALC.BuscarPorCodigo(codigoPago);
        }

        public PagoBE? BuscarPorCodigoDeuda(string codigoDeuda)
        {
            if (string.IsNullOrWhiteSpace(codigoDeuda))
                throw new Exception("El código de deuda es obligatorio.");

            return pagoDALC.BuscarPorCodigoDeuda(codigoDeuda);
        }

        public List<PagoBE> ListarPorEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                throw new Exception("El estado es obligatorio.");

            return pagoDALC.ListarPorEstado(estado);
        }

        public List<PagoBE> ListarPorCodigoPuesto(string codigoPuesto)
        {
            if (string.IsNullOrWhiteSpace(codigoPuesto))
                throw new Exception("El código de puesto es obligatorio.");

            return pagoDALC.ListarPorCodigoPuesto(codigoPuesto);
        }

        public List<PagoBE> ListarPorFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
                throw new Exception("La fecha de inicio no puede ser mayor que la fecha fin.");

            return pagoDALC.ListarPorFechas(fechaInicio, fechaFin);
        }

        public void Crear(string codigoDeuda, string medioPago, string? numeroOperacion, long usuarioId)
        {
            if (string.IsNullOrWhiteSpace(codigoDeuda))
                throw new Exception("El código de deuda es obligatorio.");

            if (string.IsNullOrWhiteSpace(medioPago))
                throw new Exception("El medio de pago es obligatorio.");

            if (usuarioId <= 0)
                throw new Exception("Usuario inválido.");

            pagoDALC.Crear(codigoDeuda, medioPago, numeroOperacion, usuarioId);
        }

        public void Anular(long pagoId, string motivo, long usuarioId)
        {
            if (pagoId <= 0)
                throw new Exception("Pago inválido.");

            if (string.IsNullOrWhiteSpace(motivo))
                throw new Exception("El motivo de anulación es obligatorio.");

            if (usuarioId <= 0)
                throw new Exception("Usuario inválido.");

            pagoDALC.Anular(pagoId, motivo, usuarioId);
        }
    }
}
