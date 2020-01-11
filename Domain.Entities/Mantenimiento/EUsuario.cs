using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Mantenimiento
{
    public class EUsuario : EAudioria
    {
        public int UsuarioId { get; set; }
        public int PersonaId { get; set; }
        [Display(Name = "Usuario")]
        public string CodUsuario { get; set; }
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }
        public string Correo { get; set; }
    }
}
