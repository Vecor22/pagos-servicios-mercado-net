using Mercado.BL.BC;
using Mercado.BL.BE;

namespace Mercado.PL.GUI.Models
{
    public class SocioModel
    {
        private readonly SocioBC socioBC = new SocioBC();

        public List<SocioBE> Listar()
        {
            return socioBC.Listar();
        }

        public SocioBE? BuscarPorId(long socioId)
        {
            return socioBC.BuscarPorId(socioId);
        }

        public List<SocioBE> BuscarPorNombre(string nombre)
        {
            return socioBC.BuscarPorNombre(nombre);
        }

        public SocioBE? BuscarPorDni(string dni)
        {
            return socioBC.BuscarPorDni(dni);
        }

        public void Crear(SocioBE socio)
        {
            socioBC.Crear(socio);
        }

        public void Actualizar(SocioBE socio)
        {
            socioBC.Actualizar(socio);
        }

        public void CambiarEstado(long socioId, string estado)
        {
            socioBC.CambiarEstado(socioId, estado);
        }
    }
}
