using SG_SST.EntidadesDominio.EnfermedadLaboral;
using SG_SST.Interfaces.EnfermedadLaboral;
using SG_SST.Models;
using SG_SST.Models.EnfermedadesLaborales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.EnfermedadesLaborales
{
    public class EnfermedadLaboralManager : IEnfermedadLaboral
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EDEnfermedadLaboral RegistrarEnfermedadLaboralDiagnosticada(EDEnfermedadLaboral enfermedadARegistrar)
        {
            try
            {
                using (var context = new SG_SSTContext())
                {
                    using (var tx = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var codigoCIIE = enfermedadARegistrar.DiagnosticoCIIE10.Split('-')[0];
                            var codigoDiagnostico = context.Tbl_Diagnosticos.Where(d => d.Codigo_CIE.Equals(codigoCIIE)).FirstOrDefault();
                            var enfermedadLaboral = new EnfermedadLaboral();
                            enfermedadLaboral.CodigoEmpleado = enfermedadARegistrar.CodigoEmpleado;
                            enfermedadLaboral.CodigoDiagnosticoCIIE10 = codigoDiagnostico == null ? 0 : codigoDiagnostico.PK_Id_Diagnostico;
                            enfermedadLaboral.Diagnostico = enfermedadARegistrar.Diagnostico;
                            enfermedadLaboral.RutaDocumentoFUREL = enfermedadARegistrar.DocumentoFurel;
                            enfermedadLaboral.RutaCartaEnviadaEPS = enfermedadARegistrar.CartaEPS;
                            enfermedadLaboral.FechaEnvioDocumentosEPS = enfermedadARegistrar.FechaDocumentosCalificarEPS;
                            context.Tbl_EnfermedadesLaboralesDiagnosticadas.Add(enfermedadLaboral);
                            context.SaveChanges();
                            var codEnfermedadLabRegistrada = enfermedadLaboral.Pk_Id_EnfermedadLaboral;
                            var documentosEnviadosEPS = new List<DocumentoEnviadoEPS>();
                            foreach (var rutaDocumento in enfermedadARegistrar.TiposDocumentosEnviadosEPS)
                            {
                                var documentoEnvEPS = new DocumentoEnviadoEPS();
                                documentoEnvEPS.EnfermedadLaboral = enfermedadLaboral;
                                documentoEnvEPS.RutaDocumentoEnviadoEPS = rutaDocumento;
                                documentoEnvEPS.FechaRegistroDocumento = DateTime.Now;
                                documentosEnviadosEPS.Add(documentoEnvEPS);
                            }
                            context.Tbl_DocumentosEnviadosEPS.AddRange(documentosEnviadosEPS);
                            var instanciasEnfLab = new List<InstanciaEnfermedadLaboral>();
                            foreach (var instanciaEnferLaboral in enfermedadARegistrar.InstanciasRegistradas)
                            {
                                var estadoInstancia = context.Tbl_EstadosInstanciasRegistradas.Where(e => e.PK_Id_EstadoInstancia == instanciaEnferLaboral.EstadoInstancia).FirstOrDefault();
                                var instanciaEnfLab = new InstanciaEnfermedadLaboral();
                                instanciaEnfLab.EnfermedadLaboral = enfermedadLaboral;
                                instanciaEnfLab.EstadoInstancia = instanciaEnferLaboral.EstadoInstancia;
                                instanciaEnfLab.EstadoInstanciaRegistrada = estadoInstancia;
                                instanciaEnfLab.FechaCalificacion = instanciaEnferLaboral.FechaCalificacion;
                                instanciaEnfLab.QuienCalifica = instanciaEnferLaboral.QuienCalifica;
                                instanciasEnfLab.Add(instanciaEnfLab);
                            }
                            context.Tbl_InstanciasEnfermedadLaboralDiagnosticada.AddRange(instanciasEnfLab);
                            context.SaveChanges();
                            tx.Commit();
                            enfermedadARegistrar.IdEfermedadLaboral = enfermedadLaboral.Pk_Id_EnfermedadLaboral;
                        }
                        catch (Exception ex)
                        {
                            tx.Rollback();
                            enfermedadARegistrar = null;
                        }
                    }
                    return enfermedadARegistrar;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
