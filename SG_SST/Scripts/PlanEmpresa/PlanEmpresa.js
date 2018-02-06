// jqXHRData needed for grabbing by data object of fileupload scope
var jqXHRData;
var class_css_header = 'style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase"';
var class_css = 'style="border-right: 2px solid lightslategray; vertical-align:middle"';
var class_css_btxt = 'style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"';

$(function () {
    $('#HoraProgIni').timepicker({ 'step': 15 });
    $('#HoraProgFin').timepicker({
        'step': function (i) {
            return (i % 2) ? 15 : 45;
        }
    });

    ConstruirDatePickerPorElemento('FechaDesde');
    ConstruirDatePickerPorElemento('FechaHasta');
    ConstruirDatePickerPorElemento('FechaProg');
});

Array.prototype.unique=function(a){
  return function(){return this.filter(a)}}(function(a,b,c){return c.indexOf(a,b+1)<0
});

$(document).ready(function () {

	//Initialization of fileupload
	initSimpleFileUpload();
	initSimpleFileUpload1();
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
});

function initSimpleFileUpload() {
    'use strict';
    $('#upload').fileupload({
        url: '/PlanTrabajoAnual/SubirArchivo',
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
			$("#RepresentanteLegal").val(data.result.message);
			$.ajax({
			type: 'GET',
			url: '/PlanTrabajoAnual/ActualizarAdjuntos',
			data: { pk_id_plan_empresa : $("#pk_id_plan_empresa").val(), RepresentanteLegal : $("#RepresentanteLegal").val() , RepresentanteSGSST : $("#RepresentanteSGSST").val() },
			traditional: true,
			success: function (result) {
			    swal("Estimado Usuario", 'La Firma Representante Legal ha sido guardada', "success");
			}
			});
			
			//FormArray.push($("#RepresentanteSGSST").val());
        },
        fail: function (event, data) {
            swal("Estimado Usuario", 'Debe cargar un archivo con peso menor a 10 MB.','warning');
			if (data.files[0].error) {
                //alert(data.files[0].error);
            }
        }
    });
}

function initSimpleFileUpload1() {
    'use strict';
    $('#upload1').fileupload({
        url: '/PlanTrabajoAnual/SubirArchivo',
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
			$("#RepresentanteSGSST").val(data.result.message);
			$.ajax({
			type: 'GET',
			url: '/PlanTrabajoAnual/ActualizarAdjuntos',
			data: { pk_id_plan_empresa : $("#pk_id_plan_empresa").val(), RepresentanteLegal : $("#RepresentanteLegal").val() , RepresentanteSGSST : $("#RepresentanteSGSST").val() },
			traditional: true,
			success: function (result) {
			    swal("Estimado Usuario", 'La Firma Representante SGSST ha sido guardada', "success");
			}
			});
			//FormArray.push($("#RepresentanteSGSST").val());
        },
        fail: function (event, data) {
            swal("Estimado Usuario", 'Debe cargar un archivo con peso menor a 10 MB.','warning');
			if (data.files[0].error) {
               // alert(data.files[0].error);
            }
        }
    });
}

function CargarVigencia(fecha)
{
	var fec = fecha.split('/');
	$("#anioVigencia").val(fec[2]);
}

function CrearPlan()
{
	$("#planempresa").empty();
	$("#planactividadxempresa").empty();
	$("#tblresumen").empty();
	$("#adjuntos").css("display","none");
	$("#divporc").css("display","none");
	ValidaPlanEmpresa();
	if ($("#frmplanempresa").valid()) {
		if(ValidarFechaPlan($("#FechaDesde").val(),$("#FechaHasta").val(),$("#anioVigencia").val()))
		{
			var dias = parseInt(restaFechas($("#FechaDesde").val(),$("#FechaHasta").val()));
			if(dias <= 365)
			{
				PopupPosition();
				$.ajax({
					type: 'GET',
					url: '/PlanTrabajoAnual/GuardaPlanEmpresa',
					data: $("#frmplanempresa").serialize() ,
					traditional: true,
					success: function (result) {
						OcultarPopupposition();
						$("#pk_id_plan_empresa").val(result);
						CargarPlanAnual($("#IdSede").val());
						
					}
				});
			}
			else
				swal("Estimado Usuario", 'Las Fechas seleccionadas no deben sobrepasar los 12 meses','warning');
			
		}
		else
			swal("Estimado Usuario", 'La Fecha Hasta no puede ser menor a la Fecha Desde, el rango debe estar dentro de la vigencia','warning');
		
	}
	
}

