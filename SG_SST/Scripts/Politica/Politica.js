var urlBase = "";
var $idtextareaPolitica = $("#strDescripcion_Politica");
var $idlblMensaje = $("#lblMensaje");
var $idinputMatriz = $("#nmtDofa");


try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};




function BuscarArchivo_buscador(element) {

    var stringBusqueda = $(element).val()

    $.ajax({
        url: urlBase + '/OtrasInteracciones/BusquedaArchivo',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            Busqueda: stringBusqueda
        },
        type: 'POST',
        success: function (result) {
            if (result) {

                $('#IDscBusqueda').html(result)

            }
        }
    });
}


//Funcion mostrar la politica en el textbox
function ObtenerPolitica() {

    $.ajax({
        url: urlBase + '/Politicasst/CargarPolitica',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.

        },
        type: 'POST',
        success: function (result) {
            if (result) {
                //console.log(result.Politica);
                $idtextareaPolitica.text(result.Politica);

            }
        }
    });
}




function ValorCheckBox() {

    if ($("#CheckDocPrivado").is(':checked')) {
        $idlblMensaje.text("el check box Doc Privado ha sido seleccionado");

        DocumentoPrivadoScript();
        //$("#CheckEliminar").prop("checked", false);
    } else
        if ($("#CheckEliminar").is(':checked')) {
            $idlblMensaje.text("el check box Eliminar ha sido seleccionado");
            EliminarOtrasInteracciones();
            //$("# CheckDocPrivado").prop("checked", false);             

        }
}


function ValorRadioButton() {

    if ($("#rbDocPrivado").is(':checked')) {
        $idlblMensaje.text("el check box Doc Privado ha sido seleccionado");

        DocumentoPrivadoScript();
        //$("#CheckEliminar").prop("checked", false);
    } else
        if ($("#rbEliminar").is(':checked')) {

            $idlblMensaje.text("el check box Eliminar ha sido seleccionado");
            EliminarOtrasInteracciones();
            //$("# CheckDocPrivado").prop("checked", false);
        }
}



function EliminarOtrasInteracciones() {
    var selectedIDs = new Array();
    $('input:checkbox.checkBox').each(function () {
        if ($(this).prop('checked')) {
            selectedIDs.push($(this).val());
        }
    });
    $.ajax({
        url: urlBase + '/OtrasInteracciones/EliminarArchivoOtrasInteracciones',
        data: {
            customerIDs: selectedIDs
        },
        type: 'POST',
        success: function (result) {
            if (result.success) {
                $('input:checkbox.checkBox').each(function () {
                    if ($(this).prop('checked')) {

                        $(this).closest('tr').remove();
                        //utils.showMessage("Archivo eliminado con éxito", "success", $('#divMensaje'));
                        swal({
                            type: 'success',
                            title: 'Estimado Usuario',
                            text: 'Archivo eliminado satisfactoriamente',
                            timer: 4000,
                            confirmButtonColor: '#7E8A97'
                        })
                    }
                });
            }
            else {
                //utils.showMessage("Seleccione un archivo para eliminar", "error", $('#divMensaje'));
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'Seleccione un archivo para eliminar',
                    timer: 4000,
                    confirmButtonColor: '#7E8A97'
                })
            }
        }
    });
}



function DocumentoPrivadoScript() {

    var selectedIDs = new Array();
    $('input:checkbox.checkBox').each(function () {
        if ($(this).prop('checked')) {
            selectedIDs.push($(this).val());
        }
    });

    $.ajax({
        url: urlBase + '/OtrasInteracciones/DocumentoPrivado_script',
        data: {
            customerIDs: selectedIDs
        },
        type: 'POST',
        success: function (result) {
            if (result.success) {

                $('input:checkbox.checkBox').each(function () {
                    if ($(this).prop('checked')) {


                        var texto = $(this).closest('tr').find('#tdDocumentoPrivado').html().trim();


                        if (texto.length == 0) {

                            $(this).closest('tr').find('#tdDocumentoPrivado').text('Documento Privado');
                            $(this).prop('checked', false);
                            //utils.showMessage("Documento modificado correctamente", "success", $('#divMensaje'));
                            swal({
                                type: 'success',
                                title: 'Estimado Usuario',
                                text: 'Documento modificado correctamente',
                                timer: 4000,
                                confirmButtonColor: '#7E8A97'
                            })



                        }
                        else {

                            if (texto == 'Documento Privado') {

                                $(this).closest('tr').find('#tdDocumentoPrivado').html('');
                                $(this).prop('checked', false);
                                //utils.showMessage("Documento modificado correctamente", "success", $('#divMensaje'));
                                swal({
                                    type: 'success',
                                    title: 'Estimado Usuario',
                                    text: 'Documento modificado correctamente',
                                    timer: 4000,
                                    confirmButtonColor: '#7E8A97'
                                })


                            }
                        }

                    }
                });

            }
            else {
                //utils.showMessage("Seleccione un archivo para modificar", "error", $('#divMensaje'));
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'Seleccione un archivo para modificar',
                    timer: 4000,
                    confirmButtonColor: '#7E8A97'
                })
            }
        }
    });
}


function validarexistepolitica() {

 

    $.ajax({
        url: urlBase + '/Politicasst/Validar_ExistePoliticaCreada',
        type: 'POST',
        success: function (result) {
            if (result.success) {

                ValorCheckBoxPolitica();

            }
            else {
                $(this).prop('checked', false);
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'No se encuentra una política creada, proceda a crearla',
                    timer: 4000,
                    confirmButtonColor: '#7E8A97'
                })
               



            }

        }



    });
}





function ValorCheckBoxPolitica() {

    if ($("#idcheckfirma").is(':checked')) {
        var firma = "mostrarfirma"
        $.ajax({
            url: urlBase + '/Politicasst/Validar_ExisteFirmaRepLegal',//primero el modulo/controlador/metodo que esta en el controlador
            data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
                valormostrarfirma: firma
            },
            type: 'POST',
            success: function (result) {
                if (result.success) {

                    $('#divMensajeError').html(result.mensaje);
                    //utils.showMessage("¡Firma anexada al documento con éxito!", "success", $('#divMensaje'));
                    $(this).prop('checked', false);
                    swal({
                        type: 'success',
                        title: 'Estimado Usuario',
                        text: 'Firma anexada al documento satisfactoriamente',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })
                  
                }
                else {

                    $('#divMensajeError').html(result.mensaje);
                    $("#idcheckfirma").prop('checked', false);

                    // utils.showMessage("¡Señor usuario no se encuentra cargada la firma del representante legal!", "error", $('#divMensaje'));

                    swal({
                        type: 'warning',
                        title: 'Estimado Usuario',
                        text: 'No se encuentra cargada la firma del representante legal',
                        timer: 4000,
                        confirmButtonColor: '#7E8A97'
                    })
                    $(this).prop('checked', false);

                }
            }
        });
    }
}





function BuscarArchivo(element) {

    var stringBusqueda = $(element).val()

    $.ajax({
        url: urlBase + '/Politicasst/ReportePolitica_Aplicativo',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            Busqueda: stringBusqueda
        },
        type: 'POST',
        success: function (result) {
            if (result) {

                $('#IDscBusqueda').html(result)

            }


        }



    });
}


function MostrarPoliticaVistaParcial() {

    $.ajax({
        url: urlBase + '/Politicasst/ReportePolitica_Aplicativo2',//primero el modulo/controlador/metodo que esta en el controlador       
        type: 'POST',
        success: function (result) {
            if (result) {
                $('#politicaPartialView').html(result)

            }


        }



    });
}


