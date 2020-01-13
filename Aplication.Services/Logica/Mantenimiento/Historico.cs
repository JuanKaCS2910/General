using Aplication.Services.Interfaz;
using AutoMapper;
using Domain.Entities.Mantenimiento;
using PagedList;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
//using System.Activities.Statements;
using System.Transactions;
using Utility;

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
                            SubTramiteId = (int)SubTramites.CompresaCaliente, //Compresa Caliente
                            Condicion = registro.checkCaliente,
                        });
                        Repository.AgenteTermico term1 = AddTermicoPerson(new EAgentetermico
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.CompresaFria, //Compresa Fria
                            Condicion = registro.checkFria,
                        });
                        Repository.AgenteTermico term2 = AddTermicoPerson(new EAgentetermico
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.CompresaContraste, //Compresa Contraste
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
                            SubTramiteId = (int)SubTramites.Electroanalgesico, //Electroanalgesico
                            Condicion = false,
                            Descripcion = registro.descElectroanalgesico
                        });
                        Repository.AgenteElectrofisico Efisico1 = AddElectrofisico(new EAgenteelectrofisico
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.ElectroEstimulación, //ElectroEstimulación
                            Condicion = registro.checkElectroestimulacion,
                            Descripcion = registro.descElectroestimulacion
                        });
                        Repository.AgenteElectrofisico Efisico2 = AddElectrofisico(new EAgenteelectrofisico
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.Magnetoterapia, //Magnetoterapia
                            Condicion = registro.checkMagnetoterapia,
                            Descripcion = registro.descMagnetoterapia
                        });
                        Repository.AgenteElectrofisico Efisico3 = AddElectrofisico(new EAgenteelectrofisico
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.Ultrasonido, //Ultrasonido
                            Condicion = registro.checkUltrasonido,
                            Descripcion = registro.descUltrasonido
                        });
                        Repository.AgenteElectrofisico Efisico4 = AddElectrofisico(new EAgenteelectrofisico
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.TCombinada, //T. Combinada
                            Condicion = registro.checkTCombinada,
                            Descripcion = registro.descTCombinada
                        });
                        Repository.AgenteElectrofisico Efisico5 = AddElectrofisico(new EAgenteelectrofisico
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.Laserterapia, //Laserterapia
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
                            SubTramiteId = (int)SubTramites.MasajeRelajante, //Masaje Relajante
                            Condicion = registro.checkRelajante
                        });
                        Repository.ManiobrasTerapeuticas ETerapeutica1 = AddManiobraTerapeutica(new EManiobrasTerapeuticas
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.MasajeDescontracturante, //Masaje Descontracturante
                            Condicion = registro.checkDescontracturante
                        });
                        Repository.ManiobrasTerapeuticas ETerapeutica2 = AddManiobraTerapeutica(new EManiobrasTerapeuticas
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.Estiramiento, //Estiramiento
                            Condicion = registro.checkEstiramiento
                        });
                        Repository.ManiobrasTerapeuticas ETerapeutica3 = AddManiobraTerapeutica(new EManiobrasTerapeuticas
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.Fortalecimiento, //Fortalecimiento
                            Condicion = registro.checkFortalecimiento
                        });
                        Repository.ManiobrasTerapeuticas ETerapeutica4 = AddManiobraTerapeutica(new EManiobrasTerapeuticas
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.RPG,//RPG
                            Condicion = registro.checkRPG
                        });
                        Repository.ManiobrasTerapeuticas ETerapeutica5 = AddManiobraTerapeutica(new EManiobrasTerapeuticas
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.ActivacionMimicaF, //Activacion Mimica F.
                            Condicion = registro.checkActivacion
                        });
                        Repository.ManiobrasTerapeuticas ETerapeutica6 = AddManiobraTerapeutica(new EManiobrasTerapeuticas
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.TAPE, //TAPE
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
                        }
                        catch (Exception ex)
                        {
                            mensaje = "Inconveniente al grabar Maniobra Terapeutica";
                            transaction.Dispose();
                            //throw;
                        }

                        #endregion

                        #region Antecedentes

                        Repository.Antecedentes EAntecedentes = AddAntecedentes(new EAntecedentes
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.RiesgoCaida, //RIESGO DE CAIDAS
                            Condicion = registro.checkRCaida
                        });
                        Repository.Antecedentes EAntecedentes1 = AddAntecedentes(new EAntecedentes
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.EstaEmbarazada, //ESTA EMBARAZADA
                            Condicion = registro.checkEEmbarazada
                        });
                        Repository.Antecedentes EAntecedentes2 = AddAntecedentes(new EAntecedentes
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.TieneDiabetes, //TIENE DIABETES
                            Condicion = registro.checkTDiabetes
                        });
                        Repository.Antecedentes EAntecedentes3 = AddAntecedentes(new EAntecedentes
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.DiagnosticoCancer, //DIAGNOSTICO DE CÁNCER
                            Condicion = registro.checkTDiabetes
                        });
                        Repository.Antecedentes EAntecedentes4 = AddAntecedentes(new EAntecedentes
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.EnfermedadCardiaca, //TIENE ENFERMEDAD CARDIACA
                            Condicion = registro.checkTEnfCardiaca
                        });
                        Repository.Antecedentes EAntecedentes5 = AddAntecedentes(new EAntecedentes
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.RiesgoQuemadura, //RIESGO DE QUEMADURAS
                            Condicion = registro.checkRQuemadura
                        });
                        Repository.Antecedentes EAntecedentes6 = AddAntecedentes(new EAntecedentes
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.Varices, //PRESENTA VARICES
                            Condicion = registro.checkPVarices
                        });
                        Repository.Antecedentes EAntecedentes7 = AddAntecedentes(new EAntecedentes
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.HTA, //TIENE HTA
                            Condicion = registro.checkHTA
                        });
                        Repository.Antecedentes EAntecedentes8 = AddAntecedentes(new EAntecedentes
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.Marcapaso, //USA MARCAPASO
                            Condicion = registro.checkMarcapaso
                        });
                        Repository.Antecedentes EAntecedentes9 = AddAntecedentes(new EAntecedentes
                        {
                            HistoricoId = nMaxHistoricoId,
                            SubTramiteId = (int)SubTramites.Osteosintesis, //TIENE ELEMENTOS OSTEOSINTESIS
                            Condicion = registro.checkEOsteosintesis
                        });

                        try
                        {
                            oUnitOfWork.AntecedentesRepository.Insert(EAntecedentes);
                            oUnitOfWork.AntecedentesRepository.Insert(EAntecedentes1);
                            oUnitOfWork.AntecedentesRepository.Insert(EAntecedentes2);
                            oUnitOfWork.AntecedentesRepository.Insert(EAntecedentes3);
                            oUnitOfWork.AntecedentesRepository.Insert(EAntecedentes4);
                            oUnitOfWork.AntecedentesRepository.Insert(EAntecedentes5);
                            oUnitOfWork.AntecedentesRepository.Insert(EAntecedentes6);
                            oUnitOfWork.AntecedentesRepository.Insert(EAntecedentes7);
                            oUnitOfWork.AntecedentesRepository.Insert(EAntecedentes8);
                            oUnitOfWork.AntecedentesRepository.Insert(EAntecedentes9);
                            oUnitOfWork.Save();
                            transaction.Complete();
                            mensaje = "OK";
                        }
                        catch (Exception ex)
                        {
                            mensaje = "Inconveniente al grabar Antecedentes";
                            transaction.Dispose();
                            //throw;
                        }

                        #endregion

                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null &&
                        ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("FK_dbo.Historico"))
                        {
                            mensaje = "No se puede registrar al Histórico.";
                        }
                        else
                        {
                            mensaje = "Inconveniente al grabar Historico";
                        }
                        transaction.Dispose();
                        //throw;
                    }
                    #endregion

                }
            }
            return mensaje;

        }

        public string Edit(EHistorico registro)
        {
            string mensaje = "";
            registro.Usuariocreacion = "SYSTEM";

            using (var transaction = new TransactionScope())
            {
                using (var context = new UnitOfWork())
                {
                    var agenteT = oUnitOfWork.AgenteTermicoRepository.Queryable();
                    var agenteE = oUnitOfWork.AgenteElectrofisicoRepository.Queryable();
                    var maniobra = oUnitOfWork.ManiobrasTerapeuticasRepository.Queryable();
                    var antecedente = oUnitOfWork.AntecedentesRepository.Queryable();
                    var history = oUnitOfWork.HistoricoRepository.Queryable();

                    #region Agente Térmico

                    var _result = (from a in agenteT
                                   where a.HistoricoId == registro.HistoricoId
                                    select new EAgentetermico
                                    {
                                        AgenteTermicoId = a.AgenteTermicoId,
                                        HistoricoId = a.HistoricoId,
                                        SubTramiteId = a.SubTramiteId,
                                        Condicion = (bool)a.Condicion,
                                        Usuariocreacion = a.Usuariocreacion,
                                        Fechacreacion = a.Fechacreacion
                                    }).ToList();


                    foreach (var item in _result)
                    {
                        Repository.AgenteTermico agenT = new Repository.AgenteTermico();
                        agenT.AgenteTermicoId = item.AgenteTermicoId;
                        agenT.SubTramiteId = item.SubTramiteId;
                        agenT.HistoricoId = item.HistoricoId;
                        agenT.Condicion = item.Condicion;
                        
                        agenT.Usuariocreacion = item.Usuariocreacion;
                        agenT.Fechacreacion = item.Fechacreacion;
                        agenT.Fechamodificacion = DateTime.Now;
                        agenT.Usuariomodificacion = registro.Usuariocreacion;

                        if (item.SubTramiteId == (int)SubTramites.CompresaCaliente)
                        {
                            agenT.Condicion = registro.checkCaliente;
                        }
                        else if (item.SubTramiteId == (int)SubTramites.CompresaFria)
                        {
                            agenT.Condicion = registro.checkFria;
                        }
                        else if (item.SubTramiteId == (int)SubTramites.CompresaContraste)
                        {
                            agenT.Condicion = registro.checkContraste;
                        }

                        try
                        {
                            oUnitOfWork.AgenteTermicoRepository.Update(agenT);
                            oUnitOfWork.Save();
                        }
                        catch (Exception ex)
                        {
                            mensaje = "Inconveniente al Actualizar Agente Térmico";
                            transaction.Dispose();
                        }
                    }

                    #endregion

                    #region AgenteElectrofísico

                    var _resultE = (from a in agenteE
                                    where a.HistoricoId == registro.HistoricoId
                                    select new EAgenteelectrofisico
                                    {
                                        AgenteElectrofisicoId = a.AgenteElectrofisicoId,
                                        HistoricoId = a.HistoricoId,
                                        SubTramiteId = a.SubTramiteId,
                                        Condicion = (bool)a.Condicion,
                                        Descripcion = a.Descripcion,
                                        Usuariocreacion = a.Usuariocreacion,
                                        Fechacreacion = a.Fechacreacion
                                    }).ToList();

                    foreach (var item in _resultE)
                    {
                        Repository.AgenteElectrofisico agenE = new Repository.AgenteElectrofisico();
                        agenE.AgenteElectrofisicoId = item.AgenteElectrofisicoId;
                        agenE.HistoricoId = item.HistoricoId;
                        agenE.SubTramiteId = item.SubTramiteId;
                        agenE.Condicion = item.Condicion;
                        agenE.Descripcion = item.Descripcion;
                        
                        agenE.Usuariocreacion = item.Usuariocreacion;
                        agenE.Fechacreacion = item.Fechacreacion;
                        agenE.Fechamodificacion = DateTime.Now;
                        agenE.Usuariomodificacion = registro.Usuariocreacion;

                        switch (item.SubTramiteId)
                        {
                            case (int)SubTramites.Electroanalgesico:
                                agenE.Condicion = false;
                                agenE.Descripcion = registro.descElectroanalgesico;
                                break;
                            case (int)SubTramites.ElectroEstimulación:
                                agenE.Condicion = registro.checkElectroestimulacion;
                                agenE.Descripcion = registro.descElectroestimulacion;
                                break;
                            case (int)SubTramites.Magnetoterapia:
                                agenE.Condicion = registro.checkMagnetoterapia;
                                agenE.Descripcion = registro.descMagnetoterapia;
                                break;
                            case (int)SubTramites.Ultrasonido:
                                agenE.Condicion = registro.checkUltrasonido;
                                agenE.Descripcion = registro.descUltrasonido;
                                break;
                            case (int)SubTramites.TCombinada:
                                agenE.Condicion = registro.checkTCombinada;
                                agenE.Descripcion = registro.descTCombinada;
                                break;
                            case (int)SubTramites.Laserterapia:
                                agenE.Condicion = registro.checkLaserterapia;
                                agenE.Descripcion = registro.descLaserterapia;
                                break;
                            default:
                                break;
                        }

                        try
                        {
                            oUnitOfWork.AgenteElectrofisicoRepository.Update(agenE);
                            oUnitOfWork.Save();
                        }
                        catch (Exception ex)
                        {
                            mensaje = "Inconveniente al Actualizar Agente Electrofísico";
                            transaction.Dispose(); ;
                        }

                    }

                    #endregion

                    #region Maniobra
                    var _resultM = (from a in maniobra
                                    where a.HistoricoId == registro.HistoricoId
                                    select new EManiobrasTerapeuticas
                                    {
                                        ManiobrasTerapeuticasId = a.ManiobrasTerapeuticasId,
                                        HistoricoId = a.HistoricoId,
                                        SubTramiteId = a.SubTramiteId,
                                        Condicion = (bool)a.Condicion,
                                        Usuariocreacion = a.Usuariocreacion,
                                        Fechacreacion = a.Fechacreacion
                                    }).ToList();

                    foreach (var item in _resultM)
                    {
                        Repository.ManiobrasTerapeuticas maniobraT = new Repository.ManiobrasTerapeuticas();
                        maniobraT.ManiobrasTerapeuticasId = item.ManiobrasTerapeuticasId;
                        maniobraT.HistoricoId = item.HistoricoId;
                        maniobraT.SubTramiteId = item.SubTramiteId;
                        maniobraT.Condicion = item.Condicion;
                        
                        maniobraT.Usuariocreacion = item.Usuariocreacion;
                        maniobraT.Fechacreacion = item.Fechacreacion;
                        maniobraT.Fechamodificacion = DateTime.Now;
                        maniobraT.Usuariomodificacion = registro.Usuariocreacion;

                        switch (item.SubTramiteId)
                        {
                            case (int)SubTramites.MasajeRelajante:
                                maniobraT.Condicion = registro.checkRelajante;
                                break;
                            case (int)SubTramites.MasajeDescontracturante:
                                maniobraT.Condicion = registro.checkDescontracturante;
                                break;
                            case (int)SubTramites.Estiramiento:
                                maniobraT.Condicion = registro.checkEstiramiento;
                                break;
                            case (int)SubTramites.Fortalecimiento:
                                maniobraT.Condicion = registro.checkFortalecimiento;
                                break;
                            case (int)SubTramites.RPG:
                                maniobraT.Condicion = registro.checkRPG;
                                break;
                            case (int)SubTramites.ActivacionMimicaF:
                                maniobraT.Condicion = registro.checkActivacion;
                                break;
                            case (int)SubTramites.TAPE:
                                maniobraT.Condicion = registro.checkTAPE;
                                break;
                            default:
                                break;
                        }

                        try
                        {
                            oUnitOfWork.ManiobrasTerapeuticasRepository.Update(maniobraT);
                            oUnitOfWork.Save();
                        }
                        catch (Exception ex)
                        {
                            mensaje = "Inconveniente al Actualizar las maniobras Terapeuticas";
                            transaction.Dispose(); ;
                        }

                    }

                    #endregion

                    #region Antecedentes

                    var _resultA = (from a in antecedente
                                    where a.HistoricoId == registro.HistoricoId
                                    select new EAntecedentes
                                    {
                                        AntecedentesId = a.AntecedentesId,
                                        HistoricoId = a.HistoricoId,
                                        SubTramiteId = a.SubTramiteId,
                                        Condicion = (bool)a.Condicion,
                                        Usuariocreacion = a.Usuariocreacion,
                                        Fechacreacion = a.Fechacreacion
                                    }).ToList();

                    foreach (var item in _resultA)
                    {
                        Repository.Antecedentes antece = new Repository.Antecedentes();
                        antece.AntecedentesId = item.AntecedentesId;
                        antece.HistoricoId = item.HistoricoId;
                        antece.SubTramiteId = item.SubTramiteId;
                        antece.Condicion = item.Condicion;

                        antece.Usuariocreacion = item.Usuariocreacion;
                        antece.Fechacreacion = item.Fechacreacion;
                        antece.Fechamodificacion = DateTime.Now;
                        antece.Usuariomodificacion = registro.Usuariocreacion;

                        switch (item.SubTramiteId)
                        {
                            case (int)SubTramites.RiesgoCaida:
                                antece.Condicion = registro.checkRCaida;
                                break;
                            case (int)SubTramites.EstaEmbarazada:
                                antece.Condicion = registro.checkEEmbarazada;
                                break;
                            case (int)SubTramites.TieneDiabetes:
                                antece.Condicion = registro.checkTDiabetes;
                                break;
                            case (int)SubTramites.DiagnosticoCancer:
                                antece.Condicion = registro.checkTDiabetes;
                                break;
                            case (int)SubTramites.EnfermedadCardiaca:
                                antece.Condicion = registro.checkTEnfCardiaca;
                                break;
                            case (int)SubTramites.RiesgoQuemadura:
                                antece.Condicion = registro.checkRQuemadura;
                                break;
                            case (int)SubTramites.Varices:
                                antece.Condicion = registro.checkPVarices;
                                break;
                            case (int)SubTramites.HTA:
                                antece.Condicion = registro.checkHTA;
                                break;
                            case (int)SubTramites.Marcapaso:
                                antece.Condicion = registro.checkMarcapaso;
                                break;
                            case (int)SubTramites.Osteosintesis:
                                antece.Condicion = registro.checkEOsteosintesis;
                                break;
                            default:
                                break;
                                
                        }
                        
                        try
                        {
                            oUnitOfWork.AntecedentesRepository.Update(antece);
                            oUnitOfWork.Save(); 
                        }
                        catch (Exception ex)
                        {
                            mensaje = "Inconveniente al Actualizar los antecedentes" +
                                "";
                            
                        }
                        //catch (DbEntityValidationException e)
                        //{
                        //    foreach (var eve in e.EntityValidationErrors)
                        //    {
                        //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        //        foreach (var ve in eve.ValidationErrors)
                        //        {
                        //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                        //                ve.PropertyName, ve.ErrorMessage);
                        //        }
                        //    }
                        //    transaction.Dispose();
                        //}
                        
                    }

                    #endregion
                    
                    #region Historico
                    var _history = (from a in history
                                    where a.HistoricoId == registro.HistoricoId
                                    select new EHistorico
                                    {
                                        HistoricoId = a.HistoricoId,
                                        PersonaId = a.PersonaId,
                                        Diagnostico = a.Diagnostico,
                                        Observaciones = a.Observaciones,
                                        Otros = a.Otros,
                                        Paquetes = a.Paquetes,
                                        Costo = a.Costo,
                                        Frecuencia = a.Frecuencia1,
                                        Usuariocreacion = a.Usuariocreacion,
                                        Fechacreacion = a.Fechacreacion
                                    }).ToList();

                    foreach (var item in _history)
                    {
                        Repository.Historico historicoU = new Repository.Historico();
                        historicoU.HistoricoId = item.HistoricoId;
                        historicoU.PersonaId = item.PersonaId;
                        historicoU.Diagnostico = registro.Diagnostico;
                        historicoU.Observaciones = registro.Observaciones == null ? "" : registro.Observaciones.ToUpper();
                        historicoU.Otros = registro.Otros == null ? "": registro.Otros.ToUpper();
                        historicoU.Paquetes = registro.Paquetes;
                        historicoU.Costo = registro.Costo;
                        historicoU.Frecuencia1 = registro.Frecuencia;
                        historicoU.Fechacita = registro.Fechacita;
                        historicoU.Horacita = registro.Horacita;

                        historicoU.Usuariocreacion = item.Usuariocreacion;
                        historicoU.Fechacreacion = item.Fechacreacion;
                        historicoU.Fechamodificacion = DateTime.Now;
                        historicoU.Usuariomodificacion = registro.Usuariocreacion;

                        try
                        {
                            oUnitOfWork.HistoricoRepository.Update(historicoU);
                            oUnitOfWork.Save();
                            transaction.Complete();
                            mensaje = "OK";
                        }
                        catch (Exception ex)
                        {
                            mensaje = "Inconveniente al Actualizar el Histórico";
                            transaction.Dispose();
                        }
                        //catch (DbUpdateConcurrencyException ex)
                        //{
                        //    var entry = ex.Entries.Single();
                        //    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                        //}

                    }
                    #endregion
    
                }
            }
            return mensaje;
        }

        public string Delete(int idHistorico)
        {
            string mensaje = "";

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var context = new UnitOfWork())
                {
                    var agenteT = oUnitOfWork.AgenteTermicoRepository.Queryable();
                    var agenteE = oUnitOfWork.AgenteElectrofisicoRepository.Queryable();
                    var maniobra = oUnitOfWork.ManiobrasTerapeuticasRepository.Queryable();
                    var antecedente = oUnitOfWork.AntecedentesRepository.Queryable();

                    #region Agente Térmico
                    try
                    {
                        var _result = agenteT.Where(x => x.HistoricoId == idHistorico).Select(x => x.AgenteTermicoId);

                        foreach (var idAgenteTermico in _result)
                        {
                            oUnitOfWork.AgenteTermicoRepository.Delete(idAgenteTermico);
                            oUnitOfWork.Save();
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        mensaje = "Inconveniente al eliminar Agente Térmico";
                        transaction.Dispose();
                        //throw;
                    }
                    #endregion

                    #region AgenteElectrofísico
                    try
                    {
                        var _result = agenteE.Where(x => x.HistoricoId == idHistorico).Select(x => x.AgenteElectrofisicoId);
                        foreach (var idAgenteElectrofisico in _result)
                        {
                            oUnitOfWork.AgenteElectrofisicoRepository.Delete(idAgenteElectrofisico);
                            oUnitOfWork.Save();
                        }
                    }
                    catch (Exception ex)
                    {
                        mensaje = "Inconveniente al eliminar Agente Electrofísico";
                        transaction.Dispose();
                        //throw;
                    }
                    #endregion

                    #region ManiobraTerapeuticas
                    try
                    {
                        var _result = maniobra.Where(x => x.HistoricoId == idHistorico).Select(x => x.ManiobrasTerapeuticasId);
                        foreach (var idManiobra in _result)
                        {
                            oUnitOfWork.ManiobrasTerapeuticasRepository.Delete(idManiobra);
                            oUnitOfWork.Save();
                        }
                    }
                    catch (Exception ex)
                    {
                        mensaje = "Inconveniente al eliminar Maniobras Terapeuticas";
                        transaction.Dispose();
                        //throw;
                    }
                    #endregion

                    #region Antecedentes
                    try
                    {
                        var _result = antecedente.Where(x => x.HistoricoId == idHistorico).Select(x => x.AntecedentesId);
                        foreach (var idAntecedente in _result)
                        {
                            oUnitOfWork.AntecedentesRepository.Delete(idAntecedente);
                            oUnitOfWork.Save();
                        }
                    }
                    catch (Exception ex)
                    {
                        mensaje = "Inconveniente al eliminar Antecedentes";
                        transaction.Dispose();
                        //throw;
                    }
                    #endregion

                    #region Histórico
                    try
                    {
                        oUnitOfWork.HistoricoRepository.Delete(idHistorico);
                        oUnitOfWork.Save();
                        transaction.Complete();
                        mensaje = "OK";
                    }
                    catch (Exception ex)
                    {
                        mensaje = "Inconveniente al eliminar Histórico";
                        transaction.Dispose();
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
            hist.Paquetes = registro.Paquetes;
            hist.Costo = registro.Costo;
            hist.Frecuencia1 = registro.Frecuencia;
            hist.Fechacita = registro.Fechacita;
            hist.Horacita = registro.Horacita;
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

        public Repository.Antecedentes AddAntecedentes(EAntecedentes antecedente)
        {
            Repository.Antecedentes EAntecedente = new Repository.Antecedentes();
            EAntecedente.HistoricoId = antecedente.HistoricoId;
            EAntecedente.SubTramiteId = antecedente.SubTramiteId;
            EAntecedente.Condicion = antecedente.Condicion;
            EAntecedente.Usuariocreacion = "JUCASTRO";
            EAntecedente.Fechacreacion = DateTime.Now;
            return EAntecedente;
        }

        public string CreateHistory(EHistorico registro)
        {
            string mensaje = string.Empty;
            mensaje = Save(registro);
            return mensaje;
        }

        public string EditHistory(EHistorico registro)
        {
            string mensaje = string.Empty;
            mensaje = Edit(registro);
            return mensaje;
        }

        public string DeleteHistory(int idHistorico)
        {
            string mensaje = string.Empty;
            mensaje = Delete(idHistorico);
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
                                 Paquetes = h.Paquetes,
                                 Costo = h.Costo == null ? 0 : h.Costo,
                                 Frecuencia = h.Frecuencia1,
                                 Nombre = p.Nombre,
                                 Apellidopaterno = p.Apellidopaterno,
                                 Apellidomaterno = p.Apellidomaterno,
                                 Nrodocumento = p.Nrodocumento,
                                 TipodocumentoId = p.TipodocumentoId,
                                 PersonaId = p.PersonaId,
                                 Fechacita = h.Fechacita,
                                 Horacita = h.Horacita
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
                               TipodocumentoId = p.TipodocumentoId,
                               PersonaId = p.PersonaId
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
                                  Paquetes = h.Paquetes,
                                  Costo = (decimal)h.Costo,
                                  Frecuencia = h.Frecuencia,
                                  Fechacita = h.Fechacita,
                                  Horacita = h.Horacita
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
            var antecedentes = oUnitOfWork.AntecedentesRepository.Queryable();
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
                              Fechacita = h.Fechacita
                          }).OrderByDescending(d => d.Fechacita).ToList();
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
                              Fechacita = h.Fechacita
                          }).OrderByDescending(d => d.Fechacita).ToList();
            }

             
            var _result = result.ToPagedList((int)pag.page, (int)pag.countrow);

            return _result;

        }

    }
}