function CargarMetas(id){ 
	$("#ObjetivosMetas").val("");
	$.ajax({
		type: 'GET',
		url: '/PlanTrabajoAnual/ObtenerMeta',
		data: { pk_id_objetivo : id },
		success: function (result) {
		 
		 $("#ObjetivosMetas").val(result);
		}
	});
}

function CargarPlanAnual(param)
{
	$("#planempresa").empty();
	$("#planactividadxempresa").empty();
	$("#tblresumen").empty();
	$("#adjuntos").css("display","none");
	$("#divporc").css("display","none");
	$.ajax({
		type: 'GET',
		url: '/PlanTrabajoAnual/ObternerPlanAnualporSede',
		data: { IDSede : param },
		success: function (result) {
			var tHtml = "";
            $("#planempresa").empty();
			tHtml += '<thead><tr class="titulos_tabla">';
			tHtml += '<th width="117" rowspan="2" '+class_css_header+'>Sede</th>';
			tHtml += '<th colspan="2" '+class_css_header+'>Periodo</th>';
			tHtml += '<th width="122" '+class_css_header+'>&nbsp;</th>';
			tHtml += '<th width="97" '+class_css_header+'>&nbsp;</th>';
			tHtml += '</tr>';
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th width="122" '+class_css_header+'>Fecha Desde</th>';
			tHtml += '<th width="115" '+class_css_header+'>Fecha Hasta</th>';
			tHtml += '<th '+class_css_header+'>Vigencia</th>';
			tHtml += '<th '+class_css_header+'>Acciones</th>';
			tHtml += '</tr></thead>';
			//alert(JSON.stringify(result));
			 $.each(result, function (key, val) {
				 tHtml += '<tbody><tr style="cursor:pointer;" '+class_css_btxt+' onclick="ObtenerActividadporSede('+val.PkEmpresa+');Change(this);">';
				 tHtml += '<th '+class_css_btxt+'>&nbsp;<b>'+val.Sede+'</b></th>';
				 tHtml += '<th '+class_css_btxt+'>&nbsp;'+val.FechaDesde+'</th>';
				 tHtml += '<th '+class_css_btxt+'>&nbsp;'+val.FechaHasta+'</th>';
				 tHtml += '<th '+class_css_btxt+'>&nbsp;'+val.Vigencia+'</th>';
				 tHtml += '<th '+class_css_btxt+'>&nbsp;<a href="#" onclick="CargarActividad('+val.PkEmpresa+');" class="btn btn-search btn-md" title="Agregar"><span class="glyphicon glyphicon-plus"></span></a>&nbsp;|&nbsp;';
				 tHtml += '<a href="#" class="btn btn-search btn-md" onclick="CargarResumen(' + val.PkEmpresa + ');" title="Ver Resumen"><span class="glyphicon glyphicon-search"></span></a>&nbsp;|&nbsp;';
				 tHtml += '<a href="#" onclick="EliminarPlanAnual(' + val.PkEmpresa + ');" class="btn btn-search btn-md" title="Eliminar"><span class="glyphicon glyphicon-erase"></span></a></th>';
				 tHtml += '</tr></tbody>';
			 });
			 $("#planempresa").append(tHtml);
			 $("#adjuntos").css("display","none");
			 $("#divporc").css("display","none");
		}
	});
	
} 

