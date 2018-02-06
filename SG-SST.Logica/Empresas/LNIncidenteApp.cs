using RestSharp;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Empresas;
using SG_SST.InterfazManager.Empresas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SG_SST.Logica.Empresas
{
  public class LNIncidenteApp
    {

      string primerNombre, segundoNombre, primerApellido, segundoApellido, personaGenero, personaTelefono, ocupacion, direccion, actividadEconomica, idZona;

      string diaSemanaIncidente,departamentoAfiliado,municipioAfiliado;
      DateTime fechaNacimiento,fechaIngresoEmpresa;

      int idActividadEconomica, idMunAfiliado,idDepAfiliado;
    
      private static IncidenteAPP iiAPP = IMIncidenteAPP.IncidenteAPP();
      public EDIncidenteAPP GuardarIncidenteAPP(EDIncidenteAPP incidente)

      {
          obtenerDatos(incidente);
          incidente.Persona_primer_apellido = primerApellido;
          incidente.Persona_segundo_apellido = segundoApellido;
          incidente.Persona_primer_nombre = primerNombre;
          incidente.Persona_segundo_nombre = segundoNombre;
          incidente.FK_id_tipo_documento_persona = 1;
          incidente.Persona_fecha_nacimiento = fechaNacimiento;
          incidente.Persona_genero = personaGenero;
          incidente.Persona_telefono = personaTelefono;
          incidente.Persona_fecha_ingreso_empresa = fechaIngresoEmpresa;

          //incidente.General_mismos_datos_sede_principal = true;

          if (incidente.Incidentejornadanormal.Equals("Normal"))
          {
              incidente.Incidente_jornada_normal = true;
          }
          else
          {
              incidente.Incidente_jornada_normal = false;
          }
          
          if(incidente.Incidenterealizabalaborhabitual.Equals("Sí"))
          {
              incidente.Incidente_realizaba_labor_habitual = true;
          }
          else
          {
              incidente.Incidente_realizaba_labor_habitual = false;
          }
          if (incidente.incidenteOcurreEnLaEmpresa.Equals("Dentro De La Empresa"))
          {
              incidente.Incidente_ocurre_dentro_empresa = true;
          }
          else
          {
              incidente.Incidente_ocurre_dentro_empresa = false;
          }


          incidente.Persona_ocupacion_habitual = ocupacion;
          incidente.Persona_direccion = direccion;
          incidente.General_actividad_economica_id = idActividadEconomica;
          incidente.FK_id_tipo_documento_general = 2;
        //  incidente.FK_id_sede_no_principal = 0;
          incidente.departamentoTrabajador = departamentoAfiliado;
          incidente.municipioTrabajador = municipioAfiliado;
          
          incidente.departamentoTrabajador = departamentoAfiliado;
          incidente.municipioTrabajador = municipioAfiliado;


          if (idZona.Equals("U"))
          {
              incidente.General_sede_principal_zona_id = 1;
          }
          else
          {
              incidente.General_sede_principal_zona_id = 2;
          }

        //  DateTime fecha= DateTime.Now;
          //incidente.Incidente_fecha = DateTime.Now;
          incidente.Incidente_fecha_diligenciamiento = DateTime.Now;
          diaSemanaIncidente = incidente.Incidente_fecha.DayOfWeek.ToString();


          if(diaSemanaIncidente.Equals("Monday"))
          {
              incidente.Dia_Semana_Incidente = "LU";
          }
          else if (diaSemanaIncidente.Equals("Tuesday"))
          {
              incidente.Dia_Semana_Incidente = "MA";
          }
          else if (diaSemanaIncidente.Equals("Wednesday"))
          {
              incidente.Dia_Semana_Incidente = "MI";
          }
          else if (diaSemanaIncidente.Equals("Thursday"))
          {
              incidente.Dia_Semana_Incidente = "JU";
          }
          else if (diaSemanaIncidente.Equals("Friday"))
          {
              incidente.Dia_Semana_Incidente = "VI";
          }
          else if (diaSemanaIncidente.Equals("Saturday"))
          {
              incidente.Dia_Semana_Incidente = "SÁ";
          }
          else if (diaSemanaIncidente.Equals("Sunday"))
          {
              incidente.Dia_Semana_Incidente = "DO";
          }
          return iiAPP.GuardarIncidenteAPP(incidente);
      }





      public void obtenerDatos(EDIncidenteAPP incidente)
      {



          try
          {
              var cliente = new RestSharp.RestClient(incidente.afiliadoempresaactivo);
              var request = new RestRequest("wssst/afiliadoEmpresaActivo?", RestSharp.Method.GET);
              

              request.Parameters.Clear();
              request.AddParameter("tpEm", "NI");
              request.AddParameter("docEm", incidente.General_numero_identificacion);
              request.AddParameter("tpAfiliado", "cc");
              request.AddParameter("docAfiliado", incidente.Persona_numero_identificacion);
              request.AddHeader("Content-Type", "application/json");
              request.AddHeader("Accept", "application/json");



              //se omite la validación de certificado de SSL
              ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
              IRestResponse<List<EDPerfilSocioDemograficoWS>> response = cliente.Execute<List<EDPerfilSocioDemograficoWS>>(request);
              var result = response.Content;
              var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EDPerfilSocioDemograficoWS>>(result);

              var nitEmpresaU = "";
              nitEmpresaU = respuesta[0].documentoEmp;
              if (nitEmpresaU.Equals(incidente.General_numero_identificacion))
              {

                  primerNombre = respuesta[0].nombre1;
                  segundoNombre = respuesta[0].nombre2;
                  primerApellido = respuesta[0].apellido1;
                  segundoApellido = respuesta[0].apellido2;
                  personaGenero = respuesta[0].sexoPersona;
                  personaTelefono = respuesta[0].telPersona;
                  ocupacion = respuesta[0].ocupacion;
                  direccion = respuesta[0].dirPersona;
                  actividadEconomica = respuesta[0].actividadEconomica;
                  fechaNacimiento = respuesta[0].fechaNacimiento;
                  fechaIngresoEmpresa = respuesta[0].fechaInicioVinculacion;
                  idActividadEconomica = respuesta[0].idActividadEconomica;
                  idMunAfiliado = respuesta[0].idMunAfiliado;
                  idDepAfiliado = respuesta[0].idDepAfiliado;
                  idZona = respuesta[0].idZona;
                  departamentoAfiliado = respuesta[0].nomDepAfiliado;
                  municipioAfiliado = respuesta[0].nomMunAfiliado;
                  
              }
             
          }
          catch (Exception e)
          {
             
          }
      }

    }
}
