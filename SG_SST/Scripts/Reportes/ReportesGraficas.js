var rangoColores = ["#A9CCE3", "#D4AC0D", "#58D68D", "#F1948A", "#73C6B6", "#B03A2E", "#A569BD", "#138D75", "#D4AC0D", "#85C1E9", "#A93226", "#616A6B", "#34495E", "#2874A6"];
var urlBase = utils.getBaseUrl();
var urlReportes = urlBase + '/Reportes';
var anchoSVG = 960;
var altoSVG = 500;
$(document).ready(function () {
    $("#frmreportes").validate({
        rules: {
            //IdReporte: {
            //    required: true, min: 1
            //},
            anio: {
                required: true, min: 1
            },
        },
        messages: {
            //IdReporte: {
            //    required: "Debe seleccionar un reporte",
            //    min: "Debe seleccionar un reporte"
            //},
            anio: {
                required: "Este campo es requerido",
                min: "Este campo es requerido"
            }
        }
    });
    var reporte = "";
    $('#regresar').on("click", function () {
        $('#filtros').show();
        $('#chks').show();
    });
    $('#DContingencia').on("click", function () {
        if ($(this).is(":checked")) {
            reporte = $(this).val();
            document.getElementById("filtros").style.display = "";
            document.getElementById("chks").style.display = "none";
        }
    });
    $('#NContingencia').on("click", function () {
        if ($(this).is(":checked")) {
            reporte = $(this).val();
            document.getElementById("filtros").style.display = "";
            document.getElementById("chks").style.display = "none";
        }
    });
    $('#ADeptos').on("click", function () {
        if ($(this).is(":checked")) {
            reporte = $(this).val();
            document.getElementById("filtros").style.display = "";
            document.getElementById("chks").style.display = "none";
        }
    });
    $('#AusCIE').on("click", function () {
        if ($(this).is(":checked")) {
            reporte = $(this).val();
            document.getElementById("filtros").style.display = "";
            document.getElementById("chks").style.display = "none";
        }
    });
    $('#NumCie').on("click", function () {
        if ($(this).is(":checked")) {
            reporte = $(this).val();
            document.getElementById("filtros").style.display = "";
            document.getElementById("chks").style.display = "none";
        }
    });
    $('#AusProc').on("click", function () {
        if ($(this).is(":checked")) {
            reporte = $(this).val();
            document.getElementById("filtros").style.display = "";
            document.getElementById("chks").style.display = "none";
        }
    });
    $('#AusSede').on("click", function () {
        if ($(this).is(":checked")) {
            reporte = $(this).val();
            document.getElementById("filtros").style.display = "";
            document.getElementById("chks").style.display = "none";
        }
    });
    $('#PromCont').on("click", function () {
        if ($(this).is(":checked")) {
            reporte = $(this).val();
            document.getElementById("filtros").style.display = "";
            document.getElementById("chks").style.display = "none";
        }
    });
    $('#AusEps').on("click", function () {
        if ($(this).is(":checked")) {
            reporte = $(this).val();
            document.getElementById("filtros").style.display = "";
            document.getElementById("chks").style.display = "none";
        }
    });
    $('#AusSx').on("click", function () {
        if ($(this).is(":checked")) {
            reporte = $(this).val();
            document.getElementById("filtros").style.display = "";
            document.getElementById("chks").style.display = "none";
        }
    });
    $('#AusTpVinc').on("click", function () {
        if ($(this).is(":checked")) {
            reporte = $(this).val();
            document.getElementById("filtros").style.display = "";
            document.getElementById("chks").style.display = "none";
        }
    });
    $('#AusOcup').on("click", function () {
        if ($(this).is(":checked")) {
            reporte = $(this).val();
            document.getElementById("filtros").style.display = "";
            document.getElementById("chks").style.display = "none";
        }
    });
    $('#AusGruEt').on("click", function () {
        if ($(this).is(":checked")) {
            reporte = $(this).val();
            document.getElementById("filtros").style.display = "";
            document.getElementById("chks").style.display = "none";
        }
    });
    $('#IdReporte').on('change', function () {
        reporte = $(this).val();
    });


    $("#idGrafica").hide;
    $("#consultar").click(function (evt) {
        if (!$("#frmreportes").valid()) {
            return false;
        }

        var dataReportes =
        {
            anio: $("#anio").val(),
            idOrigen: $("#tipoOrigen").val(),
            IdEmpresaUsuaria: $("#IdEmpresaUsuaria").val(),
            idSede: $("#Sede").val(),
            IdDepartamento: $("#Departamento").val(),
            IdReporte: $("#IdReporte").val()
        };

        var estado = $("#IdEmpresaUsuaria").attr("disabled")

        if (estado == "disabled") {
            if ( $("#Departamento").val() == '') {
                swal("Atención", 'Debe seleccionar un reporte.');
                return false;
            }
        }


        if ($('svg').length > 0)
            $('svg').remove();

        switch (reporte) {
            case 'AC':
                TablaContingencia(dataReportes);

                //Grafica_Contingencia(dataReportes);
                mostrarTipoReporte();
                //$("#TipoReporteASPX").val("AC")
                //$("#IdReporteGrafico").val("AC")
                $("#idGrafica").show("toogle");
                $.ajax({
                    type: 'POST',
                    url: urlBase + '/ReportesAplicacion/NumEventosContingencia',
                    success: function (result) {
                        swal("Atención", 'si devolvio');
                        $('#idGrafica').innerHTML = result;
                    }
                });
                //$("#idGrafica").load('@Url.Action("NumEventosContingencia","ReportesAplicacion")');
                break;
            case 'NC':
                TablaEvento(dataReportes);
                //Grafica_Eventos(dataReportes);
                mostrarTipoReporte();
                break;
            case 'ADP':
                TablaDepartamento(dataReportes);
                //Grafica_Departamento(dataReportes);
                mostrarTipoReporte();
                break;
            case 'DCIE':
                TablaEnfermedades(dataReportes);
                //Grafica_Enfermedades(dataReportes);
                mostrarTipoReporte();
                break;
            case 'NCIE':
                TablaEnfermedadesEvt(dataReportes);
                //Grafica_EventosPorEnfermedades(dataReportes);
                mostrarTipoReporte();
                break;
            case 'DP':
                TablaAusenciasPorProcesos(dataReportes);
                //Grafica_AusenciasPorProcesos(dataReportes);
                mostrarTipoReporte();
                break;
            case 'DS':
                TablaSede(dataReportes);
                //Grafica_Sede(dataReportes);
                mostrarTipoReporte();
                break;
            case 'PC':
                TablaCosto(dataReportes);
                //Grafica_Costo(dataReportes);
                mostrarTipoReporte();
                break;
            case 'AEPS':
                TablaEps(dataReportes);
                //Grafica_Eps(dataReportes);
                mostrarTipoReporte();
                break;
            case 'ASX':
                TablaSexo(dataReportes);
                //Grafica_Sexo(dataReportes);
                mostrarTipoReporte();
                break;
            case 'AV':
                TablaTipoVinculacion(dataReportes);
                //Grafica_TipoVinculacion(dataReportes);
                mostrarTipoReporte();
                break;
            case 'AO':
                TablaOcupacion(dataReportes);
                //Grafica_Ocupacion(dataReportes);
                mostrarTipoReporte();
                break;
            case 'AET':
                TablaGruposEtarios(dataReportes);
                //Grafica_GruposEtarios(dataReportes);
                mostrarTipoReporte();
                break;
            default:
                swal('Atención', "Seleccione el Reporte que desea visualizar.");
        }
    });

    function mostrarTipoReporte() {
        $("#tiporeporte").show();
        $('#chks').find('input[type="checkbox"]:checked');
        var reporteSelect = $('#chks').find('input[type="checkbox"]:checked').parent().text();
        reporteSelect = reporteSelect.trim();
        $('#IdReporte').find('option').each(function () {
            if ($(this).text() == reporteSelect)
                $(this).attr('selected', true);
        });
        $("#IdReporte").show();
    }
    function TablaContingencia(dataReportes) {
        $.ajax({
            type: "post",
            url: urlReportes + '/ReportePorContingencia',
            data: dataReportes
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#Tablas').empty();
                $('#Tablas').html(response.Data);
            } else if (response != undefined && response != '' && response.Mensaje == 'Fail') {
                swal("Atención", 'No se encotró información para el criterio de búsqueda.');
                $('#Tablas').empty();
                if ($('svg').length > 0)
                    $('svg').remove();
            }
            else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"               
            }            
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
        });
    }
    function TablaEvento(dataReportes) {
        $.ajax({
            type: "post",
            url: urlReportes + '/ReportePorEvento',
            data: dataReportes
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#Tablas').empty();
                $('#Tablas').html(response.Data);
            } else if (response != undefined && response != '' && response.Mensaje == 'Fail') {
                swal("Atención", 'No se encotró información para el criterio de búsqueda.');
                $('#Tablas').empty();
                if ($('svg').length > 0)
                    $('svg').remove();
            }
            else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
        });
    }
    function TablaDepartamento(dataReportes) {
        $.ajax({
            type: "post",
            url: urlReportes + '/ReportePorDepartamento',
            data: dataReportes
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#Tablas').empty();
                $('#Tablas').html(response.Data);
            } else if (response != undefined && response != '' && response.Mensaje == 'Fail') {
                swal("Atención", 'No se encotró información para el criterio de búsqueda.');
                $('#Tablas').empty();
                if ($('svg').length > 0)
                    $('svg').remove();
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
        });
    }
    function TablaEnfermedades(dataReportes) {
        $.ajax({
            type: "post",
            url: urlReportes + '/ReportePorEnfermedades',
            data: dataReportes
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#Tablas').empty();
                $('#Tablas').html(response.Data);
            } else if (response != undefined && response != '' && response.Mensaje == 'Fail') {
                swal("Atención", 'No se encotró información para el criterio de búsqueda.');
                $('#Tablas').empty();
                if ($('svg').length > 0)
                    $('svg').remove();
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
        });
    }
    function TablaEnfermedadesEvt(dataReportes) {
        $.ajax({
            type: "post",
            url: urlReportes + '/ReporteNumEventosPorEnfermedad',
            data: dataReportes
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#Tablas').empty();
                $('#Tablas').html(response.Data);
            } else if (response != undefined && response != '' && response.Mensaje == 'Fail') {
                swal("Atención", 'No se encotró información para el criterio de búsqueda.');
                $('#Tablas').empty();
                if ($('svg').length > 0)
                    $('svg').remove();
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
        });
    }
    function TablaAusenciasPorProcesos(dataReportes) {
        $.ajax({
            type: "post",
            url: urlReportes + '/ReporteDiasAusentismoProceso',
            data: dataReportes
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#Tablas').empty();
                $('#Tablas').html(response.Data);
            } else if (response != undefined && response != '' && response.Mensaje == 'Fail') {
                swal("Atención", 'No se encotró información para el criterio de búsqueda.');
                $('#Tablas').empty();
                if ($('svg').length > 0)
                    $('svg').remove();
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
        });
    }
    function TablaSede(dataReportes) {
        $.ajax({
            type: "post",
            url: urlReportes + '/ReportePorSede',
            data: dataReportes
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#Tablas').empty();
                $('#Tablas').html(response.Data);
            } else if (response != undefined && response != '' && response.Mensaje == 'Fail') {
                swal("Atención", 'No se encotró información para el criterio de búsqueda.');
                $('#Tablas').empty();
                if ($('svg').length > 0)
                    $('svg').remove();
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
        });
    }
    function TablaCosto(dataReportes) {
        $.ajax({
            type: "post",
            url: urlReportes + '/ReportePorCosto',
            data: dataReportes
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#Tablas').empty();
                $('#Tablas').html(response.Data);
            } else if (response != undefined && response != '' && response.Mensaje == 'Fail') {
                swal("Atención", 'No se encotró información para el criterio de búsqueda.');
                $('#Tablas').empty();
                if ($('svg').length > 0)
                    $('svg').remove();
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
        });
    }
    function TablaEps(dataReportes) {
        $.ajax({
            type: "post",
            url: urlReportes + '/ReportePorEps',
            data: dataReportes
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#Tablas').empty();
                $('#Tablas').html(response.Data);
            } else if (response != undefined && response != '' && response.Mensaje == 'Fail') {
                swal("Atención", 'No se encotró información para el criterio de búsqueda.');
                $('#Tablas').empty();
                if ($('svg').length > 0)
                    $('svg').remove();
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
        });
    }
    function TablaSexo(dataReportes) {
        $.ajax({
            type: "post",
            url: urlReportes + '/ReportePorSexo',
            data: dataReportes
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#Tablas').empty();
                $('#Tablas').html(response.Data);
            } else if (response != undefined && response != '' && response.Mensaje == 'Fail') {
                swal("Atención", 'No se encotró información para el criterio de búsqueda.');
                $('#Tablas').empty();
                if ($('svg').length > 0)
                    $('svg').remove();
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
        });
    }
    function TablaTipoVinculacion(dataReportes) {
        $.ajax({
            type: "post",
            url: urlReportes + '/ReporteAusenciasPorVinculacion',
            data: dataReportes
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#Tablas').empty();
                $('#Tablas').html(response.Data);
            } else if (response != undefined && response != '' && response.Mensaje == 'Fail') {
                swal("Atención", 'No se encotró información para el criterio de búsqueda.');
                $('#Tablas').empty();
                if ($('svg').length > 0)
                    $('svg').remove();
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
        });
    }
    function TablaOcupacion(dataReportes) {
        $.ajax({
            type: "post",
            url: urlReportes + '/ReporteAusentismoOcupacion',
            data: dataReportes
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#Tablas').empty();
                $('#Tablas').html(response.Data);
            } else if (response != undefined && response != '' && response.Mensaje == 'Fail') {
                swal("Atención", 'No se encotró información para el criterio de búsqueda.');
                $('#Tablas').empty();
                if ($('svg').length > 0)
                    $('svg').remove();
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
        });
    }
    function TablaGruposEtarios(dataReportes) {
        $.ajax({
            type: "post",
            url: urlReportes + '/ReporteAusentismoPorGrupoEtarios',
            data: dataReportes
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                $('#Tablas').empty();
                $('#Tablas').html(response.Data);
            } else if (response != undefined && response != '' && response.Mensaje == 'Fail') {
                swal("Atención", 'No se encotró información para el criterio de búsqueda.');
                $('#Tablas').empty();
                if ($('svg').length > 0)
                    $('svg').remove();
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
        });
    }
    //Inicio Graficas
    function Grafica_Contingencia(dataReportes) {
        $.ajax({
            type: "POST",
            url: urlReportes + '/GraficaDiasContingencia',
            data: dataReportes,
            dataType: "json"
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                if ($('svg').length > 1)
                    $('svg').remove();
                var data = response.Data;
                var margin = { top: 20, right: 20, bottom: 30, left: 40 },
                    width = 960 - margin.left - margin.right,
                    height = 400 - margin.top - margin.bottom, padding = 100;

                //se configuran los colores
                var color = d3.scaleOrdinal().range(rangoColores);
                // set the ranges
                var x = d3.scaleBand()
                            .rangeRound([0, width])
                            //.range([padding, width - padding * 2])
                          .paddingInner(0.05);//.padding(0.1);
                var y = d3.scaleLinear()
                          .range([height, 0]);

                var xAxis = d3.axisBottom(x)
                                .scale(x);

                var svg = d3.select("#Temporal").append("svg")
                    .attr("width", width + margin.left + margin.right)
                    .attr("height", altoSVG)
                  .append("g")
                    .attr("transform",
                          "translate(" + margin.left + "," + margin.top + ")");

                // Scale the range of the data in the domains
                x.domain(data.map(function (d) { return d.CONTINGENCIA; }));
                y.domain([0, d3.max(data, function (d) { return d.Total; })]);
                // append the rectangles for the bar chart
                svg.selectAll(".bar")
                    .data(data)
                  .enter().append("rect")
                    .attr("class", "bar")
                    .attr("x", function (d) { return x(d.CONTINGENCIA); })
                    .attr("width", x.bandwidth() - 10)
                    .attr("y", function (d) { return y(d.Total); })
                        .transition().duration(3000)
                        .delay(function (d, i) { return i * 200; })
                    .attr("height", function (d) { return height - y(d.Total); })
                    .style("fill", function (d) { return color(d.CONTINGENCIA) });

                svg.selectAll("text")
                    .data(data)
                    .enter().append("text")
                        .text(function (d) { return d.Total + " (días)" })
                        .attr("transform", "translate(0," + height + ")")
                        .attr("x", function (d, i) { return i * (x.bandwidth() + 1) + 40 })
                        .attr("y", function (d) { return y(d.Total) - height - 10; })
                        .style("font-style", "italic")
                        .style("font-weight", "600")
                        .style("font-family", "Georgia")

                //grupo para manejar el eje x. Los nombres de las contingencias
                svg.append("g")
                    .attr("transform", "translate(0," + height + ")")
                    .attr("class", "x axis")
                    .call(xAxis)
                    .selectAll("text")
                        .style("text-anchor", "end")
                        .attr("dx", "-.8em")
                        .attr("dy", ".55em")
                        .attr("transform", "rotate(318)")
                        .style("font-size", "10px")
                        .style("font-style", "italic")
                        .style("font-weight", "600")
                        .style("font-family", "Georgia")
                        .style("fill", "black");
                //grupo para mostrar el eje y
                svg.append("g")
                    .call(d3.axisLeft(y));
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        })
    }
    function Grafica_Eventos(dataReportes) {
        $.ajax({
            type: "POST",
            url: urlReportes + '/GraficaEventos',
            data: dataReportes,
            dataType: "json"
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                if ($('svg').length > 1)
                    $('svg').remove();
                var data = response.Data;
                var margin = { top: 20, right: 20, bottom: 30, left: 40 },
                    width = 960 - margin.left - margin.right,
                    height = 400 - margin.top - margin.bottom, padding = 100;

                //se configuran los colores
                var color = d3.scaleOrdinal().range(rangoColores);
                // set the ranges
                var x = d3.scaleBand()
                          .rangeRound([0, width])
                          .paddingInner(0.05);//.padding(0.1);
                var y = d3.scaleLinear()
                          .range([height, 0]);

                var svg = d3.select("#Temporal").append("svg")
                    .attr("width", width + margin.left + margin.right)
                    .attr("height", altoSVG)
                  .append("g")
                    .attr("transform",
                          "translate(" + margin.left + "," + margin.top + ")");

                // Scale the range of the data in the domains
                x.domain(data.map(function (d) { return d.Evento; }));
                y.domain([0, d3.max(data, function (d) { return d.Total; })]);
                // append the rectangles for the bar chart
                svg.selectAll(".bar")
                    .data(data)
                  .enter().append("rect")
                    .attr("class", "bar")
                    .attr("x", function (d) { return x(d.Evento); })
                    .attr("width", x.bandwidth())
                    .attr("y", function (d) { return y(d.Total); })
                    .transition().duration(3000)
                    .delay(function (d, i) { return i * 200; })
                    .attr("height", function (d) { return height - y(d.Total); })
                    .style("fill", function (d) { return color(d.Evento) });

                //svg.selectAll("text")
                //    .data(data)
                //    .enter().append("text")
                //        .text(function (d) { return d.Total })
                //        .attr("transform", "translate(0," + height + ")")
                //        .attr("x", function (d, i) { return i * (x.bandwidth() + 1) })
                //        .attr("y", function (d) { console.log(d); return y(d.Total) - height - 10; })
                //        .style("font-style", "italic")
                //        .style("font-weight", "600")
                //        .style("font-family", "Georgia")

                // add the x Axis
                svg.append("g")
                    .attr("transform", "translate(0," + height + ")")
                    .call(d3.axisBottom(x))
                .selectAll("text")
                        .style("text-anchor", "end")
                        .attr("dx", "-.8em")
                        .attr("dy", ".55em")
                        .attr("transform", "rotate(318)")
                        .style("font-size", "10px")
                        .style("font-style", "italic")
                        .style("font-weight", "600")
                        .style("font-family", "Georgia")
                        .style("fill", "black");
                //.attr("dx", "-.8em")
                //.attr("dy", ".25em")
                //.style("text-anchor", "end")
                //.attr("font-size", "5px");
                // add the y Axis
                svg.append("g")
                    .call(d3.axisLeft(y));

                // format the data
                //data.forEach(function (d) {
                //    d.Total = +d.Total;
                //    d.Evento = d.Evento;
                //});
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        })
    }
    function Grafica_Departamento(dataReportes) {
        $.ajax({
            type: "POST",
            url: urlReportes + '/GraficaDepartamento',
            data: dataReportes,
            dataType: "json"
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                if ($('svg').length > 1)
                    $('svg').remove();
                var data = response.Data;
                var margin = { top: 20, right: 20, bottom: 30, left: 40 },
                    width = 960 - margin.left - margin.right,
                    height = 400 - margin.top - margin.bottom, padding = 100;
                //se configuran los colores
                var color = d3.scaleOrdinal().range(rangoColores);
                // set the ranges
                var x = d3.scaleBand()
                          .range([0, width])
                          .padding(0.1);
                var y = d3.scaleLinear()
                          .range([height, 0]);

                var svg = d3.select("#Temporal").append("svg")
                    .attr("width", width + margin.left + margin.right)
                    .attr("height", altoSVG)
                  .append("g")
                    .attr("transform",
                          "translate(" + margin.left + "," + margin.top + ")");
                // Scale the range of the data in the domains
                x.domain(data.map(function (d) { return d.Departamento; }));
                y.domain([0, d3.max(data, function (d) { return d.Total; })]);
                // append the rectangles for the bar chart
                svg.selectAll(".bar")
                    .data(data)
                  .enter().append("rect")
                    .attr("class", "bar")
                    .attr("x", function (d) { return x(d.Departamento); })
                    .attr("width", x.bandwidth())
                    .attr("y", function (d) { return y(d.Total); })
                        .transition().duration(3000)
                        .delay(function (d, i) { return i * 200; })
                    .attr("height", function (d) { return height - y(d.Total); })
                    .style("fill", function (d, i) { return color(d.Departamento) });

                // add the x Axis
                svg.append("g")
                    .attr("transform", "translate(0," + height + ")")
                    .call(d3.axisBottom(x))
                    .selectAll("text")
                    .style("text-anchor", "end")
                    .attr("dx", "-.8em")
                    .attr("dy", ".55em")
                    .attr("transform", "rotate(318)")
                    .style("font-size", "10px")
                    .style("font-style", "italic")
                    .style("font-weight", "600")
                    .style("font-family", "Georgia")
                    .style("fill", "black");
                // add the y Axis
                svg.append("g")
                    .call(d3.axisLeft(y));
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        })
    }
    function Grafica_Enfermedades(dataReportes) {
        $.ajax({
            type: "POST",
            url: urlReportes + '/GraficaEnfermedades',
            data: dataReportes,
            dataType: "json"
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                if ($('svg').length > 1)
                    $('svg').remove();
                var data = response.Data;
                var margin = { top: 20, right: 20, bottom: 30, left: 40 },
                    width = 960 - margin.left - margin.right,
                    height = 400 - margin.top - margin.bottom, padding = 100;
                //se configuran los colores
                var color = d3.scaleOrdinal().range(rangoColores);
                // set the ranges
                var x = d3.scaleBand()
                          .range([0, width])
                          .padding(0.1);
                var y = d3.scaleLinear()
                          .range([height, 0]);

                var svg = d3.select("#Temporal").append("svg")
                    .attr("width", width + margin.left + margin.right)
                    .attr("height", altoSVG)
                  .append("g")
                    .attr("transform",
                          "translate(" + margin.left + "," + margin.top + ")");
                // format the data
                // Scale the range of the data in the domains
                x.domain(data.map(function (d) { return d.Enfermedades; }));
                y.domain([0, d3.max(data, function (d) { return d.Total; })]);
                // append the rectangles for the bar chart
                svg.selectAll(".bar")
                    .data(data)
                  .enter().append("rect")
                    .attr("class", "bar")
                    .attr("x", function (d) { return x(d.Enfermedades); })
                    .attr("width", x.bandwidth())
                    .attr("y", function (d) { return y(d.Total); })
                        .transition().duration(3000)
                        .delay(function (d, i) { return i * 200; })
                    .attr("height", function (d) { return height - y(d.Total); })
                    .style("fill", function (d, i) { return color(d.Enfermedades) });

                // add the x Axis
                svg.append("g")
                    .attr("transform", "translate(0," + height + ")")
                    .call(d3.axisBottom(x))
                    .selectAll("text")
                    .style("text-anchor", "end")
                    .attr("dx", "-.8em")
                    .attr("dy", ".55em")
                    .attr("transform", "rotate(318)")
                    .style("font-size", "10px")
                    .style("font-style", "italic")
                    .style("font-weight", "600")
                    .style("font-family", "Georgia")
                    .style("fill", "black");
                // add the y Axis
                svg.append("g")
                    .call(d3.axisLeft(y));
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        })
    }
    function Grafica_EventosPorEnfermedades(dataReportes) {
        $.ajax({
            type: "POST",
            url: urlReportes + '/GraficaNumEventosPorEnfermedad',
            data: dataReportes,
            dataType: "json"
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                if ($('svg').length > 1)
                    $('svg').remove();
                var data = response.Data;
                var margin = { top: 20, right: 20, bottom: 30, left: 40 },
                    width = 960 - margin.left - margin.right,
                    height = 400 - margin.top - margin.bottom, padding = 100;

                //se configuran los colores
                var color = d3.scaleOrdinal().range(rangoColores);
                // set the ranges
                var x = d3.scaleBand()
                          .rangeRound([0, width])
                          .paddingInner(0.05);//.padding(0.1);
                var y = d3.scaleLinear()
                          .range([height, 0]);

                var svg = d3.select("#Temporal").append("svg")
                    .attr("width", width + margin.left + margin.right)
                    .attr("height", altoSVG)
                  .append("g")
                    .attr("transform",
                          "translate(" + margin.left + "," + margin.top + ")");

                // Scale the range of the data in the domains
                x.domain(data.map(function (d) { return d.Evento; }));
                y.domain([0, d3.max(data, function (d) { return d.Total; })]);
                // append the rectangles for the bar chart
                svg.selectAll(".bar")
                    .data(data)
                  .enter().append("rect")
                    .attr("class", "bar")
                    .attr("x", function (d) { return x(d.Evento); })
                    .attr("width", x.bandwidth())
                    .attr("y", function (d) { return y(d.Total); })
                    .transition().duration(3000)
                    .delay(function (d, i) { return i * 200; })
                    .attr("height", function (d) { return height - y(d.Total); })
                    .style("fill", function (d) { return color(d.Evento) });

                //svg.selectAll("text")
                //    .data(data)
                //    .enter().append("text")
                //        .text(function (d) { return d.Total })
                //        .attr("transform", "translate(0," + height + ")")
                //        .attr("x", function (d, i) { return i * (x.bandwidth() + 1) })
                //        .attr("y", function (d) { console.log(d); return y(d.Total) - height - 10; })
                //        .style("font-style", "italic")
                //        .style("font-weight", "600")
                //        .style("font-family", "Georgia")

                // add the x Axis
                svg.append("g")
                    .attr("transform", "translate(0," + height + ")")
                    .call(d3.axisBottom(x))
                .selectAll("text")
                        .style("text-anchor", "end")
                        .attr("dx", "-.8em")
                        .attr("dy", ".55em")
                        .attr("transform", "rotate(318)")
                        .style("font-size", "10px")
                        .style("font-style", "italic")
                        .style("font-weight", "600")
                        .style("font-family", "Georgia")
                        .style("fill", "black");
                //.attr("dx", "-.8em")
                //.attr("dy", ".25em")
                //.style("text-anchor", "end")
                //.attr("font-size", "5px");
                // add the y Axis
                svg.append("g")
                    .call(d3.axisLeft(y));

                // format the data
                //data.forEach(function (d) {
                //    d.Total = +d.Total;
                //    d.Evento = d.Evento;
                //});
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        })
    }
    function Grafica_AusenciasPorProcesos(dataReportes) {
        $.ajax({
            type: "POST",
            url: urlReportes + '/GraficaDiasAusentismoProceso',
            data: dataReportes,
            dataType: "json"
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                if ($('svg').length > 1)
                    $('svg').remove();
                var data = response.Data;
                var margin = { top: 20, right: 20, bottom: 30, left: 40 },
                    width = 960 - margin.left - margin.right,
                    height = 400 - margin.top - margin.bottom, padding = 100;

                //se configuran los colores
                var color = d3.scaleOrdinal().range(rangoColores);
                // set the ranges
                var x = d3.scaleBand()
                          .rangeRound([0, width])
                          .paddingInner(0.05);//.padding(0.1);
                var y = d3.scaleLinear()
                          .range([height, 0]);

                var svg = d3.select("#Temporal").append("svg")
                    .attr("width", width + margin.left + margin.right)
                    .attr("height", altoSVG)
                  .append("g")
                    .attr("transform",
                          "translate(" + margin.left + "," + margin.top + ")");

                // Scale the range of the data in the domains
                x.domain(data.map(function (d) { return d.Evento; }));
                y.domain([0, d3.max(data, function (d) { return d.Total; })]);
                // append the rectangles for the bar chart
                svg.selectAll(".bar")
                    .data(data)
                  .enter().append("rect")
                    .attr("class", "bar")
                    .attr("x", function (d) { return x(d.Evento); })
                    .attr("width", x.bandwidth())
                    .attr("y", function (d) { return y(d.Total); })
                    .transition().duration(3000)
                    .delay(function (d, i) { return i * 200; })
                    .attr("height", function (d) { return height - y(d.Total); })
                    .style("fill", function (d) { return color(d.Evento) });

                //svg.selectAll("text")
                //    .data(data)
                //    .enter().append("text")
                //        .text(function (d) { return d.Total })
                //        .attr("transform", "translate(0," + height + ")")
                //        .attr("x", function (d, i) { return i * (x.bandwidth() + 1) })
                //        .attr("y", function (d) { console.log(d); return y(d.Total) - height - 10; })
                //        .style("font-style", "italic")
                //        .style("font-weight", "600")
                //        .style("font-family", "Georgia")

                // add the x Axis
                svg.append("g")
                    .attr("transform", "translate(0," + height + ")")
                    .call(d3.axisBottom(x))
                .selectAll("text")
                        .style("text-anchor", "end")
                        .attr("dx", "-.8em")
                        .attr("dy", ".55em")
                        .attr("transform", "rotate(318)")
                        .style("font-size", "10px")
                        .style("font-style", "italic")
                        .style("font-weight", "600")
                        .style("font-family", "Georgia")
                        .style("fill", "black");
                //.attr("dx", "-.8em")
                //.attr("dy", ".25em")
                //.style("text-anchor", "end")
                //.attr("font-size", "5px");
                // add the y Axis
                svg.append("g")
                    .call(d3.axisLeft(y));

                // format the data
                //data.forEach(function (d) {
                //    d.Total = +d.Total;
                //    d.Evento = d.Evento;
                //});
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        })
    }
    //Error Engrafica Revisar
    function Grafica_Sede(dataReportes) {
        $.ajax({
            type: "POST",
            url: urlReportes + '/GraficaSede',
            data: dataReportes,
            dataType: "json"
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                if ($('svg').length > 1)
                    $('svg').remove();
                var data = response.Data;
                var margin = { top: 20, right: 20, bottom: 30, left: 40 },
                    width = 960 - margin.left - margin.right,
                    height = 400 - margin.top - margin.bottom, padding = 100;
                //se configuran los colores
                var color = d3.scaleOrdinal().range(rangoColores);
                // set the ranges
                var x = d3.scaleBand()
                          .range([0, width])
                          .padding(0.1);
                var y = d3.scaleLinear()
                          .range([height, 0]);

                var svg = d3.select("#Temporal").append("svg")
                    .attr("width", width + margin.left + margin.right)
                    .attr("height", altoSVG)
                  .append("g")
                    .attr("transform",
                          "translate(" + margin.left + "," + margin.top + ")");
                // Scale the range of the data in the domains
                x.domain(data.map(function (d) { return d.Sede; }));
                y.domain([0, d3.max(data, function (d) { return d.Total; })]);
                // append the rectangles for the bar chart
                svg.selectAll(".bar")
                    .data(data)
                  .enter().append("rect")
                    .attr("class", "bar")
                    .attr("x", function (d) { return x(d.Sede); })
                    .attr("width", x.bandwidth())
                    .attr("y", function (d) { return y(d.Total); })
                        .transition().duration(3000)
                        .delay(function (d, i) { return i * 200; })
                    .attr("height", function (d) { return height - y(d.Total); })
                    .style("fill", function (d, i) { return color(d.Sede) });

                // add the x Axis
                svg.append("g")
                    .attr("transform", "translate(0," + height + ")")
                    .call(d3.axisBottom(x))
                    .selectAll("text")
                    .style("text-anchor", "end")
                    .attr("dx", "-.8em")
                    .attr("dy", ".55em")
                    .attr("transform", "rotate(318)")
                    .style("font-size", "10px")
                    .style("font-style", "italic")
                    .style("font-weight", "600")
                    .style("font-family", "Georgia")
                    .style("fill", "black");
                // add the y Axis
                svg.append("g")
                    .call(d3.axisLeft(y));
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        })
    }
    //Costo 
    function Grafica_Costo(dataReportes) {
        $.ajax({
            type: "POST",
            url: urlReportes + '/GraficaCosto',
            data: dataReportes,
            dataType: "json"
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                if ($('svg').length > 1)
                    $('svg').remove();
                var data = response.Data;
                var margin = { top: 20, right: 20, bottom: 30, left: 40 },
                    width = 960 - margin.left - margin.right,
                    height = 400 - margin.top - margin.bottom, padding = 100;
                //se configuran los colores
                var color = d3.scaleOrdinal().range(rangoColores);
                // set the ranges
                var x = d3.scaleBand()
                          .range([0, width])
                          .padding(0.1);
                var y = d3.scaleLinear()
                          .range([height, 0]);

                var svg = d3.select("#Temporal").append("svg")
                    .attr("width", width + margin.left + margin.right)
                    .attr("height", altoSVG)
                  .append("g")
                    .attr("transform",
                          "translate(" + margin.left + "," + margin.top + ")");
                // Scale the range of the data in the domains
                x.domain(data.map(function (d) { return d.Descripcion; }));
                y.domain([0, d3.max(data, function (d) { return d.Total; })]);
                // append the rectangles for the bar chart
                svg.selectAll(".bar")
                    .data(data)
                  .enter().append("rect")
                    .attr("class", "bar")
                    .attr("x", function (d) { return x(d.Descripcion); })
                    .attr("width", x.bandwidth())
                    .attr("y", function (d) { return y(d.Total); })
                        .transition().duration(3000)
                        .delay(function (d, i) { return i * 200; })
                    .attr("height", function (d) { return height - y(d.Total); })
                    .style("fill", function (d, i) { return color(d.Descripcion) });

                // add the x Axis
                svg.append("g")
                    .attr("transform", "translate(0," + height + ")")
                    .call(d3.axisBottom(x))
                    .selectAll("text")
                    .style("text-anchor", "end")
                    .attr("dx", "-.8em")
                    .attr("dy", ".55em")
                    .attr("transform", "rotate(318)")
                    .style("font-size", "10px")
                    .style("font-style", "italic")
                    .style("font-weight", "600")
                    .style("font-family", "Georgia")
                    .style("fill", "black");
                // add the y Axis
                svg.append("g")
                    .call(d3.axisLeft(y));
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        })
    }
    //Eps 
    function Grafica_Eps(dataReportes) {
        $.ajax({
            type: "POST",
            url: urlReportes + '/GraficaEps',
            data: dataReportes,
            dataType: "json"
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                if ($('svg').length > 1)
                    $('svg').remove();
                var data = response.Data;
                var margin = { top: 20, right: 20, bottom: 30, left: 40 },
                    width = 960 - margin.left - margin.right,
                    height = 400 - margin.top - margin.bottom, padding = 100;
                //se configuran los colores
                var color = d3.scaleOrdinal().range(rangoColores);
                // set the ranges
                var x = d3.scaleBand()
                          .range([0, width])
                          .padding(0.1);
                var y = d3.scaleLinear()
                          .range([height, 0]);

                var svg = d3.select("#Temporal").append("svg")
                    .attr("width", width + margin.left + margin.right)
                    .attr("height", altoSVG)
                  .append("g")
                    .attr("transform",
                          "translate(" + margin.left + "," + margin.top + ")");
                // Scale the range of the data in the domains
                x.domain(data.map(function (d) { return d.Eps; }));
                y.domain([0, d3.max(data, function (d) { return d.Total; })]);
                // append the rectangles for the bar chart
                svg.selectAll(".bar")
                    .data(data)
                  .enter().append("rect")
                    .attr("class", "bar")
                    .attr("x", function (d) { return x(d.Eps); })
                    .attr("width", x.bandwidth())
                    .attr("y", function (d) { return y(d.Total); })
                        .transition().duration(3000)
                        .delay(function (d, i) { return i * 200; })
                    .attr("height", function (d) { return height - y(d.Total); })
                    .style("fill", function (d, i) { return color(d.Eps) });

                // add the x Axis
                svg.append("g")
                    .attr("transform", "translate(0," + height + ")")
                    .call(d3.axisBottom(x))
                    .selectAll("text")
                    .style("text-anchor", "end")
                    .attr("dx", "-.8em")
                    .attr("dy", ".55em")
                    .attr("transform", "rotate(318)")
                    .style("font-size", "10px")
                    .style("font-style", "italic")
                    .style("font-weight", "600")
                    .style("font-family", "Georgia")
                    .style("fill", "black");
                // add the y Axis
                svg.append("g")
                    .call(d3.axisLeft(y));
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        })
    }
    //Sexo 
    function Grafica_Sexo(dataReportes) {
        $.ajax({
            type: "POST",
            url: urlReportes + '/GraficaSexo',
            data: dataReportes,
            dataType: "json"
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                if ($('svg').length > 1)
                    $('svg').remove();
                var data = response.Data;
                var margin = { top: 20, right: 20, bottom: 30, left: 40 },
                    width = 960 - margin.left - margin.right,
                    height = 400 - margin.top - margin.bottom, padding = 100;
                //se configuran los colores
                var color = d3.scaleOrdinal().range(rangoColores);
                // set the ranges
                var x = d3.scaleBand()
                          .range([0, width])
                          .padding(0.1);
                var y = d3.scaleLinear()
                          .range([height, 0]);

                var svg = d3.select("#Temporal").append("svg")
                    .attr("width", width + margin.left + margin.right)
                    .attr("height", altoSVG)
                  .append("g")
                    .attr("transform",
                          "translate(" + margin.left + "," + margin.top + ")");
                // Scale the range of the data in the domains
                x.domain(data.map(function (d) { return d.Sexo; }));
                y.domain([0, d3.max(data, function (d) { return d.Total; })]);
                // append the rectangles for the bar chart
                svg.selectAll(".bar")
                    .data(data)
                  .enter().append("rect")
                    .attr("class", "bar")
                    .attr("x", function (d) { return x(d.Sexo); })
                    .attr("width", x.bandwidth())
                    .attr("y", function (d) { return y(d.Total); })
                        .transition().duration(3000)
                        .delay(function (d, i) { return i * 200; })
                    .attr("height", function (d) { return height - y(d.Total); })
                    .style("fill", function (d, i) { return color(d.Sexo) });

                // add the x Axis
                svg.append("g")
                    .attr("transform", "translate(0," + height + ")")
                    .call(d3.axisBottom(x))
                    .selectAll("text")
                    .style("text-anchor", "end")
                    .attr("dx", "-.8em")
                    .attr("dy", ".55em")
                    .attr("transform", "rotate(318)")
                    .style("font-size", "10px")
                    .style("font-style", "italic")
                    .style("font-weight", "600")
                    .style("font-family", "Georgia")
                    .style("fill", "black");
                // add the y Axis
                svg.append("g")
                    .call(d3.axisLeft(y));
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        })
    }
    function Grafica_TipoVinculacion(dataReportes) {
        $.ajax({
            type: "POST",
            url: urlReportes + '/GraficaAusenciasPorVinculacion',
            data: dataReportes,
            dataType: "json"
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                if ($('svg').length > 1)
                    $('svg').remove();
                var data = response.Data;
                var margin = { top: 20, right: 20, bottom: 30, left: 40 },
                    width = 960 - margin.left - margin.right,
                    height = 400 - margin.top - margin.bottom, padding = 100;
                //se configuran los colores
                var color = d3.scaleOrdinal().range(rangoColores);
                // set the ranges
                var x = d3.scaleBand()
                          .range([0, width])
                          .padding(0.1);
                var y = d3.scaleLinear()
                          .range([height, 0]);

                var svg = d3.select("#Temporal").append("svg")
                    .attr("width", width + margin.left + margin.right)
                    .attr("height", altoSVG)
                  .append("g")
                    .attr("transform",
                          "translate(" + margin.left + "," + margin.top + ")");
                // Scale the range of the data in the domains
                x.domain(data.map(function (d) { return d.Ocupacion; }));
                y.domain([0, d3.max(data, function (d) { return d.Total; })]);
                // append the rectangles for the bar chart
                svg.selectAll(".bar")
                    .data(data)
                  .enter().append("rect")
                    .attr("class", "bar")
                    .attr("x", function (d) { return x(d.Ocupacion); })
                    .attr("width", x.bandwidth())
                    .attr("y", function (d) { return y(d.Total); })
                        .transition().duration(3000)
                        .delay(function (d, i) { return i * 200; })
                    .attr("height", function (d) { return height - y(d.Total); })
                    .style("fill", function (d, i) { return color(d.Ocupacion) });

                // add the x Axis
                svg.append("g")
                    .attr("transform", "translate(0," + height + ")")
                    .call(d3.axisBottom(x))
                    .selectAll("text")
                    .style("text-anchor", "end")
                    .attr("dx", "-.8em")
                    .attr("dy", ".55em")
                    .attr("transform", "rotate(318)")
                    .style("font-size", "10px")
                    .style("font-style", "italic")
                    .style("font-weight", "600")
                    .style("font-family", "Georgia")
                    .style("fill", "black");
                // add the y Axis
                svg.append("g")
                    .call(d3.axisLeft(y));
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        })
    }
    //Ocupacion
    function Grafica_Ocupacion(dataReportes) {
        $.ajax({
            type: "POST",
            url: urlReportes + '/GraficaAusentismoOcupacion',
            data: dataReportes,
            dataType: "json"
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                if ($('svg').length > 1)
                    $('svg').remove();
                var data = response.Data;
                var margin = { top: 20, right: 20, bottom: 30, left: 40 },
                    width = 960 - margin.left - margin.right,
                    height = 400 - margin.top - margin.bottom, padding = 100;
                //se configuran los colores
                var color = d3.scaleOrdinal().range(rangoColores);
                // set the ranges
                var x = d3.scaleBand()
                          .range([0, width])
                          .padding(0.1);
                var y = d3.scaleLinear()
                          .range([height, 0]);

                var svg = d3.select("#Temporal").append("svg")
                    .attr("width", width + margin.left + margin.right)
                    .attr("height", altoSVG)
                  .append("g")
                    .attr("transform",
                          "translate(" + margin.left + "," + margin.top + ")");
                // Scale the range of the data in the domains
                x.domain(data.map(function (d) { return d.Ocupacion; }));
                y.domain([0, d3.max(data, function (d) { return d.Total; })]);
                // append the rectangles for the bar chart
                svg.selectAll(".bar")
                    .data(data)
                  .enter().append("rect")
                    .attr("class", "bar")
                    .attr("x", function (d) { return x(d.Ocupacion); })
                    .attr("width", x.bandwidth())
                    .attr("y", function (d) { return y(d.Total); })
                        .transition().duration(3000)
                        .delay(function (d, i) { return i * 200; })
                    .attr("height", function (d) { return height - y(d.Total); })
                    .style("fill", function (d, i) { return color(d.Ocupacion) });

                // add the x Axis
                svg.append("g")
                    .attr("transform", "translate(0," + height + ")")
                    .call(d3.axisBottom(x))
                    .selectAll("text")
                    .style("text-anchor", "end")
                    .attr("dx", "-.8em")
                    .attr("dy", ".55em")
                    .attr("transform", "rotate(318)")
                    .style("font-size", "10px")
                    .style("font-style", "italic")
                    .style("font-weight", "600")
                    .style("font-family", "Georgia")
                    .style("fill", "black");
                // add the y Axis
                svg.append("g")
                    .call(d3.axisLeft(y));
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        })
    }


    $("#descargar").click(function (evt) {
        if (!$("#frmreportes").valid()) {
            return false;
        }

        var dataReportes =
        {
            anio: $("#anio").val(),
            idOrigen: $("#tipoOrigen").val(),
            IdEmpresaUsuaria: $("#IdEmpresaUsuaria").val(),
            idSede: $("#Sede").val(),
            IdDepartamento: $("#Departamento").val(),
            IdReporte: $("#IdReporte").val(),
            Reporte: reporte
        };

        var estado = $("#IdEmpresaUsuaria").attr("disabled")

        if (estado == "disabled") {
            if ($("#anio").val() == '' && $("#tipoOrigen").val() == '' && $("#Sede").val() == ''
            && $("#Departamento").val() == '') {
                swal("Atención", 'Debe seleccionar un filtro para realizar la consulta.');
                return false;
            }
        }
           

        DesacargarReporteExcel(dataReportes);

    });

    function DesacargarReporteExcel(dataReportes) {
        $.ajax({
            type: "post",
            url: urlReportes + '/ObtieneRporteExcelDescargar',
            data: dataReportes
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                window.location.href = urlReportes + response.Data;
            }

        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
        });
    }
    function Grafica_GruposEtarios(dataReportes) {
        $.ajax({
            type: "POST",
            url: urlReportes + '/GraficaAusentismoPorGrupoEtarios',
            data: dataReportes,
            dataType: "json"
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                if ($('svg').length > 1)
                    $('svg').remove();
                var data = response.Data;
                var margin = { top: 20, right: 20, bottom: 30, left: 40 },
                    width = 960 - margin.left - margin.right,
                    height = 400 - margin.top - margin.bottom, padding = 100;
                //se configuran los colores
                var color = d3.scaleOrdinal().range(rangoColores);
                // set the ranges
                var x = d3.scaleBand()
                          .range([0, width])
                          .padding(0.1);
                var y = d3.scaleLinear()
                          .range([height, 0]);

                var svg = d3.select("#Temporal").append("svg")
                    .attr("width", width + margin.left + margin.right)
                    .attr("height", altoSVG)
                  .append("g")
                    .attr("transform",
                          "translate(" + margin.left + "," + margin.top + ")");
                // Scale the range of the data in the domains
                x.domain(data.map(function (d) { return d.Evento; }));
                y.domain([0, d3.max(data, function (d) { return d.Total; })]);
                // append the rectangles for the bar chart
                svg.selectAll(".bar")
                    .data(data)
                  .enter().append("rect")
                    .attr("class", "bar")
                    .attr("x", function (d) { return x(d.Evento); })
                    .attr("width", x.bandwidth())
                    .attr("y", function (d) { return y(d.Total); })
                        .transition().duration(3000)
                        .delay(function (d, i) { return i * 200; })
                    .attr("height", function (d) { return height - y(d.Total); })
                    .style("fill", function (d, i) { return color(d.Evento) });

                // add the x Axis
                svg.append("g")
                    .attr("transform", "translate(0," + height + ")")
                    .call(d3.axisBottom(x))
                    .selectAll("text")
                    .style("text-anchor", "end")
                    .attr("dx", "-.8em")
                    .attr("dy", ".55em")
                    .attr("transform", "rotate(318)")
                    .style("font-size", "10px")
                    .style("font-style", "italic")
                    .style("font-weight", "600")
                    .style("font-family", "Georgia")
                    .style("fill", "black");
                // add the y Axis
                svg.append("g")
                    .call(d3.axisLeft(y));
            } else if (response != undefined && response != '' && response.Mensaje == 'FinSession') {
                swal("Atención", response.Data);
                window.location.href = "./Login/Home"
            }
        })
    }
});
