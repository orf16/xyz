var jqXHRData;
var class_css_header = 'style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase"';
var class_css = ''+class_css_btxt+'';
var class_css_btxt = 'style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"';

$( function() {
    $( "#tabs" ).tabs();
	$("#tabs").tabs("enable", 0 );
	$("#tabs").tabs("disable", 1 );
	$("#tabs").tabs("disable", 2 );
	$("#tabs").tabs("disable", 3 ); 
	$("#tabs").tabs("disable", 4 );
	$("#tabs").tabs("disable", 5 );
	$("#tabs").tabs("disable", 6 );
	$("#tabs").tabs("disable", 7 );	
	$("#tabs").tabs("disable", 8 );
	
	$('#trabajadores_hdesde').timepicker({ 'step': 15 });
    $('#trabajadore_hhasta').timepicker({'step': 15});
	
	$('#contratista_hdesde').timepicker({ 'step': 15 });
    $('#contratista_hhasta').timepicker({'step': 15});
	
	$('#visitante_hdesde').timepicker({ 'step': 15 });
    $('#visitantte_hhasta').timepicker({'step': 15});
	
	$('#cliente_hdesde').timepicker({ 'step': 15 });
    $('#cliente_hhasta').timepicker({'step': 15});
	
	CargaMasiva();
	CargarBDMasiva();
	CargarBDMasiva3();
	CargarEstructuraOrg();
	//Handler for "Start upload" button 
	 $("#hl-start-upload").on('click', function () {
		if (jqXHRData) {
			jqXHRData.submit();
		}
		return false;
	}); 
	
	$("#hl-start-upload1").on('click', function () {
		if (jqXHRData) {
			jqXHRData.submit();
		}
		return false;
	}); 
	
	$("#hl-start-upload2").on('click', function () {
		if (jqXHRData) {
			jqXHRData.submit();
		}
		return false;
	}); 
	
	$("#hl-start-upload3").on('click', function () {
		if (jqXHRData) {
			jqXHRData.submit();
		}
		return false;
	}); 
	
	////////////////////////////////////
	CargrPlanSeguridadFisica();
	$("#hl-start-upload_frenteaccion_adjunto1").on('click', function () {
		if (jqXHRData) {
			jqXHRData.submit();
		}
		return false;
	}); 
	CargarPlanAtencion();
	$("#hl-start-upload_frenteaccion_adjunto2").on('click', function () {
		if (jqXHRData) {
			jqXHRData.submit();
		}
		return false;
	}); 
	CargarPlanContraincendios();
	$("#hl-start-upload_frenteaccion_adjunto3").on('click', function () {
		if (jqXHRData) {
			jqXHRData.submit();
		}
		return false;
	}); 
	CargarPlanEvacuacion();
	$("#hl-start-upload_frenteaccion_adjunto4").on('click', function () {
		if (jqXHRData) {
			jqXHRData.submit();
		}
		return false;
	}); 
	CargarRutasEvacuacion();
	$("#hl-start-upload_frenteaccion_adjunto5").on('click', function () {
		if (jqXHRData) {
			jqXHRData.submit();
		}
		return false;
	}); 
	
	CargarGeoInterno();
	$("#hl-start-upload_adjuntos").on('click', function () {
		if (jqXHRData) {
			jqXHRData.submit();
		}
		return false;
  });
  
	//////////////////////////////////////////////
	CargarEncuentro();
	$("#hl-start-upload_adjuntos1").on('click', function () {
		if (jqXHRData) {
			jqXHRData.submit();
		}
		return false;
	}); 
  
	CargarHidrantes();
	$("#hl-start-upload_adjuntos2").on('click', function () {
		if (jqXHRData) {
			jqXHRData.submit();
		}
		return false;
	}); 
	
	
  });
  
  function CargarEncuentro() {
    'use strict';
    $('#upload_adjuntos1').fileupload({
        url: '/PlanEmergencias/CargarEncuentro',
        dataType: 'json',
        add: function (e, data) {
            jqXHRData = data
        },
        done: function (event, data) {
            if (data.result.isUploaded) {

            }
            else {

            }
            //alert(data.result.message);
			$("#upload_adjuntos1_tmp").val(data.result.message);
			if(data.result.message!="error")
			{
				swal("Estimado Usuario", 'El archivo ha sido cargado satisfactoriamente',"success");
			}
			else
			{
				swal("Estimado Usuario", 'Solo se admiten archivos de Imagen',"warning");
			}
        },
        fail: function (event, data) {
            if (data.files[0].error) {
                //alert(data.files[0].error);
            }
        }
    });
}

function CargarHidrantes() {
    'use strict';
    $('#upload_adjuntos2').fileupload({
        url: '/PlanEmergencias/CargarHidrantes',
        dataType: 'json',
        add: function (e, data) {
            jqXHRData = data
        },
        done: function (event, data) {
            if (data.result.isUploaded) {

            }
            else {

            }
            //alert(data.result.message);
			$("#upload_adjuntos2_tmp").val(data.result.message);
			if(data.result.message!="error")
			{
			    swal("Estimado Usuario", 'El archivo ha sido cargado satisfactoriamente', "success");
			}
			else
			{
				swal("Estimado Usuario", 'Solo se admiten archivos de Imagen',"warning");
			}
        },
        fail: function (event, data) {
            if (data.files[0].error) {
                //alert(data.files[0].error);
            }
        }
    });
}
  
function ActualizarEncuentro(){
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ActualizarEncuentro',
		data: { isede : $("#IdSede").val(), adjunto : $("#upload_adjuntos1_tmp").val()  },
		traditional: true,
		success: function (result) {
			
			
		}
	});
 } 
 
function ActualizarHidrantes(){
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ActualizarHidrantes',
		data: { isede : $("#IdSede").val(), adjunto : $("#upload_adjuntos2_tmp").val()  },
		traditional: true,
		success: function (result) {
			
			
		}
	});
 }  
 
 $("#cargar_img1").click(function() {
	$.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ObtenerImagenIMG1',
		data: { IdSede : $("#IdSede").val() },
		traditional: true,
		success: function (Thumbnails) {
			if(Thumbnails!=""){
				$('#myModalimg').modal('show');
				$('#ImgCarga').attr("src", Thumbnails);
			}else
				swal("Estimado Usuario", "Debe guardar la Georeferenciación primero", "warning");
		
		}
	});
});

$("#cargar_img2").click(function() {
	$.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ObtenerImagenIMG2',
		data: { IdSede : $("#IdSede").val() },
		traditional: true,
		success: function (Thumbnails) {
			if(Thumbnails!=""){
				$('#myModalimg').modal('show');
				$('#ImgCarga').attr("src", Thumbnails);
			}else
				swal("Estimado Usuario", "Debe guardar la Georeferenciación primero", "warning");
		
		}
	});
});
    
function CargarGeoInterno() {
    'use strict';
    $('#upload_adjuntos').fileupload({
        url: '/PlanEmergencias/SubirArchivoIMG',
        dataType: 'json',
        add: function (e, data) {
            jqXHRData = data
        },
        done: function (event, data) {
            if (data.result.isUploaded) {

            }
            else {

            }
            //alert(data.result.message);
			$("#upload_adjuntos_tmp").val(data.result.message);
			if(data.result.message!="error")
			{
			    swal("Estimado Usuario", 'El archivo ha sido cargado satisfactoriamente', "success");
			}
			else
			{
				swal("Estimado Usuario", 'Solo se admiten archivos de Imagen',"warning");
			}
        },
        fail: function (event, data) {
            if (data.files[0].error) {
                //alert(data.files[0].error);
            }
        }
    });
}

$("#cargar_img").click(function() {
	$.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ObtenerImagenIMG',
		data: { IdSede : $("#IdSede").val() },
		traditional: true,
		success: function (Thumbnails) {
			if(Thumbnails!=""){
				$('#myModalimg').modal('show');
				$('#ImgCarga').attr("src", Thumbnails);
			}else
				swal("Estimado Usuario", "Debe guardar la Georeferenciación primero", "warning");
		
		}
	});
});
  
 function CargrPlanSeguridadFisica(){
	 $('#upload_frenteaccion_adjunto1').fileupload({
        url: '/PlanEmergencias/CargrPlanSeguridadFisica',
        dataType: 'json',
        add: function (e, data) {jqXHRData = data },
        done: function (event, data) {
			if(data.result.message!="error"){
				$("#adjunto_tmp_upload_frenteaccion_adjunto1").val(data.result.message);
				swal("Estimado Usuario", 'El archivo ha sido cargado satisfactoriamente',"success");
			}
			else
				swal("Estimado Usuario", 'Solo se admiten archivos de Imagen',"warning");
		
        },
        fail: function (event, data) {}
    });
 }
 
 function CargarPlanAtencion(){
	 $('#upload_frenteaccion_adjunto2').fileupload({
        url: '/PlanEmergencias/CargarPlanAtencion',
        dataType: 'json',
        add: function (e, data) {jqXHRData = data },
        done: function (event, data) {
			if(data.result.message!="error"){
				$("#adjunto_tmp_upload_frenteaccion_adjunto2").val(data.result.message);
				swal("Estimado Usuario", 'El archivo ha sido cargado satisfactoriamente', "success");
			}
			else
				swal("Estimado Usuario", 'Solo se admiten archivos de Imagen',"warning");
		
        },
        fail: function (event, data) {}
    });
 }
 
 function CargarPlanContraincendios(){
	 $('#upload_frenteaccion_adjunto3').fileupload({
        url: '/PlanEmergencias/CargarPlanContraincendios',
        dataType: 'json',
        add: function (e, data) {jqXHRData = data },
        done: function (event, data) {
			if(data.result.message!="error"){
				$("#adjunto_tmp_upload_frenteaccion_adjunto3").val(data.result.message);
				swal("Estimado Usuario", 'El archivo ha sido cargado satisfactoriamente', "success");
			}
			else
				swal("Estimado Usuario", 'Solo se admiten archivos de Imagen',"warning");
		
        },
        fail: function (event, data) {}
    });
	 
 } 
 
 function CargarPlanEvacuacion(){
	 $('#upload_frenteaccion_adjunto4').fileupload({
        url: '/PlanEmergencias/CargarPlanEvacuacion',
        dataType: 'json',
        add: function (e, data) {jqXHRData = data },
        done: function (event, data) {
			if(data.result.message!="error"){
				$("#adjunto_tmp_upload_frenteaccion_adjunto4").val(data.result.message);
				swal("Estimado Usuario", 'El archivo ha sido cargado satisfactoriamente', "success");
			}
			else
				swal("Estimado Usuario", 'Solo se admiten archivos de Imagen',"warning");
		
        },
        fail: function (event, data) {}
    });
 } 
 
 function CargarRutasEvacuacion(){
	 $('#upload_frenteaccion_adjunto5').fileupload({
        url: '/PlanEmergencias/CargarRutasEvacuacion',
        dataType: 'json',
        add: function (e, data) {jqXHRData = data },
        done: function (event, data) {
			if(data.result.message!="error"){
				$("#adjunto_tmp_upload_frenteaccion_adjunto5").val(data.result.message);
				swal("Estimado Usuario", 'El archivo ha sido cargado satisfactoriamente',"success");
			}
			else
				swal("Estimado Usuario", 'Solo se admiten archivos Imagen',"warning");
		
        },
        fail: function (event, data) {}
    });
	 
 }
 
 function CargarFrentesAccionAdjuntos(){
	 var adjunto1 = $("#adjunto_tmp_upload_frenteaccion_adjunto1").val();
	 var adjunto2 = $("#adjunto_tmp_upload_frenteaccion_adjunto2").val();
	 var adjunto3 = $("#adjunto_tmp_upload_frenteaccion_adjunto3").val();
	 var adjunto4 = $("#adjunto_tmp_upload_frenteaccion_adjunto4").val();
	 var adjunto5 = $("#adjunto_tmp_upload_frenteaccion_adjunto5").val();
	 
 $.ajax({
	type: 'GET',
	url: '/PlanEmergencias/CargarFrentesAccionAdjuntos',
	data: { isede : $("#IdSede").val(), adjunto1 : adjunto1, adjunto2 : adjunto2, adjunto3 : adjunto3, adjunto4 : adjunto4, adjunto5 : adjunto5 },
	traditional: true,
	success: function (result) {
		//$('#pk_id_soporte').val(result);
	    swal("Estimado Usuario", 'Los archivos han sido guardados satisfactoriamente', "success");
	}
});
	 
 }
 
 function DescargarArchivoFrente(tipo){
	$.ajax({
		type: 'POST',
		url: '/PlanEmergencias/DescargarArchivoFrente',
		data:{ isede : $("#IdSede").val(), tipo : tipo },
		success: function (result) {
			  window.location = '/PlanEmergencias/Download?file=' + result;  
		}
	});
	
}
 ///////////////////////////////////////////////////
 
