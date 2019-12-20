using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Mantenimiento
{
    public class EManiobrasTerapeuticas
    {
        public int ManiobrasTerapeuticasId { get; set; }
        public int HistoricoId { get; set; }
        public int SubTramiteId { get; set; }
        public bool Condicion { get; set; }

        [Display(Name = "Usuario Creación")]
        public string Usuariocreacion { get; set; }
        public DateTime Fechacreacion { get; set; }
        [Display(Name = "Usuario Modificación")]
        public string Usuariomodificacion { get; set; }
        public DateTime? Fechamodificacion { get; set; }
    }
}
