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
            var agenteEle = oUnitOfWork.AgenteElectrofisicoRepository.Queryable();
            var subTramite = oUnitOfWork.SubTramiteRepository.Queryable();

            var resultado = from h in historico
                         join p in persona
                         on h.PersonaId equals p.PersonaId
                         join ae in agenteEle
                         on h.HistoricoId equals ae.HistoricoId
                         join sb in subTramite
                         on ae.SubTramiteId equals sb.SubTramiteId
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
                             SubTramiteId = sb.SubTramiteId,
                             DescripcionST = sb.Descripcion,
                             CodigoST = sb.Codigo,
                             CondicionAE = ae.Condicion,
                             DescripcionAE = ae.Descripcion,
                         };

            
            List<EPersona> _person = personaSearch(resultado);
            List<EHistorico> _historico = historicoSearch(resultado);
            List<EAgenteelectrofisico> _agenteEF = agenteElectrofisicoSearch(resultado);

            var result = new EHistoricoView
            {
                Persona = _person,
                Historico = _historico,
                AgenteElectofisico = _agenteEF,
            };

            return result;

        }

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
            var _agenteEF = (from h in result
                             where h.CodigoST == "AE"
                              select new EAgenteelectrofisico
                              {
                                  HistoricoId = h.HistoricoId,
                                  Condicion = (bool)h.CondicionAE,
                                  SubTramiteId = h.SubTramiteId,
                                  Descripcion = h.DescripcionST,
                                  Codigo = h.CodigoST,
                                  DescripcionAE = h.DescripcionAE
                              }).Distinct().ToList();
            return _agenteEF;
        }

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
                          }).OrderBy(d => d.NombreCompleto)
                                         .ThenBy(d => d.Fechacreacion).ToList();
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
                          }).OrderBy(d => d.NombreCompleto)
                         .ThenBy(d => d.Fechacreacion).ToList();
            }

             
            var _result = result.ToPagedList((int)pag.page, (int)pag.countrow);

            return _result;

        }

    }
}
