$(".bv_program_sell_tab li a").mouseover(function () {
    content_show(this, $(this).attr('data-content'));

});

pre_img_gallery('#popup_galery');
function pre_img_gallery(idbuild) {
    $(idbuild + " img").each(function () {
        var img = $(this).attr('src');
        var url = img.replace(/size_.+?\/[0-9]+\//, 'origin/');
        if ($(this).parents("a").length == 0) {
            $(this).wrap("<a href='" + url + "' data-gallery='#popup_galery_detail'></a>");
        }
    });
}

shop.ready.add(function () {
    $(function () {
        $(window).scroll(
                function () {
                    if ($('#btgotop').length == 0) {
                        jQuery('body').append('<a id="btgotop" href="javascript:void(0)" title="Lên đầu trang" onclick="jQuery(\'html,body\').animate({scrollTop: 0},1000);" class="go_top" style="display:none"></a>');
                    }
                    if ($(this).scrollTop() > 600) {
                        $('#btgotop').fadeIn();
                    } else {
                        $('#btgotop').fadeOut();
                    }
                });
        $('#btgotop').click(function () {
            $('body,html').animate({scrollTop: 0}, 800);
        });
    });
});