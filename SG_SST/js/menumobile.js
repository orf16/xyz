$(document).ready(main);

var contador = 1;

function main() {
    $('.botonmobile').click(function () {
        // $('nav').toggle(); 

        if (contador == 1) {
            $('#sidebar-wrapper').animate({
                left: '0'
            });
            contador = 0;
        } else {
            contador = 1;
            $('#sidebar-wrapper').animate({
                left: '-100%'
            });
        }

    });

};