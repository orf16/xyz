var urlBase = ""
var urlAfiliado = '/Usuarios';
var $idtextareaMision = $("#mision");
var $idparrafoMision = $("#mision1");
var $idtextareaVision = $("#vision");
var $idparrafoVision = $("#vision1");

var $idMunicipio = $("#Fk_Id_Municipio");
var $idDepartamento = $("#Fk_Id_Departamento");

/// Tipo de elementos matriz del analisis 
var $idElementosDofa = $("#elementosMatriz");

/// Variables elementos DOFA
var $idDivPanel1 = $("#amenazas");
var $idDivPanel = $("#opotunidades");
var $idDivPanel3 = $("#debilidades");
var $idDivPanel2 = $("#fortalezas");
var $idtxtDofa = $("#idDofa");
var $idtxtPest = $("#idPest");
var $idTextAreaDescripcion = $("#descripcion");

////VARIABLES ELEMENTOS PEST
//var $idElementosPest = $("#elementosPest")
var $idDivPanelp = $("#politico");
var $idDivPanele = $("#economico");
var $idDivPanels = $("#social");
var $idDivPanelt = $("#tecnologico");
var $idTextAreaDescripcionp = $("#descripcionp");

var $idInputFKMatriz = $("#FK_Matriz");
var $idInputPKElementoMatriz = $("#PK_Elemento_Matriz");

/// Valor tipos de elementos de las matrices DOFA y PEST, lo valores son la claves primarias de los elementos en la bd
var PKAMENAZAS = 1;
var PKDEBILIDADES = 2;
var PKFORTALEZAS = 3;
var PKOPORTUNIDADES = 4;
var PKPOLITICO = 5;
var PKECONOMICO = 6;
var PKSOCIAL = 7;
var PKTECNOLOGIACO = 8;

try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("Revisa tienes errores");
};

/// Funcion para Buscar y Mostrar la Mision
function BuscarMision() {
    PopupPosition();
    $.ajax({
        url: urlBase + '/Gobierno/CargarMision',  //primero el modulo/controlador/metodo que esta en el controlador
        data: {                                   //se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            nit: 11
        },
        type: 'POST',
        success: function (result) {
            OcultarPopupposition();
            if (result) {
                $idtextareaMision.text(result.Mision);
                $idparrafoMision.html(result.Mision);
            }
        }
    });
}

////Funcion para buscar Y mostrar la Vision.
function BuscarVision() {
    PopupPosition();
    $.ajax({
        url: urlBase + '/Gobierno/CargarVision',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            nit: 11
        },
        type: 'POST',
        success: function (result) {
            OcultarPopupposition();
            if (result) {
                $idtextareaVision.text(result.Vision);
                $idparrafoVision.html(result.Vision);
            }
        }
    });
}






//Funcion para Eliminar Archivo Fisico del Servidor. 
function EliminarArchivo(pkorganigrama) {

    $.ajax({
        url: urlBase + '/Organigrama/Eliminar',
        data: {
            idorganigrama: pkorganigrama
        },
        type: 'POST',
        success: function (result) {

            if (result.success) {
                $("#modalesEliminados").append($("#modalEliminar" + pkorganigrama));
                $div.remove();

                swal(
                    'Estimado Usuario',
                    'Se ha eliminado el archivo',
                    'success'
                    )
            } else {
                swal(
                   'Estimado Usuario',
                   'No ha sido posible eliminar el archivo',
                   'error'
                   )
            }
        }
    });
}

////Funcion para buscar los municipios por departamento
function BuscarMunicipios() {
    PopupPosition();
    $.ajax({
        url: urlBase + '/General/ObtenerMunicipios',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            PK_Departamento: $idDepartamento.val()
        },
        type: 'POST',
        success: function (result) {
            OcultarPopupposition();
            if (result) {
                $idMunicipio.find("option").remove();//Removemos las opciones anteriores 
                $idMunicipio.append(new Option("-- Seleccionar Municipio --", ""));// agregamos la opcion de seleccionar              
                $.each(result, function (ind, element) {
                    $idMunicipio.append(new Option(element.NombreMunicipio, element.PK_Municipio));//agregamos las opciones consultadas
                })
            }
        }
    });
}



