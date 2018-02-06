var urlBase = ""
try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};

var SelTodosTematica = false;
var SelTodosTematicaEmp = false;
function ObtenerSiarpAfiliados() {
    var added;
    if(document.getElementById("idEmpleados").value)
        added = document.getElementById("idEmpleados").value.split(",");
    var items;
    var carg = new Array();
    items = $("#Fk_Id_Cargo").find("option:selected");
    items.each(function (ind, element) {
        
        carg.push($.trim($(element).text()));
    });
    PopupPosition();
    $.ajax({
        url: urlBase + '/Competencia/ObtenerSiarpAfilidos',  //primero el modulo/controlador/metodo que esta en el controlador
        data: {                                           // se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            //nitempresa: $("#nitempresa").val()
            cargo: carg
        },
        type: 'POST',
        success: function (result) {
            OcultarPopupposition();
            if (result) {
                $('#tablaAfiliados').empty();
                $('#tablaAfiliados').append
                    ('<tr class="titulos_tabla"> <td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle"><b>Seleccionar</b></td><br> <td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle"><b>ID Empleado</b></td> <td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle"><b>Primer nombre</b></td> <td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle"><b>Segundo nombre</b></td> <td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle"><b>Primer apellido</b></td> <td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle"><b>Segundo apellido</b></td> <td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle"><b>Cargo</b></td></tr>');
                $.each(result.Data, function (ind, element) {
                    var id = "'" + element.idPersona + "&" + element.nombre1 + "&" + element.nombre2 + "&" + element.apellido1
                    + "&" + element.apellido2 + "&" + element.cargo + "&" + element.emailPersona + "'";
                    if (added)
                        var index = added.indexOf(element.idPersona);
                    else
                        var index = -1;
                    if (index > -1) {
                        $('#tablaAfiliados').append('<tr>' +
                                        '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center">' + ('<input type="checkbox" checked name="list" id="list" onclick="adicionarEmpleado(' + id + ')"') + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; vertical-align:middle">' + element.idPersona + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; vertical-align:middle">' + element.nombre1 + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; vertical-align:middle">' + element.nombre2 + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; vertical-align:middle">' + element.apellido1 + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; vertical-align:middle">' + element.apellido2 + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; vertical-align:middle">' + element.cargo + '</td>' +
                                      '</tr>')
                        adicionarEmpleado(element.idPersona + "&" + element.nombre1 + "&" + element.nombre2 + "&" + element.apellido1
                    + "&" + element.apellido2 + "&" + element.cargo + "&" + element.emailPersona);
                    }
                    else {
                        $('#tablaAfiliados').append('<tr>' +
                                          '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center">' + ('<input type="checkbox" name="list" id="list" onclick="adicionarEmpleado(' + id + ')"') + '</td>' +
                                          '<td style="border-right: 2px solid lightslategray; vertical-align:middle">' + element.idPersona + '</td>' +
                                          '<td style="border-right: 2px solid lightslategray; vertical-align:middle">' + element.nombre1 + '</td>' +
                                          '<td style="border-right: 2px solid lightslategray; vertical-align:middle">' + element.nombre2 + '</td>' +
                                          '<td style="border-right: 2px solid lightslategray; vertical-align:middle">' + element.apellido1 + '</td>' +
                                          '<td style="border-right: 2px solid lightslategray; vertical-align:middle">' + element.apellido2 + '</td>' +
                                          '<td style="border-right: 2px solid lightslategray; vertical-align:middle">' + element.cargo + '</td>' +
                                        '</tr>')
                    }
                })
            }
        }
    });
}

function BusquedaTematica(element) {

    var stringBusqueda = $(element).val()

    $.ajax({
        url: urlBase + '/Competencia/BusquedaTematica',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            Busqueda: stringBusqueda
        },
        type: 'POST',
        success: function (result) {
            if (result)
            {                
                $("#select_tematica").html("");
                var $trTitulo = '<tr class="titulos_tabla"><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle">Tematica</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle"></th></tr>';
                $("#select_tematica").append($trTitulo);
                $.each(result, function (ind, element) {
                    var $tr = "<tr name='tem' ><td style='border-right: 2px solid lightslategray; vertical-align:middle'>" + element.Descripcio_Tematicas + " <input name= 'idtePos' type='hidden' value = " + element.Id_Tematica + " <input name= 'tipoTem' type='hidden' value = " + element.TipoTematica + "> </td>";
                    $tr = $tr + "<td style='border-right: 2px solid lightslategray; vertical-align:middle; text-align:center'><a name='seleccionarTematica' onclick='AgregarASeleccionadas(this)' class='btn btn-search btn-md'><span class='glyphicon glyphicon-plus'></span></a></td> </tr>";
                    $("#select_tematica").append($tr);
                })
                paginador("#select_tematica", "tr[name = tem]", "#paginador1");
            }
        }
    });
}

