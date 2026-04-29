using Mercado.BL.BE;
using Mercado.DL.DALC;

namespace Mercado.BL.BC
{
    public class SocioBC
    {
        private readonly SocioDALC socioDALC = new SocioDALC();

        public List<SocioBE> Listar()
        {
            return socioDALC.Listar();
        }

        public SocioBE? BuscarPorId(long socioId)
        {
            return socioDALC.BuscarPorId(socioId);
        }

        public List<SocioBE> BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("El nombre del socio es obligatorio.");

            return socioDALC.BuscarPorNombre(nombre);
        }

        public SocioBE? BuscarPorDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new Exception("El DNI es obligatorio.");

            if (dni.Length != 8)
                throw new Exception("El DNI debe tener 8 dígitos.");

            return socioDALC.BuscarPorDni(dni);
        }

        public void Crear(SocioBE socio)
        {
            if (string.IsNullOrWhiteSpace(socio.Nombres))
                throw new Exception("Los nombres son obligatorios.");

            if (string.IsNullOrWhiteSpace(socio.Apellidos))
                throw new Exception("Los apellidos son obligatorios.");

            if (string.IsNullOrWhiteSpace(socio.Dni) || socio.Dni.Length != 8)
                throw new Exception("El DNI debe tener 8 dígitos.");

            socioDALC.Crear(socio);
        }

        public void Actualizar(SocioBE socio)
        {
            if (socio.SocioID <= 0)
                throw new Exception("Socio inválido.");

            if (string.IsNullOrWhiteSpace(socio.Nombres))
                throw new Exception("Los nombres son obligatorios.");

            if (string.IsNullOrWhiteSpace(socio.Apellidos))
                throw new Exception("Los apellidos son obligatorios.");

            socioDALC.Actualizar(socio);
        }

        public void CambiarEstado(long socioId, string estado)
        {
            if (socioId <= 0)
                throw new Exception("Socio inválido.");

            if (estado != "ACTIVO" && estado != "INACTIVO")
                throw new Exception("Estado inválido.");

            socioDALC.CambiarEstado(socioId, estado);
        }
    }
}
