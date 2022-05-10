var goldpromotion = function () {
    return {
        init: function () { },
        validateForm: function () {
            $("#formgoldpromotion").validate({
            
                rules:
                    {
                        title:
                        {
                            required: true
                        },
                        cat_id:
                        {
                            required: true
                        },
                        promo_per:
                        {
                            required: true
                        },
                        time_from:
                        {
                            required: true
                        }, time_to:
                        {
                            required: true
                        },
                        location:
                        {
                            required: function (textarea) {
                                CKEDITOR.instances[textarea.id].updateElement(); // update textarea
                                var editorcontent = textarea.value.replace(/<[^>]*>/gi, ''); // strip tags
                                return editorcontent.length === 0;
                            }
                        },
                        sort:
                        {
                            required: true
                        },
                        body:
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
                    cat_id:
                    {
                        required: "Bạn chưa chọn danh mục"
                    },
                    promo_per:
                    {
                        required: "Bạn chưa nhập tỉ lệ giảm giá"
                    },
                    time_from:
                    {
                        required: "Bạn chưa nhập thời gian bắt đầu"
                    },
                    time_to:
                    {
                        required: "Bạn chưa nhập thời gian kết thúc"
                    },
                    location:
                    {
                        required: "Bạn chưa nhập địa điểm"
                    },
                    sort:
                    {
                        required: "Bạn chưa nhập mô tả"
                    },
                    body:
                    {
                        required: "Bạn chưa nhập nội dung"
                    }
                }
            });
        }
    }
}();
$(function () { goldpromotion.init(); });