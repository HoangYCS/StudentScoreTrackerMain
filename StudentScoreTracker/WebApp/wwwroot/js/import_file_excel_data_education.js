$(() => {
    $('#ImportDataFromExcel').off('submit').on('submit', function (event) {
        event.preventDefault();
        $('#overlay').show();
        $.ajax({
            type: $(this).attr("method"),
            url: $(this).attr("action"),
            data: new FormData(this),
            processData: false,
            contentType: false,
            success: function (response) {
                $('#overlay').hide();
                alert(response.message);
                location.reload();
            },
            error: function (xhr, status, error) {
                console.log("Yêu cầu AJAX thất bại");
                console.log("Trạng thái: " + status);
                console.log("Lỗi: " + error);
            }
        });
    });
})