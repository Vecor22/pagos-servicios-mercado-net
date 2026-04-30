using Mercado.BL.BE;
using Mercado.DL.DALC;

namespace Mercado.BL.BC
{
    public class ConceptoCobroBC
    {
        private readonly ConceptoCobroDALC conceptoDALC = new ConceptoCobroDALC();

        public List<ConceptoCobroBE> Listar()
        {
            return conceptoDALC.Listar();
        }

        public ConceptoCobroBE? BuscarPorId(long id)
        {
            return conceptoDALC.BuscarPorId(id);
        }

        public void Crear(ConceptoCobroBE concepto)
        {
            Normalizar(concepto);

            if (string.IsNullOrWhiteSpace(concepto.Nombre))
                throw new Exception("El nombre del concepto es obligatorio.");

            if (string.IsNullOrWhiteSpace(concepto.TipoCobro))
                throw new Exception("El tipo de cobro es obligatorio.");

            if (concepto.Nombre.Length > 100)
                throw new Exception("El nombre del concepto no puede superar los 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(concepto.Descripcion) && concepto.Descripcion.Length > 255)
                throw new Exception("La descripcion no puede superar los 255 caracteres.");

            if (Listar().Any(c => string.Equals(c.Nombre, concepto.Nombre, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("Ya existe un concepto de cobro con ese nombre.");

            conceptoDALC.Crear(concepto);
        }

        public void Actualizar(ConceptoCobroBE concepto)
        {
            Normalizar(concepto);

            if (concepto.ConceptoCobroID <= 0)
                throw new Exception("Concepto inválido.");

            if (string.IsNullOrWhiteSpace(concepto.Nombre))
                throw new Exception("El nombre del concepto es obligatorio.");

            if (string.IsNullOrWhiteSpace(concepto.TipoCobro))
                throw new Exception("El tipo de cobro es obligatorio.");

            if (concepto.Nombre.Length > 100)
                throw new Exception("El nombre del concepto no puede superar los 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(concepto.Descripcion) && concepto.Descripcion.Length > 255)
                throw new Exception("La descripcion no puede superar los 255 caracteres.");

            if (Listar().Any(c =>
                c.ConceptoCobroID != concepto.ConceptoCobroID &&
                string.Equals(c.Nombre, concepto.Nombre, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("Ya existe un concepto de cobro con ese nombre.");
            }

            conceptoDALC.Actualizar(concepto);
        }

        private static void Normalizar(ConceptoCobroBE concepto)
        {
            concepto.Nombre = concepto.Nombre?.Trim();
            concepto.Descripcion = string.IsNullOrWhiteSpace(concepto.Descripcion)
                ? null
                : concepto.Descripcion.Trim();
            concepto.TipoCobro = concepto.TipoCobro?.Trim();
        }
    }
}
