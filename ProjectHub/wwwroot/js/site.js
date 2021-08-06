// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.js-review-add-button').on('click', function () {

    var content = $('.js-review-content').val();

    $.get('/Reviews/' + action, { content: content },
        function (data) {
            console.log(data);
            $('.js-reviews-container').html(data);
        });

});