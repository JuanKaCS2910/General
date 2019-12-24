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
        public List<EAgentetermico> AgenteTermico { get; set; }
        public List<EManiobrasTerapeuticas> ManiobraTerapeutica { get; set; }
        public List<EAntecedentes> Antecedentes { get; set; }

    }
}
