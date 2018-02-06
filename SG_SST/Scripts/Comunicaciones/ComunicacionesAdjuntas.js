$(function () {
    ConstruirDatePickerPorElemento('fecha');
});

// jqXHRData needed for grabbing by data object of fileupload scope
var jqXHRData;
$(document).ready(function () {
	initSimpleFileUpload();
	 $("#hl-start-upload").on('click', function () {
		if (jqXHRData) {
			jqXHRData.submit();
		}
		return false;
	});  
});

function initSimpleFileUpload() {
    $('#upload').fileupload({
        url: '/ComunicacionesExternas/SubirArchivo',
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
			
			
			$("#adjunto_temp").val(data.result.message);
			swal("Estimado Usuario", 'El archivo ha sido adjuntado Exitosamente.','success');
			
			//FormArray.push($("#RepresentanteSGSST").val());
        },
        fail: function (event, data) {
			swal("Estimado Usuario", 'Debe cargar un archivo con peso menor a 10 MB.','warning');
        }
    });
}

var class_css_header = 'style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase"';
var class_css = 'style="border-right: 2px solid lightslategray; vertical-align:middle"';
var class_css_btxt = 'style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"';

ListarRecibidas();
function ListarRecibidas() {
    $.ajax({
        type: 'GET',
        url: '/ComunicacionesExternas/ListarRecibidas',
        success: function (result) {
            var tHtml = "";
            $("#gridrecibidas").empty();
            tHtml += '<thead style="border-left:2px solid lightslategray;"><tr class="titulos_tabla">';
            tHtml += '<th '+class_css_header+'>Nombre del Comunicado</td>';
            tHtml += '<th '+class_css_header+'>Entidad</th>';
            tHtml += '<th '+class_css_header+'>Descripción</th>';
            tHtml += '<th '+class_css_header+'>Fecha</th>';
            tHtml += '<th '+class_css_header+'>Acciones</th>';
            tHtml += '</tr></thead>';
            $.each(result, function (key, val) {
                tHtml += '<tbody style="border-left:2px solid lightslategray;"><tr>';
                tHtml += '<td '+class_css_btxt+'>' + val.nombre + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.entidad + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.descripcion + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.fecha.substring(0, 10) + '</td>';
                tHtml += '<td '+class_css_btxt+'>&nbsp;<a href="#" class="btn btn-search btn-md" title="Editar" onClick="EditarComunicado(' + val.pk_id_comadjunto + ');"><span class="glyphicon glyphicon-pencil"></span></a>&nbsp|&nbsp';
				if(val.adjunto!=null){
					tHtml += '<a href="#" class="btn btn-search btn-md" title="Descargar" onClick="DescargarArchivo(' + val.pk_id_comadjunto + ');"><span class="glyphicon glyphicon-download-alt"></span></a>&nbsp|&nbsp';
				}
				
                tHtml += '<a href="#" class="btn btn-search btn-md" title="Eliminar" onClick="EliminarComunicado(' + val.pk_id_comadjunto + ');"><span class="glyphicon glyphicon-erase"></span></a></td>';
                tHtml += '</tr></tbody>';
            });
            $("#gridrecibidas").append(tHtml);
        }
    });
}

ListarEnviadas();
function ListarEnviadas() {
    $.ajax({
        type: 'GET',
        url: '/ComunicacionesExternas/ListarEnviadas',
        success: function (result) {
            var tHtml = "";
            $("#gridenviadas").empty();
            tHtml += '<thead style="border-left:2px solid lightslategray;"><tr class="titulos_tabla">';
            tHtml += '<th '+class_css_header+'>Nombre del Comunicado</td>';
            tHtml += '<th '+class_css_header+'>Entidad</th>';
            tHtml += '<th '+class_css_header+'>Descripción</th>';
            tHtml += '<th '+class_css_header+'>Fecha</th>';
            tHtml += '<th '+class_css_header+'>Acciones</th>';
            tHtml += '</tr></thead>';
            $.each(result, function (key, val) {
                 tHtml += '<tbody style="border-left:2px solid lightslategray;"><tr>';
                tHtml += '<td '+class_css_btxt+'>' + val.nombre + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.entidad + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.descripcion + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.fecha.substring(0, 10) + '</td>';
                tHtml += '<td '+class_css_btxt+'>&nbsp;<a href="#" class="btn btn-search btn-md" title="Editar" onClick="EditarComunicado(' + val.pk_id_comadjunto + ');"><span class="glyphicon glyphicon-pencil"></span></a>&nbsp|&nbsp';
				if(val.adjunto!=null){
					tHtml += '<a href="#" class="btn btn-search btn-md" title="Descargar" onClick="DescargarArchivo(' + val.pk_id_comadjunto + ');"><span class="glyphicon glyphicon-download-alt"></span></a>&nbsp|&nbsp';
				}
				
                tHtml += '<a href="#" class="btn btn-search btn-md" title="Eliminar" onClick="EliminarComunicado(' + val.pk_id_comadjunto + ');"><span class="glyphicon glyphicon-erase"></span></a></td>';
                tHtml += '</tr></tbody>';
            });
            $("#gridenviadas").append(tHtml);
        }
    });
}

