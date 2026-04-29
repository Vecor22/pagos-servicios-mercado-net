using Mercado.BL.BC;
using Mercado.BL.BE;

namespace Mercado.PL.GUI.Models
{
    public class ConceptoCobroModel
    {
        private readonly ConceptoCobroBC conceptoBC = new ConceptoCobroBC();

        public List<ConceptoCobroBE> Listar()
        {
            return conceptoBC.Listar();
        }

        public ConceptoCobroBE? BuscarPorId(long conceptoCobroId)
        {
            return conceptoBC.BuscarPorId(conceptoCobroId);
        }

        public void Crear(ConceptoCobroBE concepto)
        {
            conceptoBC.Crear(concepto);
        }

        public void Actualizar(ConceptoCobroBE concepto)
        {
            conceptoBC.Actualizar(concepto);
        }
    }
}
