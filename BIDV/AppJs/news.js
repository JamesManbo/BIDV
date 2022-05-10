var news = function () {
    return {
        init: function () {
        },
        validateForm: function () {
            $("#formnews").validate({
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
$(function () { news.init(); });


