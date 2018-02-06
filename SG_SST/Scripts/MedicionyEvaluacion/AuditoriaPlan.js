//Guardar Plan
$(function () {

    $("#GuardarAuditoria").bind("click", function () {
        var onEventLaunchGuardar = new postGuardar();
        onEventLaunchGuardar.launchGuardar();
    });
});
function postGuardar() {
    this.launchGuardar = function () {

        //Traer datos al modelo JSON

        var stringArray = new Array();
        stringArray[0] = $("#FechaRealizacion").val();
        stringArray[1] = $("#Pk_Id_Proceso").val();
        stringArray[2] = $("#ListaPeriodo option:selected").val();
        stringArray[3] = $("#Objetivo").val();
        stringArray[4] = $("#Alcance").val();
        stringArray[5] = $("#Criterios").val();
        stringArray[6] = $("#Alcance").val();
        stringArray[7] = $("#Auditado").val();
        stringArray[8] = $("#Auditador").val();
        stringArray[9] = $("#Requisito").val();
        stringArray[10] = $("#Duracion").val();
        stringArray[11] = $("#EdicionPrograma").val();

        // Construir objeto JSON
        var EDAuditorias = {
            Periodo: stringArray[2],
            Objetivo: stringArray[3],
            Alcance: stringArray[6],
            Criterios: stringArray[5],
            FechaRealizacion: stringArray[0],
            Auditado: stringArray[7],
            Auditador: stringArray[8],
            Requisito: stringArray[9],
            Duracion: stringArray[10],
            Fk_Id_Programa: stringArray[11],
            Fk_Id_Proceso: stringArray[1]
        };

        PopupPosition();
        $.ajax({
            type: "POST",
            url: '/Auditoria/PlanAuditoria',
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(EDAuditorias),
            success: function (data) {
                OcultarPopupposition();
                $("#val-fecha").css("display", "none");
                $("#val-fecha").text('');
                $("#val-proceso").css("display", "none");
                $("#val-proceso").text('');
                $("#val-objetivo").css("display", "none");
                $("#val-objetivo").text('');
                $("#val-alcance").css("display", "none");
                $("#val-alcance").text('');
                $("#val-criterios").css("display", "none");
                $("#val-criterios").text('');
                $("#val-Auditado").css("display", "none");
                $("#val-Auditado").text('');
                $("#val-Auditor").css("display", "none");
                $("#val-Auditor").text('');
                $("#val-Requisito").css("display", "none");
                $("#val-Requisito").text('');
                $("#val-Duracion").css("display", "none");
                $("#val-Duracion").text('');
                $("#val-periodo").css("display", "none");
                $("#val-periodo").text('');


                if (data.Probar == false) {
                    if (data.Validacion[0] == true) {
                        $("#val-periodo").css("display", "block");
                        $("#val-periodo").text(data.ValidacionStr[0]);
                    }
                    if (data.Validacion[9] == true) {
                        $("#val-fecha").css("display", "block");
                        $("#val-fecha").text(data.ValidacionStr[9]);
                    }
                    if (data.Validacion[1] == true) {
                        $("#val-proceso").css("display", "block");
                        $("#val-proceso").text(data.ValidacionStr[1]);
                    }
                    if (data.Validacion[2] == true) {
                        $("#val-objetivo").css("display", "block");
                        $("#val-objetivo").text(data.ValidacionStr[2]);
                    }
                    if (data.Validacion[3] == true) {
                        $("#val-alcance").css("display", "block");
                        $("#val-alcance").text(data.ValidacionStr[3]);
                    }
                    if (data.Validacion[4] == true) {
                        $("#val-criterios").css("display", "block");
                        $("#val-criterios").text(data.ValidacionStr[4]);
                    }
                    if (data.Validacion[5] == true) {
                        $("#val-Auditado").css("display", "block");
                        $("#val-Auditado").text(data.ValidacionStr[5]);
                    }
                    if (data.Validacion[6] == true) {
                        $("#val-Auditor").css("display", "block");
                        $("#val-Auditor").text(data.ValidacionStr[6]);
                    }
                    if (data.Validacion[7] == true) {
                        $("#val-Requisito").css("display", "block");
                        $("#val-Requisito").text(data.ValidacionStr[7]);
                    }
                    if (data.Validacion[8] == true) {
                        $("#val-Duracion").css("display", "block");
                        $("#val-Duracion").text(data.ValidacionStr[8]);
                    }





                    swal("Advertencia", data.Estado);
                }
                else {
                    swal({
                        title: "La Auditoria se ha creado exitosamente",
                        text: "",
                        type: "success",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "OK",
                        closeOnConfirm: false
                    },
                function () {
                    window.location = data.url;
                });
                }




            },
            error: function (data) {
                OcultarPopupposition();
                console.log(data.Estado)
            }
        });

    }
}
//Fecha Script
$(document).ready(function () {
    ConstruirDatePickerPorElemento('FechaRealizacion');
});
