var slide = function () {
    return {
        init: function () {
        },
        validateForm: function () {
            $("#formslide").validate({
                rules:
            {
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
                messages:
                {
                    title:
                    {
                        required: "Bạn chưa nhập tiêu đề"
                    },
                    link:
                    {
                        required: "Bạn chưa nhập đường dẫn"
                    },
                    url_show:
                    {
                        required:"Bạn chưa chọn nơi hiển thị"
                    }
                }
            });
        }
    };
}();
$(function () { slide.init(); });


