using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Mantenimiento
{
    public class EDistritoResult
    {
        public List<EDistrito> ldistrito { get; set; }
    }
    public class EDistrito
    {
        [Display(Name ="Código")]
        public int DistritoId { get; set; }
        public int DepartamentoId { get; set; }
        public string Nombre { get; set; }
    }
}
