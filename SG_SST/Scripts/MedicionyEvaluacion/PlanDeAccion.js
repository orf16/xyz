var urlBase = ""
try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};

$(document).ready(function () {
    var i = 1;
    OcultarPopupposition();
    $(".fecha").each(function (ind, element) {
        ConstruirDatePickerPorElemento($(element).attr('id'));
        i++;

    });

    ConstruirDatePickerPorElemento("fechaInicial");
    ConstruirDatePickerPorElemento("fechaFinal");


    paginador("#actividades", "tr[name = act]", "#paginador1");


});


function GuardarAccion(id, fk_Id_ModuloPlanAccion, fk_Plan_Inspección, fk_Id_Actividad) {
    var observaciones = $("#observaciones_" + id).val();
    var fechaCierre = $("#fechaCierre_" +id).val();
    if (!fechaCierre) {
        swal(
         'Estimado Usuario',
         'La fecha de Cierre es obligatoria',
         'warning'
         )
        return;
    }
    var actividadPlanDeAccion = {
        Fk_Id_ModuloPlanAccion:fk_Id_ModuloPlanAccion,
        Fk_Plan_Inspección:fk_Plan_Inspección,
        Fk_Id_Actividad:fk_Id_Actividad,
        FechaCierre: fechaCierre,
        Observaciones: observaciones,
    };

    $.ajax({
        url: urlBase + '/PlanesDeAccion/GuardarAccion',
        data: { actividadPlanDeAccion:actividadPlanDeAccion
            },
        type: 'POST',
        success: function (result) {
            if (result) {
                swal({title:"Estimado Usuario", text:"Se ha guardado correctamente la Fecha de Cierre", type:"success"},
                    function(){ 
                        window.location.reload();
                    });
               // $("#observaciones_" + id).attr('readonly', true);
                //$("#fechaCierre_" + id).replaceWith("<span>" + fechaCierre + "</span>");
               // $("#" + id).replaceWith('<a onclick="NoGuardarAccion()" title=" guardar actividad" name="agregarRend" + class="btn btn-link-1-google-plus btn-circle btn-md btn-search"><span class="glyphicon glyphicon-pencil" name="iconoRend"></span></a>');


            }
            else {
                swal(
                'Estimado Usuario',
                'No se ha guardado correctamente la Fecha de Cierre',
                'error'
                )
                $("#observaciones_" + id).val("");
                $("#fechaCierre_" + id).val("");
            }
            }
    });
}

function EliminarActividad(Origen,Fk_Id_ModuloPlanAccion, Fk_Plan_Inspección, Fk_Id_Actividad, cantidad) {
    if (cantidad==1) {
        swal(
         'Estimado Usuario',
         'El Plan de Acción solo tiene una Actividad, no se puede eliminar',
         'warning'
         )
        return;
    }
    swal({
        title: "Estimado Usuario",
        text: "¿Está seguro de eliminar la Actividad? La Actividad será eliminada del Plan de Acción y del módulo " + Origen,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si",
        cancelButtonText: "No",
        closeOnConfirm: false
    },
    function(){
        var actividadPlanDeAccion = {
            Fk_Id_ModuloPlanAccion: Fk_Id_ModuloPlanAccion,
            Fk_Plan_Inspección: Fk_Plan_Inspección,
            Fk_Id_Actividad: Fk_Id_Actividad,
        };

        $.ajax({
            url: urlBase + '/PlanesDeAccion/EliminarActividad',
            data: {
                actividadPlanDeAccion: actividadPlanDeAccion
            },
            type: 'POST',
            success: function (result) {
                if (result.Data == true) {
                    swal({ title: "Estimado Usuario", text: "Se ha eliminado correctamente la Actividad", type: "success" },
                       function () {
                           window.location.reload();
                       });
                }
                else {
                    swal(
                    'Estimado Usuario',
                    'No se ha eliminado la Actividad',
                    'error'
                    )
                }
            }
        });
    })
}


