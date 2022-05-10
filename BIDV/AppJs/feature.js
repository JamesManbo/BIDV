var feature = function () {
    return {
        init: function () { },
        validateForm: function () {
            $("#formfeature").validate({
                rules: {
                    title:
                    {
                        required: true
                    },
                    link:
                    {
                        required: true
                    },
                    url_show:
                    {
                        required: true
                    }
                },
                messages: {
                    title:
                    {
                        required: "Bạn chưa nhập tiêu đề"
                    },
                    link:
                    {
                        required: "Bạn chưa nhập link liên kết"
                    },
                    url_show:
                    {
                        required: "Bạn chưa chọn nơi hiển thị"
                    }
                }

            });
        }
    }
}();
$(function () { feature.init(); });