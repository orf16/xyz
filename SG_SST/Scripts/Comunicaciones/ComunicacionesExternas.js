var arCargos = new Array();
var arCargosTemp = new Array();
var arPersonas = new Array();
var arPersonasTemp = new Array();

var class_css_header = 'style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase"';
var class_css = 'style="border-right: 2px solid lightslategray; vertical-align:middle"';
var class_css_btxt = 'style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"';

tinyMCE.init({
	// General options
	mode: "textareas",
	theme: "modern",
	// Theme options,
	language: "es",
	theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect",
	theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
	theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
	theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,spellchecker,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,blockquote,pagebreak,|,insertfile,insertimage",
	theme_advanced_toolbar_location: "top",
	theme_advanced_toolbar_align: "left",
	theme_advanced_statusbar_location: "bottom",
	theme_advanced_resizing: true,
	plugins: 'link',
	branding: false,
	// Example content CSS (should be your site CSS)
	//content_css: "css/example.css",
});

function CrearComunicado() {
	ResetControls();
    $('#myModal1').modal('show');
    $("#PK_Id_Comunicado").val(0)
	$("#guardarc").css("display", "block");
	$("#enviar").css("display", "none");
	document.getElementById("Destinatarios").innerHTML = "";
}

function CerrarModal() {
    $('#myModal1').modal('hide');
}

function GuardarComunicado(origen) {
	ValidaGuardarFormulario();
	if ($("#frmcomunicacionesexternas").valid()) {
		PopupPosition();
		var arreglo = new Array();
		var x = document.getElementById("Destinatarios");
		for (var i = 0; i < x.length; i++) {
			var opt = x[i];
			arreglo += opt.text+',';
		}
		$.ajax({
			type: 'GET',
			url: '/ComunicacionesExternas/GuardarComunicado',
			data: { PK_Id_Comunicado: $("#PK_Id_Comunicado").val(), Titulo: $("#Titulo").val(), Asunto: $("#Asunto").val(), CuerpoMensaje: tinyMCE.get('CuerpoMensaje').getContent(), arreglo : arreglo, Origen : origen },
			success: function (result) {
				//alert(JSON.stringify(result));
				if(result.origen=='G')
				{
					OcultarPopupposition();
					Reload(result.PK_Id_Comunicado_temp);
				}
				
				if(result.origen=='E')
				{
					swal("Estimado Usuario", "El Comunicado ha sido enviado Éxito.", "success");
					$('#myModal1').modal('hide');
					OcultarPopupposition();
					ListarComunicacionesExternas();
				}
			}
		});
	}
}

function Reload(PK_Id_Comunicado)
{
	$("#PK_Id_Comunicado").val(PK_Id_Comunicado)
	swal({
        title: "Estimado Usuario",
        text: "El Comunicado ha sido Guardado, Desea Enviar el Comunicado ahora?",
        type: "warning",
		showCancelButton: true,
		confirmButtonColor: "#DD6B55",
		confirmButtonText: "Si",
		cancelButtonText: "No",
		closeOnConfirm: false
    },
	function (isConfirm) {
	   if(isConfirm){
		   GuardarComunicado('E');
		   ListarComunicacionesExternas();
		   swal("Estimado Usuario", "El Comunicado ha sido enviado con Éxito.", "success");
	   }
	   else
	   {
		   OcultarPopupposition();
		   //$('#myModal1').modal('hide');
		   ListarComunicacionesExternas();
	   }	
	});
}