function AgregarElementoMatriz(ElementoMatriz) {
    $.ajax({
        url: urlBase + '/Consideraciones/elementoMatriz',
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            elementoMatriz: ElementoMatriz
        },
        type: 'POST',
        success: function (result) {
            if (result.success) {
                var html1 = '<div class="img-thumbnail col-md-12" id="nuevoElemento" name="nuevoElemento"><div><a class="pull-right" id="removeImage" name="removeImage" onclick="EliminarElemento(this,';
                var html2 = ')"><span class="glyphicon glyphicon-erase btn-md"></span></a><a class="pull-right" id="removeImage" name="removeImage" onclick="editarElemento(';
                var html3 = ')"><span class="glyphicon glyphicon-check btn-md"></span></a></div><div><em><b>';
                var html4 = "</b></em> </div></div>";

                if (result.FK_TipoElementoAnalisis == PKOPORTUNIDADES) {
                    var item = html1 + result.PK_Elemento_Matriz + html2 + result.PK_Elemento_Matriz + html3 + result.Descripcion_Elemento + html4;
                    $idInputPKElementoMatriz.val(result.PK_Elemento_Matriz);
                    $idInputFKMatriz.val(result.FK_Matriz);
                    $idDivPanel.append(item)
                }

                if (result.FK_TipoElementoAnalisis == PKAMENAZAS) {
                    var item = html1 + result.PK_Elemento_Matriz + html2 + result.PK_Elemento_Matriz + html3 + result.Descripcion_Elemento + html4;
                    $idInputPKElementoMatriz.val(result.PK_Elemento_Matriz);
                    $idInputFKMatriz.val(result.FK_Matriz);
                    $idDivPanel1.append(item)
                }

                if (result.FK_TipoElementoAnalisis == PKFORTALEZAS) {
                    var item = html1 + result.PK_Elemento_Matriz + html2 + result.PK_Elemento_Matriz + html3 + result.Descripcion_Elemento + html4;
                    $idInputPKElementoMatriz.val(result.PK_Elemento_Matriz);
                    $idInputFKMatriz.val(result.FK_Matriz);
                    $idDivPanel2.append(item)
                }

                if (result.FK_TipoElementoAnalisis == PKDEBILIDADES) {
                    var item = html1 + result.PK_Elemento_Matriz + html2 + result.PK_Elemento_Matriz + html3 + result.Descripcion_Elemento + html4;
                    $idInputPKElementoMatriz.val(result.PK_Elemento_Matriz);
                    $idInputFKMatriz.val(result.FK_Matriz);
                    $idDivPanel3.append(item)
                }

                if (result.FK_TipoElementoAnalisis == PKPOLITICO) {
                    var item = html1 + result.PK_Elemento_Matriz + html2 + result.PK_Elemento_Matriz + html3 + result.Descripcion_Elemento + html4;
                    $idInputPKElementoMatriz.val(result.PK_Elemento_Matriz);
                    $idInputFKMatriz.val(result.FK_Matriz);
                    $idDivPanelp.append(item)
                }

                if (result.FK_TipoElementoAnalisis == PKECONOMICO) {
                    var item = html1 + result.PK_Elemento_Matriz + html2 + result.PK_Elemento_Matriz + html3 + result.Descripcion_Elemento + html4;
                    $idInputPKElementoMatriz.val(result.PK_Elemento_Matriz);
                    $idInputFKMatriz.val(result.FK_Matriz);
                    $idDivPanele.append(item)
                }

                if (result.FK_TipoElementoAnalisis == PKSOCIAL) {
                    var item = html1 + result.PK_Elemento_Matriz + html2 + result.PK_Elemento_Matriz + html3 + result.Descripcion_Elemento + html4;
                    $idInputPKElementoMatriz.val(result.PK_Elemento_Matriz);
                    $idInputFKMatriz.val(result.FK_Matriz);
                    $idDivPanels.append(item)
                }

                if (result.FK_TipoElementoAnalisis == PKTECNOLOGIACO) {
                    var item = html1 + result.PK_Elemento_Matriz + html2 + result.PK_Elemento_Matriz + html3 + result.Descripcion_Elemento + html4;
                    $idInputPKElementoMatriz.val(result.PK_Elemento_Matriz);
                    $idInputFKMatriz.val(result.FK_Matriz);
                    $idDivPanelt.append(item)
                }
                //   location.reload();//con esta instruccion se recarga la pagina
                location.reload();
            }
        }
    });
}

