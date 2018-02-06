//Mostrar cargos en Modal
$(function () {
    $(".MostrarCargos").click(function () {
        $('#tabla_modal').html('');
        $("#Nombre_EPP").empty();

        $("#val-error").css("display", "none");
        $("#val-error").text('');

        var modal = document.getElementById('myModal');
        var row = $(this).attr('name');
        var NombreEPP = $(this).attr('nameEPP');
        $("#Nombre_EPP").append(NombreEPP);
        $('#IdTabla').val(row.toString());
        var IdTabla = 'TablaCargoEPP ' + row;
        var TablaElegida = document.getElementById(IdTabla);
        $('#tabla_modal').html($(TablaElegida).html());
        //$(TablaElegida).contents().appendTo('#tabla_modal');
        modal.style.display = "block";
    });
});
//Funciones del Modal cargos
$(document).ready(function () {

    var modal = document.getElementById('myModal');
    var span = document.getElementById("close_modal");
    span.onclick = function () {
        modal.style.display = "none";
        $("#val-error").css("display", "none");
        $("#val-error").text('');
    }
    document.addEventListener('keyup', function (e) {
        if (e.keyCode == 27) {
            modal.style.display = "none";
            $("#val-error").css("display", "none");
            $("#val-error").text('');
        }
    });
});


//Establecer número de cargos por tabla
function ContadorCargos(NombreTabla) {
    var location = ".Epptable #" + NombreTabla + " > tbody";
    var location_span = "#Span" + NombreTabla;
    var tableBody = $(location);
    var span = $(location_span);
    var cont = 0;
    tableBody.find('tr').each(function () {
        cont = cont + 1;
    });
    span[0].innerText = " (" + cont.toString() + ")";
};
$(document).ready(function () {
    $('#Grid3 > tbody').find('tr.paginacc').each(function () {
        var row = $(this);
        var cont = 0;
        row.find('td').each(function () {
            var row1 = $(this);
            if (cont == 10) {
                row1.find('table').each(function () {
                    var table_cargos_tr = $(this);
                    var Id_table = table_cargos_tr.attr('id')
                    ContadorCargos(Id_table);
                });
            }
            cont++;
        });

    });
});


///Imagenes///
//Mostrar Imagenes en Modal1
$(function () {
    $(".MostrarImagen").click(function () {
        $('#imagen_modal').html('');
        $("#Nombre_EPP_img").empty();
        var modal = document.getElementById('myModal1');
        var row = $(this).attr('name');
        var NombreEPP = $(this).attr('nameEPP');
        $("#Nombre_EPP_img").append(NombreEPP);
        $('#IdTabla_img').val(row.toString());
        var IdTabla = 'ImagenEPP ' + row;
        var TablaElegida = document.getElementById(IdTabla);
        $('#imagen_modal').html($(TablaElegida).html());
        modal.style.display = "block";
    });
});
//Funciones del Modal1
$(document).ready(function () {
    var modal = document.getElementById('myModal1');
    var span = document.getElementById("close_modal1");
    span.onclick = function () {
        modal.style.display = "none";
    }
    document.addEventListener('keyup', function (e) {
        if (e.keyCode == 27) {
            modal.style.display = "none";
        }
    });
});

//Eliminar EPP
$(document).ready(function () {
    $('.btnEliminarEPP').click(function () {
        var Id_Elm = $(this).attr('id');
        var Id_Elm1 = $(this).attr('name');
        swal({
            title: "Estimado Usuario",
            text: "Esta seguro(a) que desea eliminar el EPP: " + Id_Elm1 + "?",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Si",
            cancelButtonText: "No",
            type: "warning",
            closeOnConfirm: false
        },
        function () {

            $.ajax({
                type: "POST",
                url: "/AdmoEPP/EliminarEPP",
                data: '{IdEPP: "' + Id_Elm + '" }',
                contentType: "application/json; charset=utf-8",
                cache: false,
                dataType: "json",
                success: function (response) {
                    if (response.probar == false) {
                        if (response.resultado == "El usuario no ha iniciado sesión en el sistema") {
                            location.reload(true);
                        }
                        swal({
                            title: "Estimado Usuario",
                            text: response.resultado,
                            confirmButtonColor: "#DD6B55",
                            type: "warning",
                            closeOnConfirm: false
                        });
                    }
                    else {
                        swal({
                            title: "Estimado Usuario",
                            text: response.resultado,
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "OK",
                            type:"success",
                            closeOnConfirm: false
                        },
                        function () {
                            location.reload(true);
                        });
                    }

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $("#msj_novedad").text('No se ha podido eliminar el EPP');
                    $("#div_novedad").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                }
            });

        });
    });
});



//Exportar Matriz
$(function () {
    $("#DescargarMatriz").click(function () {
        var Seleccion = $("#Formato option:selected").val();
        var stringArray = new Array();
        $('#Grid3 > tbody').find('tr.paginacc').each(function () {
            var row = $(this);
            var IdEPP = row.attr('name');
            if (IdEPP != null) {
                stringArray.push(IdEPP);
            }
        });
        if (Seleccion == "PDF") {
            var postData = { values: stringArray };
            $.ajax({
                type: "POST",
                url: "/AdmoEPP/ValidarMatriz",
                data: postData,
                dataType: "json",
                //traditional: true,
                success: function (data) {
                    if (data.probar == true) {
                        window.location.href = '/AdmoEPP/ExportMatrizPDF?resultado=' + data.resultado + '&IdEmpresa=' + data.IdEmpresa;
                    }
                    else {

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {

                }

            });

        }
        if (Seleccion == "EXCEL") {
            var postData = { values: stringArray };
            $.ajax({
                type: "POST",
                url: "/AdmoEPP/ValidarMatriz",
                data: postData,
                dataType: "json",
                //traditional: true,
                success: function (data) {
                    if (data.probar == true) {
                        window.location.href = '/AdmoEPP/ExportMatrizExcel?resultado=' + data.resultado + '&IdEmpresa=' + data.IdEmpresa;
                    }
                    else {

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {

                }

            });


        }

    });
});