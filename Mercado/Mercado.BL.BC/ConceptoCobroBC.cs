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
            if (string.IsNullOrWhiteSpace(concepto.Nombre))
                throw new Exception("El nombre del concepto es obligatorio.");

            if (string.IsNullOrWhiteSpace(concepto.TipoCobro))
                throw new Exception("El tipo de cobro es obligatorio.");

            conceptoDALC.Crear(concepto);
        }

        public void Actualizar(ConceptoCobroBE concepto)
        {
            if (concepto.ConceptoCobroID <= 0)
                throw new Exception("Concepto inválido.");

            if (string.IsNullOrWhiteSpace(concepto.Nombre))
                throw new Exception("El nombre del concepto es obligatorio.");

            if (string.IsNullOrWhiteSpace(concepto.TipoCobro))
                throw new Exception("El tipo de cobro es obligatorio.");

            conceptoDALC.Actualizar(concepto);
        }
    }
}