function CargaMasiva() {
    $('#upload1').fileupload({
        url: '/PlanEmergencias/SubirArchivoMasivo',
        dataType: 'json',
        add: function (e, data) {jqXHRData = data },
        done: function (event, data) {
			if(data.result.message!="error"){
			    swal("Estimado Usuario", 'El archivo ha sido cargado satisfactoriamente', "success");
				ListarBDInterna($("#IdSede").val());
			}
			else
				swal("Estimado Usuario", 'Solo se admiten archivos de Excel',"warning");
		
        },
        fail: function (event, data) {}
    });
}

function CargarBDMasiva() {
    $('#upload2').fileupload({
        url: '/PlanEmergencias/SubirArchivoMasivo2',
        dataType: 'json',
        add: function (e, data) {jqXHRData = data },
        done: function (event, data) {
			if(data.result.message!="error"){
			    swal("Estimado Usuario", 'El archivo ha sido cargado satisfactoriamente', "success");
				ListarBDExterna($("#IdSede").val());
			}
			else
				swal("Estimado Usuario", 'Solo se admiten archivos de Excel',"warning");
		
        },
        fail: function (event, data) {}
    });
}

function CargarBDMasiva3() {
    $('#upload3').fileupload({
        url: '/PlanEmergencias/SubirArchivoMasivo3',
        dataType: 'json',
        add: function (e, data) {jqXHRData = data },
        done: function (event, data) {
			if(data.result.message!="error"){
			    swal("Estimado Usuario", 'El archivo ha sido cargado satisfactoriamente', "success");
				ListarBDPlanAyuda($("#IdSede").val());
			}
			else
				swal("Estimado Usuario", 'Solo se admiten archivos de Excel',"warning");
		
        },
        fail: function (event, data) {}
    });
}

function CargarEstructuraOrg() {
	'use strict';
    $('#upload').fileupload({
        url: '/PlanEmergencias/CargarEstructuraOrg',
        dataType: 'json',
        add: function (e, data) {
            jqXHRData = data
        },
        done: function (event, data) {
            if (data.result.isUploaded) {

            }
            else {

            }
            //alert(data.result.message);
			if(data.result.message!="error")
			{
				$("#adjunto_tmp").val(data.result.message);
				$.ajax({
					type: 'GET',
					url: '/PlanEmergencias/ActualizarAdjuntos',
					data: { isede : $("#IdSede").val(), adjunto : $("#adjunto_tmp").val()  },
					traditional: true,
					success: function (result) {
						//$('#pk_id_soporte').val(result);
					    swal("Estimado Usuario", 'El archivo ha sido cargado satisfactoriamente', "success");
					}
				});
			}
			else
			{
				swal("Estimado Usuario", 'Solo se admiten archivos de Imagen',"warning");
			}
        },
        fail: function (event, data) {
            if (data.files[0].error) {
                //alert(data.files[0].error);
            }
        }
    });
}

$("#cargar").click(function() {
	$.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ObtenerImagen',
		data: { IdSede : $("#IdSede").val() },
		traditional: true,
		success: function (Thumbnails) {
			if(Thumbnails!=""){
				$('#myModalimg').modal('show');
				$('#ImgCarga').attr("src", Thumbnails);
			}else
				swal("Estimado Usuario", "Debe adjuntar una firma primero", "warning");
		
		}
	});
});

 function GenerarPlanEmergenciaSede(){
	 var sede = $("#IdSede").val();
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ObtenerInfoSede',
		data: { isede : sede },
		success: function (result) {
			$("#objetivos").val(result.objetivos);
			$("#alcance").val(result.alcance);
			$("#razon_social").val(result.razon_social);
			$("#identificacion_sede").val(result.identificacion_sede);
			$("#direccion_sede").val(result.direccion_sede);
			$("#telefono_sede").val(result.telefono_sede);
			$("#correo_electronico").val(result.correo_electronico);
			$("#departamento_sede").val(result.departamento_sede);
			$("#municipio_sede").val(result.municipio_sede);
			$("#actividad_economica").val(result.actividad_economica);
			$("#representante").val(result.representante);
			$("#lindero_norte").val(result.lindero_norte);
			$("#lindero_sur").val(result.lindero_sur);
			$("#lindero_occidente").val(result.lindero_occidente);
			$("#lindero_oriente").val(result.lindero_oriente);
			$("#acceso_principales").val(result.acceso_principales);
			$("#acceso_alternas").val(result.acceso_alternas);
			$("#trabajadores_cantidad").val(result.trabajadores_cantidad);
			$("#trabajadores_hdesde").val(result.trabajadores_hdesde);
			$("#trabajadore_hhasta").val(result.trabajadore_hhasta);
			$("#contratista_cantidad").val(result.contratista_cantidad);
			$("#contratista_hdesde").val(result.contratista_hdesde);
			$("#contratista_hhasta").val(result.contratista_hhasta);
			$("#cliente_cantidad").val(result.cliente_cantidad);
			$("#cliente_hdesde").val(result.cliente_hdesde);
			$("#cliente_hhasta").val(result.cliente_hhasta);
			$("#bo_tratamiento_especial").prop( "checked", result.bo_tratamiento_especial);
			$("#cual").val(result.cual);
			$("#ventilacion_mecanica").val(result.ventilacion_mecanica);
			$("#ascensores").val(result.ascensores);
			$("#sotanos").val(result.sotanos);
			$("#red_hidraulica").val(result.red_hidraulica);
			$("#transformadores").val(result.transformadores);
			$("#plantas_electricas").val(result.plantas_electricas);
			$("#escaleras").val(result.escaleras);
			$("#zonas_parqueo").val(result.zonas_parqueo);
			$("#areas_especiales").val(result.areas_especiales);
			$("#estructurales_descripcion").val(result.estructurales_descripcion);
			$("#estructurales_ubicacion").val(result.estructurales_ubicacion);
			$("#equipos_descripcion").val(result.equipos_descripcion);
			$("#equipos_ubicacion").val(result.equipos_ubicacion);
			$("#insumos_descripcion").val(result.insumos_descripcion);
			$("#insumos_ubicacion").val(result.insumos_ubicacion);
			$("#bo_externo").prop( "checked", result.bo_externo);
			$("#bo_colegio").prop( "checked", result.bo_colegio);
			$("#bo_iglesia").prop( "checked", result.bo_iglesia);
			$("#bo_comercial").prop( "checked", result.bo_comercial);
			$("#bo_centro_atencion").prop( "checked", result.bo_centro_atencion);
			$("#bo_parque").prop( "checked", result.bo_parque);
			$("#bo_otro").prop( "checked", result.bo_otro);
			$("#tab3_cual").val(result.cual);
			$("#punto_encuentro").val(result.punto_encuentro_img);
			$("#ubicacion_hidrantes").val(result.ubicacion_hidrantes_img);
			$("#plan_seguridadfisica").val(result.plan_seguridadfisica);
			$("#plan_primerosaux").val(result.plan_primerosaux);
			$("#plan_contraincendios").val(result.plan_contraincendios);
			$("#nombrecoordinador").val(result.nombrecoordinador);
			$("#tab7_objetivos").val(result.objetivos);
			$("#estructura").val(result.estructura);
			$("#proc_coordinacion").val(result.proc_coordinacion);
			$("#proc_internos").val(result.proc_internos);
			$("#proc_externos").val(result.proc_externos);
			$("#mecanismos_alarma").val(result.mecanismos_alarma);
			$("#simulacros").val(result.simulacros);
			$("#instructivo_evacuacion").val(result.instructivo_evacuacion);
			$("#proc_retorno").val(result.proc_retorno);
			$("#fk_id_sede_generalidades").val(sede);
			$("#fk_id_sede_infogeneral").val(sede);
			$("#fk_id_sede_descocupacion").val(sede);
			$("#fk_id_sede_cinstalaciones").val(sede);
			$("#fk_id_sede_elementos").val(sede);
			$("#fk_id_sede_georeferenciacion").val(sede);
			$("#fk_id_sede_roles").val(sede);
			$("#fk_id_sede_frenteaccion").val(sede);
			$("#fk_id_sede_proc_normalizados").val(sede);
			$("#fk_id_sede_nivelemergencia").val(sede);
			$("#fk_id_sede_recursosh").val(sede);
			$("#fk_id_sede_recursostecnicos").val(sede);
			ListarRTecnicos(sede);
			ListarHR(sede);
			ListarProcedimientosOperativosNormalizados(sede);
			ListarRoles(sede);
			ListarNivelesEmergencia(sede);
			ListarBDInterna(sede);
			ListarBDExterna(sede);
			ListarBDPlanAyuda(sede);
			AnalisisRiesgo();
			
			for(i=0;i<=7;i++){
				$("#tabs").tabs("enable", i );
			}
			
			swal("Estimado Usuario", "La interfaz de la Sede ha sido generada satisfactoriamente", "success");
		}
	});  
 }
 
 function Siguiente(numtab){
	if($("#IdSede").val()==""){
		swal("Estimado Usuario", "Debe seleccionar una Sede", "warning");
		return;
	}

	 switch(numtab) {
		case 1:
			$("#tabs").tabs("enable", 0 );
			$("#tabs").tabs("enable", numtab );
			Generalidades(numtab);
			break;
		case 2:
			$("#tabs").tabs("enable", numtab );
			InformacionGeneral(numtab);
			break;
		case 3:
			$("#tabs").tabs("enable", numtab );
			$( "#tabs" ).tabs({ active: numtab });
			break;
		case 4:
			$("#tabs").tabs("enable", numtab );
			$( "#tabs" ).tabs({ active: numtab });
			guardarriesgo(numtab);
			break;
		case 5:
			$("#tabs").tabs("enable", numtab );
			$( "#tabs" ).tabs({ active: numtab });
			break;
		case 6:
			$("#tabs").tabs("enable", numtab );
			$( "#tabs" ).tabs({ active: numtab });
			break;
		case 7:
			$("#tabs").tabs("enable", numtab );
			$( "#tabs" ).tabs({ active: numtab });
			FrentesAccion(numtab);
			break;
		case 8:
			$("#tabs").tabs("enable", numtab );
			ProcedimientosOperativosNormalizados();
			break;
	}
 }
 
 function Generalidades(numtab){
	$.ajax({
		type: 'POST',
		url: '/PlanEmergencias/GuardarGeneralidades',
		data: $("#frmgeneralidades").serialize(),
		traditional: true,
		success: function (result) {
			$( "#tabs" ).tabs({ active: numtab });
		}
	}); 
 }
 
 function InformacionGeneral(numtab){
	$.ajax({
		type: 'POST',
		url: '/PlanEmergencias/GuardarInformacionGeneral',
		data: $("#frmInformacionGeneral").serialize(),
		traditional: true,
		success: function (result) {
			DescripcionOcupacion();
			$( "#tabs" ).tabs({ active: numtab });
		}
	}); 
 }
 
  function DescripcionOcupacion(){
	$.ajax({
		type: 'POST',
		url: '/PlanEmergencias/GuardarDescripcionOcupacion',
		data: $("#frmDescripcionOcupacion").serialize(),
		traditional: true,
		success: function (result) {
			CaracteristicasInstalacion();
		}
	}); 
 }
 
 function CaracteristicasInstalacion(){
	 $.ajax({
		type: 'POST',
		url: '/PlanEmergencias/GuardarCaracteristicasInstalacion',
		data: $("#frmCaracteristicasInstalacion").serialize(),
		traditional: true,
		success: function (result) {
			GuardarElementos();
		}
	}); 
 }
 
  function GuardarElementos(){
	 $.ajax({
		type: 'POST',
		url: '/PlanEmergencias/GuardarElementos',
		data: $("#frmelementos").serialize(),
		traditional: true,
		success: function (result) {
			//CaracteristicasInstalacion();
			$( "#tabs" ).tabs({ active: numtab });
		}
	}); 
 }
  
 function Georeferenciacion(){
	 $.ajax({
		type: 'POST',
		url: '/PlanEmergencias/GuardarGeoreferenciacion',
		data: $("#frmGeoreferenciacion").serialize(),
		traditional: true,
		success: function (result) {
			ActualizarIMG();
			ActualizarEncuentro();
			ActualizarHidrantes();
			swal("Estimado Usuario", "La Georeferenciación se ha guardado satisfactoriamente", "success");
			//$( "#tabs" ).tabs({ active: 3 });
		}
	}); 
 }
 
