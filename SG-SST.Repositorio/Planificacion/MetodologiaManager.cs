using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Models;
using SG_SST.Interfaces.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Models.Planificacion;
using System.Configuration;
using SG_SST.EntidadesDominio.Empresas;


namespace SG_SST.Repositorio.Planificacion
{
    public class MetodologiaManager : IMetodologia
    {
        private int pkTipoMetodologiaGtc45 = Int32.Parse(ConfigurationManager.AppSettings["MetdologiaGTC45"]);
        private int pkTipoMetodologiaRam = Int32.Parse(ConfigurationManager.AppSettings["MetdologiaRAM"]);
        private int pkTipoMetodologiaINSHT = Int32.Parse(ConfigurationManager.AppSettings["MetdologiaINSHT"]);
        public List<EDMetodologia> ObtenerMedologias()
        {
            List<EDMetodologia> metodologias = null;
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                metodologias = (from m in contex.Tbl_Metodologia
                                select new EDMetodologia
                                {
                                    PK_Metodologia = m.PK_Metodologia,
                                    Descripcion_Metodologia = m.Descripcion_Metodologia
                                }
                               ).ToList();
            }

            return metodologias;
        }
        public List<EDMetodologia> ObtenerMedologias(int id_Sede)
        {
            List<EDMetodologia> metodologias = new List<EDMetodologia>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                metodologias = (from p in contex.Tbl_Peligro
                                join cscp in contex.Tbl_Consecuencia_Por_Peligro on p.PK_Peligro equals cscp.FK_Peligro
                                join csc in contex.Tbl_Consecuencias on cscp.FK_Consecuencia equals csc.PK_Consecuencia
                                join gp in contex.Tbl_Grupos on csc.FK_Grupo equals gp.PK_Grupo
                                join m in contex.Tbl_Metodologia on gp.FK_Metodologia equals m.PK_Metodologia
                                where p.FK_Sede == id_Sede
                                select new EDMetodologia
                                 {
                                     PK_Metodologia = m.PK_Metodologia,
                                     Descripcion_Metodologia = m.Descripcion_Metodologia
                                 }).Distinct().ToList();


            }
            return metodologias;

        }

        public List<EDPeligroIdentificadoApp> ObtenerPeligrosIdentificadosApp(int id_Sede, int idMetodologia)
        {
            List<EDPeligroIdentificadoApp> peligrosIdentificados = new List<EDPeligroIdentificadoApp>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                var GrupoTipoPeligros = (from p in contex.Tbl_Peligro
                                         join cscp in contex.Tbl_Consecuencia_Por_Peligro on p.PK_Peligro equals cscp.FK_Peligro
                                         join csc in contex.Tbl_Consecuencias on cscp.FK_Consecuencia equals csc.PK_Consecuencia
                                         join gp in contex.Tbl_Grupos on csc.FK_Grupo equals gp.PK_Grupo
                                         join m in contex.Tbl_Metodologia on gp.FK_Metodologia equals m.PK_Metodologia
                                         where (p.FK_Sede == id_Sede && m.PK_Metodologia == idMetodologia)
                                         group p by new { p.ClasificacionDePeligro.TipoDePeligro.PK_Tipo_De_Peligro, p.ClasificacionDePeligro.TipoDePeligro.Descripcion_Del_Peligro } into clasesPeligros
                                         select clasesPeligros
                                 ).ToList();

                foreach (var tipopeligro in GrupoTipoPeligros)
                {
                    EDPeligroIdentificadoApp peligroIdentificado = new EDPeligroIdentificadoApp();
                    peligroIdentificado.idClasificacionPeligro = tipopeligro.Key.PK_Tipo_De_Peligro;
                    peligroIdentificado.descripcionClasificacion = tipopeligro.Key.Descripcion_Del_Peligro;
                    peligroIdentificado.cantidadDeClasifiacion = tipopeligro.Count();
                    peligrosIdentificados.Add(peligroIdentificado);

                }
            }
            return peligrosIdentificados;
        }
        public List<EDPeligroIdentificadoApp> ObtenerPeligrosIdentificadosFiltroApp(int id_Sede, int idMetodologia, int id_Proceso, string zonaLugar, string actividad)
        {
            List<EDPeligroIdentificadoApp> peligrosIdentificados = new List<EDPeligroIdentificadoApp>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                var GrupoTipoPeligros = (from p in contex.Tbl_Peligro
                                         join cscp in contex.Tbl_Consecuencia_Por_Peligro on p.PK_Peligro equals cscp.FK_Peligro
                                         join csc in contex.Tbl_Consecuencias on cscp.FK_Consecuencia equals csc.PK_Consecuencia
                                         join gp in contex.Tbl_Grupos on csc.FK_Grupo equals gp.PK_Grupo
                                         join m in contex.Tbl_Metodologia on gp.FK_Metodologia equals m.PK_Metodologia
                                         where (p.FK_Sede == id_Sede && m.PK_Metodologia == idMetodologia)
                                         //&& p.FK_Proceso == id_Proceso && p.Lugar == zonaLugar && p.Actividad == Actividad)
                                        // group p by new { p.ClasificacionDePeligro.TipoDePeligro.PK_Tipo_De_Peligro, p.ClasificacionDePeligro.TipoDePeligro.Descripcion_Del_Peligro } into clasesPeligros
                                        // select clasesPeligros
                                        select p);

                if (id_Proceso > 0)
                    GrupoTipoPeligros = GrupoTipoPeligros.Where(p => p.FK_Proceso == id_Proceso);
                if (zonaLugar != "")
                    GrupoTipoPeligros = GrupoTipoPeligros.Where(p => p.Lugar == zonaLugar);
                if (actividad != "")
                    GrupoTipoPeligros = GrupoTipoPeligros.Where(p => p.Actividad == actividad);


                var query = (from p in GrupoTipoPeligros
                            group p by new { p.ClasificacionDePeligro.TipoDePeligro.PK_Tipo_De_Peligro, p.ClasificacionDePeligro.TipoDePeligro.Descripcion_Del_Peligro } into clasesPeligros
                            select clasesPeligros).ToList();

                foreach (var tipopeligro in query)
                {
                    EDPeligroIdentificadoApp peligroIdentificado = new EDPeligroIdentificadoApp();
                    peligroIdentificado.idClasificacionPeligro = tipopeligro.Key.PK_Tipo_De_Peligro;
                    peligroIdentificado.descripcionClasificacion = tipopeligro.Key.Descripcion_Del_Peligro;
                    peligroIdentificado.cantidadDeClasifiacion = tipopeligro.Count();
                    peligrosIdentificados.Add(peligroIdentificado);

                }
            }
            return peligrosIdentificados;
        }
        public List<EDValoracionDeRiesgosApp> ValoracionDeRiesgosApp(int id_Sede, int idMetodologia, int idTipoPeligro)
        {

            List<EDValoracionDeRiesgosApp> peligrosIdentificados = new List<EDValoracionDeRiesgosApp>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                if (idMetodologia == pkTipoMetodologiaGtc45)
                {
                    var GrupoTipoPeligros = (from gtc45 in contex.Tbl_GTC45
                                             join p in contex.Tbl_Peligro on gtc45.FK_Peligro equals p.PK_Peligro
                                             join cscp in contex.Tbl_Consecuencia_Por_Peligro on p.PK_Peligro equals cscp.FK_Peligro
                                             join csc in contex.Tbl_Consecuencias on cscp.FK_Consecuencia equals csc.PK_Consecuencia
                                             join gp in contex.Tbl_Grupos on csc.FK_Grupo equals gp.PK_Grupo
                                             join m in contex.Tbl_Metodologia on gp.FK_Metodologia equals m.PK_Metodologia
                                             where
                                             (p.FK_Sede == id_Sede && m.PK_Metodologia == idMetodologia && p.ClasificacionDePeligro.TipoDePeligro.PK_Tipo_De_Peligro == idTipoPeligro)
                                             select gtc45
                                    ).ToList();
                    foreach (var tipopeligro in GrupoTipoPeligros)
                    {
                        EDValoracionDeRiesgosApp peligroIdentificado = new EDValoracionDeRiesgosApp();
                        peligroIdentificado.InterpretacionRiesgos = ObtenerInterpretacionDeRiesgo(tipopeligro.Nivel_De_Riesgo).Interpretacion;
                        peligroIdentificado.totalInterpretacion = tipopeligro.Nivel_De_Riesgo;
                        peligrosIdentificados.Add(peligroIdentificado);

                    }
                    var listaAgrupada = peligrosIdentificados.GroupBy(x => x.InterpretacionRiesgos).ToList();
                    peligrosIdentificados = new List<EDValoracionDeRiesgosApp>();
                    foreach (var tipoPeligro in listaAgrupada)
                    {
                        EDValoracionDeRiesgosApp peligroIdentificado = new EDValoracionDeRiesgosApp();
                        peligroIdentificado.InterpretacionRiesgos = tipoPeligro.FirstOrDefault().InterpretacionRiesgos;
                        peligroIdentificado.totalInterpretacion = tipoPeligro.Count();
                        peligrosIdentificados.Add(peligroIdentificado);

                    }
                }
                else if (idMetodologia == pkTipoMetodologiaRam)
                {
                    var peligrosMetodologias = (from cscp in contex.Tbl_Consecuencia_Por_Peligro
                                                join csc in contex.Tbl_Consecuencias on cscp.FK_Consecuencia equals csc.PK_Consecuencia
                                                join gp in contex.Tbl_Grupos on csc.FK_Grupo equals gp.PK_Grupo
                                                join m in contex.Tbl_Metodologia on gp.FK_Metodologia equals m.PK_Metodologia
                                                where m.PK_Metodologia == idMetodologia
                                                group m by new { cscp.FK_Peligro, m.Descripcion_Metodologia } into clasesPeligros
                                                select clasesPeligros).ToList();

                    var GrupoTipoPeligros = (from ram in contex.Tbl_RAM
                                             join perexp in contex.Tbl_PersonaExpuesta on ram.FK_Persona_Expuesta equals perexp.PK_Persona_Expuesta
                                             join p in contex.Tbl_Peligro on perexp.FK_Peligro equals p.PK_Peligro
                                             join cp in contex.Tbl_Clasificacion_De_Peligro on p.FK_Clasificacion_De_Peligro equals cp.PK_Clasificacion_De_Peligro
                                             where p.FK_Sede == id_Sede && cp.FK_Tipo_De_Peligro == idTipoPeligro
                                             select ram).ToList();

                    var query = from gtp in GrupoTipoPeligros
                                join pm in peligrosMetodologias on gtp.PersonaExpuesta.FK_Peligro equals pm.Key.FK_Peligro
                                group gtp by new { gtp.Nivel_De_Riesgo } into grupoNivel
                                select grupoNivel;



                    foreach (var tipoPeligro in query)
                    {
                        EDValoracionDeRiesgosApp peligroIdentificado = new EDValoracionDeRiesgosApp();
                        peligroIdentificado.InterpretacionRiesgos = tipoPeligro.Key.Nivel_De_Riesgo;
                        peligroIdentificado.totalInterpretacion = tipoPeligro.Count();
                        peligrosIdentificados.Add(peligroIdentificado);

                    }
                } if (idMetodologia == pkTipoMetodologiaINSHT)
                {
                    var GrupoTipoPeligros = (from inst in contex.Tbl_INSHT
                                             join perexp in contex.Tbl_PersonaExpuesta on inst.FK_Persona_Expuesta equals perexp.PK_Persona_Expuesta
                                             join p in contex.Tbl_Peligro on perexp.FK_Peligro equals p.PK_Peligro
                                             join cscp in contex.Tbl_Consecuencia_Por_Peligro on p.PK_Peligro equals cscp.FK_Peligro
                                             join csc in contex.Tbl_Consecuencias on cscp.FK_Consecuencia equals csc.PK_Consecuencia
                                             join gp in contex.Tbl_Grupos on csc.FK_Grupo equals gp.PK_Grupo
                                             join m in contex.Tbl_Metodologia on gp.FK_Metodologia equals m.PK_Metodologia
                                             where (p.FK_Sede == id_Sede && m.PK_Metodologia == idMetodologia && p.ClasificacionDePeligro.TipoDePeligro.PK_Tipo_De_Peligro == idTipoPeligro)
                                             group p by new { inst.Estimacion_Riesgo } into clasesPeligros
                                             select clasesPeligros
                                 ).ToList();

                    foreach (var tipopeligro in GrupoTipoPeligros)
                    {
                        EDValoracionDeRiesgosApp peligroIdentificado = new EDValoracionDeRiesgosApp();
                        peligroIdentificado.InterpretacionRiesgos = tipopeligro.Key.Estimacion_Riesgo;
                        peligroIdentificado.totalInterpretacion = tipopeligro.Count();
                        peligrosIdentificados.Add(peligroIdentificado);

                    }
                }

            }
            return peligrosIdentificados;
        }

        public List<EDDetalleValoracionRiesgoApp> DetalleValoracionDeRiesgosApp(int id_Sede, int idMetodologia, int idTipoPeligro, string intepretacionRiesgo)
        {
            List<EDDetalleValoracionRiesgoApp> peligrosIdentificados = new List<EDDetalleValoracionRiesgoApp>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                if (idMetodologia == pkTipoMetodologiaGtc45)
                {

                    var GrupoTipoPeligros = (from gtc45 in contex.Tbl_GTC45
                                             join p in contex.Tbl_Peligro on gtc45.FK_Peligro equals p.PK_Peligro
                                             join cscp in contex.Tbl_Consecuencia_Por_Peligro on p.PK_Peligro equals cscp.FK_Peligro
                                             join csc in contex.Tbl_Consecuencias on cscp.FK_Consecuencia equals csc.PK_Consecuencia
                                             join gp in contex.Tbl_Grupos on csc.FK_Grupo equals gp.PK_Grupo
                                             join m in contex.Tbl_Metodologia on gp.FK_Metodologia equals m.PK_Metodologia
                                             where
                                             (p.FK_Sede == id_Sede && m.PK_Metodologia == idMetodologia && p.ClasificacionDePeligro.TipoDePeligro.PK_Tipo_De_Peligro == idTipoPeligro)
                                             select gtc45
                                    ).ToList();
                    foreach (var tipopeligro in GrupoTipoPeligros)
                    {
                        if (intepretacionRiesgo == ObtenerInterpretacionDeRiesgo(tipopeligro.Nivel_De_Riesgo).Interpretacion)
                        {
                            EDDetalleValoracionRiesgoApp peligroIdentificado = new EDDetalleValoracionRiesgoApp();
                            peligroIdentificado.Descripcion = tipopeligro.Peligro.ClasificacionDePeligro.Descripcion_Clase_De_Peligro;
                            peligroIdentificado.Crtl_Exist_Fuente = tipopeligro.Peligro.Fuente;
                            peligroIdentificado.Crtl_Exist_Medio = tipopeligro.Peligro.Medio;
                            peligroIdentificado.Crtl_Exist_Individuo = tipopeligro.Peligro.Accion_De_Prevencion;
                            peligrosIdentificados.Add(peligroIdentificado);
                        }

                    }

                } if (idMetodologia == pkTipoMetodologiaRam) 
                {
                    var peligrosMetodologias = (from cscp in contex.Tbl_Consecuencia_Por_Peligro
                                                join csc in contex.Tbl_Consecuencias on cscp.FK_Consecuencia equals csc.PK_Consecuencia
                                                join gp in contex.Tbl_Grupos on csc.FK_Grupo equals gp.PK_Grupo
                                                join m in contex.Tbl_Metodologia on gp.FK_Metodologia equals m.PK_Metodologia
                                                where m.PK_Metodologia == idMetodologia
                                                group m by new { cscp.FK_Peligro, m.Descripcion_Metodologia } into clasesPeligros
                                                select clasesPeligros).ToList();

                    var GrupoTipoPeligros = (from ram in contex.Tbl_RAM
                                             join perexp in contex.Tbl_PersonaExpuesta on ram.FK_Persona_Expuesta equals perexp.PK_Persona_Expuesta
                                             join p in contex.Tbl_Peligro on perexp.FK_Peligro equals p.PK_Peligro
                                             join cp in contex.Tbl_Clasificacion_De_Peligro on p.FK_Clasificacion_De_Peligro equals cp.PK_Clasificacion_De_Peligro
                                             where p.FK_Sede == id_Sede && cp.FK_Tipo_De_Peligro == idTipoPeligro
                                             select ram).ToList();

                    var query = from gtp in GrupoTipoPeligros
                                join pm in peligrosMetodologias on gtp.PersonaExpuesta.FK_Peligro equals pm.Key.FK_Peligro
                                where gtp.Nivel_De_Riesgo == intepretacionRiesgo
                                select gtp;

                    foreach (var tipopeligro in GrupoTipoPeligros)
                    {
                      
                            EDDetalleValoracionRiesgoApp peligroIdentificado = new EDDetalleValoracionRiesgoApp();
                            peligroIdentificado.Descripcion = tipopeligro.PersonaExpuesta.Peligro.ClasificacionDePeligro.Descripcion_Clase_De_Peligro;
                            peligroIdentificado.Crtl_Exist_Fuente = tipopeligro.PersonaExpuesta.Peligro.Fuente;
                            peligroIdentificado.Crtl_Exist_Medio = tipopeligro.PersonaExpuesta.Peligro.Medio;
                            peligroIdentificado.Crtl_Exist_Individuo = tipopeligro.PersonaExpuesta.Peligro.Accion_De_Prevencion;
                            peligrosIdentificados.Add(peligroIdentificado);
                        

                    }
                } if (idMetodologia == pkTipoMetodologiaINSHT)
                {
                    var GrupoTipoPeligros = (from inst in contex.Tbl_INSHT
                                             join perexp in contex.Tbl_PersonaExpuesta on inst.FK_Persona_Expuesta equals perexp.PK_Persona_Expuesta
                                             join p in contex.Tbl_Peligro on perexp.FK_Peligro equals p.PK_Peligro
                                             join cscp in contex.Tbl_Consecuencia_Por_Peligro on p.PK_Peligro equals cscp.FK_Peligro
                                             join csc in contex.Tbl_Consecuencias on cscp.FK_Consecuencia equals csc.PK_Consecuencia
                                             join gp in contex.Tbl_Grupos on csc.FK_Grupo equals gp.PK_Grupo
                                             join m in contex.Tbl_Metodologia on gp.FK_Metodologia equals m.PK_Metodologia
                                             where (p.FK_Sede == id_Sede && 
                                                    m.PK_Metodologia == idMetodologia && 
                                                    p.ClasificacionDePeligro.TipoDePeligro.PK_Tipo_De_Peligro == idTipoPeligro&&
                                                    inst.Estimacion_Riesgo == intepretacionRiesgo)                                            
                                             select p
                                 ).ToList();

                    foreach (var tipopeligro in GrupoTipoPeligros)
                    {

                        EDDetalleValoracionRiesgoApp peligroIdentificado = new EDDetalleValoracionRiesgoApp();
                        peligroIdentificado.Descripcion = tipopeligro.ClasificacionDePeligro.Descripcion_Clase_De_Peligro;
                        peligroIdentificado.Crtl_Exist_Fuente = tipopeligro.Fuente;
                        peligroIdentificado.Crtl_Exist_Medio = tipopeligro.Medio;
                        peligroIdentificado.Crtl_Exist_Individuo = tipopeligro.Accion_De_Prevencion;
                        peligrosIdentificados.Add(peligroIdentificado);


                    }
                }
            }
            return peligrosIdentificados;
        }

        public List<EDProceso> ProcesosMetodologiaApp(int id_Sede, int idMetodologia)
        {
            List<EDProceso> procesosIdentificados = new List<EDProceso>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                var procesos = (from pro in contex.Tbl_Procesos
                                join p in contex.Tbl_Peligro on pro.Pk_Id_Proceso equals p.FK_Proceso
                                join cscp in contex.Tbl_Consecuencia_Por_Peligro on p.PK_Peligro equals cscp.FK_Peligro
                                join csc in contex.Tbl_Consecuencias on cscp.FK_Consecuencia equals csc.PK_Consecuencia
                                join gp in contex.Tbl_Grupos on csc.FK_Grupo equals gp.PK_Grupo
                                join m in contex.Tbl_Metodologia on gp.FK_Metodologia equals m.PK_Metodologia
                                where (p.FK_Sede == id_Sede && m.PK_Metodologia == idMetodologia)
                                group p by new { pro.Pk_Id_Proceso, pro.Descripcion_Proceso } into clasesPeligros
                                select clasesPeligros
                                 ).ToList();

                foreach (var proceso in procesos)
                {
                    EDProceso procesoED = new EDProceso();
                    procesoED.Id_Proceso = proceso.Key.Pk_Id_Proceso;
                    procesoED.Descripcion = proceso.Key.Descripcion_Proceso;
                    procesosIdentificados.Add(procesoED);

                }
            }
            return procesosIdentificados;

        }

        public List<EDZonaLugar> ZonLuagarMetodologiaApp(int id_Sede, int idMetodologia, int id_Proceso)
        {
            List<EDZonaLugar> zonaslugares = new List<EDZonaLugar>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                var zonas = (from pro in contex.Tbl_Procesos
                             join p in contex.Tbl_Peligro on pro.Pk_Id_Proceso equals p.FK_Proceso
                             join cscp in contex.Tbl_Consecuencia_Por_Peligro on p.PK_Peligro equals cscp.FK_Peligro
                             join csc in contex.Tbl_Consecuencias on cscp.FK_Consecuencia equals csc.PK_Consecuencia
                             join gp in contex.Tbl_Grupos on csc.FK_Grupo equals gp.PK_Grupo
                             join m in contex.Tbl_Metodologia on gp.FK_Metodologia equals m.PK_Metodologia
                             where (p.FK_Sede == id_Sede && m.PK_Metodologia == idMetodologia && p.FK_Proceso == id_Proceso)
                             group p by new { p.Lugar } into clasesPeligros
                             select clasesPeligros
                                 ).ToList();

                foreach (var zona in zonas)
                {
                    EDZonaLugar zonasED = new EDZonaLugar();
                    zonasED.Descripcion_ZonaLugar = zona.Key.Lugar;
                    zonaslugares.Add(zonasED);

                }
            }
            return zonaslugares;
        }

        public List<EDActividadApp> ActividadMetodologiaApp(int id_Sede, int idMetodologia, int id_Proceso, string zonaLugar)
        {
            List<EDActividadApp> zonaslugares = new List<EDActividadApp>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                var zonas = (from pro in contex.Tbl_Procesos
                             join p in contex.Tbl_Peligro on pro.Pk_Id_Proceso equals p.FK_Proceso
                             join cscp in contex.Tbl_Consecuencia_Por_Peligro on p.PK_Peligro equals cscp.FK_Peligro
                             join csc in contex.Tbl_Consecuencias on cscp.FK_Consecuencia equals csc.PK_Consecuencia
                             join gp in contex.Tbl_Grupos on csc.FK_Grupo equals gp.PK_Grupo
                             join m in contex.Tbl_Metodologia on gp.FK_Metodologia equals m.PK_Metodologia
                             where (p.FK_Sede == id_Sede && m.PK_Metodologia == idMetodologia && p.FK_Proceso == id_Proceso && p.Lugar == zonaLugar)
                             group p by new { p.Actividad } into clasesPeligros
                             select clasesPeligros
                                 ).ToList();

                foreach (var zona in zonas)
                {
                    EDActividadApp zonasED = new EDActividadApp();
                    zonasED.descripcionProceso = zona.Key.Actividad;
                    zonaslugares.Add(zonasED);

                }
            }
            return zonaslugares;
        }
        private InterpretacionNivelDeRiesgo ObtenerInterpretacionDeRiesgo(int valor_Del_Riesgo)
        {
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                List<InterpretacionNivelDeRiesgo> interpretacionRiesgoList = contex.Tbl_Interpretacion_Nivel_Riesgo.
                     Where(idp => idp.Nivel_Superior >= valor_Del_Riesgo && idp.Nivel_Inferior <= valor_Del_Riesgo).ToList();
                if (interpretacionRiesgoList.Count() > 0)
                {
                    return interpretacionRiesgoList.FirstOrDefault();
                }
            }
            return null;
        }

    }
}
