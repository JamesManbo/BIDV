var qa = function () {
    return {
        init: function () {
        },
        validateForm: function () {
            $("#formqa").validate({
                rules:
            {
                cat_id:
                {
                    required: true
                },
                question:
                {
                    required: true
                }
            },
                messages:
                {
                    cat_id:
                    {
                        required: "Bạn chưa chọn danh mục"
                    },
                    question:
                    {
                        required: "Bạn chưa nhập câu hỏi"
                    }
                }
            });
        }
    };
}();
$(function () { qa.init(); });


