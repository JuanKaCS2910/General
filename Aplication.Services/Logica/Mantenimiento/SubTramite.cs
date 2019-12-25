using Aplication.Services.Interfaz;
using Domain.Entities.Mantenimiento;
using Repository.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace Aplication.Services.Logica.Mantenimiento
{
    public class SubTramite : ISubTramite
    {
        private readonly UnitOfWork oUnitOfWork;
        public SubTramite()
        {
            this.oUnitOfWork = new UnitOfWork();
        }
        public List<ESubtramite> CargarSubtTramite(string Tipo)
        {

            var sub = oUnitOfWork.SubTramiteRepository.Queryable();
            var resultado = (from s in sub
                             where s.Codigo == Tipo
                             select new ESubtramite
                             {
                                 SubTramiteId = s.SubTramiteId,
                                 Codigo = s.Codigo,
                                 Descripcion = s.Descripcion
                             }).ToList();

            return resultado;
        }
    }
}
