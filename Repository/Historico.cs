//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Historico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Historico()
        {
            this.AgenteElectrofisico = new HashSet<AgenteElectrofisico>();
            this.Frecuencia = new HashSet<Frecuencia>();
            this.ManiobrasTerapeuticas = new HashSet<ManiobrasTerapeuticas>();
            this.AgenteTermico = new HashSet<AgenteTermico>();
        }
    
        public int HistoricoId { get; set; }
        public int PersonaId { get; set; }
        public string Diagnostico { get; set; }
        public string Observaciones { get; set; }
        public string Otros { get; set; }
        public string Usuariocreacion { get; set; }
        public System.DateTime Fechacreacion { get; set; }
        public string Usuariomodificacion { get; set; }
        public Nullable<System.DateTime> Fechamodificacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgenteElectrofisico> AgenteElectrofisico { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Frecuencia> Frecuencia { get; set; }
        public virtual Persona Persona { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManiobrasTerapeuticas> ManiobrasTerapeuticas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgenteTermico> AgenteTermico { get; set; }
    }
}
