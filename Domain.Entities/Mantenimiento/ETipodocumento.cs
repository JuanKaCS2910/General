using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Mantenimiento
{
    public class ETipodocumentoResult
    {
        public List<ETipodocumento> ltipodocumento { get; set; }
    }

    public class ETipodocumento
    {
        public int TipodocumentoId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
}