function ActualizarIMG(){
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ActualizarAdjuntosIMG',
		data: { isede : $("#IdSede").val(), adjunto : $("#upload_adjuntos_tmp").val()  },
		traditional: true,
		success: function (result) {
			
			
		}
	});
 }
 
 function FrentesAccion(numtab){
	 $.ajax({
		type: 'POST',
		url: '/PlanEmergencias/GuardarFrentesAccion',
		data: $("#frmFrentesAccion").serialize(),
		traditional: true,
		success: function (result) {
			$( "#tabs" ).tabs({ active: numtab });
		}
	}); 
 }
 
 function ProcedimientosOperativosNormalizados(){
	 var sede = $("#IdSede").val();
	 $.ajax({
		type: 'POST',
		url: '/PlanEmergencias/GuardarProcedimientosOperativosNormalizados',
		data: $("#frmProcedimientosOperativosNormalizados").serialize(),
		traditional: true,
		success: function (result) {
			$('#myModal0').modal('hide');
			ListarProcedimientosOperativosNormalizados(sede);
		}
	}); 
	 
 }
 
 function GuardarNivelEmergencia(){
	 var sede = $("#IdSede").val();
	 $.ajax({
		type: 'POST',
		url: '/PlanEmergencias/GuardarNivelEmergencia',
		data: $("#frmNivelEmergencia").serialize(),
		traditional: true,
		success: function (result) {
			$('#myModal2').modal('hide');
			ListarNivelesEmergencia(sede);
		}
	});  
 }
 
  function ListarNivelesEmergencia(isede){
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ListarNivelesEmergencia',
		data: { isede : isede},
		success: function (result) {
			var tHtml = "";
            $("#gridemergencia").empty();
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+'>Nivel</td>';
			tHtml += '<th '+class_css_header+'>Tipo de Emergencia</th>';
			///tHtml += '<th style="border-right: 2px solid lightslategray; text-align:center">Acciones</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
                tHtml += "<tr>";
                tHtml += '<td '+class_css_btxt+'>' + val.nivel + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.emergencia + '</td>';
				//tHtml += '<td>&nbsp;<a href="#" class="btn btn-search btn-md" title="Editar"><span class="glyphicon glyphicon-pencil"></span></a>&nbsp;';
				//tHtml += '<a href="#" class="btn btn-search btn-md" title="Eliminar"><span class="glyphicon glyphicon-erase"></span></a></td>';
				tHtml += '</tr>';
            });
            $("#gridemergencia").append(tHtml);
		}
	}); 
 }
 
  function ListarBDInterna(isede){
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ListarBDInterna',
		data: { isede : isede},
		success: function (result) {
			var tHtml = "";
            $("#gridinterna").empty();
			tHtml += '<thead>';
			tHtml += '<tr class="titulos_tabla"><th '+class_css_header+' colspan="5">&nbsp;</th>';
			tHtml += '<th colspan="3" '+class_css_header+'>Información del Contacto</th>';
			tHtml += '<th ' + class_css_header + 'colspan="2">&nbsp;</th></tr>';
            tHtml += '<tr class="titulos_tabla">';
            tHtml += '<th '+class_css_header+'>Nombre</th>';
            tHtml += '<th '+class_css_header+'>Número de Documento</th>';
            tHtml += '<th '+class_css_header+'>Género</th>';
            tHtml += '<th '+class_css_header+'>EPS</th>';
            tHtml += '<th '+class_css_header+'>RH</th>';
            tHtml += '<th '+class_css_header+'>Nombre</th>';
            tHtml += '<th '+class_css_header+'>Teléfono</th>';
			tHtml += '<th '+class_css_header+'>Parentesco</th>';
			tHtml += '<th '+class_css_header+'>Requiere Manejo</th>';
			tHtml += '<th '+class_css_header+'>Cuál</th></tr></thead>';
			$.each(result, function (key, val) {
                tHtml += '<tbody><tr>';
                tHtml += '<td '+class_css_btxt+'>'+val.nombre+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.numdocumento+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.genero+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.eps+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.rh+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.contacto_nombre+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.contacto_telefono+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.contacto_parentesco+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.requiere_manejo+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.cual_manejo+'</td>';
				tHtml += '</tr></tbody>';
            });
            $("#gridinterna").append(tHtml);
		}
	}); 
 }
 
   function ListarBDExterna(isede){
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ListarBDExterna',
		data: { isede : isede},
		success: function (result) {
			var tHtml = "";
            $("#gridbdexterna").empty();
            tHtml += '<tr class="titulos_tabla">';
            tHtml += '<th '+class_css_header+'>Entidad</th>';
            tHtml += '<th '+class_css_header+'>Dirección</th>';
            tHtml += '<th '+class_css_header+'>Teléfono</th>';
            tHtml += '<th '+class_css_header+'>Contacto</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
                tHtml += '<tr>';
                tHtml += '<td '+class_css_btxt+'>'+val.entidad+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.direccion+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.telefono+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.nombre_contacto+'</td>';
				tHtml += '</tr>';
            });
            $("#gridbdexterna").append(tHtml);
		}
	}); 
 }
 
 
