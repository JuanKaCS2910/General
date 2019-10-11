using Aplication.Services.Interfaz;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Logica.Mantenimiento
{
    public class Historico : IHistorico
    {
        private readonly UnitOfWork oUnitOfWork;
        public Historico()
        {
            this.oUnitOfWork = new UnitOfWork();
        }

    }
}
