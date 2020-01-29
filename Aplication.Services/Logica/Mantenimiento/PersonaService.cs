using Aplication.Services.Interfaz.Mantenimiento;
using Domain.Entities.Mantenimiento;
using Repository.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace Aplication.Services.Logica.Mantenimiento
{
    public class PersonaService : IPersonaService
    {
        private readonly UnitOfWork oUnitOfWork;

        public PersonaService()
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
                              PersonaId = p.PersonaId,
                              SexoId = p.SexoId,
                              TipodocumentoId = p.TipodocumentoId,
                              //NombreDistrito = d.Nombre,
                              Usuariocreacion = p.Usuariocreacion,
                              Fechacreacion = p.Fechacreacion,
                              Usuariomodificacion = p.Usuariomodificacion,
                              Fechamodificacion = p.Fechamodificacion
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
                              //Ocupacion = p.Ocupacion,
                              PersonaId = p.PersonaId,
                              SexoId = p.SexoId,
                              TipodocumentoId = p.TipodocumentoId,
                              //NombreDistrito = d.Nombre,
                              Usuariocreacion = p.Usuariocreacion,
                              Fechacreacion = p.Fechacreacion,
                              Usuariomodificacion = p.Usuariomodificacion,
                              Fechamodificacion = p.Fechamodificacion
                          }).ToList();
            }

            return result;
        }

    }
}