function EditarActividad(Origen,id, Fk_Id_ModuloPlanAccion, Fk_Plan_Inspección, Fk_Id_Actividad) {
    swal({
        title: "Estimado Usuario",
        text: "¿Está seguro de editar la Actividad? Todo cambio que se genere en este módulo será guardado en el módulo " + Origen,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si",
        cancelButtonText: "No",
        closeOnConfirm: false
    },
 function(){
        var observaciones = $("#observacionesEdit_" + id).val();
        var fechaCierre = $("#fechaCierreEdit_" + id).val();
        var responsable = $("#responsableEdit_" + id).val();
        var actividad = $("#actividadEdit_" + id).val();
        if (!actividad  || !responsable) {
            swal(
         'Estimado Usuario',
         'Los campos Actividad y Responsable son obligatorios',
         'warning'
         )
            return;

        }
        var actividadPlanDeAccion = {
            Fk_Id_ModuloPlanAccion: Fk_Id_ModuloPlanAccion,
            Fk_Plan_Inspección: Fk_Plan_Inspección,
            Fk_Id_Actividad: Fk_Id_Actividad,
            FechaCierre: fechaCierre,
            Observaciones: observaciones,
            Actividad:actividad,
            Responsable: responsable,
        };
        $.ajax({
            url: urlBase + '/PlanesDeAccion/EditarActividad',
            data: {
                actividadPlanDeAccion: actividadPlanDeAccion
            },
            type: 'POST',
            success: function (result) {
                if (result.Data == true) {
                    swal({ title: "Estimado Usuario", text: "Se ha editado correctamente la Actividad", type: "success" },
                       function () {
                           window.location.reload();
                       });
                }
                else {
                    swal(
                    'Estimado Usuario',
                    'No se ha editado la Actividad',
                    'error'
                    )
                }
            }
        });
    })
}



function AdicionarActividad(id,Fk_Id_ModuloPlanAccion, Fk_Plan_Inspección, Fk_Id_Actividad, Num_Actividad) {
    var observaciones = $("#observacionesAdd_" + id).val();
    var fechaCierre = $("#fechaCierreAdd_" + id).val();
    var fechaFinalizacion = $("#fechaFinalizacionAdd_" + id).val();
    var responsable = $("#responsableAdd_" + id).val();
    var actividad = $("#actividadAdd_" + id).val();
    if (!actividad || !fechaFinalizacion || !responsable) {
        swal(
     'Estimado Usuario',
     'Los campos Actividad, Responsable y Fecha Fin son obligatorios',
     'warning'
     )
        return;

    }
    var actividadPlanDeAccion = {
        Fk_Id_ModuloPlanAccion: Fk_Id_ModuloPlanAccion,
        Fk_Plan_Inspección: Fk_Plan_Inspección,
        Fk_Id_Actividad: Fk_Id_Actividad,
        FechaCierre: fechaCierre,
        Observaciones: observaciones,
        Actividad: actividad,
        Responsable: responsable,
        fechaFinalizacion: fechaFinalizacion
    };
    $.ajax({
        url: urlBase + '/PlanesDeAccion/AdicionarActividad',
        data: {
            actividadPlanDeAccion: actividadPlanDeAccion
        },
        type: 'POST',
        success: function (result) {
            if (result.Data == true) {
                swal({ title: "Estimado Usuario", text: "Se ha adicionado correctamente la Actividad", type: "success" },
                  function () {
                      window.location.reload();
                  });

            }
            else {
                swal(
                'Estimado Usuario',
                'No se ha adicionado la Actividad',
                'error'
                )
            }
        }
    });
}


function NoGuardarAccion() {
    swal(
         'Estimado Usuario',
         'Ya se ingreso la Fecha de Cierre para la Actividad',
         'warning'
         )

}

function NoEditarAccion() {
    swal(
         'Estimado Usuario',
         'La Actividad ya se encuentra cerrada, no se puede editar',
         'warning'
         )

}

function NoAdicionarActividad() {
    swal(
         'Estimado Usuario',
         'El Plan de Acción ya se encuentra cerrado, no se pueden Adicionar Actividades',
         'warning'
         )

}
function NoAdicionarInspección() {
    swal(
         'Estimado Usuario',
         'El módulo Inspecciones no permite Adicionar Actividades, solo las puede editar y cerrar',
         'warning'
         )

}
function NoEliminarInspección() {
    swal(
         'Estimado Usuario',
         'El módulo Inspecciones no permite Eliminar Actividades, solo las puede editar y cerrar',
         'warning'
         )

}
function NoEliminarActividad() {
    swal(
         'Estimado Usuario',
         'La actividad ya se encuentra cerrada, no se puede eliminar',
         'warning'
         )

}



