/* =========================================================
 * dpto-mcpio.js
 * Genérico para manejar combos de departamento/municipio.

 * Prerequisito: La lista de los departamentos:
    <datalist id="dl-departamentos">
        @foreach (var item in ListaBasicas.Departamentos)
        {
            <option data-value="@item.Pk_Id_Departamento" value="@item.Nombre_Departamento"/>
        }
    </datalist>
 
 * Ejemplo de construcción del HTML
    <div class="col-xs-12 col-sm-6 col-md-4 linea02">
        <h2>Departamento</h2>
        <input list="dl-departamentos" // Esta lista es única para todos los departamentos.
                id="persona-departamento"
                name="persona-departamento"
                data-lista="data-departamentos" // Todos los combos de dpto tienen este mismo atributo/valor.
                data-lista-id="persona-departamento-id" // El hidden que almacenará el id seleccionado.
                data-lista-municipios="dl-persona-municipios" // Este el el input[list] de los municipios.
                data-lista-municipio-id="persona-municipio-id" /> // Este es el hidden que guarada el id del municipio.
        <input type="hidden" id="persona-departamento-id" name="persona-departamento-id" />
    </div>
    <div class="col-xs-12 col-sm-6 col-md-4 linea02">
        <h2>Municipio</h2>
        <input list="dl-persona-municipios"
                id="persona-municipio"
                name="persona-municipio"
                data-lista="data-municipios" // Todos los combos de dpto tienen este mismo atributo/valor.
                data-lista-id="persona-municipio-id" // Este es el hidden que guarada el id del municipio.
                data-lista-valores="dl-persona-municipios" /> // Este es el datalist con los municipios.
        <input type="hidden" id="persona-municipio-id" name="persona-municipio-id" />
        <datalist id="dl-persona-municipios" name="dl-persona-municipios" />
    </div>
 * ========================================================= */
var urlObtenerMunicipios = '/Incidente/ObtenerMunicipios'

$(document).ready(function () {

    // Listas pares departamento/municipio.
    var listas_deptos = $('[data-lista="data-departamentos"]');
    listas_deptos.on('input propertychange', function () {
        var current = $(this);
        var selectedItem = current.val();
        var selectedValue = $("#dl-departamentos").find('option[value="' + selectedItem + '"]').attr("data-value");
        var tagMunicipios = $("#" + current.attr('data-lista-municipios')).first();
        var tagMunicipioId = $("#" + current.attr("data-lista-municipio-id"));
        var tagMunicipioTexto = $("input[list='" + current.attr("data-lista-municipios") + "']");

        if (selectedValue == null || selectedValue === '') {
            tagMunicipios.empty();
            tagMunicipioId.val('');
            tagMunicipioTexto.val('');
            return;
        }

        $("#" + current.attr("data-lista-id")).first().val(selectedValue);
        $.ajax({
            type: "POST",
            data: { departamentoid: selectedValue },
            url: urlObtenerMunicipios
        }).done(function (response) {
            if (response != undefined && response != '' && response.Mensaje == 'Success') {
                tagMunicipios.html(response.Data);
            }
            else if (response != undefined && response.Data == '' && response.Mensaje == 'Fail') {
                console.log("Error. No se obtuvo respuesta para el departamento: " + selectedValue);
                tagMunicipios.empty();
            }
            tagMunicipioId.val('');
            tagMunicipioTexto.val('');
        }).fail(function (response) {
            console.log("Error en la peticion: " + response.Data);
        });
    });

    var lista_municipios = $('[data-lista="data-municipios"]');
    lista_municipios.on('input propertychange', function () {
        var selectedItem = $(this).val();
        var selectedValue = $("#" + $(this).attr("data-lista-valores")).find('option[value="' + selectedItem + '"]').attr("data-value");
        var itemId = $("#" + $(this).attr("data-lista-id"));
        if (selectedValue === '')
            itemId.val('');
        else
            itemId.val(selectedValue);
    });

    // Listas simples de datos.
    var listas_simples = $('[data-lista="data-lista-simple"]');
    listas_simples.on('input propertychange', function () {
        var current = $(this);
        var selectedItem = current.val();
        var selectedValue = $("#" + $(this).attr('list')).find('option[value="' + selectedItem + '"]').attr("data-value");
        var itemId = $("#" + $(this).attr("data-lista-id"));

        if (selectedValue === '')
            itemId.val('');
        else
            itemId.val(selectedValue);
    });


});