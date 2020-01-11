using Aplication.Services.Interfaz;
using Domain.Entities.Mantenimiento;
using Repository.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace Aplication.Services.Logica.Mantenimiento
{
    public class Usuario : IUsuario
    {
        private readonly UnitOfWork oUnitOfWork;

        public Usuario()
        {
            this.oUnitOfWork = new UnitOfWork();
        }

        public List<EUsuario> ObtenerPersona(EUsuario data)
        {
            var usuario = oUnitOfWork.UsuarioRepository.Queryable();

            var result = (from u in usuario
                          where u.CodUsuario == data.CodUsuario
                             && u.Contrasena == data.Contrasena
                          select new EUsuario
                          {
                              PersonaId = u.PersonaId,
                              CodUsuario = u.CodUsuario,
                              Contrasena = u.Contrasena,
                              Correo = u.Correo
                          });

            var _result = result.ToList();
            return _result;

        }

    }
}
