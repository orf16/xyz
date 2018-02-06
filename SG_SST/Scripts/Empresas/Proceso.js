var urlBase = ""
try {
    urlBase = utils.getBaseUrl();
} catch (e) {
    console.error(e.message);
    throw new Error("El modulo utilidades es requerido");
};

window.onload = function init() {
    var $ = go.GraphObject.make;  // for conciseness in defining templates
    myDiagram = new go.Diagram("ProcesosPositiva");
    myDiagram.initialAutoScale = go.Diagram.Uniform,
    myDiagram.initialContentAlignment = go.Spot.Left;
    myDiagram.model = go.GraphObject.make(go.TreeModel,
         {
             nodeKeyProperty: "ProcesoPadre",
             nodeParentKeyProperty: "ProcesoHijo",

         });

    myDiagram.layout =
$(go.TreeLayout, { angle: 0, nodeSpacing: 25 });


    // define the Link template

    myDiagram.linkTemplateMap.add = "Comment",


      $(go.Link, go.Link.Orthogonal,
      {
          curve: go.Link.Bezier, adjusting: go.Link.Stretch,
          corner: 2,
          relinkableFrom: true, relinkableTo: true, reshapable: true
      },
       $(go.Shape, {
           strokeWidth: 1,
           stroke: "orange",
           toArrow: "standard"
       }));


    myDiagram.nodeTemplate =
$(go.Node, "Spot", go.Panel.Auto,
$(go.Shape, "RoundedRectangle",
{

    width: 240, height: 60,
    strokeWidth: 1,
    figure: "RoundedRectangle",
    fill: "#7E8A97",
    stroke: "#ff7500",

}),
$(go.Panel, go.Panel.Horizontal,
$(go.TextBlock,
{ font: "bold 15px oon, Bold Arial, sans-serif", stroke: "white", margin: 1, opacity: 0.95, },
new go.Binding("text", "Descripcion_Proceso").makeTwoWay())
)



);
    pintar();
}

function pintar() {
    $.ajax({
        url: urlBase + '/Proceso/JsonProceso',
        type: 'POST',
        success: function (result) {
            myDiagram.model.nodeDataArray = result;
        }
    });
}