function EliminarElemento9(elemento, Pk_elementoMatriz) {


    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then(function () {
        swal(
          'Deleted!',
          'Your file has been deleted.',
          'success'
        )
    })
    EliminarElemento_Matriz(elemento, Pk_elementoMatriz);
}


function EliminarElemento(elemento, Pk_elementoMatriz) {

    swal({
        title: "Estimado Usuario",
        text: "¿Está seguro de eliminar el ítem?",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Aceptar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false
    },
    function () {
        swal(
            "Estimado Usuario",
            "El ítem fué eliminado con éxito.",
            "success"
        );

        $.ajax({
            url: urlBase + '/Consideraciones/EliminarElementoMatriz',
            data: {
                Pk_ElementoMatriz: Pk_elementoMatriz
            },
            type: 'POST',
            success: function (result) {
                if (result.success) {
                    $(elemento).closest("div[name =nuevoElemento]").remove();
                }
            }
        });
    });

}

function BusquedaRelacionesLaborales() {

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



/////Funcion para buscar Procesos
function BuscarProceso(element) {
    var searchstring = $(element).val()
    $.ajax({

        url: urlBase + '/Proceso/BusquedaProceso',//primero el modulo/controlador/metodo que esta en el controlador
        data: {// se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            searchstring: searchstring
        },
        type: 'POST',
        success: function (result) {
            if (result) {
                $('#tablaPV').html(result)

            }
        }
    });
}

////funcion que permite guardar la edicion del elemento matriz
function GrabarEdicionMatriz() {
    var selectedIDs = new Array();
    $('input:checkbox.checkBox').each(function () {
        if ($(this).prop('checked')) {
            selectedIDs.push($(this).val());
        }
    });
    var ElementoMatriz = new Object();
    ElementoMatriz.PK_Elemento_Matriz = selectedIDs;
    ElementoMatriz.Descripcion_Elemento = $idtxtDofa.val();

    $.ajax({
        url: urlBase + '/Consideraciones/MatrizEditarElemento',
        data: {
            elementoMatriz: ElementoMatriz
        },
        type: 'POST',
        success: function (result) {
            if (result.success) {
                location.reload();//con esta instruccion se recarga la pagina

            }
        }
    });
}

///Funcion que valida que no ingresen datos nulos en los textos de empresa
function ValidacionCrearEmpresa() {
    $('#grabarempresa').validate({
        errorClass: "error",

        rules: {
            Nit_Empresa: { required: true, },
            Razon_Social: { required: true },
            Sector: { required: true },
            Fk_Id_Departamento: { required: true },
            Fk_Id_Municipio: { required: true },
            Telefono: { required: true, min: 1, digits: true },
            Fax: { required: true },
            Codigo_Actividad: { required: true },
            Descripcion_Actividad: { required: true },
            Email: { required: true },
            Fk_Id_CIU: { required: true },
            Flg_Estado: { required: true },
            Fecha_Vigencia_Actual: { required: true },
        },
        messages: {
            Nit_Empresa: {
                required: "Ingresar un NIT"
            },

            Razon_Social: {
                required: "El nombre de la Empresa no puede estar vacío"
            },

            Sector: {
                required: "Seleccionar un Sector"
            },

            Fk_Id_Departamento: {
                required: "Seleccionar un Departamento"
            },

            Fk_Id_Municipio: {
                required: "Selecciona un Municipio"
            },

            Telefono: {
                required: "El Teléfono no puede estar vacío"
            },

            Fax: {
                required: "Debes ingresar mínimo un número"
            },

            Codigo_Actividad: {
                required: "El Código Actividad es requerido"
            },

            Descripcion_Actividad: {
                required: "La Descripción Actividad es requerido"
            },

            Email: {
                required: "El Email es requerido"
            },

            Flg_Estado: {
                required: "El estado es requerido"
            },

            Fecha_Vigencia_Actual: {
                required: "La Fecha Vigencia Actual es requerido"
            },

        }
    });
}

////funcion que permite pintar en el modal la descripcion del elemento matriz- para su edicion
function CargarElementoDOFA() {
    var i = 0;
    var selectedIDs = new Array();
    $('input:checkbox.checkBox').each(function () {
        if ($(this).prop('checked')) {
            selectedIDs.push($(this).val());
        }
    });
    $.ajax({
        url: urlBase + '/Consideraciones/CargarMatrizElemento2',
        data: {
            customerIDs: selectedIDs
        },
        type: 'POST',
        success: function (result) {
            if (result.success) {
                $idtxtDofa.text(result.jsmatriz_Cargar);
            }
        }
    });
}



////funcion que permite pintar en el modal la descripcion del elemento matriz- para su edicion
function CargarElementoPEST() {
    var i = 0;
    var selectedIDs = new Array();
    $('input:checkbox.checkBox').each(function () {
        if ($(this).prop('checked')) {
            selectedIDs.push($(this).val());
        }
    });
    $.ajax({
        url: urlBase + '/Consideraciones/CargarMatrizElemento2',
        data: {
            customerIDs: selectedIDs
        },
        type: 'POST',
        success: function (result) {
            if (result.success) {
                $idtxtPest.text(result.jsmatriz_Cargar);

            }
        }
    });
}




$(document).ready(function () {
    $('input[type=checkbox]').on('click', function () {
        var parent = $(this).parent().attr('id');
        $('#' + parent + ' input[type=checkbox]').removeAttr('checked');
        $(this).attr('checked', 'checked');
    });
});



$('#Numero_Documento').on('change', function () {
    var documento = $("#Numero_Documento").val();
    if (documento != '') {
        DatosTrabajador(documento);
    } else
        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'Ingrese un Documento de Cedula',
            confirmButtonColor: '#7E8A97'
        });
    return false;
});



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
///Funcion para Cargar Logo de la Empresa
//$("#logocargado").hide();
function AdjuntarArchivo() {
    PopupPosition();
    var formData = new FormData();
    var file = document.getElementById("archivo").files[0];
    var idEmpresa = $("#idEmpresa").val();
    formData.append("archivo", file);
    formData.append("idEmpresa", idEmpresa);
    $.ajax({
        type: "POST",
        url: '/Empresas/AgregarAdjunto',
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (result) {
            if (result) {
                if (result.Mensaje == "OK") {
                    OcultarPopupposition();
                    $("#cargue").hide();
                    $("#logocargado").show();
                    $("#imagen").attr("src", "/Content/Logo_Empresa/" + result.Data.NombreArchivo);
                    swal({
                        title: "Estimado Usuario",
                        text: "El logo se cargó correctamente.",
                        type: "warning",
                        showCancelButton: false,
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Aceptar",
                        confirmButtonColor: '#7E8A97',
                        closeOnConfirm: false,
                        closeOnCancel: false
                    },
                    function (isConfirm) {
                        if (isConfirm) {
                            window.location.href = "../Empresas/Create";
                        }
                    });
                }
                else {
                    if (result.Mensaje == "ERRORTIPO") {
                        swal({
                            type: 'warning',
                            title: 'Estimado Usuario',
                            text: 'Sólo se pueden cargar archivos en formato, JPG o PNG.',
                            confirmButtonColor: '#7E8A97'
                        });
                        OcultarPopupposition();
                    }
                    if (result.Mensaje == "ERRORVACIO") {
                        swal({
                            type: 'warning',
                            title: 'Estimado Usuario',
                            text: 'Por favor seleccione el archivo y presione nuevamente Cargar Logo.',
                            confirmButtonColor: '#7E8A97'
                        });
                    }
                    OcultarPopupposition();
                }
            }
            else {
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'No se pudo realizar la carga, error en la transacción.',
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
            })
            OcultarPopupposition();
        }
    });

}
/////Function Obtener Logo en HEader
function ObtenerLogo(idEmpresa) {
    PopupPosition();
    $.ajax({
        url: urlBase + '/Home/ObtenerLogo',
        data: { pkempresa: idEmpresa },
        type: 'POST',
        success: function (result) {
            if (result.Data != "") {
                OcultarPopupposition();
                $("#logo").attr("src", "/Content/Logo_Empresa/" + result);

            }
            else {
                if (result.Mensaje == "No Existe Logo") {
                    OcultarPopupposition();
                    $("#logo").attr("src", "/Images/water-mark-no-logo.png");
                }

            }
        }, error: function (result) {
            swal({
                type: 'error',
                title: 'Estimado Usuario',
                text: 'Se presentó un error, intente más tarde.',
                confirmButtonColor: '#7E8A97'
            })
            OcultarPopupposition();
        }
    });
}
///funcion para eliminar el logo que se encuentre Cargado de la Empresa.
function EliminarLogo(IdEmpresa) {
    $.ajax({
        type: "post",
        url: '/Empresas/EliminarLogo',
        data: { pkempresa: IdEmpresa },
        success: function (result) {
            if (result.Data == 1) {
                swal({
                    title: "Estimado Usuario",
                    text: "El logo se Eliminó del Sistema Correctamente.",
                    type: "warning",
                    showCancelButton: false,
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Aceptar",
                    confirmButtonColor: '#7E8A97',
                    closeOnConfirm: false,
                    closeOnCancel: false
                },
                                  function (isConfirm) {
                                      if (isConfirm) {
                                          window.location.href = "../Home/Index";
                                      }
                                  });
            }

        },
        error: function (result) {
            swal({
                type: 'error',
                title: 'Estimado Usuario',
                text: 'Se presentó un error, intente más tarde.',
                confirmButtonColor: '#7E8A97'
            })
            OcultarPopupposition();
        }



    });
}
///Funcion para moStrar automaticamente el logo de la Empresa en caso de Existir almacenado.
function MostrarLogoCargado() {
    PopupPosition();
    $.ajax({
        type: "post",
        data: { nitempresa: $("#nitempresa").val() },
        url: '/Empresas/MostrarLogoCargado',
        success: function (result) {
            OcultarPopupposition();
            if (result.Data != "") {
                $("#imagen").attr("src", "../Content/Logo_Empresa/" + result);
               // $("#logo").attr("src", "/Content/Logo_Empresa/" + result);
                $("#cargue").hide();
            }
            else {
                //$("#logo").attr("src", "/Images/water-mark-no-logo.png");
                $("#logocargado").hide();
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'Aún no se ha cargado el Logo de la Empresa.',
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
            })
            OcultarPopupposition();
        }



    });
}
///Consulta la informacion del trabajador
function DatosTrabajador(documento) {
    if (documento == '')
        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'Campo Obligatorio',
            confirmButtonColor: '#7E8A97'
        });
    PopupPosition();
    $.ajax({
        type: "post",
        data: { numeroDocumento: documento },
        url: urlBase + urlAfiliado + '/ConsultarDatosTrabajador'
    }).done(function (response) {
        if (response != undefined && response != '' && response.Mensaje == 'OK') {
            var trabajador = response.Data;
            var nombre = trabajador.nombre1 + ' ' + trabajador.nombre2 + ' ' + trabajador.apellido1 + ' ' + trabajador.apellido2;
            $('#Nombre_Usuario').val(nombre);
        } else if (response != undefined && response != '' && response.Mensaje != '') {
            $("#Documento").val('');
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
        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'No se pudo obtener la información, por favor intente más tarde',
            confirmButtonColor: '#7E8A97'
        });
        OcultarPopupposition();
    });
}