function AgregarASeleccionadas(tematicaselecionada, idtePos)
{
    var $trTematica = $(tematicaselecionada).closest("tr");
    var $trStematica = $trTematica.clone();
    var index = $.inArray(idtePos, addedTematicas);
    if (index >= 0 || SelTodosTematica==true) {
        swal("Estimado Usuario", "La temática ya se encuentra seleccionada", "error");
        return;
    }
    $trStematica.find("span").attr("class", "glyphicon glyphicon-minus")
    //$("#divresponsabilidadClon").find("span").attr("title", "Eliminar Responsabilidad")
    $trStematica.find("a[name=seleccionarTematica]").attr("onclick", "DesmarcarTematica(this,"+idtePos+")")
    //$(tematicaselecionada).closest("tr").remove();
    $("#seleccionadas_tematicas").append($trStematica);
    addedTematicas.push(idtePos);
}
function AgregarASeleccionadasEmp(tematicaselecionada, idtePos) {
    var $trTematica = $(tematicaselecionada).closest("tr");
    var $trStematica = $trTematica.clone();
    var index = $.inArray(idtePos, addedTematicasEmp);
    if (index >= 0 || SelTodosTematicaEmp==true) {
        swal("Estimado Usuario", "La temática ya se encuentra seleccionada", "error");
        return;
    }
    $trStematica.find("span[name=adicionar]").attr("class", "glyphicon glyphicon-minus")
    //$("#divresponsabilidadClon").find("span").attr("title", "Eliminar Responsabilidad")
    $trStematica.find("a[name=seleccionarTematicaEmp]").attr("onclick", "DesmarcarTematicaEmp(this," + idtePos + ")")
    //$(tematicaselecionada).closest("tr").remove();
    $("#seleccionadas_tematicasEmp").append($trStematica);
    addedTematicasEmp.push(idtePos);
}

function DesmarcarTematica(tematicaAdesmarcar, idtePos) {
    var $trTematica = $(tematicaAdesmarcar).closest("tr");
    var $trStematica = $trTematica.clone();
    $trStematica.find("span").attr("class", "glyphicon glyphicon-plus")
    //$("#divresponsabilidadClon").find("span").attr("title", "Eliminar Responsabilidad")
    $trStematica.find("a[name=seleccionarTematica]").attr("onclick", "AgregarASeleccionadas(this)")
    $(tematicaAdesmarcar).closest("tr").remove();
    var index = addedTematicas.indexOf(idtePos);
    addedTematicas.splice(index, 1);
    //$("#select_tematica").append($trStematica);
}

function DesmarcarTematicaEmp(tematicaAdesmarcar, idtePos) {
    var $trTematica = $(tematicaAdesmarcar).closest("tr");
    var $trStematica = $trTematica.clone();
    $trStematica.find("span").attr("class", "glyphicon glyphicon-plus")
    //$("#divresponsabilidadClon").find("span").attr("title", "Eliminar Responsabilidad")
    $trStematica.find("a[name=seleccionarTematicaEmp]").attr("onclick", "AgregarASeleccionadasEmp(this)")
    $(tematicaAdesmarcar).closest("tr").remove();
    var index = addedTematicasEmp.indexOf(idtePos);
    addedTematicasEmp.splice(index, 1);
    //$("#select_tematica").append($trStematica);
}