ListarComunicacionesExternas();
function ListarComunicacionesExternas() {
    $.ajax({
        type: 'POST',
        url: '/ComunicacionesExternas/ListarComunicacionesExternas',
        success: function (result) {
            var tHtml = "";
            $("#gridcomunicadosapp").empty();
            tHtml += '<thead style="border-left:2px solid lightslategray;"><tr class="titulos_tabla">';
            tHtml += '<th '+class_css_header+'>Nombre del Comunicado</td>';
            tHtml += '<th '+class_css_header+'>Estado Comunicado</th>';
            tHtml += '<th '+class_css_header+'>Fecha de la Creación</th>';
            tHtml += '<th '+class_css_header+'>Fecha de Envío</th>';
            tHtml += '<th '+class_css_header+'>Acciones</th>';
            tHtml += '</tr></thead>';
            $.each(result, function (key, val) {
                tHtml += '<tbody style="border-left:2px solid lightslategray;"><tr class="titulos_filas">';
                tHtml += '<td '+class_css_btxt+'>' + val.Titulo + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.EstadoComunicado + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.FechaCreacion.substring(0, 10) + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.FechaEnvio.substring(0, 10) + '</td>';
                tHtml += '<td '+class_css_btxt+'>&nbsp;<a href="#" class="btn btn-search btn-md" title="Editar" onClick="EditarComunicado(' + val.PK_Id_Comunicado + ');"><span class="glyphicon glyphicon-pencil"></span></a>&nbsp;';
                tHtml += '<a href="#" class="btn btn-search btn-md" title="Eliminar" onClick="EliminarComunicado(' + val.PK_Id_Comunicado + ');"><span class="glyphicon glyphicon-erase"></span></a></td>';
                tHtml += '</tr><tbody>';
            });
            $("#gridcomunicadosapp").append(tHtml);
        }
    });

}

function EditarComunicado(PK_Id_Comunicado) {
	PopupPosition();
    $("#PK_Id_Comunicado").val(PK_Id_Comunicado);
    $.ajax({
        type: 'GET',
        url: '/ComunicacionesExternas/EditarComunicado',
        data: { PK_Id_Comunicado: PK_Id_Comunicado },
        success: function (result) {
            $('#myModal1').modal('show');$("#mceu_30").css("display","none");
			$("#PK_Id_Comunicado").val(result.PK_Id_Comunicado)
			$("#Titulo").val(result.Titulo)
			$("#Asunto").val(result.Asunto)
			document.getElementById("Destinatarios").innerHTML = "";
			var destinatarios = result.Destinatarios.split(',');
			var x = document.getElementById("Destinatarios");
			for(i=0;i < destinatarios.length-1;i++){
				var option = document.createElement("option");
				option.text = destinatarios[i];
				x.add(option);
			}
			
			
			$(tinymce.get('CuerpoMensaje').getBody()).html(result.CuerpoMensaje);
			$("#guardarc").css("display", "none");
			$("#enviar").css("display", "block");
			OcultarPopupposition();
        }
    });
}

function ResetControls(){
	$("#PK_Id_Comunicado").val(0);
	$("#Titulo").val("");
	$("#Asunto").val("");
	$(tinymce.get('CuerpoMensaje').getBody()).html("");
}

function EliminarComunicado(PK_Id_Comunicado) {
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
	    swal("Estimado Usuario", "El Comunicado ha sido eliminado con Éxito.", "success");
	    $.ajax({
	        type: 'GET',
	        url: '/ComunicacionesExternas/EliminarComunicado',
	        data: { PK_Id_Comunicado: PK_Id_Comunicado },
	        success: function (result) {
	            ListarComunicacionesExternas();
	        }
	    });
	});
}

function ValidaGuardarFormulario(){
	$("#frmcomunicacionesexternas").validate({
        errorClass: "error",
        rules: {
			Titulo: {
                required: true
            },
			Destinatarios: {
                required: false
            }
		},
		messages: {
			Titulo: {
                required: "*Este campo es requerido."
            },
			Destinatarios: {
                required: "*Este campo es requerido."
            }		
		}
	});
}


function AgregarCargos(){
	$('#myModal0').modal('show');
	$('#txtbuscar').val("");
	$("#cargos").empty();
	ResetCargos();
	var table = document.getElementById('cargos');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     //var val = checkboxes[0].checked;
     for (var i = 0; i < checkboxes.length; i++){
			checkboxes[i].checked = false;
	 }
	 
	 $("#chktodos").prop("checked",false);
}

function BuscarCargos(e, textarea){
	var code = (e.keyCode ? e.keyCode : e.which);
		if(code == 13) { //Enter keycode
			$.ajax({
				type: 'POST',
				url: '/ComunicacionesExternas/ObtenerCargos',
				data : { cargotemp : textarea },
				success: function (result) {
					var tHtml = "";
					$("#cargos").empty();
					tHtml += '<thead style="border-left:2px solid lightslategray;"><tr class="titulos_tabla">';
					tHtml += '<th '+class_css_header+'>&nbsp;</td>';
					tHtml += '<th '+class_css_header+'>Cargo</th>';
					tHtml += '</tr></thead>';
					$.each(result, function (key, val) {
						tHtml += '<tbody style="border-left:2px solid lightslategray;"><tr class="titulos_filas">';
						tHtml += '<td '+class_css_btxt+'><input type="checkbox" name="checkbox" id="chk" value="'+val.Nombre_Cargo+'"></td>';
						tHtml += '<td '+class_css_btxt+'>'+val.Nombre_Cargo+'</td>';
						tHtml += '</tr></tbody>';
					});
					$("#cargos").append(tHtml);
					$('#txtbuscar').val("");
				}
			});
		}
}