function ListarBDPlanAyuda(isede){
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ListarBDPlanAyuda',
		data: { isede : isede},
		success: function (result) {
			var tHtml = "";
            $("#gridplanayuda").empty();
			tHtml += '<thead>';
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+'>&nbsp;</th>';
			tHtml += '<th '+class_css_header+'>&nbsp;</th>';
			tHtml += '<th '+class_css_header+'>&nbsp;</th>';
			tHtml += '<th '+class_css_header+'>&nbsp;</th>';
			tHtml += '<th '+class_css_header+' colspan="2">Información de Contacto</th></tr>';
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+'>Empresa Participante</th>';
			tHtml += '<th '+class_css_header+'>Recursos a Disposición</th>';
			tHtml += '<th '+class_css_header+'>Compensación económica por uso del recurso</th>';
			tHtml += '<th '+class_css_header+'>Reintegro del recurso</th>';
			tHtml += '<th '+class_css_header+'>Nombre</th>';
			tHtml += '<th '+class_css_header+'>Teléfono</th>';
			tHtml += '</tr></thead>';
			$.each(result, function (key, val) {
                tHtml += '<tbody><tr>';
                tHtml += '<td '+class_css_btxt+'>'+val.empresa+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.recurso+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.compensacion+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.reintegro+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.nombre_contacto+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.telefono_contacto+'</td>';
				tHtml += '</tr></tbody>';
            });
            $("#gridplanayuda").append(tHtml);
		}
	}); 
 }
 
 
 function GuardarRoles(){
	 var sede = $("#IdSede").val();
	 $.ajax({
		type: 'POST',
		url: '/PlanEmergencias/GuardarRoles',
		data: $("#frmRolFunciones").serialize(),
		traditional: true,
		success: function (result) {
			$('#myModal1').modal('hide');
			ListarRoles(sede);
		}
	});  
 }
 
 function ListarRoles(isede){
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ListarRoles',
		data: { isede : isede},
		success: function (result) {
			var tHtml = "";
            $("#gridroles").empty();
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+'>Rol</th>';
			tHtml += '<th '+class_css_header+'>Antes</th>';
			tHtml += '<th '+class_css_header+'>Durante</th>';
			tHtml += '<th '+class_css_header+'>Después</th>';
			//tHtml += '<th style="border-right: 2px solid lightslategray; text-align:center">Acciones</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
                tHtml += "<tr>";
                tHtml += '<td '+class_css_btxt+'>' + val.nombre + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.antes + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.durante + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.despues + '</td>';
				//tHtml += '<td>&nbsp;<a href="#" class="btn btn-search btn-md" title="Editar"><span class="glyphicon glyphicon-pencil"></span></a>&nbsp;';
				//tHtml += '<a href="#" class="btn btn-search btn-md" title="Eliminar"><span class="glyphicon glyphicon-erase"></span></a></td>';
				tHtml += '</tr>';
            });
            $("#gridroles").append(tHtml);
		}
	}); 
 }

 function ListarProcedimientosOperativosNormalizados(isede){
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ListarProcedimientosOperativosNormalizados',
		data: { isede : isede},
		success: function (result) {
			var tHtml = "";
            $("#procopera").empty();
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+'>Nombre del Procedimiento</th>';
			tHtml += '<th '+class_css_header+'>Responsable</th>';
			tHtml += '<th '+class_css_header+'>Antes</th>';
			tHtml += '<th '+class_css_header+'>Durante</th>';
			tHtml += '<th '+class_css_header+'>Después</th>';
			tHtml += '<th '+class_css_header+'>Recursos</th>';
			//tHtml += '<th style="border-right: 2px solid lightslategray; text-align:center">Acciones</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
                tHtml += "<tr>";
                tHtml += '<td '+class_css_btxt+'>' + val.nombre_proc + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.responsable + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.proc_antes + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.proc_durante + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.proc_despues + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.proc_recursos + '</td>';
				//tHtml += '<td>&nbsp;<a href="#" class="btn btn-search btn-md" title="Editar"><span class="glyphicon glyphicon-pencil"></span></a>&nbsp;';
				//tHtml += '<a href="#" class="btn btn-search btn-md" title="Eliminar"><span class="glyphicon glyphicon-erase"></span></a></td>';
				tHtml += '</tr>';
            });
            $("#procopera").append(tHtml);
		}
	}); 
 }
 
 function GuardarHR(){
	 var sede = $("#IdSede").val();
	 $.ajax({
		type: 'POST',
		url: '/PlanEmergencias/GuardarHR',
		data: $("#frmHR").serialize(),
		traditional: true,
		success: function (result) {
			$('#myModal3').modal('hide');
			ListarHR(sede);
		}
	});  
 }
 
 function ListarHR(isede){
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ListarHR',
		data: { isede : isede},
		success: function (result) {
			var tHtml = "";
            $("#gridHR").empty();
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+'>Brigadistas Primeros Auxilios</th>';
			tHtml += '<th '+class_css_header+'>Brigadistas Contraincendios</th>';
			tHtml += '<th '+class_css_header+'>Brigadistas de Evacuación y Rescate</th>';
			//tHtml += '<th style="border-right: 2px solid lightslategray; text-align:center">&nbsp</th>';
			tHtml += '</tr>';
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+'>Nombre</th>';
			tHtml += '<th '+class_css_header+'>Nombre</th>';
			tHtml += '<th '+class_css_header+'>Nombre</th>';
			//tHtml += '<th style="border-right: 2px solid lightslategray; text-align:center">Acciones</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
                tHtml += "<tr>";
                tHtml += '<td '+class_css_btxt+'>' + val.bpaux_nombre + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.bcontra_nombre + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.bevalresc_nombre + '</td>';
				//tHtml += '<td>&nbsp;<a href="#" class="btn btn-search btn-md" title="Editar"><span class="glyphicon glyphicon-pencil"></span></a>&nbsp;';
				//tHtml += '<a href="#" class="btn btn-search btn-md" title="Eliminar"><span class="glyphicon glyphicon-erase"></span></a></td>';
				tHtml += '</tr>';
            });
            $("#gridHR").append(tHtml);
		}
	}); 
 }
 
 
 function GuardarRTecnicos(){
	 var sede = $("#IdSede").val();
	 $.ajax({
		type: 'POST',
		url: '/PlanEmergencias/GuardarRTecnicos',
		data: $("#frmRTecnicos").serialize(),
		traditional: true,
		success: function (result) {
			$('#myModal4').modal('hide');
			ListarRTecnicos(sede);
		}
	});  
 }
 
 function ListarRTecnicos(isede){
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ListarRTecnicos',
		data: { isede : isede},
		success: function (result) {
			var tHtml = "";
            $("#gridRT").empty();
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+'>Tipo</td>';
			tHtml += '<th '+class_css_header+'>Cantidad</th>';
			tHtml += '<th '+class_css_header+'>Ubicación</th>';
			//tHtml += '<th style="border-right: 2px solid lightslategray; text-align:center">&nbsp</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
                tHtml += "<tr>";
                tHtml += '<td '+class_css_btxt+'>' + val.tipo + '</td>';
                tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center">' + val.cantidad + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.ubicacion + '</td>';
				//tHtml += '<td>&nbsp;<a href="#" class="btn btn-search btn-md" title="Editar"><span class="glyphicon glyphicon-pencil"></span></a>&nbsp;';
				//tHtml += '<a href="#" class="btn btn-search btn-md" title="Eliminar"><span class="glyphicon glyphicon-erase"></span></a></td>';
				tHtml += '</tr>';
            });
            $("#gridRT").append(tHtml);
		}
	}); 
 }

function CrearRolesFuncion()
{
	$('#myModal1').modal('show');
}

function CrearNivelEmergencia ()
{
	$('#myModal2').modal('show');
}

function CrearHR ()
{
	$('#myModal3').modal('show');
}