function BusquedaTematica(element) {
    var stringBusqueda = $(element).val()
    $.ajax({
        url: urlBase + '/Competencia/BusquedaTematica',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            Busqueda: stringBusqueda
        },
        type: 'POST',
        success: function (result) {
            if (result) {
                $("#select_tematica").find("tr:gt(0)").remove();
                $.each(result, function (ind, element) {
                    var $tr = "<tr class='titulos_filas' name='tem' ><td style='border-right: 2px solid lightslategray; vertical-align:middle'>" + element.Descripcio_Tematicas + " <input name= 'idtePos' type='hidden' value = " + element.Id_Tematica + " <input name= 'tipoTem' type='hidden' value = " + element.TipoTematica + " > </td>";
                    $tr = $tr + '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><a onclick="AgregarASeleccionadas(this, ' + element.Id_Tematica + ')" name="seleccionarTematica"><span class="glyphicon glyphicon-plus"></span></a></td></tr>';
                    $("#select_tematica").append($tr);//agregamos las opciones consultadas
                })

                paginador("#select_tematica", "tr[name = tem]", "#paginador1");
            }
        }
    });
}

function BusquedaTematicaEmp(element) {
    //var stringBusqueda = $("#BuscarTematica").val()
    var stringBusqueda = $(element).val()
    $.ajax({
        url: urlBase + '/Competencia/BusquedaTematicaEmp',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            Busqueda: stringBusqueda
        },
        type: 'POST',
        success: function (result) {
            if (result) {
                $("#select_tematicaEmp").find("tr:gt(0)").remove();
                $.each(result, function (ind, element) {
                    var $tr = "<tr class='titulos_filas'  name='temEmp' ><td name='tdtemaEmp' style='border-right: 2px solid lightslategray; vertical-align:middle'>" + element.Descripcio_Tematicas + " <input name= 'idtePosEmp' type='hidden' value = " + element.Id_Tematica + "<input name= 'temaPosEmp' type='hidden' value = " + element.Tematicas + " <input name= 'tipoTemEmp' type='hidden' value = " + element.TipoTematica + "> </td>";
                    $tr = $tr +'<td style="border-right: 2px solid lightslategray; text-align:center">';
                    if (element.NombreDocumento) {
                        $tr = $tr + '<button type="button" class="btn btn-search btn-md" data-toggle="modal" data-target="#modal' + element.Id_Tematica + '" title="Ver documento asociado"><span class="glyphicon glyphicon-search"></span></button>';
                        $tr = $tr + '<div id="modal' + element.Id_Tematica + '" class=" modal fade" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header encabezado" style="background-color:transparent; border-bottom:none">';
                        $tr = $tr + '<button type="button" class="close" data-dismiss="modal" aria-label="Close"></button><h4 class="modal-title title">  </h4></div> <div class="modal-body"> <div class="pdf-cargar-tematica"><iframe width="640" height="360" src="/Competencia/MostrarTematicaPDF?Id_Tematica=' + element.Id_Tematica + '" frameborder="0" allowfullscreen></iframe>';
                        $tr = $tr + ' </div></div><div class="modal-footer alert-dismissable"><button type="button" class="boton botoncancel" data-dismiss="modal">Cancelar</button> </div> </div></div></div>';
                    }
                    $tr = $tr + "</td><td style='border-right: 2px solid lightslategray; vertical-align:middle; text-align:center'><a name='seleccionarTematicaEmp' onclick='AgregarASeleccionadasEmp(this,"+ element.Id_Tematica + ")' class='btn btn-search btn-md'><span class='glyphicon glyphicon-plus'></span></a></td> </tr>";
                    $("#select_tematicaEmp").append($tr);//agregamos las opciones consultadas
                })
                paginador("#select_tematicaEmp", "tr[name = temEmp]", "#paginador2");
            }
        }
    });
}

