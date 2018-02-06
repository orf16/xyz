





try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};








function MostrarArchivosBusqueda() {

    $.ajax({
        url: urlBase + '/Organizacion/MostrarArchivosVPbusqueda',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.

        },
        type: 'POST',
        success: function (result) {
            if (result) {

                $('#IDscBusqueda').html(result)

            }
        }
    });
}




function MostrarArchivosEmpresa() {

    $.ajax({
        url: urlBase + '/Organizacion/MostrarArchivosVPEmpresa',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.

        },
        type: 'POST',
        success: function (result) {
            if (result) {

                $('#IDscBusquedaEmpresa').html(result)

            }
        }
    });
}
function MostrarArchivosLiderazgo() {

    $.ajax({
        url: urlBase + '/Organizacion/MostrarArchivosVPLiderazgo',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.

        },
        type: 'POST',
        success: function (result) {
            if (result) {

                $('#IDscBusquedaLiderazgo').html(result)

            }
        }
    });
}
function MostrarArchivosPolitica() {

    $.ajax({
        url: urlBase + '/Organizacion/MostrarArchivosVPPolitica',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.

        },
        type: 'POST',
        success: function (result) {
            if (result) {

                $('#IDscBusquedaPolítica').html(result)

            }
        }
    });
}
function MostrarArchivosOrganizacion() {

    $.ajax({
        url: urlBase + '/Organizacion/MostrarArchivosVPOrganizacion',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.

        },
        type: 'POST',
        success: function (result) {
            if (result) {

                $('#IDscBusquedaOrganización').html(result)

            }
        }
    });
}
function MostrarArchivosPlanificacion() {

    $.ajax({
        url: urlBase + '/Organizacion/MostrarArchivosVPPlanificacion',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.

        },
        type: 'POST',
        success: function (result) {
            if (result) {

                $('#IDscBusquedaPlanificación').html(result)

            }
        }
    });
}

function MostrarArchivosAplicacion() {

    $.ajax({
        url: urlBase + '/Organizacion/MostrarArchivosVPAplicacion',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.

        },
        type: 'POST',
        success: function (result) {
            if (result) {

                $('#IDscBusquedaApl').html(result)

            }
        }
    });
}






function MostrarArchivosrReporteInvestigacion() {

    $.ajax({
        url: urlBase + '/Organizacion/MostrarArchivosVPReporteInvestigacion',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.

        },
        type: 'POST',
        success: function (result) {
            if (result) {

                $('#IDscBusquedaReporte').html(result)

            }
        }
    });
}
function MostrarArchivosMedicionEvaluacion() {

    $.ajax({
        url: urlBase + '/Organizacion/MostrarArchivosVPMedicionEvaluacion',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.

        },
        type: 'POST',
        success: function (result) {
            if (result) {

                $('#IDscBusquedaMedición').html(result)

            }
        }
    });
}
function MostrarArchivosParticipacionColaboradores() {

    $.ajax({
        url: urlBase + '/Organizacion/MostrarArchivosVPParticipacionColaboradores',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.

        },
        type: 'POST',
        success: function (result) {
            if (result) {

                $('#IDscBusquedaParticipación').html(result)

            }
        }
    });
}


//SCRIPT USADO PARA ELIMINAR POR SCRIPT 
function EliminarArchivoDocumentacion(i, ID_Documentacion_Org) {

    //$ideliminarfila = $(element).closest("td").find("button[name=ideliminarfila]");



    $.ajax({
        url: urlBase + '/Organizacion/EliminarArchivoDocumetacion',
        data: {
            PKdArchivo: ID_Documentacion_Org
        },
        type: 'POST',
        success: function (result) {
            if (result) {
                $('#modalEliminar').hide();
                $(i).closest("tr").remove();
                swal({
                    type: 'success',
                    title: 'Estimado usuario',
                    text: 'Archivo eliminado satisfactoriamente',
                    //timer: 500,
                    confirmButtonColor: '#7E8A97'
                })

                /*
                utils.showMessage("Archivo eliminado con éxito", "success", $('#divMensaje1'));
                //location.reload();
                utils.showMessage("Archivo eliminado con éxito", "success", $('#divMensaje2'));
                utils.showMessage("Archivo eliminado con éxito", "success", $('#divMensaje3'));
                utils.showMessage("Archivo eliminado con éxito", "success", $('#divMensaje4'));
                utils.showMessage("Archivo eliminado con éxito", "success", $('#divMensaje5'));
                utils.showMessage("Archivo eliminado con éxito", "success", $('#divMensaje6'));
                utils.showMessage("Archivo eliminado con éxito", "success", $('#divMensaje7'));
                utils.showMessage("Archivo eliminado con éxito", "success", $('#divMensaje8'));
                utils.showMessage("Archivo eliminado con éxito", "success", $('#divMensaje9'));
               */

            }

        }
    });
}



