using Aplication.Services.Interfaz;
using Domain.Entities.Mantenimiento;
using Repository.UnitOfWork;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Logica.Mantenimiento
{
    public class Historico : IHistorico
    {
        private readonly UnitOfWork oUnitOfWork;
        public Historico()
        {
            this.oUnitOfWork = new UnitOfWork();
        }

        #region Save,Edit,Delete

        public void Save()
        {
            using (var context = new UnitOfWork()) {
                using (var transaction = context..BeginTransaction()) {

                }
            }
            //using (TransactionScope scope = new TransactionScope())
            //{
                
            //    //FacturacionRepository.Save(..); //aqui grabar la factura y el detalle
            //    //
            //    //CtaCte Repository.Save(..);
            //    //
            //    //scope.Complete();

            //}
        }

        public string CreateHistory(EHistorico registro)
        {
            string mensaje = string.Empty;
            
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    scope.Complete();
            //}
            Repository.Historico hist = new Repository.Historico();
            hist.PersonaId = registro.PersonaId;
            hist.Diagnostico = registro.Diagnostico == null ? "" : registro.Diagnostico.ToUpper();
            hist.Observaciones = registro.Observaciones == null ? "": registro.Observaciones.ToUpper();
            hist.Otros = registro.Otros == null ? "" : registro.Otros.ToUpper();
            hist.Fechacreacion = DateTime.Now;
            hist.Usuariocreacion = "JUCASTRO";
            oUnitOfWork.HistoricoRepository.Insert(hist);
            try
            {
                oUnitOfWork.Save();
                mensaje = "OK";
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("Nrodocumento_Index"))
                {
                    mensaje = "El Nro. Documento ya se encuentra registrado";
                }
                else
                {
                    mensaje = ex.Message;
                }
            }

            return mensaje;
        }

        #endregion

    }
}