//funcion para mostrar los datos de la integracion con Siarp
function ObtenerSiarp() {
    PopupPosition();
 
    $.ajax({
        url: urlBase + '/Empresas/ObtenerSiarp',  //controlador/metodo que esta en el controlador
        data: {                                           // se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            nitempresa: $("#nitempresa").val()
        },
        type: 'POST',
        success: function (result) {
            OcultarPopupposition();
            if (result) {
                //$("#cc").val(result.Data[0].tipoDoc);
                $("#cc").css("background-color", "#DDD")
                .css("color", "black");;

                $("#idempresa").val(result.Data[0].idEmpresa);
                $("#idempresa").css("background-color", "#DDD").css("color", "black");;


                $("#idrepresentante").val(result.Data[0].idRepresentanteLegal);
                $("#idrepresentante").css("background-color", "#DDD")
                .css("color", "#3B3A3A");;


                $("#razonsocial").val(result.Data[0].razonSocial);
                $("#razonsocial").css("background-color", "#DDD").css("color", "black");;

                $("#direccion").val(result.Data[0].direccionEmpresa);
                $("#direccion").css("background-color", "#DDD").css("color", "black");;


                $("#email").val(result.Data[0].emailEmpresa);
                $("#email").css("background-color", "#DDD").css("color", "black");;


                $("#www").val(result.Data[0].paginaWeb);
                $("#www").css("background-color", "#DDD").css("color", "black");;


                $("#telefono").val(result.Data[0].telefonoEmpresa);
                $("#telefono").css("background-color", "#DDD").css("color", "black");;


                $("#fax").val(result.Data[0].faxEmpresa);
                $("#fax").css("background-color", "#DDD").css("color", "black");;


                $("#zona").val(result.Data[0].zona);
                $("#zona").css("background-color", "#DDD").css("color", "black");;


                $("#riesgo").val(result.Data[0].riesgo);
                $("#riesgo").css("background-color", "#DDD").css("color", "black");;


                $("#totalemp").val(result.Data[0].numeroDeTrabajadores);
                $("#totalemp").css("background-color", "#DDD").css("color", "black");;

                $("#estado").val(result.Data[0].estado);
                $("#estado").css("background-color", "#DDD").css("color", "black");;

                $("#fecha").val(result.Data[0].fecAfiliaEfectiva);
                $("#fecha").css("background-color", "#DDD").css("color", "black");;

                $("#idseccional").val(result.Data[0].idSeccional);
                $("#idseccional").css("background-color", "#DDD").css("color", "black");;

                $("#departamento").val(result.Data[0].departamento);
                $("#departamento").css("background-color", "#DDD").css("color", "black");;

                $("#municipio").val(result.Data[0].municipio);
                $("#municipio").css("background-color", "#DDD").css("color", "black");;

                $("#idsector").val(result.Data[0].idSectorEconomico);
                $("#idsector").css("background-color", "#DDD").css("color", "black");;

                $("#codactividad").val(result.Data[0].actividadEconomica);
                $("#codactividad").css("background-color", "#DDD").css("color", "black");;

                $("#actecon").val(result.Data[0].nomActEconomico);
                $("#actecon").css("background-color", "#DDD").css("color", "black");;
            }
            else {

                swal('Estimado Usuario', 'No se logró obtener datos de la empresa. Por favor, Intente más tarde.', 'warning');
            }
        }
    }).fail(function (result) {
        $("#nitempresa").val('');
        swal('Estimado Usuario', 'No se logró obtener datos de la empresa. Por favor, Intente más tarde.', 'warning');
        OcultarPopupposition();
    });
}

