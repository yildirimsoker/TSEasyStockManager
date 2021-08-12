$(document).ready(function () {
    $("#btnDeleteImage").click(function () {
        var id = $(this).attr("data-id");

        $.ajax({
            url: '/Product/DeleteImage/' + id,
            type: "POST",
            async: false,
            success: function (data) {
                if (data.IsSucceeded) {
                    toastr.success(data.UserMessage);
                    $("#delete-image-main").hide();
                    $("#Image").val("");
                }
                else {
                    toastr.error(data.UserMessage);
                }
            },
            beforeSend: function () {
                $(this).attr("disabled", true);
                $(this).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');
            },
            complete: function () {
                $(this).html('Delete Image');
            },
        });
    });
});