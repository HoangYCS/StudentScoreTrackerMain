$('.search-container').off('submit').on('submit', function (event) {
    event.preventDefault();
    $('#overlay').show();
    $.ajax({
        type: $(this).attr("method"),
        url: $(this).attr("action"),
        data: $(this).serialize(),
        success: function (response) {
            $('#content-result-score').html(response);
            $('#overlay').hide();
        },
        error: function (xhr, status, error) {
            console.log("Yêu cầu AJAX thất bại");
            console.log("Trạng thái: " + status);
            console.log("Lỗi: " + error);
        }
    });
});