function ValidaPlanEmpresa(){
	$("#frmplanempresa").validate({
        errorClass: "error",
        rules: {
            IdSede: {
                required: true
            },
            Vigencia: {
                required: true
            },
            FechaDesde: {
                required: true
            },
            FechaHasta: {
                required: true
            }
        },
        messages: {
            IdSede: {
				required: "Seleccione una Sede"
            },
            Vigencia: {
                required: "Seleccione una Vigencia"
            },
            FechaDesde: {
                required: "Seleccione una Fecha Desde"
            },
            FechaHasta: {
                required: "Seleccione una Fecha Hasta"
            }
        }
    });
}

function ValidarFechaPlan(fechaini, fechafin, vigencia){
	fechaini = fechaini.toString();
	fechafin = fechafin.toString();
	
	
	var fecini = fechaini.split('/');
	var fecfin = fechafin.split('/');
	
	var fecini1 = fecini[2]+"-"+fecini[1]+"-"+fecini[1];
	var fecfin1 = fecfin[2]+"-"+fecfin[1]+"-"+fecfin[1];

	if(fecfin1 < fecini1)
		return false;
	
	if(vigencia < fecini[2])
		return false;

	if(vigencia > fecfin[2])
		return false;

	
	return true;
}

restaFechas = function(f1,f2){
 var aFecha1 = f1.split('/'); 
 var aFecha2 = f2.split('/'); 
 var fFecha1 = Date.UTC(aFecha1[2],aFecha1[1]-1,aFecha1[0]); 
 var fFecha2 = Date.UTC(aFecha2[2],aFecha2[1]-1,aFecha2[0]); 
 var dif = fFecha2 - fFecha1;
 var dias = Math.floor(dif / (1000 * 60 * 60 * 24)); 
 return dias;
 }

function CargarResumen(PkEmpresa){
	$("#pk_id_plan_empresa").val(PkEmpresa);
    $.ajax({
        type: 'GET',
        url: '/PlanTrabajoAnual/ObtenerActividadPorPlan',
        data: { pkPlanEmpresa: PkEmpresa },
        traditional: true,
        success: function (result) {
            var tHtml = "";
            $("#tblresumen").empty();
			var mesp = new Array();
			var arMes = new Array();
			tHtml += '<thead><tr class="titulos_tabla">';
			tHtml += '<th colspan="14" style="background-color: #7E8A97;text-align:center"><font color="white">RESUMEN</font></th>';
			tHtml += '</tr></thead>';
			tHtml += '<thead><tr class="titulos_tabla">';
			for (var i = 0; i <= 13; i++) {
				if(i==0)
					tHtml += '<td '+class_css_btxt+'>&nbsp</td>';
				else{
					if(i==13)
						i=14;
					
					tHtml += '<td '+class_css_btxt+'>'+GetMes(i)+'</td>';
				}
			}
			tHtml += '</tr></thead>';
			tHtml += '<tbody><tr><td '+class_css_btxt+'>Total Actividades Programadas</td>';
            
			$.each(result.planempresaactividad, function (key, val) {
				var fec = val.FechaProg.split("/");
                var mes = parseInt(fec[1]);
				var year = fec[2];
                var color = "";
				for (var i = 1; i <= 12; i++) {
					if(val.FechaProg!="" && val.FechaReProg=="" && val.FechaEje==""){
						if (i == mes) {
							arMes.push(mes);
						}
					}
                }
				
            });
			for (var i = 1; i <= 13; i++) {
				
				if(i==13)
					tHtml += '<td '+class_css_btxt+'><input type="text" id="totalprogramadas" style="border:none;width:25px"></td>';
				else
					tHtml += '<td '+class_css_btxt+'>'+getArMes(i,arMes)+'</td>';
				
			}
			tHtml += '</tr><tr><td '+class_css_btxt+'>Total Actividades Ejecutadas</td>';
			arMesE = new Array();
			$.each(result.planempresaactividad, function (key, val) {
				var fec = val.FechaEje.split("/");
                var mes = parseInt(fec[1]);
				var year = fec[2];
                var color = "";
				for (var i = 1; i <= 12; i++) {
					if(val.FechaEje!=""){
						if (i == mes) {
							arMesE.push(mes);
						}
					}
                }   
            });
			for (var i = 1; i <= 13; i++) {
				if(i==13)
					tHtml += '<td '+class_css_btxt+'><input type="text" id="totalejecutadas" style="border:none;width:25px"></td>';
				else
					tHtml += '<td '+class_css_btxt+'>'+getArMes(i,arMesE)+'</td>';
				
			}
			var prog =0;
			var ejec = 0;
			var progcalc = 0;
			tHtml += '</tr><tr><td '+class_css_btxt+'>Porcentaje de Ejecucion Mes</td>';
			for (var i = 1; i <= 13; i++) {
				tHtml += '<td '+class_css_btxt+'>'+getArMesPorcentaje(i,arMesE,arMes)+'</td>';
			}
			tHtml += '</tr><tbody>';
            $("#tblresumen").append(tHtml);	
			
			
			var totalprogramadas = 0;
			var totalejecutadas =  0;
			totalprogramadas = arMes.length;
			totalejecutadas = arMesE.length;
			$("#totalprogramadas").val(totalprogramadas);
			$("#totalejecutadas").val(totalejecutadas);
			$("#divporc").css("display","block");
			totalprogramadas = totalprogramadas+totalejecutadas;
			totalejecutadas = (totalejecutadas*100)/totalprogramadas;
			if(isNaN(totalejecutadas))
				totalejecutadas = 0;
			
			
			$("#promedios").val(totalejecutadas.toFixed(2)+'%');
			arMes = new Array();
			arMesE = new Array();
        }
    });
}

