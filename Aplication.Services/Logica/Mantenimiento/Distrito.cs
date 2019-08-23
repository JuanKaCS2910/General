namespace Aplication.Services.Logica.Mantenimiento
{
    using Domain.Entities.General;
    using Domain.Entities.Mantenimiento;
    using Interfaz;
    using PagedList;
    using Repository.UnitOfWork;
    using System.Collections.Generic;
    using System.Linq;
    public class Distrito : IDistrito
    {
        private readonly UnitOfWork oUnitOfWork;

        public Distrito()
        {
            this.oUnitOfWork = new UnitOfWork();
        }

        public List<EDistrito> DistritoGrilla()
        {
            var distrito = oUnitOfWork.DistritoRepository.Queryable();

            var result = from d in distrito
                         select new EDistrito
                         {
                             Nombre = d.Nombre,
                             DistritoId = d.DistritoId,
                             DepartamentoId = d.DepartamentoId
                         };
            var _result = result.ToList();
            //var result = distrito.Select(d => new EDistrito {
            //            Nombre = d.Nombre,
            //            DepartamentoId = 1 }).ToList();

            return _result;

        }

        public IPagedList<EDistrito> DistritoGrillaToPageList(Grilla pag)
        {

            pag.page = (pag.page ?? 1);
            var distrito = oUnitOfWork.DistritoRepository.Queryable();

            var result = (from d in distrito
                         select new EDistrito
                         {
                             Nombre = d.Nombre,
                             DistritoId = d.DistritoId,
                             DepartamentoId = d.DepartamentoId
                         }).OrderBy(d => d.DepartamentoId)
                         .ThenBy(d => d.Nombre);
            var _result = result.ToPagedList((int)pag.page, (int)pag.countrow);
            //var result = distrito.Select(d => new EDistrito {
            //            Nombre = d.Nombre,
            //            DepartamentoId = 1 }).ToList();

            return _result;

        }



    }
}