//Funcion para validar que ingresen un dato en la caja de texto para obtener Siarp Centro de Costo.

function ObtenerSiarpCentro(nit) {
    PopupPosition();
    $.ajax({
        url: urlBase + '/Sede/ObtenerSiarpCentro',  //controlador/metodo que esta en el controlador
        data: {                                           // se colocan los parametros a enviar... en este caso no porque los voy es a obtener.
            nitempresa: nit
        },
        type: 'POST',
        success: function (result) {
            OcultarPopupposition();
            if (result) {
                $('#tablacentros').empty();
                $('#tablacentros').append
                //('<tr class="titulos_tabla"> <td style="border-right: 1px solid lightslategray; border-bottom: 1px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase"><b>Seleccionar</b></td><td style="border-right: 1px solid lightslategray; border-bottom: 2px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase"><b>Actividad Economica</b></td> <td style="border-right: 1px solid lightslategray; border-bottom: 1px solid lightslategray; text-align:center; vertical-align:middle; text-transform:uppercase"><b>Codigo Actividad</b></td></tr>');
                $.each(result.Data, function (ind, element) {
                    var elemento = '<tr name="pcs">' +
                                    '<td style="vertical-align:middle; border-right: 2px solid lightslategray;border-left:2px solid lightslategray;text-align:center;color:black;background-color:rgba(196, 202,196, 0.1);font-size:15px">' + '<label><input type="checkbox" name="centros" id="centros"></label>' + '</td>' +
                                    //'<td style="border-right: 2px solid lightslategray; vertical-align:middle"><input type="hidden" name="idCentroTrabajo" value="' + element.idCentroTrabajo + '">' + element.idCentroTrabajo + '</td>' +
                                    '<td style="vertical-align:middle; border-right: 2px solid lightslategray;border-left:2px solid lightslategray ;text-align:justify;color:black;background-color:rgba(196, 202,196, 0.1);font-size:15px"><input type="hidden" name="nombreActEcon" value="' + element.nombreActEcon + '">' + element.nombreActEcon + '</td>' +
                                    '<td style="vertical-align:middle; border-right: 2px solid lightslategray;border-left:2px solid lightslategray; text-align:center;color:black;background-color:rgba(196, 202,196, 0.1);font-size:15px"><input type="hidden" name="idActiEconomica" value="' + element.idActiEconomica + '">' + element.idActiEconomica + '</td>' +
                                    //'<td style="border-right: 2px solid lightslategray; vertical-align:middle"><input type="hidden" name="nomDepartamento" value="' + element.nomDepartamento + '">' + element.nomDepartamento + '</td>' +
                                    //'<td style="vertical-align:middle"><input type="hidden" name="nomMunicipio" value="' + element.nomMunicipio + '">' + element.nomMunicipio + '</td>' +
                                   '</tr>'
                    $('#tablacentros').append(elemento)
                })
                paginador("#tablacentros", "tr[name = pcs]", "#paginador1")
            }
            else {
                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'En este momento no es posible obtener las Actividades Economicas, por favor intente mas tarde',
                    confirmButtonColor: '#7E8A97'
                })

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
/////funcion que envia al controlador los centros de trabajo que se obtienen de SIARP y seleccionados(Checkbox) en la vista. 
function ValidarCentrosCheck() {
    var $filascheck = $("#tablacentros").find("input[name='centros']:checked");
    if ($filascheck.length > 0) {
        $filascheck.each(function (ind, fila) {
            var tr = $(fila).closest('tr')
            $(fila).closest('tr').find("input[name='idCentroTrabajo']").attr("name", "centro[" + ind + "].ID_Centro");
            tr.find("input[name='nombreActEcon']").attr("name", "centro[" + ind + "].Descripcion_Actividad");
            tr.find("input[name='idActiEconomica']").attr("name", "centro[" + ind + "].Codigo_Actividad");
            //tr.find("input[name='nomDepartamento']").attr("name", "centro[" + ind + "].nomDepartamento");
            //tr.find("input[name='nomMunicipio']").attr("name", "centro[" + ind + "].nomMunicipio");
        });
    }
}


///Funcion para eliminar una Actividad de una sede.. 
function EliminarActividadEconomica(Pk_Id_Centro_de_Trabajo) {
    //PopupPosition();
    swal({
        title: 'Estimado Usuario',
        text: "¿Seguro desea eliminar la Actividad Económica de la Sede?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#7E8A97',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Eliminar'
    }, (function (isConfirm) {
        if (isConfirm) {
            //PopupPosition();
            $.ajax({
                url: urlBase + '/Sede/EliminarActividadEconomica',
                data: {

                    pkactividad: Pk_Id_Centro_de_Trabajo,

                },
                type: 'POST',
                success: function (result) {
                    location.reload(true);
                    //swal({
                    //    title: 'Estimado Usuario',
                    //    text: "Actividad Economica Eliminada para la Sede Seleccionada.",
                    //    type: 'warning',
                    //    showConfirmButton: true,
                    //    confirmButtonColor: '#7E8A97',
                    //    cancelButtonColor: '#d33',
                    //    cancelButtonText: 'Cancelar',
                    //    confirmButtonText: 'Aceptar'
                    //},
                    //(function (isConfirm) {
                    //    if (isConfirm) {

                    //        OcultarPopupposition();
                    //    }
                    //}))
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
        else {
            OcultarPopupposition();
        }
    }))

}

///FUncion para paginador
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

//Modulo dofa -pest funcion que permite verificar si esta seleccionado un checkbox en DOFA-PEST PARA ABRIR el modal o mostra mensaje de alerta(seleccione item para editar) 
function validarcheckbox_seleccionadoDOFA() {

    var selectedIDs = new Array();
    $('input:checkbox.checkBox').each(function () {
        if ($(this).prop('checked')) {
            selectedIDs.push($(this).val());
        }
    });

    if (selectedIDs > 0) {

        $('#ModaleditarDOFA').modal("show");
        CargarElementoDOFA();
    }
    else {
        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'Seleccione un sólo ítem para editar por favor.',
            timer: 4000,
            confirmButtonColor: '#7E8A97'
        }).then(
             function () { },
             function (dismiss) {
                 if (dismiss === 'timer') {
                     console.log('Cerrado')
                 }
             }
             )
    }
}

//Modulo dofa -pest funcion que permite verificar si esta seleccionado un checkbox en DOFA-PEST PARA ABRIR el modal o mostra mensaje de alerta(seleccione item para editar) 
function validarcheckbox_seleccionadoPEST() {

    var selectedIDs = new Array();
    $('input:checkbox.checkBox').each(function () {
        if ($(this).prop('checked')) {
            selectedIDs.push($(this).val());
        }
    });

    if (selectedIDs > 0) {

        $('#ModaleditarPEST').modal("show");
        CargarElementoPEST();
    }
    else {
        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'Seleccione un sólo ítem para editar por favor.',
            timer: 4000,
            confirmButtonColor: '#7E8A97'
        }).then(
             function () { },
             function (dismiss) {
                 if (dismiss === 'timer') {
                     console.log('Cerrado')
                 }
             }
             )
    }
}





//metodo para validar que se ingresen los item de matriz DOFA
function validacion_AgregarMatrizDOFA() {
    if ($('#descripcion').val() === '' && $('#idelementomatriz').val() === '') {

        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'Por favor ingrese el tipo y la descripción del elemento DOFA.',
            timer: 4000,
            confirmButtonColor: '#7E8A97'
        }).then(
                        function () { },
                        function (dismiss) {
                            if (dismiss === 'timer') {
                                console.log('Cerrado')
                            }
                        }
                        )
    } else
        if ($('#descripcion').val() === '') {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Por favor ingrese la descripción del elemento DOFA.',
                timer: 4000,
                confirmButtonColor: '#7E8A97'
            }).then(
                            function () { },
                            function (dismiss) {
                                if (dismiss === 'timer') {
                                    console.log('Cerrado')
                                }
                            }
                            )
        } else
            if ($('#idelementomatriz').val() === '') {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'Por favor ingrese el tipo de elemento DOFA.',
                    timer: 4000,
                    confirmButtonColor: '#7E8A97'
                }).then(
                                function () { },
                                function (dismiss) {
                                    if (dismiss === 'timer') {
                                        console.log('Cerrado')
                                    }
                                }
                                )
            }
            else {

                var ElementoMatriz = new Object();
                ElementoMatriz.PK_Elemento_Matriz = $idInputPKElementoMatriz.val();
                ElementoMatriz.Descripcion_Elemento = $idTextAreaDescripcion.val();
                ElementoMatriz.FK_TipoElementoAnalisis = $('#idelementomatriz').val();
                ElementoMatriz.FK_Matriz = $idInputFKMatriz.val();



                AgregarElementoMatriz(ElementoMatriz);
            }
}


