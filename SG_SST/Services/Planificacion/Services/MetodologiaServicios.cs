// <copyright file="MetodologiaServicios.cs" company="Ada SA">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Cristian Mauricio Arenas Gomez.</author>
// <date>21/01/2017</date>
// <summary>Clase que contiene toda la implementacion de la Interfaz IMetodologiaServicios y servicios para las
// la gestion de las metodologias</summary>

namespace SG_SST.Services.Planificacion.Services
{
    using SG_SST.Dtos.Planificacion;
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Empresas.IRepositories;
    using SG_SST.Repositories.Empresas.Repositories;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using SG_SST.Repositories.Planificacion.Repositories;
    using SG_SST.Services.Planificacion.IServices;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    public class MetodologiaServicios : IMetodologiaServicios
    {
        IPeligroRepositorio peligroRepositorio;
        IInterpretacionDeProbabilidadRepositorio InterProbRepositorio;
        IInterpretacionDeRiesgoRepositorio InterRiesgoRespositorio;
        IProcesoRepositorio procesoRepositorio;
        private int pkTipoPeligroOtro;

        public MetodologiaServicios()
        {
            peligroRepositorio = new PeligroRepositorio();
            InterProbRepositorio = new InterpretacionDeProbabilidadRepositorio();
            InterRiesgoRespositorio = new InterpretacionDeRiesgoRepositorio();
            procesoRepositorio = new ProcesoRepositorio();
            pkTipoPeligroOtro = Int32.Parse(ConfigurationManager.AppSettings["pktipoPeligro"]);
        }

      
      


        public List<MatrizGTC45> ObtenerMatrizGTC45(int IdSede, int IdMetodologia)
        {
            List<Peligro> peligros = peligroRepositorio.ObtenerPeligros(IdSede, IdMetodologia);
            List<MatrizGTC45> matrizGtc = peligros.Select(peligro =>
                 new MatrizGTC45(
                     peligro.PK_Peligro,
                     procesoRepositorio.ObtenerProceso(peligro.FK_Proceso).Descripcion_Proceso,
                     peligro.Lugar,
                     peligro.Actividad,
                     peligro.Tarea,
                     ((peligro.FLG_Rutinaria) ? "si" : "No"),
                     peligro.Fuente_Generadora_De_Peligro,
                     peligro.ClasificacionDePeligro.TipoDePeligro.Descripcion_Del_Peligro,
                     (peligro.ClasificacionDePeligro.TipoDePeligro.PK_Tipo_De_Peligro == pkTipoPeligroOtro) ? peligro.Otro : peligro.ClasificacionDePeligro.Descripcion_Clase_De_Peligro,// Preguntas si el tipo de peligro es otro y mostramos la descripcion ingresada el campo otro que se encuntra en el modelo de peligro
                     peligro.GTC45.FirstOrDefault().Efectos_Posibles,
                     peligro.Fuente,
                     peligro.Medio,
                     peligro.Accion_De_Prevencion,
                     peligro.GTC45.FirstOrDefault().NivelDeDeficiencia.Descripcion_Deficiciencia,
                     peligro.GTC45.FirstOrDefault().NivelDeExposicion.Descripcion_Exposicion,
                     peligro.GTC45.FirstOrDefault().Nivel_De_Probablidad,
                     InterProbRepositorio.ConsultarInterpretacion(peligro.GTC45.FirstOrDefault().Nivel_De_Probablidad),
                     peligro.ConsecuenciasPorPeligros.FirstOrDefault().Consecuencia.Descripcion_Consecuencia,
                     peligro.GTC45.FirstOrDefault().Nivel_De_Riesgo,
                     InterRiesgoRespositorio.ObtenerInterpretacionDeRiesgo(peligro.GTC45.FirstOrDefault().Nivel_De_Riesgo).Interpretacion,
                     InterRiesgoRespositorio.ObtenerInterpretacionDeRiesgo(peligro.GTC45.FirstOrDefault().Nivel_De_Riesgo).Resultado,
                     peligro.GTC45.FirstOrDefault().Numero_De_Expuestos,
                     peligro.GTC45.FirstOrDefault().Peor_Consecuencia,
                     ((peligro.GTC45.FirstOrDefault().FLG_Requisito_Legal) ? "Si" : "No"),
                     peligro.Eliminacion,
                     peligro.Sustitucion,
                     peligro.Controles_De_Ingenieria,
                     peligro.Controles_Administrativos,
                     peligro.Elementos_De_Proteccion
                     )).ToList();
            return matrizGtc;
        }
        public MatrizEditGTC45 ObtenerMatrizEditGTC45(int PK_Peligro)
        {
            Peligro peligro = peligroRepositorio.ObtenerPeligro(PK_Peligro);
            MatrizEditGTC45 matrizEditGTC45 =
                 new MatrizEditGTC45(
                     peligro.Nombre_Del_Profesional,
                     peligro.Numero_De_Documento,
                     peligro.Numero_De_Licencia_SST,
                     peligro.Fecha_De_Evaluacion,
                     peligro.ClasificacionDePeligro.TipoDePeligro.PK_Tipo_De_Peligro,
                     peligro.ClasificacionDePeligro.PK_Clasificacion_De_Peligro,
                     peligro.GTC45.FirstOrDefault().NivelDeDeficiencia.FLAG_Cuantitativa,
                     peligro.GTC45.FirstOrDefault().NivelDeDeficiencia.PK_Nivel_De_Deficiencia,
                     peligro.GTC45.FirstOrDefault().NivelDeExposicion.PK_Nivel_De_Exposicion,
                     peligro.ConsecuenciasPorPeligros.FirstOrDefault().Consecuencia.PK_Consecuencia,
                     peligro.GTC45.FirstOrDefault().PK_GTC45,
                     peligro.ConsecuenciasPorPeligros.FirstOrDefault().PK_Consecuencia_Por_Peligro,
                     peligro.PK_Peligro,
                     procesoRepositorio.ObtenerProceso(peligro.FK_Proceso).Descripcion_Proceso,
                     peligro.Lugar,
                     peligro.Actividad,
                     peligro.Tarea,
                     ((peligro.FLG_Rutinaria) ? "si" : "No"),
                     peligro.Fuente_Generadora_De_Peligro,
                     peligro.ClasificacionDePeligro.TipoDePeligro.Descripcion_Del_Peligro,
                     peligro.ClasificacionDePeligro.Descripcion_Clase_De_Peligro,
                     peligro.GTC45.FirstOrDefault().Efectos_Posibles,
                     peligro.Fuente,
                     peligro.Medio,
                     peligro.Accion_De_Prevencion,
                     peligro.GTC45.FirstOrDefault().NivelDeDeficiencia.Descripcion_Deficiciencia,
                     peligro.GTC45.FirstOrDefault().NivelDeExposicion.Descripcion_Exposicion,
                     peligro.GTC45.FirstOrDefault().Nivel_De_Probablidad,
                     InterProbRepositorio.ConsultarInterpretacion(peligro.GTC45.FirstOrDefault().Nivel_De_Probablidad),
                     peligro.ConsecuenciasPorPeligros.FirstOrDefault().Consecuencia.Descripcion_Consecuencia,
                     peligro.GTC45.FirstOrDefault().Nivel_De_Riesgo,
                     InterRiesgoRespositorio.ObtenerInterpretacionDeRiesgo(peligro.GTC45.FirstOrDefault().Nivel_De_Riesgo).Interpretacion,
                     InterRiesgoRespositorio.ObtenerInterpretacionDeRiesgo(peligro.GTC45.FirstOrDefault().Nivel_De_Riesgo).Resultado,
                     peligro.GTC45.FirstOrDefault().Numero_De_Expuestos,
                     peligro.GTC45.FirstOrDefault().Peor_Consecuencia,
                     ((peligro.GTC45.FirstOrDefault().FLG_Requisito_Legal) ? "Si" : "No"),
                     peligro.Eliminacion,
                     peligro.Sustitucion,
                     peligro.Controles_De_Ingenieria,
                     peligro.Controles_Administrativos,
                     peligro.Elementos_De_Proteccion,
                     peligro.FK_Proceso,
                     peligro.Otro,
                     peligro.GTC45.FirstOrDefault().FLG_Higienico
                     );
            return matrizEditGTC45;
        }


        public List<PeligrosPorSede> ObtenerMatrizPorSedeEmpresa(int Pk_Id_Empresa)
        {
            return peligroRepositorio.ObtenerMatrizPorSedeEmpresa(Pk_Id_Empresa);
        }

        public List<MatrizINSTH> ObtenerMatrizINSTH(int IdSede, int IdMetodologia)
        {
            List<Peligro> peligros = peligroRepositorio.ObtenerPeligros(IdSede, IdMetodologia);
            List<MatrizINSTH> matrizINSTH = peligros.Select(peligro =>
               new MatrizINSTH(
                   peligro.PK_Peligro,
                   procesoRepositorio.ObtenerProceso(peligro.FK_Proceso).Descripcion_Proceso,
                   peligro.Lugar,
                   peligro.Actividad,
                   peligro.Tarea,
                   ((peligro.FLG_Rutinaria) ? "si" : "No"),
                   peligro.Fuente_Generadora_De_Peligro,
                   peligro.ClasificacionDePeligro.TipoDePeligro.Descripcion_Del_Peligro,
                   (peligro.ClasificacionDePeligro.TipoDePeligro.PK_Tipo_De_Peligro == pkTipoPeligroOtro) ? peligro.Otro : peligro.ClasificacionDePeligro.Descripcion_Clase_De_Peligro,// Preguntas si el tipo de peligro es otro y mostramos la descripcion ingresada el campo otro que se encuntra en el modelo de peligro
                   peligro.PersonaExpuestas.FirstOrDefault().Planta_Directo,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Planta,
                   peligro.PersonaExpuestas.FirstOrDefault().Contratista,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Contratista,
                   peligro.PersonaExpuestas.FirstOrDefault().Temporal,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Temporal,
                   peligro.PersonaExpuestas.FirstOrDefault().Estudiante_Pasante,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Estudiante,
                   peligro.PersonaExpuestas.FirstOrDefault().Visitante,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Visitante,
                   peligro.Fuente,
                   peligro.Medio,
                   peligro.Accion_De_Prevencion,
                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.FirstOrDefault().Probabilidad.Descripcion_Probabilidad,
                   peligro.ConsecuenciasPorPeligros.FirstOrDefault().Consecuencia.Descripcion_Consecuencia,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Estimacion_Riesgo,
                   peligro.Eliminacion,
                   peligro.Sustitucion,
                   peligro.Controles_De_Ingenieria,
                   peligro.Controles_Administrativos,
                   peligro.Elementos_De_Proteccion,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Medidas_De_Control,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Procedimientos_De_Trabajo,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Informacion,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Formacion,
                   ((peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Riesgo_Controlado) ? "Si" : "No"),
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Accion_Requerida,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Responsable,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Fecha_Finalizacion,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Fecha_De_Comprobacion
                   )).ToList();

            return matrizINSTH;

        }


        public MatrizEditINSTH ObtenerMatrizEditINSTH(int PK_Peligro)
        {
            Peligro peligro = peligroRepositorio.ObtenerPeligro(PK_Peligro);
            MatrizEditINSTH matrizEditINSTH =
               new MatrizEditINSTH(peligro.Nombre_Del_Profesional,
                   peligro.Numero_De_Documento,
                   peligro.Numero_De_Licencia_SST,
                   peligro.Fecha_De_Evaluacion,
                   peligro.ClasificacionDePeligro.TipoDePeligro.PK_Tipo_De_Peligro,
                   peligro.ClasificacionDePeligro.PK_Clasificacion_De_Peligro,
                   peligro.ConsecuenciasPorPeligros.FirstOrDefault().Consecuencia.PK_Consecuencia,
                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.FirstOrDefault().Probabilidad.PK_Probabilidad,
                   peligro.ConsecuenciasPorPeligros.FirstOrDefault().PK_Consecuencia_Por_Peligro,
                   peligro.PersonaExpuestas.FirstOrDefault().PK_Persona_Expuesta,
                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.FirstOrDefault().PK_Probabilidad_Por_PersonaExpuesta,
                    peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().PK_INSHT,
                   peligro.PK_Peligro,
                   procesoRepositorio.ObtenerProceso(peligro.FK_Proceso).Descripcion_Proceso,
                   peligro.Lugar,
                   peligro.Actividad,
                   peligro.Tarea,
                   ((peligro.FLG_Rutinaria) ? "si" : "No"),
                   peligro.Fuente_Generadora_De_Peligro,
                   peligro.ClasificacionDePeligro.TipoDePeligro.Descripcion_Del_Peligro,
                   peligro.ClasificacionDePeligro.Descripcion_Clase_De_Peligro,
                   peligro.PersonaExpuestas.FirstOrDefault().Planta_Directo,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Planta,
                   peligro.PersonaExpuestas.FirstOrDefault().Contratista,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Contratista,
                   peligro.PersonaExpuestas.FirstOrDefault().Temporal,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Temporal,
                   peligro.PersonaExpuestas.FirstOrDefault().Estudiante_Pasante,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Estudiante,
                   peligro.PersonaExpuestas.FirstOrDefault().Visitante,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Visitante,
                   peligro.Fuente,
                   peligro.Medio,
                   peligro.Accion_De_Prevencion,
                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.FirstOrDefault().Probabilidad.Descripcion_Probabilidad,
                   peligro.ConsecuenciasPorPeligros.FirstOrDefault().Consecuencia.Descripcion_Consecuencia,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Estimacion_Riesgo,
                   peligro.Eliminacion,
                   peligro.Sustitucion,
                   peligro.Controles_De_Ingenieria,
                   peligro.Controles_Administrativos,
                   peligro.Elementos_De_Proteccion,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Medidas_De_Control,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Procedimientos_De_Trabajo,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Informacion,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Formacion,
                   ((peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Riesgo_Controlado) ? "Si" : "No"),
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Accion_Requerida,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Responsable,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Fecha_Finalizacion,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().Fecha_De_Comprobacion,
                   peligro.FK_Proceso,
                   peligro.PersonaExpuestas.FirstOrDefault().INSHT.FirstOrDefault().FirmaResponsable,
                   peligro.Otro
                   );

            return matrizEditINSTH;

        }

        public List<MatrizRAM> ObtenerMatrizRAM(int IdSede, int IdMetodologia)
        {
            List<Peligro> peligros = peligroRepositorio.ObtenerPeligros(IdSede, IdMetodologia);
            List<MatrizRAM> matrizRAM = peligros.Select(peligro =>
               new MatrizRAM(
                   peligro.PK_Peligro,
                   procesoRepositorio.ObtenerProceso(peligro.FK_Proceso).Descripcion_Proceso,
                   peligro.Lugar,
                   peligro.Actividad,
                   peligro.Tarea,
                   ((peligro.FLG_Rutinaria) ? "si" : "No"),
                   peligro.Fuente_Generadora_De_Peligro,
                   peligro.ClasificacionDePeligro.TipoDePeligro.Descripcion_Del_Peligro,
                   (peligro.ClasificacionDePeligro.TipoDePeligro.PK_Tipo_De_Peligro == pkTipoPeligroOtro) ? peligro.Otro : peligro.ClasificacionDePeligro.Descripcion_Clase_De_Peligro,// Preguntas si el tipo de peligro es otro y mostramos la descripcion ingresada el campo otro que se encuntra en el modelo de peligro
                   peligro.PersonaExpuestas.FirstOrDefault().Planta_Directo,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Planta,
                   peligro.PersonaExpuestas.FirstOrDefault().Contratista,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Contratista,
                   peligro.PersonaExpuestas.FirstOrDefault().Temporal,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Temporal,
                   peligro.PersonaExpuestas.FirstOrDefault().Estudiante_Pasante,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Estudiante,
                   peligro.PersonaExpuestas.FirstOrDefault().Visitante,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Visitante,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().Consecuencias_Reales,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().Consecuencias_Potenciales,
                   peligro.Fuente,
                   peligro.Medio,
                   peligro.Accion_De_Prevencion,

                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(0).Probabilidad.Descripcion_Probabilidad,
                   peligro.ConsecuenciasPorPeligros.ElementAt(0).Consecuencia.Descripcion_Consecuencia,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(0).EstimacionDeRiesgo.Detalle_Estimacion,

                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(1).Probabilidad.Descripcion_Probabilidad,
                   peligro.ConsecuenciasPorPeligros.ElementAt(1).Consecuencia.Descripcion_Consecuencia,
                    peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(1).EstimacionDeRiesgo.Detalle_Estimacion,

                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(2).Probabilidad.Descripcion_Probabilidad,
                   peligro.ConsecuenciasPorPeligros.ElementAt(2).Consecuencia.Descripcion_Consecuencia,
                    peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(2).EstimacionDeRiesgo.Detalle_Estimacion,

                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(3).Probabilidad.Descripcion_Probabilidad,
                   peligro.ConsecuenciasPorPeligros.ElementAt(3).Consecuencia.Descripcion_Consecuencia,
                    peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(3).EstimacionDeRiesgo.Detalle_Estimacion,

                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(4).Probabilidad.Descripcion_Probabilidad,
                   peligro.ConsecuenciasPorPeligros.ElementAt(4).Consecuencia.Descripcion_Consecuencia,
                    peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(4).EstimacionDeRiesgo.Detalle_Estimacion,

                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().Nivel_De_Riesgo,

                   peligro.Eliminacion,
                   peligro.Sustitucion,
                   peligro.Controles_De_Ingenieria,
                   peligro.Controles_Administrativos,
                   peligro.Elementos_De_Proteccion
                   )).ToList();

            return matrizRAM;

        }

        public MatrizEditRAM ObtenerMatrizEditRAM(int PK_Peligro)
        {
            Peligro peligro = peligroRepositorio.ObtenerPeligro(PK_Peligro);
            MatrizEditRAM MatrizEditRAM =
               new MatrizEditRAM(
                   peligro.Nombre_Del_Profesional,
                   peligro.Numero_De_Documento,
                   peligro.Numero_De_Licencia_SST,
                   peligro.Fecha_De_Evaluacion,
                   peligro.ClasificacionDePeligro.TipoDePeligro.PK_Tipo_De_Peligro,
                   peligro.ClasificacionDePeligro.PK_Clasificacion_De_Peligro,
                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(0).FK_Probabilidad,
                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(1).FK_Probabilidad,
                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(2).FK_Probabilidad,
                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(3).FK_Probabilidad,
                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(4).FK_Probabilidad,
                   peligro.ConsecuenciasPorPeligros.ElementAt(0).FK_Consecuencia,
                   peligro.ConsecuenciasPorPeligros.ElementAt(1).FK_Consecuencia,
                   peligro.ConsecuenciasPorPeligros.ElementAt(2).FK_Consecuencia,
                   peligro.ConsecuenciasPorPeligros.ElementAt(3).FK_Consecuencia,
                   peligro.ConsecuenciasPorPeligros.ElementAt(4).FK_Consecuencia,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(0).FK_Estimacion_De_Riesgo,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(1).FK_Estimacion_De_Riesgo,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(2).FK_Estimacion_De_Riesgo,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(3).FK_Estimacion_De_Riesgo,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(4).FK_Estimacion_De_Riesgo,
                   peligro.PersonaExpuestas.FirstOrDefault().PK_Persona_Expuesta,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().PK_RAM,
                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(0).PK_Probabilidad_Por_PersonaExpuesta,
                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(1).PK_Probabilidad_Por_PersonaExpuesta,
                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(2).PK_Probabilidad_Por_PersonaExpuesta,
                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(3).PK_Probabilidad_Por_PersonaExpuesta,
                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(4).PK_Probabilidad_Por_PersonaExpuesta,
                   peligro.ConsecuenciasPorPeligros.ElementAt(0).PK_Consecuencia_Por_Peligro,
                   peligro.ConsecuenciasPorPeligros.ElementAt(1).PK_Consecuencia_Por_Peligro,
                   peligro.ConsecuenciasPorPeligros.ElementAt(2).PK_Consecuencia_Por_Peligro,
                   peligro.ConsecuenciasPorPeligros.ElementAt(3).PK_Consecuencia_Por_Peligro,
                   peligro.ConsecuenciasPorPeligros.ElementAt(4).PK_Consecuencia_Por_Peligro,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(0).PK_Estimacion_Riesgo_Por_RAM,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(1).PK_Estimacion_Riesgo_Por_RAM,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(2).PK_Estimacion_Riesgo_Por_RAM,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(3).PK_Estimacion_Riesgo_Por_RAM,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(4).PK_Estimacion_Riesgo_Por_RAM,
                   peligro.PK_Peligro,
                   procesoRepositorio.ObtenerProceso(peligro.FK_Proceso).Descripcion_Proceso,
                   peligro.Lugar,
                   peligro.Actividad,
                   peligro.Tarea,
                   ((peligro.FLG_Rutinaria) ? "si" : "No"),
                   peligro.Fuente_Generadora_De_Peligro,
                   peligro.ClasificacionDePeligro.TipoDePeligro.Descripcion_Del_Peligro,
                   peligro.ClasificacionDePeligro.Descripcion_Clase_De_Peligro,
                   peligro.PersonaExpuestas.FirstOrDefault().Planta_Directo,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Planta,
                   peligro.PersonaExpuestas.FirstOrDefault().Contratista,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Contratista,
                   peligro.PersonaExpuestas.FirstOrDefault().Temporal,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Temporal,
                   peligro.PersonaExpuestas.FirstOrDefault().Estudiante_Pasante,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Estudiante,
                   peligro.PersonaExpuestas.FirstOrDefault().Visitante,
                   peligro.PersonaExpuestas.FirstOrDefault().Horas_De_Exposicion_Visitante,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().Consecuencias_Reales,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().Consecuencias_Potenciales,
                   peligro.Fuente,
                   peligro.Medio,
                   peligro.Accion_De_Prevencion,

                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(0).Probabilidad.Descripcion_Probabilidad,
                   peligro.ConsecuenciasPorPeligros.ElementAt(0).Consecuencia.Descripcion_Consecuencia,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(0).EstimacionDeRiesgo.Detalle_Estimacion,

                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(1).Probabilidad.Descripcion_Probabilidad,
                   peligro.ConsecuenciasPorPeligros.ElementAt(1).Consecuencia.Descripcion_Consecuencia,
                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(1).EstimacionDeRiesgo.Detalle_Estimacion,

                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(2).Probabilidad.Descripcion_Probabilidad,
                   peligro.ConsecuenciasPorPeligros.ElementAt(2).Consecuencia.Descripcion_Consecuencia,
                    peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(2).EstimacionDeRiesgo.Detalle_Estimacion,

                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(3).Probabilidad.Descripcion_Probabilidad,
                   peligro.ConsecuenciasPorPeligros.ElementAt(3).Consecuencia.Descripcion_Consecuencia,
                    peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(3).EstimacionDeRiesgo.Detalle_Estimacion,

                   peligro.PersonaExpuestas.FirstOrDefault().ProbabilidadesPorPersonasExpuestas.ElementAt(4).Probabilidad.Descripcion_Probabilidad,
                   peligro.ConsecuenciasPorPeligros.ElementAt(4).Consecuencia.Descripcion_Consecuencia,
                    peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().EstimacionDeRiesgosPorRAM.ElementAt(4).EstimacionDeRiesgo.Detalle_Estimacion,

                   peligro.PersonaExpuestas.FirstOrDefault().RAM.FirstOrDefault().Nivel_De_Riesgo,

                   peligro.Eliminacion,
                   peligro.Sustitucion,
                   peligro.Controles_De_Ingenieria,
                   peligro.Controles_Administrativos,
                   peligro.Elementos_De_Proteccion,

                    peligro.FK_Proceso,

                    peligro.Otro
                   );

            return MatrizEditRAM;

        }

        public bool GuardarPeligro(Peligro peligro)
        {
            return peligroRepositorio.GuardarPeligro(peligro);
        }

        public bool EliminarPeligros(int IdSede, int IdMetodologia)
        {
            return peligroRepositorio.EliminarPeligros(IdSede, IdMetodologia);
        }


        public bool EliminarPeligro(int Pk_Peligro)
        {
            return peligroRepositorio.EliminarPeligro(Pk_Peligro);
        }
    }
}