function uploadAjax() {
    var inputFileImage = document.getElementById("archivoSubir");
    if (!$("#nuevaFormacion").val()) {
        swal("Estimado Usuario", "Debe ingresar el nombre de la nueva formación", "warning");
        return;
    }
    else  if ($("#archivoSubir").val()) {
            if ($("#archivoSubir").val().substring($("#archivoSubir").val().lastIndexOf(".")).toLowerCase() != '.pdf') {
            swal("Estimado Usuario", "El archivo debe tener extensión .pdf", "warning");
            return;
        }
    }
    var file = inputFileImage.files[0];
    var data = new FormData();
    data.append("archivo",file)
    $.ajax({
        url: urlBase + '/Competencia/ObtenerArchivo',
        type:'POST',
        contentType: false,
        data : data,
        processData:false,
        cache: false        
    }).done(function (res) {
        var tematica = $("#nuevaFormacion").val();
        var respu = res;
        $.ajax({
            url: urlBase + '/Competencia/GuardarTematicaE',
            data: {
                tematicasEmp: tematica,
                res: respu,
            },
            type: 'POST',
            success: function (result) {
                if (result) {
                    $("#select_tematicaEmp").find("tr:gt(0)").remove();
                    $.each(result, function (ind, element) {
                        var $tr = "<tr class='titulos_filas'  name='temEmp' ><td style='border-right: 2px solid lightslategray; vertical-align:middle'>" + element.Descripcio_Tematicas + " <input name= 'idtePosEmp' type='hidden' value = " + element.Id_Tematica + "<input name= 'temaPosEmp' type='hidden' value = " + element.Tematicas + " <input name= 'tipoTemEmp' type='hidden' value = " + element.TipoTematica + "> </td>";
                        $tr = $tr +'<td style="border-right: 2px solid lightslategray; text-align:center">';
                        if (element.NombreDocumento) {
                            $tr = $tr + '<button type="button" class="btn btn-search btn-md" data-toggle="modal" data-target="#modal' + element.Id_Tematica + '" title="Ver documento asociado"><span class="glyphicon glyphicon-search"></span></button>';
                            $tr = $tr + '<div id="modal' + element.Id_Tematica + '" class=" modal fade" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header encabezado" style="background-color:transparent; border-bottom:none">';
                            $tr = $tr + '<button type="button" class="close" data-dismiss="modal" aria-label="Close"></button><h4 class="modal-title title">  </h4></div> <div class="modal-body"> <div class="pdf-cargar-tematica"><iframe width="640" height="360" src="/Competencia/MostrarTematicaPDF?Id_Tematica=' + element.Id_Tematica + '" frameborder="0" allowfullscreen></iframe>';
                            $tr = $tr + ' </div></div><div class="modal-footer alert-dismissable"><button type="button" class="boton botoncancel" data-dismiss="modal">Cancelar</button> </div> </div></div></div>';
                        }
                        $tr = $tr + "</td><td style='border-right: 2px solid lightslategray; vertical-align:middle; text-align:center'><a name='seleccionarTematicaEmp' onclick='AgregarASeleccionadasEmp(this," + element.Id_Tematica + ")' class='btn btn-search btn-md'><span  name='adicionar' class='glyphicon glyphicon-plus'></span></a></td> </tr>";
                        $("#select_tematicaEmp").append($tr);//agregamos las opciones consultadas
                    })
                    paginador("#select_tematicaEmp", "tr[name = temEmp]", "#paginador2");
                    $("#nuevaFormacion").val("");
                    $("#archivoSubir").val("");
                    swal("Estimado Usuario", "Se ha guardado correctamente la temática", "success");
                }
                else if(result=="")
                    swal("Estimado Usuario", "La tématica con nombre " + tematica + " ya existe, no se puede guardar con el mismo nombre.", "error");


            }
        })
    });
}

function pregunta() {
    var i = 0;
    if (addedTematicas.length== 0 && addedTematicasEmp.length==0){
        swal("Estimado Usuario", "Debe seleccionar mínimo una temática", "warning");
        return;
    }
    document.getElementById("idEmpleados").value = addedTrabajador;
    //if (confirm('¿Estas seguro de asignar las Competencias?')) {
    //var temposse = new array();
    swal({
        title: "Estimado usuario",
        text: "¿Está seguro de asignar la competencia?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si",
        cancelButtonText: "No",
        closeOnConfirm: false
    },
    function(){
        var temaPosAenviarEmp = $("#seleccionadas_tematicasEmp").find("td[name='tdtemaEmp']");
        temaPosAenviarEmp.each(function (ind, element) {
            $(element).find("input[name='idtePosEmp']").attr("name", "[" + i + "].Id_Tematica");
            $(element).find("input[name='temaPosEmp']").attr("name", "[" + i + "].Tematicas");
            $(element).find("input[name='tipoTemEmp']").attr("name", "[" + i + "].TipoTematica");
            i++;
        });
        var temaPosAenviar = $("#seleccionadas_tematicas").find("td[name='tdtema']");
        temaPosAenviar.each(function (ind, element) {
            $(element).find("input[name='idtePos']").attr("name", "[" + i + "].Id_Tematica");
            $(element).find("input[name='temaPos']").attr("name", "[" + i+ "].Tematicas");
            $(element).find("input[name='tipoTem']").attr("name", "[" + i + "].TipoTematica");
            i++;
        });
        $("#competencia").submit();
    });
        //$('#datosCampos').append(
        //    '<input value = "' + $("#Fk_Id_Rol").val() + '">' +
        //    '<input value = "' + $("#Fk_Id_Cargo").val() + '">' +
        //    '<input value = "' + $("#Fk_id_Tematica").val() + '">' +
        //    '<input value = "' + $("#Fk_id_Tematica2").val() + '">'+
        //    '<input value = "' + temaPosAenviar + '">'
        //    )

        
    //}
}

