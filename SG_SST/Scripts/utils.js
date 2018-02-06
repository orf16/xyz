var utils = (function ($) {
    //Variables globales
    //Retardo de tiempo para empezar a escribir
    var SEARCHDELAY = 500;

    //Funcion que retorna la url base del proyecto   
    var getBaseUrl = function () {
        var rootFolder = "";
        switch (document.location.hostname) {
            case 'positiva.adacsc.co':
                rootFolder = '/'; break;           
            case 'localhost':
                showMessageDevelopment();
                rootFolder = ''; break;
            default:  // set whatever you want
        }
        var urlBase = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + rootFolder;
        return urlBase;
    };

    //Mensaje para verificar que navegador esta utilizando el usuario
    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");
    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))  // If Internet Explorer, return Alert
    {
        alert('No todas las funcionalidades están disponibles en este navegador, recomendamos utilizar google chrome. \n \nNot all features are available in this browser, we recommend using google chrome');
    }
 

    ////Mensaje para mostrar cuando es desarrollo
    function showMessageDevelopment()
    {
        if (!$("body").hasClass("is-development")) {
            $("body").addClass("is-development");
            var closeHtml = '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>';
            $("body").prepend('<div class="container text-center"><div class="alert alert-success" role="alert"> ' + closeHtml + ' Ambiente de prueba - Test Environment</div></div>');
        }
    }

    //Funcion que tiene un formato de precio de 3 decimales
    var instanceCurrency = function (element) {
        $(element).inputmask("numeric", {
            radixPoint: ".",
            groupSeparator: "",
            digits: 3,
            autoGroup: true,
            rightAlign: false,
        })
    }

    var showLoading = function(showDuration){
        var loading = $.loading();
        if (showDuration) {
            loading.open(showDuration);
        } else {
            loading.open();
        }
    }

    var closeLoading = function () {
        var loading = $.loading();
        loading.close();
    }

    //Funcion util para mostrar mensajes que desaparecen despues de un tiempo
    //Message: Mensaje a mostrar
    //tipo: success, info, warn, error
    //element: si se desea agregar la notificacion al elemento
    //position: top, botton left, botton right.... etc
    //https://notifyjs.com/
    var showMessage = function (message, tipo, element, position) {

        $(function () {

            if (position == "") {
                position: "bottom left";
            }
            if (element) {
                var options = {
                    clickToHide: true,
                    autoHideDelay: 2500,
                    style: 'bootstrap',
                    position: position,
                    className: tipo
                };
                $(element).notify(message, options);

            } else {
                var options = {
                    clickToHide: true,
                    autoHideDelay: 5000,
                    style: 'bootstrap',
                    position: "bottom right",
                    className: tipo
                };
                $.notify(message, options);
            }
        });

    }

    return {
        getBaseUrl: getBaseUrl,
        instanceCurrency: instanceCurrency,
        showMessage: showMessage,
        showLoading: showLoading,
        closeLoading: closeLoading
    }
})(jQuery);