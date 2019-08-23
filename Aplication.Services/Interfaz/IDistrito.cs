using Domain.Entities.General;
using Domain.Entities.Mantenimiento;
using PagedList;
using System.Collections.Generic;

namespace Aplication.Services.Interfaz
{
    public interface IDistrito
    {
        List<EDistrito> DistritoGrilla();
        IPagedList<EDistrito> DistritoGrillaToPageList(Grilla pag);

    }
}