function getArMes(mes, arreglo){
	var cont = 0;
	for (var i = 0; i < arreglo.length; i++) {
		if(arreglo[i]==mes)
			cont++;
	}
	
	if(cont>0)
		return cont;
	else
		return "";
}

function getArMesPorcentaje(mes, arreglo,mesp){
	var cont = 0;var porc =0;var contp = 0;
	for (var i = 0; i < arreglo.length; i++) {
		if(arreglo[i]==mes)
			cont++;
	}
	
	for (var i = 0; i < mesp.length; i++) {
		if(mesp[i]==mes)
			contp++;
	}

	if(cont>0){
		if(contp>0){
			var ppmes = contp+cont;
			porc = (cont * 100)/ppmes;
		}
		else
		 porc = 100;
		
		return round(porc,2)+'%';
	}else
		return "";
}

function round(num, decimals) {
        var n = Math.pow(10, decimals);
        return Math.round( (n * num).toFixed(decimals) )  / n;
};

function EliminarPlanAnual(pk_plan_empresa){
	swal({
        title: "Estimado Usuario",
        text: "¿Está seguro que desea eliminar el Plan Anual seleccionado?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si",
        cancelButtonText: "No",
        closeOnConfirm: false
    },
	function () {
	    swal("Estimado Usuario", "El Plan Anual ha sido eliminado satisfactoriamente", "success");
	    $.ajax({
        type: 'GET',
        url: '/PlanTrabajoAnual/EliminarPlanAnual',
        data: { pk_plan_empresa : pk_plan_empresa },
		traditional: true,
        success: function (result) {
		  $("#IdSede").val("");
		  $("#planempresa").empty();
		  $("#planactividadxempresa").empty();
		  $("#tblresumen").empty();
		  $("#adjuntos").css("display","none");
		  $("#divporc").css("display","none");
        }
    });	
	});
}

