var urlBase = utils.getBaseUrl();
$(document).ready(function () {
    $('#descargarExcelComparativo').hide();
    var indi = $("#frmindicadores")
    indi.validate({
        rules: {
            AnioSeleccionado: {
                required: true
            },
            PrimerAnio: {
                required: true
            },
            SegundoAnio: {
                required: true
            },
            ConstanteSeleccionada: {
                required: true
            }           
        },
        messages: {
            AnioSeleccionado: {
                required: "Debe seleccionar un año de gestion"
            },
            PrimerAnio: {
                required: "Debe seleccionar un año de gestion"
            },
            SegundoAnio: {
                required: "Debe seleccionar un año de gestion"
            },
            ConstanteSeleccionada: {
                required: "Debe seleccionar un valor de constante"
            }           
        }
    });
    var constante = $("#ConstanteSeleccionada").val();
    var anioSel = $("#AnioSeleccionado").val();

    $('#4').change(function () {
        if ($(this).is(":checked")) {
            $("#descargarTablaExcel").hide();
            $('.campos-comparacion-indicadores').show();
            $('.campos-consultar-comparacion-indicadores').show();
            $('#consultarIndicador').hide();
            $('#AnioSeleccionado').hide();
            $('#agestion').hide();
            $('#descargarExcelComparativo').hide();
            $('#panelAcumulado').hide();
            $('#banneracumulado').hide();               
            $('#panelIndicador').hide();
            $('#bannerindicador').hide();            
        }
        else {
            $('#Indicadores').empty();
            $('#consultarIndicador').show();
            $('.campos-comparacion-indicadores').hide();
            $('.campos-consultar-comparacion-indicadores').hide();
            $('#AnioSeleccionado').show();
            $('#agestion').show();
            $('#descargarExcelComparativo').hide();
            $('#panelAcumulado').hide();
            $('#banneracumulado').hide();            
            $('#panelIndicador').hide();
            $('#bannerindicador').hide();            
        }
    });

    $('#volver').click(function () {        
        $("#descargarTablaExcelacumulado").hide();
        $("#descargarTablaExcel").show();
        $('#volver').hide();
        ConstanteSeleccionada: $("#ConstanteSeleccionada").val()
        
        if (indi.valid() != false) {
            var idContingencia = $('input:radio[id=tipoComparacion]:checked').val()
            if (idContingencia < 1 || idContingencia == undefined) {
                $('#errorcontigencia').show();
                return false;
            }
            
            var indicadores = {
                AnioSeleccionado: $("#AnioSeleccionado").val(),
                ConstanteSeleccionada: $("#ConstanteSeleccionada").val(),
                IdEmpresaUsuaria: $('#IdEmpresaUsuaria').val(),
                IdContingencia: $('input:radio[id=tipoComparacion]:checked').val()
            }
            PopupPosition();
            $.ajax({
                type: "post",
                url: urlBase + '/Indicadores/IndicadorIF',
                data: { indicadores: indicadores }
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'OK') {
                    $('#Indicadores').empty();
                    $('#Indicadores').html(response.Data);
                    $('.opciones-indicadores').show();
                    $('#panelIndicador').show();
                    $('#bannerindicador').show();
                } else if (response != undefined && response != '' && response.Mensaje == 'NOTFOUND')
                    swal('Atención', response.Data);
                else if (response != undefined && response != '' && response.Mensaje == 'INVALID')
                    swal('Atención', response.Data);
            }).fail(function (response) {
                console.log("Error en la peticion: " + response.Data);
            });

            PopupPosition();
            $.ajax({
                type: "post",
                url: urlBase + '/Indicadores/TotalAcumuladoContingencias',
                data: { indicadores: indicadores }
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'OK') {
                    $('#Acumulado').empty();
                    $('#Acumulado').html(response.Data);
                    $('#descargarTablaExcelacumulado').show();
                    $('#GraficaExcelacumulado').show();
                    $('#panelAcumulado').show();
                    $('#banneracumulado').show();
                } else if (response != undefined && response != '' && response.Mensaje == 'NOTFOUND') {
                    swal('Atención', 'No se encontró información asociada a los datos ingresados.');
                } else if (response != undefined && response != '' && response.Mensaje == 'INVALID')
                    swal('Atención', response.Data);
            }).fail(function (response) {
                console.log("Error en la peticion: " + response.Data);
            });
        }
        else {
            var idContingencia = $('input:radio[id=tipoComparacion]:checked').val()
            if (idContingencia < 1 || idContingencia == undefined)
                $('#errorcontigencia').show();            
        }
        OcultarPopupposition();
    });

    $("#consultarIndicador").click(function () {
        
        $("#descargarTablaExcelacumulado").hide();
        $("#descargarTablaExcel").show();        
        ConstanteSeleccionada: $("#ConstanteSeleccionada").val()
        
        if (indi.valid() != false) {
            var idContingencia = $('input:radio[id=tipoComparacion]:checked').val()
            if (idContingencia < 1 || idContingencia == undefined) {
                $('#errorcontigencia').show();
                return false;
            }
            
            var indicadores = {
                AnioSeleccionado: $("#AnioSeleccionado").val(),
                ConstanteSeleccionada: $("#ConstanteSeleccionada").val(),
                IdEmpresaUsuaria: $('#IdEmpresaUsuaria').val(),
                IdContingencia: $('input:radio[id=tipoComparacion]:checked').val()
            }
            PopupPosition();
            $.ajax({
                type: "post",
                url: urlBase + '/Indicadores/IndicadorIF',
                data: { indicadores: indicadores }
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'OK') {
                    $('#Indicadores').empty();
                    $('#Indicadores').html(response.Data);
                    $('.opciones-indicadores').show();
                    $('#panelIndicador').show();
                    $('#bannerindicador').show();
                } else if (response != undefined && response != '' && response.Mensaje == 'NOTFOUND')
                    swal('Atención', response.Data);
                else if (response != undefined && response != '' && response.Mensaje == 'INVALID')
                    swal('Atención', response.Data);
            }).fail(function (response) {
                console.log("Error en la peticion: " + response.Data);
            });

            PopupPosition();
            $.ajax({
                type: "post",
                url: urlBase + '/Indicadores/TotalAcumuladoContingencias',
                data: { indicadores: indicadores }
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'OK') {
                    $('#Acumulado').empty();
                    $('#Acumulado').html(response.Data);
                    $('#descargarTablaExcelacumulado').show();
                    $('#GraficaExcelacumulado').show();
                    $('#panelAcumulado').show();
                    $('#banneracumulado').show();
                } else if (response != undefined && response != '' && response.Mensaje == 'NOTFOUND') {
                    swal('Atención', 'No se encontró información asociada a los datos ingresados.');
                } else if (response != undefined && response != '' && response.Mensaje == 'INVALID')
                    swal('Atención', response.Data);
            }).fail(function (response) {
                console.log("Error en la peticion: " + response.Data);
            });
        }
        else {
            var idContingencia = $('input:radio[id=tipoComparacion]:checked').val()
            if (idContingencia < 1 || idContingencia == undefined)
                $('#errorcontigencia').show();            
        }
        OcultarPopupposition();
    });


    $(".ttipoComparacion").click(function () {
        $('#errorcontigencia').hide();
    });

    $("#descargarTablaExcel").click(function () {
        if (indi.valid() != false) {
            var indicadores = {
                AnioSeleccionado: $("#AnioSeleccionado").val(),
                ConstanteSeleccionada: $("#ConstanteSeleccionada").val(),
                IdEmpresaUsuaria: $('#IdEmpresaUsuaria').val(),
                IdContingencia: $('input:radio[id=tipoComparacion]:checked').val()
            }
            PopupPosition();
            $.ajax({
                type: "post",
                url: urlBase + '/Indicadores/GenerarExcelIndicadorIF',
                data: { indicadores: indicadores }
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'Succes') {
                    window.location.href = response.Data
                } else if (response != undefined && response != '' && response.Mensaje == 'Fail')
                    swal('Atención', response.Data);
                OcultarPopupposition();
            }).fail(function (response) {
                console.log("Error en la peticion: " + response.Data);
                OcultarPopupposition();
            });
        }
    });



    $("#GraficaExcelacumulado").click(function () {
        $("#descargarTablaExcel").hide();
        $('#volver').show();
        if (indi.valid() != false) {
            var indicadores = {
                AnioSeleccionado: $("#AnioSeleccionado").val(),
                ConstanteSeleccionada: $("#ConstanteSeleccionada").val(),
                IdEmpresaUsuaria: $('#IdEmpresaUsuaria').val(),
                IdContingencia: $('input:radio[id=tipoComparacion]:checked').val()
            }
            PopupPosition();
            $.ajax({
                type: "post",
                url: urlBase + '/Indicadores/ObtenerDatosGraficaAcumulado',
                data: { indicadores: indicadores }
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'OK') {
                    $('#Acumulado').empty();
                    $('#descargarTablaExcelacumulado').show();
                    $('#panelIndicador').hide();
                    $('#bannerindicador').hide();                    
                    $('#GraficaExcelacumulado').hide();
                    GenerarGrafica(response.Data, 1);
                } else if (response != undefined && response != '' && response.Mensaje == 'NOTFOUND') {
                    swal('Atención', 'No se encontró información asociada a los datos ingresados.');
                } else if (response != undefined && response != '' && response.Mensaje == 'INVALID')
                    swal('Atención', response.Data);
                OcultarPopupposition();
            }).fail(function (response) {
                console.log("Error en la peticion: " + response.Data);
                OcultarPopupposition();
            });
        }
    });

    $("#descargarTablaExcelacumulado").click(function () {
        if (indi.valid() != false) {
            var indicadores = {
                AnioSeleccionado: $("#AnioSeleccionado").val(),
                ConstanteSeleccionada: $("#ConstanteSeleccionada").val(),
                IdEmpresaUsuaria: $('#IdEmpresaUsuaria').val(),
                IdContingencia: $('input:radio[id=tipoComparacion]:checked').val()
            }
            PopupPosition();
            $.ajax({
                type: "post",
                url: urlBase + '/Indicadores/GenerarExcelAcumulado',
                data: { indicadores: indicadores }
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'Succes') {
                    window.location.href = response.Data
                } else if (response != undefined && response != '' && response.Mensaje == 'Fail')
                    swal('Atención', response.Data);
                OcultarPopupposition();
            }).fail(function (response) {
                console.log("Error en la peticion: " + response.Data);
                OcultarPopupposition();
            });
        }
    });

    $("#compararIndicadoresAnios").click(function () {
       
        if (indi.valid() != false) {

            var tipoContingencia = $('input:radio[id=tipoComparacion]:checked').val()
            if (tipoContingencia < 1 || tipoContingencia == undefined) {
                $('#errorcontigencia').show();
                return false;
            }            
            var Comparar = 0;
            //$('input[type="radio"]').each(function () {
            //    if ($(this).is(':checked')) {
            //        tipoContingencia = $(this).attr('id');
            //    }
            //});
            $('input[type="checkbox"]').each(function () {
                if ($(this).is(':checked')) {
                    Comparar = $(this).attr('id');
                }
            });

            //if (Comparar == 4 && tipoContingencia < 1) {
            //    swal('Atención', 'Debe seleccionar una contigencia a comparar.');
            //    return false;
            //}

            var indicadores = {
                PrimerAnio: $("#PrimerAnio").val(),
                SegundoAnio: $("#SegundoAnio").val(),
                ConstanteSeleccionada: $("#ConstanteSeleccionada").val(),
                IdEmpresaUsuaria: $('#IdEmpresaUsuaria').val(),
                TipoContingeciaComparar: tipoContingencia
            }
            PopupPosition();
            $('#descargarExcelComparativo').show();
            $.ajax({
                type: "post",
                url: urlBase + '/Indicadores/ObtenerDatosComparacionIndicadores',
                data: { indicadores: indicadores }
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'OK') {
                    if ($('svg').length > 0)
                        $('svg').remove();
                    $('#Indicadores').empty();
                    $('#panelIndicador').show();
                    $('#bannerindicador').show();                    
                    GenerarGrafica(response.Data[0].Datos, 2);
                    GenerarGrafica(response.Data[1].Datos, 2);
                } else if (response != undefined && response != '' && response.Mensaje == 'NOTFOUND') {
                    swal('Atención', response.Data);
                } else if (response != undefined && response != '' && response.Mensaje == 'INVALID')
                    swal('Atención', response.Data);
                OcultarPopupposition();
            }).fail(function (response) {
                console.log("Error en la peticion: " + response.Data);
                OcultarPopupposition();
            });
        }
        else {
            var idContingencia = $('input:radio[id=tipoComparacion]:checked').val()
            if (idContingencia < 1 || idContingencia == undefined)
                $('#errorcontigencia').show();
        }
    });

    $("#descargarExcelComparativo").click(function () {
        if (indi.valid() != false) {
            var tipoContingencia = $('input:radio[id=tipoComparacion]:checked').val();
            
            var indicadores = {
                PrimerAnio: $("#PrimerAnio").val(),
                SegundoAnio: $("#SegundoAnio").val(),
                IdEmpresaUsuaria: $('#IdEmpresaUsuaria').val(),
                ConstanteSeleccionada: $("#ConstanteSeleccionada").val(),
                TipoContingeciaComparar: tipoContingencia
            }
            PopupPosition();
            $.ajax({
                type: "post",
                url: urlBase + '/Indicadores/GenerarExcelComparativo',
                data: { indicadores: indicadores }
            }).done(function (response) {
                if (response != undefined && response != '' && response.Mensaje == 'Succes') {
                    window.location.href = response.Data
                } else if (response != undefined && response != '' && response.Mensaje == 'Fail')
                    swal('Atención', response.Data);
                OcultarPopupposition();
            }).fail(function (response) {
                console.log("Error en la peticion: " + response.Data);
                OcultarPopupposition();
            });
        }
    });
});
//genera la gráfica a partir de los datos
function GenerarGrafica(datos, tipo) {
    var rangoColores = ["#5DADE2", "#F39C12", "#58D68D"];
    var anchoSVG = 1700;
    var altoSVG = 800;
    var data1 = datos;
    var valoresIF = new Array();
    var valoresIS = new Array();
    var valoresILI = new Array();
    $.each(data1, function (i, value) {
        valoresIF.push(parseInt(value.VariableIF));
    });
    $.each(data1, function (i, value) {
        valoresIS.push(parseInt(value.VariableIS));
    });
    $.each(data1, function (i, value) {
        valoresILI.push(parseInt(value.VariableILI));
    });
    var dominioY = 0;
    var maxValorIF = Math.max.apply(null, valoresIF);
    var maxValorIS = Math.max.apply(null, valoresIS);
    var maxValorILI = Math.max.apply(null, valoresILI);

    var margin = { top: 20, right: 20, bottom: 30, left: 40 },
        width = 1700 - margin.left - margin.right,
        height = 600 - margin.top - margin.bottom, padding = 100;

    //se configuran los colores
    var color = d3.scaleOrdinal().range(rangoColores);
    // set the ranges
    var x1 = d3.scaleBand()
                .rangeRound([0, width])
                //.range([padding, width - padding * 2])
              .paddingInner(0.05);//.padding(0.1);

    var y1 = d3.scaleLinear()
              .range([height, 0])

    var xAxis = d3.axisBottom(x1)
                    .scale(x1);

    // function for the x grid lines
    function make_x_axis() {
        return d3.axisBottom()
            .scale(x1)
            .ticks(5)
    }

    // function for the y grid lines
    function make_y_axis() {
        return d3.axisLeft()
            .scale(y1)
            .ticks(5)
    }

    if (tipo == 2) {
        var svg = d3.select("#Indicadores").append("svg")
            .attr("width", width + margin.left + margin.right)
            .attr("height", altoSVG)
          .append("g")
            .attr("transform",
                  "translate(" + margin.left + "," + margin.top + ")");
    }
    else
    {
        var svg = d3.select("#Acumulado").append("svg")
            .attr("width", width + margin.left + margin.right)
            .attr("height", altoSVG)
          .append("g")
            .attr("transform",
                  "translate(" + margin.left + "," + margin.top + ")");
    }


    //Scale the range of the data in the domains
    x1.domain(data1.map(function (d) { return d.Mes; }));
    if (maxValorIF > maxValorIS && maxValorIF > maxValorILI)
        y1.domain([0, d3.max(valoresIF, function (d) { return (d / 1000) + 10; })]);
    else if (maxValorIS > maxValorIF && maxValorIS > maxValorILI)
        y1.domain([0, d3.max(valoresIS, function (d) { return (d / 1000) + 10; })]);
    else if (maxValorILI > maxValorIF && maxValorILI > maxValorIS)
        y1.domain([0, d3.max(valoresILI, function (d) { return (d / 1000) + 10; })]);
    //y1.domain([0, 200]);
    //append the rectangles for the bar chart
    svg.selectAll(".bar")
        .data(data1)
      .enter().append("rect")
        .attr("class", "bar")
        .attr("x", function (d) { return x1(d.Mes); })
        .attr("width", 30)
        .attr("y", function (d) { return y1(d.VariableIF / 1000); })
            .transition().duration(3000)
            .delay(function (d, i) { return i * 200; })
        .attr("height", function (d) { return height - y1(d.VariableIF / 1000); })
        .style("fill", function (d) { return "#5DADE2" });

    svg.selectAll(".bar2")
          .data(data1)
          .enter().append("rect")
          .attr("class", "bar2")
          .attr("x", function (d) { return x1(d.Mes); })
          .attr("width", 30)
          .attr("y", function (d) { return y1(d.VariableIS / 1000); })
          .transition().duration(3000)
          .delay(function (d, i) { return i * 200; })
          .attr("height", function (d) { return height - y1(d.VariableIS / 1000); })
           .attr("transform", "translate(" + (30) + "," + 0 + ")")
          .style("fill", function (d) { return "#F39C12" });

    svg.selectAll(".bar3")
              .data(data1)
              .enter().append("rect")
              .attr("class", "bar3")
              .attr("x", function (d) { return x1(d.Mes); })
              .attr("width", 30)
              .attr("y", function (d) { return y1(d.VariableILI / 1000); })
              .transition().duration(3000)
              .delay(function (d, i) { return i * 200; })
              .attr("height", function (d) { return height - y1(d.VariableILI / 1000); })
              .attr("transform", "translate(" + ((30 * 2)) + "," + 0 + ")")
              .style("fill", function (d) { return "#58D68D" });


    var tituloIF = ['IF'];
    var tituloIS = ['IS'];
    var tituloILI = ['ILI'];
    var colorscale = d3.scaleOrdinal(d3.schemeCategory10);
    var textSizeTooltip = "13px !important";
    var textSizeLegend = "11px !important";
    // Initiate Legend
    var legendIF = svg.append("g")
    .attr("class", "legend")
    .attr("height", 100)
    .attr("width", 200)
    .attr('transform', 'translate(500,680)');
    var legendIS = svg.append("g")
        .attr("class", "legend")
        .attr("height", 100)
        .attr("width", 200)
        .attr('transform', 'translate(700,680)');
    var legendILI = svg.append("g")
        .attr("class", "legend")
        .attr("height", 100)
        .attr("width", 200)
        .attr('transform', 'translate(900,680)');

    //crea cuadro para IF
    legendIF.selectAll('rect')
    .data(tituloIF)
    .enter()
    .append("rect")
    .attr("x", 30 - 8)
    .attr("y", function (d, i) {
        return i * 20;
    })
    .attr("width", 10)
    .attr("height", 30)
    .style("fill", function (d, i) {
        return "#5DADE2";
    });

    //Create texto para IF
    legendIF.selectAll('text')
    .data(tituloIF)
    .enter()
    .append("text")
    .attr("x", 30 + 3)
    .attr("y", function (d, i) {
        return i * 20 + 9;
    })
    .attr("font-size", textSizeLegend)
    .attr("fill", "#737373")
    .text(function (d) {
        return d;
    });

    //Crea el cuadro para IS
    legendIS.selectAll('rect')
    .data(tituloIS)
    .enter()
    .append("rect")
    .attr("x", 30 - 8)
    .attr("y", function (d, i) {
        return i * 20;
    })
    .attr("width", 10)
    .attr("height", 30)
    .style("fill", function (d, i) {
        return "#F39C12";
    });
    //Crea el texto para IS
    legendIS.selectAll('text')
    .data(tituloIS)
    .enter()
    .append("text")
    .attr("x", 30 + 3)
    .attr("y", function (d, i) {
        return i * 20 + 9;
    })
    .attr("font-size", textSizeLegend)
    .attr("fill", "#737373")
    .text(function (d) {
        return d;
    });
    //crea cuadro para ILI
    legendILI.selectAll('rect')
    .data(tituloILI)
    .enter()
    .append("rect")
    .attr("x", 30 - 8)
    .attr("y", function (d, i) {
        return i * 20;
    })
    .attr("width", 10)
    .attr("height", 30)
    .style("fill", function (d, i) {
        return "#58D68D";
    });
    //Crea el texto para ILI
    legendILI.selectAll('text')
    .data(tituloILI)
    .enter()
    .append("text")
    .attr("x", 30 + 3)
    .attr("y", function (d, i) {
        return i * 20 + 9;
    })
    .attr("font-size", textSizeLegend)
    .attr("fill", "#737373")
    .text(function (d) {
        return d;
    });

    //grupo para manejar el eje x. Los nombres de las contingencias
    svg.append("g")
        .attr("transform", "translate(0," + height + ")")
        .attr("class", "x axis")
        .call(xAxis)
        .selectAll("text")
            .style("text-anchor", "end")
            .attr("dx", "-.82em")
            .attr("dy", ".55em")
            .attr("width", x1.bandwidth() - 10)
            .attr("transform", "rotate(318)")
            .style("font-size", "10px")
            .style("font-weight", "600")
            .style("font-family", "Georgia")
            .style("fill", "black");
    //grupo para mostrar el eje y
    svg.append("g")
        .call(d3.axisLeft(y1))
        .selectAll("text")
            .attr("dx", "-1.4em")
            .attr("dy", ".55em");
}