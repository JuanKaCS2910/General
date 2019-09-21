using Aplication.Services.Interfaz;
using AutoMapper;
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

        public List<EPersona> PersonaGrilla(int? personId)
        {
            var persona = oUnitOfWork.PersonaRepository.Queryable();
            var distrito = oUnitOfWork.DistritoRepository.Queryable();
            var result = new List<EPersona>();

            if (personId == null)
            {
                result = (from p in persona
                          join d in distrito
                          on p.DistritoId equals d.DistritoId
                          select new EPersona
                          {
                              Apellidomaterno = p.Apellidomaterno,
                              Apellidopaterno = p.Apellidopaterno,
                              Direccion = p.Direccion,
                              DistritoId = p.DistritoId,
                              Fecnacimiento = p.Fecnacimiento,
                              Nombre = p.Nombre,
                              Nrodocumento = p.Nrodocumento,
                              Nrotelefono = p.Nrotelefono,
                              Ocupacion = p.Ocupacion,
                              PersonaId = p.PersonaId,
                              SexoId = p.SexoId,
                              TipodocumentoId = p.TipodocumentoId,
                              NombreDistrito = d.Nombre
                          }).ToList();
            }
            else
            {
                result = (from p in persona
                          join d in distrito
                          on p.DistritoId equals d.DistritoId
                          where p.PersonaId == personId
                          select new EPersona
                          {
                              Apellidomaterno = p.Apellidomaterno,
                              Apellidopaterno = p.Apellidopaterno,
                              Direccion = p.Direccion,
                              DistritoId = p.DistritoId,
                              Fecnacimiento = p.Fecnacimiento,
                              Nombre = p.Nombre,
                              Nrodocumento = p.Nrodocumento,
                              Nrotelefono = p.Nrotelefono,
                              Ocupacion = p.Ocupacion,
                              PersonaId = p.PersonaId,
                              SexoId = p.SexoId,
                              TipodocumentoId = p.TipodocumentoId,
                              NombreDistrito = d.Nombre
                          }).ToList();
            }
            
            return result; 
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
            pers.Fecnacimiento = registro.Fecnacimiento;
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
                //Nrodocumento_Index
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("Nrodocumento_Index"))
                {
                    mensaje = "El Nro. Documento ya se encuentra registrado";
                }
                else 
                {
                    mensaje = ex.Message;
                }
            }
            
            return mensaje;
        }

        public string EditPerson(EPersona registro)
        {
            string mensaje = string.Empty;
            Repository.Persona pers = new Repository.Persona();
            pers.PersonaId = registro.PersonaId;
            pers.Apellidopaterno = registro.Apellidopaterno;
            pers.Apellidomaterno = registro.Apellidopaterno;
            pers.Nombre = registro.Nombre;
            pers.SexoId = registro.SexoId;
            pers.TipodocumentoId = registro.TipodocumentoId;
            pers.Nrodocumento = registro.Nrodocumento;
            pers.Fecnacimiento = registro.Fecnacimiento;
            pers.DistritoId = registro.DistritoId;
            pers.Direccion = registro.Direccion;
            pers.Nrotelefono = registro.Nrotelefono;
            pers.Ocupacion = registro.Ocupacion;
            pers.Fechacreacion = DateTime.Now;
            pers.Usuariocreacion = "JUCASTRO";
            pers.Fechamodificacion = DateTime.Now;
            pers.Usuariomodificacion = "JUCASTRO";
            oUnitOfWork.PersonaRepository.Update(pers);
            try
            {
                oUnitOfWork.Save();
                mensaje = "OK";
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("Nrodocumento_Index"))
                {
                    mensaje = "El Nro. Documento ya se encuentra registrado";
                }
                else
                {
                    mensaje = ex.Message;
                }
            }

            return mensaje;
        }

        public string DeletePerson(EPersona registro)
        {
            string mensaje = string.Empty;
            try
            {
                oUnitOfWork.PersonaRepository.Delete(registro.PersonaId);
                oUnitOfWork.Save();
                mensaje = "OK";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
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
