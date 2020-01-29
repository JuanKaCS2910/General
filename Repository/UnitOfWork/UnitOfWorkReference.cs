using Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
    public partial class UnitOfWork : IDisposable
    {

        #region Private
        private GenericRepository<Persona> personaRepository;
        private GenericRepository<Distrito> distritoRepository;
        #endregion

        #region Public

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

        #endregion

    }
}
