$( ".bv_program_sell_tab li a" ).mouseover(function() {
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
$(document).bind('click', function (e) {
    var $clicked = $(e.target);
    if (!$clicked.parents().hasClass("checkshow")) {
        jQuery('._os_Gen').addClass('hidden');
    }
});
function show_hidendiv(elm,obj) {
    if (jQuery('.' + elm).hasClass('hidden')) {
        jQuery('.' + elm).removeClass('hidden');
        jQuery(obj).addClass('active');
    } else {
        jQuery('.' + elm).addClass('hidden');
        jQuery(obj).removeClass('active');
    }
    
}