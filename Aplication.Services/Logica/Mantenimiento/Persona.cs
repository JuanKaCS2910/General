using Aplication.Services.Interfaz;
using Domain.Entities.General;
using Domain.Entities.Mantenimiento;
using PagedList;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Logica.Mantenimiento
{
    public class Persona : IPersona
    {
        private readonly UnitOfWork oUnitOfWork;

        public Persona()
        {
            this.oUnitOfWork = new UnitOfWork();
        }

        public IPagedList<EPersona> PersonaGrillaToPageList(Grilla pag)
        {
            pag.page = (pag.page ?? 1);
            var persona = oUnitOfWork.PersonaRepository.Queryable();

            var result = (from d in persona
                          select new EPersona
                          {
                              PersonaId = d.PersonaId,
                              TipodocumentoId = d.TipodocumentoId,
                              //Tipodocumento = d.TipodocumentoId,
                              Nrodocumento = d.Nrodocumento,
                              Nombre = d.Nombre,
                              Apellidopaterno = d.Apellidopaterno,
                              Apellidomaterno = d.Apellidomaterno,
                              Nrotelefono = d.Nrotelefono,
                              Fecnacimiento = d.Fecnacimiento,
                              DistritoId = d.DistritoId,
                              Direccion = d.Direccion,
                              Ocupacion = d.Ocupacion,
                              SexoId = d.SexoId,
                          }).OrderBy(d => d.Apellidopaterno)
                         .ThenBy(d => d.Nombre);
            var _result = result.ToPagedList((int)pag.page, (int)pag.countrow);

            return _result;

        }

        #region Save,Edit,Delet

        public string CreatePerson(EPersona registro)
        {
            string mensaje = string.Empty;
            Repository.Persona pers = new Repository.Persona();
            pers.Apellidopaterno = registro.Apellidopaterno;
            pers.Apellidomaterno = registro.Apellidopaterno;
            pers.Nombre = registro.Nombre;
            pers.SexoId = registro.SexoId;
            pers.TipodocumentoId = registro.TipodocumentoId;
            pers.Nrodocumento = registro.Nrodocumento;
            pers.Fecnacimiento = DateTime.Now;// registro.Fecnacimiento;
            pers.DistritoId = registro.DistritoId;
            pers.Direccion = registro.Direccion;
            pers.Nrotelefono = registro.Nrotelefono;
            pers.Ocupacion = registro.Ocupacion;
            pers.Fechacreacion = DateTime.Now;
            pers.Usuariocreacion = "JUCASTRO";
            oUnitOfWork.PersonaRepository.Insert(pers);
            try
            {
                oUnitOfWork.Save();
                mensaje = "OK";
            }
            catch (Exception ex)
            {
                //string mensaje = string.Empty;
                mensaje = "error";
                //throw;
            }
            

            return mensaje;
        }

        #endregion

        //public List<EPersona> PersonaGrilla(int? personaId)
        //{
        //    var persona = oUnitOfWork.PersonaRepository.Queryable();

        //}

    }
}
