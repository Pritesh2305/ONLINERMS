jQuery(document).ready(function ($) {

    $(".btnrating1").on('click', (function (e) {

        var previous_value = $("#selected_rating1").val();

        var selected_value = $(this).attr("data-attr");
        $("#selected_rating1").val(selected_value);

        $(".selected-rating1").empty();
        $(".selected-rating1").html(selected_value);

        for (i = 1; i <= selected_value; ++i) {
            $("#rating-star-" + i).toggleClass('btn-warning');
            $("#rating-star-" + i).toggleClass('btn-default');
        }

        for (ix = 1; ix <= previous_value; ++ix) {
            $("#rating-star-" + ix).toggleClass('btn-warning');
            $("#rating-star-" + ix).toggleClass('btn-default');
        }

    }));

    $(".btnrating2").on('click', (function (e) {

        var previous_value = $("#selected_rating2").val();

        var selected_value = $(this).attr("data-attr");
        $("#selected_rating2").val(selected_value);

        $(".selected-rating2").empty();
        $(".selected-rating2").html(selected_value);

        for (i = 1; i <= selected_value; ++i) {
            $("#rating2-star-" + i).toggleClass('btn-warning');
            $("#rating2-star-" + i).toggleClass('btn-default');
        }

        for (ix = 1; ix <= previous_value; ++ix) {
            $("#rating2-star-" + ix).toggleClass('btn-warning');
            $("#rating2-star-" + ix).toggleClass('btn-default');
        }

    }));

    $(".btnrating3").on('click', (function (e) {

        var previous_value = $("#selected_rating3").val();

        var selected_value = $(this).attr("data-attr");
        $("#selected_rating3").val(selected_value);

        $(".selected-rating3").empty();
        $(".selected-rating3").html(selected_value);

        for (i = 1; i <= selected_value; ++i) {
            $("#rating3-star-" + i).toggleClass('btn-warning');
            $("#rating3-star-" + i).toggleClass('btn-default');
        }

        for (ix = 1; ix <= previous_value; ++ix) {
            $("#rating3-star-" + ix).toggleClass('btn-warning');
            $("#rating3-star-" + ix).toggleClass('btn-default');
        }

    }));

    $(".btnrating4").on('click', (function (e) {

        var previous_value = $("#selected_rating4").val();

        var selected_value = $(this).attr("data-attr");
        $("#selected_rating4").val(selected_value);

        $(".selected-rating4").empty();
        $(".selected-rating4").html(selected_value);

        for (i = 1; i <= selected_value; ++i) {
            $("#rating4-star-" + i).toggleClass('btn-warning');
            $("#rating4-star-" + i).toggleClass('btn-default');
        }

        for (ix = 1; ix <= previous_value; ++ix) {
            $("#rating4-star-" + ix).toggleClass('btn-warning');
            $("#rating4-star-" + ix).toggleClass('btn-default');
        }

    }));


});