function CrearRecursosFisico ()
{
	$('#myModal4').modal('show');
}

 
function CrearProcOperativos ()
{
	$('#myModal0').modal('show');
}
 ////////////////////////////// ////////////////////////////// ////////////////////////////// ////////////////////////////// ////////////////////////////// //////////////////////////////
 
  
 $( function() {
    $("#tabs_vulneravilidades" ).tabs({
		activate: function (event, ui) {
			ObtenerIdentificacionAmenaza();
			ObtenerPersona();
			ObtenerRecurso();
			ObtenerSistemaProceso();
			ObtenerConsolidado();
		}
	});
	$("#tabs_vulneravilidades").tabs("enable", 0 );
	$("#tabs_vulneravilidades").tabs("disable", 1 );
	$("#tabs_vulneravilidades").tabs("disable", 2 );
	$("#tabs_vulneravilidades").tabs("disable", 3 ); 
	$("#tabs_vulneravilidades").tabs("disable", 4 );
	$("#tabs_vulneravilidades").tabs("disable", 5 );
 });
 
 function ColorRombo(param, id){
	switch(param)
	{
		case "P": $("#color_"+id).css("background", "#009E11");
		break;
		case "PR": $("#color_"+id).css("background", "#FFFF00");
		break;
		case "I": $("#color_"+id).css("background", "#CC0000");
		break;
		default:$("#color_"+id).css("background", "#FFFFFF");
		break;
	}
 }
 
 function CrearVulnerabilidad(){
	$('#myModal5').modal('show');
	ListarPreguntasIdentificacionAmenazas();
	ListaPersonas();
	ListaRecursos();
	ListaSistemasProcesos();
	
 }
 
 function tabs_vulneravilidades_Siguiente(numtab){
	 var sede = $("#IdSede").val();
	 switch(numtab) {
		case 1:
			GuardarIdentificacionAmenazas(sede);
			$("#tabs_vulneravilidades").tabs("enable", numtab );
			$("#tabs_vulneravilidades" ).tabs({ active: numtab });
			break;
		case 2:
			$("#tabs_vulneravilidades").tabs("enable", numtab );
			$("#tabs_vulneravilidades" ).tabs({ active: numtab });
			//ObtenerPersona();
			GuardarPersonas(sede);
			break;
		case 3:
			$("#tabs_vulneravilidades").tabs("enable", numtab );
			$("#tabs_vulneravilidades" ).tabs({ active: numtab });
			//ObtenerRecurso();
			GuardarRecursos(sede);
			break;
		case 4:
			$("#tabs_vulneravilidades").tabs("enable", numtab );
			$("#tabs_vulneravilidades" ).tabs({ active: numtab });
			//ObtenerSistemaProceso();
			GuardarSistemasProcesos(sede);
			break;
		case 5:
			$("#tabs_vulneravilidades").tabs("enable", numtab );
			$("#tabs_vulneravilidades" ).tabs({ active: numtab });
			ObtenerConsolidado();
			ObtenerNivelesRiesgo();
			break;
	}
 }
 
 function ListarPreguntasIdentificacionAmenazas(){
	 $.ajax({
		type: 'POST',
		url: '/PlanEmergencias/ListarPreguntasIdentificacionAmenazas',
		success: function (result) {
			var tHtml = "";
            $("#grid_ident_amenaza").empty();
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+' width="10"></th>';
			tHtml += '<th '+class_css_header+' width="76">Amenaza</th>';
			tHtml += '<th '+class_css_header+' width="56">Origen</th>';
			tHtml += '<th '+class_css_header+' width="142">Fuente de Riesgo</th>';
			tHtml += '<th '+class_css_header+' width="95">Calificación</th>';
			tHtml += '<th '+class_css_header+' width="64">Color</th>';
			tHtml += '</tr>';
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+' colspan="6">Naturales</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
				if(val.tipo=="N")
				{
					tHtml += '<tr>';
					tHtml += '<th align="center"><input id="chk_'+val.pk_id_identificacion_amenazas+'" type="checkbox" value="'+val.pk_id_identificacion_amenazas+'"/></th>';
					tHtml += '<th align="center">'+val.amenaza+'</th>';
					tHtml += '<th align="center"><select id="origen_'+val.pk_id_identificacion_amenazas+'" id="select"><option value="I">Interno</option><option value="E">Externo</option></select></th>';
					tHtml += '<th align="center"><input id="text_'+val.pk_id_identificacion_amenazas+'" type="text"></th>';
					tHtml += '<th align="center"><select id="calificacion_'+val.pk_id_identificacion_amenazas+'" onchange="ColorRombo(this.value,'+val.pk_id_identificacion_amenazas+');"><option value="0">-Seleccione-</option><option value="P">Posible</option><option value="PR">Probable</option><option value="I">Inminente</option></select></th>';
					tHtml += '<th align="center"><div id="color_'+val.pk_id_identificacion_amenazas+'" class="rombo"></div></th>';
					tHtml += '</tr>';
				}
			});
			tHtml += '<tr class ="titulos_tabla">';
			tHtml += '<th '+class_css_header+' colspan="6">Tecnológico</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
				if(val.tipo=="T")
				{
					tHtml += '<tr>';
					tHtml += '<th align="center"><input id="chk_'+val.pk_id_identificacion_amenazas+'" type="checkbox" value="'+val.pk_id_identificacion_amenazas+'"/></th>';
					tHtml += '<th align="center">'+val.amenaza+'</th>';
					tHtml += '<th align="center"><select id="origen_'+val.pk_id_identificacion_amenazas+'"><option value="I">Interno</option><option value="E">Externo</option></select></th>';
					tHtml += '<th align="center"><input id="text_'+val.pk_id_identificacion_amenazas+'" type="text"></th>';
					tHtml += '<th align="center"><select id="calificacion_'+val.pk_id_identificacion_amenazas+'" onchange="ColorRombo(this.value,'+val.pk_id_identificacion_amenazas+');"><option value="0">-Seleccione-</option><option value="P">Posible</option><option value="PR">Probable</option><option value="I">Inminente</option></select></th>';
					tHtml += '<th align="center"><div id="color_'+val.pk_id_identificacion_amenazas+'" class="rombo"></div></th>';
					tHtml += '</tr>';
				}
			});
			tHtml += '<tr class ="titulos_tabla">';
			tHtml += '<th '+class_css_header+' colspan="6">Sociales</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
				if(val.tipo=="S")
				{
					tHtml += '<tr>';
					tHtml += '<th align="center"><input id="chk_'+val.pk_id_identificacion_amenazas+'" type="checkbox" value="'+val.pk_id_identificacion_amenazas+'"/></th>';
					tHtml += '<th align="center">'+val.amenaza+'</th>';
					tHtml += '<th align="center"><select id="origen_'+val.pk_id_identificacion_amenazas+'"><option value="I">Interno</option><option value="E">Externo</option></select></th>';
					tHtml += '<th align="center"><input id="text_'+val.pk_id_identificacion_amenazas+'" type="text"></th>';
					tHtml += '<th align="center"><select id="calificacion_'+val.pk_id_identificacion_amenazas+'" onchange="ColorRombo(this.value,'+val.pk_id_identificacion_amenazas+');"><option value="0">-Seleccione-</option><option value="P">Posible</option><option value="PR">Probable</option><option value="I">Inminente</option></select></th>';
					tHtml += '<th align="center"><div id="color_'+val.pk_id_identificacion_amenazas+'" class="rombo"></div></th>';
					tHtml += '</tr>';
				}
			});
            $("#grid_ident_amenaza").append(tHtml);
		}
	}); 
	
	ObtenerIdentificacionAmenaza();
 }

 function ListaPersonas(){
	 $.ajax({
		type: 'POST',
		url: '/PlanEmergencias/ListaPersonas',
		success: function (result) {
			var tHtml = "";
            $("#grid_personas").empty();
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+' width="402">Aspecto Vulnerable</th>';
			tHtml += '<th '+class_css_header+' width="77">Observación</th>';
			tHtml += '<th '+class_css_header+' width="97">Recomendación</th>';
			tHtml += '<th '+class_css_header+' width="70">Calificación</th>';
			tHtml += '</tr>';
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+' colspan="4">Organización</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
				if(val.tipo=="O")
				{
					tHtml += '<tr>';
					tHtml += '<td '+class_css_btxt+'>' + val.aspectos + '</td>';
					tHtml += '<td '+class_css_btxt+'><input id="p_observacion_' + val.pk_id_personas + '" type="text"></td>';
					tHtml += '<td '+class_css_btxt+'><input id="p_recomendacion_' + val.pk_id_personas + '" type="text"></td>';
					tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><select id="p_calificacion_' + val.pk_id_personas + '"><option value="0.0">0.0</option><option value="0.5">0.5</option><option value="1.0">1.0</option></select><input id="p_tipo_' + val.pk_id_personas + '" value="O" type="hidden"></td>';
					tHtml += '</tr>';
				}
			});
			tHtml += '<tr><td width="92%" align="right" colspan="3" '+class_css_btxt+'>Subtotal</td><td width="8%" align="center"><input name="subtotal_o" id="subtotal_o" type="text" style="border:none;width:40%;background-color:transparent;" readonly /></td></tr>';
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+' colspan="4">Capacitación</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
				if(val.tipo=="C")
				{
					tHtml += '<tr>';
					tHtml += '<td '+class_css_btxt+'>' + val.aspectos + '</td>';
					tHtml += '<td '+class_css_btxt+'><input id="p_observacion_' + val.pk_id_personas + '" type="text"></td>';
					tHtml += '<td '+class_css_btxt+'><input id="p_recomendacion_' + val.pk_id_personas + '" type="text"></td>';
					tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><select id="p_calificacion_' + val.pk_id_personas + '"><option value="0.0">0.0</option><option value="0.5">0.5</option><option value="1.0">1.0</option></select><input id="p_tipo_' + val.pk_id_personas + '" value="C" type="hidden"></td>';
					tHtml += '</tr>';
				}
			});
			tHtml += '<tr><td width="92%" align="right" colspan="3" '+class_css_btxt+'>Subtotal</td><td width="8%" align="center"><input name="subtotal_c" id="subtotal_c" type="text" style="border:none;width:40%;background-color:transparent;" readonly /></td></tr>';
			tHtml += '<tr>';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; vertical-align:middle">&nbsp;</th>';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; vertical-align:middle">&nbsp;</th>';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; vertical-align:middle">&nbsp;</th>';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; vertical-align:middle">&nbsp;</th>';
			tHtml += '</tr>';
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+' colspan="4">Dotación</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
				if(val.tipo=="D")
				{
				    tHtml += '<tr>';
				    tHtml += '<td '+class_css_btxt+'>' + val.aspectos + '</td>';
				    tHtml += '<td '+class_css_btxt+'><input id="p_observacion_' + val.pk_id_personas + '" type="text"></td>';
				    tHtml += '<td '+class_css_btxt+'><input id="p_recomendacion_' + val.pk_id_personas + '" type="text"></td>';
				    tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><select id="p_calificacion_' + val.pk_id_personas + '"><option value="0.0">0.0</option><option value="0.5">0.5</option><option value="1.0">1.0</option></select><input id="p_tipo_' + val.pk_id_personas + '" value="D" type="hidden"></td>';
					tHtml += '</tr>';
				}
			});
			tHtml += '<tr><td width="92%" align="right" colspan="3" '+class_css_btxt+'>Subtotal</td><td width="8%" align="center"><input name="subtotal_d" id="subtotal_d" type="text" style="border:none;width:40%;background-color:transparent;" readonly /></td></tr>';
            $("#grid_personas").append(tHtml);
		}
	}); 
	ObtenerIdentificacionAmenaza();
 }

 function ListaRecursos(){
	 $.ajax({
		type: 'POST',
		url: '/PlanEmergencias/ListaRecursos',
		success: function (result) {
			var tHtml = "";
            $("#grid_recursos").empty();
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+' width="402">Aspecto Vulnerable</th>';
			tHtml += '<th '+class_css_header+' width="77">Observación</th>';
			tHtml += '<th '+class_css_header+' width="97">Recomendación</th>';
			tHtml += '<th '+class_css_header+' width="70">Calificación</th>';
			tHtml += '</tr>';
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+' colspan="4">Materiales</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
				if(val.tipo=="M")
				{
					tHtml += '<tr>';
					tHtml += '<td '+class_css_btxt+'>' + val.aspectos + '</td>';
					tHtml += '<td '+class_css_btxt+'><input id="r_observacion_' + val.pk_id_recursos + '" type="text"></td>';
					tHtml += '<td '+class_css_btxt+'><input id="r_recomendacion_' + val.pk_id_recursos + '" type="text"></td>';
					tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><select id="r_calificacion_' + val.pk_id_recursos + '" id="select"><option value="0.0">0.0</option><option value="0.5">0.5</option><option value="1.0">1.0</option></select><input id="r_tipo_' + val.pk_id_recursos + '" value="M" type="hidden"></td>';
					tHtml += '</tr>';
				}
			});
			tHtml += '<tr><td width="92%" align="right" colspan="3" '+class_css_btxt+'>Subtotal</td><td width="8%" align="center"><input name="subtotal_m" id="subtotal_m" type="text" style="border:none;width:40%;background-color:transparent;" readonly /></td></tr>';
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+' colspan="4">Edificaciones</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
				if(val.tipo=="E")
				{
					tHtml += '<tr>';
					tHtml += '<td '+class_css_btxt+'>' + val.aspectos + '</td>';
					tHtml += '<td '+class_css_btxt+'><input id="r_observacion_' + val.pk_id_recursos + '" type="text"></td>';
					tHtml += '<td '+class_css_btxt+'><input id="r_recomendacion_' + val.pk_id_recursos + '" type="text"></td>';
					tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><select id="r_calificacion_' + val.pk_id_recursos + '" id="select"><option value="0.0">0.0</option><option value="0.5">0.5</option><option value="1.0">1.0</option></select><input id="r_tipo_' + val.pk_id_recursos + '" value="E" type="hidden"></td>';
					tHtml += '</tr>';
				}
			});
			tHtml += '<tr><td width="92%" align="right" colspan="3" '+class_css_btxt+'>Subtotal</td><td width="8%" align="center"><input name="subtotal_e" id="subtotal_e" type="text" style="border:none;width:40%;background-color:transparent;" readonly /></td></tr>';
			tHtml += '<tr>';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; vertical-align:middle">&nbsp;</th>';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; vertical-align:middle">&nbsp;</th>';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; vertical-align:middle">&nbsp;</th>';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; vertical-align:middle">&nbsp;</th>';
			tHtml += '</tr>';
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase" colspan="4">Equipos</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
				if(val.tipo=="EQ")
				{
					tHtml += '<tr class="titulos_filas">';
					tHtml += '<td '+class_css_btxt+'>' + val.aspectos + '</td>';
					tHtml += '<td '+class_css_btxt+'><input id="r_observacion_' + val.pk_id_recursos + '" type="text"></td>';
					tHtml += '<td '+class_css_btxt+'><input id="r_recomendacion_' + val.pk_id_recursos + '" type="text"></td>';
					tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><select id="r_calificacion_' + val.pk_id_recursos + '" id="select"><option value="0.0">0.0</option><option value="0.5">0.5</option><option value="1.0">1.0</option></select><input id="r_tipo_' + val.pk_id_recursos + '" value="EQ" type="hidden"></td>';
					tHtml += '</tr>';
				}
			});
			tHtml += '<tr><td width="92%" align="right" colspan="3" '+class_css_btxt+'>Subtotal</td><td width="8%" align="center"><input name="subtotal_eq" id="subtotal_eq" type="text" style="border:none;width:40%;background-color:transparent;" readonly /></td></tr>';
            $("#grid_recursos").append(tHtml);
		}
	}); 
	ObtenerIdentificacionAmenaza();
 }
 
 function ListaSistemasProcesos(){
	 $.ajax({
		type: 'POST',
		url: '/PlanEmergencias/ListaSistemasProcesos',
		success: function (result) {
			var tHtml = "";
            $("#grid_sistemasprocesos").empty();
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase" width="402">Aspecto Vulnerable</th>';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase" width="77">Observación</th>';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase" width="97">Recomendación</th>';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase" width="70">Calificación</th>';
			tHtml += '</tr>';
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase" colspan="4">Servicios Públicos</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
				if(val.tipo=="SP")
				{
					tHtml += '<tr>';
					tHtml += '<td '+class_css_btxt+'>' + val.aspectos + '</td>';
					tHtml += '<td '+class_css_btxt+'><input id="sp_observacion_' + val.pk_id_sistemas_procesos + '" type="text"></td>';
					tHtml += '<td '+class_css_btxt+'><input id="sp_recomendacion_' + val.pk_id_sistemas_procesos + '" type="text"></td>';
					tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><select id="sp_calificacion_' + val.pk_id_sistemas_procesos + '" id="select"><option value="0.0">0.0</option><option value="0.5">0.5</option><option value="1.0">1.0</option></select><input id="sp_tipo_' + val.pk_id_sistemas_procesos + '" value="SP" type="hidden"></td>';
					tHtml += '</tr>';
				}
			});
			tHtml += '<tr><td width="92%" align="right" colspan="3" '+class_css_btxt+'>Subtotal</td><td width="8%" align="center"><input name="subtotal_sp" id="subtotal_sp" type="text" style="border:none;width:40%;background-color:transparent;" readonly /></td></tr>';
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase" colspan="4">Sistemas Alternos</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
				if(val.tipo=="SA")
				{
					tHtml += '<tr>';
					tHtml += '<td '+class_css_btxt+'>' + val.aspectos + '</td>';
					tHtml += '<td '+class_css_btxt+'><input id="sp_observacion_' + val.pk_id_sistemas_procesos + '" type="text"></td>';
					tHtml += '<td '+class_css_btxt+'><input id="sp_recomendacion_' + val.pk_id_sistemas_procesos + '" type="text"></td>';
					tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><select id="sp_calificacion_' + val.pk_id_sistemas_procesos + '" id="select"><option value="0.0">0.0</option><option value="0.5">0.5</option><option value="1.0">1.0</option></select><input id="sp_tipo_' + val.pk_id_sistemas_procesos + '" value="SA" type="hidden"></td>';
					tHtml += '</tr>';
				}
			});
			tHtml += '<tr><td width="92%" align="right" colspan="3" '+class_css_btxt+'>Subtotal</td><td width="8%" align="center"><input name="subtotal_sa" id="subtotal_sa" type="text" style="border:none;width:40%;background-color:transparent;" readonly /></td></tr>';
			tHtml += '<tr>';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; vertical-align:middle">&nbsp;</th>';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; vertical-align:middle">&nbsp;</th>';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; vertical-align:middle">&nbsp;</th>';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; vertical-align:middle">&nbsp;</th>';
			tHtml += '</tr>';
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase" colspan="4">Recuperación</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
				if(val.tipo=="R")
				{
					tHtml += '<tr>';
					tHtml += '<td '+class_css_btxt+'>' + val.aspectos + '</td>';
					tHtml += '<td '+class_css_btxt+'><input id="sp_observacion_' + val.pk_id_sistemas_procesos + '" type="text"></td>';
					tHtml += '<td '+class_css_btxt+'><input id="sp_recomendacion_' + val.pk_id_sistemas_procesos + '" type="text"></td>';
					tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><select id="sp_calificacion_' + val.pk_id_sistemas_procesos + '" id="select"><option value="0.0">0.0</option><option value="0.5">0.5</option><option value="1.0">1.0</option></select><input id="sp_tipo_' + val.pk_id_sistemas_procesos + '" value="R" type="hidden"></td>';
					tHtml += '</tr>';
				}
			});
			tHtml += '<tr><td width="92%" align="right" colspan="3" '+class_css_btxt+'>Subtotal</td><td width="8%" align="center"><input name="subtotal_r" id="subtotal_r" type="text" style="border:none;width:40%;background-color:transparent;" readonly /></td></tr>';
            $("#grid_sistemasprocesos").append(tHtml);
		}
	}); 
	ObtenerIdentificacionAmenaza();

 }
 
