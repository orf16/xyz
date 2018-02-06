// jqXHRData needed for grabbing by data object of fileupload scope
var jqXHRData;

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
	ConstruirDatePickerPorElemento('FechaInicio');
	ConstruirDatePickerPorElemento('FechaFin');
	
});

var class_css_header = 'style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase"';
var class_css = 'style="border-right: 2px solid lightslategray; vertical-align:middle"';
var class_css_btxt = 'style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"';

function CargarPlanAnual(param){
	$.ajax({
		type: 'GET',
		url: '/PlanTrabajoAnual/ObternerPlanAnualporSede',
		data: { IDSede : param },
		success: function (result) {
			var tHtml = "";
            $("#planempresa").empty();
			tHtml += '<thead style="border-left:2px solid lightslategray;"><tr class="titulos_tabla">';
			tHtml += '<th width="117" rowspan="2" '+class_css_header+'>Sede</th>';
			tHtml += '<th colspan="2" '+class_css_header+'>Periodo</th>';
			tHtml += '<th width="122" '+class_css_header+'>&nbsp;</th>';
			tHtml += '</tr>';
			tHtml += '<tr class="titulos_tabla">';
			tHtml += '<th width="122" '+class_css_header+'>Fecha Desde</th>';
			tHtml += '<th width="115" '+class_css_header+'>Fecha Hasta</th>';
			tHtml += '<th '+class_css_header+'>Vigencia</th>';
			tHtml += '</tr></thead>';
			var cont = 0;
			 $.each(result, function (key, val) {
				 tHtml += '<tbody style="border-left:2px solid lightslategray;"><tr id="row_'+val.PkEmpresa+'" style="cursor:pointer;" onclick="Change(this);ObtenerCalendarioXFecha(this.id);">';
				 tHtml += '<th '+class_css_btxt+'><b>'+val.Sede+'</b></th>';
				 tHtml += '<th '+class_css_btxt+'>'+val.FechaDesde+'</th>';
				 tHtml += '<th '+class_css_btxt+'>'+val.FechaHasta+'</th>';
				 tHtml += '<th '+class_css_btxt+'>'+val.Vigencia+'</th>';
				 tHtml += '</tr></tbody>';
				 cont++;
			 });
			 $("#planempresa").append(tHtml);
		}
	});
}

function Change(node)
{
   var j=document.getElementsByClassName("on");
   for(var i=0;i<j.length;i++){
     j[i].className="";
   }  
	
	node.className="on";  
 }
	
function ObtenerCalendarioXFecha(param)
{
	$('#calendar_plantrabajo').fullCalendar('destroy');
	$('#calendar_plantrabajo').fullCalendar('render');
	var plan_empresa = param.split("row_");
	var pk_id_plan_empresa = plan_empresa[1];
	$.ajax({
		type: 'GET',
        dataType: "json",
        contentType: "application/json",
        data: { pk_id_plan_empresa : pk_id_plan_empresa, FechaInicio: document.getElementById(param).cells[1].innerHTML, FechaFin: document.getElementById(param).cells[2].innerHTML },
        url: "/PlanTrabajoAnual/ObtenerAgendaXFecha",
        success: function (data) {
            $('#calendar_plantrabajo').fullCalendar({
                theme: false,
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                defaultView: 'month',
                editable: false,
                locale: 'es',
                eventLimit: 1,
                allDaySlot: false,
                timeFormat: 'h:mm:ss A',
				eventTextColor : '#000000',
				eventColor: '#F5D583',
				//defaultDate: moment(dateformat(document.getElementById(param).cells[1].innerHTML)),
                events : $.map(data, function (item, i) {//This is where you need to take care				
                    var event = new Object();
                    event.start = moment(item.DateFrom +' '+ item.HoraInicio);
                    event.end = moment(item.DateTo +' '+ item.HoraFin);
                    event.title = item.name;
                    event.brief = item.name;
                    event.place = item.name;
                    event.id = item.id;
					return event;
                }),
            });

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $("#cal_error").text(errorThrown); //Handle Error
        }
    });
	

}

function dateformat(val){
	var fec = val.split('/');
	return fec[2]+"-"+fec[0]+"-"+fec[1];
}