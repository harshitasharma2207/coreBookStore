$(document).ready(function () {

    $(".pushme").click(function () {
        $(this).text("DON'T PUSH ME");
    });

    $(".pushme-with-color").click(function () {
        $(this).text("DON'T PUSH ME");
        $(this).addClass("btn-danger");
        $(this).removeClass("btn-warning");
    });

    $(".with-color").click(function () {
        if ($(this).hasClass("btn-warning")) {
            $(this).addClass("btn-danger");
            $(this).removeClass("btn-warning");
        }
        else {
            $(this).addClass("btn-warning");
            $(this).removeClass("btn-danger");
        }
    });

    $(".pushme2").click(function () {
        $(this).text(function (i, v) {
            return v === 'PUSH ME' ? 'PULL ME' : 'PUSH ME'
        });
    });
});