var urlBase = ""
try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};

window.onload = function init() {
    var $ = go.GraphObject.make;  // for conciseness in defining templates
    myDiagram = new go.Diagram("DiagramaPositiva");
    myDiagram.initialContentAlignment = go.Spot.TopCenter;
    myDiagram.model = go.GraphObject.make(go.TreeModel,
      {
          nodeKeyProperty: "Id_Empleado",
          nodeParentKeyProperty: "Id_Jefe",
      });
    myDiagram.layout =
$(go.TreeLayout,
{
    angle: 90
});

    // define the Link template
    myDiagram.linkTemplate =
      $(go.Link, go.Link.Orthogonal,
        {
            corner: 10,
            relinkableFrom: true, relinkableTo: false
        },
        $(go.Shape, {
            strokeWidth: 2,
            stroke: "#ff7500"
        }));

    myDiagram.nodeTemplate =
$(go.Node, go.Panel.Auto,
$(go.Shape,
{
    strokeWidth: 4,
    figure: "RoundedRectangle",
    fill: "#7E8A97",
    stroke: "whitesmoke"
}),
$(go.Panel, go.Panel.Horizontal,
//$(go.TextBlock,
//{ margin: 10 }, // some room around the text
//new go.Binding("text", "Nombre_Empleado")
//),
$(go.TextBlock,
{ margin: 10, font: "bold 15px Helvetica, bold Arial, sans-serif", stroke: "whitesmoke", margin: 12, opacity: 0.85 }, // some room around the text
new go.Binding("text", "Cargo_Empleado"))
)

);


//function getParameterByName(name) {
//    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
//    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
//      results = regex.exec(location.search);
//    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
//}

//function generarImagen() {
//    var html = d3.select("svg")
//      .attr("title", "test")
//      .attr("version", 1.1)
//      .attr("xmlns", "http://www.w3.org/2000/svg")
//      .node().parentNode.innerHTML;
//    $(".preview").html("<img src=data:image/svg+xml;base64," + btoa(unescape(encodeURIComponent(html))) + ">");
//    /*    d3.select(".centro").append("div")
//              .attr("id", "download")
//              .html("Right-click on this preview and choose Save as<br />Left-Click to dismiss<br />")
//              .append("img")
//              .attr("src", "data:image/svg+xml;base64,"+ btoa(unescape(encodeURIComponent(html))));
//    */
//}

//function mouseover(d) {
//    d3.select(this).select("rect.rectangulo-hijos2").classed("hover", true);
//    d3.select(this).select("rect.rectangulo-hijos").classed("hover", true);
//    d3.select(this).select("rect.rectangulo-texto").classed("hover", true);

//    /* d3.select(this).append("text")
//         .attr("class", "hover")
//         .attr('transform', function(d){ 
//             return 'translate(5, -10)';
//         })
//         .text(d.name + ": " + d.id);*/
//}

//// Toggle children on click.
//function mouseout(d) {
//    //d3.select(this).select("text.hover").remove();
//    d3.select(this).select("rect.rectangulo-hijos2").classed("hover", false);
//    d3.select(this).select("rect.rectangulo-hijos").classed("hover", false);
//    d3.select(this).select("rect.rectangulo-texto").classed("hover", false);
//}

//function collapse(d) {
//    if (d.children) {
//        d._children = d.children;
//        d._children.forEach(collapse);
//        d.children = null;
//    }
//}

//function toggleAll(d) {
//    if (d.children) {
//        d.children.forEach(toggleAll);
//        toggle(d);
//    }
//}

//function actualizarEventos() {
//    $(".node a").on("click", function (e) {
//        window.location = $(this).attr('href');
//        //e.preventDefault();
//        e.stopPropagation();
//        //console.log("enlace");
//    });

//    /*        $(".node .texto .cuadro-nodo-texto i.fa.fa-info-circle").unbind("click").on("click", function(e){
//              var cuadroEnlaces=$(this).closest("g").parent().find(".cuadro-nodo-enlaces");
              
//              if(cuadroEnlaces.is(':visible')){
//                cuadroEnlaces.hide()  
//              }else{
//                cuadroEnlaces.show()  
//              }
              
//               //e.preventDefault();
//               e.stopPropagation();
//               //console.log("enlace");
//            });*/
//}

//function getDepth(obj) {
//    var depth = 0;
//    if (obj.children) {
//        obj.children.forEach(function (d) {
//            var tmpDepth = getDepth(d)
//            if (tmpDepth > depth) {
//                depth = tmpDepth
//            }
//        })
//    }
//    return 1 + depth
//}

//function wrap(text, width) {
//    text.each(function () {
//        var text = d3.select(this),
//          words = text.text().split(/\s+/).reverse(),
//          word,
//          line = [],
//          lineNumber = 0,
//          lineHeight = 1.2, // ems
//          x = text.attr("x"),
//          y = text.attr("y"),
//          dy = text.attr("dy") ? text.attr("dy") : 0;

//        tspan = text.text(null).append("tspan").attr("x", x).attr("y", y).attr("dy", dy + "em");
//        while (word = words.pop()) {
//            line.push(word);
//            tspan.text(line.join(" "));
//            var centradox = x + (width - tspan.node().getComputedTextLength()) / 2;

//            if (tspan.node().getComputedTextLength() > width) {
//                line.pop();
//                tspan.text(line.join(" "));
//                line = [word];
//                tspan = text.append("tspan").attr("x", x).attr("y", y).attr("dy", ++lineNumber * lineHeight + dy + "em").text(word);
//            }
//        }
//    });
//}

//var _queryTreeSort = function (options) {
//    var cfi, e, i, id, o, pid, rfi, ri, thisid, _i, _j, _len, _len1, _ref, _ref1;
//    id = options.id || "id";
//    pid = options.parentid || "Id_Jefe";
//    ri = [];
//    rfi = {};
//    cfi = {};
//    o = [];
//    _ref = options.q;
//    for (i = _i = 0, _len = _ref.length; _i < _len; i = ++_i) {
//        e = _ref[i];
//        rfi[e[id]] = i;
//        if (cfi[e[pid]] == null) {
//            cfi[e[pid]] = [];
//        }
//        cfi[e[pid]].push(options.q[i][id]);
//    }
//    _ref1 = options.q;
//    for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
//        e = _ref1[_j];
//        if (rfi[e[pid]] == null) {
//            ri.push(e[id]);
//        }
//    }
//    while (ri.length) {
//        thisid = ri.splice(0, 1);
//        o.push(options.q[rfi[thisid]]);
//        if (cfi[thisid] != null) {
//            ri = cfi[thisid].concat(ri);
//        }
//    }
//    return o;
//};

//var _makeTree = function (options) {
//    var children, e, id, o, pid, temp, _i, _len, _ref;
//    id = options.id || "id";
//    pid = options.parentid || "Id_Jefe";
//    children = options.children || "Id_Empleado";
//    temp = {};
//    o = [];
//    _ref = options.q;
//    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
//        e = _ref[_i];
//        e[children] = [];
//        temp[e[id]] = e;
//        if (temp[e[pid]] != null) {
//            temp[e[pid]][children].push(e);
//        } else {
//            o.push(e);
//        }
//    }
//    return o;
//};

//var _renderTree = function (tree) {
//    var e, html, _i, _len;
//    html = "<ul class='sections'>";
//    for (_i = 0, _len = tree.length; _i < _len; _i++) {
//        e = tree[_i];
//        html += "<li class='department'><a href=''><span>" + e.name + "</span></a>";
//        if (e.children != null) {
//            html += _renderTree(e.children);
//        }
//        html += "</li>";
//    }
//    html += "</ul>";
//    return html;
//};

//$(document).ready(function () {

//    var id_padre = getParameterByName('organigrama_id_padre');
//    if (id_padre == '') {
//        id_padre = 1;
//    }
//    var orgJSON = [{ "id": "1", "parentid": "20", "name": "Presidencia", "path": "" }, { "id": "4803", "parentid": "1", "name": "Asesoría e Asistencia Xurídica", "path": "100,20,1" }, { "id": "13477", "parentid": "1", "name": "Área de Acción Territorial", "path": "100,20,1" }, { "id": "3924", "parentid": "1", "name": "Representación Sindical", "path": "100,20,1" }, { "id": "4221", "parentid": "1", "name": "Grupos Políticos", "path": "100,20,1" }, { "id": "14177", "parentid": "1", "name": "Área de Servizos Provinciais", "path": "100,20,1" }, { "id": "14184", "parentid": "1", "name": "Área de Desenvolvemento Económico, Turismo e Medio Ambiente", "path": "100,20,1" }, { "id": "136", "parentid": "1", "name": "Intervención Xeral e Xestión Económico-Financeira", "path": "100,20,1" }, { "id": "6200", "parentid": "1", "name": "Gabinete de Presidencia", "path": "100,20,1" }, { "id": "609", "parentid": "1", "name": "Secretaría Xeral", "path": "100,20,1" }, { "id": "688", "parentid": "1", "name": "Tesourería e Xestión de Tributos", "path": "100,20,1" }, { "id": "12961", "parentid": "1", "name": "Área de Persoal e Organización", "path": "100,20,1" }, { "id": "14175", "parentid": "12961", "name": "Servizo de Organización", "path": "100,20,1,12961" }, { "id": "13420", "parentid": "12961", "name": "Servizo de Sistemas e Soporte", "path": "100,20,1,12961" }, { "id": "13424", "parentid": "12961", "name": "Servizo de Informática e Administración Electrónica", "path": "100,20,1,12961" }, { "id": "165", "parentid": "12961", "name": "Servizo de Planificación e Xestión de Recursos Humanos", "path": "100,20,1,12961" }, { "id": "13421", "parentid": "13420", "name": "Sección de Soporte e Atención a Usuarios", "path": "100,20,1,12961,13420" }, { "id": "13423", "parentid": "13420", "name": "Sección de Sistemas e Infraestruturas", "path": "100,20,1,12961,13420" }, { "id": "13422", "parentid": "13424", "name": "Sección de Administración Electrónica", "path": "100,20,1,12961,13424" }, { "id": "13425", "parentid": "13424", "name": "Sección de Asistencia Informática", "path": "100,20,1,12961,13424" }, { "id": "11", "parentid": "14175", "name": "Sección de Formación", "path": "100,20,1,12961,14175" }, { "id": "13428", "parentid": "14175", "name": "Sección de Organización", "path": "100,20,1,12961,14175" }, { "id": "4498", "parentid": "14175", "name": "Sección da Organización da Prevención de Riscos Laborais", "path": "100,20,1,12961,14175" }, { "id": "6199", "parentid": "165", "name": "Sección de Xestión de Recursos Humanos", "path": "100,20,1,12961,165" }, { "id": "7672", "parentid": "165", "name": "Sección de Planificación de Recursos Humanos", "path": "100,20,1,12961,165" }, { "id": "10233", "parentid": "6199", "name": "Negociado de Seguridade Social I", "path": "100,20,1,12961,165,6199" }, { "id": "10232", "parentid": "6199", "name": "Negociado de Nóminas", "path": "100,20,1,12961,165,6199" }, { "id": "14179", "parentid": "6199", "name": "Negociado de Xestión", "path": "100,20,1,12961,165,6199" }, { "id": "14188", "parentid": "6199", "name": "Negociado de Seguridade Social II", "path": "100,20,1,12961,165,6199" }, { "id": "10231", "parentid": "7672", "name": "Negociado de Planificación I", "path": "100,20,1,12961,165,7672" }, { "id": "14925", "parentid": "7672", "name": "Negociado de Planificación II", "path": "100,20,1,12961,165,7672" }, { "id": "9", "parentid": "13477", "name": "Servizo de Xestión de Plans", "path": "100,20,1,13477" }, { "id": "7674", "parentid": "13477", "name": "Servizo de Vías e Obras", "path": "100,20,1,13477" }, { "id": "166", "parentid": "13477", "name": "Servizo de Arquitectura e Mantemento", "path": "100,20,1,13477" }, { "id": "14202", "parentid": "13477", "name": "Servizo de Apoio Técnico a Municipios", "path": "100,20,1,13477" }, { "id": "149", "parentid": "13477", "name": "Servizo de Asistencia Técnica Urbanística", "path": "100,20,1,13477" }, { "id": "7665", "parentid": "166", "name": "Sección de Arquitectura e Mantemento", "path": "100,20,1,13477,166" }, { "id": "19", "parentid": "9", "name": "Sección de Plans Especiais", "path": "100,20,1,13477,9" }, { "id": "18", "parentid": "9", "name": "Sección de Plans Provinciais", "path": "100,20,1,13477,9" }, { "id": "10238", "parentid": "18", "name": "Negociado de Plans Provinciais", "path": "100,20,1,13477,9,18" }, { "id": "10237", "parentid": "19", "name": "Negociado de Plans Especiais", "path": "100,20,1,13477,9,19" }, { "id": "135", "parentid": "136", "name": "Servizo de Contabilidade", "path": "100,20,1,136" }, { "id": "151", "parentid": "136", "name": "Servizo de Orzamentos, Estudos e Asistencia Económica", "path": "100,20,1,136" }, { "id": "2647", "parentid": "136", "name": "Servizo de Fiscalización e Control Financeiro", "path": "100,20,1,136" }, { "id": "15886", "parentid": "135", "name": "Sección de Contabilidade III", "path": "100,20,1,136,135" }, { "id": "6190", "parentid": "135", "name": "Sección de Contabilidad I", "path": "100,20,1,136,135" }, { "id": "6191", "parentid": "135", "name": "Sección de Contabilidade II", "path": "100,20,1,136,135" }, { "id": "15887", "parentid": "15886", "name": "Negociado VI", "path": "100,20,1,136,135,15886" }, { "id": "14181", "parentid": "6190", "name": "Negociado II", "path": "100,20,1,136,135,6190" }, { "id": "10215", "parentid": "6190", "name": "Negociado I", "path": "100,20,1,136,135,6190" }, { "id": "10213", "parentid": "6191", "name": "Negociado III", "path": "100,20,1,136,135,6191" }, { "id": "10216", "parentid": "6191", "name": "Negociado V", "path": "100,20,1,136,135,6191" }, { "id": "10214", "parentid": "6191", "name": "Negociado IV", "path": "100,20,1,136,135,6191" }, { "id": "2994", "parentid": "151", "name": "Sección I", "path": "100,20,1,136,151" }, { "id": "6201", "parentid": "151", "name": "Sección II", "path": "100,20,1,136,151" }, { "id": "10212", "parentid": "2994", "name": "Negociado I", "path": "100,20,1,136,151,2994" }, { "id": "3846", "parentid": "2647", "name": "Sección de Control I", "path": "100,20,1,136,2647" }, { "id": "3173", "parentid": "2647", "name": "Sección de Control II", "path": "100,20,1,136,2647" }, { "id": "10209", "parentid": "3173", "name": "Negociado de Control de Ingresos", "path": "100,20,1,136,2647,3173" }, { "id": "10211", "parentid": "3173", "name": "Negociado de Control Financeiro e Auditoría de Gastos", "path": "100,20,1,136,2647,3173" }, { "id": "10208", "parentid": "3846", "name": "Negociado de Contratos e Outros Gastos Correntes", "path": "100,20,1,136,2647,3846" }, { "id": "10205", "parentid": "3846", "name": "Negociado de Fiscalización de Subvencións", "path": "100,20,1,136,2647,3846" }, { "id": "10206", "parentid": "3846", "name": "Negociado de Fiscalización de Plans  e Outros Gastos con Financiamento Afectado", "path": "100,20,1,136,2647,3846" }, { "id": "10207", "parentid": "3846", "name": "Negociado de Fiscalización de Persoal", "path": "100,20,1,136,2647,3846" }, { "id": "7667", "parentid": "14177", "name": "Servizos Xerais Centro Educativo Calvo Sotelo", "path": "100,20,1,14177" }, { "id": "7666", "parentid": "14177", "name": "Centro Residencial Docente Calvo Sotelo", "path": "100,20,1,14177" }, { "id": "1699", "parentid": "14177", "name": "IES Rafael Puga Ramón", "path": "100,20,1,14177" }, { "id": "6", "parentid": "14177", "name": "Servizo de Acción Social, Cultural e Deportes", "path": "100,20,1,14177" }, { "id": "14146", "parentid": "14177", "name": "Servizo de Patrimonio", "path": "100,20,1,14177" }, { "id": "3314", "parentid": "14177", "name": "Biblioteca", "path": "100,20,1,14177" }, { "id": "3824", "parentid": "14177", "name": "IES Calvo Sotelo", "path": "100,20,1,14177" }, { "id": "5039", "parentid": "14177", "name": "Conservatorio Profesional de Danza", "path": "100,20,1,14177" }, { "id": "6192", "parentid": "14177", "name": "Centro de Día de Menores de Ferrol", "path": "100,20,1,14177" }, { "id": "5402", "parentid": "14177", "name": "Fogar Infantil Emilio Romay", "path": "100,20,1,14177" }, { "id": "6195", "parentid": "14177", "name": "Centro Residencial Cultural Pazo de Mariñán", "path": "100,20,1,14177" }, { "id": "14392", "parentid": "14146", "name": "Negociado", "path": "100,20,1,14177,14146" }, { "id": "15299", "parentid": "14146", "name": "Sección de Patrimonio", "path": "100,20,1,14177,14146" }, { "id": "10242", "parentid": "3314", "name": "Negociado", "path": "100,20,1,14177,3314" }, { "id": "9898", "parentid": "3824", "name": "Departamento de Artes Gráficas", "path": "100,20,1,14177,3824" }, { "id": "9896", "parentid": "3824", "name": "Departamento de Electricidade", "path": "100,20,1,14177,3824" }, { "id": "9894", "parentid": "3824", "name": "Departamento de Formación e Orientación Laboral", "path": "100,20,1,14177,3824" }, { "id": "9893", "parentid": "3824", "name": "Departamento de Orientación", "path": "100,20,1,14177,3824" }, { "id": "9892", "parentid": "3824", "name": "Departamento de Programas de Cualificación Profesional Inicial", "path": "100,20,1,14177,3824" }, { "id": "9897", "parentid": "3824", "name": "Departamento de Madeira", "path": "100,20,1,14177,3824" }, { "id": "11843", "parentid": "3824", "name": "Formación en Centros de Trabajo", "path": "100,20,1,14177,3824" }, { "id": "9895", "parentid": "3824", "name": "Departamento de Fabricación Mecánica", "path": "100,20,1,14177,3824" }, { "id": "602", "parentid": "6", "name": "Sección de Servizos Sociais", "path": "100,20,1,14177,6" }, { "id": "13", "parentid": "6", "name": "Sección de Educación, Cultura e Deportes", "path": "100,20,1,14177,6" }, { "id": "10219", "parentid": "13", "name": "Negociado de Deportes", "path": "100,20,1,14177,6,13" }, { "id": "10218", "parentid": "13", "name": "Negociado de Cultura", "path": "100,20,1,14177,6,13" }, { "id": "10217", "parentid": "602", "name": "Negociado de Servicios Sociais", "path": "100,20,1,14177,6,602" }, { "id": "14185", "parentid": "14184", "name": "Servizo de Promoción Económica, Turismo e Emprego", "path": "100,20,1,14184" }, { "id": "3", "parentid": "14184", "name": "Servizo de Desenvolvemento Territorial e Medio Ambiente", "path": "100,20,1,14184" }, { "id": "14186", "parentid": "14185", "name": "Sección de Promoción e Desenvolvemento Turístico", "path": "100,20,1,14184,14185" }, { "id": "13426", "parentid": "14185", "name": "Sección de Coordinación de Proxectos Técnicos", "path": "100,20,1,14184,14185" }, { "id": "14", "parentid": "14185", "name": "Sección de Promoción Económica e Emprego", "path": "100,20,1,14184,14185" }, { "id": "10220", "parentid": "14", "name": "Negociado de Promoción Económica", "path": "100,20,1,14184,14185,14" }, { "id": "14187", "parentid": "3", "name": "Sección de Desenvolvemento Territorial, Cooperación e Medio Ambiente", "path": "100,20,1,14184,3" }, { "id": "6233", "parentid": "4221", "name": "Grupo Popular", "path": "100,20,1,4221" }, { "id": "16248", "parentid": "4221", "name": "Alternativa dos Veciños", "path": "100,20,1,4221" }, { "id": "16246", "parentid": "4221", "name": "Marea Atlántica", "path": "100,20,1,4221" }, { "id": "16247", "parentid": "4221", "name": "Compostela Aberta", "path": "100,20,1,4221" }, { "id": "9364", "parentid": "4221", "name": "Grupo Socialista", "path": "100,20,1,4221" }, { "id": "9365", "parentid": "4221", "name": "Grupo BNG", "path": "100,20,1,4221" }, { "id": "6188", "parentid": "609", "name": "Oficialía Maior", "path": "100,20,1,609" }, { "id": "5", "parentid": "6188", "name": "Servizo de Contratación", "path": "100,20,1,609,6188" }, { "id": "4", "parentid": "6188", "name": "Imprenta Provincial", "path": "100,20,1,609,6188" }, { "id": "134", "parentid": "6188", "name": "Sección de Información y Actas", "path": "100,20,1,609,6188" }, { "id": "7700", "parentid": "6188", "name": "Sección de Arquivo", "path": "100,20,1,609,6188" }, { "id": "4645", "parentid": "6188", "name": "Parque Móbil", "path": "100,20,1,609,6188" }, { "id": "6189", "parentid": "6188", "name": "Servizos Internos", "path": "100,20,1,609,6188" }, { "id": "10229", "parentid": "134", "name": "Negociado de Información e Rexistro", "path": "100,20,1,609,6188,134" }, { "id": "15937", "parentid": "134", "name": "Negociado de Actas e Acordos", "path": "100,20,1,609,6188,134" }, { "id": "7673", "parentid": "5", "name": "Sección de Contratación I", "path": "100,20,1,609,6188,5" }, { "id": "15057", "parentid": "5", "name": "Sección de Contratación II", "path": "100,20,1,609,6188,5" }, { "id": "10223", "parentid": "7673", "name": "Negociado IV", "path": "100,20,1,609,6188,5,7673" }, { "id": "10222", "parentid": "7673", "name": "Negociado II", "path": "100,20,1,609,6188,5,7673" }, { "id": "10224", "parentid": "7673", "name": "Negociado III", "path": "100,20,1,609,6188,5,7673" }, { "id": "10221", "parentid": "7673", "name": "Negociado I", "path": "100,20,1,609,6188,5,7673" }, { "id": "14182", "parentid": "7700", "name": "Negociado de Arquivo", "path": "100,20,1,609,6188,7700" }, { "id": "2998", "parentid": "6200", "name": "Gabinete de Presidencia e Comunicación", "path": "100,20,1,6200" }, { "id": "12663", "parentid": "6200", "name": "Cambio Estratéxico e Desenvolvemento Sostible", "path": "100,20,1,6200" }, { "id": "6302", "parentid": "6200", "name": "Secretaría Particular", "path": "100,20,1,6200" }, { "id": "3152", "parentid": "6200", "name": "Relacións Públicas e Protocolo", "path": "100,20,1,6200" }, { "id": "12654", "parentid": "2998", "name": "Gabinete de Prensa", "path": "100,20,1,6200,2998" }, { "id": "3181", "parentid": "688", "name": "Zona de Recadación núm.1", "path": "100,20,1,688" }, { "id": "6187", "parentid": "688", "name": "Unidade Central de Atención ao Contribuínte", "path": "100,20,1,688" }, { "id": "3197", "parentid": "688", "name": "Zona de Recadación núm.4", "path": "100,20,1,688" }, { "id": "3196", "parentid": "688", "name": "Zona de Recadación núm.2", "path": "100,20,1,688" }, { "id": "3195", "parentid": "688", "name": "Zona de Recadación núm.3", "path": "100,20,1,688" }, { "id": "653", "parentid": "688", "name": "Inspección de Tributos Locales", "path": "100,20,1,688" }, { "id": "8", "parentid": "688", "name": "Servizo de Xestión Tributaria", "path": "100,20,1,688" }, { "id": "691", "parentid": "688", "name": "Servizo Central de Recadación", "path": "100,20,1,688" }, { "id": "11350", "parentid": "688", "name": "Unidade de Instrución de Sancións Municipais", "path": "100,20,1,688" }, { "id": "10247", "parentid": "688", "name": "Sección Central de Tesourería", "path": "100,20,1,688" }, { "id": "10227", "parentid": "10247", "name": "Negociado de Ingresos", "path": "100,20,1,688,10247" }, { "id": "10226", "parentid": "10247", "name": "Negociado de Contabilidade", "path": "100,20,1,688,10247" }, { "id": "14393", "parentid": "11350", "name": "Negociado de procesos masivos", "path": "100,20,1,688,11350" }, { "id": "11351", "parentid": "11350", "name": "Negociado de procedemento administrativo e tramitación", "path": "100,20,1,688,11350" }, { "id": "7338", "parentid": "3181", "name": "Oficina de Ordes", "path": "100,20,1,688,3181" }, { "id": "7334", "parentid": "3181", "name": "Oficina de Arteixo", "path": "100,20,1,688,3181" }, { "id": "7329", "parentid": "3195", "name": "Oficina de Arzúa", "path": "100,20,1,688,3195" }, { "id": "7340", "parentid": "3195", "name": "Oficina de Ribeira", "path": "100,20,1,688,3195" }, { "id": "13936", "parentid": "3195", "name": "Oficina de Ames", "path": "100,20,1,688,3195" }, { "id": "7341", "parentid": "3195", "name": "Oficina de Santiago", "path": "100,20,1,688,3195" }, { "id": "7339", "parentid": "3196", "name": "Oficina de Ortigueira", "path": "100,20,1,688,3196" }, { "id": "7335", "parentid": "3196", "name": "Oficina de Ferrol", "path": "100,20,1,688,3196" }, { "id": "7336", "parentid": "3196", "name": "Oficina de Narón", "path": "100,20,1,688,3196" }, { "id": "7332", "parentid": "3196", "name": "Oficina de Betanzos", "path": "100,20,1,688,3196" }, { "id": "7337", "parentid": "3197", "name": "Oficina de Negreira", "path": "100,20,1,688,3197" }, { "id": "7333", "parentid": "3197", "name": "Oficina de Corcubión", "path": "100,20,1,688,3197" }, { "id": "10245", "parentid": "6187", "name": "Negociado de Información", "path": "100,20,1,688,6187" }, { "id": "14394", "parentid": "653", "name": "Negociado de Coordinación", "path": "100,20,1,688,653" }, { "id": "5497", "parentid": "691", "name": "Coordinación da Xestión Recadatoria", "path": "100,20,1,688,691" }, { "id": "10243", "parentid": "691", "name": "Sección de Revisión", "path": "100,20,1,688,691" }, { "id": "14410", "parentid": "691", "name": "Unidade Central de Recadación Executiva (UCRE)", "path": "100,20,1,688,691" }, { "id": "13427", "parentid": "691", "name": "Sección de Asistencia á Xestión e Procedementos Tributarios", "path": "100,20,1,688,691" }, { "id": "10246", "parentid": "10243", "name": "Negociado de Devolucións", "path": "100,20,1,688,691,10243" }, { "id": "14414", "parentid": "10243", "name": "Negociado de Recursos", "path": "100,20,1,688,691,10243" }, { "id": "14411", "parentid": "14410", "name": "Negociado de Control", "path": "100,20,1,688,691,14410" }, { "id": "14413", "parentid": "14410", "name": "Negociado de Contabilidade", "path": "100,20,1,688,691,14410" }, { "id": "14412", "parentid": "14410", "name": "Negociado de Executiva", "path": "100,20,1,688,691,14410" }, { "id": "10244", "parentid": "5497", "name": "Negociado", "path": "100,20,1,688,691,5497" }, { "id": "17", "parentid": "8", "name": "Sección I", "path": "100,20,1,688,8" }, { "id": "124", "parentid": "8", "name": "Sección II", "path": "100,20,1,688,8" }, { "id": "125", "parentid": "8", "name": "Sección IV", "path": "100,20,1,688,8" }, { "id": "126", "parentid": "8", "name": "Sección III", "path": "100,20,1,688,8" }, { "id": "14574", "parentid": "124", "name": "Negociado II", "path": "100,20,1,688,8,124" }, { "id": "10240", "parentid": "124", "name": "Negociado I", "path": "100,20,1,688,8,124" }, { "id": "14575", "parentid": "125", "name": "Negociado I", "path": "100,20,1,688,8,125" }, { "id": "10241", "parentid": "126", "name": "Negociado I", "path": "100,20,1,688,8,126" }, { "id": "10239", "parentid": "17", "name": "Negociado I", "path": "100,20,1,688,8,17" }];

//    var indiceNodo = 0;

//    resultado = _queryTreeSort({
//        q: orgJSON
//    });
//    //    $('#arbol_resultado').html(JSON.stringify(sqlquery, true, 2));

//    var treeBD;
//    treeBD = _makeTree({
//        q: resultado
//    });

//    var html_arbol = _renderTree(treeBD);
//    //    $('#arbol_ordenado').html(JSON.stringify(treeBD, true, 2));

//    var margin = {
//        top: 20,
//        right: 30,
//        bottom: 20,
//        left: 30
//    },
//      width = $(".org-chart.cf").width() - margin.right - margin.left,
//      height = 1200 - margin.top - margin.bottom;

//    var i = 0,
//      duration = 750,
//      root;

//    var tree = d3.layout.tree()
//      .size([width, height])
//      .separation(function separation(a, b) {
//          return (a.parent == b.parent ? 1 : 2) * a.depth;
//      });

//    var diagonal = d3.svg.diagonal()
//      .projection(function (d) {
//          return [d.x, d.y];
//      });

//    var svg = d3.select(".org-chart").append("svg")
//      .attr("width", width + margin.right + margin.left)
//      .attr("height", height + margin.top + margin.bottom)
//      //.attr("viewbox", "0 0 500 500")
//      .call(zm = d3.behavior.zoom().scaleExtent([0.5, 3]).on("zoom", redraw))
//      .append("g")
//      .attr("transform", "translate(" + margin.left + "," + margin.top + ")")
//      .attr("width", width + margin.right + margin.left)
//      .attr("height", height + margin.top + margin.bottom)
//      .style("text-rendering", "optimizeLegibility")
//      .style("shape-rendering", "default");

//    var slider = d3.select(".sliderZoom").append("p").append("input")
//      .datum({})
//      .attr("type", "range")
//      .attr("value", 1)
//      .attr("min", zm.scaleExtent()[0])
//      .attr("max", zm.scaleExtent()[1])
//      .attr("step", (zm.scaleExtent()[1] - zm.scaleExtent()[0]) / 100)
//      .on("input", slided);

//    function slided(d) {
//        zm.scale(d3.select(this).property("value"))
//          .event(svg);
//    }

//    zm.translate([margin.left, margin.top]);
//    var flare = treeBD[0];

//    root = flare;
//    root.x0 = width / 2;
//    root.y0 = 0;

//    var num_nodo = 0;
//    /*
  
//        function getIndiceHijo(nodo){
//          var padre=
//        }*/

//    function update(source) {

//        // Compute the new tree layout.
//        var nodes = tree.nodes(root).reverse(),
//          links = tree.links(nodes);
//        var profundidadArbol = getDepth(root);

//        //var hijosPares = (source._children != null) ? 1 - (source._children.length % 2) : 0;
//        //var numeroHijos = 0;
//        var profundidadActual = source.depth;
//        // Normalize for fixed-depth.
//        var indice = 0;
//        var niveles = [];

//        for (n = 0; n < profundidadArbol; n++) {
//            niveles[n] = 0;
//        }

//        nodes.forEach(function (d, i) {
//            //var index = d3.select(this).indexOf(d3.select(this.parentNode).datum());
//            //var hijosPares=(d._children != null) ? (1-d._children.length) % 2 : 0;
//            //console.log("update");

//            /* indice=i-numeroHijos;
//             if (d.depth > profundidadActual) {
//               numeroHijos=(d._children != null) ? d._children.length  : 0;
//             }*/

//            niveles[d.depth] = niveles[d.depth] + 1;
//            indice = niveles[d.depth];
//            //console.log(d.name+":"+d.depth+":"+i+":"+indice);
//            if (d.depth > 1) {
//                d.y = d.depth * 200;
//                if (d.parent.children.length > 2) {
//                    d.y += (indice % 2) * 75;
//                }

//            } else if (d.depth == 1) { //primer nivel, menos separación
//                d.y = d.depth * 100;
//                if (d.parent.children.length > 2) {
//                    d.y += (indice % 2) * 75;
//                }
//            } else {
//                d.y = 0;
//            }

//        });

//        // Update the nodes…
//        var node = svg.selectAll("g.node")
//          .data(nodes, function (d, i) {
//              return d.id || (d.id = ++i);
//          });

//        // Enter any new nodes at the parent's previous position.

//        var nodeEnter = node.enter();
//        var grupoNodo = nodeEnter.append("g")
//          .attr("class", "node")
//          .attr("id", function (d) {
//              return d.id
//          })
//          .attr("transform", function (d, i) {
//              return "translate(" + source.x0 + "," + source.y0 + ")";
//          })
//          .on("mouseover", mouseover)
//          .on("mouseout", mouseout)
//        //.call(drag);
//        ;

//        var grupoTexto = grupoNodo.append("g")
//          .attr("class", "texto");

//        grupoTexto.append("circle")
//          .attr("r", 1e-6)
//          .on("click", click)
//          .style("fill", function (d) {
//              return d._children ? "lightsteelblue" : "lightgrey";
//          });

//        /*          var cuadroTexto = grupoTexto.append('foreignObject');
//                  cuadroTexto.attr('dx', function(d) { return 2*d.name.length} )
//                        .attr('y', function(d) { return d.children || d._children ? 0 : 100; })
//                        .on("click", click)
//                        .html(function(d,i) { 
//                      var cuadroNodoEnlaces = '<div class="cuadro-nodo-enlaces"><div class="enlaces-nodo">'
//                      +'<a title="Ver en directorio" href="/directorio?id='+d.id+'"><i class="fa fa-list-alt"></i></a>'
//                      +'<a title="Ver Organigrama" href="<?php echo $_SERVER['REQUEST_URI']; ?>&organigrama_id_padre='+d.id+'"><i class="fa fa-sitemap"></i></a>'
//                      +'</div></div>';
          
//                      var cuadroNodo = '<div class="cuadro-nodo-texto">'
//                      +cuadroNodoEnlaces
//                      +'<div class="texto-nodo">'
//                      +d.name
//                      +'</div></div>'; 
//                          return cuadroNodo;
//                      });*/

//        //si tiene hijos 

//        grupoTexto.append('rect')
//          .on("click", click)
//          .attr("x", "-75px")
//          .attr("y", "20")
//          .attr("rx", 4)
//          .attr("ry", 4)
//          .attr("width", 150)
//          .attr("height", 50)
//          .attr("fill", "#fff")
//          .attr("class", "rectangulo-hijos")
//          .style("stroke", "#777")
//          //.attr("transform", "rotate(-2)")
//          .style("stroke-width", function (d) {
//              var hijos = 0;
//              if (d._children != null) {
//                  hijos = d._children.length;
//              }
//              return (hijos <= 0) ? "0px" : "0.2px";
//          });

//        grupoTexto.append('rect')
//          .on("click", click)
//          .attr("x", "-75px")
//          .attr("y", "20")
//          .attr("rx", 4)
//          .attr("ry", 4)
//          .attr("width", 150)
//          .attr("height", 50)
//          .attr("fill", "#fff")
//          .attr("class", "rectangulo-hijos2")
//          .style("stroke", "#777")
//          //.attr("transform", "rotate(2)")
//          .style("stroke-width", function (d) {
//              var hijos = 0;
//              if (d._children != null) {
//                  hijos = d._children.length;
//              }
//              return (hijos <= 0) ? "0px" : "0.2px";
//          });

//        //Dibujamos el cuadro con la barra de enlaces y con el texto de cada departamento
//        grupoTexto.append('rect')
//          .attr("class", "barra-enlaces")
//          .attr("x", "-75px")
//          .attr("y", "0")
//          .attr("width", 150)
//          .attr("height", 20)
//          .attr("fill", "#247497")
//          .attr("stroke", "#ccc")
//          .attr("stroke-width", "0.2px");

//        grupoTexto.append('a')
//          .attr("x", "-50px")
//          .attr("y", "10")
//          .attr("xlink:href", function (d) {
//              return '/directorio?id=' + d.id;
//          })
//          .attr("xlink:title", "Ver directorio")
//          .attr("fill", "white")
//          .attr("height", 20)
//          .attr("width", 150)
//          .attr("font-size", 12)
//          .append('text')
//          .attr("font-family", "FontAwesome")
//          .attr("x", "40px")
//          .attr("y", "15")
//          .text('\uf095');

//        grupoTexto.append('a')
//          .attr("x", "25px")
//          .attr("y", "10")
//          .attr("xlink:href", function (d) {
//              return window.location.href + "&organigrama_id_padre=" + d.id;
//          })
//          .attr("xlink:title", "Ver organigrama")
//          .attr("fill", "white")
//          .attr("height", 20)
//          .attr("width", 150)
//          .attr("font-size", 12)
//          .append('text')
//          .attr("font-family", "FontAwesome")
//          .attr("x", "55px")
//          .attr("y", "15")
//          .text('\uf0e8');

//        grupoTexto.append('rect')
//          .on("click", click)
//          .attr("x", "-75px")
//          .attr("y", "20")
//          .attr("rx", 4)
//          .attr("ry", 4)
//          .attr("width", 150)
//          .attr("height", 50)
//          .attr("fill", "white")
//          .classed("rectangulo-texto", true)
//          .attr("stroke", "#777")
//          .attr("stroke-width", "0.2px");

//        grupoTexto.append('text')
//          .on("click", click)
//          .text(function (d, i) {
//              return d.name;
//          })
//          .attr("x", "0")
//          .attr("text-anchor", "middle")
//          .attr("y", "33")
//          .attr("font-size", 11)
//          .attr("fill", "#555")
//          .call(wrap, 140);

//        var grupoEnlaces = grupoNodo.append("g").attr("class", "nodo-enlaces");

//        /*            grupoEnlaces.append('foreignObject')
//                        .attr('dx', function(d) { return 2*d.name.length} )
//                      .attr('y', function(d) { return d.children || d._children ? 0 : 100; })
//                      .html(function(d,i) { 
//                    var cuadroNodo = '<div class="cuadro-nodo-enlaces"><div class="enlaces-nodo">'
//                    +'<a href="/directorio?id='+d.id+'"><i class="fa fa-list-alt"></i></a>'
//                    +'<a href="<?php echo $_SERVER['REQUEST_URI']; ?>&organigrama_id_padre='+d.id+'"><i class="fa fa-sitemap"></i></a>'
//                    +'</div></div>';
//                        return cuadroNodo;
//                    });*/

//        // Transition nodes to their new position.
//        var nodeUpdate = node.transition()
//          .duration(duration)
//          .attr("transform", function (d) {
//              return "translate(" + d.x + "," + d.y + ")";
//          });

//        nodeUpdate.select("circle")
//          .attr("r", 4.5)
//          .style("fill", function (d) {
//              return d._children ? "lightsteelblue" : "lightgrey";
//          });

//        nodeUpdate.select("text")
//          .style("fill-opacity", 1);

//        // Transition exiting nodes to the parent's new position.
//        var nodeExit = node.exit().transition()
//          .duration(duration)
//          .attr("transform", function (d) {
//              return "translate(" + source.x + "," + source.y + ")";
//          })
//          .remove();

//        nodeExit.select("circle")
//          .attr("r", 1e-6);

//        nodeExit.select("text")
//          .style("fill-opacity", 1e-6);

//        // Update the links…
//        var link = svg.selectAll("path.link")
//          .data(links, function (d) {
//              return d.target.id;
//          });

//        // Enter any new links at the parent's previous position.
//        link.enter().insert("path", "g")
//          .attr("class", "link")
//          .attr("d", function (d) {
//              var o = {
//                  x: source.x0,
//                  y: source.y0
//              };
//              return diagonal({
//                  source: o,
//                  target: o
//              });
//          });

//        // Transition links to their new position.
//        link.transition()
//          .duration(duration)
//          .attr("d", diagonal);

//        // Transition exiting nodes to the parent's new position.
//        link.exit().transition()
//          .duration(duration)
//          .attr("d", function (d) {
//              var o = {
//                  x: source.x,
//                  y: source.y
//              };
//              return diagonal({
//                  source: o,
//                  target: o
//              });
//          })
//          .remove();

//        // Stash the old positions for transition.
//        nodes.forEach(function (d) {
//            d.x0 = d.x;
//            d.y0 = d.y;
//        });

//        actualizarEventos();
//        //d3.selectAll(".node").call(drag);

//        /*var delay=2000; //1 seconds
  
//        setTimeout(function(){
//          generarImagen();
//        }, delay); */
//    } //fin update

//    // Toggle children on click.
//    function click(d) {
//        if (d.children) {
//            d._children = d.children;
//            d.children = null;
//        } else {
//            d.children = d._children;
//            d._children = null;
//        }
//        /*      var nodo = d3.select(this.parentNode.parentNode);
//          activo = nodo.attr("activo");
//          if (activo == 1){
           
//            //nodo.moveToBack();
    
//            nodo.attr("activo",0);
//            nodo.attr("class","noactivo");
    
//            //d3.select(this.parentNode).select("rect").style("fill","#0079c2");
//          }else{
//            //nodo.moveToFront();
//            nodo.attr("activo",1);
//            nodo.attr("class","activo");
            
//            //d3.select(this.parentNode).select("rect").style("fill","#229be4");
              
//          }
//          */

//        update(d);

//    }

//    d3.selection.prototype.moveToFront = function () {
//        return this.each(function () {
//            //this.parentNode.appendChild(this);
//            //        this.parentNode.replaceChild(this, this)
//        });
//    };

//    d3.selection.prototype.moveToBack = function () {
//        return this.each(function () {
//            var firstChild = this.parentNode.firstChild;
//            if (firstChild) {
//                //    this.remove();
//                //this.parentNode.insertBefore(this, firstChild); 
//            }
//        });
//    };

//    function redraw(d) {
//        $(".sliderZoom input").val(d3.event.scale);
//        /*    $( ".texto-nodo" ).each(function() {
//              var width = $( this ).outerWidth();
//              var maximo =  parseInt($(this).css("max-width")); 
//              var minimo = parseInt($(this).css("min-width"));
//              if(minimo > width) width=minimo;
//            var marginLeft = width / 2;
              
//              if(maximo < (width + marginLeft)){
//                width=maximo;   
//              }else{
//                width = width + marginLeft;
//              }
//              marginLeft = width / 2;
//            // set css
//            //$(this).css('margin-left', -marginLeft).css("width", width);
    
//              var position = $(this).offset().top;
//              if (position < 180){
                
//                $(this).parent().css("opacity",0);
//              }else{
//                $(this).parent().css("opacity",1)
//              }
    
    
//          });
//        */

//        /* var delay=2000; //1 seconds
    
//         setTimeout(function(){
//           generarImagen();
//         }, delay); */

//        var escala = 1;
//        var d3scale = d3.event.scale;
//        escala = (d3scale > 1) ? (1.1 / d3scale) : 0.8;
//        if (d3scale == 1) escala = 1;

//        d3.selectAll(".texto").attr("transform", "scale(" + escala + ")");
//        //console.log("here", d3.event.translate, d3.event.scale);
//        svg.attr("transform",
//          "translate(" + d3.event.translate + ")" + " scale(" + d3.event.scale + ")");
//    }

//    // Initialize the display to show a few nodes.
//    //    root.children.forEach(toggleAll);

//    root.children.forEach(collapse);
//    update(root);

//    d3.select(self.frameElement).style("height", "600px").style("width", width);

//    /*    $(".cuadro-nodo-enlaces a").on("click", function(e){
//          window.location = $(this).attr('href');
//           //e.preventDefault();
//           e.stopPropagation();
//           console.log("enlace");
//        });*/

//    /*    $(".node .texto .cuadro-nodo-texto i.fa-info-circle").on("click", function(e){
//          var cuadroEnlaces=$(this).closest("g").parent().find(".cuadro-nodo-enlaces");
          
//          if(cuadroEnlaces.is(':visible')){
//            cuadroEnlaces.hide()  
//          }else{
//            cuadroEnlaces.show()  
//          }
//           e.stopPropagation();
//           console.log("enlace2");
//        });*/
//    //d3.selectAll(".node").call(drag);

//});

   pintar();
}

function pintar() {
    $.ajax({
        url: urlBase + '/Empleado/JsonEmpleado',
        type: 'POST',
        success: function (result) {
            myDiagram.model.nodeDataArray = result;
        }
    });
}