function graficar() {
    utils.showLoading();
    $.ajax({
        url: urlBase + '/PlanesDeAccion/Graficar',
        data: { },
        type: 'POST',
        success: function (result) {
            utils.closeLoading();
            if (result) {
                var ctx1 = document.getElementById("canvas1");
                var ctx2 = document.getElementById("canvas2");
                var ctx3 = document.getElementById("canvas3").getContext("2d");
                var context1 = ctx1.getContext('2d');
                context1.clearRect(0, 0, ctx1.width, ctx1.height);
                var context2 = ctx2.getContext('2d');
                context2.clearRect(0, 0, ctx2.width, ctx2.height);
      

                ctx3.clearRect(0, 0, ctx3.width, ctx3.height);
                var planesAccion = result.planesAccion - result.planesAccionAbiertos;
                var planesAccionAbiertos = result.planesAccionAbiertos;
                var planesAccionCerradosNoCumple = result.planesAccionCerradosNoCumple;
                var planesAccionCumple = planesAccion - planesAccionCerradosNoCumple;
                var planesAccionEvaluacion = 0;
                if (result.planesAccionEvaluacion != 0)
                {
                    planesAccionEvaluacion = 100;
                }


                var planesAccionEvaluacionAbiertos = (result.planesAccionEvaluacionAbiertos * 100) / result.planesAccionEvaluacion;


                var planesAccionAccion = 0;
                if (result.planesAccionAccion != 0) {
                    planesAccionAccion = 100;
                }

          
                var planesAccionAccionAbiertos = (result.planesAccionAccionAbiertos * 100) / result.planesAccionAccion;


                var planesAccionAuditoria = 0;


                if (result.planesAccionAuditoria != 0) {
                    planesAccionAuditoria = 100;
                }

                var planesAccionAuditoriaAbiertos = (result.planesAccionAuditoriaAbiertos * 100) / result.planesAccionAuditoria;

                var planesAccionInspecciones = 0;

                if (result.planesAccionInspecciones != 0) {
                    planesAccionInspecciones = 100;
                }

                var planesAccionInspeccionesAbiertos = (result.planesAccionInspeccionesAbiertos * 100) / result.planesAccionInspecciones;

                //reportes
                var planesAccionReportes = 0;

                if (result.planesAccionReportes != 0) {
                    planesAccionReportes = 100;
                }

                var planesAccionReportesAbiertos = (result.planesAccionReportesAbiertos * 100) / result.planesAccionReportes;

                //coppast
               
                var planesAccionCoppast = 0;

                if (result.planesAccionCoppast != 0) {
                    planesAccionCoppast = 100;
                }

                var planesAccionCoppastAbiertos = (result.planesAccionCoppastAbiertos * 100) / result.planesAccionCoppast;



                // convivencia
                var planesAccionConvivencia = 0;

                if (result.planesAccionConvivencia != 0) {
                    planesAccionConvivencia = 100;
                }

                var planesAccionConvivenciaAbiertos = (result.planesAccionConvivenciaAbiertos * 100) / result.planesAccionConvivencia;


                // Revision SGSST

                var planesAccionRevisionSGSST = 0;

                if (result.planesAccionRevisionSGSST != 0) {
                    planesAccionRevisionSGSST = 100;
                }

                var planesAccionRevisionSGSSTAbiertos = (result.planesAccionRevisionSGSSTAbiertos * 100) / result.planesAccionRevisionSGSST;


                var data1 = {
                    labels: [
                        "Abiertos",
                        "Cerrados"
                    ],
                    datasets: [
                        {
                            data: [planesAccionAbiertos, planesAccion],
                            backgroundColor: [
                                
                                "#82E0AA",
                                "#FF7500",
                            ],
                            hoverBackgroundColor: [
                              
                                "#82E0AA",
                                "#FF7500",
                            ]
                        }]
                }
                var data2 = {
                    labels: ["Evaluación SG-SST", "Acciones", "Auditorias SG-SST","Inspecciones","Rep. Actos Inseguros","Coppast","Comité de convivencia laboral", "Revisión del SG-SST"],
                    datasets: [{
                        label: "Abierto",
                       
                        backgroundColor: "#82E0AA",
                        data: [planesAccionEvaluacionAbiertos, planesAccionAccionAbiertos, planesAccionAuditoriaAbiertos, planesAccionInspeccionesAbiertos, planesAccionReportesAbiertos, planesAccionCoppastAbiertos, planesAccionConvivenciaAbiertos, planesAccionRevisionSGSSTAbiertos]
                    }, {
                        label: "Creados",
                        backgroundColor: "#FF7500",
                        data: [planesAccionEvaluacion, planesAccionAccion, planesAccionAuditoria, planesAccionInspecciones, planesAccionReportes, planesAccionCoppast, planesAccionConvivencia, planesAccionRevisionSGSST]
                    }]
                };
                var data3 = {
                    labels: [
                        "Cumple",
                        "No Cumple"
                    ],
                    datasets: [
                        {
                            data: [planesAccionCumple, planesAccionCerradosNoCumple],
                            backgroundColor: [
                               
                                "#82E0AA",
                                 "#FF7500",
                            ],
                            hoverBackgroundColor: [
                                
                                "#82E0AA",
                                "#FF7500",
                            ]
                        }]
                }
                var myPieChart1 = new Chart(ctx1, {
                    type: 'pie',
                    data: data1,
                    options: {
                        title: {
                            display: true,
                            text: 'Porcentaje de Planes de Acción abiertos',
                            top: 'bottom',
                            fontSize:12
                        },
                        tooltips: {
                            callbacks: {
                                label: function(tooltipItem, data) {
                                    var allData = data.datasets[tooltipItem.datasetIndex].data;
                                    var tooltipLabel = data.labels[tooltipItem.index];
                                    var tooltipData = allData[tooltipItem.index];
                                    var total = 0;
                                    for (var i in allData) {
                                        total += allData[i];
                                    }
                                    var tooltipPercentage = Math.round((tooltipData / total) * 100);
                                    return tooltipLabel + ': ' + tooltipData + ' (' + tooltipPercentage + '%)';
                                }
                            }
                        }

                    }
                });
                var myBarChart = new Chart(ctx3, {
                    type: 'bar',
                    data: data2,
                    options: {
                        title: {
                            display: true,
                            text: 'Porcentaje de Planes de Acción abiertos por Origen',
                            top: 'bottom',
                            fontSize: 12
                        },
                        scales: {
                            yAxes: [{
                                ticks: {
                                    beginAtZero: true,
                                    min: 0,
                                    max: 100,
                                    callback: function (value) {
                                        return value + "%"
                                    }
                                },
                                scaleLabel: {
                                    display: true,
                                    labelString: "Porcentaje"
                                }
                            }],
                                xAxes: [{
                        ticks: {
                        autoSkip: false
                    }
                }]
                        }
                    }
                });
                var myPieChart3 = new Chart(ctx2, {
                    type: 'pie',
                    data: data3,
                    options: {
                        title: {
                            display: true,
                            text: 'Porcentaje de eficacia de los Planes de Acción',
                            top: 'bottom',
                            fontSize: 12
                        },
                        tooltips: {
                            callbacks: {
                                label: function (tooltipItem, data) {
                                    var allData = data.datasets[tooltipItem.datasetIndex].data;
                                    var tooltipLabel = data.labels[tooltipItem.index];
                                    var tooltipData = allData[tooltipItem.index];
                                    var total = 0;
                                    for (var i in allData) {
                                        total += allData[i];
                                    }
                                    var tooltipPercentage = Math.round((tooltipData / total) * 100);
                                    return tooltipLabel + ': ' + tooltipData + ' (' + tooltipPercentage + '%)';
                                }
                            }
                        }
                    }
                });



            }
        }
    });
}
function paginador(idTabla, nameTr, pagination) {
    jQuery(function ($) {
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

function BuscarPlanesAccion() {
var modulo = $("#modulo").val()
var fechaInicial = $("#fechaInicial").val()
var fechaFinal = $("#fechaFinal").val()
if (modulo == "" && fechaInicial == "" && fechaFinal == "") {
    swal(
               'Estimado Usuario',
               'Debe seleccionar un campo de búsqueda',
               'warning'
               )
    return false;
}

if (modulo == "") {
    swal(
               'Estimado Usuario',
               'Debe seleccionar un Origen de búsqueda',
               'warning'
               )
    return false;
}

if (fechaFinal != "")
{

    if (fechaFinal != "" && fechaInicial == "" && modulo != "") {
        swal(
                   'Estimado Usuario',
                   'Por favor ingresar una Fecha Inicial',
                   'warning'
                   )
        return false;
    }

}

if (fechaInicial != "") {

    if (fechaFinal == "" && fechaInicial != "" && modulo != "") {
        swal(
                   'Estimado Usuario',
                   'Por favor ingresar una Fecha Final',
                   'warning'
                   )
        return false;
    }

}

if ($.datepicker.parseDate('dd/mm/yy', fechaFinal) < $.datepicker.parseDate('dd/mm/yy', fechaInicial)) {
    swal(
                'Estimado Usuario',
                'La fecha final no puede ser inferior a la fecha inicial',
                'warning'
                )
    return false;

}

    $("#formPlanDeAccion").submit();
}
