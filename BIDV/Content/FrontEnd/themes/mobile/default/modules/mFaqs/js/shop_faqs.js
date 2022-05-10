
$(".do_right_bo_cate a").click(function (e) {
    var dataid = $(this).attr('data-id');
    var datachild = $(this).attr('data-child');
    jQuery('#show_form').remove();
    if (datachild == 1) {
        jQuery('.remove_activechild').removeClass('active');
        jQuery(this).addClass('active');
    } else {
        jQuery('.hide_active').removeClass('active');
        jQuery(this).parent('li.hide_active').addClass('active');
        if (jQuery('#do_right_bo_cate_child_' + dataid).length > 0) {
            jQuery('#do_right_bo_cate_child_' + dataid).show();
        }
    }
    if (datachild > 0) {
        load_content(dataid, this);
    }
    e.preventDefault();
});

function load_content(id, obj) {
    if (id > 0) {
        shop.ajax_popup("act=faqs&code=buildcontent", 'POST', {id: id},
                function (json) {
                    if (json.err == 0) {
                        var html = '<div id="show_form" class="bv_contain"><ul class="faqs_right_build"><li style="margin: 15px 0;color: red;">Đang cập nhật dữ liệu</li>';
                        if (json.items != '') {
                            html = '<div id="show_form" class="bv_contain"><ul class="faqs_right_build">';
                            for (var i in json.items) {
                                html += '<li>' +
                                        '<a onclick="jQuery(\'#show_faq_f_' + i + '\').slideToggle();" href="javascript:void(0)">' +
                                        '<span>&#xf107;</span> ' + json.items[i].question +
                                        '</a>' +
                                        '<div class="show_forem" id=\'show_faq_f_' + i + '\'>' +
                                        json.items[i].answer +
                                        '</div>' +
                                        '</li>';
                            }
                            html += '</ul></div>'
                        }
                        jQuery(obj).after(html);
                    } else {
                        alert(json.msg);
                    }
                }, "json"
                );
    }
}