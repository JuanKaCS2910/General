using Domain.Entities.General;
using Domain.Entities.Mantenimiento;
using PagedList;
using System.Collections.Generic;

namespace Aplication.Services.Interfaz
{
    public interface IDistrito
    {
        List<EDistrito> DistritoGrilla(int? distritoId);
        IPagedList<EDistritoView> DistritoGrillaToPageList(Grilla pag);
        string CreateDistrito(EDistrito registro);
        string UpdateDistrito(EDistrito registro);
        string EliminarDistrito(int? distritoId);
        List<EDistrito> BusquedaDistrito(FiltroDistritoPersona filtro);
    }
}
