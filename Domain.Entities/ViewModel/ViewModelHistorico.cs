using Domain.Entities.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ViewModel
{
    public class ViewModelHistorico : ViewModelGrilla
    {

        public EHistorico Historicos { get; set; }
        public EAgentetermico AgenteTermicos { get; set; }
        public EAgenteelectrofisico AgenteElectrofisicos { get; set; }
        
    }
}
