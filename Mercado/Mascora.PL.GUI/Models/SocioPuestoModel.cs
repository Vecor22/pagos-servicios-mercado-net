using Mercado.BL.BC;
using Mercado.BL.BE;

namespace Mercado.PL.GUI.Models
{
    public class SocioPuestoModel
    {
        private readonly SocioPuestoBC socioPuestoBC = new SocioPuestoBC();

        public List<SocioPuestoBE> Listar()
        {
            return socioPuestoBC.Listar();
        }

        public void CrearAsignacion(string codigoSocio, string codigoPuesto, long usuarioId)
        {
            socioPuestoBC.CrearAsignacion(codigoSocio, codigoPuesto, usuarioId);
        }

        public void Reasignar(string codigoSocio, string codigoPuesto, long usuarioId)
        {
            socioPuestoBC.Reasignar(codigoSocio, codigoPuesto, usuarioId);
        }

        public SocioPuestoBE? BuscarPorCodigoPuesto(string codigoPuesto)
        {
            return socioPuestoBC.BuscarPorCodigoPuesto(codigoPuesto);
        }

        public List<SocioPuestoBE> ListarPorNombreSocio(string nombre)
        {
            return socioPuestoBC.ListarPorNombreSocio(nombre);
        }

        public List<SocioPuestoBE> ListarPorDniSocio(string dni)
        {
            return socioPuestoBC.ListarPorDniSocio(dni);
        }
    }
}
