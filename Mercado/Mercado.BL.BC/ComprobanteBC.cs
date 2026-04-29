using Mercado.BL.BE;
using Mercado.DL.DALC;

namespace Mercado.BL.BC
{
    public class ComprobanteBC
    {
        private readonly ComprobanteDALC comprobanteDALC = new ComprobanteDALC();

        public ComprobanteBE? BuscarPorIdPago(long pagoId)
        {
            if (pagoId <= 0)
                throw new Exception("Pago inválido.");

            return comprobanteDALC.BuscarPorIdPago(pagoId);
        }

        public ComprobanteBE? BuscarPorNumero(string numeroComprobante)
        {
            if (string.IsNullOrWhiteSpace(numeroComprobante))
                throw new Exception("El número de comprobante es obligatorio.");

            return comprobanteDALC.BuscarPorNumero(numeroComprobante);
        }

        public void Crear(long pagoId, long usuarioId)
        {
            if (pagoId <= 0)
                throw new Exception("Pago inválido.");

            if (usuarioId <= 0)
                throw new Exception("Usuario inválido.");

            comprobanteDALC.Crear(pagoId, usuarioId);
        }

        public void AnularPorPago(long pagoId, string motivo, long usuarioId)
        {
            if (pagoId <= 0)
                throw new Exception("Pago inválido.");

            if (string.IsNullOrWhiteSpace(motivo))
                throw new Exception("El motivo de anulación es obligatorio.");

            if (usuarioId <= 0)
                throw new Exception("Usuario inválido.");

            comprobanteDALC.AnularPorPago(pagoId, motivo, usuarioId);
        }
    }
}