function paginador(idTabla, nameTr, pagination) {
    jQuery(function ($) {
        // Consider adding an ID to your table
        // incase a second table ever enters the picture.
        var items = $(idTabla).find(nameTr);
        var numItems = items.length;
        var perPage = 10;

        // Only show the first 10 (or first `per_page`) items initially.
        items.slice(perPage).hide();

        // Now setup the pagination using the `.pagination-page` div.
        $(pagination).pagination({
            prevText: '<i class="fa fa-chevron-left"><i class="fa fa-chevron-left">',
            nextText: '<i class="fa fa-chevron-right"><i class="fa fa-chevron-right">',
            items: numItems,
            itemsOnPage: perPage,
            cssStyle: "compact-theme",

            // This is the actual page changing functionality.
            onPageClick: function (pageNumber) {
                // We need to show and hide `tr`s appropriately.
                var showFrom = perPage * (pageNumber - 1);
                var showTo = showFrom + perPage;

                // We'll first hide everything...
                items.hide()
                     // ... and then only show the appropriate rows.
                     .slice(showFrom, showTo).show();
            }
        });

        // EDIT: Let's cover URL fragments (i.e. #page-3 in the URL).
        // More thoroughly explained (including the regular expression) in:
        // https://github.com/bilalakil/bin/tree/master/simplepagination/page-fragment/index.html

        // We'll create a function to check the URL fragment
        // and trigger a change of page accordingly.
        function checkFragment() {
            // If there's no hash, treat it like page 1.
            var hash = window.location.hash || "#page-1";

            // We'll use a regular expression to check the hash string.
            //hash = hash.match(/^#page-(\d+)$/);
            //if (hash) {
            //    // The `selectPage` function is described in the documentation.
            //    // We've captured the page number in a regex group: `(\d+)`.
            //    $(pagination).pagination("selectPage", parseInt(hash[1]));
            //}
        };

        // We'll call this function whenever back/forward is pressed...
        $(window).bind("popstate", checkFragment);

        // ... and we'll also call it when the page has loaded
        // (which is right now).
        checkFragment();
    });
}

