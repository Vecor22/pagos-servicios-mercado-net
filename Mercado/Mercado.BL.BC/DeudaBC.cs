using Mercado.BL.BE;
using Mercado.DL.DALC;

namespace Mercado.BL.BC
{
    public class DeudaBC
    {
        private readonly DeudaDALC deudaDALC = new DeudaDALC();

        public List<DeudaBE> Listar()
        {
            return deudaDALC.Listar();
        }

        public DeudaBE? BuscarPorId(long deudaId)
        {
            return deudaDALC.BuscarPorId(deudaId);
        }

        public DeudaBE? BuscarPorCodigo(string codigoDeuda)
        {
            if (string.IsNullOrWhiteSpace(codigoDeuda))
                throw new Exception("El código de deuda es obligatorio.");

            return deudaDALC.BuscarPorCodigo(codigoDeuda);
        }

        public List<DeudaBE> ListarPorEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                throw new Exception("El estado es obligatorio.");

            return deudaDALC.ListarPorEstado(estado);
        }

        public List<DeudaBE> ListarPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
                throw new Exception("La fecha de inicio no puede ser mayor que la fecha fin.");

            return deudaDALC.ListarPorFecha(fechaInicio, fechaFin);
        }

        public List<DeudaBE> ListarPorCodigoPuesto(string codigoPuesto)
        {
            if (string.IsNullOrWhiteSpace(codigoPuesto))
                throw new Exception("El código de puesto es obligatorio.");

            return deudaDALC.ListarPorCodigoPuesto(codigoPuesto);
        }

        public void Crear(DeudaBE deuda)
        {
            if (deuda.ConceptoCobro == null || deuda.ConceptoCobro.ConceptoCobroID <= 0)
                throw new Exception("Debe seleccionar un concepto de cobro válido.");

            if (deuda.Monto < 1)
                throw new Exception("El monto mínimo de la deuda es 1.");

            if (string.IsNullOrWhiteSpace(deuda.TipoGeneracion))
                throw new Exception("El tipo de generación es obligatorio.");

            if (deuda.TipoGeneracion == "INDIVIDUAL")
            {
                if (deuda.Puesto == null || string.IsNullOrWhiteSpace(deuda.Puesto.CodigoPuesto))
                    throw new Exception("Debe seleccionar un puesto para una deuda individual.");
            }

            if (deuda.TipoGeneracion != "INDIVIDUAL" && deuda.TipoGeneracion != "REPARTIBLE")
                throw new Exception("Tipo de generación inválido.");

            if (deuda.CreadoPor == null || deuda.CreadoPor.UsuarioID <= 0)
                throw new Exception("Usuario creador inválido.");

            deudaDALC.Crear(deuda);
        }

        public void Exonerar(long deudaId, string motivo, long usuarioId)
        {
            if (deudaId <= 0)
                throw new Exception("Deuda inválida.");

            if (string.IsNullOrWhiteSpace(motivo))
                throw new Exception("El motivo de exoneración es obligatorio.");

            if (usuarioId <= 0)
                throw new Exception("Usuario inválido.");

            deudaDALC.Exonerar(deudaId, motivo, usuarioId);
        }
    }
}
