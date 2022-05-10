
$(".do_right_bo_cate a").click(function (e) {
    var dataid = $(this).attr('data-id');
    var datachild = $(this).attr('data-child');
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
    //load_content(dataid);
    e.preventDefault();
});

function load_content(id) {
    if (id > 0) {
        shop.ajax_popup("act=faqs&code=buildcontent", 'POST', {id: id},
                function (json) {
                    if (json.err == 0) {
                        var html = '<li>Đang cập nhật dữ liệu</li>';
                        if (json.items != '') {
                            html = '';
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
                        }
                        jQuery('.faqs_right_build').html(html);
                    } else {
                        alert(json.msg);
                    }
                }, "json"
                );
    }
}