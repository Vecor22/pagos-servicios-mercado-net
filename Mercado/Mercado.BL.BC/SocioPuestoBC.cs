using Mercado.BL.BE;
using Mercado.DL.DALC;

namespace Mercado.BL.BC
{
    public class SocioPuestoBC
    {
        private readonly SocioPuestoDALC socioPuestoDALC = new SocioPuestoDALC();

        public List<SocioPuestoBE> Listar()
        {
            return socioPuestoDALC.Listar();
        }

        public void CrearAsignacion(string codigoSocio, string codigoPuesto, long usuarioId)
        {
            if (string.IsNullOrWhiteSpace(codigoSocio))
                throw new Exception("El código del socio es obligatorio.");

            if (string.IsNullOrWhiteSpace(codigoPuesto))
                throw new Exception("El código del puesto es obligatorio.");

            if (usuarioId <= 0)
                throw new Exception("Usuario inválido.");

            socioPuestoDALC.CrearAsignacion(codigoSocio, codigoPuesto, usuarioId);
        }

        public void Reasignar(string codigoSocio, string codigoPuesto, long usuarioId)
        {
            if (string.IsNullOrWhiteSpace(codigoSocio))
                throw new Exception("El código del socio es obligatorio.");

            if (string.IsNullOrWhiteSpace(codigoPuesto))
                throw new Exception("El código del puesto es obligatorio.");

            if (usuarioId <= 0)
                throw new Exception("Usuario inválido.");

            socioPuestoDALC.Reasignar(codigoSocio, codigoPuesto, usuarioId);
        }

        public SocioPuestoBE? BuscarPorCodigoPuesto(string codigoPuesto)
        {
            if (string.IsNullOrWhiteSpace(codigoPuesto))
                throw new Exception("El código del puesto es obligatorio.");

            return socioPuestoDALC.BuscarPorCodigoPuesto(codigoPuesto);
        }

        public List<SocioPuestoBE> ListarPorNombreSocio(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("El nombre del socio es obligatorio.");

            return socioPuestoDALC.ListarPorNombreSocio(nombre);
        }

        public List<SocioPuestoBE> ListarPorDniSocio(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new Exception("El DNI es obligatorio.");

            if (dni.Length != 8)
                throw new Exception("El DNI debe tener 8 dígitos.");

            return socioPuestoDALC.ListarPorDniSocio(dni);
        }
    }
}