var arIdentAmenazas = new Array();
 function GuardarIdentificacionAmenazas(sede){
	 arIdentAmenazas = new Array();
	 var table = document.getElementById('grid_ident_amenaza');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     for (var i = 0; i < checkboxes.length; i++){
		if(checkboxes[i].checked){
		arIdentAmenazas.push(sede+'|'+checkboxes[i].value+'|'+$("#origen_"+checkboxes[i].value).val()+'|'+$("#text_"+checkboxes[i].value).val()+'|'+$("#calificacion_"+checkboxes[i].value).val());	
		}
	 } 
	 GuardarVulnerabilidades(1, arIdentAmenazas);
 }
 
var arPersonas = new Array();
var arOrganizacion = new Array();
var arCapacitacion = new Array();
var arDotacion = new Array();
 function GuardarPersonas(sede){
	 arPersonas = new Array();
	 var table = document.getElementById('grid_personas');
	 for (var i = 1; i < table.rows.length; i++){
		if($("#p_observacion_"+i).val()!=undefined)
		 if($("#p_observacion_"+i).val()!=""){
			arPersonas.push(sede+'|'+i+'|'+$("#p_observacion_"+i).val()+'|'+$("#p_recomendacion_"+i).val()+'|'+$("#p_calificacion_"+i).val()+'|'+$("#p_tipo_"+i).val());	
			if($("#p_tipo_"+i).val()=="O")
				arOrganizacion.push($("#p_calificacion_"+i).val());
				
			if($("#p_tipo_"+i).val()=="C")
				arCapacitacion.push($("#p_calificacion_"+i).val());
			
			if($("#p_tipo_"+i).val()=="D")
				arDotacion.push($("#p_calificacion_"+i).val());
		 
		 }
	 } 
	 
	 
	 GuardarVulnerabilidades(2, arPersonas);
 }
 
var arRecursos = new Array();
var arMateriales = new Array();
var arEdificacion = new Array();
var arEquipos = new Array();
 function GuardarRecursos(sede){
	 arRecursos = new Array();
	 var table = document.getElementById('grid_recursos');
	 for (var i = 1; i < table.rows.length; i++){
		if($("#r_observacion_"+i).val()!=undefined)
			if($("#r_observacion_"+i).val()!=""){
				arRecursos.push(sede+'|'+i+'|'+$("#r_observacion_"+i).val()+'|'+$("#r_recomendacion_"+i).val()+'|'+$("#r_calificacion_"+i).val()+'|'+$("#r_tipo_"+i).val());	
				if($("#r_tipo_"+i).val()=="M")
					arMateriales.push($("#r_calificacion_"+i).val());
			
				if($("#r_tipo_"+i).val()=="E")
					arEdificacion.push($("#r_calificacion_"+i).val());
				
				if($("#r_tipo_"+i).val()=="EQ")
					arEquipos.push($("#r_calificacion_"+i).val());
				
			}
	 } 
	 
	 GuardarVulnerabilidades(3, arRecursos);
 }
 
