var jqXHRData;
$(function () {
	$( "#tabs" ).tabs();
    $('#hora_inicio').timepicker({
        'step': function (i) {
            return (i % 2) ? 15 : 45;
        },
		//'noneOption': [{'label': '12:00M','value': '12:00AM'}]
    });
	
    $('#hora_fin').timepicker({
        'step': function (i) {
            return (i % 2) ? 15 : 45;
        }
    });

    ConstruirDatePickerPorElemento('fecha_programada');
	initSimpleFileUpload();
	
	//Handler for "Start upload" button 
	 $("#hl-start-upload").on('click', function () {
		if (jqXHRData) {
			jqXHRData.submit();
		}
		return false;
	}); 

});

function initSimpleFileUpload() {
    'use strict';
    $('#upload').fileupload({
        url: '/PlanCapacitacion/SubirArchivo',
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
					url: '/PlanCapacitacion/ActualizarAdjuntos',
					data: { pk_id_plan_capacitacion : $("#pk_id_plan_capacitacion_temp").val(), adjunto : $("#adjunto_tmp").val()  },
					traditional: true,
					success: function (result) {
						$('#pk_id_soporte').val(result);
						swal("Estimado Usuario", 'El archivo ha sido cargado satisfactoriamente',"success");
					}
				});
			}
			else
			{
				swal("Estimado Usuario", 'Solo se admiten archivos PDF',"warning");
			}
        },
        fail: function (event, data) {
            swal("Estimado Usuario", 'Debe cargar un archivo con peso menor a 10 MB.','warning');
			if (data.files[0].error) {
                //alert(data.files[0].error);
            }
        }
    });
}

function DescargarArchivo()
{
	$.ajax({
		type: 'POST',
		url: '/PlanCapacitacion/DescargarArchivo',
		data:{ pk_id_soporte : $("#pk_id_plan_capacitacion_temp").val() },
		success: function (result) {
			  window.location = '/PlanCapacitacion/Download?file=' + result;  
		}
	});	
}

var class_css_header = 'style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase"';
var class_css = 'style="border-right: 2px solid lightslategray; vertical-align:middle"';
var class_css_btxt = 'style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center; cursor:pointer"';

function CrearPlanCapacitacion(){
	$('#myModal1').modal('show');
	$("#pk_id_plan_capacitacion").val(0);
	$("#fk_id_tipo_actividad").val(0);
	$("#fk_id_rol").val(0);
	$("#fk_id_competencia").val(0);
	$("#tema").val("");
	$("#fecha_programada").val("");
	$("#hora_inicio").val("");
	$("#hora_fin").val("");
}

function AgregarEmpleados(pk_id_plan_capacitacion){
	$('#myModal2').modal('show');
	$("#pk_id_plan_capacitacion_temp").val(pk_id_plan_capacitacion);
	CargarPersonas();
	Confirmados();
	Asistentes();
}

function CargarPersonas(){
	$.ajax({
        type: 'GET',
        url: '/PlanCapacitacion/ListarEmpleadosxTipo',
		data: { pk_id_plan_capacitacion : $("#pk_id_plan_capacitacion_temp").val() },
        success: function (result) {
			var tHtml = "";
            $("#gridinvitados").empty();
            tHtml += '<tr class="titulos_tabla">';
            tHtml += '<th ' + class_css_header + '><input id="chktodos" type="checkbox" onclick="CheckTodos();" /></th>';
			tHtml += '<th '+class_css_header+'>Número de Documento</th>';
			tHtml += '<th '+class_css_header+'>Nombres</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
			    tHtml += '<tr id="_pges" name="_pges" class="_pges">';
			    tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="checkbox" name="checkbox" id="chk" value="' + val.Value + ',' + val.Text + '"></td>';
                tHtml += '<td '+class_css+'>'+val.Value+'</td>';
				tHtml += '<td '+class_css+'>'+val.Text+'</td>';
				tHtml += '</tr>';
            });
            $("#gridinvitados").append(tHtml);
        }
    });
}

var arPersonas = new Array();
function EnviarInvitaciones(){
	 var table = document.getElementById ('gridinvitados');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     for (var i = 0; i < checkboxes.length; i++){
			if(checkboxes[i].checked){
				arPersonas.push(checkboxes[i].value);
			}
	 }
	
	$.ajax({
		type: 'GET',
		url: '/PlanCapacitacion/GuardarAsignaciones',
		data: { pk_id_plan_capacitacion : $("#pk_id_plan_capacitacion_temp").val(),  asignaciones : arPersonas },
		traditional : true,
		success: function (result) {
			arPersonas = new Array();
			$( "#tabs" ).tabs({ active: 1 });	
			Confirmados();
		}
	});

	
}

