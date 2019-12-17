using Domain.Entities.General;
using Domain.Entities.Mantenimiento;
using PagedList;
using System.Collections.Generic;

namespace Aplication.Services.Interfaz
{
    public interface IHistorico 
    {
        string CreateHistory(EHistorico registro);
        List<EPersona> PersonaHistoricoGrilla(int? personId);
        IPagedList<EHistorico> HistoricoGrillaToPageList(FiltroGrilloHistorico pag);
    }
}
