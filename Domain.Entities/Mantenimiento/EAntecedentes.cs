using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Mantenimiento
{
    public class EAntecedentes
    {
        /*
        public int AntecedentesId { get; set; }
        public int HistoricoId { get; set; }
        public int SubTramiteId { get; set; }
        public bool Condicion { get; set; }
        public string Usuariocreacion { get; set; }
        public DateTime Fechacreacion { get; set; }
        [Display(Name = "Usuario Modificación")]
        public string Usuariomodificacion { get; set; }
        public DateTime? Fechamodificacion { get; set; }
*/
        public int AntecedentesId { get; set; }
        public int HistoricoId { get; set; }
        public int SubTramiteId { get; set; }
        public Nullable<bool> Condicion { get; set; }
        public string Usuariocreacion { get; set; }
        public System.DateTime Fechacreacion { get; set; }
        public string Usuariomodificacion { get; set; }
        public Nullable<System.DateTime> Fechamodificacion { get; set; }
    }
}
