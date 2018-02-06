
var urlBase = ""
try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};

$("#infosede").hide();
$("#labelinfosede").hide();

//renderiza en un input tipo text un datapicker para
//la selección de fechas
$(function () {

    ConstruirDatePickerPorElemento('Fecha');
    ConstruirDatePickerPorElemento('FechaProbable');

});

//Funcion que obtiene la informacion de la sede seleccionada por empresa.
$("#btncontcopasst").hide();
function BuscarInformacionSede() {
    $idSede = $("#idSede");
    PopupPosition();
    $.ajax({
        url: urlBase + '/Comites/BuscarInformacionSede',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            IdSede: $idSede.val()
        },
        type: 'POST',
        success: function (result) {
            if (result.Data) {
                $("#infosede").show("toogle");
                $("#labelinfosede").show("toogle");
                $("#btncontcopasst").show("toogle");
                var nomempresa = (result.Data.NombreEmpresa);
                $("#NombreEmpresaVM").val(nomempresa);
                var nomsede = (result.Data.NombreSede);
                $("#NombreSedeVM").val(nomsede);
                var nitempresa = (result.Data.IDEmpresa);
                $("#NitEmpresaVM").val(nitempresa);
                var direccionsede = (result.Data.DireccionSede);
                $("#DireccionSedeVM").val(direccionsede);
                OcultarPopupposition();
            }
        },
        error: function (result) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Seleccione una Sede de la lista para continuar con el Comité Copasst',
                confirmButtonColor: '#7E8A97',
                confirmButtonText: "Aceptar",
            });
            OcultarPopupposition();
        }
    });
}

//Funcion que obtiene la informacion de la sede seleccionada por empresa.
$("#btncontconv").hide();
function BuscarInformacionSedeconv() {
    $idSede = $("#idSede");
    PopupPosition();
    $.ajax({
        url: urlBase + '/Comites/BuscarInformacionSede',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            IdSede: $idSede.val()
        },
        type: 'POST',
        success: function (result) {
            if (result.Data) {
                $("#infosede").show("toogle");
                $("#labelinfosede").show("toogle");
                $("#btncontconv").show("slow");
                var nomempresa = (result.Data.NombreEmpresa);
                $("#NombreEmpresaVM").val(nomempresa);
                var nomsede = (result.Data.NombreSede);
                $("#NombreSedeVM").val(nomsede);
                var nitempresa = (result.Data.IDEmpresa);
                $("#NitEmpresaVM").val(nitempresa);
                var direccionsede = (result.Data.DireccionSede);
                $("#DireccionSedeVM").val(direccionsede);
                OcultarPopupposition();
            }
        },
        error: function (result) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Seleccione una Sede de la lista para continuar con el Comité de Convivencia',
                confirmButtonColor: '#7E8A97',
                confirmButtonText: "Aceptar",
            });
            OcultarPopupposition();
        }
    });
}

