using Aplication.Services.Interfaz;
using Domain.Entities.Mantenimiento;
using Repository;
using Repository.UnitOfWork;
using System;
//using System.Activities.Statements;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Domain.Entities.General;

namespace Aplication.Services.Logica.Mantenimiento
{
    public class Historico : IHistorico
    {
        private readonly UnitOfWork oUnitOfWork;
        public Historico()
        {
            this.oUnitOfWork = new UnitOfWork();
        }

        public List<EPersona> PersonaHistoricoGrilla(int? personId)
        {
            var persona = oUnitOfWork.PersonaRepository.Queryable();
            var historico = oUnitOfWork.HistoricoRepository.Queryable();
            var result = new List<EPersona>();

            if (personId == null)
            {
                result = (from p in persona
                          join h in historico
                          on p.PersonaId equals h.PersonaId
                          select new EPersona
                          {
                              Apellidomaterno = p.Apellidomaterno,
                              Apellidopaterno = p.Apellidopaterno,
                              Direccion = p.Direccion,
                              DistritoId = p.DistritoId,
                              Fecnacimiento = p.Fecnacimiento,
                              Nombre = p.Nombre,
                              Nrodocumento = p.Nrodocumento,
                              Nrotelefono = p.Nrotelefono,
                              Ocupacion = p.Ocupacion,
                              PersonaId = p.PersonaId,
                              SexoId = p.SexoId,
                              TipodocumentoId = p.TipodocumentoId,
                              Usuariocreacion = p.Usuariocreacion,
                              Fechacreacion = p.Fechacreacion,
                              Usuariomodificacion = p.Usuariomodificacion,
                              Fechamodificacion = p.Fechamodificacion
                          }).Distinct().ToList();
            }
            else
            {
                result = (from p in persona
                          join h in historico
                          on p.PersonaId equals h.PersonaId
                          where p.PersonaId == personId
                          select new EPersona
                          {
                              Apellidomaterno = p.Apellidomaterno,
                              Apellidopaterno = p.Apellidopaterno,
                              Direccion = p.Direccion,
                              DistritoId = p.DistritoId,
                              Fecnacimiento = p.Fecnacimiento,
                              Nombre = p.Nombre,
                              Nrodocumento = p.Nrodocumento,
                              Nrotelefono = p.Nrotelefono,
                              Ocupacion = p.Ocupacion,
                              PersonaId = p.PersonaId,
                              SexoId = p.SexoId,
                              TipodocumentoId = p.TipodocumentoId,
                              Usuariocreacion = p.Usuariocreacion,
                              Fechacreacion = p.Fechacreacion,
                              Usuariomodificacion = p.Usuariomodificacion,
                              Fechamodificacion = p.Fechamodificacion
                          }).Distinct().ToList();
            }

            return result;
        }

        #region Save,Edit,Delete

