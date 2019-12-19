using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Mantenimiento
{
    public class EAgenteelectrofisico : ESubtramite
    {
        public int AgenteElectrofisicoId { get; set; }
        public int HistoricoId { get; set; }
        //public int SubTramiteId { get; set; }
        //public string Descripcion { get; set; }
        //public string Codigo { get; set; }
        public bool Condicion { get; set; }
        public string DescripcionAE { get; set; }

        [Display(Name = "Usuario Creación")]
        public string Usuariocreacion { get; set; }
        public DateTime Fechacreacion { get; set; }
        [Display(Name = "Usuario Modificación")]
        public string Usuariomodificacion { get; set; }
        public DateTime? Fechamodificacion { get; set; }
    }
}
