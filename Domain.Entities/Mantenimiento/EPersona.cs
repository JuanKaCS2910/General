using Domain.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Mantenimiento
{

    public class EPersonaDTO
    {
        public List<EPersona> personaDTO { get; set; }
    }

    public class EPersona : ECampoAuditoria
    {

        public int PersonaId { get; set; }
        public int TipodocumentoId { get; set; }
        public string Nrodocumento { get; set; }
        public string Nombre { get; set; }
        public string Apellidopaterno { get; set; }
        public string Apellidomaterno { get; set; }
        public string Nrotelefono { get; set; }
        public DateTime? Fecnacimiento { get; set; }
        public int DistritoId { get; set; }
        public string Direccion { get; set; }
        public int SexoId { get; set; }
        

    }
}
