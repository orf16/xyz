using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SG_SST.Repositories.Planificacion;
using SG_SST.Repositorio.Planificacion;
using System.Collections.Generic;
using SG_SST.EntidadesDominio.Planificacion;

namespace SG_SST.LogicaTest.Planeacion
{
    [TestClass]
    public class PerfilSocioDemograficoRepositorioTest
    {
        [TestMethod]
        //Nombre, lo que se va enviar, lo que va a retornar
        public void GuardarPerfilSociodemografico_GuardarPerfilSocioDemografico_EDPerfilSocioDemografico()
        {

            
            PerfilSocioDemograficoManager perfilManager = new PerfilSocioDemograficoManager();
            EDPerfilSocioDemografico edPerfil= new EDPerfilSocioDemografico();
          
            edPerfil.idtipodoc = "CC";
            edPerfil.PK_Numero_Documento_Empl = "1004688509";
            edPerfil.Nombre1 = "Jorge";
            edPerfil.Nombre2 = "Humberto";
            edPerfil.Apellido1 = "Echeverri";
            edPerfil.Apellido2 = "Escobar";
            //edPerfil.Pk_Id_Sede = 1062;
            //edPerfil.FK_Clasificacion_De_Peligro = 1;
            edPerfil.GradoEscolaridad = "Profesional Universitario";
            edPerfil.Ingresos = "Entre 5 y 10 SMLV";
            edPerfil.Direccion = "Calle 46 # 52-49";
            edPerfil.Conyuge = true;
            edPerfil.Hijos = false;
            edPerfil.FK_Estrato = 3;
            edPerfil.FK_Estado_Civil=1008;
            //edPerfil.FK_Raza = 2;
         
            edPerfil.Sexo = "M";
            edPerfil.GrupoEtarios = "18 a 35 años";
            edPerfil.FK_VinculacionLaboral = 3;
            edPerfil.TurnoTrabajo = "8 am a 2pm";
            edPerfil.Cargo = "Desarrollador";
            edPerfil.fechaIngresoEmpresa = Convert.ToDateTime("13/06/2017");
            edPerfil.FechaIngresoUltimoCargo = Convert.ToDateTime("10/05/2017");
            //edPerfil.AntecedentesExpLaboral = "No tiene antencedentes";
            //edPerfil.FactorRiesgoPeligro = "Factor de peligro";
            //edPerfil.EvaluacionMedica = "Evaluación medica";
            //edPerfil.ZonaLugar = "rionegro";
            edPerfil.Fk_Id_Municipio = 2;

            edPerfil = perfilManager.GuardarPerfilSociodemografico(edPerfil);
            Assert.IsNotNull(edPerfil, "EL perfil sociodemográfico no se guardó correctamente");

        }

        [TestMethod]
        public void GuardarPerfilSociodemografico_EditarPerfilSocioDemografico_EDPerfilSocioDemograficoEditado()
        {

            
            PerfilSocioDemograficoManager perfilManager = new PerfilSocioDemograficoManager();
            EDPerfilSocioDemografico edPerfil= new EDPerfilSocioDemografico();

            edPerfil.IDEmpleado_PerfilSocioDemoGrafico = 6737;
            edPerfil.idtipodoc = "CC";
            edPerfil.PK_Numero_Documento_Empl = "1004688509";
            edPerfil.Nombre1 = "Jorge";
            edPerfil.Nombre2 = "Humberto";
            edPerfil.Apellido1 = "Echeverri";
            edPerfil.Apellido2 = "Escobar";
            //edPerfil.Pk_Id_Sede = 1062;
            //edPerfil.FK_Clasificacion_De_Peligro = 1;
            edPerfil.GradoEscolaridad = "Profesional Universitario";
            edPerfil.Ingresos = "Entre 5 y 10 SMLV";
            edPerfil.Direccion = "Calle 46 # 52-49";
            edPerfil.Conyuge = true;
            edPerfil.Hijos = false;
            edPerfil.FK_Estrato = 3;
            edPerfil.FK_Estado_Civil=1008;
            //edPerfil.FK_Raza = 2;
            edPerfil.Sexo = "M";
            edPerfil.GrupoEtarios = "18 a 35 años";
            edPerfil.FK_VinculacionLaboral = 3;
            edPerfil.TurnoTrabajo = "8 am a 2pm";
            edPerfil.Cargo = "Desarrollador";
            edPerfil.fechaIngresoEmpresa = Convert.ToDateTime("13/06/2017");
            edPerfil.FechaIngresoUltimoCargo = Convert.ToDateTime("10/05/2017");
            //edPerfil.AntecedentesExpLaboral = "No tiene antencedentes";
            //edPerfil.FactorRiesgoPeligro = "Factor de peligro";
            //edPerfil.EvaluacionMedica = "Evaluación medica";
            //edPerfil.ZonaLugar = "rionegro";
            edPerfil.Fk_Id_Municipio = 2;

            edPerfil = perfilManager.GuardarPerfilSociodemografico(edPerfil);
            Assert.IsNotNull(edPerfil, "EL perfil sociodemográfico no se editó correctamente");

        }

        [TestMethod]
        public void InsertarCargoMasivo_GuardarRegistros_bool()
        {


            PerfilSocioDemograficoManager perfilManager = new PerfilSocioDemograficoManager();

            List<EDPerfilSocioDemografico> perfiles = new List<EDPerfilSocioDemografico>();
            EDPerfilSocioDemografico perfil = new EDPerfilSocioDemografico();
            bool validarPerfil;



            perfil.PK_Numero_Documento_Empl = "1004688509";

            //perfil.Pk_Id_Sede = 1062;
            //perfil.FK_Clasificacion_De_Peligro = 1;
            perfil.GradoEscolaridad = "Profesional Universitario";
            perfil.Ingresos = "Entre 5 y 10 SMLV";

            perfil.Conyuge = true;
            perfil.Hijos = false;
            perfil.FK_Estrato = 3;
            perfil.FK_Estado_Civil = 1008;
            //perfil.FK_Raza = 2;
           
            perfil.Sexo = "M";
            perfil.GrupoEtarios = "18 a 35 años";
            perfil.FK_VinculacionLaboral = 3;
            perfil.FK_VinculacionLaboral = 3;
            perfil.TurnoTrabajo = "8 am a 2pm";
            perfil.Cargo = "Desarrollador";
            perfil.fechaIngresoEmpresa = Convert.ToDateTime("13/06/2017");
            perfil.FechaIngresoUltimoCargo = Convert.ToDateTime("10/05/2017");
            //perfil.AntecedentesExpLaboral = "No tiene antencedentes";
            //perfil.FactorRiesgoPeligro = "Factor de peligro";
            //perfil.EvaluacionMedica = "Evaluación medica";
            //perfil.ZonaLugar = "rionegro";
            perfil.Fk_Id_Municipio = 2;
            perfiles.Add(perfil);
            validarPerfil = perfilManager.InsertarCargueMasivoPerfil(perfiles);
            Assert.IsNotNull(validarPerfil, "Los perfiles fueron cargados correctamente");

        }


    }
}