var arPersonas = new Array();
function ConfirmarAsistencia(){
	 var table = document.getElementById ('gridconfirmados');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     for (var i = 0; i < checkboxes.length; i++){
			if(checkboxes[i].checked){
				arPersonas.push(checkboxes[i].value);
			}
	 }
	
	$.ajax({
		type: 'GET',
		url: '/PlanCapacitacion/GuardarAsistentes',
		data: { pk_id_plan_capacitacion : $("#pk_id_plan_capacitacion_temp").val(),  asignaciones : arPersonas },
		traditional : true,
		success: function (result) {
			arPersonas = new Array();
			$( "#tabs" ).tabs({ active: 2 });	
			Asistentes();
		}
	});
}

function Confirmados(){
	$.ajax({
        type: 'GET',
        url: '/PlanCapacitacion/ListarEmpleadosAsignados',
		data: { pk_id_plan_capacitacion : $("#pk_id_plan_capacitacion_temp").val() },
        success: function (result) {
			var tHtml = "";
            $("#gridconfirmados").empty();
            tHtml += '<tr class="titulos_tabla">';
            tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase"><input id="chktodos1" type="checkbox" onclick="CheckTodos1();" /></th>';
            tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase">Número de Documento</th>';
            tHtml += '<th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase">Nombres</th>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
			    tHtml += '<tr>';
			    tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="checkbox" name="checkbox" id="chk" value="' + val.numero_documento + ',' + val.nombre + '"></td>';
                tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center">' + val.numero_documento + '</td>';
                tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle">' + val.nombre + '</td>';
				tHtml += '</tr>';
            });
            $("#gridconfirmados").append(tHtml);
        }
    });
}

function Asistentes(){
	$.ajax({
        type: 'GET',
        url: '/PlanCapacitacion/ListarAsistentes',
		data: { pk_id_plan_capacitacion : $("#pk_id_plan_capacitacion_temp").val() },
        success: function (result) {
			var tHtml = "";
            $("#gridasistentes").empty();
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase">Número de Documento</td>';
			tHtml += '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase">Nombres</td>';
			tHtml += '</tr>';
			$.each(result, function (key, val) {
                tHtml += '<tr>';
                tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center">' + val.numero_documento + '</td>';
                tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle">' + val.nombre + '</td>';
				tHtml += '</tr>';
            });
            $("#gridasistentes").append(tHtml);
        }
    });
}

ObtenerActividadporSede();
function ObtenerActividadporSede() 
{
    $.ajax({
        type: 'GET',
        url: '/PlanCapacitacion/ObtenerActividad',
        success: function (result) {
            var tHtml = "";
            $("#planactividadxempresa").empty();
            tHtml += '<thead>';
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th colspan="13" style="background-color: #7E8A97; text-transform:uppercase" align="center"><center><font color="white">Programación </font></center></th></tr>';
            tHtml += '<tr class="titulos_tabla">';
            tHtml += '<th '+class_css_header+'>Enero</th>';
            tHtml += '<th '+class_css_header+'>Febrero</th>';
            tHtml += '<th '+class_css_header+'>Marzo</th>';
            tHtml += '<th '+class_css_header+'>Abril</th>';
            tHtml += '<th '+class_css_header+'>Mayo</th>';
            tHtml += '<th '+class_css_header+'>Junio</th>';
            tHtml += '<th '+class_css_header+'>Julio</th>';
            tHtml += '<th '+class_css_header+'>Agosto</th>';
            tHtml += '<th '+class_css_header+'>Septiembre</th>';
            tHtml += '<th '+class_css_header+'>Octubre</th>';
            tHtml += '<th '+class_css_header+'>Noviembre</th>';
            tHtml += '<th '+class_css_header+'>Diciembre</th>';
            tHtml += '<th width="150%" '+class_css_header+'>Acciones</th>';
            tHtml += '</tr></thead>';
			var cont = 0;
            $.each(result, function (key, val) {
                tHtml += '<tbody><tr>';
                var fec = val.fecha_programada.split("/");
                var mes = parseInt(fec[1]);
                var color = "";
                for (var i = 1; i <= 12; i++) {
                    if (i == mes) {
                        tHtml += '<td '+class_css_btxt+' onclick="ObtenerActividad(' + val.pk_id_plan_capacitacion + ')">';
						tHtml += '<div style="background-color:#009900;"><font color="#FFFFFF">' + val.fecha_programada + ' ' + val.hora_inicio + ' - ' + val.hora_fin + '</font></div>';
						tHtml += '</td>';
                    }

                    if (i == 12) {
						tHtml += '<td '+class_css_btxt+'><a href="#" class="btn btn-search btn-md" onclick="AgregarEmpleados(' + val.pk_id_plan_capacitacion + ');return false;" title="Invitar Asistentes"><span class="glyphicon glyphicon-user"></span></a>&nbsp;|&nbsp;<a href="#" class="btn btn-search btn-md" onclick="EliminarActividades(' + val.pk_id_plan_capacitacion + ');return false;" title="Eliminar"><span class="glyphicon glyphicon-erase"></span></a>';
                    }
                    else
                        tHtml += '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center">&nbsp;</td>';

                }

				cont++;
                tHtml += '</tr></tbody>';
            });
            $("#planactividadxempresa").append(tHtml);
        }
    });
}