function CrearExterna()
{
	$('#myModal1').modal('show');
	$("#pk_id_comadjunto").val("");
	$('#nombre').val("");
	$('#entidad').val("");
	$('#descripcion').val("");
	$('#fecha').val("");
	$('#adjunto').val("");
	$('#respuesta').val("");
}

function Guardar()
{
	ValidaGuardarFormulario();
	if ($("#frmcomunicacionesadjuntos").valid()) {
		PopupPosition();
		$.ajax({
			type: 'POST',
			url: '/ComunicacionesExternas/GuardarComunicadosAdjuntos',
			data: $("#frmcomunicacionesadjuntos").serialize(),
			traditional: true,
			success: function (result) {
				GuardarArchivo(result);
			}
		});
	}
	

}

function GuardarArchivo(pk_id_comadjunto)
{
	$.ajax({
		type: 'GET',
		url: '/ComunicacionesExternas/ActualizarAdjuntos',
		data: { pk_id_comadjunto : pk_id_comadjunto, adjunto : $("#adjunto_temp").val() },
		traditional: true,
		success: function (result) {
			$('#myModal1').modal('hide');
			OcultarPopupposition();
			$("#pk_id_comadjunto").val(0);
			ListarEnviadas();
			ListarRecibidas();
			
		}
	});
}

function EliminarComunicado(pk_id_comadjunto) {
    swal({
        title: "Estimado Usuario",
        text: "Desea Eliminar el Comunicado?",
        type: "warning",
        showCancelButton: true,
		confirmButtonColor: "#DD6B55",
		confirmButtonText: "Si",
		cancelButtonText: "No",
		closeOnConfirm: false
    },
	function () {
	    swal("Estimado Usuario", "El Comunicado a sido eliminado con exito.", "success");
	    $.ajax({
	        type: 'GET',
	        url: '/ComunicacionesExternas/EliminarComunicadoAdjunto',
	        data: { pk_id_comadjunto: pk_id_comadjunto },
	        success: function (result) {
	            ListarEnviadas();
				ListarRecibidas();
	        }
	    });
	});
}

function EditarComunicado(pk_id_comadjunto)
{
	$.ajax({
		type: 'GET',
		url: '/ComunicacionesExternas/EditarComunicadoAdjunto',
		data:{ pk_id_comadjunto : pk_id_comadjunto },
		success: function (result) {
			$('#myModal1').modal('show');
			//$("#attach").css("display", "block");
			$("#pk_id_comadjunto").val(result.pk_id_comadjunto);
			$('#nombre').val(result.nombre);
			$('#entidad').val(result.entidad);
			$('#descripcion').val(result.descripcion);
			$('#fecha').val(result.fecha);
			$('#adjunto').val(result.adjunto);
			$('#respuesta').val(result.respuesta);
			$('#requiere').val(result.requiere);
			$('#tipo').val(result.tipo);
		}
	});
}


function ValidaGuardarFormulario(){
	$("#frmcomunicacionesadjuntos").validate({
        errorClass: "error",
        rules: {
			nombre :{ required: true },
			entidad :{ required: true },
			descripcion :{ required: true },
			fecha :{ required: true },
			respuesta :{ required: true },
			requiere :{ required: true },
			tipo :{ required: true },
			adjunto :{ required: false } 
        },
        messages: {
			nombre :{ required: "*Este campo es requerido." },
			entidad :{ required: "*Este campo es requerido." },
			descripcion :{ required: "*Este campo es requerido." },
			fecha :{ required: "*Este campo es requerido." },
			respuesta :{ required: "*Este campo es requerido." },
			requiere :{ required: "*Este campo es requerido." },
			tipo :{ required: "*Este campo es requerido." },
			adjunto : { required: "*Este campo es requerido." }
        }
    });
}

function CerrarModal()
{
	$('#myModal1').modal('hide');
}

function DescargarArchivo(pk_id_comadjunto)
{
	$.ajax({
		type: 'POST',
		url: '/ComunicacionesExternas/DescargarArchivo',
		data:{ pk_id_comadjunto : pk_id_comadjunto },
		success: function (result) {
			  window.location = '/ComunicacionesExternas/Download?file=' + result;
			  ListarEnviadas();
			  ListarRecibidas();
			  
		}
	});
	
}