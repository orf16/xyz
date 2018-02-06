$(function () {
	ConstruirDatePickerPorElemento('FechaInicio');
	ConstruirDatePickerPorElemento('FechaFin');	
});

//ObtenerCalendarioXFecha();
function ObtenerCalendarioXFecha()
{
	$('#calendar_plantrabajo').fullCalendar('destroy');
	$('#calendar_plantrabajo').fullCalendar('render');
	$.ajax({
		type: 'GET',
        dataType: "json",
        contentType: "application/json",
        data: { FechaInicio: $("#FechaInicio").val(), FechaFin: $("#FechaFin").val() },
        url: "/PlanCapacitacion/ObtenerAgendaXFecha",
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
				//defaultDate: moment(dateformat($("#FechaInicio").val())),
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