//metodo para validar que se ingresen los item de matriz PEST
function validacion_AgregarMatrizPEST() {
    if ($('#descripcion').val() === '' && $('#idelementomatriz').val() === '') {

        swal({
            type: 'warning',
            title: 'Estimado Usuario',
            text: 'Por favor ingrese el tipo y la descripción del elemento PEST.',
            timer: 4000,
            confirmButtonColor: '#7E8A97'
        }).then(
                        function () { },
                        function (dismiss) {
                            if (dismiss === 'timer') {
                                console.log('Cerrado')
                            }
                        }
                        )
    } else
        if ($('#descripcion').val() === '') {
            swal({
                type: 'warning',
                title: 'Estimado Usuario',
                text: 'Por favor ingrese la descripción del elemento PEST.',
                timer: 4000,
                confirmButtonColor: '#7E8A97'
            }).then(
                            function () { },
                            function (dismiss) {
                                if (dismiss === 'timer') {
                                    console.log('Cerrado')
                                }
                            }
                            )
        } else
            if ($('#idelementomatriz').val() === '') {

                swal({
                    type: 'warning',
                    title: 'Estimado Usuario',
                    text: 'Por favor ingrese el tipo de elemento PEST.',
                    timer: 4000,
                    confirmButtonColor: '#7E8A97'
                }).then(
                                function () { },
                                function (dismiss) {
                                    if (dismiss === 'timer') {
                                        console.log('Cerrado')
                                    }
                                }
                                )
            }
            else {


                var ElementoMatriz = new Object();
                ElementoMatriz.PK_Elemento_Matriz = $idInputPKElementoMatriz.val();
                ElementoMatriz.Descripcion_Elemento = $idTextAreaDescripcion.val();
                ElementoMatriz.FK_TipoElementoAnalisis = $('#idelementomatriz').val();
                ElementoMatriz.FK_Matriz = $idInputFKMatriz.val();

                AgregarElementoMatriz(ElementoMatriz);
            }
}