        public string Save(EHistorico registro)
        {
            string mensaje = "";
            using (var transaction = new TransactionScope())
            {
                using (var context = new UnitOfWork())
                {
                    #region Histórico
                    //Histórico.
                    Repository.Historico hist = AddHistorico(registro);
                    try
                    {
                        oUnitOfWork.HistoricoRepository.Insert(hist);
                        oUnitOfWork.Save();
                        
                        #region ObtenerMáxPacientexPersonaId
                        int nMaxHistoricoId = ObtenerHistoricoxPersonId(registro);
                        #endregion

                        #region AgenteTérmico
                        //Agente Térmico
                        Repository.AgenteTermico term = AddTermicoPerson(new EAgentetermico
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = 1, //Compresa Caliente
                            Condicion = registro.checkCaliente,
                        });
                        Repository.AgenteTermico term1 = AddTermicoPerson(new EAgentetermico
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = 2, //Compresa Fria
                            Condicion = registro.checkFria,
                        });
                        Repository.AgenteTermico term2 = AddTermicoPerson(new EAgentetermico
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = 3, //Compresa Contraste
                            Condicion = registro.checkContraste,
                        });

                        try
                        {
                            oUnitOfWork.AgenteTermicoRepository.Insert(term);
                            oUnitOfWork.AgenteTermicoRepository.Insert(term1);
                            oUnitOfWork.AgenteTermicoRepository.Insert(term2);
                            oUnitOfWork.Save();
                        }
                        catch (Exception ex)
                        {
                            mensaje = "Inconveniente al grabar Agente Térmico";
                            transaction.Dispose();
                            //throw;
                        }


                        #endregion

                        #region AgenteElectrofísico
                        Repository.AgenteElectrofisico Efisico = AddElectrofisico(new EAgenteelectrofisico
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = 4, //Electroanalgesico
                            Condicion = false,
                            Descripcion = registro.descElectroanalgesico
                        });
                        Repository.AgenteElectrofisico Efisico1 = AddElectrofisico(new EAgenteelectrofisico
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = 5, //ElectroEstimulación
                            Condicion = registro.checkElectroestimulacion,
                            Descripcion = registro.descElectroestimulacion
                        });
                        Repository.AgenteElectrofisico Efisico2 = AddElectrofisico(new EAgenteelectrofisico
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = 6, //Magnetoterapia
                            Condicion = registro.checkMagnetoterapia,
                            Descripcion = registro.descMagnetoterapia
                        });
                        Repository.AgenteElectrofisico Efisico3 = AddElectrofisico(new EAgenteelectrofisico
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = 7, //Ultrasonido
                            Condicion = registro.checkUltrasonido,
                            Descripcion = registro.descUltrasonido
                        });
                        Repository.AgenteElectrofisico Efisico4 = AddElectrofisico(new EAgenteelectrofisico
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = 8, //T. Combinada
                            Condicion = registro.checkTCombinada,
                            Descripcion = registro.descTCombinada
                        });
                        Repository.AgenteElectrofisico Efisico5 = AddElectrofisico(new EAgenteelectrofisico
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = 9, //Laserterapia
                            Condicion = registro.checkLaserterapia,
                            Descripcion = registro.descLaserterapia
                        });

                        try
                        {
                            oUnitOfWork.AgenteElectrofisicoRepository.Insert(Efisico);
                            oUnitOfWork.AgenteElectrofisicoRepository.Insert(Efisico1);
                            oUnitOfWork.AgenteElectrofisicoRepository.Insert(Efisico2);
                            oUnitOfWork.AgenteElectrofisicoRepository.Insert(Efisico3);
                            oUnitOfWork.AgenteElectrofisicoRepository.Insert(Efisico4);
                            oUnitOfWork.AgenteElectrofisicoRepository.Insert(Efisico5);
                            oUnitOfWork.Save();
                        }
                        catch (Exception ex)
                        {
                            mensaje = "Inconveniente al grabar Agente Electrofísico";
                            transaction.Dispose();
                            //throw;
                        }
                        #endregion

                        #region ManiobraTerapeuticas

                        Repository.ManiobrasTerapeuticas ETerapeutica = AddManiobraTerapeutica(new EManiobrasTerapeuticas
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = 10, //Masaje Relajante
                            Condicion = registro.checkRelajante
                        });

                        Repository.ManiobrasTerapeuticas ETerapeutica1 = AddManiobraTerapeutica(new EManiobrasTerapeuticas
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = 11, //Masaje Descontracturante
                            Condicion = registro.checkDescontracturante
                        });

                        Repository.ManiobrasTerapeuticas ETerapeutica2 = AddManiobraTerapeutica(new EManiobrasTerapeuticas
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = 12, //Estiramiento
                            Condicion = registro.checkEstiramiento
                        });

                        Repository.ManiobrasTerapeuticas ETerapeutica3 = AddManiobraTerapeutica(new EManiobrasTerapeuticas
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = 13, //Fortalecimiento
                            Condicion = registro.checkFortalecimiento
                        });

                        Repository.ManiobrasTerapeuticas ETerapeutica4 = AddManiobraTerapeutica(new EManiobrasTerapeuticas
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = 14, //Fortalecimiento
                            Condicion = registro.checkRPG
                        });

                        Repository.ManiobrasTerapeuticas ETerapeutica5 = AddManiobraTerapeutica(new EManiobrasTerapeuticas
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = 15, //Activacion Mimica F.
                            Condicion = registro.checkActivacion
                        });

                        Repository.ManiobrasTerapeuticas ETerapeutica6 = AddManiobraTerapeutica(new EManiobrasTerapeuticas
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = 16, //TAPE
                            Condicion = registro.checkTAPE
                        });

                        try
                        {
                            oUnitOfWork.ManiobrasTerapeuticasRepository.Insert(ETerapeutica);
                            oUnitOfWork.ManiobrasTerapeuticasRepository.Insert(ETerapeutica1);
                            oUnitOfWork.ManiobrasTerapeuticasRepository.Insert(ETerapeutica2);
                            oUnitOfWork.ManiobrasTerapeuticasRepository.Insert(ETerapeutica3);
                            oUnitOfWork.ManiobrasTerapeuticasRepository.Insert(ETerapeutica4);
                            oUnitOfWork.ManiobrasTerapeuticasRepository.Insert(ETerapeutica5);
                            oUnitOfWork.ManiobrasTerapeuticasRepository.Insert(ETerapeutica6);
                            oUnitOfWork.Save();
                            transaction.Complete();
                            mensaje = "OK";
                        }
                        catch (Exception ex)
                        {
                            mensaje = "Inconveniente al grabar Agente Electrofísico";
                            transaction.Dispose();
                            //throw;
                        }

                        #endregion

                    }
                    catch (Exception ex)
                    {
                        mensaje = "Inconveniente al grabar Historico";
                        transaction.Dispose();
                        //throw;
                    }
                    #endregion

                }
            }
            return mensaje;

        }

        /// <summary>
        /// Agregar Historico
        /// </summary>
        /// <param name="registro"></param>
        /// <returns></returns>
        public Repository.Historico AddHistorico(EHistorico registro)
        {
            Repository.Historico hist = new Repository.Historico();
            hist.PersonaId = registro.PersonaId;
            hist.Diagnostico = registro.Diagnostico == null ? "" : registro.Diagnostico.ToUpper();
            hist.Observaciones = registro.Observaciones == null ? "" : registro.Observaciones.ToUpper();
            hist.Otros = registro.Otros == null ? "" : registro.Otros.ToUpper();
            hist.Fechacreacion = DateTime.Now;
            hist.Usuariocreacion = "JUCASTRO";

            return hist;
        }

        /// <summary>
        /// Add Agente Térmico
        /// </summary>
        /// <param name="termico"></param>
        /// <returns></returns>
        public Repository.AgenteTermico AddTermicoPerson(EAgentetermico termico)
        {
            Repository.AgenteTermico term = new Repository.AgenteTermico();
            term.HistoricoId = termico.HistoricoId;
            term.SubTramiteId = termico.SubTramiteId;
            term.Condicion = termico.Condicion;
            term.Usuariocreacion = "JUCASTRO";
            term.Fechacreacion = DateTime.Now;
            return term;
        }

        public Repository.AgenteElectrofisico AddElectrofisico(EAgenteelectrofisico fisico)
        {
            Repository.AgenteElectrofisico Efisico = new Repository.AgenteElectrofisico();
            Efisico.HistoricoId = fisico.HistoricoId;
            Efisico.SubTramiteId = fisico.SubTramiteId;
            Efisico.Condicion = fisico.Condicion;
            Efisico.Descripcion = fisico.Descripcion;
            Efisico.Usuariocreacion = "JUCASTRO";
            Efisico.Fechacreacion = DateTime.Now;
            return Efisico;
        }

        public Repository.ManiobrasTerapeuticas AddManiobraTerapeutica(EManiobrasTerapeuticas terapeutica)
        {
            Repository.ManiobrasTerapeuticas ETerapeutica = new Repository.ManiobrasTerapeuticas();
            ETerapeutica.HistoricoId = terapeutica.HistoricoId;
            ETerapeutica.SubTramiteId = terapeutica.SubTramiteId;
            ETerapeutica.Condicion = terapeutica.Condicion;
            ETerapeutica.Usuariocreacion = "JUCASTRO";
            ETerapeutica.Fechacreacion = DateTime.Now;
            return ETerapeutica;
        }

        public string CreateHistory(EHistorico registro)
        {
            string mensaje = string.Empty;
            mensaje = Save(registro);
            return mensaje;
        }

        #endregion

        public int ObtenerHistoricoxPersonId(EHistorico registro)
        {
            int nMaxPacienteId = 0;

            var Historico = oUnitOfWork.HistoricoRepository.Queryable();
            var result = Historico.Where(x => x.PersonaId == registro.PersonaId).Max(a => a.HistoricoId);
            nMaxPacienteId = result;
            return nMaxPacienteId;
        }

        public EHistoricoView SearchHistorico(int idHistorico)
        {
            var persona = oUnitOfWork.PersonaRepository.Queryable();
            var historico = oUnitOfWork.HistoricoRepository.Queryable();

            var resultado = from h in historico
                             join p in persona
                             on h.PersonaId equals p.PersonaId
                             where h.HistoricoId == idHistorico
                             select new EHistoricoSearch
                             {
                                 HistoricoId = h.HistoricoId,
                                 Diagnostico = h.Diagnostico,
                                 Observaciones = h.Observaciones,
                                 Otros = h.Otros,
                                 Nombre = p.Nombre,
                                 Apellidopaterno = p.Apellidopaterno,
                                 Apellidomaterno = p.Apellidomaterno,
                                 Nrodocumento = p.Nrodocumento,
                                 TipodocumentoId = p.TipodocumentoId,
                             };

            List<EPersona> _person = personaSearch(resultado);
            List<EHistorico> _historico = historicoSearch(resultado);
            List<EAgenteelectrofisico> _agenteEF = agenteElectrofisicoSearch(resultado);
            List<EAgentetermico> _agenteTE = agenteTermicoSearch(resultado);
            List<EManiobrasTerapeuticas> _maniobraTE = maniobraTerapeuticaSearch(resultado);
            List<EAntecedentes> _antecedente = AntecedentesSearch(resultado);

            var result = new EHistoricoView
            {
                Persona = _person,
                Historico = _historico,
                AgenteElectofisico = _agenteEF,
                AgenteTermico = _agenteTE,
                ManiobraTerapeutica = _maniobraTE,
                Antecedentes = _antecedente,
            };

            return result;
        }

        #region ModelViewHistorico
        public List<EPersona> personaSearch(IQueryable<EHistoricoSearch> result)
        {
            var _person = (from p in result
                           select new EPersona
                           {
                               Nombre = p.Nombre,
                               Apellidopaterno = p.Apellidopaterno,
                               Apellidomaterno = p.Apellidomaterno,
                               Nrodocumento = p.Nrodocumento,
                               TipodocumentoId = p.TipodocumentoId
                           }).Distinct().ToList();

            return _person;
        }

        public List<EHistorico> historicoSearch(IQueryable<EHistoricoSearch> result)
        {
            var _historico = (from h in result
                              select new EHistorico
                              {
                                  HistoricoId = h.HistoricoId,
                                  Diagnostico = h.Diagnostico,
                                  Observaciones = h.Observaciones,
                                  Otros = h.Otros,
                              }).Distinct().ToList();
            return _historico;
        }

        public List<EAgenteelectrofisico> agenteElectrofisicoSearch(IQueryable<EHistoricoSearch> result)
        {
            var agenteEle = oUnitOfWork.AgenteElectrofisicoRepository.Queryable();
            var subTramite = oUnitOfWork.SubTramiteRepository.Queryable();

            var _agenteEF = (from h in result
                             join ae in agenteEle
                             on h.HistoricoId equals ae.HistoricoId
                             join sb in subTramite
                             on ae.SubTramiteId equals sb.SubTramiteId
                             select new EAgenteelectrofisico
                             {
                                 HistoricoId = h.HistoricoId,
                                 Condicion = (bool)ae.Condicion,
                                 SubTramiteId = ae.SubTramiteId,
                                 Descripcion = ae.Descripcion,
                             }).Distinct().ToList();
            return _agenteEF;
        }

        public List<EAgentetermico> agenteTermicoSearch(IQueryable<EHistoricoSearch> result)
        {
            var agenteTE = oUnitOfWork.AgenteTermicoRepository.Queryable();
            var subTramite = oUnitOfWork.SubTramiteRepository.Queryable();

            var _agenteTE = (from h in result
                             join at in agenteTE
                             on h.HistoricoId equals at.HistoricoId
                             join sb in subTramite
                             on at.SubTramiteId equals sb.SubTramiteId
                             select new EAgentetermico
                             {
                                 HistoricoId = h.HistoricoId,
                                 Condicion = (bool)at.Condicion,
                                 SubTramiteId = at.SubTramiteId,
                             }).Distinct().ToList();
            return _agenteTE;
        }

        public List<EManiobrasTerapeuticas> maniobraTerapeuticaSearch(IQueryable<EHistoricoSearch> result)
        {
            var maniobraTE = oUnitOfWork.ManiobrasTerapeuticasRepository.Queryable();
            var subTramite = oUnitOfWork.SubTramiteRepository.Queryable();

            var _maniobraTE = (from h in result
                             join ma in maniobraTE
                             on h.HistoricoId equals ma.HistoricoId
                             join sb in subTramite
                             on ma.SubTramiteId equals sb.SubTramiteId
                             select new EManiobrasTerapeuticas
                             {
                                 HistoricoId = h.HistoricoId,
                                 Condicion = (bool)ma.Condicion,
                                 SubTramiteId = ma.SubTramiteId,
                             }).Distinct().ToList();
            return _maniobraTE;
        }

        public List<EAntecedentes> AntecedentesSearch(IQueryable<EHistoricoSearch> result)
        {
            var antecedentes = oUnitOfWork.ManiobrasTerapeuticasRepository.Queryable();
            var subTramite = oUnitOfWork.SubTramiteRepository.Queryable();

            var _antecedente = (from h in result
                               join a in antecedentes
                               on h.HistoricoId equals a.HistoricoId
                               join sb in subTramite
                               on a.SubTramiteId equals sb.SubTramiteId
                               select new EAntecedentes
                               {
                                   HistoricoId = h.HistoricoId,
                                   Condicion = (bool)a.Condicion,
                                   SubTramiteId = a.SubTramiteId,
                               }).Distinct().ToList();
            return _antecedente;
        }

        #endregion



        public IPagedList<EHistorico> HistoricoGrillaToPageList(FiltroGrilloHistorico pag)
        {
            pag.page = (pag.page ?? 1);
            var historico = oUnitOfWork.HistoricoRepository.Queryable();
            var person = oUnitOfWork.PersonaRepository.Queryable();

            var result = new List<EHistorico>();

            if (pag.PersonaId != 0)
            {
                result = (from h in historico
                          join p in person
                          on h.PersonaId equals p.PersonaId
                          where h.PersonaId == pag.PersonaId
                          select new EHistorico
                          {
                              HistoricoId = h.HistoricoId,
                              NombreCompleto = p.Apellidopaterno + " " + p.Apellidomaterno + " , " + p.Nombre,
                              Diagnostico = h.Diagnostico,
                              Observaciones = h.Observaciones,
                              Otros = h.Otros,
                              Fechacreacion = h.Fechacreacion
                          }).OrderByDescending(d => d.Fechacreacion).ToList();
            }
            else
            {
                result = (from h in historico
                          join p in person
                          on h.PersonaId equals p.PersonaId
                          select new EHistorico
                          {
                              HistoricoId = h.HistoricoId,
                              NombreCompleto = p.Apellidopaterno + " " + p.Apellidomaterno + " , " + p.Nombre,
                              Diagnostico = h.Diagnostico,
                              Observaciones = h.Observaciones,
                              Otros = h.Otros,
                              Fechacreacion = h.Fechacreacion
                          }).OrderByDescending(d => d.Fechacreacion).ToList();
            }

             
            var _result = result.ToPagedList((int)pag.page, (int)pag.countrow);

            return _result;

        }

    }
}
