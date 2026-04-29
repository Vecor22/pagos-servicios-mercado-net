using Mercado.BL.BC;
using Mercado.BL.BE;

namespace Mercado.PL.GUI.Models
{
    public class ComprobanteModel
    {
        private readonly ComprobanteBC comprobanteBC = new ComprobanteBC();

        public ComprobanteBE? BuscarPorIdPago(long pagoId)
        {
            return comprobanteBC.BuscarPorIdPago(pagoId);
        }

        public ComprobanteBE? BuscarPorNumero(string numeroComprobante)
        {
            return comprobanteBC.BuscarPorNumero(numeroComprobante);
        }

        public void Crear(long pagoId, long usuarioId)
        {
            comprobanteBC.Crear(pagoId, usuarioId);
        }

        public void AnularPorPago(long pagoId, string motivo, long usuarioId)
        {
            comprobanteBC.AnularPorPago(pagoId, motivo, usuarioId);
        }
    }
}
