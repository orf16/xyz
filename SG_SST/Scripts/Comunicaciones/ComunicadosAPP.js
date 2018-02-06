var arCargos = new Array();
var arCargosTemp = new Array();
var arPersonas = new Array();
var arPersonasTemp = new Array();
 

tinyMCE.init({
	// General options
	mode: "textareas",
	theme: "modern",
	// Theme options
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
	content_css: "css/example.css",
});

var class_css_header = 'style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase"';
var class_css = 'style="border-right: 2px solid lightslategray; vertical-align:middle"';
var class_css_btxt = 'style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"';

function CrearComunicado() {
	ResetControls();
    $('#myModal1').modal('show');
    $("#IDComunicadosAPP").val(0);
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
			url: '/ComunicadosAPP/GuardarComunicado',
			data: { IDComunicadosAPP: $("#IDComunicadosAPP").val(), Titulo: $("#Titulo").val(), Asunto: tinyMCE.get('Asunto').getContent(), arreglo : arreglo, Origen : origen },
			success: function (result) {
				//alert(JSON.stringify(result));
				if(result.origen=='G')
				{
					OcultarPopupposition();
					Reload(result.PK_Id_Comunicado_temp);
				}

				if(result.origen=='E')
				{
					swal("Estimado Usuario", "El Comunicado ha sido enviado con Éxito.", "success");
					$('#myModal1').modal('hide');
					OcultarPopupposition();
					ListarComunicacionesExternas();
				}
			}
		});
	}
}

var ar1 = new Array();
var ar2 = new Array();
var ar3 = new Array();
var ar4 = new Array();
var ar5 = new Array();
function SplitArray(arreglo){
	var contador = arreglo.length/5; 
	var x = 0;
	for (var i = 0; i <= contador; i++) {
		ar1.push(arreglo[i]);
		x=i;
	}
	
	contador = contador+x;
	x = x+1;

	for (var i = x; i <= contador; i++) {
		ar2.push(arreglo[x]);
		x=i;
	}
			
		
		
}

function Reload(PK_Id_Comunicado)
{
	$("#IDComunicadosAPP").val(PK_Id_Comunicado)
	swal({
        title: "Estimado Usuario",
        text: "El Comunicado APP ha sido Guardado, Desea Enviar el Comunicado APP ahora?",
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
		   swal("Estimado Usuario", "El Comunicado APP ha sido enviado con Éxito.", "success");
	   }
	   else
		   ListarComunicacionesExternas();
	   
	});
}

ListarComunicacionesExternas();
function ListarComunicacionesExternas() {
    $.ajax({
        type: 'POST',
        url: '/ComunicadosAPP/ListarComunicadosAPP',
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
                tHtml += '<td '+class_css_btxt+'>' + val.Estado + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.FechaCreacion.substring(0, 10) + '</td>';
                tHtml += '<td '+class_css_btxt+'>' + val.FechaEnvio.substring(0, 10) + '</td>';
                tHtml += '<td '+class_css_btxt+'>&nbsp;<a href="#" class="btn btn-search btn-md" title="Editar" onClick="EditarComunicado(' + val.IDComunicadosAPP + ');"><span class="glyphicon glyphicon-pencil"></span></a>&nbsp;|&nbsp;';
                tHtml += '<a href="#" class="btn btn-search btn-md" title="Eliminar" onClick="EliminarComunicado(' + val.IDComunicadosAPP + ');"><span class="glyphicon glyphicon-erase"></span></a></td>';
                tHtml += '</tr></tbody>';
            });
            $("#gridcomunicadosapp").append(tHtml);
        }
    });
}

