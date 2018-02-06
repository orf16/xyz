$(document).ready(function () {
    $("#add").click(function () {
        var intId = $("#buildyourform div").length + 1;
        var fieldWrapper = $("<div class=\"fieldwrapper\" id=\"field" + intId + "\"/>");
        var fName = $("<input type=\"text\" class=\"fieldname\" />");
        var fType = $("<select class=\"fieldtype\"><option value=\"label\">Etiqueta Titulo</option><option value=\"radio\">Radio</option><option value=\"checkbox\">Check</option><option value=\"textbox\">Campo Texto</option><option value=\"textarea\">Area Texto</option></select>");
        var removeButton = $("<input type=\"button\" class=\"remove\" value=\"-\" />");
        removeButton.click(function () {
            $(this).parent().remove();
        });
        fieldWrapper.append(fName);
        fieldWrapper.append(fType);
        fieldWrapper.append(removeButton);
        $("#buildyourform").append(fieldWrapper);
    });
    $("#preview").click(function () {
        $("#yourform").remove();
        var fieldSet = $("<fieldset id=\"yourform\"><legend>"+$("#Titulo").val()+"</legend></fieldset>");
		var Html = "<table border=\"0\">";
		$("#buildyourform div").each(function () {
            Html +="<tr class=\"noborder\">";
			var id = "input" + $(this).attr("id").replace("field", "");
            Html +="<td class=\"noborder\"><label for=\"" + id + "\">" + $(this).find("input.fieldname").first().val() + "</label></td>";
            var input;
            switch ($(this).find("select.fieldtype").first().val()) {
                case "label":
                    Html +="<td class=\"noborder\"><label id=\"" + id + "\" name=\"" + id + "\"></label></td>";
                    break;
				case "radio":
                    Html +="<td class=\"noborder\"><input type=\"radio\" id=\"radio_op\" name=\"radio_op\" /></td>";
                    break;
                case "checkbox":
                    Html +="<td class=\"noborder\"><input type=\"checkbox\" id=\"" + id + "\" name=\"" + id + "\" /></td>";
                    break;
                case "textbox":
                    Html +="<td class=\"noborder\"><input type=\"text\" id=\"" + id + "\" name=\"" + id + "\" class=\"form-control\"/></td>";;
                    break;
                case "textarea":
                    Html +="<td class=\"noborder\"><textarea id=\"" + id + "\" name=\"" + id + "\" class=\"form-control\"></textarea></td>";
                    break;
            }
			Html += "</tr>";
            //fieldSet.append(label);
            //fieldSet.append(input);
        });
		
		//if($("#CuerpoHtmlTemp").val()!=null)
			fieldSet.append(Html);
		
		
		Html += "</table>";
        $("#myform").append(fieldSet);
});
});

var class_css_header = 'style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase"';
var class_css = 'style="border-right: 2px solid lightslategray; vertical-align:middle"';
var class_css_btxt = 'style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"';	
	
function CrearEncuesta(){
	 $('#myModal1').modal('show');
	 $("#PK_Id_Encuesta").val(0);
	 $("#Titulo").val("");
	 $("#link_url").css("display","none");
	 $("#enviar").css("display", "none");
	 $("#myform").empty();
	 $("#buildyourform").empty();
	 
	 
}

function GenerarLink()
{
	$.ajax({
		type: 'GET',
		url: '/ComunicacionesInternas/GenerarLink',
		data: { PK_Id_Encuesta : $("#PK_Id_Encuesta").val() },
		traditional: true,
		success: function (result) {
			$("#URL").val(result);
		}
	});
}

function CerrarModal(){
	 $('#myModal1').modal('hide');
}

function ReCrear(){
	$("#myform").empty();
}	

function GuardarEncuesta(){
	 PopupPosition();
	 var cuerpoencuesta =  $("#myform").html();
	 $.ajax({
        type: 'GET',
        url: '/ComunicacionesInternas/GuardarEncuesta',
        data: { PK_Id_Encuesta : $("#PK_Id_Encuesta").val(), Titulo : $("#Titulo").val(), CuerpoEncuesta : cuerpoencuesta, EstadoEncuesta : "En Espera" },
        success: function (result) {
			$('#myModal1').modal('hide');
			OcultarPopupposition();
			ListarComunicacionesInternas();
        }
    });
}

function GuardarEncuestas(){
	PopupPosition();
	 $.ajax({
		type: 'POST',
		url: '/ComunicacionesInternas/GuardarEncuesta',
		data: { formulario : $("#frmencuesta").serialize()},
		traditional: true,
		success: function (result) {
			OcultarPopupposition();
			swal("Estimado Usuario!", "La encuesta ha sido Guardada con éxito.", "success");
		}
	});
}

