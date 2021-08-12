
$(document).ready(function () {
    var datatable = $('#datatable').dataTable({
        "searching": false,
        "iDisplayLength": 10,
        "ordering": false,
        "lengthChange": false,
        "bServerSide": true,
        "processing": true,
        "paging": true,
        "sAjaxSource": "/Transaction/List",
        "info": true,
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            aoData.push(
                { "name": "returnformat", "value": "plain" },
                { "name": "TransactionCode", "value": $('input[name="TransactionCode"]').val() },
                { "name": "SearchStartDate", "value": $('input[name="SearchStartDate"]').val() },
                { "name": "SearchEndDate", "value": $('input[name="SearchEndDate"]').val() },
                { "name": "TransactionTypeId", "value": $('select[name="TransactionTypeId"]').val() },
                { "name": "StoreId", "value": $('select[name="StoreId"]').val() },
                { "name": "ToStoreId", "value": $('select[name="ToStoreId"]').val() }
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
                    mDataProp: "TransactionCode"
                },
                {
                    mDataProp: "TransactionTypeName"
                },
                {
                    mDataProp: "TransactionDate"
                },
                {
                    mDataProp: "StoreName"
                },
                {
                    mDataProp: "ToStoreName"
                },
                {
                    "sDefaultContent": "",
                    "bSortable": false,
                    "mRender": function (data, type, row) {
                        var buttons = "";
                        buttons += '<a onclick="detailShow(this,' + row.Id + ')"  class="btn btn-xs btn-default"><i class="fas fa-list"></i> Detail</a>&nbsp;'
                        buttons += '<a href="/Transaction/Edit/' + row.Id + '?typeId=' + row.TransactionTypeId + '" class="btn btn-xs btn-warning"><i class="fas fa-pen"></i> Edit</a>&nbsp;'
                        buttons += '<a onclick="deleteRow(this,' + row.Id + ')"  class="btn btn-xs btn-danger"><i class="fas fa-trash"></i> Delete</a>'
                        return buttons;
                    }
                }
            ]
    });

    $("#btnFilter").click(function () {
        datatable.fnFilter();
    });

    $("#btnClear").click(function () {
        $('div.dataTable-search-form').clearForm();
        datatable.fnFilter();
    });


    $('.enter-keyup').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            datatable.fnFilter();
        }
    });

});

function deleteRow(row, id) {

    $.ajax({
        url: '/Transaction/Delete/' + id,
        type: "POST",
        async: false,
        success: function (data) {
            if (data.IsSucceeded) {
                var aPos = $('#datatable').dataTable().fnGetPosition(row);
                $('#datatable').dataTable().fnDeleteRow(aPos);
                toastr.success(data.UserMessage);
            }
            else {
                toastr.error(data.UserMessage);
            }
        }
    });
}

function detailShow(row, id) {
    $("#tbl-transaction-detail tbody").empty();
    var str = '';
    $.ajax({
        url: '/Transaction/GetTransactionDetail/' + id,
        type: "GET",
        success: function (data) {
            if (data.IsSucceeded) {
                $.each(data.Data, function (i, item) {
                    str += '<tr>';
                    str += '<td>' + item.Barcode + "-" + item.ProductName + ' </td>';
                    str += '<td>' + item.Amount + ' </td>';
                    str += '<td>' + item.UnitOfMeasureName + "-" + item.UnitOfMeasureShortName + ' </td>';
                    str += '</tr>';
                });
                $("#tbl-transaction-detail tbody").append(str);
            }
            else {
                toastr.error(data.UserMessage);
            }
        }
    });

    $('#modal-detail').modal('show');
}

