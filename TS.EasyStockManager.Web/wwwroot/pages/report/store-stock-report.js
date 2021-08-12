
$(document).ready(function () {
    var datatable = $('#datatable').dataTable({
        "dom": 'lBfrtip',
        "searching": false,
        "iDisplayLength": 10,
        "ordering": false,
        "bServerSide": true,
        "processing": true,
        "paging": true,
        "sAjaxSource": "/Report/StoreStockList",
        "info": true,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            aoData.push(
                { "name": "returnformat", "value": "plain" },
                { "name": "StoreId", "value": $('select[name="StoreId"]').val() },
                { "name": "ProductId", "value": $('select[name="ProductId"]').val() }
            );
            $.ajax({
                "dataType": 'json',
                "type": "GET",
                "url": sSource,
                "data": aoData,
                "success": function (data) {
                    if (data.IsSucceeded == true) {
                        fnCallback(data);
                    }
                    else {
                        toastr.error(data.UserMessage);
                    }
                }
            });
        },
        aoColumns:
            [
                {
                    mDataProp: "StoreFullName"
                },
                {
                    mDataProp: "ProductFullName"
                },
                {
                    mDataProp: "QTY"
                }
            ]
    });

    $("#btnFilter").click(function () {
        datatable.fnFilter();
    });

    $("#btnClear").click(function () {
        $('div.dataTable-search-form').clearForm();
        $("#ProductId").val(null).trigger('change');
        datatable.fnFilter();
    });


    $('.enter-keyup').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            datatable.fnFilter();
        }
    });

    $("#ProductId").select2({
        minimumInputLength: 1,
        ajax: {
            url: '/Report/GetProduct',
            dataType: 'json',
            type: "GET",
            delay: 500,
            data: function (params) {
                return {
                    search: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.Text,
                            id: item.Value
                        }
                    })
                };
            }
        }
    });

});





