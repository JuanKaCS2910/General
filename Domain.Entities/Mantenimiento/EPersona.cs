using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Mantenimiento
{
    public class EPersonaResult
    {
        public List<EPersona> lpersona { get; set; }
    }

    public class FiltroDistritoPersona
    {
        public string FDistrito { get; set; }
        public int rows { get; set; }

    }

    public class EPersona
    {
        [Display(Name="Codigo")]
        public int PersonaId { get; set; }
        [Display(Name = "T. Documento")]
        public int TipodocumentoId { get; set; }
        [Display(Name = "Nro. Documento")]
        public string Nrodocumento { get; set; }
        [Required]
        [Display(Name = "Nombres")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name ="Ap. Paterno")]
        public string Apellidopaterno { get; set; }
        [Display(Name = "Ap. Materno")]
        public string Apellidomaterno { get; set; }
        [Display(Name ="Nro. Teléfono")]
        public string Nrotelefono { get; set; }
        [Display(Name ="F. Nacimiento")]
        public DateTime? Fecnacimiento { get; set; }
        [Display(Name = "Distrito")]
        public int DistritoId { get; set; }
        public string Direccion { get; set; }
        public string Ocupacion { get; set; }
        [Display(Name ="Sexo")]
        public int SexoId { get; set; }
        public string Usuariocreacion { get; set; }
        public DateTime Fechacreacion { get; set; }
        public string Usuariomodificacion { get; set; }
        public DateTime? Fechamodificacion { get; set; }
    }
}
