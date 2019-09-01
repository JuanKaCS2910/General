namespace Aplication.Services.Logica.Mantenimiento
{
    using Domain.Entities.General;
    using Domain.Entities.Mantenimiento;
    using Interfaz;
    using PagedList;
    using Repository.UnitOfWork;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using System.Threading.Tasks;

    public class Distrito : IDistrito
    {
        private readonly UnitOfWork oUnitOfWork;

        public Distrito()
        {
            this.oUnitOfWork = new UnitOfWork();
        }

        public List<EDistrito> DistritoGrilla(int? distritoId)
        {
            var distrito = oUnitOfWork.DistritoRepository.Queryable();

            var result = from d in distrito
                         where d.DistritoId == distritoId
                         select new EDistrito
                         {
                             Nombre = d.Nombre,
                             DistritoId = d.DistritoId,
                             DepartamentoId = d.DepartamentoId
                         };
            var _result = result.ToList();
            return _result;
        }

        public List<EDistrito> BusquedaDistrito(FiltroDistritoPersona filtro)
        {
            var distrito = oUnitOfWork.DistritoRepository.Queryable();
            var result = new List<EDistrito>();

            if (!string.IsNullOrEmpty(filtro.FDistrito))
            {
                result = (distrito.Where(d => d.Nombre.Contains(filtro.FDistrito))
                            .Select(a => new EDistrito
                            {
                                Nombre = a.Nombre,
                                DistritoId = a.DistritoId,
                                DepartamentoId = a.DepartamentoId
                            })).OrderBy(d => d.Nombre).Take(filtro.rows).ToList();
            }
            else
            {
                result = (distrito.Select(a => new EDistrito
                            {
                                Nombre = a.Nombre,
                                DistritoId = a.DistritoId,
                                DepartamentoId = a.DepartamentoId
                            })).OrderBy(d => d.Nombre).Take(filtro.rows).ToList();
            }

            return result;
        }

        public IPagedList<EDistritoView> DistritoGrillaToPageList(Grilla pag)
        {

            pag.page = (pag.page ?? 1);
            var distrito = oUnitOfWork.DistritoRepository.Queryable();

            var result = (from d in distrito
                         select new EDistritoView
                         {
                             Nombre = d.Nombre,
                             DistritoId = d.DistritoId,
                             DepartamentoId = d.DepartamentoId
                         }).OrderBy(d => d.DepartamentoId)
                         .ThenBy(d => d.Nombre);
            var _result = result.ToPagedList((int)pag.page, (int)pag.countrow);
            
            return _result;

        }

        public string CreateDistrito(EDistrito registro)
        {
            Repository.Distrito d = new Repository.Distrito();
            d.Nombre = registro.Nombre;
            d.DepartamentoId = registro.DepartamentoId;

            oUnitOfWork.DistritoRepository.Insert(d);
            oUnitOfWork.Save();

            return "OK";
        }

        public string UpdateDistrito(EDistrito registro)
        {
            Repository.Distrito d = new Repository.Distrito();
            d.Nombre = registro.Nombre;
            d.DepartamentoId = registro.DepartamentoId;
            d.DistritoId = registro.DistritoId;

            oUnitOfWork.DistritoRepository.Update(d);
            oUnitOfWork.Save();

            return "OK";
        }

        public string EliminarDistrito(int? distritoId)
        {
            string Resultado = string.Empty;
            var distrito = oUnitOfWork.DistritoRepository.GetByID(distritoId);

            if (distrito == null)
            {
                Resultado = "Distrito no existe";
            }
            else
            {
                oUnitOfWork.DistritoRepository.Delete(distritoId);
                oUnitOfWork.Save();
                Resultado = "OK";
            }

            return Resultado;

        }

    }
}