var arSP = new Array();
var arServiciosPublicos = new Array();
var arSistemasAlternos = new Array();
var arRecuperacion = new Array();
 function GuardarSistemasProcesos(sede){
	 arSP = new Array();
	 var table = document.getElementById('grid_sistemasprocesos');
	 for (var i = 1; i < table.rows.length; i++){
		if($("#sp_observacion_"+i).val()!=undefined)
			if($("#sp_observacion_"+i).val()!=""){
				arSP.push(sede+'|'+i+'|'+$("#sp_observacion_"+i).val()+'|'+$("#sp_recomendacion_"+i).val()+'|'+$("#sp_calificacion_"+i).val()+'|'+$("#sp_tipo_"+i).val());	
				if($("#sp_tipo_"+i).val()=="SP")
					arServiciosPublicos.push($("#sp_calificacion_"+i).val());
			
				if($("#sp_tipo_"+i).val()=="SA")
					arSistemasAlternos.push($("#sp_calificacion_"+i).val());
				
				if($("#sp_tipo_"+i).val()=="R")
					arRecuperacion.push($("#sp_calificacion_"+i).val());
			
			}
	 } 
	 
	 GuardarVulnerabilidades(4, arSP);
	 GuardarConsolidado(sede);
 }
 
 function GuardarVulnerabilidades(num, arreglo){
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/GuardarVulnerabilidades',
		data: { num : num, arreglo : arreglo },
		traditional: true,
		success: function (result) {
			/*
			arIdentAmenazas = new Array();
			arPersonas = new Array();
			arRecursos = new Array();
			arSP = new Array();*/
		}
	});
 }
 
 function GuardarConsolidado(sede){
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/GuardarConsolidado',
		data: { sede : sede, arOrganizacion : arOrganizacion, arCapacitacion : arCapacitacion, arDotacion : arDotacion, arMateriales : arMateriales, arEdificacion : arEdificacion, arEquipos : arEquipos,arServiciosPublicos : arServiciosPublicos, arSistemasAlternos : arSistemasAlternos, arRecuperacion : arRecuperacion },
		traditional: true,
		success: function (result) {
			arOrganizacion = new Array();
			arCapacitacion = new Array();
			arDotacion = new Array();
			arMateriales = new Array();
			arEdificacion = new Array();
			arEquipos = new Array();
			arServiciosPublicos = new Array();
			arSistemasAlternos = new Array();
			arRecuperacion = new Array();
		}
	 });
 }
 
 var calper = 0;
 function ObtenerConsolidado(){
	 $.ajax({
		type: 'POST',
		url: '/PlanEmergencias/ObtenerConsolidado',
		data : { sede : $("#IdSede").val() },
		success: function (result) {
			$("#grid_consolidados").empty();
			var Html = "";
			Html += '<thead style="border-left:2px solid lightslategray;"><tr class="titulos_tabla">';
			Html += '<th width="136" rowspan="2">Aspecto Vulnerable a Calificar</th>';
			Html += '<th colspan="3" align="center">Riesgo</th>';
			Html += '<th width="70" rowspan="2"  align="center">Calificación</th>';
			Html += '<th width="6" rowspan="2"  align="center">Interpretación</th>';
			Html += '<th width="17" rowspan="2"  align="center">Color</th>';
			Html += '</tr>';
			Html += '<tr class="titulos_tabla">';
			Html += '<th width="50">Bueno 0.0</th>';
			Html += '<th width="50">Regular 0.5</th>';
			Html += '<th width="50">Malo 1.0</th>';
			Html += '</tr></thead>';
			Html += '<tr class="titulos_tabla">';
			Html += '<td colspan="7" align="center">Personas</td>';
			Html += '</tr>';
			Html += '<tr>';
			Html += '<td>Organización</td>';
			var subb = 0;subm = 0;subr = 0;
			var b1 = 0; m1 = 0; r1 = 0;
			var b2 = 0; m2 = 0; r2 = 0;
			var b3 = 0; m3 = 0; r3 = 0;
			if(ValidarRiesgo(result.organizacion)=="B"){
				Html += '<td>'+result.organizacion+'</td>';
				b1 = result.organizacion;
			}else
				Html += '<td>0.0</td>';
		
			if(ValidarRiesgo(result.organizacion)=="R"){
				Html += '<td>'+result.organizacion+'</td>';
				r1 = result.organizacion;
			}else
				Html += '<td>0.0</td>';
			
			if(ValidarRiesgo(result.organizacion)=="M"){
				Html += '<td>'+result.organizacion+'</td>';
				m1 = result.organizacion;
			}else
				Html += '<td>0.0</td>';
	
			Html += '<td rowspan="4" align="center">'+result.calificacion_personas+'</td>';
			Html += '<td rowspan="4" align="center">'+result.interpretacion_personas+'</td>';
			Html += '<td rowspan="4"><div class="rombo" style="background:'+result.color_personas+';"></div></td>';
			Html += '</tr>';
			Html += '<tr>';
			Html += '<td >Capacitación</td>';
			if(ValidarRiesgo(result.capacitacion)=="B"){
				Html += '<td>'+result.capacitacion+'</td>';
				b2 = result.capacitacion;
			}else
				Html += '<td>0.0</td>';
		
			if(ValidarRiesgo(result.capacitacion)=="R"){
				Html += '<td>'+result.capacitacion+'</td>';
				r2 = result.capacitacion;
			}else
				Html += '<td>0.0</td>';
			
			if(ValidarRiesgo(result.capacitacion)=="M"){
				Html += '<td>'+result.capacitacion+'</td>';
				m2 = result.capacitacion;
			}else
				Html += '<td>0.0</td>';
			
			Html += '</tr>';
			Html += '<tr>';
			Html += '<td>Dotación</td>';
			if(ValidarRiesgo(result.dotacion)=="B"){
				Html += '<td>'+result.dotacion+'</td>';
				b3 = result.dotacion;
			}else
				Html += '<td>0.0</td>';
		
			if(ValidarRiesgo(result.dotacion)=="R"){
				Html += '<td>'+result.dotacion+'</td>';
				r3 = result.dotacion;
			}else
				Html += '<td>0.0</td>';
			
			if(ValidarRiesgo(result.dotacion)=="M"){
				Html += '<td>'+result.dotacion+'</td>';
				m3 = result.dotacion;
			}else
				Html += '<td>0.0</td>';
			
			
			subb = b1+b2+b3;
			subr = r1+r2+r3;
			subm = m1+m2+m3;
			Html += '</tr>';
			Html += '<tr class="titulos_tabla">';
			Html += '<td>Subtotal</td>';
			Html += '<td>'+subb.toFixed(2)+'</td>';
			Html += '<td>'+subr.toFixed(2)+'</td>';
			Html += '<td>'+subm.toFixed(2)+'</td>';
			Html += '</tr>';
			Html += '<tr class="titulos_tabla">';
			Html += '<td colspan="7" align="center">Recursos</td>';
			Html += '</tr>';
			Html += '<tr>';
			Html += '<td>Materiales</td>';
			var subb1 = 0;subm1 = 0;subr1 = 0;
			var bb1 = 0; mm1 = 0; rr1 = 0;
			var bb2 = 0; mm2 = 0; rr2 = 0;
			var bb3 = 0; mm3 = 0; rr3 = 0;
			
			if(ValidarRiesgo(result.materiales)=="B"){
				Html += '<td>'+result.materiales+'</td>';
				bb1 = result.materiales;
			}else
				Html += '<td>0.0</td>';
		
			if(ValidarRiesgo(result.materiales)=="R"){
				Html += '<td>'+result.materiales+'</td>';
				rr1 = result.materiales;
			}else
				Html += '<td>0.0</td>';
			
			if(ValidarRiesgo(result.materiales)=="M"){
				Html += '<td>'+result.materiales+'</td>';
				mm1 = result.materiales;
			}else
				Html += '<td>0.0</td>';
			
			
			Html += '<td rowspan="4" align="center">'+result.calificacion_recursos+'</td>';
			Html += '<td rowspan="4" align="center">'+result.interpretacion_recursos+'</td>';
			Html += '<td rowspan="4"><div class="rombo" style="background:'+result.color_recursos+';"></div></td>';
			Html += '</tr>';
			Html += '<tr>';
			Html += '<td>Edificación</td>';
			if(ValidarRiesgo(result.edificacion)=="B"){
				Html += '<td>'+result.edificacion+'</td>';
				bb2 = result.edificacion;
			}else
				Html += '<td>0.0</td>';
		
			if(ValidarRiesgo(result.edificacion)=="R"){
				Html += '<td>'+result.edificacion+'</td>';
				rr2 = result.edificacion;
			}else
				Html += '<td>0.0</td>';
			
			if(ValidarRiesgo(result.edificacion)=="M"){
				Html += '<td>'+result.edificacion+'</td>';
				mm2 = result.edificacion;							
			}else
				Html += '<td>0.0</td>';
			
			
			Html += '</tr>';
			Html += '<tr>';
			Html += '<td>Equipos</td>';
			if(ValidarRiesgo(result.equipos)=="B"){
				Html += '<td>'+result.equipos+'</td>';
				bb3 = result.equipos;
			}else
				Html += '<td>0.0</td>';
		
			if(ValidarRiesgo(result.equipos)=="R"){
				Html += '<td>'+result.equipos+'</td>';
				rr3 = result.equipos;
			}else
				Html += '<td>0.0</td>';
			
			if(ValidarRiesgo(result.equipos)=="M"){
				Html += '<td>'+result.equipos+'</td>';
				mm3 = result.equipos;
			}else
				Html += '<td>0.0</td>';

			subb1 = bb1+bb2+bb3;
			subr1 = rr1+rr2+rr3;
			subm1 = mm1+mm2+mm3;
			Html += '</tr>';
			Html += '<tr class="titulos_tabla">';
			Html += '<td>Subtotal</td>';
			Html += '<td>'+subb1.toFixed(2)+'</td>';
			Html += '<td>'+subr1.toFixed(2)+'</td>';
			Html += '<td>'+subm1.toFixed(2)+'</td>';
			Html += '</tr>';
			Html += '<tr class="titulos_tabla">';
			Html += '<td colspan="7" align="center">Sistemas y Procesos</td>';
			Html += '</tr>';
			Html += '<tr>';
			Html += '<td>Servicios Públicos</td>';
			var subb2 = 0;subm2 = 0;subr2 = 0;
			var bbb1 = 0; mmm1 = 0; rrr1 = 0;
			var bbb2 = 0; mmm2 = 0; rrr2 = 0;
			var bbb3 = 0; mmm3 = 0; rrr3 = 0;
			
			if(ValidarRiesgo(result.servicios_publicos)=="B"){
				Html += '<td>'+result.servicios_publicos+'</td>';
				bbb1 = result.servicios_publicos;
			}else
				Html += '<td>0.0</td>';
		
			if(ValidarRiesgo(result.servicios_publicos)=="R"){
				Html += '<td>'+result.servicios_publicos+'</td>';
				rrr1 = result.servicios_publicos;
			}else
				Html += '<td>0.0</td>';
			
			if(ValidarRiesgo(result.servicios_publicos)=="M"){
				Html += '<td>'+result.servicios_publicos+'</td>';
				mmm1 = result.servicios_publicos;
			}else
				Html += '<td>0.0</td>';
			
			Html += '<td rowspan="4" align="center">'+result.calificacion_sistemas_procesos+'</td>';
			Html += '<td rowspan="4" align="center">'+result.interpretacion_sistemas_procesos+'</td>';
			Html += '<td rowspan="4"><div class="rombo" style="background:'+result.color_sistemas_procesos+';"></div></td>';
			Html += '</tr>';
			Html += '<tr>';
			Html += '<td>Sistemas Alternos</td>';
			if(ValidarRiesgo(result.sistemas_alternos)=="B"){
				Html += '<td>'+result.sistemas_alternos+'</td>';
				bbb2 = result.sistemas_alternos;
			}else
				Html += '<td>0.0</td>';
		
			if(ValidarRiesgo(result.sistemas_alternos)=="R"){
				Html += '<td>'+result.sistemas_alternos+'</td>';
				rrr2 = result.sistemas_alternos;
			}else
				Html += '<td>0.0</td>';
			
			if(ValidarRiesgo(result.sistemas_alternos)=="M"){
				Html += '<td>'+result.sistemas_alternos+'</td>';
				mmm2 = result.sistemas_alternos;
			}else
				Html += '<td>0.0</td>';
			
			Html += '</tr>';
			Html += '<tr>';
			Html += '<td>Recuperación</td>';
			if(ValidarRiesgo(result.recuperacion)=="B"){
				Html += '<td>'+result.recuperacion+'</td>';
				bbb3 = result.recuperacion;
			}else
				Html += '<td>0.0</td>';
		
			if(ValidarRiesgo(result.recuperacion)=="R"){
				Html += '<td>'+result.recuperacion+'</td>';
				rrr3 = result.recuperacion;
			}else
				Html += '<td>0.0</td>';
			
			if(ValidarRiesgo(result.recuperacion)=="M"){
				Html += '<td>'+result.recuperacion+'</td>';
				mmm3 = result.recuperacion;
			}else
				Html += '<td>0.0</td>';

			subb3 = bbb1+bbb2+bbb3;
			subr3 = rrr1+rrr2+rrr3;
			subm3 = mmm1+mmm2+mmm3;
			Html += '</tr>';
			Html += '<tr class="titulos_tabla">';
			Html += '<td>Subtotal</td>';
			Html += '<td>'+subb3.toFixed(2)+'</td>';
			Html += '<td>'+subr3.toFixed(2)+'</td>';
			Html += '<td>'+subm3.toFixed(2)+'</td>';
			Html += '</tr>';
			Html += '<tr>';
			Html += '<td>&nbsp;</td>';
			Html += '<td>&nbsp;</td>';
			Html += '<td>&nbsp;</td>';
			Html += '<td>&nbsp;</td>';
			Html += '<td>&nbsp;</td>';
			Html += '<td>&nbsp;</td>';
			Html += '<td>&nbsp;</td>';
			Html += '</tr>';
			$("#grid_consolidados").append(Html);
		}
	 });
	 
 }

 function ValidarRiesgo(param)
 {
	if(param >= 0.0 && param <= 0.5)
		 return "B";
	 
	if(param >= 0.5 && param <= 1.0)
		 return "R";
	
	if(param >= 1.0)
		 return "M";

 }
 
 
 function ObtenerIdentificacionAmenaza()
 {
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ObtenerIdentificacionAmenaza',
		data : { pk_id_sede : $("#IdSede").val() },
		success: function (result) {
			$.each(result, function (key, val) {
				$("#chk_"+val.fk_id_amenaza+"").prop("checked",true);
				$("#origen_"+val.fk_id_amenaza+"").val(val.origen);
				$("#text_"+val.fk_id_amenaza+"").val(val.fuenteriesgo);
				$("#calificacion_"+val.fk_id_amenaza+"").val(val.calificacion);
				$("#color_"+val.fk_id_amenaza+"").css( "background", val.color);

			});
		}
	 });
 }
 
 function ObtenerPersona()
 {
	var subtotal_o = 0;
	var subtotal_c = 0;
	var subtotal_d = 0;
	$.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ObtenerPersona',
		data : { pk_id_sede : $("#IdSede").val() },
		success: function (result) {
			$.each(result, function (key, val) {
				$("#p_observacion_"+val.fk_id_aspecto+"").val(val.observacion);
				$("#p_recomendacion_"+val.fk_id_aspecto+"").val(val.recomendacion);
				$("#p_calificacion_"+val.fk_id_aspecto+"").val(val.calificacion);
				
				if(val.tipo=="O")
					subtotal_o += parseFloat($("#p_calificacion_"+val.fk_id_aspecto).val());
			
				if(val.tipo=="C")
					subtotal_c += parseFloat($("#p_calificacion_"+val.fk_id_aspecto).val());
			
				if(val.tipo=="D")
					subtotal_d += parseFloat($("#p_calificacion_"+val.fk_id_aspecto).val());
			
			
			});
		},
		complete : function (result){
			 $("#subtotal_o").val(subtotal_o);
			 $("#subtotal_c").val(subtotal_c);
			 $("#subtotal_d").val(subtotal_d);
		}
	 });
	 
	
 }

 function ObtenerRecurso()
 {
	var subtotal_m = 0;
	var subtotal_e = 0;
	var subtotal_eq = 0;
	$.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ObtenerRecurso',
		data : { pk_id_sede : $("#IdSede").val() },
		success: function (result) {
			$.each(result, function (key, val) {
				$("#r_observacion_"+val.fk_id_aspecto+"").val(val.observacion);
				$("#r_recomendacion_"+val.fk_id_aspecto+"").val(val.recomendacion);
				$("#r_calificacion_"+val.fk_id_aspecto+"").val(val.calificacion);
				if(val.tipo=="M")
					subtotal_m += parseFloat($("#r_calificacion_"+val.fk_id_aspecto).val());
			
				if(val.tipo=="E")
					subtotal_e += parseFloat($("#r_calificacion_"+val.fk_id_aspecto).val());
			
				if(val.tipo=="EQ")
					subtotal_eq += parseFloat($("#r_calificacion_"+val.fk_id_aspecto).val());
			
			});
		},
		complete : function (result){
			 $("#subtotal_m").val(subtotal_m);
			 $("#subtotal_e").val(subtotal_e);
			 $("#subtotal_eq").val(subtotal_eq);
		}
	 });
 }
 
 function ObtenerSistemaProceso()
 {
	var subtotal_sp = 0;
	var subtotal_sa = 0;
	var subtotal_r = 0;
	$.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ObtenerSistemaProceso',
		data : { pk_id_sede : $("#IdSede").val() },
		success: function (result) {
			$.each(result, function (key, val) {
				$("#sp_observacion_"+val.fk_id_aspecto+"").val(val.observacion);
				$("#sp_recomendacion_"+val.fk_id_aspecto+"").val(val.recomendacion);
				$("#sp_calificacion_"+val.fk_id_aspecto+"").val(val.calificacion);
				
				if(val.tipo=="SP")
					subtotal_sp += parseFloat($("#sp_calificacion_"+val.fk_id_aspecto).val());
			
				if(val.tipo=="SA")
					subtotal_sa += parseFloat($("#sp_calificacion_"+val.fk_id_aspecto).val());
			
				if(val.tipo=="R")
					subtotal_r += parseFloat($("#sp_calificacion_"+val.fk_id_aspecto).val());
				
			});
		},
		complete : function (result){
			 $("#subtotal_sp").val(subtotal_sp);
			 $("#subtotal_sa").val(subtotal_sa);
			 $("#subtotal_r").val(subtotal_r);
		}
	 });
 }
 
 function AnalisisRiesgo()
 {
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/AnalisisRiesgo',
		data : { pk_id_sede : $("#IdSede").val() },
		success: function (result) {
			var tHtml = "";
            $("#gridriesgo").empty();
			tHtml += '<thead style="border-left:2px solid lightslategray;"><tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+'>Amenazas</td>';
			tHtml += '<th '+class_css_header+'>Generar Intervención</th>';
			tHtml += '<th '+class_css_header+'>Plan de Acción</th>';
			tHtml += '</tr></thead>';
			$.each(result, function (key, val) {
                tHtml += '<tbody style="border-left:2px solid lightslategray;"><tr class="titulos_filas">';
                tHtml += '<td '+class_css_btxt+'>' + val.amenaza + '</td>';
                tHtml += '<td '+class_css_btxt+'><input id="archk_' + val.pk_id_amenazas + '" type="checkbox" value="' + val.pk_id_amenazas + '"/></td>';
                tHtml += '<td '+class_css_btxt+'><input id="riesgo_' + val.pk_id_amenazas + '" type="text" class="form-control"></td>';
				tHtml += '</tr></tbody>';
            });
            $("#gridriesgo").append(tHtml);
		}
	 });
	 
	 ObtenerAnalisisRiesgo();
 }
 
  function ObtenerAnalisisRiesgo()
 {
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ObtenerAnalisisRiesgo',
		data : { pk_id_sede : $("#IdSede").val() },
		success: function (result) {
			$.each(result, function (key, val) {
				$("#archk_"+val.fk_id_identamenaza+"").prop('checked', true);
				$("#riesgo_"+val.fk_id_identamenaza+"").val(val.plan_de_accion);
            });
		}
	 });
 }
 
 
 function guardarriesgo(numtab)
 {
	 var arAmenazas = new Array();
	 var table = document.getElementById('gridriesgo');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     for (var i = 0; i < checkboxes.length; i++){
		if(checkboxes[i].checked){
			arAmenazas.push(checkboxes[i].value+'|'+$("#riesgo_"+checkboxes[i].value).val());	
		}
	 } 
	 
	 $.ajax({
		type: 'GET',
		url: '/PlanEmergencias/GuardarAnalisisRiesgos',
		data: { sede: $("#IdSede").val(), arAmenazas: arAmenazas },
		traditional: true,
		success: function (result) {
			arAmenazas = new Array();
			$( "#tabs" ).tabs({ active: numtab });
		}
	 });
 }
 
