using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Mantenimiento
{
    public class EHistoricoView
    {
        public List<EPersona> Persona { get; set; }
        public List<EHistorico> Historico { get; set; }
        public List<EAgenteelectrofisico> AgenteElectofisico { get; set; }
        //public List<EHistoricoSearch> HistoricoSearch { get; set; }
    }
}
