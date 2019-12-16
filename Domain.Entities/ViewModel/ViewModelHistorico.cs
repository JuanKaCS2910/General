using Domain.Entities.Mantenimiento;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ViewModel
{
    public class ViewModelHistorico : ViewModelGrilla
    {
        public IPagedList<EPersona> PersonaGrilla { get; set; }
        public EPersona FiltroPerson { get; set; }
        public EHistorico Historicos { get; set; }
        public IPagedList<EHistorico> HistoricoGrilla { get; set; }
        public EAgentetermico AgenteTermicos { get; set; }
        public EAgenteelectrofisico AgenteElectrofisicos { get; set; }
        
    }
}