function CheckTodos(){
	var booVal = "";
	if($("#chktodos").is(':checked'))
		booVal = true;
	else
		booVal = false;

	if(booVal==false)
		return;

	 document.getElementById("Destinatarios").innerHTML = "";
	 var table = document.getElementById ('cargos');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     //var val = checkboxes[0].checked;
     for (var i = 0; i < checkboxes.length; i++){
			checkboxes[i].checked = true;
	 }
}

function AsignarCargos(){
	 var table = document.getElementById ('cargos');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     //var val = checkboxes[0].checked;
     for (var i = 0; i < checkboxes.length; i++){
			if(checkboxes[i].checked){
				arCargos.push(checkboxes[i].value);
			}
	 }

	var x = document.getElementById("Destinatarios");
	if(x.length==0)
	{
		for(i=0;i < arCargos.length;i++){
			var option = document.createElement("option");
			option.text = arCargos[i];
			x.add(option);
		}
	}
	else{
		for (var i = 0; i < x.length; i++) {
			var opt = x[i];
			arCargosTemp.push(opt.text);
		}

		for(i=0;i < arCargos.length;i++){
			arCargosTemp.push(arCargos[i]);
		}
		

		var SinDuplicados = [];
			var SinDuplicados = arCargosTemp.filter(function(elem, pos) {
			return arCargosTemp.indexOf(elem) == pos;
		});
		
		document.getElementById("Destinatarios").innerHTML = "";
		for(i=0;i < SinDuplicados.length;i++){
			var option = document.createElement("option");
			option.text = SinDuplicados[i];
			x.add(option);
		}
	}
	
	arCargos = new Array();
	arCargosTemp = new Array();
	$('#myModal0').modal('hide');
}

function QuitarCampos(){
	var x = document.getElementById("Destinatarios");
	x.remove(x.selectedIndex);
}

