﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBFisioterapiaEntities : DbContext
    {
        public DBFisioterapiaEntities(string name)
            : base(name)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<Distrito> Distrito { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Sexo> Sexo { get; set; }
        public virtual DbSet<Tipodocumento> Tipodocumento { get; set; }
        public virtual DbSet<Historico> Historico { get; set; }
        public virtual DbSet<SubTramite> SubTramite { get; set; }
        public virtual DbSet<Tramite> Tramite { get; set; }
        public virtual DbSet<AgenteElectrofisico> AgenteElectrofisico { get; set; }
        public virtual DbSet<AgenteTermico> AgenteTermico { get; set; }
        public virtual DbSet<Frecuencia> Frecuencia { get; set; }
        public virtual DbSet<ManiobrasTerapeuticas> ManiobrasTerapeuticas { get; set; }
        public virtual DbSet<Antecedentes> Antecedentes { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
