var card = function () {
    return {
        init: function () {
        },
        validateform: function () {
            $("#formcard").validate({
                rules:
                {
                    title:
                    {
                        required: true
                    },
                    type_id:
                    {
                        required: true
                    }
                   
                },
                messages:
                {
                    title:
                    {
                        required: "Bạn chưa nhập tên thẻ"
                    },
                    type_id:
                 {
                     required: "Bạn chưa chọn loại thẻ"
                 }
                }
            });
        },
        onLoadGroupCard: function (id) {
            $.get("/AdminCardService/GroupCard?catgoryId=" + id, function (res) {
                var strHtml = "<option>---Chọn nhóm---</option>";
                for (var i = 0; i < res.length; i++) {
                    strHtml += "<option value='" + res[i].id + "' >" + res[i].title + "</option>";
                }
                $("#pid").html(strHtml);
            });
        },
        onLoadAddSlogan: function (cardId) {
            $.get("/AdminCardService/AddSlogan?cardId=" + cardId, function (res) {
                $("#frmSlogan").html(res);
            });
        },
        onAddSloganSuccess: function (res) {
            $("#frmSlogan").html(res);
        },
        onDeleteSlogan: function (id) {
            bootbox.confirm({
                message: "Bạn có muốn xóa không?",
                buttons: {
                    confirm: {
                        label: 'Có',
                        className: 'btn-success'
                    },
                    cancel: {
                        label: 'Không',
                        className: 'btn-danger'
                    }
                },
                callback: function (result) {
                    if (result) {
                        $.get("/AdminCardService/DeleteSlogan", { id: id }).done(function (res) {
                            $("#frmSlogan").html(res);
                        });
                    }
                }
            });
        },
        onEditSlogan: function (id) {
            $.get("/AdminCardService/EditSlogan?id=" + id, function (res) {
                $("#frmSlogan").html(res);
            });
        },
        onEditSloganSuccess: function (res) {
            $("#frmSlogan").html(res);
        }
    };
}();
$(function () { card.init(); });