function CerrarModal1(){
	arCargos = new Array();
	$('#myModal0').modal('hide');
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function AgregarPersonas(){
	$('#myModal2').modal('show');
	$('#txtbuscarpersona').val("");
	ResetTable();
	var table = document.getElementById ('personas');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     //var val = checkboxes[0].checked;
     for (var i = 0; i < checkboxes.length; i++){
			checkboxes[i].checked = false;
	 }
	 
	 $("#chktodos").prop("checked",false);
}

function BuscarPersonas(e, textarea){
	var code = (e.keyCode ? e.keyCode : e.which);
		if(code == 13) { //Enter keycode
			$.ajax({
				type: 'POST',
				url: '/ComunicacionesExternas/ListarPersonas',
				data : { Numero_Documento : textarea },
				success: function (result) {
					var tHtml = "";
					tHtml += '<thead style="border-left:2px solid lightslategray;"><tr class="titulos_filas">';
					tHtml += '<td '+class_css_btxt+'><input type="checkbox" name="checkbox" id="chk1" value="C-'+result.idPersona +'-'+result.nombre1+' '+result.nombre2+' '+result.apellido1+' '+result.apellido2+' '+result.emailPersona +'"></td>';
					tHtml += '<td '+class_css_btxt+'>'+result.idPersona+'</td>';
					tHtml += '<td '+class_css_btxt+'>'+result.nombre1+' '+result.nombre2+' '+result.apellido1+' '+result.apellido2+'</td>';
					tHtml += '<td '+class_css_btxt+'>'+result.emailPersona +'</td>';
					tHtml += '</tr><thead>';
					$("#personas").append(tHtml);
					$("#txtbuscarpersona").val("");
				}
			});
		}
}

function ResetTable()
{
var tHtml = "";
$("#personas").empty();
tHtml += '<thead style="border-left:2px solid lightslategray;"><tr class="titulos_tabla">';
tHtml += '<th '+class_css_btxt+'>&nbsp;</td>';
tHtml += '<th '+class_css_btxt+'>Id Persona</th>';
tHtml += '<th '+class_css_btxt+'>Nombre Persona</th>';
tHtml += '<th '+class_css_btxt+'>Correo Electronico</th>';
tHtml += '</tr></thead>';
$("#personas").append(tHtml);
}

function ResetCargos(){
var tHtml = "";
$("#cargos").empty();
tHtml += '<thead style="border-left:2px solid lightslategray;"><tr class="titulos_tabla">';
tHtml += '<td style="border-right: 2px solid lightslategray; text-align:center" width="10%">&nbsp;</td>';
tHtml += '<td>Cargo</td></tr></thead>';
$("#cargos").append(tHtml);
}


var arPersonas = new Array();
function CheckTodos1(){
	var booVal = "";
	if($("#chktodos1").is(':checked'))
		booVal = true;
	else
		booVal = false;

	if(booVal==false)
		return;

	 document.getElementById("Destinatarios").innerHTML = "";
	 var table = document.getElementById ('grupos');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     var val = checkboxes[0].checked;
     for (var i = 0; i < checkboxes.length; i++){
			checkboxes[i].checked = true;
	 }
}

function AsignarPersonas(){
	var table = document.getElementById ('grupos');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     //var val = checkboxes[0].checked;
     for (var i = 0; i < checkboxes.length; i++){
			if(checkboxes[i].checked){
				arPersonas.push(checkboxes[i].value);
			}
	 }

	var x = document.getElementById("Destinatarios");
	if(x.length==0)
	{
		for(i=0;i < arPersonas.length;i++){
			var option = document.createElement("option");
			option.text = arPersonas[i];
			x.add(option);
		}
	}
	else{
		for (var i = 0; i < x.length; i++) {
			var opt = x[i];
			arPersonasTemp.push(opt.text);
		}

		for(i=0;i < arPersonas.length;i++){
			arPersonasTemp.push(arPersonas[i]);
		}
		
		
		var SinDuplicados1 = [];
			var SinDuplicados1 = arPersonasTemp.filter(function(elem, pos) {
		   return arPersonasTemp.indexOf(elem) == pos;
		});
		
		document.getElementById("Destinatarios").innerHTML = "";
		for(i=0;i < SinDuplicados1.length;i++){
			var option = document.createElement("option");
			option.text = SinDuplicados1[i];
			x.add(option);
		}

	}
	

	arPersonas = new Array();
	arPersonasTemp = new Array();
	$('#myModal3').modal('hide');
}

function QuitarCampos(){
	var x = document.getElementById("Destinatarios");
	x.remove(x.selectedIndex);
}


function CheckTodos11(){
	var booVal = "";
	if($("#chktodos11").is(':checked'))
		booVal = true;
	else
		booVal = false;

	if(booVal==false)
		return;

	 document.getElementById("Destinatarios").innerHTML = "";
	 var table = document.getElementById ('personas');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     var val = checkboxes[0].checked;
     for (var i = 0; i < checkboxes.length; i++){
			checkboxes[i].checked = true;
	 }
}


function AsignarPersonasTemp(){
	var table = document.getElementById ('personas');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     //var val = checkboxes[0].checked;
     for (var i = 0; i < checkboxes.length; i++){
			if(checkboxes[i].checked){
				arPersonas.push(checkboxes[i].value);
			}
	 }

	var x = document.getElementById("Destinatarios");
	if(x.length==0)
	{
		for(i=0;i < arPersonas.length;i++){
			var option = document.createElement("option");
			option.text = arPersonas[i];
			x.add(option);
		}
	}
	else{
		for (var i = 0; i < x.length; i++) {
			var opt = x[i];
			arPersonasTemp.push(opt.text);
		}

		for(i=0;i < arPersonas.length;i++){
			arPersonasTemp.push(arPersonas[i]);
		}
		
		
		var SinDuplicados1 = [];
			var SinDuplicados1 = arPersonasTemp.filter(function(elem, pos) {
		   return arPersonasTemp.indexOf(elem) == pos;
		});
		
		document.getElementById("Destinatarios").innerHTML = "";
		for(i=0;i < SinDuplicados1.length;i++){
			var option = document.createElement("option");
			option.text = SinDuplicados1[i];
			x.add(option);
		}

	}
	

	arPersonas = new Array();
	arPersonasTemp = new Array();
	$('#myModal2').modal('hide');
}

function AsignarAnonimos()
{
	$('#myModal6').modal('show');
	$("#txtnombre1").val('');
    $("#txtcorreo1").val('');	
}


function add2(){
	var txtnombre = $("#txtnombre1").val();
    var txtcorreo = $("#txtcorreo1").val();	
	var tHtml = "";
	tHtml += '<tbody style="border-left:2px solid lightslategray;"><tr class="titulos_filas">';
	tHtml += '<td style="border-right: 2px solid lightslategray; text-align:center"><input type="checkbox" name="checkbox" id="chk1" value="N/A-'+txtnombre+','+txtcorreo+'"></td>';
	tHtml += '<td style="border-right: 2px solid lightslategray; text-align:center">N/A</td>';
	tHtml += '<td style="border-right: 2px solid lightslategray; text-align:center">'+txtnombre+'</td>';
	tHtml += '<td style="border-right: 2px solid lightslategray; text-align:center">'+txtcorreo+'</td>';
	tHtml += '</tr></tbody>';
	$("#personas").append(tHtml);
	$('#myModal6').modal('hide');
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function CrearGrupos(){
	$('#myModal3').modal('show');
	ListarGrupos();
}

function GuardarGrupo(){
	$.ajax({
		type: 'GET',
		url: '/ComunicacionesExternas/GuardarGrupo',
		data: { NombreGrupo: $("#txtgrupo").val() },
		success: function (result) {
			ListarGrupos();
		}
	});
}

function ListarGrupos(){
	$.ajax({
		type: 'GET',
		url: '/ComunicacionesExternas/ListarGrupos',
		success: function (result) {
			var tHtml = "";
			$("#grupos").empty();
			tHtml += '<thead style="border-left:2px solid lightslategray;"><tr class="titulos_tabla">';
			tHtml += '<th '+class_css_header+'>&nbsp;</td>';
			tHtml += '<th '+class_css_header+'>Nombre de Grupo</th>';
			tHtml += '<th '+class_css_header+'>Acciones</th>';
			tHtml += '</tr><thead>';
			$.each(result, function (key, val) {
				tHtml += '<tbody style="border-left:2px solid lightslategray;"><tr class="titulos_filas">';
				tHtml += '<td '+class_css_btxt+'><input type="checkbox" name="checkbox" id="chk" value="'+val.NombreGrupo+'"></td>';
				tHtml += '<td '+class_css_btxt+'>'+val.NombreGrupo+'</td>';
				tHtml += '<td '+class_css_btxt+'>';
				tHtml += '<a href="#" onclick="AgregarMiembros('+val.pk_id_grupo+');" class="btn btn-search btn-md" title="Agregar Miembros"><span class="glyphicon glyphicon-pencil"></span></a>';
				tHtml += '&nbsp;<a href="#" onclick="EliminarGrupo('+val.pk_id_grupo+');" class="btn btn-search btn-md" title="Eliminar"><span class="glyphicon glyphicon-erase"></span></a>';
				tHtml += '</td></tr></tbody>';
			});
			$("#grupos").append(tHtml);
		}
	});
}

function EliminarGrupo(idgrupo){
	$("#PK_Id_grupo").val(idgrupo);
	$.ajax({
		type: 'POST',
		url: '/ComunicacionesExternas/EliminarGrupo',
		data : { idgrupo : idgrupo },
		success: function (result) {
			ListarGrupos();
		}
	});
}

function AgregarMiembros(idgrupo){
	$("#PK_Id_grupo").val(idgrupo);
	$('#myModal4').modal('show');
	ListarMiembros();
}

function AgregarContacto(){
	$('#myModal5').modal('show');
		
}

function add(){
	var idgrupo = $("#PK_Id_grupo").val();
	var txtnombre = $("#txtnombre").val();
    var txtcorreo = $("#txtcorreo").val();
	var pk_id_grupo_usuario_comunicaciones = $("#pk_id_grupo_usuario_comunicaciones").val();
	
	$.ajax({
		type: 'GET',
		url: '/ComunicacionesExternas/GuardarMiembro',
		data : { idgrupo : idgrupo, pk_id_grupo_usuario_comunicaciones : pk_id_grupo_usuario_comunicaciones, txtnombre : txtnombre, txtcorreo : txtcorreo },
		success: function (result) {
			ListarMiembros();
			$('#myModal5').modal('hide');
			$("#txtnombre").val('');
			$("#txtcorreo").val('');
		}
	});
}



function ListarMiembros(){
	$.ajax({
		type: 'POST',
		url: '/ComunicacionesExternas/ListarMiembros',
		data : { idgrupo : $("#PK_Id_grupo").val() },
		success: function (result) {
			var tHtml = "";
			$("#miembros").empty();
			tHtml +='<thead style="border-left:2px solid lightslategray;"><tr class="titulos_tabla">';
            tHtml +='<td '+class_css_header+' width="10%">&nbsp;</td>';
            tHtml +='<td '+class_css_header+'>Nombre del Contacto</td>';
            tHtml +='<td '+class_css_header+'>Correo Electronico</td>';
            tHtml +='<td '+class_css_header+'>Acciones</td>';
            tHtml +='</tr></thead>';
			$.each(result, function (key, val) {
			    if(val.Status == true)
					tHtml +='<tbody style="border-left:2px solid lightslategray;"><tr class="titulos_filas"><td '+class_css_btxt+'><input type="checkbox" name="checkbox" id="chkmember" value="'+val.pk_id_grupo_usuario_comunicaciones+'" checked></td>';
				else
					tHtml +='<tr class="titulos_filas"><td '+class_css_btxt+'><input type="checkbox" name="checkbox" id="chkmember" value="'+val.pk_id_grupo_usuario_comunicaciones+'"></td>';
					
				tHtml +='<td '+class_css_btxt+'>'+val.nombre_contacto+'</td>';
				tHtml +='<td '+class_css_btxt+'>'+val.email+'</td>';
				tHtml +='<td>';
				tHtml += '<a href="#" onclick="EditarMiembros('+val.pk_id_grupo_usuario_comunicaciones+');" class="btn btn-search btn-md" title="Agregar Miembros"><span class="glyphicon glyphicon-pencil"></span></a>';
				tHtml += '&nbsp;<a href="#" onclick="EliminarMiembros('+val.pk_id_grupo_usuario_comunicaciones+');" class="btn btn-search btn-md" title="Eliminar"><span class="glyphicon glyphicon-erase"></span></a>';
				tHtml +='</td></tr></tbody>';
			});
			$("#miembros").append(tHtml);
		}
	});
}

function EditarMiembros(pk_id_grupo_usuario_comunicaciones)
{
	$.ajax({
		type: 'POST',
		url: '/ComunicacionesExternas/ConsultarMiembro',
		data : { pk_id_grupo_usuario_comunicaciones : pk_id_grupo_usuario_comunicaciones },
		success: function (result) {
			$('#myModal5').modal('show');
			$("#pk_id_grupo_usuario_comunicaciones").val(pk_id_grupo_usuario_comunicaciones);
			$("#txtnombre").val(result.nombre_contacto);
			$("#txtcorreo").val(result.email);
		}
	});	
}

function EliminarMiembros(pk_id_grupo_usuario_comunicaciones)
{	

	$.ajax({
		type: 'GET',
		url: '/ComunicacionesExternas/EliminarMiembros',
		data : { pk_id_grupo_usuario_comunicaciones : pk_id_grupo_usuario_comunicaciones },
		success: function (result) {
			ListarMiembros();
		}
	});
}

function ActualizarGrupo(){
	 var status_miembros = new Array();
	 var table = document.getElementById ('miembros');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     for (var i = 0; i < checkboxes.length; i++){
		status_miembros.push(checkboxes[i].value+','+checkboxes[i].checked);
	 }

	$.ajax({
		type: 'POST',
		dataType: "json",
		url: '/ComunicacionesExternas/ActualizarGrupo',
		data : { PK_Id_grupo : $("#PK_Id_grupo").val(), txtmiembro : $("#txtmiembro").val(), armiembros : status_miembros },
		success: function (result) {
			$('#myModal4').modal('hide');
			ListarGrupos();
		}
	});
}

function CheckMiembros(){
	var booVal = "";
	if($("#CheckMiembros1").is(':checked'))
		booVal = true;
	else
		booVal = false;

	 var table = document.getElementById ('miembros');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     //var val = checkboxes[0].checked;
     for (var i = 0; i < checkboxes.length; i++){
			checkboxes[i].checked = booVal;
	 }
}

function CerrarModal3(){
	$('#myModal3').modal('hide');
}

function CerrarModal4(){
	$('#myModal4').modal('hide');
}

function CerrarModal5(){
	$('#myModal5').modal('hide');
}