function paginador(idTabla, nameTr, pagination) {
    jQuery(function ($) {
        //language: {
        //        paginate: {
        //            next: '&#8594;'; // or '→'
        //            previous: '&#8592;' // or '←' 
        //        }
        //}
        // Consider adding an ID to your table
        // incase a second table ever enters the picture.
        var items = $(idTabla).find(nameTr);
        var numItems = items.length;
        var perPage = 5;

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

$("#divTmiebro").hide();
$("#Numero_Documento").empty();
$("#Nombre").empty();

// ELIMINAR MIEMBRO ACTA COPASST
function EliminarMiembrosCopasst(id_Acta, documento) {
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar el Registro?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    }, (function (isConfirm) {
        if (isConfirm) {
            PopupPosition();
            $.ajax({
                url: urlBase + '/Comites/eliminarMiembroCopasst',
                data: {
                    FK_Id_Acta: id_Acta,
                    Documento: documento
                },
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    if (result.Mensaje == "OK") {
                        swal(
                                  'Estimado Usuario',
                                  'Su registro ha sido eliminado satisfactoriamente',
                                  'success'
                        );
                        $("#tMiembros11 td").parent().remove();
                        $("#divTmiebro").trigger("reset");
                        $('#tMiembros').empty();
                        $('#tMiembros').append
                        //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                        var contador = 0;
                        $.each(result.Data, function (ind, element) {
                            var TipoPrincipal = element.Des_TipoPrincipal;
                            if (TipoPrincipal == null)
                                TipoPrincipal = "";
                            var elemento = '<tr name="trMiembros">' +
                                            '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="hidden" name="Numero_Documento" id="Numero_Documento' + contador + '" value="' + element.Numero_Documento + '">' + element.Numero_Documento + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; vertical-align:middle"><input type="hidden" name="Nombre" id="Nombre' + contador + '"value="' + element.Nombre + '">' + element.Nombre + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; vertical-align:middle"><input type="hidden" name="Des_TipoPrioridadMiembro" id ="Des_TipoPrioridadMiembro' + contador + '"value="' + element.Des_TipoPrioridadMiembro + '">' + element.Des_TipoPrioridadMiembro + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; vertical-align:middle"><input type="hidden" name="TipoRepresentante" id ="TipoRepresentante' + contador + '"value="' + element.TipoRepresentante + '">' + element.TipoRepresentante + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; vertical-align:middle"><input type="hidden" name="Des_TipoPrincipal" id ="Des_TipoPrincipal' + contador + '"value="' + TipoPrincipal + '">' + TipoPrincipal + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><a id="botonBorrar" onclick="EliminarMiembrosCopasst(' + element.PK_Id_Acta + ',' + element.Numero_Documento + ')" class="btn btn-search btn-md" title="Eliminar"><span class="glyphicon glyphicon-erase"></span></a></td>' +
                                            //'<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a data-toggle="modal" data-target="#@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" value="@SG_SST.Recursos.GeneralApp.General.btn_Eliminar" class="btn btn-search"><span class="glyphicon glyphicon-erase"></span></a><div id="@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" class=" modal fade" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header encabezado" style="background-color:transparent; border-bottom:none"><button type="button" class="close" data-dismiss="modal" aria-label="Close"></button><h4 class="modal-title title">Eliminar Miembro Acta</h4></div><div class="modal-body" style="color:black"><center><p>¿Seguro desea eliminar el Registro?</p></center></div><div class="modal-footer">@Html.ActionLink(SG_SST.Recursos.GeneralApp.General.btn_Eliminar, "DeleteConfirmed", new { id = ' + element.Numero_Documento + ' },htmlAttributes: new { @type = "button", @class = "boton botonactive" })<button type="button" class="boton botoncancel" data-dismiss="modal">Cancelar</button></div></div></div></div></td>' +
                                            '</tr></table>'
                            $('#tMiembros').append(elemento)
                            contador = contador + 1;
                        })

                        paginador("#tMiembros", "tr[name = trMiembros]", "#paginadorMiembros")
                    }
                    else {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó un error. Por favor, intente mas tarde',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó un error. Por favor, intente mas tarde',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }))

}

// ELIMINAR PARTICIPANTE ACTA COPASST
function EliminarParticipanteCopasst(id_Acta, documento) {
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar el Registro?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    }, (function (isConfirm) {
        if (isConfirm) {
            PopupPosition();
            $.ajax({
                url: urlBase + '/Comites/eliminarParticipanteCopasst',
                data: {
                    FK_Id_Acta: id_Acta,
                    Documento: documento
                },
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    if (result.Mensaje == "OK") {
                        swal(
                            'Estimado Usuario',
                            'Su registro ha sido eliminado satisfactoriamente',
                            'success'
                        );
                        $("#tParticipantes td").parent().remove();
                        $("#Numero_Documento").val("");
                        $("#Nombre").val("");
                        $("#divTmiebro").show("toogle");
                        $("#divTmiebro").trigger("reset");
                        $('#tbParticipantes').empty();
                        $('#tbParticipantes').append
                        //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                        var contador = 0;
                        $.each(result.Data, function (ind, element) {
                            var elemento = '<tr name="trParticipante">' +
                                            '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="hidden" name="Numero_Documento" id="Numero_Documento' + contador + '" value="' + element.Numero_Documento + '">' + element.Numero_Documento + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; vertical-align:middle"><input type="hidden" name="Nombre" id="Nombre' + contador + '"value="' + element.Nombre + '">' + element.Nombre + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><a id="botonBorrar" onclick="EliminarParticipanteCopasst(' + element.PK_Id_Acta + ',' + element.Numero_Documento + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon glyphicon-erase"></span></a></td>' +
                                            //'<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a data-toggle="modal" data-target="#@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" value="@SG_SST.Recursos.GeneralApp.General.btn_Eliminar" class="btn btn-search"><span class="glyphicon glyphicon-erase"></span></a><div id="@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" class=" modal fade" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header encabezado" style="background-color:transparent; border-bottom:none"><button type="button" class="close" data-dismiss="modal" aria-label="Close"></button><h4 class="modal-title title">Eliminar Miembro Acta</h4></div><div class="modal-body" style="color:black"><center><p>¿Seguro desea eliminar el Registro?</p></center></div><div class="modal-footer">@Html.ActionLink(SG_SST.Recursos.GeneralApp.General.btn_Eliminar, "DeleteConfirmed", new { id = ' + element.Numero_Documento + ' },htmlAttributes: new { @type = "button", @class = "boton botonactive" })<button type="button" class="boton botoncancel" data-dismiss="modal">Cancelar</button></div></div></div></div></td>' +
                                            '</tr></table>'
                            $('#tbParticipantes').append(elemento)
                            contador = contador + 1;
                        })

                        paginador("#tbParticipantes", "tr[name = trParticipante]", "#paginadorParticipantes")
                    }
                    else {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó un error. Por favor, intente mas tarde',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó un error. Por favor, intente mas tarde',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }))

}

//ADICIONAR MIEMBROS COPASST
function CamposAdicionar() {
    ValidarAdicionarMiembro();
    if ($("#CrearActa").valid()) {
        PopupPosition();
        var Documento = $("#Numero_Documento").val();
        var Nombre = $("#Nombre").val();
        var Prioridad = $("#TiposPrioridadMiembros").val();
        var Representa = $("#Representa").val();
        var Principal = $("#Principal").val();
        var IdSede = $("#IdSede").val();
        var ConsecutivoActa = $("#Consecutivo_Acta").val();
        if (Nombre.length == 0) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe ingresar la información del Asistente',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
        else {
            if (Prioridad.length == 0) {
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'Debe ingresar una prioridad',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
            else {
                if (Representa.length == 0) {
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'Debe ingresar a quien representa',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
                else {
                    var Miembro = {
                        Numero_Documento: Documento,
                        Nombre: Nombre,
                        Fk_Id_TipoPrioridadMiembro: Prioridad,
                        TipoRepresentante: Representa,
                        Fk_Id_TipoPrincipal: Principal,
                        Fk_Id_Sede: IdSede,
                        Consecutivo_Acta: ConsecutivoActa,
                    }
                    $.ajax({
                        url: urlBase + '/Comites/GuardarMiembro',
                        data: Miembro,
                        type: 'POST',
                        success: function (result) {
                            $('#Consecutivo_Acta').val(result.Data.Consecutivo_Acta);
                            $('#PK_Id_Acta').val(result.Data.PK_Id_Acta);
                            $.ajax({
                                url: urlBase + '/Comites/MiembroActaCopasst',
                                data: result.Data,
                                type: 'POST',
                                success: function (result) {
                                    OcultarPopupposition();
                                    $("#tMiembros11 td").parent().remove();
                                    $("#Numero_Documento").val("");
                                    $("#Nombre").val("");
                                    $("#TiposPrioridadMiembros").val("");
                                    $("#Representa").val("");
                                    $("#Principal").val("");
                                    $("#divTmiebro").show("toogle");
                                    $("#divTmiebro").trigger("reset");
                                    $('#tMiembros').empty();
                                    //$('#tMiembros').append
                                    //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                                    var contador = 0;
                                    $.each(result.Data, function (ind, element) {
                                        var TipoPrincipal = element.Des_TipoPrincipal;
                                        if (TipoPrincipal == null)
                                            TipoPrincipal = "";
                                        var elemento = '<tr name="trMiembros">' +
                                                        '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="hidden" name="Numero_Documento" id="Numero_Documento' + contador + '" value="' + element.Numero_Documento + '">' + element.Numero_Documento + '</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; vertical-align:middle"><input type="hidden" name="Nombre" id="Nombre' + contador + '"value="' + element.Nombre + '">' + element.Nombre + '</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; vertical-align:middle"><input type="hidden" name="Des_TipoPrioridadMiembro" id ="Des_TipoPrioridadMiembro' + contador + '"value="' + element.Des_TipoPrioridadMiembro + '">' + element.Des_TipoPrioridadMiembro + '</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; vertical-align:middle"><input type="hidden" name="TipoRepresentante" id ="TipoRepresentante' + contador + '"value="' + element.TipoRepresentante + '">' + element.TipoRepresentante + '</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; vertical-align:middle"><input type="hidden" name="Des_TipoPrincipal" id ="Des_TipoPrincipal' + contador + '"value="' + TipoPrincipal + '">' + TipoPrincipal + '</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><a id="botonBorrar" onclick="EliminarMiembrosCopasst(' + element.PK_Id_Acta + ',' + element.Numero_Documento + ')" class="btn btn-search btn-md" title="Eliminar"><span class="glyphicon glyphicon-erase"></span></a></td>' +
                                                        //'<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a data-toggle="modal" data-target="#@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" value="@SG_SST.Recursos.GeneralApp.General.btn_Eliminar" class="btn btn-search"><span class="glyphicon glyphicon-erase"></span></a><div id="@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" class=" modal fade" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header encabezado" style="background-color:transparent; border-bottom:none"><button type="button" class="close" data-dismiss="modal" aria-label="Close"></button><h4 class="modal-title title">Eliminar Miembro Acta</h4></div><div class="modal-body" style="color:black"><center><p>¿Seguro desea eliminar el Registro?</p></center></div><div class="modal-footer">@Html.ActionLink(SG_SST.Recursos.GeneralApp.General.btn_Eliminar, "DeleteConfirmed", new { id = ' + element.Numero_Documento + ' },htmlAttributes: new { @type = "button", @class = "boton botonactive" })<button type="button" class="boton botoncancel" data-dismiss="modal">Cancelar</button></div></div></div></div></td>' +
                                                        '</tr></table>'
                                        $('#tMiembros').append(elemento)
                                        contador = contador + 1;
                                    })

                                    paginador("#tMiembros", "tr[name = trMiembros]", "#paginadorMiembros")

                                },
                                error: function (result) {
                                    swal({
                                        type: 'error',
                                        title: 'Estimado Usuario',
                                        text: 'Se presentó un error. Por favor, intente mas tarde',
                                        confirmButtonColor: '#7E8A97'
                                    });
                                    OcultarPopupposition();
                                }
                            });
                        },
                        error: function (result) {
                            swal({
                                type: 'error',
                                title: 'Estimado Usuario',
                                text: 'Se presentó un error. Por favor, intente mas tarde',
                                confirmButtonColor: '#7E8A97'
                            });
                            OcultarPopupposition();
                        }
                    });
                }
            }
        }
    }
}

//ADICIONAR PARTICIPANTES ACTA COPASST
function AdicionarPaticipante() {
    //ValidarAdicionarParticipante();
    if ($("#CrearActa").valid()) {
        PopupPosition();
        var Documento = $("#Numero_Documento").val();
        var Nombre = $("#Nombre").val();
        var PK_Id_Acta = $("#PK_Id_Acta").val();
        var Consecutivo_Acta = $("#Consecutivo_Acta").val();
        var Fk_Id_Sede = $("#Fk_Id_Sede").val();
        if (Nombre.length == 0 || Documento.length == 0) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe ingresar el Documento y el Nombre del Participante',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
        else {
            var Miembro = {
                Numero_Documento: Documento,
                Nombre: Nombre,
                PK_Id_Acta: PK_Id_Acta,
                Consecutivo_Acta: Consecutivo_Acta,
                Fk_Id_Sede: Fk_Id_Sede,
            }
            $.ajax({
                url: urlBase + '/Comites/GuardarParticipante',
                data: Miembro,
                type: 'POST',
                success: function (result) {
                OcultarPopupposition();
                $("#tParticipantes td").parent().remove();
                $("#Numero_Documento").val("");
                $("#Nombre").val("");
                $("#divTmiebro").show("toogle");
                $("#divTmiebro").trigger("reset");
                $('#tbParticipantes').empty();
                $('#tbParticipantes').append
                //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                var contador = 0;
                $.each(result.Data, function (ind, element) {
                    var elemento = '<tr name="trParticipante">' +
                                    '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><input type="hidden" name="Numero_Documento" id="Numero_Documento' + contador + '" value="' + element.Numero_Documento + '">' + element.Numero_Documento + '</td>' +
                                    '<td style="border-right: 2px solid lightslategray; vertical-align:middle"><input type="hidden" name="Nombre" id="Nombre' + contador + '"value="' + element.Nombre + '">' + element.Nombre + '</td>' +
                                    '<td style="border-right: 2px solid lightslategray; vertical-align:middle; text-align:center"><a id="botonBorrar" onclick="EliminarParticipanteCopasst(' + element.PK_Id_Acta + ',' + element.Numero_Documento + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                    //'<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a data-toggle="modal" data-target="#@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" value="@SG_SST.Recursos.GeneralApp.General.btn_Eliminar" class="btn btn-search"><span class="glyphicon glyphicon-erase"></span></a><div id="@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" class=" modal fade" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header encabezado" style="background-color:transparent; border-bottom:none"><button type="button" class="close" data-dismiss="modal" aria-label="Close"></button><h4 class="modal-title title">Eliminar Miembro Acta</h4></div><div class="modal-body" style="color:black"><center><p>¿Seguro desea eliminar el Registro?</p></center></div><div class="modal-footer">@Html.ActionLink(SG_SST.Recursos.GeneralApp.General.btn_Eliminar, "DeleteConfirmed", new { id = ' + element.Numero_Documento + ' },htmlAttributes: new { @type = "button", @class = "boton botonactive" })<button type="button" class="boton botoncancel" data-dismiss="modal">Cancelar</button></div></div></div></div></td>' +
                                    '</tr></table>'
                    $('#tbParticipantes').append(elemento)
                    contador = contador + 1;
                })

                paginador("#tbParticipantes", "tr[name = trParticipante]", "#paginadorParticipantes")

            },
            error: function (result) {
            swal({
                type: 'error',
                title: 'Estimado Usuario',
                text: 'Se presentó un error. Por favor, intente mas tarde',
                confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
             }
        });
      }
   }
}

//ADICIONAR TEMAS ACTA COPASST
function AdicionarTemas() {
    //ValidarAdicionarTemas();
    if ($("#CrearActa").valid()) {
        PopupPosition();
        var vTema = $("#TemaOrdenDia").val();
        var PK_Id_Acta = $("#PK_Id_Acta").val();
        var Consecutivo_Acta = $("#Consecutivo_Acta").val();
        var Fk_Id_Sede = $("#Fk_Id_Sede").val();
        if (vTema.length == 0) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe ingresar el Tema',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
        else {
            var Miembro = {
                TemaOrdenDia : vTema,
                PK_Id_Acta: PK_Id_Acta,
                Consecutivo_Acta: Consecutivo_Acta,
                Fk_Id_Sede: Fk_Id_Sede,
            }
            $.ajax({
                url: urlBase + '/Comites/GuardarTema',
                data: Miembro,
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    $("#tTemas td").parent().remove();
                    $("#TemaOrdenDia").val("");
                    $("#divTmiebro").show("toogle");
                    $("#divTmiebro").trigger("reset");
                    $('#tbTemas').empty();
                    $('#tbTemas').append
                    //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                    var contador = 0;
                    var contador1 = 1;
                    $.each(result.Data, function (ind, element) {
                        var elemento = '<tr name="trOrdenDia">' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="Contador1' + contador + '" value="' + contador1 + '">' + contador1 + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Nombre" id="Tema' + contador + '"value="' + element.Tema + '">' + element.Tema + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarTemaCopasst(' + element.PK_Id_Acta + ',' + element.PK_Id_TemaActa + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                        '</tr></table>'
                        $('#tbTemas').append(elemento)
                        contador = contador + 1;
                        contador1 = contador1 + 1;
                    })
                    paginador("#tbTemas", "tr[name = trOrdenDia]", "#paginadorTemas");

                    //$("#ListaTema").empty();
                    //$("#ListaTema").append("<option value=\"\">--Seleccionar Tema--</option>");

                    //$.each(result.Data, function (key, value) {
                    //    $("#ListaTema").append("<option value=\"" + value.PK_Id_TemaActa + "\">" + value.Tema + "</option>");
                    //});
                    $("#tObservaciones td").parent().remove();
                    $('#tbObservacion').empty();
                    $("#divTmiebro").show("toogle");
                    $("#divTmiebro").trigger("reset");
                    $('#tbObservacion').append
                    var contador2 = 0;
                    var contador3 = 1;
                    $.each(result.Data, function (ind, element) {
                        var observacion = element.Observaciones;
                        if (observacion == null)
                            observacion = "";
                        var elemento = '<tr name="trObservacion">' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="contador3' + contador2 + '" value="' + contador3 + '">' + contador3 + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Tema" id="Tema' + contador2 + '"value="' + element.Tema + '">' + element.Tema + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Observaciones" id="Observaciones' + contador2 + '"value="' + observacion + '">' + observacion + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="ActualizarObservacionTemaCopasst(' + element.PK_Id_Acta + ',' + element.PK_Id_TemaActa + ')" title="Actualizar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-refresh"></span></a></td>' +
                                        '</tr></table>'
                        $('#tbObservacion').append(elemento)
                        contador2 = contador2 + 1;
                        contador3 = contador3 + 1;
                    })
                    paginador("#tbObservacion", "tr[name = trObservacion]", "#paginadorObservacion");

                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó un error. Por favor, intente mas tarde',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }
}

//ELIMINAR TEMAS
function EliminarTemaCopasst(id_Acta, PK_Id_TemaActa) {
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar el Registro?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    }, (function (isConfirm) {
        if (isConfirm) {
            PopupPosition();
            $.ajax({
                url: urlBase + '/Comites/eliminarTemaCopasst',
                data: {
                    FK_Id_Acta: id_Acta,
                    PK_Id_TemaActa: PK_Id_TemaActa
                },
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    if (result.Mensaje == "OK") {
                        swal(
                                  'Estimado Usuario',
                                  'Su registro ha sido eliminado satisfactoriamente',
                                  'success'
                        );
                        $("#tTemas td").parent().remove();
                        $("#Tema").val("");
                        $("#divTmiebro").show("toogle");
                        $("#divTmiebro").trigger("reset");
                        $('#tbTemas').empty();
                        $('#tbTemas').append
                        //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                        var contador = 0;
                        var contador1 = 1;
                        $.each(result.Data, function (ind, element) {
                            var elemento = '<tr name="trOrdenDia">' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="Contador1' + contador + '" value="' + contador1 + '">' + contador1 + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Nombre" id="Tema' + contador + '"value="' + element.Tema + '">' + element.Tema + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarTemaCopasst(' + element.PK_Id_Acta + ',' + element.PK_Id_TemaActa + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                            '</tr></table>'
                            $('#tbTemas').append(elemento)
                            contador = contador + 1;
                            contador1 = contador1 + 1;
                        })
                        paginador("#tbTemas", "tr[name = trOrdenDia]", "#paginadorTemas");

                        $("#tObservaciones td").parent().remove();
                        $('#tbObservacion').empty();
                        $("#divTmiebro").show("toogle");
                        $("#divTmiebro").trigger("reset");
                        $('#tbObservacion').append
                        var contador2 = 0;
                        var contador3 = 1;
                        $.each(result.Data, function (ind, element) {
                            var observacion = element.Observaciones;
                            if (observacion == null)
                                observacion = "";
                            var elemento = '<tr name="trObservacion">' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="contador3' + contador2 + '" value="' + contador3 + '">' + contador3 + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Tema" id="Tema' + contador2 + '"value="' + element.Tema + '">' + element.Tema + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Observaciones" id="Observaciones' + contador2 + '"value="' + observacion + '">' + observacion + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="ActualizarObservacionTemaCopasst(' + element.PK_Id_Acta + ',' + element.PK_Id_TemaActa + ')" title="Actualizar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-refresh"></span></a></td>' +
                                            '</tr></table>'
                            $('#tbObservacion').append(elemento)
                            contador2 = contador2 + 1;
                            contador3 = contador3 + 1;
                        })
                        paginador("#tbObservacion", "tr[name = trObservacion]", "#paginadorObservacion");
                    }
                    else {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó, un error Intente mas tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó, un error Intente mas tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }))

}

//ELIMINAR ACCIONES
function EliminarAccionCopasst(id_Acta, Pk_Id_AccionActaCopasst) {
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar el Registro?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    }, (function (isConfirm) {
        if (isConfirm) {
            PopupPosition();
            $.ajax({
                url: urlBase + '/Comites/eliminarAccionCopasst',
                data: {
                    FK_Id_Acta: id_Acta,
                    Pk_Id_AccionActaCopasst: Pk_Id_AccionActaCopasst
                },
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    if (result.Mensaje == "OK") {
                        swal(
                                  'Estimado Usuario',
                                  'Su registro ha sido eliminado',
                                  'success'
                        );
                        $("#tAcciones td").parent().remove();
                        $("#AccionARealizar").val("");
                        $("#Responsable").val("");
                        $("#divTmiebro").show("toogle");
                        $("#divTmiebro").trigger("reset");
                        $('#tbAccion').empty();
                        $('#tbAccion').append
                        //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                        var contador = 0;
                        var contador1 = 1;
                        $.each(result.Data, function (ind, element) {
                            var Fecha = moment(result.Data[ind].FechaProbable).format("DD/MM/YYYY");
                            var elemento = '<tr name="trAcciones">' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="Contador1' + contador + '" value="' + contador1 + '">' + contador1 + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="AccionARealizar" id="AccionARealizar' + contador + '"value="' + element.AccionARealizar + '">' + element.AccionARealizar + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Responsable" id="Responsable' + contador + '"value="' + element.Responsable + '">' + element.Responsable + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="FechaProbable" id="FechaProbable' + contador + '"value="' + Fecha + '">' + Fecha + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarAccionCopasst(' + element.PK_Id_Acta + ',' + element.Pk_Id_AccionActaCopasst + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                            '</tr></table>'
                            $('#tbAccion').append(elemento)
                            contador = contador + 1;
                            contador1 = contador1 + 1;
                        })
                        paginador("#tbAccion", "tr[name = trAcciones]", "#paginadorAcciones");
                    }
                    else {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó, un error Intente mas tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó, un error Intente mas tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }))

}

//ADICIONAR ACCIONES ACTA COPASST
function AdicionarAcciones() {
    //ValidarAdicionarAccion();
    if ($("#CrearActa").valid()) {
        PopupPosition();
        var AccionARealizar = $("#AccionARealizar").val();
        var Responsable = $("#Responsable").val();
        var Fecha = $("#FechaProbable").val();
        
        var hoy = new Date();
        dia = hoy.getDate();
        mes = hoy.getMonth() + 1;
        anio = hoy.getFullYear();
        if (mes < 10) {
            fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
        } else {
            fecha_actual = String(dia + "/" + mes + "/" + anio);
        }
        var PK_Id_Acta = $("#PK_Id_Acta").val();
        var Consecutivo_Acta = $("#Consecutivo_Acta").val();
        var Fk_Id_Sede = $("#Fk_Id_Sede").val();
        if (AccionARealizar.length == 0) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe ingresar la actividad a realizar',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
         }
        else {
            if (Responsable.length == 0) {
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'Debe ingresar el responsable',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
            else {
                if (($.datepicker.parseDate('dd/mm/yy', fecha_actual)) >= ($.datepicker.parseDate('dd/mm/yy', Fecha))) {
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'Debe ingresar una fecha probable mayor a la fecha actual.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
                else {
                    var Miembro = {
                        AccionARealizar: AccionARealizar,
                        Responsable: Responsable,
                        FechaProbable: Fecha,
                        PK_Id_Acta: PK_Id_Acta,
                        Consecutivo_Acta: Consecutivo_Acta,
                        Fk_Id_Sede: Fk_Id_Sede,
                    }
                    $.ajax({
                        url: urlBase + '/Comites/GuardarAccion',
                        data: Miembro,
                        type: 'POST',
                        success: function (result) {
                            OcultarPopupposition();
                            $("#tAcciones td").parent().remove();
                            $("#AccionARealizar").val("");
                            $("#Responsable").val("");
                            $("#divTmiebro").show("toogle");
                            $("#divTmiebro").trigger("reset");
                            $('#tbAccion').empty();
                            $('#tbAccion').append
                            //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                            var contador = 0;
                            var contador1 = 1;
                            $.each(result.Data, function (ind, element) {
                                var Fecha = moment(result.Data[ind].FechaProbable).format("DD/MM/YYYY");
                                var elemento = '<tr name="trAcciones">' +
                                                '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="Contador1' + contador + '" value="' + contador1 + '">' + contador1 + '</td>' +
                                                '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="AccionARealizar" id="AccionARealizar' + contador + '"value="' + element.AccionARealizar + '">' + element.AccionARealizar + '</td>' +
                                                '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Responsable" id="Responsable' + contador + '"value="' + element.Responsable + '">' + element.Responsable + '</td>' +
                                                '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="FechaProbable" id="FechaProbable' + contador + '"value="' + Fecha + '">' + Fecha + '</td>' +
                                                '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarAccionCopasst(' + element.PK_Id_Acta + ',' + element.Pk_Id_AccionActaCopasst + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                                '</tr></table>'
                                $('#tbAccion').append(elemento)
                                contador = contador + 1;
                                contador1 = contador1 + 1;
                            })
                            paginador("#tbAccion", "tr[name = trAcciones]", "#paginadorAcciones");

                        },
                        error: function (result) {
                            swal({
                                type: 'error',
                                title: 'Estimado Usuario',
                                text: 'Se presentó, un error Intente mas tarde.',
                                confirmButtonColor: '#7E8A97'
                            });
                            OcultarPopupposition();
                        }
                    });
                }
            }
        }
    }
}

//Validacion para Adccionar Miembro al Acta Copasst
function ValidarCamposActaCopasst() {
    $('#CrearActa').validate({
        errorClass: "error",
        rules: {
            TemaReunion: { required: true},
        },
        messages: {
            TemaReunion: { required: " * Debe Ingresar el Tema de la reunión." },
        }
    });
};

//Consulta la informacion del trabajador
function DatosTrabajador() {
    ValidarDocumento();
    var documento = $("#Numero_Documento").val();
    if ($("#CrearActa").valid()) {
            PopupPosition();
            $.ajax({
                type: "post",
                data: { numeroDocumento: documento },
                url: urlBase + '/Comites/ConsultarDatosTrabajador'
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'OK') {
                    var trabajador = response.Data;
                    var nombre = trabajador.nombre1 + ' ' + trabajador.nombre2 + ' ' + trabajador.apellido1 + ' ' + trabajador.apellido2;
                    $('#Nombre').val(nombre);
                } else if (response != undefined && response != '' && response.Mensaje != '') {
                    $("#Numero_Documento").val('');
                    $("#Nombre").val('');
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario.',
                        text: response.Data,
                        confirmButtonColor: '#7E8A97'
                    });
                    
                }
                OcultarPopupposition();
            }).fail(function (response) {
                $("#Numero_Documento").val('');
                $("#Nombre").val('');
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'No se encontró información, asociada al documento ingresado',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
                
            });
            
        }
    //}
}

//Importar archivos para las actas copasst
function importarArchivo(consecutivoActa,nombreArchivo) {
    //var nombreArchivo = $("#idNombreArchivo").val();
    var idSede = $("#IdSede").val();
    PopupPosition();
    $.ajax({
        type: "post",
        //enctype: 'multipart/form-data',
        data: {
            NombreArchivo: nombreArchivo,
            consecutivo_Acta: consecutivoActa,
            IdSede: idSede,
        },
        url: urlBase + '/Comites/CargarActaCopasst'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'OK') {
                OcultarPopupposition();
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'El archivo ha sido Importado',
                    confirmButtonColor: '#7E8A97'
                });
                
            } else if (response != undefined && response != '' && response.Mensaje != '') {
                OcultarPopupposition();
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: response.Mensaje,
                    confirmButtonColor: '#7E8A97'
                });
            }
       
    }).fail(function (response) {
        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'No se pudo obtener la información, Intente mas tarde.',
            confirmButtonColor: '#7E8A97'
        });
        OcultarPopupposition();
    });
}

//Llama al accion que muestra la vista para crear el acta con todos los datos que tenga guardados
function DatosActaCopasst(Fk_Id_Acta) {
    PopupPosition();
    $.ajax({
        url: urlBase + '/Comites/DatosActaCopasst',//primero el modulo/controlador/metodo que esta en el controlador
        data: {
            FK_Id_Acta: Fk_Id_Acta
        },
        type: 'POST',
        success: function (result) {
            if (result.Data) {
                swal({
                    type: 'success',
                    title: 'Estimado Usuario',
                    text: 'Ahora puede iniciar con la creacion del acta',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
        },
        error: function (result) {
            swal({
                type: 'warning',
                title: 'Atención',
                text: 'No se pudo obtener la información, Intente mas tarde.',
                confirmButtonColor: '#7E8A97',
                confirmButtonText: "Aceptar",
            });
            OcultarPopupposition();
        }
    })
}

//Actualizacion del Acta Copasst
function ActualizarActaCopasst() {
    ValidarCamposActaCopasst();
    if ($("#CrearActa").valid()) {
        PopupPosition();
        var hoy = new Date();
        dia = hoy.getDate();
        mes = hoy.getMonth() + 1;
        anio = hoy.getFullYear();
        if (mes < 10) {
            fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
        } else {
            fecha_actual = String(dia + "/" + mes + "/" + anio);
        }
        var Fecha = $("#Fecha").val();
        var TemaReunion = $("#TemaReunion").val();
        var Conclusiones = $("#Conclusiones").val();
        var PK_Id_Acta = $("#PK_Id_Acta").val();
        var Consecutivo_Acta = $("#Consecutivo_Acta").val();
        var Fk_Id_Sede = $("#Fk_Id_Sede").val();
        //if (($.datepicker.parseDate('dd/mm/yy', fecha_actual)) > ($.datepicker.parseDate('dd/mm/yy', Fecha))) {
        //    swal({
        //        type: 'warning',
        //        title: 'Estimado Usuario',
        //        text: 'La Fecha del acta no puede ser inferior a la Fecha actual',
        //        confirmButtonColor: '#7E8A97'
        //    });
        //    OcultarPopupposition();
        //}
        //else {
            var Acta = {
                Fecha: Fecha,
                TemaReunion: TemaReunion,
                Conclusiones: Conclusiones,
                PK_Id_Acta: PK_Id_Acta,
                Consecutivo_Acta: Consecutivo_Acta,
                Fk_Id_Sede: Fk_Id_Sede,
            }
            $.ajax({
                url: urlBase + '/Comites/ActualizarActaCopasst',
                data: Acta,
                type: 'POST',
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'OK') {
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario.',
                        text: "El Acta Copasst ha sido guardada.",
                        confirmButtonColor: '#7E8A97'
                    });
                } else if (response != undefined && response != '' && response.Mensaje != '') {
                     swal({
                        type: 'error',
                        title: 'Estimado Usuario.',
                        text: "No se pudo guardar la información, Intente mas tarde.",
                        confirmButtonColor: '#7E8A97'
                    });
                }
                OcultarPopupposition();
            }).fail(function (response) {
                 swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'No se pudo guardar la información, Intente mas tarde.',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            });
        //}
    }
}

//ACTUALIZAR OBSERVACIONES DE LOS TEMAS DEL ACTA COPASST
function ActualizarObservacionTemaCopasst(Fk_Id_Acta, PK_Id_TemaActa) {
    //ValidarActualizarObservacion();
    if ($("#CrearActa").valid()) {
        var dato = $("#Observaciones").val();
        OcultarPopupposition();
        var Tema = {
            Observaciones: dato,
            Fk_Id_Acta: Fk_Id_Acta,
            PK_Id_TemaActa: PK_Id_TemaActa,
        }
        $.ajax({
            url: urlBase + '/Comites/actualizarTemaCopasst',
            data: Tema,
            type: 'POST',
            success: function (result) {
                if (result.Mensaje == 'OK') {
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario.',
                        text: "La observación ha sido actualizada.",
                        confirmButtonColor: '#7E8A97'
                    });
                    $("#Observaciones").val("");
                    $("#tObservaciones td").parent().remove();
                    $('#tbObservacion').empty();
                    $("#divTmiebro").show("toogle");
                    $("#divTmiebro").trigger("reset");
                    $('#tbObservacion').append
                    var contador2 = 0;
                    var contador3 = 1;
                    $.each(result.Data, function (ind, element) {
                        var observacion = element.Observaciones;
                        if (observacion == null)
                            observacion = "";
                        var elemento = '<tr name="trObservacion">' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="contador3' + contador2 + '" value="' + contador3 + '">' + contador3 + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Tema" id="Tema' + contador2 + '"value="' + element.Tema + '">' + element.Tema + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Observaciones" id="Observaciones' + contador2 + '"value="' + observacion + '">' + observacion + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="ActualizarObservacionTemaCopasst(' + element.PK_Id_Acta + ',' + element.PK_Id_TemaActa + ')" title="Actualizar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-refresh"></span></a></td>' +
                                        '</tr></table>'
                        $('#tbObservacion').append(elemento)
                        contador2 = contador2 + 1;
                        contador3 = contador3 + 1;
                    })
                    paginador("#tbObservacion", "tr[name = trObservacion]", "#paginadorObservacion");
                }
                else {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó, un error Intente mas tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            },
            error: function (result) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'Se presentó, un error Intente mas tarde.',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
        });
    }
}

///////////////////////////////////////////////////////////////////

//ADICIONAR MIEMBROS COMITE CONVIVENCIA LABORAL
function AdicionarMiembroConvivencia() {
    ValidarAdicionarMiembro();
    if ($("#CrearActa").valid()) {
        PopupPosition();
        var Documento = $("#Numero_Documento").val();
        var Nombre = $("#Nombre").val();
        var Prioridad = $("#TiposPrioridadMiembros").val();
        var Representa = $("#Representa").val();
        var Principal = $("#Principal").val();
        var IdSede = $("#IdSede").val();
        var ConsecutivoActa = $("#Consecutivo_Acta").val();
        if (Nombre.length == 0) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe Ingresar la información del Asistente.',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
        else {
            if (Prioridad.length == 0) {
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'Debe Ingresar una prioridad.',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
            else {
                if (Representa.length == 0) {
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'Debe Ingresar a quien representa.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
                else {
                    var Miembro = {
                        Numero_Documento: Documento,
                        Nombre: Nombre,
                        Fk_Id_TipoPrioridadMiembro: Prioridad,
                        TipoRepresentante: Representa,
                        Fk_Id_TipoPrincipal: Principal,
                        Fk_Id_Sede: IdSede,
                        Consecutivo_Acta: ConsecutivoActa,
                    }
                    $.ajax({
                        url: urlBase + '/ComitesConvivencia/GuardarMiembro',
                        data: Miembro,
                        type: 'POST',
                        success: function (result) {
                            $('#Consecutivo_Acta').val(result.Data.Consecutivo_Acta);
                            $('#PK_Id_Acta').val(result.Data.PK_Id_Acta);
                            $.ajax({
                                url: urlBase + '/ComitesConvivencia/MiembroActaConvivencia',
                                data: result.Data,
                                type: 'POST',
                                success: function (result) {
                                    OcultarPopupposition();
                                    $("#tMiembros11 td").parent().remove();
                                    $("#Numero_Documento").val("");
                                    $("#Nombre").val("");
                                    $("#TiposPrioridadMiembros").val("");
                                    $("#Representa").val("");
                                    $("#Principal").val("");
                                    $("#divTmiebro").show("toogle");
                                    $("#divTmiebro").trigger("reset");
                                    $('#tMiembros').empty();
                                    //$('#tMiembros').append
                                    //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                                    var contador = 0;
                                    $.each(result.Data, function (ind, element) {
                                        var TipoPrincipal = element.Des_TipoPrincipal;
                                        if (TipoPrincipal == null)
                                            TipoPrincipal = "";
                                        var elemento = '<tr name="trMiembros">' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Numero_Documento" id="Numero_Documento' + contador + '" value="' + element.Numero_Documento + '">' + element.Numero_Documento + '</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Nombre" id="Nombre' + contador + '"value="' + element.Nombre + '">' + element.Nombre + '</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; "><input type="hidden" name="Des_TipoPrioridadMiembro" id ="Des_TipoPrioridadMiembro' + contador + '"value="' + element.Des_TipoPrioridadMiembro + '">' + element.Des_TipoPrioridadMiembro + '</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; "><input type="hidden" name="TipoRepresentante" id ="TipoRepresentante' + contador + '"value="' + element.TipoRepresentante + '">' + element.TipoRepresentante + '</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; "><input type="hidden" name="Des_TipoPrincipal" id ="Des_TipoPrincipal' + contador + '"value="' + TipoPrincipal + '">' + TipoPrincipal + '</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarMiembrosCopasst(' + element.PK_Id_Acta + ',' + element.Numero_Documento + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                                        //'<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a data-toggle="modal" data-target="#@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" value="@SG_SST.Recursos.GeneralApp.General.btn_Eliminar" class="btn btn-search"><span class="glyphicon glyphicon-erase"></span></a><div id="@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" class=" modal fade" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header encabezado" style="background-color:transparent; border-bottom:none"><button type="button" class="close" data-dismiss="modal" aria-label="Close"></button><h4 class="modal-title title">Eliminar Miembro Acta</h4></div><div class="modal-body" style="color:black"><center><p>¿Seguro desea eliminar el Registro?</p></center></div><div class="modal-footer">@Html.ActionLink(SG_SST.Recursos.GeneralApp.General.btn_Eliminar, "DeleteConfirmed", new { id = ' + element.Numero_Documento + ' },htmlAttributes: new { @type = "button", @class = "boton botonactive" })<button type="button" class="boton botoncancel" data-dismiss="modal">Cancelar</button></div></div></div></div></td>' +
                                                        '</tr></table>'
                                        $('#tMiembros').append(elemento)
                                        contador = contador + 1;
                                    })

                                    paginador("#tMiembros", "tr[name = trMiembros]", "#paginadorMiembros")

                                },
                                error: function (result) {
                                    swal({
                                        type: 'error',
                                        title: 'Estimado Usuario',
                                        text: 'Se presentó, un error Intente mas tarde.',
                                        confirmButtonColor: '#7E8A97'
                                    });
                                    OcultarPopupposition();
                                }
                            });
                        },
                        error: function (result) {
                            swal({
                                type: 'error',
                                title: 'Estimado Usuario',
                                text: 'Se presentó, un error Intente mas tarde.',
                                confirmButtonColor: '#7E8A97'
                            });
                            OcultarPopupposition();
                        }
                    });
                }
            }
        }
    }
}

// ELIMINAR PARTICIPANTE ACTA COPASST
function EliminarParticipanteConvivencia(id_Acta, documento) {
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar el Registro?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    }, (function (isConfirm) {
        if (isConfirm) {
            PopupPosition();
            $.ajax({
                url: urlBase + '/ComitesConvivencia/eliminarParticipanteConvivencia',
                data: {
                    FK_Id_Acta: id_Acta,
                    Documento: documento
                },
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    if (result.Mensaje == "OK") {
                        swal(
                                  'Estimado Usuario',
                                  'Su registro ha sido eliminado',
                                  'success'
                        );
                        $("#tParticipantes td").parent().remove();
                        $("#Numero_Documento").val("");
                        $("#Nombre").val("");
                        $("#divTmiebro").show("toogle");
                        $("#divTmiebro").trigger("reset");
                        $('#tbParticipantes').empty();
                        $('#tbParticipantes').append
                        //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                        var contador = 0;
                        $.each(result.Data, function (ind, element) {
                            var elemento = '<tr name="trParticipante">' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Numero_Documento" id="Numero_Documento' + contador + '" value="' + element.Numero_Documento + '">' + element.Numero_Documento + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Nombre" id="Nombre' + contador + '"value="' + element.Nombre + '">' + element.Nombre + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarParticipanteConvivencia(' + element.PK_Id_Acta + ',' + element.Numero_Documento + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                            //'<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a data-toggle="modal" data-target="#@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" value="@SG_SST.Recursos.GeneralApp.General.btn_Eliminar" class="btn btn-search"><span class="glyphicon glyphicon-erase"></span></a><div id="@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" class=" modal fade" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header encabezado" style="background-color:transparent; border-bottom:none"><button type="button" class="close" data-dismiss="modal" aria-label="Close"></button><h4 class="modal-title title">Eliminar Miembro Acta</h4></div><div class="modal-body" style="color:black"><center><p>¿Seguro desea eliminar el Registro?</p></center></div><div class="modal-footer">@Html.ActionLink(SG_SST.Recursos.GeneralApp.General.btn_Eliminar, "DeleteConfirmed", new { id = ' + element.Numero_Documento + ' },htmlAttributes: new { @type = "button", @class = "boton botonactive" })<button type="button" class="boton botoncancel" data-dismiss="modal">Cancelar</button></div></div></div></div></td>' +
                                            '</tr></table>'
                            $('#tbParticipantes').append(elemento)
                            contador = contador + 1;
                        })

                        paginador("#tbParticipantes", "tr[name = trParticipante]", "#paginadorParticipantes")
                    }
                    else {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó, un error Intente mas tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó, un error Intente mas tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }))

}

// ELIMINAR MIEMBRO ACTA COMITE CONVIVENCIA LABORAL
function EliminarMiembrosConvivencia(id_Acta, documento) {
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar el Registro?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    }, (function (isConfirm) {
        if (isConfirm) {
            PopupPosition();
            $.ajax({
                url: urlBase + '/ComitesConvivencia/eliminarMiembroConvivencia',
                data: {
                    FK_Id_Acta: id_Acta,
                    Documento: documento
                },
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    if (result.Mensaje == "OK") {
                        swal(
                                  'Estimado Usuario',
                                  'Su registro ha sido eliminado',
                                  'success'
                        );
                        $("#tMiembros11 td").parent().remove();
                        $("#divTmiebro").trigger("reset");
                        $('#tMiembros').empty();
                        $('#tMiembros').append
                        //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                        var contador = 0;
                        $.each(result.Data, function (ind, element) {
                            var TipoPrincipal = element.Des_TipoPrincipal;
                            if (TipoPrincipal == null)
                                TipoPrincipal = "";
                            var elemento = '<tr name="trMiembros">' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Numero_Documento" id="Numero_Documento' + contador + '" value="' + element.Numero_Documento + '">' + element.Numero_Documento + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Nombre" id="Nombre' + contador + '"value="' + element.Nombre + '">' + element.Nombre + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; "><input type="hidden" name="Des_TipoPrioridadMiembro" id ="Des_TipoPrioridadMiembro' + contador + '"value="' + element.Des_TipoPrioridadMiembro + '">' + element.Des_TipoPrioridadMiembro + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; "><input type="hidden" name="TipoRepresentante" id ="TipoRepresentante' + contador + '"value="' + element.TipoRepresentante + '">' + element.TipoRepresentante + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; "><input type="hidden" name="Des_TipoPrincipal" id ="Des_TipoPrincipal' + contador + '"value="' + TipoPrincipal + '">' + TipoPrincipal + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarMiembrosConvivencia(' + element.PK_Id_Acta + ',' + element.Numero_Documento + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                            //'<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a data-toggle="modal" data-target="#@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" value="@SG_SST.Recursos.GeneralApp.General.btn_Eliminar" class="btn btn-search"><span class="glyphicon glyphicon-erase"></span></a><div id="@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" class=" modal fade" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header encabezado" style="background-color:transparent; border-bottom:none"><button type="button" class="close" data-dismiss="modal" aria-label="Close"></button><h4 class="modal-title title">Eliminar Miembro Acta</h4></div><div class="modal-body" style="color:black"><center><p>¿Seguro desea eliminar el Registro?</p></center></div><div class="modal-footer">@Html.ActionLink(SG_SST.Recursos.GeneralApp.General.btn_Eliminar, "DeleteConfirmed", new { id = ' + element.Numero_Documento + ' },htmlAttributes: new { @type = "button", @class = "boton botonactive" })<button type="button" class="boton botoncancel" data-dismiss="modal">Cancelar</button></div></div></div></div></td>' +
                                            '</tr></table>'
                            $('#tMiembros').append(elemento)
                            contador = contador + 1;
                        })

                        paginador("#tMiembros", "tr[name = trMiembros]", "#paginadorMiembros")
                    }
                    else {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó, un error Intente mas tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó, un error Intente mas tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }))

}

//ADICIONAR PARTICIPANTES ACTA COMITE CONVIVENCIA LABORAL
function AdicionarPaticipanteConvivencia() {
    //ValidarAdicionarParticipante();
    if ($("#CrearActa").valid()) {
        PopupPosition();
        var Documento = $("#Numero_Documento").val();
        var Nombre = $("#Nombre").val();
        var PK_Id_Acta = $("#PK_Id_Acta").val();
        var Consecutivo_Acta = $("#Consecutivo_Acta").val();
        var Fk_Id_Sede = $("#Fk_Id_Sede").val();
        if (Nombre.length == 0 || Documento.length == 0) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe Ingresar el Documento y el Nombre del Participante.',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
        else {
            var Miembro = {
                Numero_Documento: Documento,
                Nombre: Nombre,
                PK_Id_Acta: PK_Id_Acta,
                Consecutivo_Acta: Consecutivo_Acta,
                Fk_Id_Sede: Fk_Id_Sede,
            }
            $.ajax({
                url: urlBase + '/ComitesConvivencia/GuardarParticipante',
                data: Miembro,
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    $("#tParticipantes td").parent().remove();
                    $("#Numero_Documento").val("");
                    $("#Nombre").val("");
                    $("#divTmiebro").show("toogle");
                    $("#divTmiebro").trigger("reset");
                    $('#tbParticipantes').empty();
                    $('#tbParticipantes').append
                    //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                    var contador = 0;
                    $.each(result.Data, function (ind, element) {
                        var elemento = '<tr name="trParticipante">' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Numero_Documento" id="Numero_Documento' + contador + '" value="' + element.Numero_Documento + '">' + element.Numero_Documento + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Nombre" id="Nombre' + contador + '"value="' + element.Nombre + '">' + element.Nombre + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarParticipanteCopasst(' + element.PK_Id_Acta + ',' + element.Numero_Documento + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                        //'<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a data-toggle="modal" data-target="#@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" value="@SG_SST.Recursos.GeneralApp.General.btn_Eliminar" class="btn btn-search"><span class="glyphicon glyphicon-erase"></span></a><div id="@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" class=" modal fade" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header encabezado" style="background-color:transparent; border-bottom:none"><button type="button" class="close" data-dismiss="modal" aria-label="Close"></button><h4 class="modal-title title">Eliminar Miembro Acta</h4></div><div class="modal-body" style="color:black"><center><p>¿Seguro desea eliminar el Registro?</p></center></div><div class="modal-footer">@Html.ActionLink(SG_SST.Recursos.GeneralApp.General.btn_Eliminar, "DeleteConfirmed", new { id = ' + element.Numero_Documento + ' },htmlAttributes: new { @type = "button", @class = "boton botonactive" })<button type="button" class="boton botoncancel" data-dismiss="modal">Cancelar</button></div></div></div></div></td>' +
                                        '</tr></table>'
                        $('#tbParticipantes').append(elemento)
                        contador = contador + 1;
                    })

                    paginador("#tbParticipantes", "tr[name = trParticipante]", "#paginadorParticipantes")

                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó, un error Intente mas tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }
}

//ADICIONAR RESPONSABLE QUEJA ACTA COMITE CONVIVENCIA LABORAL
function AdicionarResponsableQueja() {
    //ValidarAdicionarParticipante();
    if ($("#CrearActa").valid()) {
        PopupPosition();
        var Documento = $("#Numero_Documento").val();
        var Nombre = $("#Nombre").val();
        var PK_Id_Acta = $("#PK_Id_Acta").val();
        var Fk_Id_Queja = $("#PK_Id_Queja").val();
        if (Nombre.length == 0 || Documento.length == 0) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe Ingresar el Documento y el Nombre del Responsable.',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
        else {
            var Miembro = {
                Numero_Documento: Documento,
                Nombre: Nombre,
                PK_Id_Acta: PK_Id_Acta,
                Fk_Id_Queja: Fk_Id_Queja,
             }
            $.ajax({
                url: urlBase + '/ComitesConvivencia/guardarResponsableQueja',
                data: Miembro,
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    $("#tResponsables td").parent().remove();
                    $("#Numero_Documento").val("");
                    $("#Nombre").val("");
                    $("#divTmiebro").show("toogle");
                    $("#divTmiebro").trigger("reset");
                    $('#tbResponsables').empty();
                    $('#tbResponsables').append
                    //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                    var contador = 0;
                    $.each(result.Data, function (ind, element) {
                        var elemento = '<tr name="trResponsable">' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Numero_Documento" id="Numero_Documento' + contador + '" value="' + element.Numero_Documento + '">' + element.Numero_Documento + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Nombre" id="Nombre' + contador + '"value="' + element.Nombre + '">' + element.Nombre + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarResponsableQueja(' + element.Fk_Id_Queja + ',' + element.Numero_Documento + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                        //'<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a data-toggle="modal" data-target="#@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" value="@SG_SST.Recursos.GeneralApp.General.btn_Eliminar" class="btn btn-search"><span class="glyphicon glyphicon-erase"></span></a><div id="@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" class=" modal fade" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header encabezado" style="background-color:transparent; border-bottom:none"><button type="button" class="close" data-dismiss="modal" aria-label="Close"></button><h4 class="modal-title title">Eliminar Miembro Acta</h4></div><div class="modal-body" style="color:black"><center><p>¿Seguro desea eliminar el Registro?</p></center></div><div class="modal-footer">@Html.ActionLink(SG_SST.Recursos.GeneralApp.General.btn_Eliminar, "DeleteConfirmed", new { id = ' + element.Numero_Documento + ' },htmlAttributes: new { @type = "button", @class = "boton botonactive" })<button type="button" class="boton botoncancel" data-dismiss="modal">Cancelar</button></div></div></div></div></td>' +
                                        '</tr></table>'
                        $('#tbResponsables').append(elemento)
                        contador = contador + 1;
                    })

                    paginador("#tbResponsables", "tr[name = trResponsable]", "#paginadorResponsable")

                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó, un error Intente mas tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }
}

//ADICIONAR TEMAS ACTA COMITE CONVIVENCIA LABORAL
function AdicionarTemasConvivencia() {
    //ValidarAdicionarTemas();
    if ($("#CrearActa").valid()) {
        PopupPosition();
        var vTema = $("#TemaOrdenDia").val();
        var PK_Id_Acta = $("#PK_Id_Acta").val();
        var Consecutivo_Acta = $("#Consecutivo_Acta").val();
        var Fk_Id_Sede = $("#Fk_Id_Sede").val();
        if (vTema.length == 0) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe Ingresar el Tema.',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
        else {
            var Miembro = {
                TemaOrdenDia: vTema,
                PK_Id_Acta: PK_Id_Acta,
                Consecutivo_Acta: Consecutivo_Acta,
                Fk_Id_Sede: Fk_Id_Sede,
            }
            $.ajax({
                url: urlBase + '/ComitesConvivencia/GuardarTema',
                data: Miembro,
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    $("#tTemas td").parent().remove();
                    $("#TemaOrdenDia").val("");
                    $("#divTmiebro").show("toogle");
                    $("#divTmiebro").trigger("reset");
                    $('#tbTemas').empty();
                    $('#tbTemas').append
                    //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                    var contador = 0;
                    var contador1 = 1;
                    $.each(result.Data, function (ind, element) {
                        var elemento = '<tr name="trOrdenDia">' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="Contador1' + contador + '" value="' + contador1 + '">' + contador1 + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Nombre" id="Tema' + contador + '"value="' + element.Tema + '">' + element.Tema + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarTemaConvivencia(' + element.PK_Id_Acta + ',' + element.PK_Id_TemaActa + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                        '</tr></table>'
                        $('#tbTemas').append(elemento)
                        contador = contador + 1;
                        contador1 = contador1 + 1;
                    })
                    paginador("#tbTemas", "tr[name = trOrdenDia]", "#paginadorTemas");

                    //$("#ListaTema").empty();
                    //$("#ListaTema").append("<option value=\"\">--Seleccionar Tema--</option>");

                    //$.each(result.Data, function (key, value) {
                    //    $("#ListaTema").append("<option value=\"" + value.PK_Id_TemaActa + "\">" + value.Tema + "</option>");
                    //});
                    $("#tObservaciones td").parent().remove();
                    $('#tbObservacion').empty();
                    $("#divTmiebro").show("toogle");
                    $("#divTmiebro").trigger("reset");
                    $('#tbObservacion').append
                    var contador2 = 0;
                    var contador3 = 1;
                    $.each(result.Data, function (ind, element) {
                        var observacion = element.Observaciones;
                        if (observacion == null)
                            observacion = "";
                        var elemento = '<tr name="trObservacion">' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="contador3' + contador2 + '" value="' + contador3 + '">' + contador3 + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Tema" id="Tema' + contador2 + '"value="' + element.Tema + '">' + element.Tema + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Observaciones" id="Observaciones' + contador2 + '"value="' + observacion + '">' + observacion + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="ActualizarObservacionTemaConvivencia(' + element.PK_Id_Acta + ',' + element.PK_Id_TemaActa + ')" title="Actualizar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-refresh"></span></a></td>' +
                                        '</tr></table>'
                        $('#tbObservacion').append(elemento)
                        contador2 = contador2 + 1;
                        contador3 = contador3 + 1;
                    })
                    paginador("#tbObservacion", "tr[name = trObservacion]", "#paginadorObservacion");

                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó, un error Intente mas tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }
}

//ELIMINAR TEMAS COMITE CONVIVENCIA LABORAL
function EliminarTemaConvivencia(id_Acta, PK_Id_TemaActa) {
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar el Registro?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    }, (function (isConfirm) {
        if (isConfirm) {
            PopupPosition();
            $.ajax({
                url: urlBase + '/ComitesConvivencia/eliminarTemaConvivencia',
                data: {
                    FK_Id_Acta: id_Acta,
                    PK_Id_TemaActa: PK_Id_TemaActa
                },
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    if (result.Mensaje == "OK") {
                        swal(
                                  'Estimado Usuario',
                                  'Su registro ha sido eliminado',
                                  'success'
                        );
                        $("#tTemas td").parent().remove();
                        $("#Tema").val("");
                        $("#divTmiebro").show("toogle");
                        $("#divTmiebro").trigger("reset");
                        $('#tbTemas').empty();
                        $('#tbTemas').append
                        //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                        var contador = 0;
                        var contador1 = 1;
                        $.each(result.Data, function (ind, element) {
                            var elemento = '<tr name="trOrdenDia">' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="Contador1' + contador + '" value="' + contador1 + '">' + contador1 + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray;"><input type="hidden" name="Nombre" id="Tema' + contador + '"value="' + element.Tema + '">' + element.Tema + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarTemaConvivencia(' + element.PK_Id_Acta + ',' + element.PK_Id_TemaActa + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                            '</tr></table>'
                            $('#tbTemas').append(elemento)
                            contador = contador + 1;
                            contador1 = contador1 + 1;
                        })
                        paginador("#tbTemas", "tr[name = trOrdenDia]", "#paginadorTemas");

                        $("#tObservaciones td").parent().remove();
                        $('#tbObservacion').empty();
                        $("#divTmiebro").show("toogle");
                        $("#divTmiebro").trigger("reset");
                        $('#tbObservacion').append
                        var contador2 = 0;
                        var contador3 = 1;
                        $.each(result.Data, function (ind, element) {
                            var observacion = element.Observaciones;
                            if (observacion == null)
                                observacion = "";
                            var elemento = '<tr name="trObservacion">' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="contador3' + contador2 + '" value="' + contador3 + '">' + contador3 + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Tema" id="Tema' + contador2 + '"value="' + element.Tema + '">' + element.Tema + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Observaciones" id="Observaciones' + contador2 + '"value="' + observacion + '">' + observacion + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="ActualizarObservacionTemaConvivencia(' + element.PK_Id_Acta + ',' + element.PK_Id_TemaActa + ')" title="Actualizar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-refresh"></span></a></td>' +
                                            '</tr></table>'
                            $('#tbObservacion').append(elemento)
                            contador2 = contador2 + 1;
                            contador3 = contador3 + 1;
                        })
                        paginador("#tbObservacion", "tr[name = trObservacion]", "#paginadorObservacion");
                    }
                    else {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó, un error Intente mas tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó, un error Intente mas tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }))

}

//ELIMINAR ACCIONES COMITE CONVIVENCIA LABORAL
function EliminarAccionConvivencia(id_Acta, Pk_Id_AccionActaConvivencia) {
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar el Registro?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    }, (function (isConfirm) {
        if (isConfirm) {
            PopupPosition();
            $.ajax({
                url: urlBase + '/ComitesConvivencia/eliminarAccionConvivencia',
                data: {
                    FK_Id_Acta: id_Acta,
                    Pk_Id_AccionActaConvivencia: Pk_Id_AccionActaConvivencia
                },
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    if (result.Mensaje == "OK") {
                        swal(
                                  'Estimado Usuario',
                                  'Su registro ha sido eliminado',
                                  'success'
                        );
                        $("#tAcciones td").parent().remove();
                        $("#AccionARealizar").val("");
                        $("#Responsable").val("");
                        $("#divTmiebro").show("toogle");
                        $("#divTmiebro").trigger("reset");
                        $('#tbAccion').empty();
                        $('#tbAccion').append
                        //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                        var contador = 0;
                        var contador1 = 1;
                        $.each(result.Data, function (ind, element) {
                            var Fecha = moment(result.Data[ind].FechaProbable).format("DD/MM/YYYY");
                            var elemento = '<tr name="trAcciones">' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="Contador1' + contador + '" value="' + contador1 + '">' + contador1 + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; "><input type="hidden" name="AccionARealizar" id="AccionARealizar' + contador + '"value="' + element.AccionARealizar + '">' + element.AccionARealizar + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; "><input type="hidden" name="Responsable" id="Responsable' + contador + '"value="' + element.Responsable + '">' + element.Responsable + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="FechaProbable" id="FechaProbable' + contador + '"value="' + Fecha + '">' + Fecha + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarAccionConvivencia(' + element.PK_Id_Acta + ',' + element.Pk_Id_AccionActaCopasst + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                            '</tr></table>'
                            $('#tbAccion').append(elemento)
                            contador = contador + 1;
                            contador1 = contador1 + 1;
                        })
                        paginador("#tbAccion", "tr[name = trAcciones]", "#paginadorAcciones");
                    }
                    else {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó, un error Intente mas tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó, un error Intente mas tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }))

}

//ELIMINAR ACCIONES QUEJAS COMITE CONVIVENCIA LABORAL
function EliminarAccionQueja(Fk_Id_Queja, Pk_Id_AccionQueja) {
    var PK_Id_Acta = $("#PK_Id_Acta").val();
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar el Registro?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    }, (function (isConfirm) {
        if (isConfirm) {
            PopupPosition();
            $.ajax({
                url: urlBase + '/ComitesConvivencia/eliminarAccionQueja',
                data: {
                    Fk_Id_Queja: Fk_Id_Queja,
                    Pk_Id_AccionQueja: Pk_Id_AccionQueja,
                    PK_Id_Acta: PK_Id_Acta,
                },
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    if (result.Mensaje == "OK") {
                        swal(
                                  'Estimado Usuario',
                                  'Su registro ha sido eliminado',
                                  'success'
                        );
                        $("#tAcciones td").parent().remove();
                        $("#AccionARealizar").val("");
                        $("#divTmiebro").show("toogle");
                        $("#divTmiebro").trigger("reset");
                        $('#tbAccion').empty();
                        $('#tbAccion').append
                        //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                        var contador = 0;
                        var contador1 = 1;
                        $.each(result.Data, function (ind, element) {
                            var elemento = '<tr name="trAcciones">' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="Contador1' + contador + '" value="' + contador1 + '">' + contador1 + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; "><input type="hidden" name="AccionARealizar" id="AccionARealizar' + contador + '"value="' + element.AccionARealizar + '">' + element.AccionARealizar + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarAccionQueja(' + element.Fk_Id_Queja + ',' + element.Pk_Id_AccionQueja + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                            '</tr></table>'
                            $('#tbAccion').append(elemento)
                            contador = contador + 1;
                            contador1 = contador1 + 1;
                        })
                        paginador("#tbAccion", "tr[name = trAcciones]", "#paginadorAcciones");
                    }
                    else {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó, un error Intente mas tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó, un error Intente mas tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }))

}

// ELIMINAR RESPONSABLE ACTA COPASST
function EliminarResponsableQueja(Fk_Id_Queja, documento) {
    var PK_Id_Acta = $("#PK_Id_Acta").val();
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar el Registro?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    }, (function (isConfirm) {
        if (isConfirm) {
            PopupPosition();
            $.ajax({
                url: urlBase + '/ComitesConvivencia/eliminarResponsableQueja',
                data: {
                    Fk_Id_Queja: Fk_Id_Queja,
                    Documento: documento,
                    PK_Id_Acta: PK_Id_Acta,
                },
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    if (result.Mensaje == "OK") {
                        swal(
                                  'Estimado Usuario',
                                  'Su registro ha sido eliminado',
                                  'success'
                        );
                        $("#tResponsables td").parent().remove();
                        $("#Numero_Documento").val("");
                        $("#Nombre").val("");
                        $("#divTmiebro").show("toogle");
                        $("#divTmiebro").trigger("reset");
                        $('#tbResponsables').empty();
                        $('#tbResponsables').append
                        //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                        var contador = 0;
                        $.each(result.Data, function (ind, element) {
                            var elemento = '<tr name="trResponsable">' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Numero_Documento" id="Numero_Documento' + contador + '" value="' + element.Numero_Documento + '">' + element.Numero_Documento + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Nombre" id="Nombre' + contador + '"value="' + element.Nombre + '">' + element.Nombre + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarResponsableQueja(' + element.Fk_Id_Queja + ',' + element.Numero_Documento + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                            //'<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a data-toggle="modal" data-target="#@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" value="@SG_SST.Recursos.GeneralApp.General.btn_Eliminar" class="btn btn-search"><span class="glyphicon glyphicon-erase"></span></a><div id="@string.Format("modalEliminar{0}", ' + element.Numero_Documento + ')" class=" modal fade" role="dialog"><div class="modal-dialog"><div class="modal-content"><div class="modal-header encabezado" style="background-color:transparent; border-bottom:none"><button type="button" class="close" data-dismiss="modal" aria-label="Close"></button><h4 class="modal-title title">Eliminar Miembro Acta</h4></div><div class="modal-body" style="color:black"><center><p>¿Seguro desea eliminar el Registro?</p></center></div><div class="modal-footer">@Html.ActionLink(SG_SST.Recursos.GeneralApp.General.btn_Eliminar, "DeleteConfirmed", new { id = ' + element.Numero_Documento + ' },htmlAttributes: new { @type = "button", @class = "boton botonactive" })<button type="button" class="boton botoncancel" data-dismiss="modal">Cancelar</button></div></div></div></div></td>' +
                                            '</tr></table>'
                            $('#tbResponsables').append(elemento)
                            contador = contador + 1;
                        })

                        paginador("#tbResponsables", "tr[name = trResponsable]", "#paginadorResponsable")
                    }
                    else {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó, un error Intente mas tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó, un error Intente mas tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }))

}

//ADICIONAR ACCIONES ACTA COMITE CONVIVENCIA LABORAL
function AdicionarAccionesConvivencia() {
    //ValidarAdicionarAccion();
    if ($("#CrearActa").valid()) {
        PopupPosition();
        var AccionARealizar = $("#AccionARealizar").val();
        var Responsable = $("#Responsable").val();
        var Fecha = $("#FechaProbable").val();

        var hoy = new Date();
        dia = hoy.getDate();
        mes = hoy.getMonth() + 1;
        anio = hoy.getFullYear();
        if (mes < 10) {
            fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
        } else {
            fecha_actual = String(dia + "/" + mes + "/" + anio);
        }
        var PK_Id_Acta = $("#PK_Id_Acta").val();
        var Consecutivo_Acta = $("#Consecutivo_Acta").val();
        var Fk_Id_Sede = $("#Fk_Id_Sede").val();
        if (AccionARealizar.length == 0) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe ingresar la actividad a realizar',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
        else {
            if (Responsable.length == 0) {
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'Debe ingresar el responsable',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
            else {
                if (($.datepicker.parseDate('dd/mm/yy', fecha_actual)) >= ($.datepicker.parseDate('dd/mm/yy', Fecha))) {
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'Debe ingresar una fecha probable mayor a la fecha actual.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
                else {
                    var Miembro = {
                        AccionARealizar: AccionARealizar,
                        Responsable: Responsable,
                        FechaProbable: Fecha,
                        PK_Id_Acta: PK_Id_Acta,
                        Consecutivo_Acta: Consecutivo_Acta,
                        Fk_Id_Sede: Fk_Id_Sede,
                    }
                    $.ajax({
                        url: urlBase + '/ComitesConvivencia/GuardarAccion',
                        data: Miembro,
                        type: 'POST',
                        success: function (result) {
                            OcultarPopupposition();
                            $("#tAcciones td").parent().remove();
                            $("#AccionARealizar").val("");
                            $("#Responsable").val("");
                            $("#divTmiebro").show("toogle");
                            $("#divTmiebro").trigger("reset");
                            $('#tbAccion').empty();
                            $('#tbAccion').append
                            //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                            var contador = 0;
                            var contador1 = 1;
                            $.each(result.Data, function (ind, element) {
                                var Fecha = moment(result.Data[ind].FechaProbable).format("DD/MM/YYYY");
                                var elemento = '<tr name="trAcciones">' +
                                                '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="Contador1' + contador + '" value="' + contador1 + '">' + contador1 + '</td>' +
                                                '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; "><input type="hidden" name="AccionARealizar" id="AccionARealizar' + contador + '"value="' + element.AccionARealizar + '">' + element.AccionARealizar + '</td>' +
                                                '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; "><input type="hidden" name="Responsable" id="Responsable' + contador + '"value="' + element.Responsable + '">' + element.Responsable + '</td>' +
                                                '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="FechaProbable" id="FechaProbable' + contador + '"value="' + Fecha + '">' + Fecha + '</td>' +
                                                '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarAccionCopasst(' + element.PK_Id_Acta + ',' + element.Pk_Id_AccionActaCopasst + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                                '</tr></table>'
                                $('#tbAccion').append(elemento)
                                contador = contador + 1;
                                contador1 = contador1 + 1;
                            })
                            paginador("#tbAccion", "tr[name = trAcciones]", "#paginadorAcciones");

                        },
                        error: function (result) {
                            swal({
                                type: 'error',
                                title: 'Estimado Usuario',
                                text: 'Se presentó, un error Intente mas tarde.',
                                confirmButtonColor: '#7E8A97'
                            });
                            OcultarPopupposition();
                        }
                    });
                }
            }
        }
    }
}

//ADICIONAR ACCIONES QUEJA ACTA COMITE CONVIVENCIA LABORAL
function AdicionarAccionesQueja() {
    //ValidarAdicionarAccion();
    if ($("#CrearActa").valid()) {
        PopupPosition();
        var AccionARealizar = $("#AccionARealizar").val();
        var Fk_Id_Queja = $("#PK_Id_Queja").val();
        var PK_Id_Acta = $("#PK_Id_Acta").val();
        if (AccionARealizar.length == 0 ) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe Ingresar la acción a realizar.',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
        else {
            var Miembro = {
                AccionARealizar: AccionARealizar,
                PK_Id_Acta: PK_Id_Acta,
                Fk_Id_Queja: Fk_Id_Queja,
             }
            $.ajax({
                url: urlBase + '/ComitesConvivencia/guardarAccionQueja',
                data: Miembro,
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    $("#tAcciones td").parent().remove();
                    $("#AccionARealizar").val("");
                    $("#divTmiebro").show("toogle");
                    $("#divTmiebro").trigger("reset");
                    $('#tbAccion').empty();
                    $('#tbAccion').append
                    //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                    var contador = 0;
                    var contador1 = 1;
                    $.each(result.Data, function (ind, element) {
                        var elemento = '<tr name="trAcciones">' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="Contador1' + contador + '" value="' + contador1 + '">' + contador1 + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; "><input type="hidden" name="AccionARealizar" id="AccionARealizar' + contador + '"value="' + element.AccionARealizar + '">' + element.AccionARealizar + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarAccionQueja(' + element.Fk_Id_Queja + ',' + element.Pk_Id_AccionQueja + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                        '</tr></table>'
                        $('#tbAccion').append(elemento)
                        contador = contador + 1;
                        contador1 = contador1 + 1;
                    })
                    paginador("#tbAccion", "tr[name = trAcciones]", "#paginadorAcciones");

                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó, un error Intente mas tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }
}

//ACTUALIZAR OBSERVACIONES DE LOS TEMAS DEL ACTA COMITE CONVIVENCIA LABORAL
function ActualizarObservacionTemaConvivencia(Fk_Id_Acta, PK_Id_TemaActa) {
    //ValidarActualizarObservacion();
    if ($("#CrearActa").valid()) {
        var dato = $("#Observaciones").val();
        OcultarPopupposition();
        var Tema = {
            Observaciones: dato,
            Fk_Id_Acta: Fk_Id_Acta,
            PK_Id_TemaActa: PK_Id_TemaActa,
        }
        $.ajax({
            url: urlBase + '/ComitesConvivencia/actualizarTemaConvivencia',
            data: Tema,
            type: 'POST',
            success: function (result) {
                if (result.Mensaje == 'OK') {
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario.',
                        text: "La observación ha sido actualizada.",
                        confirmButtonColor: '#7E8A97'
                    });
                    $("#Observaciones").val("");
                    $("#tObservaciones td").parent().remove();
                    $('#tbObservacion').empty();
                    $("#divTmiebro").show("toogle");
                    $("#divTmiebro").trigger("reset");
                    $('#tbObservacion').append
                    var contador2 = 0;
                    var contador3 = 1;
                    $.each(result.Data, function (ind, element) {
                        var observacion = element.Observaciones;
                        if (observacion == null)
                            observacion = "";
                        var elemento = '<tr name="trObservacion">' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="contador3' + contador2 + '" value="' + contador3 + '">' + contador3 + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; "><input type="hidden" name="Tema" id="Tema' + contador2 + '"value="' + element.Tema + '">' + element.Tema + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; "><input type="hidden" name="Observaciones" id="Observaciones' + contador2 + '"value="' + observacion + '">' + observacion + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="ActualizarObservacionTemaConvivencia(' + element.PK_Id_Acta + ',' + element.PK_Id_TemaActa + ')" title="Actualizar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-refresh"></span></a></td>' +
                                        '</tr></table>'
                        $('#tbObservacion').append(elemento)
                        contador2 = contador2 + 1;
                        contador3 = contador3 + 1;
                    })
                    paginador("#tbObservacion", "tr[name = trObservacion]", "#paginadorObservacion");
                }
                else {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó, un error Intente mas tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            },
            error: function (result) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'Se presentó, un error Intente mas tarde.',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
        });
    }
}

//Actualizacion del Acta Copasst
function ActualizarActaConvivencia() {
    ValidarCamposActaCopasst();
    if ($("#CrearActa").valid()) {
        PopupPosition();
        var hoy = new Date();
        dia = hoy.getDate();
        mes = hoy.getMonth() + 1;
        anio = hoy.getFullYear();
        if (mes < 10) {
            fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
        } else {
            fecha_actual = String(dia + "/" + mes + "/" + anio);
        }
        var Fecha = $("#Fecha").val();
        var TemaReunion = $("#TemaReunion").val();
        var Conclusiones = $("#Conclusiones").val();
        var PK_Id_Acta = $("#PK_Id_Acta").val();
        var Consecutivo_Acta = $("#Consecutivo_Acta").val();
        var Fk_Id_Sede = $("#Fk_Id_Sede").val();
        //if (($.datepicker.parseDate('dd/mm/yy', fecha_actual)) > ($.datepicker.parseDate('dd/mm/yy', Fecha))) {
        //    swal({
        //        type: 'warning',
        //        title: 'Estimado Usuario',
        //        text: 'La Fecha del acta no puede ser inferior a la Fecha actual',
        //        confirmButtonColor: '#7E8A97'
        //    });
        //    OcultarPopupposition();
        //}
        //else {
            var Acta = {
                Fecha: Fecha,
                TemaReunion: TemaReunion,
                Conclusiones: Conclusiones,
                PK_Id_Acta: PK_Id_Acta,
                Consecutivo_Acta: Consecutivo_Acta,
                Fk_Id_Sede: Fk_Id_Sede,
            }
            $.ajax({
                url: urlBase + '/ComitesConvivencia/ActualizarActaConvivencia',
                data: Acta,
                type: 'POST',
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'OK') {
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario.',
                        text: "El Acta Comité Convivencia Laboral ha sido guardada.",
                        confirmButtonColor: '#7E8A97'
                    });
                } else if (response != undefined && response != '' && response.Mensaje != '') {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario.',
                        text: "No se pudo guardar la información, Intente mas tarde.",
                        confirmButtonColor: '#7E8A97'
                    });
                }
                OcultarPopupposition();
            }).fail(function (response) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'No se pudo guardar la información, Intente mas tarde.',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            });
        //}
    }
}

//ACTUALIZACION QUEJA
function ActualizarQueja() {
    if ($("#CrearActa").valid()) {
        PopupPosition();
        var hoy = new Date();
        dia = hoy.getDate();
        mes = hoy.getMonth() + 1;
        anio = hoy.getFullYear();
        if (mes < 10) {
            fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
        } else {
            fecha_actual = String(dia + "/" + mes + "/" + anio);
        }
        var Fecha = $("#Fecha").val();
        var NombreRefiereSituacion = $("#NombreRefiereSituacion").val();
        var AspectosNoResueltos = $("#AspectosNoResueltos").val();
        var Compromisos = $("#Compromisos").val();
        var PK_Id_Acta = $("#PK_Id_Acta").val();
        var PK_Id_Queja = $("#PK_Id_Queja").val();
        //if (($.datepicker.parseDate('dd/mm/yy', fecha_actual)) > ($.datepicker.parseDate('dd/mm/yy', Fecha))) {
        //    swal({
        //        type: 'warning',
        //        title: 'Estimado Usuario',
        //        text: 'La Fecha del acta no puede ser inferior a la Fecha actual',
        //        confirmButtonColor: '#7E8A97'
        //    });
        //    OcultarPopupposition();
        //}
        //else {
            var Acta = {
                Fecha: Fecha,
                NombreRefiereSituacion: NombreRefiereSituacion,
                AspectosNoResueltos: AspectosNoResueltos,
                Compromisos: Compromisos,
                Fk_Id_Acta: PK_Id_Acta,
                PK_Id_Queja: PK_Id_Queja,
            }
            $.ajax({
                url: urlBase + '/ComitesConvivencia/ActualizarQueja',
                data: Acta,
                type: 'POST',
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'OK') {
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario.',
                        text: "El Seguimiento a Quejas ha sido guardado.",
                        confirmButtonColor: '#7E8A97'
                    });
                } else if (response != undefined && response != '' && response.Mensaje != '') {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario.',
                        text: "No se pudo guardar la información, Intente mas tarde.",
                        confirmButtonColor: '#7E8A97'
                    });
                }
                OcultarPopupposition();
            }).fail(function (response) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'No se pudo guardar la información, Intente mas tarde.',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            });
        }
    //}
}

//ACTUALIZACION SEGUIMIENTO
function ActualizarSeguimiento() {
    if ($("#CrearActa").valid()) {
        PopupPosition();
        var hoy = new Date();
        dia = hoy.getDate();
        mes = hoy.getMonth() + 1;
        anio = hoy.getFullYear();
        if (mes < 10) {
            fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
        } else {
            fecha_actual = String(dia + "/" + mes + "/" + anio);
        }
        var Fecha = $("#Fecha").val();
        var NombreParteInvolucrada = $("#NombreParteInvolucrada").val();
        var CompromisosAdquiridos = $("#CompromisosAdquiridos").val();
        var Observaciones = $("#Observaciones").val();
        var PK_Id_Acta = $("#PK_Id_Acta").val();
        var PK_Id_Seguimiento = $("#PK_Id_Seguimiento").val();
        //if (($.datepicker.parseDate('dd/mm/yy', fecha_actual)) > ($.datepicker.parseDate('dd/mm/yy', Fecha))) {
        //    swal({
        //        type: 'warning',
        //        title: 'Estimado Usuario',
        //        text: 'La Fecha del acta no puede ser inferior a la Fecha actual',
        //        confirmButtonColor: '#7E8A97'
        //    });
        //    OcultarPopupposition();
        //}
        //else {
            var Acta = {
                Fecha: Fecha,
                NombreParteInvolucrada: NombreParteInvolucrada,
                CompromisosAdquiridos: CompromisosAdquiridos,
                Observaciones: Observaciones,
                Fk_Id_Acta: PK_Id_Acta,
                PK_Id_Seguimiento: PK_Id_Seguimiento,
            }
            $.ajax({
                url: urlBase + '/ComitesConvivencia/ActualizarSeguimiento',
                data: Acta,
                type: 'POST',
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'OK') {
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario.',
                        text: "El Acta de Compromisos ha sido guardada.",
                        confirmButtonColor: '#7E8A97'
                    });
                } else if (response != undefined && response != '' && response.Mensaje != '') {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario.',
                        text: "No se pudo guardar la información, Intente mas tarde.",
                        confirmButtonColor: '#7E8A97'
                    });
                }
                OcultarPopupposition();
            }).fail(function (response) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'No se pudo guardar la información, Intente mas tarde.',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            });
        }
    //}
}

//ADICIONAR COMPROMISOS
function AdicionarCompromisosSeguimiento() {
    if ($("#CrearActa").valid()) {
        PopupPosition();
        var CompromisosPendientes = $("#CompromisosPendientes").val();
        var PK_Id_Seguimiento = $("#PK_Id_Seguimiento").val();
        var PK_Id_Acta = $("#PK_Id_Acta").val();
        if (CompromisosPendientes.length == 0) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe Ingresar el Compromiso pendiente de seguimiento.',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        } else {
            var Miembro = {
                CompromisoPendiente: CompromisosPendientes,
                PK_Id_Acta: PK_Id_Acta,
                FK_Id_Seguimiento: PK_Id_Seguimiento,
            }
            $.ajax({
                url: urlBase + '/ComitesConvivencia/guardarCompromisoSeguimiento',
                data: Miembro,
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    $("#tCompromisos td").parent().remove();
                    $("#CompromisosPendientes").val("");
                    $("#divTmiebro").show("toogle");
                    $("#divTmiebro").trigger("reset");
                    $('#tbCompromiso').empty();
                    $('#tbCompromiso').append
                    //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                    var contador = 0;
                    var contador1 = 1;
                    $.each(result.Data, function (ind, element) {
                        var elemento = '<tr name="trCompromisos">' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="Contador1' + contador + '" value="' + contador1 + '">' + contador1 + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; "><input type="hidden" name="CompromisoPendiente" id="CompromisoPendiente' + contador + '"value="' + element.CompromisoPendiente + '">' + element.CompromisoPendiente + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarCompromiso(' + element.FK_Id_Seguimiento + ',' + element.Pk_Id_Compromiso + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                        '</tr></table>'
                        $('#tbCompromiso').append(elemento)
                        contador = contador + 1;
                        contador1 = contador1 + 1;
                    })
                    paginador("#tbCompromiso", "tr[name = trCompromisos]", "#paginadorCompromiso");

                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó, un error Intente mas tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }
}

//ELIMINAR COMPROMISOS
function EliminarCompromiso(FK_Id_Seguimiento, Pk_Id_Compromiso) {
    var PK_Id_Acta = $("#PK_Id_Acta").val();
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar el Registro?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    }, (function (isConfirm) {
        if (isConfirm) {
            PopupPosition();
            $.ajax({
                url: urlBase + '/ComitesConvivencia/eliminarCompromisoSeguimiento',
                data: {
                    FK_Id_Seguimiento: FK_Id_Seguimiento,
                    Pk_Id_Compromiso: Pk_Id_Compromiso,
                    PK_Id_Acta: PK_Id_Acta,
                },
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    if (result.Mensaje == "OK") {
                        swal(
                                  'Estimado Usuario',
                                  'Su registro ha sido eliminado',
                                  'success'
                        );
                        $("#tCompromisos td").parent().remove();
                        $("#CompromisosPendientes").val("");
                        $("#divTmiebro").show("toogle");
                        $("#divTmiebro").trigger("reset");
                        $('#tbCompromiso').empty();
                        $('#tbCompromiso').append
                        //('<table class="table table-responsive table-bordered" style="border: 2px solid lightslategray"><tr class="titulos_tabla"> <th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Documento</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Nombre Asistente</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Prioridad</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Representa</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Principales</th><th style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Acción</th></tr>');
                        var contador = 0;
                        var contador1 = 1;
                        $.each(result.Data, function (ind, element) {
                            var elemento = '<tr name="trCompromisos">' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Contador1" id="Contador1' + contador + '" value="' + contador1 + '">' + contador1 + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; "><input type="hidden" name="CompromisoPendiente" id="CompromisoPendiente' + contador + '"value="' + element.CompromisoPendiente + '">' + element.CompromisoPendiente + '</td>' +
                                            '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarCompromiso(' + element.FK_Id_Seguimiento + ',' + element.Pk_Id_Compromiso + ')" title="Eliminar" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                            '</tr></table>'
                            $('#tbCompromiso').append(elemento)
                            contador = contador + 1;
                            contador1 = contador1 + 1;
                        })
                        paginador("#tbCompromiso", "tr[name = trCompromisos]", "#paginadorCompromiso");
                    }
                    else {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó, un error Intente mas tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó, un error Intente mas tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }))

}

function ValidarDocumento() {
    jQuery.validator.addMethod("Numberonly", function (value, element) {
        return this.optional(element) || /^[0-9]+$/i.test(value);
    }, "Solo se permite el ingreso de numeros");

    $("#CrearActa").validate({
         errorClass: "error",
         rules: {
             Numero_Documento:
            {
                //required: true,
                Numberonly: true,
                min: 1,
                maxlength: 15,
            },
        },
        messages: {
            Numero_Documento:
            {
                //required: "Se debe ingresar el número de documento",
                min: "el numero de documento debe ser mayor  a cero(0)",
                maxlength: "solo se permite el ingreso de números de máximo 15 caraceres"
            },
        }


    });

}

function ValidarAdicionarMiembro() {
    jQuery.validator.addMethod("Numberonly", function (value, element) {
        return this.optional(element) || /^[0-9]+$/i.test(value);
    }, "Solo se permite el ingreso de numeros");

    $("#CrearActa").validate({
        errorClass: "error",
        rules: {
            Numero_Documento:
           {
               //required: true,
               Numberonly: true,
               min: 1,
               maxlength: 15,
           },
            //TiposPrioridadMiembros:
            //{
            //    required: true,
            // },
            //Representa:
            //{
            //    required: true,
            // },
        },
        messages: {
            Numero_Documento:
            {
                //required: "Se debe ingresar el número de documento",
                min: "el numero de documento debe ser mayor  a cero(0)",
                maxlength: "solo se permite el ingreso de números de máximo 15 caraceres"
            },
            //TiposPrioridadMiembros:
            //{
            //    required: "Se debe seleccionar el tipo de prioridad",
            //},
            //Representa:
            //{
            //    required: "Se debe seleccionar a quien representa",
            //},
        },
  
    });

}

function ValidarAdicionarParticipante() {
    jQuery.validator.addMethod("Numberonly", function (value, element) {
        return this.optional(element) || /^[0-9]+$/i.test(value);
    }, "Solo se permite el ingreso de numeros");

    $("#CrearActa").validate({
        errorClass: "error",
        rules: {
            Numero_Documento:
           {
               //required: true,
               Numberonly: true,
               min: 1,
               maxlength: 15,
           },
            //Nombre:
            //{
            //    required: true,
            //},
        },
        messages: {
            Numero_Documento:
            {
                //required: "Se debe ingresar el número de documento",
                min: "el numero de documento debe ser mayor a cero(0)",
                maxlength: "solo se permite el ingreso de números de máximo 15 caraceres"
            },
            //Nombre:
            //{
            //    required: "Se debe ingresar el nombre",
            //},
        },



    });

}

function ValidarAdicionarTemas() {
    $("#CrearActa").validate({
        errorClass: "error",
        rules: {
            TemaOrdenDia:
            {
                required: true,
            },
        },
        messages: {
            TemaOrdenDia:
            {
                required: "Se debe ingresar el tema del orden del día",
            },
        },

    });

}

function ValidarActualizarObservacion() {
    $("#CrearActa").validate({
        errorClass: "error",
        rules: {
            Observaciones:
            {
                required: true,
            },
        },
        messages: {
            Observaciones:
            {
                required: "Se debe ingresar la observación del orden del día",
            },
        }


    });

}

function ValidarAdicionarAccion() {
    $("#CrearActa").validate({
        errorClass: "error",
        rules: {
            AccionARealizar:
            {
                required: true,
            },
            Responsable:
             {
                 required: true,
             },
          },
        messages: {
            AccionARealizar:
            {
                required: "Se debe ingresar la actividad del plan de acción",
            },
            Responsable:
            {
                required: "Se debe ingresar el responsable de la actividad",
            },
        }


    });

}