function CargarActividad(pk_plan_empresa){
	$.ajax({
        type: 'GET',
        url: '/PlanTrabajoAnual/ObtenerPlanPorIdPlan',
        data: { pk_plan_empresa : pk_plan_empresa },
		traditional: true,
        success: function (result) {
			$('#myModal1').modal('show');
			ResetControl();
			$("#pk_id_plan_empresa").val(pk_plan_empresa);
			$("#FechaDesdeTemp").val(result.FechaDesde);
			$("#FechaHastaTemp").val(result.FechaHasta);
			$("#Estados").empty();
			$("#Estados").append("<option value=''>-Estado-</option>");
			$("#Estados").append("<option value='P'>Programada</option>");
		 
			
			
        }
    });	
}

function Change(node){
    
 var j=document.getElementsByClassName("on");
  for(var i=0;i<j.length;i++){
    j[i].className="";
    }  
  node.className="on";  
 
    }

function ResetControl(){
	$("#pk_id_plan_empresa").val("");
	$("#ObjetivosDescripcion").val("");
	$("#ObjetivosMetas").val("");
	$("#Actividad").val("");
	$("#Responsable").val("");
	$("#RecursosHumanos").val("");
	$("#RecursosTecnologico").val("");
	$("#RecursosFinanciero").val("");
	$("#FechaProg").val("");
	$("#HoraProgIni").val("");
	$("#HoraProgFin").val("");
}

function ValidarFechas(fechaini, fechafin){
	var fecini = new Date(fechaini);
	var fecfin = new Date(fechafin);
	if(fecfin <= fecini)
		return false;
	
	return true;
}

function ValidaGuardarActividades(){
	$("#frmActividades").validate({
        errorClass: "error",
        rules: {
			ObjetivosDescripcion:{
                required: true
            },
			ObjetivosMetas:{
                required: true
            },
			Actividad:{
                required: true
            }, 
			Responsable:{
                required: true
            },
			FechaProg:{
                required: true
            },
			HoraProgIni:{
                required: true
            },
			HoraProgFin:{
                required: true
            },
			Estado:{
                required: true
            },
			PorcentajeEjecucion:{
                required: true
            }
        },
        messages: {
            ObjetivosDescripcion:{
                required: "Este campo es requerido"
            },
			ObjetivosMetas:{
                required: "Este campo es requerido"
            },
			Actividad:{
                required: "Este campo es requerido"
            },
			Responsable:{
                required: "Este campo es requerido"
            },
			FechaProg:{
                required: "Este campo es requerido"
            },
			HoraProgIni:{
                required: "Este campo es requerido"
            },
			HoraProgFin:{
                required: "Este campo es requerido"
            },
			Estado:{
                required: "Este campo es requerido"
            },
			PorcentajeEjecucion:{
                required: "Este campo es requerido"
            }
        }
    });
	
}

function ValidarFechaActividad(fechaprog, fechaini, fechafin){
	fechaprog = fechaprog.toString();
	fechaini = fechaini.toString();
	fechafin = fechafin.toString();

	var fecini = fechaini.split('/');
	var fecfin = fechafin.split('/');
	var fecpro = fechaprog.split('/');
	
	var fecini1 = fecini[2]+"-"+fecini[1]+"-"+fecini[1];
	var fecfin1 = fecfin[2]+"-"+fecfin[1]+"-"+fecfin[1];
	var fecpro1 = fecpro[2]+"-"+fecpro[1]+"-"+fecpro[1];
	
	if(fecpro1 < fecini1 || fecpro1 > fecfin1)
		return false;
		
		
	return true;
}

