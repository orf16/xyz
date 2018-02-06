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
            $("textarea#ProblemaTxt").text('');
            var totalChars = 100;
            var countTextBox = $('#ProblemaTxt')
            var charsCounter = $('#CharContadorProb');
            charsCounter.text(totalChars);
            var myLength = $("#ProblemaTxt").val().length;
            var quedan = totalChars - myLength;
            $("#CharContadorProb").text(quedan.toString());
        }
    });
 
//Añadir Nodo Problema

    $(function () {
        $("#AgregarProblema").click(function () {
            $("#val-agregar-problema").css("display", "none");
            $("#val-agregar-problema").text('');
            $("#val-agregar-opcion").css("display", "none");
            $("#val-agregar-opcion").text('');
            $("#val-agregar-eliminar").css("display", "none");
            $("#val-agregar-eliminar").text('');
            $("#div_novedad_ult").removeClass("alert alert-info alert-warning alert-danger alert-success")
            $("#msj_novedad_ult").text('');
            localStorage.removeItem('Nombre');
            var userInfo = $("#userInfoJson1").val();
            var stringArray = new Array();
            stringArray[0] = $("#ProblemaTxt").val();
            stringArray[1] = userInfo;
            var postData = { values: stringArray };

            $.ajax({
                type: "POST",
                url: "/Acciones/AgregarNodoPadreLluvia",
                data: postData,
                success: function (data) {
                    if (data.probar == false) {
                        $("#val-agregar-problema").text(data.resultado);
                        $("#val-agregar-problema").css("display", "block");
                    }
                    else {
                        localStorage.setItem('Nombre', data.resultado);
                        localStorage.setItem('Problema', data.resultado1);
                        $(document).ajaxStop(function () {  location.reload(true); });
                    }
                },
                dataType: "json",
                traditional: true
            });

        });
    });
 
//Añadir Nodo Hijo

        $(function () {
            $("#AgregarOpcion").click(function () {
                $("#val-agregar-problema").css("display", "none");
                $("#val-agregar-problema").text('');
                $("#val-agregar-opcion").css("display", "none");
                $("#val-agregar-opcion").text('');
                $("#val-agregar-eliminar").css("display", "none");
                $("#val-agregar-eliminar").text('');
                $("#div_novedad_ult").removeClass("alert alert-info alert-warning alert-danger alert-success")
                $("#msj_novedad_ult").text('');
                localStorage.removeItem('Nombre');
                var userInfo = $("#userInfoJson1").val();
                var stringArray = new Array();
                stringArray[0] = $("#PadreTxt").val();
                stringArray[1] = $("#ValorTxt").val();
                stringArray[2] = userInfo;
                var postData = { values: stringArray };
                $.ajax({
                    type: "POST",
                    url: "/Acciones/AgregarNodoHijoLluvia",
                    data: postData,
                    success: function (data) {

                        if (data.probar == false) {
                            $("#val-agregar-opcion").text(data.resultado);
                            $("#val-agregar-opcion").css("display", "block");
                        }
                        else {
                            localStorage.setItem('Nombre', data.resultado);
                            $(document).ajaxStop(function () { location.reload(true); });
                        }
                    },
                    dataType: "json",
                    traditional: true
                });

            });
        });
 
//Seleccionar Nodo
    $(function () {
        $("#val-agregar-problema").css("display", "none");
        $("#val-agregar-problema").text('');
        $("#val-agregar-opcion").css("display", "none");
        $("#val-agregar-opcion").text('');
        $("#val-agregar-eliminar").css("display", "none");
        $("#val-agregar-eliminar").text('');
        localStorage.removeItem('Nombre');
        $(".TreeviewCont ul li a").click(function () {

            $(".TreeviewCont ul li a").each(function () {
                $this = $(this);
                var parent = $(this).parent();
                parent.find('span').each(function () {
                    $this = $(this);
                    $this.empty();
                });
                $(this).css({ "background": "none" });
            });

            //reestrablecer otras opciones
            $(this).css({ "background": "#d8dcde" });
            var parent1 = $(this).parent();
            var idA = $(this).attr('id');
            var id_icon = "#" + idA + "elim";
            var icon_inner = ' <i  class="glyphicon glyphicon-trash quitarelementoanalisis" name="' + idA + '" style="color:#513fa2" title="Eliminar Elemento" onclick="FuncionBorrar(' + idA + ')"></i>';
            var icon = $(id_icon);
            icon.append(icon_inner);
            $("#PadreTxt").val($(this).attr('id'));
            $("#OpcionSelTxt").val($(this).text());

        });
    });