function BusquedaCompetencia() {
    var IdBusqueda = $("#IdTematicaComp").val()
    var rol = $("#Fk_Id_Rol").val()
    var cargo = $("#Fk_Id_Cargo").val()
    var dsBusqueda = $("#BuscarCompetencia").val()
    if (IdBusqueda == "" && dsBusqueda != "") {
        swal(
        'Estimado Usuario',
        'La tématica ingresada no existe',
        'error'
        )
        $("#BuscarCompetencia").val("");
        }
    else if (rol != "" && (IdBusqueda == "" || dsBusqueda == "") && cargo == "")
    {
        $.ajax({
            url: urlBase + '/Competencia/BuscarCompetenciaPorRol',
            data: {
                rol: rol              
            },
            type: 'POST',
            success: function (result) {
                utils.closeLoading();
                if (result) {
                    $("#TablaTematica").html(result);
                }
                else
                {
                    $("#TablaTematica").html("");
                    swal(
                    'Estimado Usuario',
                    'Este rol no tiene asignado ninguna competencia',
                    'warning'
                    )
                }
            }
        });
    }
    else if (cargo != "" && (IdBusqueda == "" || dsBusqueda == "") && rol == "") {
        $.ajax({
            url: urlBase + '/Competencia/BuscarCompetenciaPorCargo',
            data: {
                cargo: cargo
            },
            type: 'POST',
            success: function (result) {
                utils.closeLoading();
                if (result) {
                    $("#TablaTematica").html(result);
                }
                else {
                    $("#TablaTematica").html("");
                    swal(
                    'Estimado Usuario',
                    'Este cargo no tiene asignado ninguna competencia',
                    'warning'
                    )
                }
            }
        });
    }
    else if (cargo != "" && (IdBusqueda == "" || dsBusqueda == "") && rol != "") {
        $.ajax({
            url: urlBase + '/Competencia/BuscarCompetenciaPorRolCargo',
            data: {
                rol: rol,
                cargo: cargo
            },
            type: 'POST',
            success: function (result) {
                utils.closeLoading();
                if (result) {
                    $("#TablaTematica").html(result);
                }
                else {
                    $("#TablaTematica").html("");
                    swal(
                    'Estimado Usuario',
                    'Algunos de los campos no coinciden con la búsqueda',
                    'warning'
                    )
                }
            }
        });
    }
    else if (IdBusqueda != "" && dsBusqueda != "" && rol == "" && cargo == "")
    {
        $.ajax({
            url: urlBase + '/Competencia/BuscarCompetenciaPorTema',
            data: {
                idbusqueda: IdBusqueda
            },
            type: 'POST',
            success: function (result) {
                utils.closeLoading();
                if (result) {
                    $("#TablaTematica").html(result);
                }
                else {
                    $("#TablaTematica").html("");
                    swal(
                    'Estimado Usuario',
                    'Esa temática no se ha asignado a ninguna competencia',
                    'warning'
                    )
                }
            }
        });
    } 
    else if (IdBusqueda != "" && dsBusqueda != "" && rol != "" && cargo == "") {
        $.ajax({
            url: urlBase + '/Competencia/BuscarCompetenciaPorTemaYRol',
            data: {
                idbusqueda: IdBusqueda,
                rol: rol
            },
            type: 'POST',
            success: function (result) {
                utils.closeLoading();
                if (result) {
                    $("#TablaTematica").html(result);
                    $("#IdTematicaComp").val()= IdBusqueda 
                    $("#BuscarCompetencia").val() = dsBusqueda;
                }
                else {
                    $("#TablaTematica").html("");
                    swal(
                    'Estimado Usuario',
                    'Algunos de los campos no coinciden con la búsqueda',
                    'warning'
                    )
                }
            }
        });
    }
    else if (IdBusqueda != "" && dsBusqueda != "" && rol == "" && cargo != "") {
        $.ajax({
            url: urlBase + '/Competencia/BuscarCompetenciaPorTemaYCargo',
            data: {
                idbusqueda: IdBusqueda,
                cargo: cargo
            },
            type: 'POST',
            success: function (result) {
                utils.closeLoading();
                if (result) {
                    $("#TablaTematica").html(result);
                    $("#IdTematicaComp").val() = IdBusqueda
                    $("#BuscarCompetencia").val() = dsBusqueda;
                }
                else {
                    $("#TablaTematica").html("");
                    swal(
                    'Estimado Usuario',
                    'Algunos de los campos no coinciden con la búsqueda',
                    'warning'
                    )
                }
            }
        });
    }
    else if (IdBusqueda != "" && dsBusqueda != "" && rol != "" && cargo != "") {
        $.ajax({
            url: urlBase + '/Competencia/BuscarCompetenciaPorTemaRolCargo',
            data: {
                idbusqueda: IdBusqueda,
                rol: rol,
                cargo: cargo
            },
            type: 'POST',
            success: function (result) {
                utils.closeLoading();
                if (result) {
                    $("#TablaTematica").html(result);
                    $("#IdTematicaComp").val() = IdBusquedaMostrarTematicaPDF
                    $("#BuscarCompetencia").val() = dsBusqueda;
                }
                else {
                    $("#TablaTematica").html("");
                    swal(
                    'Estimado Usuario',
                    'Algunos de los campos no coinciden con la búsqueda',
                    'warning'
                    )
                }
            }
        });
    }
    else if (IdBusqueda == "" && dsBusqueda == "" && rol == "" && cargo == "")
    {
        swal(
        'Estimado Usuario',
        'Es necesario seleccionar al menos un campo para realizar la búsqueda',
        'warning'
        )
    }
}

