$(document).ready(function () {
    $('.btn_est_min').on('click', function (e) {
        var idElemento = $(this).attr('id');
        $.ajax({
            url: 'EvaluacionEstandarMinimo/ObtenerCriteriosPorCiclo',
            data: { idCiclo: idElemento },
            type: 'post'
        }).done(function (response) {
            if (response != null && response.Mensaje == 'OK') {
                $('#container_est_min').empty();
                $('#container_est_min').html(response.Datos);
            }
        }).fail(function (response) {

        });
    });
});