//Control funciones basicas del treeview
    $(document).ready(function () {
        $(".TreeviewCont ul").each(function () {
            $this = $(this);
            $this.find("li").has("ul").addClass("hasSubmenu");
        });
        $(".TreeviewCont li:last-child").each(function () {
            $this = $(this);
            if ($this.children('ul').length === 0) {
                $this.closest('ul').css("border-left", "1px solid #48388f");
            } else {
                $this.closest('ul').children("li").not(":last").css("border-left", "1px solid #48388f");
                $this.closest('ul').children("li").last().children("a").addClass("addBorderBefore");
                $this.closest('ul').css("margin-top", "20px");
                $this.closest('ul').find("li").children("ul").css("margin-top", "20px");
            };
        });
        $(".TreeviewCont ul li").each(function () {
            $this = $(this);
            $this.mouseenter(function () {
                $(this).children("a").css({ "font-weight": "bold", "color": "#48388f" });
            });
            $this.mouseleave(function () {
                $(this).children("a").css({ "font-weight": "normal", "color": "#513fa2" });
            });
        });
        $(".TreeviewCont ul li.hasSubmenu").each(function () {
            $this = $(this);
            $this.prepend("<a href='#'><i class='fa fa-minus-circle'></i><i style='display:none;' class='fa fa-plus-circle'></i></a>");
            $this.children("a").not(":last").removeClass().addClass("toogle");
        });
        $(".TreeviewCont ul li.hasSubmenu a.toogle").click(function () {
            $this = $(this);
            $this.closest("li").children("ul").toggle("slow");
            $this.children("i").toggle();
            return false;
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

    $(function() {
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
                function() {
                    var userInfo = $("#userInfoJson").val();
                    var userInfo1 = $("#userInfoJson1").val();
                    var stringArray = new Array();
                    stringArray[0] = userInfo;
                    stringArray[1] = userInfo1;
                    var postData = { values: stringArray };

                    $.ajax({
                        type: "POST",
                        url: "/Acciones/GuardarAnLluvia",
                        data: postData,
                        success: function(data) {
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

    $(function() {
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
                function() {
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



    //EliminarNodo(NUEVO)
    function FuncionBorrar(Idelemento) {

        $("#val-agregar-problema").css("display", "none");
        $("#val-agregar-problema").text('');
        $("#val-agregar-opcion").css("display", "none");
        $("#val-agregar-opcion").text('');
        $("#val-agregar-eliminar").css("display", "none");
        $("#val-agregar-eliminar").text('');
        $("#div_novedad_ult").removeClass("alert alert-info alert-warning alert-danger alert-success");
        $("#msj_novedad_ult").text('');
        localStorage.removeItem('Nombre');
        swal({
            title: "Advertencia",
            text: "Esta seguro que desea eliminar esta idea?",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Si",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        },
         function () {
             var userInfo = $("#userInfoJson1").val();
             var stringArray = new Array();
             stringArray[0] = Idelemento;
             stringArray[1] = userInfo;
             var postData = { values: stringArray };
             $.ajax({
                 type: "POST",
                 url: "/Acciones/EliminarNodoHijoLluvia",
                 data: postData,
                 success: function (data) {
                     if (data.probar == false) {
                         $("#msj_novedad").text(data.resultado);
                         $(".divMensajes").removeClass("alert alert-info alert-warning alert-danger alert-success").addClass("alert alert-warning");
                         swal("", data.resultado, "error");
                     }
                     else {
                         localStorage.setItem('Nombre', data.resultado);
                         $(document).ajaxStop(function () { location.reload(true); });
                     }
                 },
                 dataType: "json",
                 traditional: true
             });
         });
    }
 