var category = function () {
    return {
        init: function () {
        },
        validateForm: function () {
            $("#formcategory").validate({
                rules:
            {
                title:
                {
                    required: true
                },
                type:
                {
                    required: true
                }
            },
                messages:
                {
                    title:
                    {
                        required: "Bạn chưa nhập tên danh mục"
                    },
                    type:
                    {
                        required: "Bạn chưa chọn danh mục"
                    }
                }
            });
        }
    };
}();
$(function () { category.init(); });


