//Mostrar Resultado
    $(document).ready(function () {
        var nombre = localStorage.getItem('Nombre');
        var problema = localStorage.getItem('Problema');
        if (nombre != null) {
            $("#msj_novedad_ult").text(nombre);
            $("#div_novedad_ult").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-success");
        }
        else {
            $("#msj_novedad_ult").text('');
            $("#div_novedad_ult").removeClass("alert alert-info alert-warning alert-danger alert-success")
        }

        if (problema != null) {
            $("textarea#ProblemaTxt").text(problema.toString());
            var totalChars = 100;
            var countTextBox = $('#ProblemaTxt')
            var charsCounter = $('#CharContadorProb');
            charsCounter.text(totalChars);
            var myLength = $("#ProblemaTxt").val().length;
            var quedan = totalChars - myLength;
            $("#CharContadorProb").text(quedan.toString());
        }
        else {
            var totalChars = 100;
            var countTextBox = $('#ProblemaTxt')
            var charsCounter = $('#CharContadorProb');
            charsCounter.text(totalChars);
            var myLength = $("#ProblemaTxt").val().length;
            var quedan = totalChars - myLength;
            $("#CharContadorProb").text(quedan.toString());
        }

    });
 
//Generar/visualizar 
 
    $(function () {
        $("#Generar").click(function () {
            var userInfo = $("#userInfoJson1").val();
            var stringArray = new Array();
            stringArray[0] = $("#ProblemaTxt").val();

            var x = 1;
            for (var i = 1; i < 6; i++) {
                var str1 = i.toString();
                for (var i1 = 1; i1 < 8; i1++) {

                    var str2 = i1.toString();
                    var str3 = "#" + str1 + str2;

                    stringArray[x] = $(str3).val();
                    x = x + 1;
                }
            }
            stringArray[x] = userInfo;
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/Generar5Porque",
                data: postData,
                success: function (data) {
                    if (data.probar == false) {
                        $("#msj_novedad_ult").text(data.resultado);
                        $("#div_novedad_ult").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                    }
                    else {
                        $(document).ajaxStop(function () {
                            localStorage.setItem('Nombre', data.resultado);
                            location.reload(true);
                        });

                    }
                },
                dataType: "json",
                traditional: true
            });
        });
    });
 
//Seleccionar Control de caracteres por entrada 

    $(document).ready(function () {
        var totalChars = 100;
        $('#ProblemaTxt').keyup(function () {
            var countTextBox = $('#ProblemaTxt');
            var charsCounter = $('#CharContadorProb');
            charsCounter.text(totalChars);
            var charsInTextArea = this.value.length;
            if (charsInTextArea > totalChars) {
                var removeCharacters = (charsInTextArea - totalChars);
                this.value = this.value.substring(0, totalChars);
            }
            else {
                charsCounter.text(totalChars - charsInTextArea);
            }
        });
    });
 

    $(document).ready(function () {
        var totalChars = 100;
        var countTextBox = $('#ValorTxt')
        var charsCounter = $('#CharContadorOpc');
        charsCounter.text(totalChars);
        countTextBox.keyup(function () {
            var charsInTextArea = this.value.length;
            if (charsInTextArea > totalChars) {
                var removeCharacters = (charsInTextArea - totalChars);
                this.value = this.value.substring(0, totalChars);
            }
            else {
                charsCounter.text(totalChars - charsInTextArea);
            }
        });
    });
 
//Guardar y retornar a vista principal 
 
    $(function () {
        $("#Guardar").click(function () {
            localStorage.removeItem('Nombre');
            $("#val-agregar-problema").css("display", "none");
            $("#val-agregar-problema").text('');
            $("#val-agregar-opcion").css("display", "none");
            $("#val-agregar-opcion").text('');
            $("#val-agregar-eliminar").css("display", "none");
            $("#val-agregar-eliminar").text('');
            $("#div_novedad_ult").removeClass("alert alert-info alert-warning alert-danger alert-success")
            $("#msj_novedad_ult").text('');
            swal({
                title: "Advertencia",
                text: "Esta operación puede sobrescribir un análisis ya guardado, esta seguro que desea guardar este analisis? ",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Si",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false
            },
                function () {
                    var userInfo = $("#userInfoJson").val();
                    var userInfo1 = $("#userInfoJson1").val();
                    var stringArray = new Array();
                    stringArray[0] = $("#ProblemaTxt").val();

                    var x = 1;
                    for (var i = 1; i < 6; i++) {
                        var str1 = i.toString();
                        for (var i1 = 1; i1 < 8; i1++) {

                            var str2 = i1.toString();
                            var str3 = "#" + str1 + str2;

                            stringArray[x] = $(str3).val();
                            x = x + 1;
                        }
                    }
                    stringArray[x] = userInfo;
                    stringArray[x + 1] = userInfo1;
                    var postData = { values: stringArray };





                    $.ajax({
                        type: "POST",
                        url: "/Acciones/GuardarAn5Porque1",
                        data: postData,
                        success: function (data) {

                            if (data.probar == true) {

                                swal({
                                    title: "",
                                    text: data.mensaje,
                                    type: "success",
                                    showCancelButton: false,
                                    confirmButtonColor: "#DD6B55",
                                    confirmButtonText: "Ok",
                                    closeOnConfirm: false
                                },
                                    function () {
                                        localStorage.clear();
                                        window.location = data.url;
                                    });
                            } else {
                                swal("No se pudo completar la operación", data.mensaje, "error");
                            }
                        },
                        dataType: "json",
                        traditional: true
                    });
                });
        });
    });
 
//Cancelar y retornar a vista principal 
 
    $(function () {
        $("#Cancelar").click(function () {
            localStorage.removeItem('Nombre');
            $("#val-agregar-problema").css("display", "none");
            $("#val-agregar-problema").text('');
            $("#val-agregar-opcion").css("display", "none");
            $("#val-agregar-opcion").text('');
            $("#val-agregar-eliminar").css("display", "none");
            $("#val-agregar-eliminar").text('');
            $("#div_novedad_ult").removeClass("alert alert-info alert-warning alert-danger alert-success")
            $("#msj_novedad_ult").text('');
            swal({
                title: "Advertencia",
                text: "Esta seguro que desea cancelar esta operación?",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Si",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false
            },
                function () {
                    var userInfo = $("#userInfoJson").val();
                    var userInfo1 = $("#userInfoJson1").val();
                    var stringArray = new Array();
                    stringArray[0] = userInfo;
                    stringArray[1] = userInfo1;
                    var postData = { values: stringArray };
                    $.ajax({
                        type: "POST",
                        url: "/Acciones/CancelarAnalisis",
                        data: postData,
                        success: function (data) {
                            localStorage.clear();
                            window.location = data.url;
                        },
                        dataType: "json",
                        traditional: true
                    });
                });
        });
    });
 
//Modal Image 
    $(document).ready(function () {
        var modal = document.getElementById('myModal');
        var img = document.getElementById('myImg');
        var botonVer = document.getElementById('ImagenAnalisis');

        var modalImg = document.getElementById("img01");
        var captionText = document.getElementById("caption");
        botonVer.onclick = function () {
            modal.style.display = "block";
            modalImg.src = img.src;
        }
        var span = document.getElementById("close");
        span.onclick = function () {
            modal.style.display = "none";
        }
        document.addEventListener('keyup', function (e) {
            if (e.keyCode == 27) {
                modal.style.display = "none";
            }
        });
    });
 