function EditarComunicado(IDComunicadosAPP) {
    $("#IDComunicadosAPP").val(IDComunicadosAPP);
    $.ajax({
        type: 'GET',
        url: '/ComunicadosAPP/EditarComunicado',
        data: { IDComunicadosAPP: IDComunicadosAPP },
        success: function (result) {
            $('#myModal1').modal('show');
			$("#IDComunicadosAPP").val(result.IDComunicadosAPP);
			$("#Titulo").val(result.Titulo);
			document.getElementById("Destinatarios").innerHTML = "";
			var destinatarios = result.Destinatarios.split(',');
			var x = document.getElementById("Destinatarios");
			for(i=0;i < destinatarios.length-1;i++){
				var option = document.createElement("option");
				option.text = destinatarios[i];
				x.add(option);
			}
			$(tinymce.get('Asunto').getBody()).html(result.Asunto);
            $("#guardarc").css("display", "none");
	$("#enviar").css("display", "block");
        }
    });
}

function ResetControls(){
	$("#IDComunicadosAPP").val(0);
	$("#Titulo").val("");
	$(tinymce.get('Asunto').getBody()).html("");
}

function EliminarComunicado(IDComunicadosAPP) {
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
	        url: '/ComunicadosAPP/EliminarComunicado',
	        data: { IDComunicadosAPP: IDComunicadosAPP },
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
    
//////////////////////////////////////////////////////////////////////////////////////////////
function AgregarCargos(){
	$('#myModal0').modal('show');
	$('#txtbuscar').val("");
	$("#cargos").empty();
	ResetCargos();
	var table = document.getElementById('cargos');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     var val = checkboxes[0].checked;
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
					tHtml += '<tr class="titulos_tabla">';
					tHtml += '<th style="border-right: 2px solid lightslategray; text-align:center">&nbsp;</td>';
					tHtml += '<th style="border-right: 2px solid lightslategray; text-align:center">Cargo</th>';
					tHtml += '</tr>';
					$.each(result, function (key, val) {
						tHtml += '<tr class="titulos_filas">';
						tHtml += '<td style="border-right: 2px solid lightslategray; text-align:center"><input type="checkbox" name="checkbox" id="chk" value="'+val.Nombre_Cargo+'"></td>';
						tHtml += '<td style="border-right: 2px solid lightslategray; text-align:center">'+val.Nombre_Cargo+'</td>';
						tHtml += '</tr>';
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

///////////////////////////////////////////////////////////////////////////////////////////

function AgregarPersonas(){
	$('#myModal2').modal('show');
	$('#txtbuscarpersona').val("");
	ResetTable();
	var table = document.getElementById('personas');
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
					//$("#personas").empty();
					tHtml += '<tbody style="border-left:2px solid lightslategray;"><tr class="titulos_filas">';
					tHtml += '<td '+class_css_btxt+'><input type="checkbox" name="checkbox" id="chk1" value="'+result.idPersona +'-'+result.nombre1+' '+result.nombre2+' '+result.apellido1+' '+result.apellido2+'"></td>';
					tHtml += '<td '+class_css_btxt+'>'+result.idPersona+'</td>';
					tHtml += '<td '+class_css_btxt+'>'+result.nombre1+' '+result.nombre2+' '+result.apellido1+' '+result.apellido2+'</td></tr></tbody>';
					$("#personas").append(tHtml);
					$('#txtbuscarpersona').val("");
				}
			});
		}
}

function ResetTable()
{
var tHtml = "";
$("#personas").empty();
tHtml += '<thead style="border-left:2px solid lightslategray;"><tr class="titulos_tabla">';
tHtml += '<td '+class_css_header+'>&nbsp;</td>';
tHtml += '<td '+class_css_header+'><b>Id. Persona</b></td>';
tHtml += '<td '+class_css_header+'><b>Nombre Persona</b></td>';
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
	 var table = document.getElementById ('personas');
     var checkboxes = table.querySelectorAll ('input[type=checkbox]');
     var val = checkboxes[0].checked;
     for (var i = 0; i < checkboxes.length; i++){
			checkboxes[i].checked = true;
	 }
}

function AsignarPersonas(){
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

function QuitarCampos(){
	var x = document.getElementById("Destinatarios");
	x.remove(x.selectedIndex);
}

function CerrarModal2(){
	arPersonas = new Array();
	$('#myModal2').modal('hide');
}