using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Mantenimiento
{
    public class EHistoricoResult
    {
        public List<EHistorico> lhistorico { get; set; }
    }

    public class FiltroGrilloHistorico : EHistorico
    {
        public int? page { get; set; }
        public int? countrow { get; set; }
    }

    public class EHistoricoSearch : EPersona
    {
        public int HistoricoId { get; set; }
        public string Diagnostico { get; set; }
        public string Observaciones { get; set; }
        public string Otros { get; set; }
        //SubTramite
        public int SubTramiteId { get; set; }
        public string DescripcionST { get; set; }
        public string CodigoST { get; set; }

        //
        public bool? CondicionAE { get; set; }
        public string DescripcionAE { get; set; }
    }

    public class EHistorico 
    {
        public int HistoricoId { get; set; }
        public int PersonaId { get; set; }
        public string Diagnostico { get; set; }
        public string Observaciones { get; set; }
        public string Otros { get; set; }

        public string Paquetes { get; set; }

        public float Costo { get; set; }

        [Display(Name = "Usuario Creación")]
        public string Usuariocreacion { get; set; }
        public DateTime Fechacreacion { get; set; }
        [Display(Name = "Usuario Modificación")]
        public string Usuariomodificacion { get; set; }
        public DateTime? Fechamodificacion { get; set; }

        //Agente Térmico
        public bool checkCaliente { get; set; }
        public bool checkFria { get; set; }
        public bool checkContraste { get; set; }

        //Agente Electrofísico
        public string descElectroanalgesico { get; set; }
        public bool checkElectroestimulacion { get; set; }
        public string descElectroestimulacion { get; set; }
        public bool checkMagnetoterapia { get; set; }
        public string descMagnetoterapia { get; set; }
        public bool checkUltrasonido { get; set; }
        public string descUltrasonido { get; set; }
        public bool checkTCombinada { get; set; }
        public string descTCombinada { get; set; }
        public bool checkLaserterapia { get; set; }
        public string descLaserterapia { get; set; }
        //Maniobras Terapéuticas
        public bool checkRelajante { get; set; }
        public bool checkDescontracturante { get; set; }
        public bool checkEstiramiento { get; set; }
        public bool checkFortalecimiento { get; set; }
        public bool checkRPG { get; set; }
        public bool checkActivacion { get; set; }
        public bool checkTAPE { get; set; }

        public string NombreCompleto { get; set; }
    }

    public class Tratamiento
    {
        //Agente Térmico

        //Agente Electrofisico
    }
}
