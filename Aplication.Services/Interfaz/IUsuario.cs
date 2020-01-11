using Domain.Entities.Mantenimiento;
namespace Aplication.Services.Interfaz
{
    using System.Collections.Generic;
    public interface IUsuario
    {
        List<EUsuario> ObtenerPersona(EUsuario data);
    }
}
