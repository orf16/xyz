
var urlBase = ""
var $idTextAreaDesarrollo = $("#DesarrolloAgenda").val();

try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};
ConstruirDatePickerPorElemento('fechapaacta');
$("#infosede").hide();
$("#labelinfosede").hide();

//renderiza en un input tipo text un datapicker para
//la selección de fechas
$(function () {
    ConstruirDatePickerPorElemento('FechaInicialRevision');
    ConstruirDatePickerPorElemento('FechaFinalRevision');
    $('#FechaFinalRevision').on("change", function (e) {
        console.log(4);
        var f1 = document.getElementById("FechaInicialRevision").value;
        var f2 = document.getElementById("FechaFinalRevision").value;

        var fecha1 = new Date(f1.split('/')[2], f1.split('/')[1] - 1, f1.split('/')[0]);
        var fecha2 = new Date(f2.split('/')[2], f2.split('/')[1] - 1, f2.split('/')[0]);

        if (f2 == '')
            return false;

        var horaInicio = '';
        var horaFin = '';

        if (fecha2.getTime() < fecha1.getTime()) {
            $('#FechaFinalRevision').val('');
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'La Fecha de finalizacion no puede ser menor a la inicial',
                confirmButtonColor: '#7E8A97'
            });
            return false;
        }
    });

    $('#FechaInicialRevision').on("change", function (e) {
        console.log(4);
        var f1 = document.getElementById("FechaInicialRevision").value;
        var f2 = document.getElementById("FechaFinalRevision").value;

        var fecha1 = new Date(f1.split('/')[2], f1.split('/')[1] - 1, f1.split('/')[0]);
        var fecha2 = new Date(f2.split('/')[2], f2.split('/')[1] - 1, f2.split('/')[0]);

        if (f2 == '')
            return false;

        var horaInicio = '';
        var horaFin = '';

        if (fecha1.getTime() > fecha2.getTime()) {
            $('#FechaInicialRevision').val('');
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'La Fecha inicial no puede ser mayor a la final',
                confirmButtonColor: '#7E8A97'
            });
            return false;
        }
    });
});