function GuardarActividad() {
   ValidaGuardarActividades();
	if ($("#frmActividades").valid()) {
		
		var start = timeToInt($("#hora_inicio").val());
		var end = timeToInt($("#hora_fin").val());
		
		//alert(start+' > '+end);
		
		if (start > end) {
		  swal("Estimado Usuario", 'La Hora inicial debe ser inferior a la hora final', "warning");
		  return 
		}
		
		
		$('#myModal1').modal('hide');
		PopupPosition();
		var FormArray = new Array();
		$.ajax({
			type: 'GET',
			url: '/PlanCapacitacion/GuardarActividades',
			data:  $("#frmActividades").serialize() ,
			traditional: true,
			success: function (result) {
				OcultarPopupposition();
				ObtenerActividadporSede();
				swal("Estimado Usuario", 'El Plan de Capacitación ha sido guardado satisfactoriamente', "success");
			}
		});
	}
}

function timeToInt(time) {
    var arr = time.match(/^(0?[1-9]|1[012]):([0-5][0-9])([APap][mM])$/);
    if (arr == null) return -1;

    if (arr[3].toUpperCase() == 'PM') {
      arr[1] = parseInt(arr[1]) + 12;
    }
    return parseInt(arr[1]*100) + parseInt(arr[2]);
  }

function ValidaGuardarActividades(){
	$("#frmActividades").validate({
        errorClass: "error",
        rules: {
			tema:{
                required: true
            },
			fecha_programada:{
                required: true
            },
			hora_inicio:{
                required: true
            },
			hora_fin:{
                required: true
            }
        },
        messages: {
			tema:{
                required: "Este campo es requerido"
            },
            fecha_programada:{
                required: "Este campo es requerido"
            },
			hora_inicio:{
                required: "Este campo es eequerido"
            },
			hora_fin:{
                required: "Este campo es requerido"
            }
        }
    });
	
}

function CargarCompetencia(idRol){
	$.ajax({
        type: 'GET',
        url: '/PlanCapacitacion/CompetenciasxRol',
        data: { idRol: idRol },
        traditional: true,
        success: function (result) {
		 $("#fk_id_competencia").empty();
		 $.each(result, function(k,v){
			$("#fk_id_competencia").append("<option value=\""+v.Value+"\">"+v.Text+"</option>");
		 });
		}
    });
}

function ObtenerActividad(pk_id_plan_capacitacion) {
    $.ajax({
        type: 'GET',
        url: '/PlanCapacitacion/ObtenerActividadxId',
        data: { pk_id_plan_capacitacion: pk_id_plan_capacitacion },
        traditional: true,
        success: function (result) {
        $('#myModal1').modal('show');
            $("#pk_id_plan_capacitacion").val(result.pk_id_plan_capacitacion);
			$("#fk_id_tipo_actividad").val(result.fk_id_tipo_actividad);
			$("#tema").val(result.tema);
			$("#fk_id_rol").val(result.fk_id_rol);
			$("#fk_id_competencia").val(result.fk_id_competencia);
			$("#fecha_programada").val(result.fecha_programada);
            $("#hora_inicio").val(result.hora_inicio);
            $("#hora_fin").val(result.hora_fin);
        }
    });
}

function EliminarActividades(pk_id_plan_capacitacion) {
	 swal({
        title: "Estimado Usuario",
        text: "¿Está seguro que desea eliminar el Plan Programado?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si",
        cancelButtonText: "No",
        closeOnConfirm: false
    },
	function () {

	    swal("Estimado Usuario", "El plan de Capacitacion ha sido eliminado con éxito.", "success");
	    $.ajax({
        type: 'GET',
        url: '/PlanCapacitacion/EliminarActividad',
        data: { pk_id_plan_capacitacion : pk_id_plan_capacitacion },
		traditional: true,
        success: function (result) {
			ObtenerActividadporSede();
        }
    });
	});
	
}

function CheckTodos(){
	var booVal = "";
	if($("#chktodos").is(':checked'))
		booVal = true;
	else
		booVal = false;

	 var table = document.getElementById ('gridinvitados');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     for (var i = 0; i < checkboxes.length; i++){
			checkboxes[i].checked = booVal;
	 }
}

function CheckTodos1(){
	var booVal = "";
	if($("#chktodos1").is(':checked'))
		booVal = true;
	else
		booVal = false;

	 var table = document.getElementById ('gridconfirmados');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     for (var i = 0; i < checkboxes.length; i++){
			checkboxes[i].checked = booVal;
	 }
}
