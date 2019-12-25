using Domain.Entities.Mantenimiento;
using System.Collections.Generic;

namespace Aplication.Services.Interfaz
{
    public interface ISubTramite
    {
        List<ESubtramite> CargarSubtTramite(string Tipo);

    }
}
