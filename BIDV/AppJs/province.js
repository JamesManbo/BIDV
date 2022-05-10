var province = function () {
    return {
        init: function () { },
        validateForm: function () {
            $("#formprovince").validate({
                rules: {
                    title:
                    {
                        required: true
                    },
                    is_city:
                    {
                        required: true
                    },
                    status:
                    {
                        required: true
                    }
                },
                messages: {
                    title:
                    {
                        required: "Bạn chưa nhập tên tỉnh thành "
                    },
                    is_city:
                    {
                        required: "Bạn chưa chọn cấp "
                    },
                    status:
                    {
                        required: "Bạn chưa chọn trạng thái "
                    }
                }

            });
        }
    }
}();
$(function () { province.init(); });