function mensajesubirarchivo(i, ID_Documentacion_Org) {


    //utils.showMessage("Archivo cargado con éxito", "success", $('#divMensaje'));

    swal({
        type: 'success',
        title: 'Estimado Usuario',
        text: 'Archivo cargado satisfactoriamente',
        timer: 4000,
        confirmButtonColor: '#7E8A97'
    })

}

function validarDocumentacion() {



    $("#idDocumentacion").validate({

        rules: {
            ID_TipoModulo_Organizacion: { required: true },

        },
        messages: {



            ID_TipoModulo_Organizacion: {
                required: "Se debe seleccionar un módulo para cargar la información"
            },



        }

    });

}

/*

function PaginadorDocumentacion(idTabla, nameTr, pagination) {
    jQuery(function ($) {
        language: {
                paginate: {
                    next: '&#8594;'; // or '→'
                    previous: '&#8592;' // or '←' 
                }
        }
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
            cssStyle: "light-theme",

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
*/

function paginador(idTabla, nameTr, pagination) {
    jQuery(function ($) {
        language: {
                paginate: {
                    next: '&#8594;'; // or '→'
                    previous: '&#8592;' // or '←' 
                }
        }
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

function paginador1(idTabla, nameTr, pagination) {
    jQuery(function ($) {
        language: {
                paginate: {
                    next: '&#8594;'; // or '→'
                    previous: '&#8592;' // or '←' 
                }
        }
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
            hash = hash.match(/^#page-(\d+)$/);
            if (hash) {
                // The `selectPage` function is described in the documentation.
                // We've captured the page number in a regex group: `(\d+)`.
                $(pagination).pagination("selectPage", parseInt(hash[1]));
            }
        };

        // We'll call this function whenever back/forward is pressed...
        $(window).bind("popstate", checkFragment);

        // ... and we'll also call it when the page has loaded
        // (which is right now).
        checkFragment();
    });
}

function CargarArchivo() {
    validaciontamaño();
    var Archivo = document.getElementById('NombreArchivo_Documentacion').files[0];
    var ID_TipoModulo_Organizacion = $("#ID_TipoModulo_Organizacion").val();
    $.ajax({
        url: urlBase + '/Organizacion/CargarArchivoOrganizacion',
        data: {
            NombreArchivo_Documentacion: Archivo,
            ID_TipoModulo_Organizacion: ID_TipoModulo_Organizacion,
        },

        type: 'POST',
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result) {
                $('#modalEliminar').hide();
                $(i).closest("tr").remove();
                swal({
                    type: 'success',
                    title: 'Estimado usuario',
                    text: 'Archivo eliminado satisfactoriamente',
                    //timer: 500,
                    confirmButtonColor: '#7E8A97'
                })

             }

        }
    });
}


function paginador2(idTabla, nameTr, pagination) {
    jQuery(function ($) {
        language: {
                paginate: {
                    next: '&#8594;'; // or '→'
                    previous: '&#8592;' // or '←' 
                }
        }
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

function validaciontamaño() {
    if (typeof FileReader !== "undefined") {
        var size = document.getElementById('NombreArchivo_Documentacion').files[0].size;
        // check file size

        if (size > 4194304) {
           
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe cargar un archivo con peso menor a 4 Mb',
                timer: 4000,
                confirmButtonColor: '#7E8A97'
            })
          
        }
      
    }
 

}


function validaciontamañodocumento() {
    if (typeof FileReader !== "undefined") {
        var size = document.getElementById('NombreArchivo_Documentacion').files[0].size;         
     
        if (size > 10800332.8) {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Debe cargar un archivo con peso menor a 10 Mb',
                //timer: 4000,
                confirmButtonColor: '#7E8A97'
            })
            document.getElementById("NombreArchivo_Documentacion").value = "";
        }      
    }
}


