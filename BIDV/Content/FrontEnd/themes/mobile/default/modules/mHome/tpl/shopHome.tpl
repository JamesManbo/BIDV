<div class="bv_program_sell">
    <div class="bv_contain">
        <div class="bv_program_sell_title">CHƯƠNG TRÌNH ƯU ĐÃI</div>
        <div class="bv_program_sell_note">Tận hưởng thế giới ưu đãi cùng thẻ BIDV</div>
        <ul class="bv_program_sell_tab">
            {if $newspromotion}
                <li>
                    <a class="active" href="javascript:void(0)" data-content="bv_program_sell_ct_khuyenmai">
                        Tin tức khuyến mại
                    </a>
                </li>
            {/if}
            {if $data_promotion}
                <li>
                    <a href="javascript:void(0)" data-content="bv_program_sell_ct_diemvang">
                        Điểm ưu đãi vàng
                    </a>
                </li>
            {/if}
            <li>
                <a href="uu-dai-vang.html" data-content="bv_program_sell_ct_diemvang">
                    Tích luỹ điểm thưởng
                </a>
            </li>

        </ul>
    </div>
    <ul class="bv_program_sell_content">
        {if $newspromotion}
            <li class="bv_program_sell_content_li" id="bv_program_sell_ct_khuyenmai">
                <div class="wrapper">
                    <div class="jcarousel-wrapper">
                        <!-- Carousel -->
                        <div class="jcarousel_two">
                            <ul>
                                {foreach from=$newspromotion item=newprom name=i}
                                    <li>
                                        <a href="{$newprom.link}" class="bv_program_content">{$newprom.title}</a>
                                        <a href="{$newprom.link}"><img style="background-image: url({$newprom.image});background-size:contain" src="style/images/blank.gif"  alt="" width="245" height="286"></a>
                                    </li>
                                {/foreach}
                            </ul>
                        </div>
                        <div class="bv_contain bv_slide_jc_page">
                            <p class="jcarousel-two-pagination" data-jcarouselpagination="true"></p>
                        </div>

                    </div>
                </div>
            </li>
        {/if}
        {if $data_promotion}
            <li id="bv_program_sell_ct_diemvang"  class="bv_program_sell_content_li hidden">
                <div class="wrapper">
                    <div class="jcarousel-wrapper">
                        <!-- Carousel -->
                        <div class="jcarousel_tree">
                            <ul>
                                {foreach from=$data_promotion item=promotion name=i}
                                    <li>
                                        <a href="{$promotion.link}" class="bv_program_content">{$promotion.title}</a>
                                        <a href="{$promotion.link}"><img style="background-image: url({$promotion.image});background-size:contain" src="style/images/blank.gif"  alt="" width="245" height="286"></a>
                                    </li>
                                {/foreach}
                            </ul>
                        </div>
                        <div class="bv_contain bv_slide_jc_page">
                            <p class="jcarousel-tree-pagination" data-jcarouselpagination="true"></p>
                        </div>
                    </div>
                </div>
            </li>
        {/if}
    </ul>
    {literal}
        <script>
            function content_show(obj, id) {
                $('.bv_program_sell_tab a').removeClass('active');
                $(obj).addClass('active');
                $('.bv_program_sell_content_li').addClass('hidden');
                $('#' + id).removeClass('hidden');
                call();
            }
            shop.ready.add(function () {
                call();
            });
            function call() {
                (function ($) {
                    $(function () {
                        /*
                         Carousel initialization
                         */
                        $('.jcarousel_two')
                                .jcarousel({
                                    wrap: 'circular',
                                    animation: {
                                        duration: 1200,
                                        easing: 'linear',
                                        complete: function () {}
                                    },
                                    transitions: Modernizr.csstransitions ? {
                                        transforms: Modernizr.csstransforms,
                                        transforms3d: Modernizr.csstransforms3d,
                                        easing: 'ease'
                                    } : false
                                }).jcarouselAutoscroll({
                            interval: 1200,
                            target: '+=1',
                            autostart: true
                        });
                        ;

                        $('.jcarousel_tree')
                                .jcarousel({
                                    wrap: 'circular',
                                    animation: {
                                        duration: 1200,
                                        easing: 'linear',
                                        complete: function () {}
                                    },
                                    transitions: Modernizr.csstransitions ? {
                                        transforms: Modernizr.csstransforms,
                                        transforms3d: Modernizr.csstransforms3d,
                                        easing: 'ease'
                                    } : false
                                }).jcarouselAutoscroll({
                            interval: 1200,
                            target: '+=1',
                            autostart: true
                        });
                        ;

                        /*
                         Pagination initialization
                         */
                        $('.jcarousel-two-pagination')
                                .on('jcarouselpagination:active', 'a', function () {
                                    $(this).addClass('active');
                                })
                                .on('jcarouselpagination:inactive', 'a', function () {
                                    $(this).removeClass('active');
                                })
                                .on('click', function (e) {
                                    e.preventDefault();
                                })
                                .jcarouselPagination({
                                    item: function (page) {
                                        return '<a href="#' + page + '">' + page + '</a>';
                                    }
                                });
                        $('.jcarousel-tree-pagination')
                                .on('jcarouselpagination:active', 'a', function () {
                                    $(this).addClass('active');
                                })
                                .on('jcarouselpagination:inactive', 'a', function () {
                                    $(this).removeClass('active');
                                })
                                .on('click', function (e) {
                                    e.preventDefault();
                                })
                                .jcarouselPagination({
                                    item: function (page) {
                                        return '<a href="#' + page + '">' + page + '</a>';
                                    }
                                });
                    });
                })(jQuery);
            }
        </script>
    {/literal}
