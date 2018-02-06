$(document).ready(function () {


    $('html').click(function () {
        $(".sub-menu").each(function () { $(this).hide(); });
    });

    $(window).resize(function () {
        if ($("#wrapper").width() < 750) {
            $(".menu-text").each(function () { $(this).show(); });
            $("#sidebar").css('width', '270px');
            $("#sidebar-wrapper").css('min-width', '270px');
            $(".sub-menu").each(function () { $(this).css('left', '270px'); });
            $(".open-close-sidebar").css('left', '270px');
        }
    });

    $(".orden1").on("click", function (e) {
        e.stopPropagation();
        //if($("#wrapper").width() > 750){
        $(".sub-sub-menu").each(function () { $(this).hide(); });
        if ($("." + $(this).attr("id")).is(":visible")) {
            $("." + $(this).attr("id")).hide(400);
            $(".sub-menu").each(function () { $(this).hide(); });

        } else {
            $(".sub-menu").each(function () { $(this).hide(); });
            $("." + $(this).attr("id")).show(400);
        }
        //}
    });



    $("#button-menu").on("click", function () {
        $(".sub-sub-menu").each(function () { $(this).hide(); });
        if ($(".menu-text").is(":visible")) {
            $(".menu-text").each(function () { $(this).hide(); });
            $("#sidebar").css('width', '82px');
            $("#sidebar-wrapper").css('min-width', '82px');
            $(".sub-menu").each(function () { $(this).css('left', '82px'); });
            $(".open-close-sidebar").css('left', '82px');
            $(this).html("<i class='fa fa-arrow-right' ></i>");
        } else {
            $(".menu-text").each(function () { $(this).show(); });
            $("#sidebar").css('width', '270px');
            $("#sidebar-wrapper").css('min-width', '270px');
            $(".sub-menu").each(function () { $(this).css('left', '270px'); });
            $(".open-close-sidebar").css('left', '270px');
            $(this).html("<i class='fa fa-arrow-left' ></i>");
        }
    });

    $(".sub-menu-item").on("click", function (e) {
        var a = $(this);
        var li = a.parent();
        if (li.find("ul").length) {
            e.stopPropagation();
            var ul = li.find("ul");
            $(".sub-sub-menu").each(function () { $(this).hide(); });
            ul.show(400);
        }
    });

    //swal({
    //title: "<small>Hola, esto es una mensaje</small>",
    //text: "Permitimos <span style='color:#F8BB86'>html<span> personalizado.",
    //html: true
    //});
});


