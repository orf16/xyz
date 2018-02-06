var urlBase = utils.getBaseUrl();

$(document).ready(function () {
    ConstruirDatePickerPorElemento('FechaNacimiento');
    ConstruirDatePickerPorElemento('FechaIngreso');
    ConstruirDatePickerPorElemento('FechaIncidente');
    ConstruirDatePickerPorElemento('FechaCreacionIncidente');
    
    $("#frmIncidente").validate({
        rules: {
            ActividadEconomica: {
                required: true
            },
            codactividad: {
                required: true
            },
            IdTipoDocumentoEmpresa: {
                required: true
            },
            RazonSocial: {
                required: true
            },
            NitEmpresa: {
                required: true
            },
            DireccionEmpresa: {
                required: true
            },
            TelefonoEmpresa: {
                required: true
            },
            CorreoElectronico: {
                required: true
            },
            IdDepartamentoEmp: {
                required: true, min: 1
            },
            IdMunicipioEmp: {
                required: true, min: 1
            },
            IdZonaEmpresa: {
                required: true
            },
            EsSedePrincipal: {
                required: true
            },
            IdSede: {
                required: true, min: 1
            },
            DireccionSede: {
                required: true
            },
            TelefonoSede: {
                required: true
            },
            IdDepartamentoSede: {
                required: true, min: 1
            },
            IdMunicipioSede: {
                required: true, min: 1
            },
            IdZonaSede: {
                required: true
            },
            IdVinculacionLab: {
                required: true
            },
            IdTipoDocumentoEmpleado: {
                required: true
            },
            DocumentoEmpleado: {
                required: true
            },
            PrimerApellido: {
                required: true
            },
            PrimerNombre: {
                required: true
            },
            FechaNacimiento: {
                required: true
            },
            Genero: {
                required: true
            },
            DireccionEmpleado: {
                required: true
            },
            TelefonoEmpleado: {
                required: true
            },
            IdDepartamentoEmpleado: {
                required: true, min: 1
            },
            IdMunicipioEmpleado: {
                required: true, min: 1
            },
            Ocupacion: {
                required: true
            },
            FechaIngreso: {
                required: true
            },
            FechaIncidente: {
                required: true
            },
            DiaSemanaIncidente: {
                required: true
            },
            EsJornadaNormal: {
                required: true
            },
            RealizabaLaborHabitual: {
                required: true
            },
            DescripcionLabor: {
                required: true
            },
            IdDepartamentoIncidente: {
                required: true, min: 1
            },
            IdMunicipioIncidente: {
                required: true, min: 1
            },
            EsDentroEmpresa: {
                required: true
            },
            IdSitioIncidente: {
                required: true, min: 1
            },
            OtroSitio: {
                required: true
            },
            IdConsecuencia: {
                required: true, min: 1
            },
            DescripcionIncidente: {
                required: true
            },
            FechaCreacionIncidente: {
                required: true
            }
        },
        messages: {
            ActividadEconomica: {
                required: "Este campo es obligatorio"
            },
            codactividad: {
                required: "Este campo es obligatorio"
            },
            RazonSocial: {
                required: "Este campo es obligatorio"
            },
            NitEmpresa: {
                required: "Este campo es obligatorio"
            },
            DireccionEmpresa: {
                required: "Este campo es obligatorio"
            },
            TelefonoEmpresa: {
                required: "Este campo es obligatorio"
            },
            CorreoElectronico: {
                required: "Este campo es obligatorio"
            },
            IdDepartamentoEmp: {
                required: "Este campo es obligatorio", min: "Debe seleccionar una opción"
            },
            IdMunicipioEmp: {
                required: "Este campo es obligatorio", min: "Debe seleccionar una opción"
            },            
            IdSede: {
                required: "Este campo es obligatorio", min: "Debe seleccionar una opción"
            },
            DireccionSede: {
                required: "Este campo es obligatorio"
            },
            TelefonoSede: {
                required: "Este campo es obligatorio"
            },
            IdDepartamentoSede: {
                required: "Este campo es obligatorio", min: "Debe seleccionar una opción"
            },
            IdMunicipioSede: {
                required: "Este campo es obligatorio", min: "Debe seleccionar una opción"
            },
            DocumentoEmpleado: {
                required: "Este campo es obligatorio"
            },
            PrimerApellido: {
                required: "Este campo es obligatorio"
            },
            PrimerNombre: {
                required: "Este campo es obligatorio"
            },
            FechaNacimiento: {
                required: "Este campo es obligatorio"
            },            
            DireccionEmpleado: {
                required: "Este campo es obligatorio"
            },
            TelefonoEmpleado: {
                required: "Este campo es obligatorio"
            },
            IdDepartamentoEmpleado: {
                required: "Este campo es obligatorio", min: "Debe seleccionar una opción"
            },
            IdMunicipioEmpleado: {
                required: "Este campo es obligatorio", min: "Debe seleccionar una opción"
            },
            Ocupacion: {
                required: "Este campo es obligatorio"
            },
            FechaIngreso: {
                required: "Este campo es obligatorio"
            },
            FechaIncidente: {
                required: "Este campo es obligatorio"
            },            
            DescripcionLabor: {
                required: "Este campo es obligatorio"
            },
            IdDepartamentoIncidente: {
                required: "Este campo es obligatorio", min: "Debe seleccionar una opción"
            },
            IdMunicipioIncidente: {
                required: "Este campo es obligatorio", min: "Debe seleccionar una opción"
            },
            IdSitioIncidente: {
                required: "Este campo es obligatorio", min: "Debe seleccionar una opción"
            },
            OtroSitio: {
                required: "Este campo es obligatorio"
            },
            IdConsecuencia: {
                required: "Este campo es obligatorio", min: "Debe seleccionar una opción"
            },
            DescripcionIncidente: {
                required: "Este campo es obligatorio"
            },
            FechaCreacionIncidente: {
                required: "Este campo es obligatorio"
            }
        }
    });

    //RETRICCIONES
    $(document).on("input", "#ActividadEconomica", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $('#codactividad').keypress(function (tecla) {
        if (tecla.charCode < 48 || tecla.charCode > 57) return false;
    });

    $(document).on("input", "#codactividad", function (tecla) {
        var limite = 7;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $(document).on("input", "#RazonSocial", function () {
        var limite = 150;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $(document).on("input", "#NitEmpresa", function () {
        var limite = 15;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $("#NitEmpresa").keypress(function (tecla) {
        if (tecla.charCode > 47 && tecla.charCode < 58) return true;
        else if (tecla.charCode > 64 && tecla.charCode < 91) return true;
        else if (tecla.charCode > 96 && tecla.charCode < 123) return true;
        else
            return false;
    });

    $(document).on("input", "#DireccionEmpresa", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $(document).on("input", "#TelefonoEmpresa", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $("#TelefonoEmpresa").keypress(function (tecla) {
        if (tecla.charCode < 48 || tecla.charCode > 57) return false;
    });

    $(document).on("input", "#CorreoElectronico", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $(document).on("input", "#DireccionSede", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $(document).on("input", "#TelefonoSede", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $("#TelefonoSede").keypress(function (tecla) {
        if (tecla.charCode < 48 || tecla.charCode > 57) return false;
    });

    $(document).on("input", "#TelefonoSede", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $("#DocumentoEmpleado").keypress(function (tecla) {
        if (tecla.charCode > 47 && tecla.charCode < 58) return true;
        else if (tecla.charCode > 64 && tecla.charCode < 91) return true;
        else if (tecla.charCode > 96 && tecla.charCode < 123) return true;
        else
            return false;
    });

    $(document).on("input", "#DocumentoEmpleado", function () {
        var limite = 15;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $(document).on("input", "#PrimerApellido", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });
    $(document).on("input", "#SegundoApellido", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });
    $(document).on("input", "#PrimerNombre", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });
    $(document).on("input", "#SegundNombre", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $(document).on("input", "#DireccionEmpleado", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $(document).on("input", "#TelefonoEmpleado", function () {
        var limite = 15;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $("#TelefonoEmpleado").keypress(function (tecla) {
        if (tecla.charCode < 48 || tecla.charCode > 57) return false;
    });

    $(document).on("input", "#Ocupacion", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $(document).on("input", "#DescripcionLabor", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $(document).on("input", "#OtroSitio", function () {
        var limite = 20;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $(document).on("input", "#DescripcionIncidente", function () {
        var limite = 2000;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $("#FechaIncidente").keypress(function (tecla) {
        if (tecla.charCode > 0) return false;
    });

    $("#HoraIncidente").keypress(function (tecla) {
        if (tecla.charCode < 48 || tecla.charCode > 57)
            if (tecla.charCode == 58)
                return true;
            else
                return false;
    });

    $(document).on("input", "#HoraIncidente", function () {
        var limite = 5;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $("#Incidente_tiempo_previo_al_incidente_HHMM").keypress(function (tecla) {
        if (tecla.charCode < 48 || tecla.charCode > 57)
            if (tecla.charCode == 58)
                return true;
            else
                return false;
    });

    $(document).on("input", "#Incidente_tiempo_previo_al_incidente_HHMM", function () {
        var limite = 5;
        var textreal = $(this).val();
        var text;

        if ($(this).val().length > limite) {
            text = textreal.substr(0, limite);
            $(this).val(text);
        }
    });

    $(".tip-top").tooltip({
        placement: 'right'        
    });
    //$('body').tooltip({ title: "Hooraynkjlkj!", selector: ".tpl" });
    //$('.tpl').tooltip({ title: "<h1><strong>HTML</strong> inside <code>the</code> <em>tooltip</em></h1>", html: true, placement: "bottom" });
    
    //Se seleccion el tipo de documento de la empresa
    var tipo = $("#TipoDocumentoEmpresa").val();
    if (tipo == 'CC')
        $('.TipoDocumentoEmpresa')[0].checked = true;
    if (tipo == 'NI')
        $('.TipoDocumentoEmpresa')[1].checked = true;
    if (tipo == 'CE')
        $('.TipoDocumentoEmpresa')[2].checked = true;
    if (tipo == 'NU')
        $('.TipoDocumentoEmpresa')[3].checked = true;
    if (tipo == 'PA')
        $('.TipoDocumentoEmpresa')[4].checked = true;

    //Se selecciona el departamento de la empresa
    var deptoEmp = $("#DepartamentoEmp").val();
    //if (deptoEmp.includes("BOGOTA"))
    //   deptoEmp = "BOGOTA";
    $('#IdDepartamentoEmp').find('option').each(function () {
        var deptoIterator = $.trim($(this).text());
        var deptoObtenido = $.trim(deptoEmp);
        if (deptoIterator == deptoObtenido) {
            $(this).prop('selected', true);
        }
    });

    //Se selecciona el minicipio de la empresa
    var depto = $("#IdDepartamentoEmp").val()
    var miniciEmp = $("#MunicipioEmp").val();

    $.ajax({
        type: "POST",
        data: { idDepto: depto },
        url: urlBase + '/Ausencias/ConsultarMunicipiosPorDepto'
    }).done(function (response) {
        if (response != undefined && response != '' && response.Mensaje == 'Success') {
            $("#IdMunicipioEmp").empty();
            $.each(response.Data, function (i, munici) {
                $("#IdMunicipioEmp").append('<option value="' + munici.Value + '">' +
                         munici.Text + '</option>');
            });
            $('#IdMunicipioEmp option:contains(' + miniciEmp + ')').prop('selected', true);
        }
    })

    //Se selecciona la Zona de la empresa
    var zona = $("#ZonaEmpresa").val();
    if (zona == 'U') {
        if ($('.ZonasEmpresa').length > 0)
            $('.ZonasEmpresa')[0].checked = true;
    }
    if (zona == 'R') {
        if ($('.ZonasEmpresa').length > 0)
            $('.ZonasEmpresa')[1].checked = true;
    }
    //Se selecciona el tipo de vinculacion laboral
    var vinculacion = $("#VinculacionLab").val();
    if (vinculacion == "Dependiente")
        vinculacion = "Planta";
    if (vinculacion == 'Planta') {
        if ($('.vinculacion').length > 0)
            $('.vinculacion')[0].checked = true;
    }
    if (vinculacion == 'Misión') {
        if ($('.vinculacion').length > 0)
            $('.vinculacion')[1].checked = true;
    }

    if (vinculacion == 'Cooperado') {
        if ($('.vinculacion').length > 0)
            $('.vinculacion')[2].checked = true;
    }
    if (vinculacion.includes('Estudiante')) {
        if ($('.vinculacion').length > 0)
            $('.vinculacion')[3].checked = true;
    }
    if (vinculacion == 'Independiente') {
        if ($('.vinculacion').length > 0)
            $('.vinculacion')[4].checked = true;
    }
    //Se seleccion el tipo de documento del empleado
    var tipo = $("#TipoDocumentoEmpleado").val();
    if (tipo == 'CC')
        $('.TipoDocumentopers')[0].checked = true;
    if (tipo == 'CE')
        $('.TipoDocumentopers')[1].checked = true;
    if (tipo == 'PA')
        $('.TipoDocumentopers')[2].checked = true;
    if (tipo == 'TI')
        $('.TipoDocumentopers')[3].checked = true;


    //Se selecciona el departamento del empleado
    var deptoEmpleado = $("#DepartamentoEmpleado").val();
    //if (deptoEmpleado.includes("BOGOTA"))
    //    deptoEmpleado = "BOGOTA";
    $('#IdDepartamentoEmpleado').find('option').each(function () {
        var deptoIterator = $.trim($(this).text());
        var deptoObtenido = $.trim(deptoEmpleado);
        if (deptoIterator == deptoObtenido) {
            $(this).prop('selected', true);
        }
    });

    //Se selecciona el minicipio de la empleado
    var deptoEmpleado = $("#IdDepartamentoEmpleado").val()
    var miniciEmpletado = $("#MunicipioEmpleado").val();

    $.ajax({
        type: "POST",
        data: { idDepto: deptoEmpleado },
        url: urlBase + '/Ausencias/ConsultarMunicipiosPorDepto'
    }).done(function (response) {
        if (response != undefined && response != '' && response.Mensaje == 'Success') {
            $("#IdMunicipioEmpleado").empty();
            $.each(response.Data, function (i, munici) {
                $("#IdMunicipioEmpleado").append('<option value="' + munici.Value + '">' +
                         munici.Text + '</option>');
            });
            $('#IdMunicipioEmpleado option:contains(' + miniciEmpletado + ')').prop('selected', true);
        }
    })

    //Si elige que el incidente no fue en la sede principal se obtienen las sedes
    $(".sedePrincipal").click(function () {
        PopupPosition();
        var Essedeprin = $(this).val();
        if (Essedeprin == "False") {

            $.ajax({
                type: "POST",
                url: urlBase + '/Incidente/ConsultarSedes'
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'OK') {
                    $("#IdSede").empty();
                    $("#IdSede").append('<option value="0"> ---Sedes--- </option>');
                    $.each(response.Datos, function (i, sede) {
                        $("#IdSede").append('<option value="' + sede.IdSede + '">' +
                                 sede.NombreSede + '</option>');
                        $("#IdSede").prop("disabled", false);
                    });
                }
            })
        }
        else {
            $("#IdSede").empty();
            $("#IdSede").append('<option value="0"> ---Sedes--- </option>');
            $("#DireccionSede").val('');
            $("#TelefonoSede").val('');
            $('#IdDepartamentoSede').find('option').each(function () {
                var deptoIterator = $.trim($(this).text());
                var deptoObtenido = $.trim('---Departamentos--');
                if (deptoIterator == deptoObtenido) {
                    $(this).prop('selected', true);
                }
            });

            $('#IdMunicipioSede option:contains(---Municipios---)').prop('selected', true);
            $('.ZonasSede')[0].checked = false;
            $('.ZonasSede')[0].checked = false;

            $("#IdSede").prop("disabled", true);
            $("#DireccionSede").prop("disabled", true);
            $("#TelefonoSede").prop("disabled", true);
            $("#IdDepartamentoSede").prop("disabled", true);
            $("#IdMunicipioSede").prop("disabled", true);
            $('.ZonasSede').prop("disabled", true);

        }

        OcultarPopupposition();
    });

    //Al seleccionar una sede se debe seleccionar la informacion de la sede     
    $('#IdSede').on('change', function () {
        var sede = $("#IdSede").val();
        if (sede != 0) {
            PopupPosition();
            $.ajax({
                type: "POST",
                data: { idSede: sede },
                url: urlBase + '/Incidente/ConsultarSedePorId'
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'OK') {

                    $("#DireccionSede").empty();
                    $("#TelefonoSede").empty();

                    //$("#DireccionSede").prop("disabled", false);
                    $("#TelefonoSede").prop("disabled", false);
                    //$("#IdDepartamentoSede").prop("disabled", false);
                    //$("#IdMunicipioSede").prop("disabled", false);
                    //$(".ZonasSede").prop("disabled", false);

                    $("#DireccionSede").val(response.Datos.DireccionSede);
                    $('#IdDepartamentoSede').find('option').each(function () {
                        var deptoIterator = $.trim($(this).text());
                        var deptoObtenido = $.trim(response.Datos.NombreDepto);
                        if (deptoIterator == deptoObtenido) {
                            $(this).prop('selected', true);
                        }
                    });

                    var deptoSede = $("#IdDepartamentoSede").val()
                    var NombreMunici = response.Datos.NombreMunici;

                    $.ajax({
                        type: "POST",
                        data: { idDepto: deptoSede },
                        url: urlBase + '/Ausencias/ConsultarMunicipiosPorDepto'
                    }).done(function (response) {
                        if (response != undefined && response != '' && response.Mensaje == 'Success') {
                            $("#IdMunicipioSede").empty();
                            $("#IdMunicipioSede").append('<option value="0">---Municipios---</option>');
                            $.each(response.Data, function (i, munici) {
                                $("#IdMunicipioSede").append('<option value="' + munici.Value + '">' +
                                         munici.Text + '</option>');
                            });
                            $('#IdMunicipioSede option:contains(' + NombreMunici + ')').prop('selected', true);
                        }
                    })
                    var p = $('.ZonasSede');
                    if (response.Datos.Sector == 'Urbano')
                        $('.ZonasSede')[0].checked = true;
                    if (response.Datos.Sector == 'Rural')
                        $('.ZonasSede')[1].checked = true;

                }
            })
            OcultarPopupposition();
        }
    });

    //Se obtienen los municipios segun el depto seleccionado
    $('#IdDepartamentoEmp').change(function () {
        var deptoEmpresa = $("#IdDepartamentoEmp").val()

        $.ajax({
            type: "POST",
            data: { idDepto: deptoEmpresa },
            url: urlBase + '/Ausencias/ConsultarMunicipiosPorDepto'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $("#IdMunicipioEmp").empty();
                $("#IdMunicipioEmp").append('<option value="0">---Municipios---</option>');
                $.each(response.Data, function (i, munici) {
                    $("#IdMunicipioEmp").append('<option value="' + munici.Value + '">' +
                             munici.Text + '</option>');
                });
            }
        })
    });

    $('#IdDepartamentoSede').change(function () {
        var deptosede = $("#IdDepartamentoSede").val()

        $.ajax({
            type: "POST",
            data: { idDepto: deptosede },
            url: urlBase + '/Ausencias/ConsultarMunicipiosPorDepto'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $("#IdMunicipioSede").empty();
                $("#IdMunicipioSede").append('<option value="0">---Municipios---</option>');
                $.each(response.Data, function (i, munici) {
                    $("#IdMunicipioSede").append('<option value="' + munici.Value + '">' +
                             munici.Text + '</option>');
                });
            }
        })
    });

    $('#IdDepartamentoEmpleado').change(function () {
        var deptoEmpleado = $("#IdDepartamentoEmpleado").val()

        $.ajax({
            type: "POST",
            data: { idDepto: deptoEmpleado },
            url: urlBase + '/Ausencias/ConsultarMunicipiosPorDepto'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $("#IdMunicipioEmpleado").empty();
                $("#IdMunicipioEmpleado").append('<option value="0">---Municipios---</option>');
                $.each(response.Data, function (i, munici) {
                    $("#IdMunicipioEmpleado").append('<option value="' + munici.Value + '">' +
                             munici.Text + '</option>');
                });
            }
        })
    });

    $('#IdDepartamentoIncidente').change(function () {
        var deptoIncidente = $("#IdDepartamentoIncidente").val()

        $.ajax({
            type: "POST",
            data: { idDepto: deptoIncidente },
            url: urlBase + '/Ausencias/ConsultarMunicipiosPorDepto'
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $("#IdMunicipioIncidente").empty();
                $("#IdMunicipioIncidente").append('<option value="0">---Municipios---</option>');
                $.each(response.Data, function (i, munici) {
                    $("#IdMunicipioIncidente").append('<option value="' + munici.Value + '">' +
                             munici.Text + '</option>');
                });
            }
        })
    });


    //Hbilita el campo de cual en caso de seleccionar no en Estaba realizando su labor habitual
    $(".laborHabitual").click(function () {
        var Essedeprin = $(this).val();
        if (Essedeprin == "False")
            $('#DescripcionLabor').prop("disabled", false);
        else {
            $('#DescripcionLabor').prop("disabled", true);
            $('#DescripcionLabor').val('');
        }
    });


    //Hbilita el combo de sitios en caso de seleccionar que el accidente no fue dentro de la empresa
    //$(".dondeOcurrAccidente").click(function () {
    //    var Essedeprin = $(this).val();
    //    if (Essedeprin == "False")
    //        $('#IdSitioIncidente').prop("disabled", false);
    //    else {
    //        $('#IdSitioIncidente').prop("disabled", true);
    //        $('#IdSitioIncidente option:contains(---Sitios--)').prop('selected', true);
    //        $('#OtroSitio').prop("disabled", true);
    //        $('#OtroSitio').val('');
    //    }
    //});


    //Hbilita el campo Otro si eligen la opcion otro del combo sitio
    $('#IdSitioIncidente').change(function () {
        var sitio = $('#IdSitioIncidente option:selected').text();
        if (sitio == 'Otro')
            $('#OtroSitio').prop("disabled", false);
        else {
            $('#OtroSitio').prop("disabled", true);
            $('#OtroSitio').val('');
        }
    });

    $('#FechaIncidente').change(function () {

        var f1 = $("#FechaIncidente").val();
        var fecha1 = new Date(f1.split('/')[2], f1.split('/')[1] - 1, f1.split('/')[0]);

        var dtActual = new Date();
        if (dtActual.getTime() < fecha1.getTime()) {
            swal("Estimado Usuario", 'La Fecha de incidente no puede ser mayor a la actual');
            $('#FechaIncidente').val('')
            return false;
        }
        else
            diaSemana();
    });


    $('.JornadaHabitual').change(function () {
        $('#errorIdJornadaHabitual').hide();
        $('#errorform').hide();
    });

    $('.cTipoIncidente').change(function () {
        $('#errorIdTipoIncidente').hide();
        $('#errorform').hide();
    });

    $('.ZonaIncidente').change(function () {
        $('#errorIdZonaIncidente').hide();
        $('#errorform').hide();
    });

    $('.ZonaEmpleado').change(function () {
        $('#errorIdZonaEmpleado').hide();
        $('#errorform').hide();
    });    

    $('#HoraIncidente').change(function () {
        $('#errorHoraIncidente').hide();
        $('#errorform').hide();
    });

    $('#Incidente_tiempo_previo_al_incidente_HHMM').change(function () {
        $('#errorIncidentetiempoprevio').hide();
        $('#errorform').hide();
    });

    $('#HoraIncidente').change(function () {
        $('#errorHoraIncidente').hide();
        $('#errorform').hide();
    });

    //Guradar el formulario
    $('#btnguardar').click(function () {
        var valido = true;

        var RealizabaLaborHabitual = $('input:radio[id=RealizabaLaborHabitual]:checked').val();
        var LaborHabitual = $('input:radio[class=LaborHabitual]:checked').val();

        var ZonaEmpleado = $('input:radio[id=IdZonaEmpleado]:checked').val();
        if (ZonaEmpleado == undefined) {
            valido = false;
            $('#errorIdZonaEmpleado').show();
        }
        else
            $('#errorIdZonaEmpleado').hide();
        var JornadaHabitual = $('input:radio[id=IdJornadaHabitual]:checked').val();
        if (JornadaHabitual == undefined) {
            valido = false;
            $('#errorIdJornadaHabitual').show();
        }
        else
            $('#errorIdJornadaHabitual').hide();
        var TipoIncidente = $('input:radio[id=IdTipoIncidente]:checked').val();
        if (TipoIncidente == undefined) {
            valido = false;
            $('#errorIdTipoIncidente').show();
        }
        else
            $('#errorIdTipoIncidente').hide();
        var ZonaIncidente = $('input:radio[id=IdZonaIncidente]:checked').val();
        if (ZonaIncidente == undefined) {
            valido = false;
            $('#errorIdZonaIncidente').show();
        }
        else
            $('#errorIdZonaIncidente').hide();

        var horaIncidente = $('#HoraIncidente').val();
        if (horaIncidente == '') {
            valido = false;
            $('#errorHoraIncidente').show();
        }
        else
            $('#errorHoraIncidente').hide();

        var tiempo_previo = $('#Incidente_tiempo_previo_al_incidente_HHMM').val();
        if (tiempo_previo == '') {
            valido = false;
            $('#errorIncidentetiempoprevio').show();
        }
        else
            $('#errorIncidentetiempoprevio').hide();

        if (!validarhora($('#HoraIncidente').val())) {
            valido = false;
            $('#errorHoraIncidente').show();
        }
        else
            $('#errorHoraIncidente').hide();

        if (!validarhora($('#Incidente_tiempo_previo_al_incidente_HHMM').val())) {
            valido = false;
            $('#errorIncidentetiempoprevio').show();
        }
        else
            $('#errorIncidentetiempoprevio').hide();

        if (!$("#frmIncidente").valid())
            valido = false;
        if (valido) {
            $('#errorform').hide();
            $('#errorIdZonaEmpleado').hide();
            $('#errorIdJornadaHabitual').hide();
            $('#errorIdTipoIncidente').hide();
            PopupPosition();
            var datosFormulario = {
                ActividadEconomica: $('#ActividadEconomica').val(),
                CodActividadEconomica: $('#CodActividadEconomica').val(),
                IdTipoDocumentoEmpresa: $('input:radio[id=IdTipoDocumentoEmpresa]:checked').val(),
                NitEmpresa: $('#NitEmpresa').val(),
                DireccionEmpresa: $('#DireccionEmpresa').val(),
                TelefonoEmpresa: $('#TelefonoEmpresa').val(),
                CorreoElectronico: $('#CorreoElectronico').val(),
                IdDepartamentoEmp: $('#IdDepartamentoEmp').val(),
                IdMunicipioEmp: $('#IdMunicipioEmp').val(),
                IdZonaEmpresa: $('input:radio[id=IdZonaEmpresa]:checked').val(),
                EsSedePrincipal: $('input:radio[id=EsSedePrincipal]:checked').val(),
                IdSede: $('#IdSede').val(),
                NombreSede: $('#IdSede option:selected').text(),
                DireccionSede: $('#DireccionSede').val(),
                TelefonoSede: $('#TelefonoSede').val(),
                IdDepartamentoSede: $('#IdDepartamentoSede').val(),
                IdMunicipioSede: $('#IdMunicipioSede').val(),
                MunicipioSede: $('#IdMunicipioSede option:selected').text(),
                IdZonaSede: $('input:radio[id=IdZonaSede]:checked').val(),
                IdVinculacionLab: $('input:radio[id=IdVinculacionLab]:checked').val(),
                IdTipoDocumentoEmpleado: $('input:radio[id=IdTipoDocumentoEmpleado]:checked').val(),
                DocumentoEmpleado: $('#DocumentoEmpleado').val(),
                PrimerApellido: $('#PrimerApellido').val(),
                SegundoApellido: $('#SegundoApellido').val(),
                PrimerNombre: $('#PrimerNombre').val(),
                SegundNombre: $('#SegundNombre').val(),
                FechaNacimiento: $('#FechaNacimiento').val(),
                Genero: $('input:radio[id=Genero]:checked').val(),
                DireccionEmpleado: $('#DireccionEmpleado').val(),
                TelefonoEmpleado: $('#TelefonoEmpleado').val(),
                IdDepartamentoEmpleado: $('#IdDepartamentoEmpleado').val(),
                IdMunicipioEmpleado: $('#IdMunicipioEmpleado').val(),
                MunicipioEmpleado: $('#IdMunicipioEmpleado option:selected').text(),
                IdZonaEmpleado: $('input:radio[id=IdZonaEmpleado]:checked').val(),
                Ocupacion: $('#Ocupacion').val(),
                IdOcupacion: $('#IdOcupacion').val(),
                FechaIngreso: $('#FechaIngreso').val(),
                IdJornadaHabitual: $('input:radio[id=IdJornadaHabitual]:checked').val(),
                FechaIncidente: $('#FechaIncidente').val(),
                HoraIncidente: $('#HoraIncidente').val(),
                DiaSemanaIncidente: $('input:radio[id=DiaSemanaIncidente]:checked').val(),
                EsJornadaNormal: $('input:radio[id=EsJornadaNormal]:checked').val(),
                RealizabaLaborHabitual: $('input:radio[id=RealizabaLaborHabitual]:checked').val(),
                DescripcionLabor: $('#DescripcionLabor').val(),
                Incidente_tiempo_previo_al_incidente_HHMM: $('#Incidente_tiempo_previo_al_incidente_HHMM').val(),
                IdTipoIncidente: $('input:radio[id=IdTipoIncidente]:checked').val(),
                IdDepartamentoIncidente: $('#IdDepartamentoIncidente').val(),
                IdMunicipioIncidente: $('#IdMunicipioIncidente').val(),
                MunicipioIncidente: $('#IdMunicipioIncidente option:selected').text(),
                IdZonaIncidente: $('input:radio[id=IdZonaIncidente]:checked').val(),
                EsDentroEmpresa: $('input:radio[id=EsDentroEmpresa]:checked').val(),
                IdSitioIncidente: $('#IdSitioIncidente').val(),
                SitioIncidente: $('#IdSitioIncidente option:selected').text(),
                OtroSitio: $('#OtroSitio').val(),
                IdConsecuencia: $('#IdConsecuencia').val(),
                Consecuencia: $('#IdConsecuencia option:selected').text(),
                DescripcionIncidente: $('#DescripcionIncidente').val(),
                MunicipioEmp: $('#IdMunicipioEmp option:selected').text(),
                FechaCreacionIncidente: $('#FechaCreacionIncidente').val()
            }


            $.ajax({
                type: "POST",
                data: { datosIncidente: datosFormulario },
                url: urlBase + '/Incidente/GuardarIncidente'
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'OK') {
                    swal('Estimado Usuario', response.Datos);
                    bloqueartodosloscampos();
                    OcultarPopupposition();
                }
                if (response != undefined && response != '' && response.Mensaje == 'ERROR') {
                    swal('Estimado Usuario', response.Datos);
                    OcultarPopupposition();
                }
            })
        } else {
            $('#errorform').show();            
            return valido;
        }
    })


    $('#btnDescargar').click(function () {
        var valido = true;
        var ZonaEmpleado = $('input:radio[id=IdZonaEmpleado]:checked').val();
        if (ZonaEmpleado == undefined) {
            valido = false;
            $('#errorIdZonaEmpleado').show();
        }
        else
            $('#errorIdZonaEmpleado').hide();
        var JornadaHabitual = $('input:radio[id=IdJornadaHabitual]:checked').val();
        if (JornadaHabitual == undefined) {
            valido = false;
            $('#errorIdJornadaHabitual').show();
        }
        else
            $('#errorIdJornadaHabitual').hide();
        var TipoIncidente = $('input:radio[id=IdTipoIncidente]:checked').val();
        if (TipoIncidente == undefined) {
            valido = false;
            $('#errorIdTipoIncidente').show();
        }
        else
            $('#errorIdTipoIncidente').hide();
        var ZonaIncidente = $('input:radio[id=IdZonaIncidente]:checked').val();
        if (ZonaIncidente == undefined) {
            valido = false;
            $('#errorIdZonaIncidente').show();
        }
        else
            $('#errorIdZonaIncidente').hide();

        var horaIncidente = $('#HoraIncidente').val();
        if (horaIncidente == '') {
            valido = false;
            $('#errorHoraIncidente').show();
        }
        else
            $('#errorHoraIncidente').hide();

        var tiempo_previo = $('#Incidente_tiempo_previo_al_incidente_HHMM').val();
        if (tiempo_previo == '') {
            valido = false;
            $('#errorIncidentetiempoprevio').show();
        }
        else
            $('#errorIncidentetiempoprevio').hide();

        if (!validarhora($('#HoraIncidente').val())) {
            valido = false;
            $('#errorHoraIncidente').show();
        }
        else
            $('#errorHoraIncidente').hide();

        if (!validarhora($('#Incidente_tiempo_previo_al_incidente_HHMM').val())) {
            valido = false;
            $('#errorIncidentetiempoprevio').show();
        }
        else
            $('#errorIncidentetiempoprevio').hide();


        if (!$("#frmIncidente").valid()) {
            valido = false;
        }


        if (valido) {
            $('#errorform').hide();
            var datosFormulario = {
                ActividadEconomica: $('#ActividadEconomica').val(),
                CodActividadEconomica: $('#CodActividadEconomica').val(),
                RazonSocial: $('#RazonSocial').val(),
                IdTipoDocumentoEmpresa: $('input:radio[id=IdTipoDocumentoEmpresa]:checked').val(),
                NitEmpresa: $('#NitEmpresa').val(),
                DireccionEmpresa: $('#DireccionEmpresa').val(),
                TelefonoEmpresa: $('#TelefonoEmpresa').val(),
                CorreoElectronico: $('#CorreoElectronico').val(),
                IdDepartamentoEmp: $('#IdDepartamentoEmp').val(),
                IdMunicipioEmp: $('#IdMunicipioEmp').val(),
                IdZonaEmpresa: $('input:radio[id=IdZonaEmpresa]:checked').val(),
                EsSedePrincipal: $('input:radio[id=EsSedePrincipal]:checked').val(),
                IdSede: $('#IdSede').val(),
                NombreSede: $('#IdSede option:selected').text(),
                DireccionSede: $('#DireccionSede').val(),
                TelefonoSede: $('#TelefonoSede').val(),
                IdDepartamentoSede: $('#IdDepartamentoSede').val(),
                IdMunicipioSede: $('#IdMunicipioSede').val(),
                IdZonaSede: $('input:radio[id=IdZonaSede]:checked').val(),
                IdVinculacionLab: $('input:radio[id=IdVinculacionLab]:checked').val(),
                IdTipoDocumentoEmpleado: $('input:radio[id=IdTipoDocumentoEmpleado]:checked').val(),
                DocumentoEmpleado: $('#DocumentoEmpleado').val(),
                PrimerApellido: $('#PrimerApellido').val(),
                SegundoApellido: $('#SegundoApellido').val(),
                PrimerNombre: $('#PrimerNombre').val(),
                SegundNombre: $('#SegundNombre').val(),
                FechaNacimiento: $('#FechaNacimiento').val(),
                Genero: $('input:radio[id=Genero]:checked').val(),
                DireccionEmpleado: $('#DireccionEmpleado').val(),
                TelefonoEmpleado: $('#TelefonoEmpleado').val(),
                IdDepartamentoEmpleado: $('#IdDepartamentoEmpleado').val(),
                IdMunicipioEmpleado: $('#IdMunicipioEmpleado').val(),
                IdZonaEmpleado: $('input:radio[id=IdZonaEmpleado]:checked').val(),
                Ocupacion: $('#Ocupacion').val(),
                IdOcupacion: $('#IdOcupacion').val(),
                FechaIngreso: $('#FechaIngreso').val(),
                IdJornadaHabitual: $('input:radio[id=IdJornadaHabitual]:checked').val(),
                FechaIncidente: $('#FechaIncidente').val(),
                HoraIncidente: $('#HoraIncidente').val(),
                DiaSemanaIncidente: $('input:radio[id=DiaSemanaIncidente]:checked').val(),
                EsJornadaNormal: $('input:radio[id=EsJornadaNormal]:checked').val(),
                RealizabaLaborHabitual: $('input:radio[id=RealizabaLaborHabitual]:checked').val(),
                DescripcionLabor: $('#DescripcionLabor').val(),
                Incidente_tiempo_previo_al_incidente_HHMM: $('#Incidente_tiempo_previo_al_incidente_HHMM').val(),
                IdTipoIncidente: $('input:radio[id=IdTipoIncidente]:checked').val(),
                IdDepartamentoIncidente: $('#IdDepartamentoIncidente').val(),
                IdMunicipioIncidente: $('#IdMunicipioIncidente').val(),
                IdZonaIncidente: $('input:radio[id=IdZonaIncidente]:checked').val(),
                EsDentroEmpresa: $('input:radio[id=EsDentroEmpresa]:checked').val(),
                IdSitioIncidente: $('#IdSitioIncidente').val(),
                SitioIncidente: $('#IdSitioIncidente option:selected').text(),
                OtroSitio: $('#OtroSitio').val(),
                IdConsecuencia: $('#IdConsecuencia').val(),
                Consecuencia: $('#IdConsecuencia option:selected').text(),
                DescripcionIncidente: $('#DescripcionIncidente').val(),
                MunicipioIncidente: $('#IdMunicipioIncidente option:selected').text(),
                MunicipioEmpleado: $('#IdMunicipioEmpleado option:selected').text(),
                MunicipioSede: $('#IdMunicipioSede option:selected').text(),
                MunicipioEmp: $('#IdMunicipioEmp option:selected').text(),
                FechaCreacionIncidente: $('#FechaCreacionIncidente').val()
            }

            PopupPosition();
            $.ajax({
                type: "POST",
                data: { datosIncidente: datosFormulario, imprimir: 1 },
                url: urlBase + '/Incidente/PrepararDescarga'
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'OK') {
                    window.location.href = '../Incidente/Descargar'
                }

                if (response != undefined && response != '' && response.Mensaje == 'ERROR') {
                    swal('Estimado Usuario', response.Datos);
                }
            })
            OcultarPopupposition();
        }
        else {
            $('#errorform').show();
            return valido;
        }
    });

    $('#Imprimir').click(function () {
        var valido = true;
        var ZonaEmpleado = $('input:radio[id=IdZonaEmpleado]:checked').val();
        if (ZonaEmpleado == undefined) {
            valido = false;
            $('#errorIdZonaEmpleado').show();
        }
        else
            $('#errorIdZonaEmpleado').hide();
        var JornadaHabitual = $('input:radio[id=IdJornadaHabitual]:checked').val();
        if (JornadaHabitual == undefined) {
            valido = false;
            $('#errorIdJornadaHabitual').show();
        }
        else
            $('#errorIdJornadaHabitual').hide();
        var TipoIncidente = $('input:radio[id=IdTipoIncidente]:checked').val();
        if (TipoIncidente == undefined) {
            valido = false;
            $('#errorIdTipoIncidente').show();
        }
        else
            $('#errorIdTipoIncidente').hide();
        var ZonaIncidente = $('input:radio[id=IdZonaIncidente]:checked').val();
        if (ZonaIncidente == undefined) {
            valido = false;
            $('#errorIdZonaIncidente').show();
        }
        else
            $('#errorIdZonaIncidente').hide();

        var horaIncidente = $('#HoraIncidente').val();
        if (horaIncidente == '') {
            valido = false;
            $('#errorHoraIncidente').show();
        }
        else
            $('#errorHoraIncidente').hide();

        var tiempo_previo = $('#Incidente_tiempo_previo_al_incidente_HHMM').val();
        if (tiempo_previo == '') {
            valido = false;
            $('#errorIncidentetiempoprevio').show();
        }
        else
            $('#errorIncidentetiempoprevio').hide();

        if (!validarhora($('#HoraIncidente').val())) {
            valido = false;
            $('#errorHoraIncidente').show();
        }
        else
            $('#errorHoraIncidente').hide();

        if (!validarhora($('#Incidente_tiempo_previo_al_incidente_HHMM').val())) {
            valido = false;
            $('#errorIncidentetiempoprevio').show();
        }
        else
            $('#errorIncidentetiempoprevio').hide();


        if (!$("#frmIncidente").valid()) {
            valido = false;
        }


        if (valido) {
            $('#errorform').hide();
            var datosFormulario = {
                ActividadEconomica: $('#ActividadEconomica').val(),
                CodActividadEconomica: $('#CodActividadEconomica').val(),
                RazonSocial: $('#RazonSocial').val(),
                IdTipoDocumentoEmpresa: $('input:radio[id=IdTipoDocumentoEmpresa]:checked').val(),
                NitEmpresa: $('#NitEmpresa').val(),
                DireccionEmpresa: $('#DireccionEmpresa').val(),
                TelefonoEmpresa: $('#TelefonoEmpresa').val(),
                CorreoElectronico: $('#CorreoElectronico').val(),
                IdDepartamentoEmp: $('#IdDepartamentoEmp').val(),
                IdMunicipioEmp: $('#IdMunicipioEmp').val(),
                IdZonaEmpresa: $('input:radio[id=IdZonaEmpresa]:checked').val(),
                EsSedePrincipal: $('input:radio[id=EsSedePrincipal]:checked').val(),
                IdSede: $('#IdSede').val(),
                NombreSede: $('#IdSede option:selected').text(),
                DireccionSede: $('#DireccionSede').val(),
                TelefonoSede: $('#TelefonoSede').val(),
                IdDepartamentoSede: $('#IdDepartamentoSede').val(),
                IdMunicipioSede: $('#IdMunicipioSede').val(),
                IdZonaSede: $('input:radio[id=IdZonaSede]:checked').val(),
                IdVinculacionLab: $('input:radio[id=IdVinculacionLab]:checked').val(),
                IdTipoDocumentoEmpleado: $('input:radio[id=IdTipoDocumentoEmpleado]:checked').val(),
                DocumentoEmpleado: $('#DocumentoEmpleado').val(),
                PrimerApellido: $('#PrimerApellido').val(),
                SegundoApellido: $('#SegundoApellido').val(),
                PrimerNombre: $('#PrimerNombre').val(),
                SegundNombre: $('#SegundNombre').val(),
                FechaNacimiento: $('#FechaNacimiento').val(),
                Genero: $('input:radio[id=Genero]:checked').val(),
                DireccionEmpleado: $('#DireccionEmpleado').val(),
                TelefonoEmpleado: $('#TelefonoEmpleado').val(),
                IdDepartamentoEmpleado: $('#IdDepartamentoEmpleado').val(),
                IdMunicipioEmpleado: $('#IdMunicipioEmpleado').val(),
                IdZonaEmpleado: $('input:radio[id=IdZonaEmpleado]:checked').val(),
                Ocupacion: $('#Ocupacion').val(),
                IdOcupacion: $('#IdOcupacion').val(),
                FechaIngreso: $('#FechaIngreso').val(),
                IdJornadaHabitual: $('input:radio[id=IdJornadaHabitual]:checked').val(),
                FechaIncidente: $('#FechaIncidente').val(),
                HoraIncidente: $('#HoraIncidente').val(),
                DiaSemanaIncidente: $('input:radio[id=DiaSemanaIncidente]:checked').val(),
                EsJornadaNormal: $('input:radio[id=EsJornadaNormal]:checked').val(),
                RealizabaLaborHabitual: $('input:radio[id=RealizabaLaborHabitual]:checked').val(),
                DescripcionLabor: $('#DescripcionLabor').val(),
                Incidente_tiempo_previo_al_incidente_HHMM: $('#Incidente_tiempo_previo_al_incidente_HHMM').val(),
                IdTipoIncidente: $('input:radio[id=IdTipoIncidente]:checked').val(),
                IdDepartamentoIncidente: $('#IdDepartamentoIncidente').val(),
                IdMunicipioIncidente: $('#IdMunicipioIncidente').val(),
                IdZonaIncidente: $('input:radio[id=IdZonaIncidente]:checked').val(),
                EsDentroEmpresa: $('input:radio[id=EsDentroEmpresa]:checked').val(),
                IdSitioIncidente: $('#IdSitioIncidente').val(),
                SitioIncidente: $('#IdSitioIncidente option:selected').text(),
                OtroSitio: $('#OtroSitio').val(),
                IdConsecuencia: $('#IdConsecuencia').val(),
                Consecuencia: $('#IdConsecuencia option:selected').text(),
                DescripcionIncidente: $('#DescripcionIncidente').val(),
                MunicipioIncidente: $('#IdMunicipioIncidente option:selected').text(),
                MunicipioEmpleado: $('#IdMunicipioEmpleado option:selected').text(),
                MunicipioSede: $('#IdMunicipioSede option:selected').text(),
                MunicipioEmp: $('#IdMunicipioEmp option:selected').text(),
                FechaCreacionIncidente: $('#FechaCreacionIncidente').val()
            }

            PopupPosition();
            $.ajax({
                type: "POST",
                data: { datosIncidente: datosFormulario, imprimir: 0 },
                url: urlBase + '/Incidente/PrepararDescarga'
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'OK') {
                    window.open('../Incidente/Imprimir');
                    //window.location.href = '../Incidente/Imprimir'
                }
                if (response != undefined && response != '' && response.Mensaje == 'ERROR') {
                    swal('Estimado Usuario', response.Datos);
                }
            })
            OcultarPopupposition();
        }
        else {
            $('#errorform').show();
            return valido;
        }

    });

    $('#Nuevoreporte').click(function () {
        window.location.href = urlBase + '/Incidente/Reportar'
    });

});

function validarhora(hora) {
    if (hora == '')
        return false;
    var pnum = hora[0];
    var snum = hora[1];
    var dospuntos = hora[2];
    var tnum = hora[3];

    if (pnum > 2)
        return false;
    else if (pnum == 2 && snum > 3)
        return false;
    else if (dospuntos != ':')
        return false;
    else if (tnum > 5)
        return false;
    else
        return true;
}

function bloqueartodosloscampos() {
    $('#ActividadEconomica').prop("disabled", true);
    $('#CodActividadEconomica').prop("disabled", true);
    $('#RazonSocial').prop("disabled", true);
    $('.TipoDocumentoEmpresa').prop("disabled", true);
    $('#NitEmpresa').prop("disabled", true);
    $('#DireccionEmpresa').prop("disabled", true);
    $('#TelefonoEmpresa').prop("disabled", true);
    $('#CorreoElectronico').prop("disabled", true);
    $('#IdDepartamentoEmp').prop("disabled", true);
    $('#IdMunicipioEmp').prop("disabled", true);
    $('.ZonasEmpresa').prop("disabled", true);
    $('.sedePrincipal').prop("disabled", true);
    $('#IdSede').prop("disabled", true);
    $('#DireccionSede').prop("disabled", true);
    $('#TelefonoSede').prop("disabled", true);
    $('#IdDepartamentoSede').prop("disabled", true);
    $('#IdMunicipioSede').prop("disabled", true);
    $('.ZonasSede').prop("disabled", true);
    $('.vinculacion').prop("disabled", true);
    $('.TipoDocumentopers').prop("disabled", true);
    $('#DocumentoEmpleado').prop("disabled", true);
    $('#PrimerApellido').prop("disabled", true);
    $('#SegundoApellido').prop("disabled", true);
    $('#PrimerNombre').prop("disabled", true);
    $('#SegundNombre').prop("disabled", true);
    $('#FechaNacimiento').prop("disabled", true);
    $('.cGenero').prop("disabled", true);
    $('#DireccionEmpleado').prop("disabled", true);
    $('#TelefonoEmpleado').prop("disabled", true);
    $('#IdDepartamentoEmpleado').prop("disabled", true);
    $('#IdMunicipioEmpleado').prop("disabled", true);
    $('.ZonaEmpleado').prop("disabled", true);
    $('#Ocupacion').prop("disabled", true);
    $('#FechaIngreso').prop("disabled", true);
    $('.JornadaHabitual').prop("disabled", true);
    $('#FechaIncidente').prop("disabled", true);
    $('#HoraIncidente').prop("disabled", true);
    $('#Incidente_tiempo_previo_al_incidente_HHMM').prop("disabled", true);
    $('.CDiaSemanaIncidente').prop("disabled", true);
    $('.JornadaNormal').prop("disabled", true);
    $('.laborHabitual').prop("disabled", true);
    $('#DescripcionLabor').prop("disabled", true);
    $('#Incidente_tiempo_previo_al_incidente_HHMM').val(),
    $('.cTipoIncidente').prop("disabled", true);
    $('#IdDepartamentoIncidente').prop("disabled", true);
    $('#IdMunicipioIncidente').prop("disabled", true);
    $('.ZonaIncidente').prop("disabled", true);
    $('.dondeOcurrAccidente').prop("disabled", true);
    $('#IdSitioIncidente').prop("disabled", true);
    $('#OtroSitio').prop("disabled", true);
    $('#IdConsecuencia').prop("disabled", true);
    $('#DescripcionIncidente').prop("disabled", true);
    $('#btnguardar').prop("disabled", true);
}



function diaSemana() {

    let dias = ["DO", "LU", "MA", "MI", "JU", "VI", "SA"];

    var f1 = document.getElementById("FechaIncidente").value;
    var fecha = new Date(f1.split('/')[2], f1.split('/')[1] - 1, f1.split('/')[0]);

    var dia = dias[fecha.getDay()];

    if (dia == 'DO')
        $('.CDiaSemanaIncidente')[0].checked = true;
    else if (dia == 'LU')
        $('.CDiaSemanaIncidente')[1].checked = true;
    else if (dia == 'MA')
        $('.CDiaSemanaIncidente')[2].checked = true;
    else if (dia == 'MI')
        $('.CDiaSemanaIncidente')[3].checked = true;
    else if (dia == 'JU')
        $('.CDiaSemanaIncidente')[4].checked = true;
    else if (dia == 'VI')
        $('.CDiaSemanaIncidente')[5].checked = true;
    else if (dia == 'SA')
        $('.CDiaSemanaIncidente')[6].checked = true;
}



