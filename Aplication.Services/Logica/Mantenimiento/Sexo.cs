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
    public class Sexo : ISexo
    {
        private readonly UnitOfWork oUnitOfWork;

        public Sexo()
        {
            this.oUnitOfWork = new UnitOfWork();
        }

        public List<ESexo> Cargarsexo()
        {
            var sexo = oUnitOfWork.SexoRepository.Queryable();
            var resultado = (from d in sexo
                             select new ESexo
                             {
                                 SexoId = d.SexoId,
                                 Codigo = d.Codigo,
                                 Descripcion = d.Descripcion
                             }).ToList();

            return resultado;
        }
    }
}
