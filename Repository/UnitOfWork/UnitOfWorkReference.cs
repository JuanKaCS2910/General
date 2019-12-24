namespace Repository.UnitOfWork
{
    using Repository.GenericRepository;
    using System;

    public partial class UnitOfWork : IDisposable
    {

        #region Private
        //private GenericRepository<AgenteElectrofisico> agenteelectrofisicoRepository;
        private GenericRepository<Distrito> distritoRepository;
        private GenericRepository<Persona> personaRepository;
        private GenericRepository<Tipodocumento> tipodocumentoRepository;
        private GenericRepository<Sexo> sexoRepository;

        private GenericRepository<Historico> historicoRepository;
        private GenericRepository<AgenteElectrofisico> agenteelectrofisicoRepository;
        private GenericRepository<AgenteTermico> agentetermicoRepository;
        private GenericRepository<ManiobrasTerapeuticas> maniobrasterapeuticasRepository;
        private GenericRepository<Antecedentes> antecedentesRepository;

        private GenericRepository<SubTramite> subtramiteRepository;
        #endregion

        #region Public

        public GenericRepository<Distrito> DistritoRepository
        {
            get
            {

                if (this.distritoRepository == null)
                {
                    this.distritoRepository = new GenericRepository<Distrito>(context);
                }
                return distritoRepository;
            }
        }

        public GenericRepository<Persona> PersonaRepository
        {
            get
            {

                if (this.personaRepository == null)
                {
                    this.personaRepository = new GenericRepository<Persona>(context);
                }
                return personaRepository;
            }
        }

        public GenericRepository<Tipodocumento> TipoDocumentoRepository
        {
            get
            {
                if (this.tipodocumentoRepository == null)
                {
                    this.tipodocumentoRepository = new GenericRepository<Tipodocumento>(context);
                }
                return tipodocumentoRepository;
            }
        }

        public GenericRepository<Sexo> SexoRepository
        {
            get
            {
                if (this.sexoRepository == null)
                {
                    this.sexoRepository = new GenericRepository<Sexo>(context);
                }
                return sexoRepository;
            }
        }
        /// <summary>
        /// Histórico
        /// </summary>
        public GenericRepository<Historico> HistoricoRepository
        {
            get
            {
                if (this.historicoRepository == null)
                {
                    this.historicoRepository = new GenericRepository<Historico>(context);
                }
                return historicoRepository;
            }
        }

        /// <summary>
        /// AgenteElectrofísico
        /// </summary>
        public GenericRepository<AgenteElectrofisico> AgenteElectrofisicoRepository
        {
            get
            {
                if (this.agenteelectrofisicoRepository == null)
                {
                    this.agenteelectrofisicoRepository = new GenericRepository<AgenteElectrofisico>(context);
                }
                return agenteelectrofisicoRepository;
            }
        }

        /// <summary>
        /// Agente Térmico
        /// </summary>
        public GenericRepository<AgenteTermico> AgenteTermicoRepository
        {
            get
            {
                if (this.agentetermicoRepository == null)
                {
                    this.agentetermicoRepository = new GenericRepository<AgenteTermico>(context);
                }
                return agentetermicoRepository;
            }
        }

        /// <summary>
        /// Maniobras Terapeuricas
        /// </summary>
        public GenericRepository<ManiobrasTerapeuticas> ManiobrasTerapeuticasRepository
        {
            get
            {
                if (this.maniobrasterapeuticasRepository == null)
                {
                    this.maniobrasterapeuticasRepository = new GenericRepository<ManiobrasTerapeuticas>(context);
                }
                return maniobrasterapeuticasRepository;
            }
        }

        public GenericRepository<SubTramite> SubTramiteRepository
        {
            get
            {
                if (this.subtramiteRepository == null)
                {
                    this.subtramiteRepository = new GenericRepository<SubTramite>(context);
                }
                return subtramiteRepository;
            }
        }

        public GenericRepository<Antecedentes> AntecedentesRepository
        {
            get
            {
                if (this.antecedentesRepository == null)
                {
                    this.antecedentesRepository = new GenericRepository<Antecedentes>(context);
                }
                return antecedentesRepository;
            }
        }

        #endregion

    }
}