ListarComunicacionesInternas();
function ListarComunicacionesInternas(){
	$.ajax({
        type: 'POST',
        url: '/ComunicacionesInternas/ListarComunicacionesInternas',
        success: function (result) {
			 var tHtml = "";
            $("#gridcomunicadosapp").empty();
			tHtml += '<thead style="border-left:2px solid lightslategray;"><tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+'>Nombre de la Encuesta</td>';
			tHtml += '<th '+class_css_header+'>Estado de la Encuesta</th>';
			tHtml += '<th '+class_css_header+'>Fecha de la Creación</th>';
			tHtml += '<th '+class_css_header+'>Fecha de Envío</th>';
			tHtml += '<th '+class_css_header+'>Acciones</th>';
			tHtml += '</tr></thead>';
			$.each(result, function (key, val) {
                tHtml += '<tbody style="border-left:2px solid lightslategray;"><tr>';
                tHtml += '<td '+class_css_btxt+'>'+val.Titulo+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.EstadoEncuesta+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.FechaCreacion.substring(0, 10)+'</td>';
				tHtml += '<td '+class_css_btxt+'>'+val.FechaEnvio.substring(0, 10)+'</td>';
				tHtml += '<td '+class_css_btxt+'>&nbsp;<a href="#" class="btn btn-search btn-md" title="Editar" onClick="EditarEncuesta('+val.PK_Id_Encuesta+');"><span class="glyphicon glyphicon-pencil"></span></a>&nbsp;|&nbsp;';
				tHtml += '<a href="#" class="btn btn-search btn-md" title="Visualizar" onClick="PublicarEncuesta('+val.PK_Id_Encuesta+');"><span class="glyphicon glyphicon-search"></span></a>&nbsp;|&nbsp;';
				tHtml += '<a href="#" class="btn btn-search btn-md" title="Eliminar" onClick="EliminarEncuesta('+val.PK_Id_Encuesta+');"><span class="glyphicon glyphicon-erase"></span></a></td>';
				tHtml += '</tr></tbody>';
            });
            $("#gridcomunicadosapp").append(tHtml);
        }
    });
}


function PublicarEncuesta(PK_Id_Encuesta){
	$.ajax({
        type: 'GET',
        url: '/Publicacion/GenerarEncuesta',
		data: { PK_Id_Encuesta : PK_Id_Encuesta },
        success: function (result) {
			//alert(JSON.stringify(result));
			window.open(result,"popup", 'width=auto,height=auto');
		}
	});	
}


function EditarEncuesta(PK_Id_Encuesta){
	$("#PK_Id_Encuesta").val(PK_Id_Encuesta);
	$.ajax({
        type: 'GET',
        url: '/ComunicacionesInternas/EditarEncuesta',
		data : { PK_Id_Encuesta : PK_Id_Encuesta },
        success: function (result) {
			$('#myModal1').modal('show');
			$("#link_url").css("display","block");
			$("#enviar").css("display", "block");
			$("#yourform").remove();
			$("#myform").html("");
			$("#myform").append(result.CuerpoHTML);
			$("#CuerpoHtmlTemp").val(result.CuerpoHTML);
			$("#Titulo").val(result.Titulo);
			$("#URL").val(result.URL);
			$("#PK_Id_Encuesta").val(PK_Id_Encuesta);
        }
    });
}

function EliminarEncuesta(PK_Id_Encuesta){
	swal({
	  title: "Estimado Usuario",
	  text: "Desea Eliminar la Encuesta?",
	  type: "warning",
	  showCancelButton: true,
	  confirmButtonColor: "#DD6B55",
	  confirmButtonText: "Si",
	  cancelButtonText: "No",
	  closeOnConfirm: false
	},
	function(){
	  swal("Estimado Usuario", "La encuesta ha sido eliminada con éxito.", "success");
	  $.ajax({
				type: 'GET',
				url: '/ComunicacionesInternas/EliminarEncuesta',
				data : { PK_Id_Encuesta : PK_Id_Encuesta },
				success: function (result) {
					ListarComunicacionesInternas();
				}
			});
	});
}

function EnviarEncuesta(PK_Id_Encuesta){
	
		PopupPosition();
		 var cuerpoencuesta =  $("#myform").html();
		 $.ajax({
			type: 'GET',
			url: '/ComunicacionesInternas/GuardarEncuesta',
			data: { PK_Id_Encuesta : $("#PK_Id_Encuesta").val(), Titulo : $("#Titulo").val(), CuerpoEncuesta : cuerpoencuesta, EstadoEncuesta : "Enviado" },
			success: function (result) {
				$('#myModal1').modal('hide');
				OcultarPopupposition();
				ListarComunicacionesInternas();
			}
		});
}











