using Domain.Entities.General;
using Domain.Entities.Mantenimiento;
using PagedList;

namespace Aplication.Services.Interfaz
{
    public interface IHistorico 
    {
        string CreateHistory(EHistorico registro);
        IPagedList<EHistorico> HistoricoGrillaToPageList(Grilla pag);
    }
}