function GuardarActividad() {
   ValidaGuardarActividades();
	if ($("#frmActividades").valid()) {
		if(ValidarFechaActividad($("#FechaProg").val(),$("#FechaDesdeTemp").val(),$("#FechaHastaTemp").val()))
		{
			var start = timeToInt($("#HoraProgIni").val());
			var end = timeToInt($("#HoraProgFin").val());
			
			if (start > end) {
			  swal("Estimado Usuario", 'La Hora Inicial debe ser inferior a la hora final','warning');
			  return 
			}
			
			$('#myModal1').modal('hide');
			PopupPosition();
			var FormArray = new Array();
			$.ajax({
				type: 'GET',
				url: '/PlanTrabajoAnual/GuardarActividades',
				data:  $("#frmActividades").serialize() ,
				traditional: true,
				success: function (result) {
					OcultarPopupposition();
					ObtenerActividadporSede(result);
				}
			});	
			
		}
		else
			swal("Estimado Usuario", 'La Fecha Programada deben estar dentro del rango de la vigencia','warning');

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

function ObtenerActividadporSede(pkplaEmpresa) {
	$("#pk_id_plan_empresa").val(pkplaEmpresa);
    $.ajax({
        type: 'GET',
        url: '/PlanTrabajoAnual/ObtenerActividadPorPlan',
        data: { pkPlanEmpresa: pkplaEmpresa },
        traditional: true,
        success: function (result) {
            var tHtml = "";
            $("#planactividadxempresa").empty();
			var cont = 0;
			tHtml += '<thead><tr class="titulos_tabla">';
			tHtml += '<th colspan="13" style="background-color: #7E8A97;text-align:center"><font color="white">CRONOGRAMA</font></th>';
			tHtml += '</tr></thead>';
            var arYear = new Array();
			$.each(result.planempresaactividad, function (key, val) {
				var fec = val.FechaProg.split("/");
				arYear.push(fec[2]);
			});
			
			arYear = arYear.unique();
			
			for (var x = 0; x <= arYear.length; x++) {
				if(arYear[x]!=undefined)
				{
					var yeartemp = arYear[x];
					tHtml += '<thead><tr class="titulos_tabla">';
					for (var i = 1; i <= 13; i++) {
						if(i==13)
							arYear[x]="";
						
						tHtml += '<td '+class_css_btxt+'>'+GetMes(i)+' '+arYear[x]+'</td>';
					
					}
					tHtml += '</tr></thead>';
					$.each(result.planempresaactividad, function (key, val) {
						var fec = val.FechaProg.split("/");
						var mes = parseInt(fec[1]);
						var year = fec[2];
						if(year==yeartemp){
							tHtml += '<tbody><tr>';
							var color = "";
								for (var i = 1; i <= 12; i++) {
									if (i == mes) {
										if (val.Estado == 'E')
											color = "#009900";

										if (val.Estado == 'R')
											color = "#FF9900";

										if (val.Estado == 'P')
											color = "#0066CC";

										tHtml += '<td '+class_css_btxt+' class="CellWithComment" style="cursor:pointer;" onclick="ObtenerActividad(' + val.pk_id_actividad + ')"><span class="CellComment">Fecha Reprogramada : '+val.FechaReProg+'<br>Fecha Ejecutada : '+val.FechaEje+'</span>';
										tHtml += '<div style="background-color:' + color + ';"><font color="#FFFFFF">' + val.FechaProg + ' ' + val.HoraProgIni + ' - ' + val.HoraProgFin + '</font></div>';
										tHtml += '</td>';
									}

									if (i == 12) {
										tHtml += '<td '+class_css_btxt+'><a href="#" class="btn btn-search btn-md" onclick="EliminarActividades(' + val.pk_id_actividad + ');return false;" title="Eliminar"><span class="glyphicon glyphicon-erase"></span></a></td>';
									}
									else
										tHtml += '<td '+class_css_btxt+'>&nbsp;</td>';
								}
						}//if(year==arYear[x])
					
					});
				}
			}	
			tHtml += '</tr></tbody>';
            $("#planactividadxempresa").append(tHtml);
			$("#adjuntos").css("display","block");
        }
    });
	CargarResumen(pkplaEmpresa);
	$("#btnGuardar").css('display','block');
}



//////////////////////////////////////////////////////////////////////////////////////////////////
function GetMes(mes){
	switch(mes)
	{
		case 1:return "Enero";
		break;
		case 2:return "Febrero";
		break;
		case 3:return "Marzo";
		break;
		case 4:return "Abril";
		break;
		case 5:return "Mayo";
		break;
		case 6:return "Junio";
		break;
		case 7:return "Julio";
		break;
		case 8:return "Agosto";
		break;
		case 9:return "Septiembre";
		break;
		case 10:return "Octubre";
		break;
		case 11:return "Noviembre";
		break;
		case 12:return "Diciembre";
		break;
		case 13:return "Acciones";
		break;
		case 14:return "Total";
		break;
		
	}
}

function EliminarActividades(pk_id_actividad) {
	swal({
        title: "Estimado Usuario",
        text: "¿Está seguro que desea eliminar la Actividad seleccionada?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si",
        cancelButtonText: "No",
        closeOnConfirm: false
    },
	function () {
	    swal("Estimado Usuario", "La Actividad fue eliminada satisfactoriamente", "success");
	    $.ajax({
        type: 'GET',
        url: '/PlanTrabajoAnual/EliminarActividad',
        data: { pk_id_actividad : pk_id_actividad },
		traditional: true,
        success: function (result) {
			ObtenerActividadporSede(result);
			CargarResumen(result);
        }
    });
	});
	
}

function ObtenerActividad(pkActividad) {
    $.ajax({
        type: 'GET',
        url: '/PlanTrabajoAnual/ObtenerActividad',
        data: { pk_id_actividad: pkActividad },
        traditional: true,
        success: function (result) {
        $('#myModal1').modal('show');
            $("#pk_id_plan_empresa").val(result.pk_id_plan_empresa);
			$("#pk_id_actividad").val(result.pk_id_actividad);
			$("#FechaDesdeTemp").val(result.FechaDesdeTemp);
			$("#FechaHastaTemp").val(result.FechaHastaTemp);
            $("#ObjetivosDescripcion").val(result.ObjetivosDescripcion);
            $("#ObjetivosMetas").val(result.ObjetivosDescripcion);
            $("#Actividad").val(result.Actividad);
            $("#Responsable").val(result.Responsable);
            $("#RecursosHumanos").val(result.RecursosHumanos);
            $("#RecursosTecnologico").val(result.RecursosTecnologico);
            $("#RecursosFinanciero").val(result.RecursosTecnologico);
            $("#FechaProg").val(result.FechaProg);
            $("#HoraProgIni").val(result.HoraProgIni);
            $("#HoraProgFin").val(result.HoraProgFin);
            
            $("#Estados").empty();
			$("#Estados").append("<option value=''>-Estado-</option>");
			$("#Estados").append("<option value='P'>Programada</option>");
			$("#Estados").append("<option value='R'>Reprogramada</option>");
			$("#Estados").append("<option value='E'>Ejecutada</option>");
			$("#Estados").val(result.Estados);
        }
    });
}

$("#cargar1").click(function() {
	$.ajax({
		type: 'GET',
		url: '/PlanTrabajoAnual/ObtenerImagen',
		data: { pk_id_plan_empresa : $("#pk_id_plan_empresa").val(), tipo : "legal"},
		traditional: true,
		success: function (Thumbnails) {
			if(Thumbnails!=""){
				$('#myModal2').modal('show');
				$('#ImgCarga').attr("src", Thumbnails);
			}else
				swal("Estimado Usuario", "Debe adjuntar una firma primero", "warning");
		
		}
	});
});
   
$("#cargar2").click(function() {
	$.ajax({
		type: 'GET',
		url: '/PlanTrabajoAnual/ObtenerImagen',
		data: { pk_id_plan_empresa : $("#pk_id_plan_empresa").val(), tipo : "sst" },
		traditional: true,
		success: function (Thumbnails) {
			if(Thumbnails!=""){
				$('#myModal3').modal('show');
				$('#ImgCarga1').attr("src", Thumbnails);
			}else
				swal("Estimado Usuario", "Debe adjuntar una firma primero", "warning");
			
			
		}
	});
});