//Autocompletar con JQuery
function buscarcompetencia() {
    $("#BuscarCompetencia").autocomplete({
        minLength: 2,
        source: function (request, response) {
            $.ajax({
                url: urlBase + '/Competencia/BuscarCompetenciaPorTematica',
                type: "POST",
                dataType: "json",
                data: { prefijo: request.term },
            }).done(function (data) {
                response($.map(data, function (item) {
                    return { label: item.Descripcio_Tematicas, value: item.Id_Tematica };
                }))
            })
        },
        focus: function (event, ui) {
            event.preventDefault();
            $(this).val(ui.item.label);
        },
        select: function (event, ui) {
            event.preventDefault();
            $(this).val(ui.item.label);
            $("#IdTematicaComp").val(ui.item.value);
        }
    });
}

function SeleccionarTodosRol() {
    if ($("#checkRol").prop("checked")) {
        $("#Fk_Id_Rol").find("option").each(function (ind, element) {
            if ($(element).val() != "") {
                $(element).attr('selected', true);
            }
        });
    }
    else {
        $("#Fk_Id_Rol").find("option").each(function (ind, element) {
                $(element).attr('selected', false);
        });
    }       
}

function SeleccionarTodosCargo() {
    if ($("#checkCargo").prop("checked")) {
        $("#Fk_Id_Cargo").find("option").each(function (ind, element) {
            if ($(element).val() != "") {
                $(element).attr('selected', true);
            }
        });
        ObtenerSiarpAfiliados();
    }
    else {
        $("#Fk_Id_Cargo").find("option").each(function (ind, element) {
            $(element).attr('selected', false);
        });
        ObtenerSiarpAfiliados();
    }
}

function SeleccionarTodosEmpleados() {
    if ($("#checkEmpleado").prop("checked")) {
        $('input[name=list]').each(function (ind, element) {
            //$(element).attr('checked', true);
            $(element).click();
        });
    }
    else {
        $('input[name=list]').each(function (ind, element) {
            //$(element).attr('checked', false);
            $(element).click();
        });       
    }
}

function SeleccionarTodosTematica() {
    if ($("#checkTem").prop("checked")) {
        var cabecera = true;
        $("#seleccionadas_tematicas tr").each(function (ind, element) {
            if (cabecera)
                cabecera = false
            else
                DesmarcarTematica(this, $(element).attr('id'))
        })
        cabecera= true;
        $("#select_tematica tr").each(function (ind, element) {
            if (cabecera)
                cabecera = false
            else
                AgregarASeleccionadas(this, $(element).attr('id'))
        });
        SelTodosTematica = true;
    }
    else {
        var cabecera = true;
        $("#seleccionadas_tematicas tr").each(function (ind, element) {
            if (cabecera)
                cabecera = false
            else
                DesmarcarTematica(this, $(element).attr('id'))
        });
        SelTodosTematica = false;
    }
    paginador("#seleccionadas_tematicas", "tr[name = tem]", "#paginador3");
}

function SeleccionarTodosTematicaEmp() {
    if ($("#checkTemEmp").prop("checked")) {
        var cabecera = true;
        $("#seleccionadas_tematicasEmp tr").each(function (ind, element) {
            if (cabecera)
                cabecera = false
            else
                DesmarcarTematicaEmp(this, $(element).attr('id'))
        })
        cabecera = true;
        $("#select_tematicaEmp tr").each(function (ind, element) {
            if (cabecera)
                cabecera = false
            else
                AgregarASeleccionadasEmp(this, $(element).attr('id'))
        });
        SelTodosTematicaEmp = true;
    }
    else {
        var cabecera = true;
        $("#seleccionadas_tematicasEmp tr").each(function (ind, element) {
            if (cabecera)
                cabecera = false
            else
                DesmarcarTematicaEmp(this, $(element).attr('id'))
        });
        SelTodosTematicaEmp = false;
    }
    paginador("#seleccionadas_tematicasEmp", "tr[name = temEmp]", "#paginador4");
}

function adicionarEmpleado(id) {
    var index = addedTrabajador.indexOf(id);
    if (index>-1)
        addedTrabajador.splice(index, 1);
    else {
        addedTrabajador.push(id);
    }

}
