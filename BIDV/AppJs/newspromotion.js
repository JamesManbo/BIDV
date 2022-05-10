var newspromotion = function () {
    return {
        init: function () {
        },
        validateForm: function () {
            $("#formnewspromotion").validate({
                ignore: [],
                rules:
            {
                title:
                {
                    required: true
                },
                dateof:
                {
                    required: true
                },
                time_from:
               {
                   required: true
               },
                time_to:
               {
                   required: true
               },
                cat_id:
                  {
                      required: true
                  },
                sort_body:
                 {
                     required: true
                 },
                body:
                {
                    required: function () {
                        CKEDITOR.instances.body.updateElement();
                    }
                }
            },
                messages:
                {
                    title:
                    {
                        required: "Bạn chưa nhập tiêu đề"
                    },
                    dateof:
                    {
                        required: "Bạn chưa nhập ngày xuất bản"
                    },
                    time_from:
                    {
                        required: "Bạn chưa nhập ngày bắt đầu"
                    },
                    time_to:
                    {
                        required: "Bạn chưa nhập ngày ngày kết thúc"
                    },
                    cat_id:
                   {
                       required: "Bạn chưa chọn danh mục"
                   },
                    sort_body:
                 {
                     required: "Bạn chưa nhập mô tả"
                 },
                    body:
          {
              required: "Bạn chưa nhập nội dung tin tức"
          }
                }
            });
        }
    };
}();
$(function () { newspromotion.init(); });


