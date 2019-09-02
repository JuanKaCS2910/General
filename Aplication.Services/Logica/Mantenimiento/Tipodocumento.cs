using Aplication.Services.Interfaz;
using Domain.Entities.Mantenimiento;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Logica.Mantenimiento
{
    public class Tipodocumento : ITipodocumento
    {
        private readonly UnitOfWork oUnitOfWork;
        public Tipodocumento()
        {
            this.oUnitOfWork = new UnitOfWork();
        }

        public List<ETipodocumento> Cargardocumento()
        {
            var documento = oUnitOfWork.TipoDocumentoRepository.Queryable();
            var resultado = (from d in documento
                             select new ETipodocumento
                             {
                                 TipodocumentoId = d.TipodocumentoId,
                                 Codigo = d.Codigo,
                                 Descripcion = d.Descripcion
                             }).ToList();
            
            return resultado;
        }

    }
}
