﻿namespace Repository.UnitOfWork
{
    using Repository.GenericRepository;
    using System;

    public partial class UnitOfWork : IDisposable
    {

        #region Private
        private GenericRepository<AgenteElectrofisico> agenteelectrofisicoRepository;
        private GenericRepository<Distrito> distritoRepository;
        private GenericRepository<Persona> personaRepository;
        private GenericRepository<Tipodocumento> tipodocumentoRepository;
        private GenericRepository<Sexo> sexoRepository;
        #endregion

        #region Public
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

        #endregion

    }
}