</div>
{if $menu_build}
    <div class="bv_product_service">
        <div class="bv_contain">
            <div class="bv_program_sell_title">SẢN PHẨM DỊCH VỤ</div>
            <ul class="bv_product_service_list">
                {foreach from=$menu_build item=mbuild name=m}
                    <li>
                        <a href="{$mbuild.link}">
                            <img src="{$blank_image}" height="110" width="100%" style="background: transparent url({$mbuild.icon}) no-repeat center center;background-size: contain" />
                            <span style="color: {$mbuild.color}">{$mbuild.title}</span>
                        </a>
                    </li>
                {/foreach}
            </ul>
            <div class="c"></div>
        </div>
    </div>
{/if}
{if $news}
    <div class="bv_news">
        <div class="bv_contain">
            <div class="bv_program_sell_title">TIN TỨC</div>
            <ul class="bv_news_list">
                {foreach from=$news item=new name=i}
                    <li>
                        <a class="bv_news_list_img_m" href="{$new.link}">
                            <img height="70" width="100%" src="style/images/blank.gif" style="background-image: url('{$new.image}');background-size:cover"/>
                        </a>
                        <a class="bv_news_list_tit_m" href="{$new.link}">
                            <span class="bv_news_list_tit_m_s">{$new.title}</span>
                            <span class="view"><i class="bv_ic_views"></i>{$new.view}</span>
                            <span class="time">{$new.dateof|date_format:"%d/%m/%Y"}</span>
                        </a>
                        <div class="c"></div>
                    </li>
                {/foreach}
            </ul>
            <div class="c"></div>
            <a href="tin-tuc.html" class="bv_news_list_linkmore">Xem thêm</a>
        </div>
    </div>
{/if}
<div class="bv_platimun">
    <div class="bv_contain">
        <div class="bv_box_img" style="background: transparent url('dev/plistden.png') no-repeat center top;background-size: contain;width: 100%;height: 260px">
            <div class="bv_text_fontrt">
                Platinum
            </div>
            <div class="bv_text_fontrt_t2">
                Visa - MasterCard <span>Credit Card</span>
            </div>
            <ul class="bv_build_link">
                <li><a href="https://ebank.bidv.com.vn/DKT/" target="_blank">mở thẻ NGAY</a></li>
                <li><a class="more" href="dich-vu-the/the-tin-dung-ca-nhan-3.html">CHI TIẾT</a></li>
            </ul>
        </div>
    </div>
</div>
<div class="bv_newletter">
    <div class="bv_contain">
        <input class="bv_newletter_input" type="text" value="" placeholder="Đăng ký nhận Newsletter" name="email_reg" id="email_reg"/>
        <a class="bv_newletter_btn" href="javascript:void(0)" onclick="shop.subscribe.reg_home()">ĐĂNG KÝ</a>
        <div class="c"></div>
    </div>
</div>