function tabs_vulneravilidades_Guardar()
{
	$('#myModal5').modal('hide');
	AnalisisRiesgo();
}

function soloNumeros(e){
	var key = window.Event ? e.which : e.keyCode
	return (key >= 48 && key <= 57)
}


function ObtenerNivelesRiesgo(){
	$.ajax({
		type: 'GET',
		url: '/PlanEmergencias/ObtenerNivelesRiesgo',
		data : { isede : $("#IdSede").val() },
		success: function (result) {	
			var tHtml = "";
			$("#grid_nivel_riesgo").empty();
			tHtml += '<thead style="border-left:2px solid lightslategray;"><tr class="titulos_tabla"><td colspan="3" align="center">Naturales</td></tr>';	
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+'>Amenaza</th>';
			tHtml += '<th '+class_css_header+'>Diamante de Riesgo</th>';
			tHtml += '<th '+class_css_header+'>Interpretación</th>';
			tHtml += '</tr></thead>';
			$.each(result, function (key, val) {
				if(val.tipo=='N')
				{
					tHtml += '<tbody style="border-left:2px solid lightslategray;"><tr class="titulos_filas">';
					tHtml += '<td>&nbsp;'+val.amenaza+'</td>';
					tHtml += '<td '+class_css_btxt+'>';
					tHtml += '<div class="animation-1">';
					tHtml += '<div class="box1" style="background-color:'+val.color_r+';"><div class="rdiv" style="border:none;text-align:center;">R</div></div>';
					tHtml += '<div class="box2" style="background-color:'+val.color_s+';"><div class="rdiv" style="border:none;text-align:center;">S</div></div>';
					tHtml += '<div class="box3" style="background-color:'+val.color_p+';"><div class="rdiv" style="border:none;text-align:center;">P</div></div>';
					tHtml += '<div class="box4" style="background-color:'+val.color_a+';"><div class="rdiv" style="border:none;text-align:center;">A</div></div>';
					tHtml += '</div>'; 
					tHtml += '</td>';
					tHtml += '<td align="center">&nbsp;'+val.interpretacion+'</td>';
					tHtml += '</tr></thead>';
				}
            });
			tHtml += '<thead style="border-left:2px solid lightslategray;"><tr class="titulos_tabla"><td colspan="3" align="center">Tecnológicos</td></tr>';	
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+'>Amenaza</th>';
			tHtml += '<th '+class_css_header+'>Diamante de Riesgo</th>';
			tHtml += '<th '+class_css_header+'>Interpretación</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
				if(val.tipo=='T')
				{
					tHtml += '<tbody style="border-left:2px solid lightslategray;"><tr class="titulos_filas">';
					tHtml += '<td>&nbsp;'+val.amenaza+'</td>';
					tHtml += '<td '+class_css_btxt+'>';
					tHtml += '<div class="animation-1">';
					tHtml += '<div class="box1" style="background-color:'+val.color_r+';"><div class="rdiv" style="border:none;text-align:center;">R</div></div>';
					tHtml += '<div class="box2" style="background-color:'+val.color_s+';"><div class="rdiv" style="border:none;text-align:center;">S</div></div>';
					tHtml += '<div class="box3" style="background-color:'+val.color_p+';"><div class="rdiv" style="border:none;text-align:center;">P</div></div>';
					tHtml += '<div class="box4" style="background-color:'+val.color_a+';"><div class="rdiv" style="border:none;text-align:center;">A</div></div>';
					tHtml += '</div>'; 
					tHtml += '</td>';
					tHtml += '<td align="center">&nbsp;'+val.interpretacion+'</td>';
					tHtml += '</tr></thead>';
				}
            });
			tHtml += '<thead style="border-left:2px solid lightslategray;"><tr class="titulos_tabla"><td colspan="3" align="center">Sociales</td></tr>';	
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+'>Amenaza</th>';
			tHtml += '<th '+class_css_header+'>Diamante de Riesgo</th>';
			tHtml += '<th '+class_css_header+'>Interpretación</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
				if(val.tipo=='S')
				{
					tHtml += '<tbody style="border-left:2px solid lightslategray;"><tr class="titulos_filas">';
					tHtml += '<td>&nbsp;'+val.amenaza+'</td>';
					tHtml += '<td '+class_css_btxt+'>';
					tHtml += '<div class="animation-1">';
					tHtml += '<div class="box1" style="background-color:'+val.color_r+';"><div class="rdiv" style="border:none;text-align:center;">R</div></div>';
					tHtml += '<div class="box2" style="background-color:'+val.color_s+';"><div class="rdiv" style="border:none;text-align:center;">S</div></div>';
					tHtml += '<div class="box3" style="background-color:'+val.color_p+';"><div class="rdiv" style="border:none;text-align:center;">P</div></div>';
					tHtml += '<div class="box4" style="background-color:'+val.color_a+';"><div class="rdiv" style="border:none;text-align:center;">A</div></div>';
					tHtml += '</div>'; 
					tHtml += '</td>';
					tHtml += '<td align="center">&nbsp;'+val.interpretacion+'</td>';
					tHtml += '</tr></thead>';
				}
            });

			$("#grid_nivel_riesgo").append(tHtml);
		}
	 });
}


function DescargarExcel(parametro)
{
	$.ajax({
		type: 'POST',
		url: '/PlanEmergencias/DescargarExcel',
		data:{ parametro : parametro },
		success: function (result) {
			  window.location = '/PlanEmergencias/DownloadExcel?file=' + result;	  
		}
	});
	
}
 