//ADICIONAR PARTICIPANTES REVISION 
function CamposAdicionar() {
    ValidarCampos();
    if ($("#formParticipantes").valid()) {
        PopupPosition();
        var NumActa = $("#NumActa").val();
        var NombreA = $("#NombreActa").val();
        var FechaCreacion = $("#FechaCreacionActa").val();
        var FechaInicialRevision = $("#FechaInicialRevision").val();
        var FechaFinalRevision = $("#FechaFinalRevision").val();
        var NombreE = $("#NombreEmpresa").val();
        var Nit = $("#NitEmpresa").val();
        var Documento = $("#Documento").val();
        var NombreP = $("#NombreParticipante").val();
        var Cargo = $("#CargoParticipante").val();
        var IdSede = $("#sedes").val();
        var IdEmpresa = $("#IdEmpresa").val();

        var ConsecutivoActa = $("#NumActa").val();
        if (NombreP.length == 0 || Documento.length == 0 || Cargo.length == 0 || IdSede < 1) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe Ingresar el documento, nombre y cargo del participante y haber seleccionado una sede.',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
        else {
            var acta = {
                NombreActa: NombreA,
                FechaCreacionActa: FechaCreacion,
                FechaInicialRevision: FechaInicialRevision,
                FechaFinalRevision: FechaFinalRevision,
                NombreEmpresa: NombreE,
                IdEmpresa: IdEmpresa,
                NitEmpresa: Nit,
                DocumentoParticipante: Documento,
                NombreParticipante: NombreP,
                CargoParticipante: Cargo,
                FKSede: IdSede,
                NumActa: ConsecutivoActa
            }
            $.ajax({
                url: urlBase + '/Revision/GuardarParticipante',
                data: acta,
                type: 'POST',
                success: function (result) {
                    $('#NumActa').val(result.Data.Num_Acta);
                    $('#IdActa').val(result.Data.PK_Id_ActaRevision);
                    var IdActa = $("#IdActa").val();
                    $.ajax({
                        url: urlBase + '/Revision/ObtenerParticipantesActa',
                        data: {
                            id_Acta: IdActa
                        },
                        type: 'POST',
                        success: function (result) {
                            OcultarPopupposition();
                            $("#tParticipantes1 td").parent().remove();
                            $("#Documento").val("");
                            $("#NombreParticipante").val("");
                            $("#CargoParticipante").val("");
                            $("#divTparticipante").show("toogle");
                            $("#divTparticipante").trigger("reset");
                            $('#tParticipantes').empty();
                            var contador = 0;
                            $.each(result.Data, function (ind, element) {
                                var elemento = '<tr name="trParticipantes">' +
                                                '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="DocumentoP" id="DocumentoP' + contador + '" value="' + element.Documento + '">' + element.Documento + '</td>' +
                                                '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="NombreP" id="NombreP' + contador + '"value="' + element.Nombre + '">' + element.Nombre + '</td>' +
                                                '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="CargoP" id ="CargoP' + contador + '"value="' + element.Cargo + '">' + element.Cargo + '</td>' +
                                                '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarParticipanteRevision(' + element.FK_ActaRevision + ',' + element.Documento + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                                '</tr></table>'
                                $('#tParticipantes').append(elemento)
                                contador = contador + 1;
                            })

                            paginador("#tParticipantes", "tr[name = trParticipantes]", "#paginadorParticipantes")
                        },
                        error: function (result) {
                            swal({
                                type: 'error',
                                title: 'Estimado Usuario',
                                text: 'Se presentó un error, intente más tarde.',
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
                        text: 'Se presentó un error, intente más tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }
}

function ValidarCampos() {
    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[ a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$/i.test(value);
    }, "Solo se permite el ingreso de letras");

    $("#formParticipantes").validate({
        // errorClass: "my-error-class",
        rules: {
            NombreActa: {
                required: true
            },
            FechaCreacionActa: {
                required: true
            },
            FechaInicialRevision: {
                required: true
            },
            FechaFinalRevision: {
                required: true
            },
            NombreEmpresa: {
                required: true
            },
            sedes: {
                required: true,
                min: 1
            },
            FK_Sede: {
                required: true,
                min: 1
            }
        },
        messages: {
            NombreActa: {
                required: "Nombre de Acta obligatorio"
            },
            FechaCreacionActa: {
                required: "Fecha de Creacion de Acta obligatorio"
            },
            FechaInicialRevision: {
                required: "Fecha Inicial de Revision obligatoria"
            },
            FechaFinalRevision: {
                required: "Fecha Final de Revision obligatoria"
            },
            NombreEmpresa: {
                required: "Nombre Empresa obligatoria"
            },
            sedes: {
                required: "Se debe seleccionar una sede",
                min: "Se debe seleccionar una sede"
            },
            FK_Sede: {
                required: "Se debe seleccionar una sede",
                min: "Se debe seleccionar una sede"
            }
        }

    });

    if ($("#formParticipantes").valid()) {
        //AsignarAtributosPestañaSiguiente(numberTab);
        //removerAtributosPestañaAnterior(numberTab - 1);
    }
}

//Consulta la informacion del trabajador
function DatosTrabajador() {
    var documento = $("#Documento").val();
    if (documento == '')
        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'Ingrese un número de Documento válido',
            confirmButtonColor: '#7E8A97'
        });
    PopupPosition();
    $.ajax({
        type: "post",
        data: { numeroDocumento: documento },
        url: urlBase + '/Revision/ConsultarDatosTrabajador'
    }).done(function (response) {
        if (response != undefined && response != '' && response.Mensaje == 'OK') {
            var trabajador = response.Data;
            var nombre = trabajador.nombre1 + ' ' + trabajador.nombre2 + ' ' + trabajador.apellido1 + ' ' + trabajador.apellido2;
            $('#NombreParticipante').val(nombre);
            $('#CargoParticipante').val(trabajador.ocupacion);
        } else if (response != undefined && response != '' && response.Mensaje != '') {
            $("#Documento").val('');
            $("#Nombre").val('');
            $('#CargoParticipante').val('');
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: response.Data,
                confirmButtonColor: '#7E8A97'
            });
        }
        OcultarPopupposition();
    }).fail(function (response) {
        $("#Documento").val('');
        $("#Nombre").val('');
        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'No se pudo obtener la información, intente más tarde.',
            confirmButtonColor: '#7E8A97'
        });
        OcultarPopupposition();
    });
}

$("#ContinuarParticipante").click(function (e) {
    ValidarCampos();
    if ($("#formParticipantes").valid()) {
        PopupPosition();
        var NumActa = $("#NumActa").val();
        var NombreA = $("#NombreActa").val();
        var FechaCreacion = $("#FechaCreacionActa").val();
        var FechaInicialRevision = $("#FechaInicialRevision").val();
        var FechaFinalRevision = $("#FechaFinalRevision").val();
        var NombreE = $("#NombreEmpresa").val();
        var Nit = $("#NitEmpresa").val();
        var Documento = $("#Documento").val();
        var NombreP = $("#NombreParticipante").val();
        var Cargo = $("#CargoParticipante").val();
        var IdSede = $("#sedes").val();
        var IdEmpresa = $("#IdEmpresa").val();
        var ConsecutivoActa = $("#NumActa").val();
        var IdActa = $("#IdActa").val();

        if (IdSede < 1 || ConsecutivoActa == 0) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe agregar al menos un participante.',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
        else {
            var acta = {
                PKActaRevision: IdActa,
                NombreActa: NombreA,
                FechaCreacionActa: FechaCreacion,
                FechaInicialRevision: FechaInicialRevision,
                FechaFinalRevision: FechaFinalRevision,
                NombreEmpresa: NombreE,
                IdEmpresa: IdEmpresa,
                NitEmpresa: Nit,
                DocumentoParticipante: Documento,
                NombreParticipante: NombreP,
                CargoParticipante: Cargo,
                FKSede: IdSede,
                NumActa: ConsecutivoActa
            }
            $.ajax({
                url: urlBase + '/Revision/GuardarActa',
                data: acta,
                type: 'POST',
                success: function (result) {
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario',
                        text: 'La información se registró exitosamente',
                        showConfirmButton: true,
                        confirmButtonText: 'Continuar',
                        confirmButtonColor: '#7E8A97'
                    });
                    $('.confirm').on('click', function () {
                        $('#IdActa').val(result.Data.PK_Id_ActaRevision);
                        var IdActa = $("#IdActa").val();
                        window.location.href = "../Revision/TemasActaRevision?IdActa=" + IdActa;
                        OcultarPopupposition();
                    });
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó un error, intente más tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }
});

$("#ContinuarTema").click(function (e) {
    if ($("#formTemas").valid()) {
        PopupPosition();
        var IdActa = $("#IdActa").val();
        $.ajax({
            url: urlBase + '/Revision/ValidarTemas',
            data: {
                id: IdActa
            },
            type: 'POST',
            success: function (result) {
                if (result.Mensaje == "ERROR") {
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'Debe agregar al menos un tema y desarrollar todos los temas agregados.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
                else {
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario',
                        text: 'La información se registró exitosamente',
                        showConfirmButton: true,
                        confirmButtonText: 'Continuar',
                        confirmButtonColor: '#7E8A97'
                    });
                    $('.confirm').on('click', function () {
                        window.location.href = "../Revision/PlanAccionActaRevision?IdActa=" + IdActa;
                        OcultarPopupposition();
                    });
                }
            },
            error: function (result) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'Se presentó un error, intente más tarde.',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
        });
    }
});

//Llama al accion que muestra la vista para crear el acta con todos los datos que tenga guardados
function ObtenerActa() {
    var IdActa = $("#IdActa").val();
    if (IdActa > 0) {
        PopupPosition();
        $.ajax({
            url: urlBase + '/Revision/ObtenerDatosActaRevision',//primero el modulo/controlador/metodo que esta en el controlador
            data: {
                PK_Id_Acta: IdActa
            },
            type: 'POST',
            success: function (result) {
                OcultarPopupposition();
                $("#NumActa").val(result.Data.NumActa);
                $("#NombreActa").val(result.Data.NombreActa);
                $("#FechaCreacionActa").val(moment(result.Data.FechaCreacionActa).format("DD/MM/YYYY"));
                $("#FechaInicialRevision").val(moment(result.Data.FechaInicialRevision).format("DD/MM/YYYY"));
                $("#FechaFinalRevision").val(moment(result.Data.FechaFinalRevision).format("DD/MM/YYYY"));
                $("#NombreEmpresa").val(result.Data.NombreEmpresa);
                $("#NitEmpresa").val(result.Data.NitEmpresa);
                $("#sedes").val(result.Data.FKSede);
                $("#IdEmpresa").val(result.Data.IdEmpresa);
                $("#IdActa").val(result.Data.PKActaRevision);

                $("#tParticipantes1 td").parent().remove();
                $("#Documento").val("");
                $("#NombreParticipante").val("");
                $("#CargoParticipante").val("");
                $("#divTparticipante").show("toogle");
                $("#divTparticipante").trigger("reset");
                $('#tParticipantes').empty();
                var contador = 0;
                $.each(result.Data.ParticipantesActa, function (ind, element) {
                    var elemento = '<tr name="trParticipantes">' +
                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="DocumentoP" id="DocumentoP' + contador + '" value="' + element.DocumentoParticipante + '">' + element.DocumentoParticipante + '</td>' +
                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="NombreP" id="NombreP' + contador + '"value="' + element.NombreParticipante + '">' + element.NombreParticipante + '</td>' +
                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="CargoP" id ="CargoP' + contador + '"value="' + element.CargoParticipante + '">' + element.CargoParticipante + '</td>' +
                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarParticipanteRevision(' + element.FKActaRevision + ',' + element.DocumentoParticipante + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                    '</tr></table>'
                    $('#tParticipantes').append(elemento)
                    contador = contador + 1;
                })

                paginador("#tParticipantes", "tr[name = trParticipantes]", "#paginadorParticipantes")







                if (result.Data) {
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario',
                        text: 'Puede iniciar con la creación del acta.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            },
            error: function (result) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'No se pudo obtener la información, intente más tarde.',
                    confirmButtonColor: '#7E8A97',
                    confirmButtonText: "Aceptar",
                });
                OcultarPopupposition();
            }
        })
    }
}

//AGREGAR TEMA A LA AGENDA
function AdicionarTema() {
    if ($("#formTemas").valid()) {
        PopupPosition();
        var ItemAdicional = $("#ItemAdicional").val();
        var items = $("#items").val();
        if (items == "") {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe seleccionar el título del tema.',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
        else {

            if ((ItemAdicional == undefined || ItemAdicional == "") && items == "Otro") {
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'Debe ingresar el título del tema adicional.',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
            else {
                var Tema;
                if (items != "Otro") {
                    Tema = $("#items").val();
                }
                else {
                    Tema = ItemAdicional;
                }
                var PKacta = $("#IdActa").val();
                var temaAgenda = {
                    PKActaRevision: PKacta,
                    Item: Tema
                }
                $.ajax({
                    url: urlBase + '/Revision/GuardarItem',
                    data: temaAgenda,
                    type: 'POST',
                    success: function (result) {
                        $('#IdActa').val(result.Data.PK_Id_ActaRevision);
                        var IdActa = $("#IdActa").val();
                        $.ajax({
                            url: urlBase + '/Revision/ObtenerAgendaPorActa',
                            data: {
                                id: IdActa
                            },
                            type: 'POST',
                            success: function (result) {
                                OcultarPopupposition();
                                $("#ItemAdicional").val("");
                                $("#items").val("");
                                $("#inputOtro").attr("hidden", "hidden");
                                $("#tTemas1 td").parent().remove();
                                $("#divTtema").show("toogle");
                                $("#divTtema").trigger("reset");
                                $('#tTemas').empty();
                                var contador = 0;
                                $.each(result.Data, function (ind, element) {
                                    if (element.Desarrollo != "") {
                                        var elemento = '<tr name="trTemas">' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="IdTituloAgenda" id="IdTituloAgenda' + contador + '"value="' + element.PK_Id_Agenda + '"><input type="hidden" name="Titulo" id="Titulo' + contador + '" value="' + element.Titulo + '">' + element.Titulo + '</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Si</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonEditar" onclick="EditarTemaAgendaRevision(' + element.FK_ActaRevision + ',' + element.PK_Id_Agenda + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-refresh"></span></a></td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarTemaAgendaRevision(' + element.FK_ActaRevision + ',' + element.PK_Id_Agenda + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                                        '</tr></table>'
                                    }
                                    else {
                                        var elemento = '<tr name="trTemas">' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="IdTituloAgenda" id="IdTituloAgenda' + contador + '"value="' + element.PK_Id_Agenda + '"><input type="hidden" name="Titulo" id="Titulo' + contador + '" value="' + element.Titulo + '">' + element.Titulo + '</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">No</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonEditar" onclick="EditarTemaAgendaRevision(' + element.FK_ActaRevision + ',' + element.PK_Id_Agenda + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-refresh"></span></a></td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarTemaAgendaRevision(' + element.FK_ActaRevision + ',' + element.PK_Id_Agenda + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                                        '</tr></table>'
                                    }

                                    $('#tTemas').append(elemento)
                                    contador = contador + 1;
                                })

                                paginador("#tTemas", "tr[name = trTemas]", "#paginadorTemas")
                            },
                            error: function (result) {
                                swal({
                                    type: 'error',
                                    title: 'Estimado Usuario',
                                    text: 'Se presentó un error, intente más tarde.',
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
                            text: 'Se presentó un error, intente más tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                });
            }
        }
    }
}

// ELIMINAR TEMA ACTA REVISION
function EliminarTemaAgendaRevision(id_Acta, idTema) {
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar el Tema del Acta de Revisión?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#7E8A97',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    }, (function (isConfirm) {
        if (isConfirm) {
            PopupPosition();
            $.ajax({
                url: urlBase + '/Revision/BorrarTemaAgendaRevision',
                data: {
                    IdTema: idTema,
                    FK_Id_Acta: id_Acta
                },
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    if (result.Mensaje == "OK") {
                        swal({
                            type: 'success',
                            title: 'Estimado Usuario',
                            text: 'Su registro ha sido eliminado.',
                            confirmButtonColor: '#7E8A97'
                        });
                        $.ajax({
                            url: urlBase + '/Revision/ObtenerAgendaPorActa',
                            data: {
                                id: id_Acta
                            },
                            type: 'POST',
                            success: function (result) {
                                OcultarPopupposition();
                                $("#tTemas1 td").parent().remove();
                                $("#divTtema").show("toogle");
                                $("#divTtema").trigger("reset");
                                $('#tTemas').empty();
                                var contador = 0;
                                $.each(result.Data, function (ind, element) {
                                    if (element.Desarrollo != "") {
                                        var elemento = '<tr name="trTemas">' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="IdTituloAgenda" id="IdTituloAgenda' + contador + '"value="' + element.PK_Id_Agenda + '"><input type="hidden" name="Titulo" id="Titulo' + contador + '" value="' + element.Titulo + '">' + element.Titulo + '</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Si</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonEditar" onclick="EditarTemaAgendaRevision(' + element.FK_ActaRevision + ',' + element.PK_Id_Agenda + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-refresh"></span></a></td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarTemaAgendaRevision(' + element.FK_ActaRevision + ',' + element.PK_Id_Agenda + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                                        '</tr></table>'
                                    }
                                    else {
                                        var elemento = '<tr name="trTemas">' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="IdTituloAgenda" id="IdTituloAgenda' + contador + '"value="' + element.PK_Id_Agenda + '"><input type="hidden" name="Titulo" id="Titulo' + contador + '" value="' + element.Titulo + '">' + element.Titulo + '</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">No</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonEditar" onclick="EditarTemaAgendaRevision(' + element.FK_ActaRevision + ',' + element.PK_Id_Agenda + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-refresh"></span></a></td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarTemaAgendaRevision(' + element.FK_ActaRevision + ',' + element.PK_Id_Agenda + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                                        '</tr></table>'
                                    }
                                    $('#tTemas').append(elemento)
                                    contador = contador + 1;
                                })

                                paginador("#tTemas", "tr[name = trTemas]", "#paginadorTemas")
                            },
                            error: function (result) {
                                swal({
                                    type: 'error',
                                    title: 'Estimado Usuario',
                                    text: 'Se presentó un error, intente más tarde.',
                                    confirmButtonColor: '#7E8A97'
                                });
                                OcultarPopupposition();
                            }
                        });
                    }
                    else {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó un error, intente más tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó un error, intente más tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }))

}

//AGREGAR DESARROLLO DEL TEMA DE LA AGENDA
function AdicionarDesarrolloTemaAgendaRevision() {
    if ($("#formDesarrollo").valid()) {
        PopupPosition();
        var IdActa = $("#IdActa").val();
        var IdTema = $("#IdAgenda").val();
        var Tema = $("#TituloAgenda").val();
        var Desarrollo = CKEDITOR.instances['DesarrolloAgenda'].getData();
        //var Desarrollo = $("#DesarrolloAgenda").val();
        if (Desarrollo == undefined || Desarrollo == "") {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe ingresar el desarrollo del tema.',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
        else {
            var acta = {
                FKActa: IdActa,
                Item: Tema,
                DesarrolloItem: Desarrollo,
                FKItem: IdTema
            }
            $.ajax({
                url: urlBase + '/Revision/GuardarDesarrolloTema',
                data: acta,
                type: 'POST',
                success: function (result) {
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario',
                        text: 'La información se registró exitosamente',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó un error, intente más tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }
}

//Funcion para consultar las clases de peligros por cada tipo de peligro
function ConsultarTipoTema(tipoTema) {

    var $tipoTema = $("#items");
    var $inputOtro = $("#inputOtro");
    if ($tipoTema.val() == "Otro") {
        $inputOtro.removeAttr("hidden");
    }
    else {
        $inputOtro.attr("hidden", "hidden");
    }
}

function ObtenerTemas() {
    if ($("#formTemas").valid()) {
        PopupPosition();
        var IdActa = $("#IdActa").val();
        $.ajax({
            url: urlBase + '/Revision/ObtenerAgendaPorActa',
            data: {
                id: IdActa
            },
            type: 'POST',
            success: function (result) {
                OcultarPopupposition();
                $("#ItemAdicional").val("");
                $("#items").val("");
                $("#tTemas1 td").parent().remove();
                $("#divTtema").show("toogle");
                $("#divTtema").trigger("reset");
                $('#tTemas').empty();
                var contador = 0;
                $.each(result.Data, function (ind, element) {
                    if (element.Desarrollo != "") {
                        var elemento = '<tr name="trTemas">' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="IdTituloAgenda" id="IdTituloAgenda' + contador + '"value="' + element.PK_Id_Agenda + '"><input type="hidden" name="Titulo" id="Titulo' + contador + '" value="' + element.Titulo + '">' + element.Titulo + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Si</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonEditar" onclick="EditarTemaAgendaRevision(' + element.FK_ActaRevision + ',' + element.PK_Id_Agenda + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-refresh"></span></a></td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarTemaAgendaRevision(' + element.FK_ActaRevision + ',' + element.PK_Id_Agenda + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                        '</tr></table>'
                    }
                    else {
                        var elemento = '<tr name="trTemas">' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="IdTituloAgenda" id="IdTituloAgenda' + contador + '"value="' + element.PK_Id_Agenda + '"><input type="hidden" name="Titulo" id="Titulo' + contador + '" value="' + element.Titulo + '">' + element.Titulo + '</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">No</td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonEditar" onclick="EditarTemaAgendaRevision(' + element.FK_ActaRevision + ',' + element.PK_Id_Agenda + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-refresh"></span></a></td>' +
                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarTemaAgendaRevision(' + element.FK_ActaRevision + ',' + element.PK_Id_Agenda + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                        '</tr></table>'
                    }
                    $('#tTemas').append(elemento)
                    contador = contador + 1;
                })

                paginador("#tTemas", "tr[name = trTemas]", "#paginadorTemas")
            },
            error: function (result) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'Se presentó un error, intente más tarde.',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
        });
    }
}
function EditarTemaAgendaRevision(IdActa,IdAgenda) {
    PopupPosition();
    //var IdActa = $("#IdActa").val();
    //var IdAgenda = $("#IdAgenda").val();

    swal({
        type: 'success',
        title: 'Estimado Usuario',
        text: 'Puede registrar el Desarrollo del Tema',
        showConfirmButton: true,
        confirmButtonText: 'Continuar',
        confirmButtonColor: '#7E8A97'
    });
    $('.confirm').on('click', function () {
        window.location.href = "../Revision/DesarrolloTemaActaRevision?IdActa=" + IdActa + "&IdAgenda=" + IdAgenda;
        OcultarPopupposition();
    });
}

function ObtenerDesarrolloTema() {
    if ($("#formDesarrollo").valid()) {
        PopupPosition();
        var IdActa = $("#IdActa").val();
        var IdTema = $("#IdAgenda").val();
        $.ajax({
            url: urlBase + '/Revision/ObtenerTemaAgendaPorActa',
            data: {
                id: IdTema
            },
            type: 'POST',
            success: function (result) {
                OcultarPopupposition();
                $("#TituloAgenda").val(result.Data.Titulo);
                $("#DesarrolloAgenda").text(result.Data.Desarrollo);
                //$idTextAreaDesarrollo.text(result.Data.Desarrollo);
                //$("#tTemas1 td").parent().remove();
                //$("#divTtema").show("toogle");
                //$("#divTtema").trigger("reset");
                //$('#tTemas').empty();
                //var contador = 0;
                //$.each(result.Data, function (ind, element) {
                //if (element.Desarrollo != "") {
                //    var elemento = '<tr name="trTemas">' +
                //                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="IdTituloAgenda" id="IdTituloAgenda' + contador + '"value="' + element.PK_Id_Agenda + '"><input type="hidden" name="Titulo" id="Titulo' + contador + '" value="' + element.Titulo + '">' + element.Titulo + '</td>' +
                //                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">Si</td>' +
                //                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonEditar" onclick="EditarTemaAgendaRevision(' + element.FK_ActaRevision + ',' + element.PK_Id_Agenda + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-refresh"></span></a></td>' +
                //                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarTemaAgendaRevision(' + element.FK_ActaRevision + ',' + element.PK_Id_Agenda + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                //                    '</tr></table>'
                //}
                //else {
                //    var elemento = '<tr name="trTemas">' +
                //                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="IdTituloAgenda" id="IdTituloAgenda' + contador + '"value="' + element.PK_Id_Agenda + '"><input type="hidden" name="Titulo" id="Titulo' + contador + '" value="' + element.Titulo + '">' + element.Titulo + '</td>' +
                //                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center">No</td>' +
                //                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonEditar" onclick="EditarTemaAgendaRevision(' + element.FK_ActaRevision + ',' + element.PK_Id_Agenda + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-refresh"></span></a></td>' +
                //                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarTemaAgendaRevision(' + element.FK_ActaRevision + ',' + element.PK_Id_Agenda + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                //                    '</tr></table>'
                //}
                //    $('#tTemas').append(elemento)
                //    contador = contador + 1;
                //})

                //paginador("#tTemas", "tr[name = trTemas]", "#paginadorTemas")
            },
            error: function (result) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'Se presentó un error, intente más tarde.',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
        });
    }
}

function CargarActas() {
    if ($("#formActas").valid()) {
        PopupPosition();
        $.ajax({
            url: urlBase + '/Revision/CargarActas',
            type: 'POST',
            success: function (result) {
                OcultarPopupposition();
                $("#tActas1 td").parent().remove();
                $("#divTacta").show("toogle");
                $("#divTacta").trigger("reset");
                $('#tActas').empty();
                var contador = 0;
                $.each(result.Data.Actas, function (ind, element) {
                    var fechaConvertida = moment(element.FechaCreacionActa).format("DD/MM/YYYY");
                    var elemento = '<tr name="trActas">' +
                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="IdActa" id="IdActa' + contador + '"value="' + element.PKActa + '"><input type="hidden" name="NActa" id="NActa' + contador + '" value="' + element.NumActa + '">' + element.NumActa + '</td>' +
                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="NomActa" id="NomActa' + contador + '" value="' + element.NombreActa + '">' + element.NombreActa + '</td>' +
                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="FecActa" id="FecActa' + contador + '" value="' + fechaConvertida + '">' + fechaConvertida + '</td>' +
                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonEditar" href="Index?IdActa=' + element.PKActa + '" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-pencil"></span></a></td>' +
                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonVisualizar" href="Index?IdActa=' + element.PKActa + '" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-search"></span></a></td>' +
                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarActaRevision(' + element.PKActa + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                    '</tr></table>'
                    $('#tActas').append(elemento)
                    contador = contador + 1;
                })

                paginador("#tActas", "tr[name = trActas]", "#paginadorActas")
            },
            error: function (result) {
                swal({
                    type: 'error',
                    title: 'Estimado Usuario',
                    text: 'Se presentó un error, intente más tarde.',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
        });
    }
}

// ELIMINAR ACTA REVISION
function EliminarActaRevision(id_Acta) {
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar el Acta de Revisión?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#7E8A97',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    }, (function (isConfirm) {
        if (isConfirm) {
            PopupPosition();
            $.ajax({
                url: urlBase + '/Revision/BorrarActaRevision',
                data: {
                    FK_Id_Acta: id_Acta
                },
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    if (result.Mensaje == "OK") {
                        swal({
                            type: 'success',
                            title: 'Estimado Usuario',
                            text: 'Su registro ha sido eliminado.',
                            confirmButtonColor: '#7E8A97'
                        });
                        $.ajax({
                            url: urlBase + '/Revision/CargarActas',
                            data: {
                                id: id_Acta
                            },
                            type: 'POST',
                            success: function (result) {
                                OcultarPopupposition();
                                $("#tActas1 td").parent().remove();
                                $("#divTacta").show("toogle");
                                $("#divTacta").trigger("reset");
                                $('#tActas').empty();
                                var contador = 0;
                                $.each(result.Data.Actas, function (ind, element) {
                                    var fechaConvertida = moment(element.FechaCreacionActa).format("DD/MM/YYYY");
                                    var elemento = '<tr name="trActas">' +
                                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="IdActa" id="IdActa' + contador + '"value="' + element.PKActa + '"><input type="hidden" name="NActa" id="NActa' + contador + '" value="' + element.NumActa + '">' + element.NumActa + '</td>' +
                                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="NomActa" id="NomActa' + contador + '" value="' + element.NombreActa + '">' + element.NombreActa + '</td>' +
                                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="FecActa" id="FecActa' + contador + '" value="' + fechaConvertida + '">' + fechaConvertida + '</td>' +
                                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonEditar" href="Index?IdActa=' + element.PKActa + '" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-pencil"></span></a></td>' +
                                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonVisualizar" href="Index?IdActa=' + element.PKActa + '" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-search"></span></a></td>' +
                                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarActaRevision(' + element.PKActa + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                                    '</tr></table>'
                                    $('#tActas').append(elemento)
                                    contador = contador + 1;
                                })

                                paginador("#tActas", "tr[name = trActas]", "#paginadorActas")
                            },
                            error: function (result) {
                                swal({
                                    type: 'error',
                                    title: 'Estimado Usuario',
                                    text: 'Se presentó un error, intente más tarde.',
                                    confirmButtonColor: '#7E8A97'
                                });
                                OcultarPopupposition();
                            }
                        });
                    }
                    else {
                        if (result.Mensaje == "ERRORPLAN") {
                            swal({
                                type: 'warning',
                                title: 'Estimado Usuario',
                                text: 'El acta no puede ser eliminada porque ya tiene cerradas una o más actividades del plan de acción.',
                                confirmButtonColor: '#7E8A97'
                            });
                            OcultarPopupposition();
                        }
                        else {
                            swal({
                                type: 'error',
                                title: 'Estimado Usuario',
                                text: 'Se presentó un error, intente más tarde.',
                                confirmButtonColor: '#7E8A97'
                            });
                            OcultarPopupposition();
                        }
                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó un error, intente más tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }))

}

function ValidarTamañoDocumento() {
    if (typeof FileReader !== "undefined") {
        var size = document.getElementById('archivo').files[0].size;

        if (size > 10800332.8) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe cargar un archivo con peso menor a 10 MB.',
                //timer: 4000,
                confirmButtonColor: '#7E8A97'
            })
            document.getElementById("archivo").value = "";
        }
    }
}

function AdjuntarArchivo() {
    var formData = new FormData();
    var file = document.getElementById("archivo").files[0];
    var idAgenda = $("#IdAgenda").val();
    formData.append("archivo", file);
    formData.append("idAgenda", idAgenda);
    //var xhr = new XMLHttpRequest();
    //xhr.open("POST", "/Revision/AgregarAdjunto", true);
    //xhr.addEventListener("load", function (evt) { UploadComplete(evt); }, false);
    //xhr.addEventListener("error", function (evt) { UploadFailed(evt); }, false);
    //xhr.send(formData);
    $.ajax({
        type: "POST",
        url: '/Revision/AgregarAdjunto',
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (result) {
            if (result.Mensaje == "ERROR") {
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'Se presentó un error, intente más tarde.',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
            }
            else {
                document.getElementById("archivo").value = "";
                if (result.Mensaje == "ERRORCONTADOR") {
                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'Solo se pueden adjuntar 3 archivos por tema.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
                else {
                    if (result.Mensaje == "ERRORTIPO") {
                        swal({
                            type: 'warning',
                            title: 'Estimado Usuario',
                            text: 'Debe seleccionar solo archivos de tipo JPG, PNG, PDF o EXCEL.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                    else {
                        if (result.Mensaje == "ERRORVACIO") {
                            swal({
                                type: 'warning',
                                title: 'Estimado Usuario',
                                text: 'Debe seleccionar un archivo.',
                                confirmButtonColor: '#7E8A97'
                            });
                            OcultarPopupposition();
                        }
                        else {
                            swal({
                                type: 'success',
                                title: 'Estimado Usuario',
                                text: 'El archivo se agregó exitosamente',
                                confirmButtonColor: '#7E8A97'
                            });
                            $.ajax({
                                url: urlBase + '/Revision/CargarTemaAgenda',
                                data: {
                                    id: idAgenda
                                },
                                type: 'POST',
                                success: function (result) {
                                    OcultarPopupposition();
                                    $("#tAdjuntos1 td").parent().remove();
                                    $("#divTadjunto").show("toogle");
                                    $("#divTadjunto").trigger("reset");
                                    $('#tAdjuntos').empty();
                                    var contador = 0;
                                    $.each(result.Data.AdjuntosAgendaRevision, function (ind, element) {
                                        var elemento = '<tr name="trAdjuntos">' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="NomAdjunto" id="NomAdjunto' + contador + '" value="' + element.Nombre_Archivo + '">' + element.Nombre_Archivo + '</td>' +
                                                        '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarAdjuntoTemaActaRevision(' + element.PK_Id_AdjuntoAgendaRevision + ',' + element.FK_AgendaRevision + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                                        '</tr></table>'
                                        $('#tAdjuntos').append(elemento)
                                        contador = contador + 1;
                                    })

                                    paginador("#tAdjuntos", "tr[name = trAdjuntos]", "#paginadorAdjuntos")
                                },
                                error: function (result) {
                                    swal({
                                        type: 'error',
                                        title: 'Estimado Usuario',
                                        text: 'Se presentó un error, intente más tarde.',
                                        confirmButtonColor: '#7E8A97'
                                    });
                                    OcultarPopupposition();
                                }
                            });

                        }
                    }
                }
            }
        },
        error: function (result) {
            swal({
                type: 'error',
                title: 'Estimado Usuario',
                text: 'Se presentó un error, intente más tarde.',
                confirmButtonColor: '#7E8A97'
            })
        }
    });
}

function UploadComplete(evt) {
    if (evt.target.status == 200)
        alert("Archivo cargado exitosamente.");

    else
        alert("Error cargando el archivo.");
}

function UploadFailed(evt) {
    alert("Se presentó un error al subir el archivo.");

}
// ELIMINAR ADJUNTO TEMA ACTA REVISION
function EliminarAdjuntoTemaActaRevision(id_Adjunto,id_Agenda) {
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar el Adjunto del Tema del Acta Revisión?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#7E8A97',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    }, (function (isConfirm) {
        if (isConfirm) {
            PopupPosition();
            $.ajax({
                url: urlBase + '/Revision/BorrarAdjuntoTemaActaRevision',
                data: {
                    idAdjunto: id_Adjunto,
                    idAgenda: id_Agenda
                },
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    if (result.Mensaje == "OK") {
                        swal({
                            type: 'success',
                            title: 'Estimado Usuario',
                            text: 'Su registro ha sido eliminado.',
                            confirmButtonColor: '#7E8A97'
                        });
                        $.ajax({
                            url: urlBase + '/Revision/CargarTemaAgenda',
                            data: {
                                id: id_Agenda
                            },
                            type: 'POST',
                            success: function (result) {
                                OcultarPopupposition();
                                $("#tAdjuntos1 td").parent().remove();
                                $("#divTadjunto").show("toogle");
                                $("#divTadjunto").trigger("reset");
                                $('#tAdjuntos').empty();
                                var contador = 0;
                                $.each(result.Data.AdjuntosAgendaRevision, function (ind, element) {
                                    var elemento = '<tr name="trAdjuntos">' +
                                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="NomAdjunto" id="NomAdjunto' + contador + '" value="' + element.Nombre_Archivo + '">' + element.Nombre_Archivo + '</td>' +
                                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarAdjuntoTemaActaRevision(' + element.PK_Id_AdjuntoAgendaRevision + ',' + element.FK_AgendaRevision + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                                    '</tr></table>'
                                    $('#tAdjuntos').append(elemento)
                                    contador = contador + 1;
                                })

                                paginador("#tAdjuntos", "tr[name = trAdjuntos]", "#paginadorAdjuntos")
                            },
                            error: function (result) {
                                swal({
                                    type: 'error',
                                    title: 'Estimado Usuario',
                                    text: 'Se presentó un error, intente más tarde.',
                                    confirmButtonColor: '#7E8A97'
                                });
                                OcultarPopupposition();
                            }
                        });
                    }
                    else {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó un error, intente más tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó un error, intente más tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }))

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

// ELIMINAR PARTICIPANTE ACTA REVISION
function EliminarParticipanteRevision(id_Acta, documento) {
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar el Participante del Acta de Revisión?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#7E8A97',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    }, (function (isConfirm) {
        if (isConfirm) {
            PopupPosition();
            $.ajax({
                url: urlBase + '/Revision/BorrarParticipanteRevision',
                data: {
                    FK_Id_Acta: id_Acta,
                    Documento: documento
                },
                type: 'POST',
                success: function (result) {
                    OcultarPopupposition();
                    if (result.Mensaje == "OK") {
                        swal({
                            type: 'success',
                            title: 'Estimado Usuario',
                            text: 'Su registro ha sido eliminado.',
                            confirmButtonColor: '#7E8A97'
                        });
                        $.ajax({
                            url: urlBase + '/Revision/ObtenerParticipantesActa',
                            data: {
                                id_Acta: id_Acta
                            },
                            type: 'POST',
                            success: function (result) {
                                OcultarPopupposition();
                                $("#tParticipantes1 td").parent().remove();
                                $("#Documento").val("");
                                $("#NombreParticipante").val("");
                                $("#CargoParticipante").val("");
                                $("#divTparticipante").show("toogle");
                                $("#divTparticipante").trigger("reset");
                                $('#tParticipantes').empty();
                                var contador = 0;
                                $.each(result.Data, function (ind, element) {
                                    var elemento = '<tr name="trParticipantes">' +
                                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="DocumentoP" id="DocumentoP' + contador + '" value="' + element.Documento + '">' + element.Documento + '</td>' +
                                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="NombreP" id="NombreP' + contador + '"value="' + element.Nombre + '">' + element.Nombre + '</td>' +
                                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="CargoP" id ="CargoP' + contador + '"value="' + element.Cargo + '">' + element.Cargo + '</td>' +
                                                    '<td style="border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><a id="botonBorrar" onclick="EliminarParticipanteRevision(' + element.FK_ActaRevision + ',' + element.Documento + ')" class="btn btn-search btn-md"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                                    '</tr></table>'
                                    $('#tParticipantes').append(elemento)
                                    contador = contador + 1;
                                })

                                paginador("#tParticipantes", "tr[name = trParticipantes]", "#paginadorParticipantes")
                            },
                            error: function (result) {
                                swal({
                                    type: 'error',
                                    title: 'Estimado Usuario',
                                    text: 'Se presentó un error, intente más tarde.',
                                    confirmButtonColor: '#7E8A97'
                                });
                                OcultarPopupposition();
                            }
                        });
                    }
                    else {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó un error, intente más tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó un error, intente más tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                    OcultarPopupposition();
                }
            });
        }
    }))
}


    //<---Funciones para Revision por Robinson.
/////Funcion para Validar las Firmas del Representante y Responsable
function validaFirmas() {
    PopupPosition();
    var idacta = $("#fkacta").val();
    $.ajax({
        url: urlBase + '/Revision/ValidarFirmas',
        data: { idacta: idacta },
        type: 'POST',
        success: function (result) {
            if (result) {
                OcultarPopupposition();
                if (result.Data.Firma_Representante_SGSST == true) {
                    $("#idcheckfirmareplegal").prop('checked', true);
                }
                if (result.Data.Firma_Responsable_SGSST == true) {
                    $("#idcheckfirmaresponsable").prop('checked', true);
                }
                if (result.Data.Elaborada != null && result.Data.Elaborada != "") {
                    $("#ElaboradaPor").val(result.Data.Elaborada);
                }
                if (result.Data.Firma_Gerente_General != null && result.Data.Firma_Gerente_General != "") {
                    $("#NombreImagenGerente").html(result.Data.Firma_Gerente_General);
                }
            }
            else {


            }
        },
        error: function (result) {
            swal({
                type: 'error',
                title: 'Estimado Usuario',
                text: 'Se presentó un error en la transacción, intente más tarde.',
                confirmButtonColor: '#7E8A97'
            });
            OcultarPopupposition();
        }
    });
}

    //Funcion para cargar automaticamente los Planes por Acta.

    function cargarplanes() {
        PopupPosition();
        var idacta = $("#fkacta").val();
        $.ajax({
            url: urlBase + '/Revision/CargarPlanes',
            data: { idacta: idacta },
            type: 'POST',
            success: function (result) {
                if (result.Data) {
                    OcultarPopupposition();
                    //$("#formplan1").modal("hide");
                    $('#planaccionesrevision').empty();
                    $('#planaccionesrevision').append;
                    var contadorcs = 0;
                    $.each(result.Data, function (ind, element) {
                        var Fecha = moment(result.Data[ind].Fecha).format("DD/MM/YYYY");
                        var elementoscs = '<tr name="planrevisiones" id="contador">' +
                                        //'<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center">' + '<input type="checkbox" name="condicionesIns" name1="' + element.EDDiasDesde + '" name2="' + element.EDDiasHasta + '" desc="' + element.EDDescribeCondicion + '" value="' + element.EDpkCondicion + '" id=' + contadorcs + '  class ="condicionesIns">' + '</td>' +
                                        '<td style="vertical-align:middle; border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Actividad" id="Actividad' + contadorcs + '"value="' + element.Actividad + '">' + element.Actividad + '</td>' +
                                        '<td style="vertical-align:middle; border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="EDDiasDesde" id="Responsable' + contadorcs + '"value="' + element.Responsable + '">' + element.Responsable + '</td>' +
                                        '<td style="vertical-align:middle; border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="EDDiasHasta" id="EDDiasHasta' + contadorcs + '"value="' + Fecha + '">' + Fecha + '</td>' +
                                        //'<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><a id="botonBorrar" title="Eliminar Plan" onclick="EliminarPlanRevision(' + element.PK_Id_PlanAccion + ',' + element.FK_Acta + ')" class="btn btn-search btn-lg"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                        //'<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDpkCondicion" id="EDpkCondicion' + contadorcs + '"value=DescribeCondicionvm"' + element.EDDescribeCondicion + '"><input type="hidden" name="Descripcion" id = "Descripcion' + contadorcs + '" value="' + element.EDDescribeCondicion + '">' + element.EDDescribeCondicion + '</td>' +
                                        //'<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDUbicacionespecifica" id="EDUbicacionespecifica' + contadorcs + '"value="' + element.EDUbicacionespecifica + '">' + element.EDUbicacionespecifica + '</td>' +
                                        //'<td style="border:solid 1px #808080 ; border-bottom: 1px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="BotonVer" title="Ver Imagen" onclick="verImagen(' + element.EDpkCondicion + ')" class="btn btn-lg" style="font-size:16px;">' + element.EDEvidenciacondicion + '</span></a></td>' +
                                        //'<td style="border:solid 1px #808080 ; border-bottom: 1px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="EliminarCondicion" title="Eliminar" onclick="EliminarCondicion(' + element.EDpkCondicion + ',' + element.EDPkInspeccion + ')" class="btn btn-lg"><span class="glyphicon  glyphicon-trash"></span></a></td>' +
                                        '</tr>'
                        $('#planaccionesrevision').append(elementoscs)
                        contadorcs = contadorcs + 1
                    });
                    paginador("#planaccionesrevision", "tr[name = planrevisiones]", "#paginadorr")
                }
                else {
                    //swal({
                    //    type: 'warning',
                    //    title: 'Estimado Usuario',
                    //    text: 'No .',
                    //    showConfirmButton: true,
                    //    confirmButtonText: 'Aceptar',
                    //    confirmButtonColor: '#7E8A97'
                    //});
                    OcultarPopupposition();
                }

            }


        });
    }

    ///Funcion para Cargar la Firma del Rep Legal y almacenarlo en la BD.
    function ValorCheckBoxreplegal() {
        var idacta = $("#IdActa").val();
        if ($("#idcheckfirmareplegal").is(':checked')) {
            $.ajax({
                url: urlBase + '/Revision/Validar_ExisteFirmaRepLegal',//primero el modulo/controlador/metodo que esta en el controlador
                data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                    IdActa: idacta
                },
                type: 'POST',
                success: function (result) {
                    if (result.Data) {
                        $(this).prop('checked', false);
                        swal({
                            type: 'success',
                            title: 'Estimado Usuario',
                            text: 'La Firma del Representante SGSST ha sido cargada satisfactoriamente',
                            confirmButtonColor: '#7E8A97'
                        })
                    }
                    else {

                        $('#divMensajeError').html(result.mensaje);
                        $("#idcheckfirma").prop('checked', false);
                        swal({
                            type: 'warning',
                            title: 'Estimado Usuario',
                            text: 'No se encuentra cargada la firma del Representante SGSST, por favor diríjase al módulo Empresa y regístrela.',
                            timer: 4000,
                            confirmButtonColor: '#7E8A97'
                        })
                        $(this).prop('checked', false);

                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó un error en la transacción, intente más tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                }
            });
        }
        else {
            $.ajax({
                url: urlBase + '/Revision/CambiarEstadoFirmaRepresentante',//primero el modulo/controlador/metodo que esta en el controlador
                data: { IdActa: idacta },
                type: 'POST',
                success: function (result) {
                    if (result.Data) {
                        swal({
                            type: 'success',
                            title: 'Estimado Usuario',
                            text: 'La firma del Representante SGSST se ha removido de esta Acta.',
                            confirmButtonColor: '#7E8A97'
                        });

                    }
                    else {
                        swal({
                            type: 'warning',
                            title: 'Estimado Usuario',
                            text: 'No fue posible remover la firma del Representante SGSST de esta Acta.',
                            showConfirmButton: true,
                            confirmButtonText: 'Aceptar',
                            confirmButtonColor: '#7E8A97'
                        });

                    }

                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó un error en la transacción, intente más tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                }


            });





        }


    }
    ////Funcion para Cargar la firma del Responsable SGSST

    function ValorCheckBoxResponsable() {
        var idacta = $("#IdActa").val();
        if ($("#idcheckfirmaresponsable").is(':checked', true)) {
            $.ajax({
                url: urlBase + '/Revision/Validar_ExisteFirmaResponsable',//primero el modulo/controlador/metodo que esta en el controlador
                data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                    IdActa: idacta
                },
                type: 'POST',
                success: function (result) {
                    if (result.Data) {
                        $(this).prop('checked', false);
                        swal({
                            type: 'success',
                            title: 'Estimado Usuario',
                            text: 'La firma del Responsable SGSST ha sido cargada satisfactoriamente.',
                            confirmButtonColor: '#7E8A97'
                        })
                    }
                    else {

                        $('#divMensajeError').html(result.mensaje);
                        $("#idcheckfirma").prop('checked', false);
                        swal({
                            type: 'warning',
                            title: 'Estimado Usuario',
                            text: 'No se encuentra cargada la firma del Responsable SGSST, por favor diríjase al módulo Empresa y regístrela.',
                            timer: 4000,
                            confirmButtonColor: '#7E8A97'
                        })
                        $(this).prop('checked', false);

                    }
                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó un error en la transacción, intente más tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                }
            });
        }
        else {

            $.ajax({
                url: urlBase + '/Revision/CambiarEstadoFirmaResponsable',//primero el modulo/controlador/metodo que esta en el controlador
                data: { IdActa: idacta },
                type: 'POST',
                success: function (result) {
                    if (result.Data) {
                        swal({
                            type: 'success',
                            title: 'Estimado Usuario',
                            text: 'La firma del Responsable SGSST se ha removido de esta Acta.',
                            showConfirmButton: true,
                            confirmButtonText: 'Aceptar',
                            confirmButtonColor: '#7E8A97'
                        });

                    }
                    else {
                        swal({
                            type: 'warning',
                            title: 'Estimado Usuario',
                            text: 'No fue posible remover la firma del Responsable SGSST de esta Acta.',
                            showConfirmButton: true,
                            confirmButtonText: 'Aceptar',
                            confirmButtonColor: '#7E8A97'
                        });

                    }

                },
                error: function (result) {
                    swal({
                        type: 'error',
                        title: 'Estimado Usuario',
                        text: 'Se presentó un error en la transacción, intente más tarde.',
                        confirmButtonColor: '#7E8A97'
                    });
                }


            });





        }

    }

    ///Funcion para Grabar los Planes de Accion de una Revision
    $("#btnplanesacta").click(function (e) {
        e.preventDefault();
        ValidarPlan();
        if ($("#planaccionrevision").valid()) {
            PopupPosition();
            var Fecha = $("#fechapaacta").val();
            var hoy = new Date();
            dia = hoy.getDate();
            mes = hoy.getMonth() + 1;
            anio = hoy.getFullYear();
            if (mes < 10) {
                fecha_actual = String(dia + "/" + "0" + mes + "/" + anio);
            } else {
                fecha_actual = String(dia + "/" + mes + "/" + anio);
            }
            if ($.datepicker.parseDate('dd/mm/yy', Fecha) < $.datepicker.parseDate('dd/mm/yy', fecha_actual)) {
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'La fecha ingresada no puede ser inferior a la Fecha actual.',
                    confirmButtonColor: '#7E8A97'
                });
                OcultarPopupposition();
                return false;
            }
            else {
                var planaccionrevision = {
                    FKActa: $("#fkacta").val(),
                    NumActa: $("#consecutivo").val(),
                    FechaPlan: $("#fechapaacta").val(),
                    ResponsablePlan: $("#responsablepaacta").val(),
                    ActividadPlan: $("#actividadpaacta").val(),
                }
                $.ajax({
                    url: urlBase + '/Revision/GrabarPlanAccionRevision',
                    data: planaccionrevision,
                    type: 'POST',
                    success: function (result) {
                        if (result.Data) {
                            OcultarPopupposition();
                            swal({
                                type: 'success',
                                title: 'Estimado Usuario',
                                text: 'El Plan de Acción del acta de revisión se ha almacenado correctamente.',
                                showConfirmButton: true,
                                confirmButtonText: 'Aceptar',
                                confirmButtonColor: '#7E8A97'
                            });
                            $('.confirm').on('click', function () {
                                $("#planaccionrevision").trigger("reset");
                                $("#formplan1").modal("hide");
                                OcultarPopupposition();
                            });
                            $('#planaccionesrevision').empty();
                            $('#planaccionesrevision').append;
                            var contadorcs = 0;
                            $.each(result.Data, function (ind, element) {
                                var Fecha = moment(result.Data[ind].Fecha).format("DD/MM/YYYY");
                                var elementoscs = '<tr name="planrevisiones" id="contador">' +
                                                //'<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center">' + '<input type="checkbox" name="condicionesIns" name1="' + element.EDDiasDesde + '" name2="' + element.EDDiasHasta + '" desc="' + element.EDDescribeCondicion + '" value="' + element.EDpkCondicion + '" id=' + contadorcs + '  class ="condicionesIns">' + '</td>' +
                                                '<td style="vertical-align:middle; border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="Actividad" id="Actividad' + contadorcs + '"value="' + element.Actividad + '">' + element.Actividad + '</td>' +
                                                '<td style="vertical-align:middle; border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="EDDiasDesde" id="Responsable' + contadorcs + '"value="' + element.Responsable + '">' + element.Responsable + '</td>' +
                                                '<td style="vertical-align:middle; border-right: 2px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center"><input type="hidden" name="EDDiasHasta" id="EDDiasHasta' + contadorcs + '"value="' + Fecha + '">' + Fecha + '</td>' +
                                                //'<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><a id="botonBorrar" title="Eliminar Plan" onclick="EliminarPlanRevision(' + element.PK_Id_PlanAccion + ',' + element.FK_Acta + ')" class="btn btn-search btn-lg"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                                //'<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDpkCondicion" id="EDpkCondicion' + contadorcs + '"value=DescribeCondicionvm"' + element.EDDescribeCondicion + '"><input type="hidden" name="Descripcion" id = "Descripcion' + contadorcs + '" value="' + element.EDDescribeCondicion + '">' + element.EDDescribeCondicion + '</td>' +
                                                //'<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDUbicacionespecifica" id="EDUbicacionespecifica' + contadorcs + '"value="' + element.EDUbicacionespecifica + '">' + element.EDUbicacionespecifica + '</td>' +
                                                //'<td style="border:solid 1px #808080 ; border-bottom: 1px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="BotonVer" title="Ver Imagen" onclick="verImagen(' + element.EDpkCondicion + ')" class="btn btn-lg" style="font-size:16px;">' + element.EDEvidenciacondicion + '</span></a></td>' +
                                                //'<td style="border:solid 1px #808080 ; border-bottom: 1px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="EliminarCondicion" title="Eliminar" onclick="EliminarCondicion(' + element.EDpkCondicion + ',' + element.EDPkInspeccion + ')" class="btn btn-lg"><span class="glyphicon  glyphicon-trash"></span></a></td>' +
                                                '</tr>'
                                $('#planaccionesrevision').append(elementoscs)
                                contadorcs = contadorcs + 1
                            });
                            paginador("#planaccionesrevision", "tr[name = planrevisiones]", "#paginadorr")
                        }
                        else {
                            swal({
                                type: 'warning',
                                title: 'Estimado Usuario',
                                text: 'No fue posible almacenar la información.',
                                showConfirmButton: true,
                                confirmButtonText: 'Aceptar',
                                confirmButtonColor: '#7E8A97'
                            });
                            OcultarPopupposition();
                        }

                    },
                    error: function (result) {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó un error en la transacción, intente más tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }


                });
            }



        }

    });
    ///Funcion para Validar los Campos de Grabar Plan de Accion Acta Revision 
    function ValidarPlan() {
        $("#planaccionrevision").validate({
            rules: {
                actividadpaacta: {
                    required: true
                },
                responsablepaacta: {
                    required: true
                },
                fechapaacta: {
                    required: true
                }
            },
            messages: {
                actividadpaacta: {
                    required: " *Campo es Obligatrorio"
                },
                responsablepaacta: {
                    required: " *Campo es Obligatrorio"
                },
                fechapaacta: {
                    required: " *Fecha no puede estar vacio."
                }
            }
        });
    }

    ///funcion para eliminar un plan de Accion de Revision 
    function EliminarPlanRevision(PK_Id_PlanAccion, FK_Acta) {
        PopupPosition();
        swal({
            title: 'Estimado Usuario',
            text: "¿Seguro desea eliminar el Plan de Acción del Acta de Revisión?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#7E8A97',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            confirmButtonText: 'Eliminar'
        }, (function (isConfirm) {
            if (isConfirm) {
                PopupPosition();
                $.ajax({
                    url: urlBase + '/Revision/EliminarPlanAccionRevision',
                    data: {
                        pkplan: PK_Id_PlanAccion,
                        idacta: FK_Acta,
                    },
                    type: 'POST',
                    success: function (result) {
                        $('#planaccionesrevision').empty();
                        $('#planaccionesrevision').append;
                        var contadorcs = 0;
                        $.each(result.Data, function (ind, element) {
                            var Fecha = moment(result.Data[ind].Fecha).format("DD/MM/YYYY");
                            var elementoscs = '<tr name="planrevisiones" id="contador">' +
                                            //'<td style="border:solid 1px #808080;color:black;background-color:whitesmoke; vertical-align:middle; text-align:center">' + '<input type="checkbox" name="condicionesIns" name1="' + element.EDDiasDesde + '" name2="' + element.EDDiasHasta + '" desc="' + element.EDDescribeCondicion + '" value="' + element.EDpkCondicion + '" id=' + contadorcs + '  class ="condicionesIns">' + '</td>' +
                                            '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="Actividad" id="Actividad' + contadorcs + '"value="' + element.Actividad + '">' + element.Actividad + '</td>' +
                                            '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDDiasDesde" id="Responsable' + contadorcs + '"value="' + element.Responsable + '">' + element.Responsable + '</td>' +
                                            '<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDDiasHasta" id="EDDiasHasta' + contadorcs + '"value="' + Fecha + '">' + Fecha + '</td>' +
                                            //'<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><a id="botonBorrar" title="Eliminar Plan" onclick="EliminarPlanRevision(' + element.PK_Id_PlanAccion + ',' + element.FK_Acta + ')" class="btn btn-search btn-lg"><span class="glyphicon  glyphicon-erase"></span></a></td>' +
                                            //'<td style="border:solid 1px #808080 ; border-bottom: 1px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="EliminarCondicion" title="Eliminar" onclick="EliminarCondicion(' + + ',' + + ')" class="btn btn-lg"><span class="glyphicon  glyphicon-trash"></span></a></td>' +
                                            //'<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDpkCondicion" id="EDpkCondicion' + contadorcs + '"value=DescribeCondicionvm"' + element.EDDescribeCondicion + '"><input type="hidden" name="Descripcion" id = "Descripcion' + contadorcs + '" value="' + element.EDDescribeCondicion + '">' + element.EDDescribeCondicion + '</td>' +
                                            //'<td style="vertical-align:middle;border:solid 1px #808080; text-align:center;color:black;background-color:whitesmoke;font-size:15px"><input type="hidden" name="EDUbicacionespecifica" id="EDUbicacionespecifica' + contadorcs + '"value="' + element.EDUbicacionespecifica + '">' + element.EDUbicacionespecifica + '</td>' +
                                            //'<td style="border:solid 1px #808080 ; border-bottom: 1px solid lightslategray; vertical-align:middle; text-align:center;background-color:whitesmoke"><a id="BotonVer" title="Ver Imagen" onclick="verImagen(' + element.EDpkCondicion + ')" class="btn btn-lg" style="font-size:16px;">' + element.EDEvidenciacondicion + '</span></a></td>' +

                                            '</tr>'
                            $('#planaccionesrevision').append(elementoscs)
                            contadorcs = contadorcs + 1
                        });
                        paginador("#planaccionesrevision", "tr[name = planrevisiones]", "#paginadorr")
                        OcultarPopupposition();
                    },
                    error: function (result) {
                        swal({
                            type: 'error',
                            title: 'Estimado Usuario',
                            text: 'Se presentó un error, intente más tarde.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                });
            }
            else {
                OcultarPopupposition();
            }
        }))

    }
    //////<--- fin Funciones para Revision por Robinson.

