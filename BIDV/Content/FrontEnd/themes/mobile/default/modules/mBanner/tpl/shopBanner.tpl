{if $page=='home'||$page=='gioithieu'}
    <div class="bv_slide_home">
        <div class="jcarousel-wrapper">
            <div class="jcarousel">
                <ul>
                    {foreach from=$banner2 item=entry2 name=i}
                        <li>
                            <a href="{if $entry2.link==''}javascript:void(0){else}{$entry2.link}{/if}" target="_blank">
                                <img style="background: transparent url('{$entry2.image}') no-repeat center center;width: 100%;background-size: cover" src="style/images/blank.gif"/>
                            </a>
                            {if $entry2.body}
                                <div class="bv_contain bv_slide_jc" style="z-index: 2">
                                    {$entry2.body}
                                </div>{/if}
                            </li>
                            {/foreach}
                            </ul>
                            {if $banner_total>1}
                                <div class="bv_contain bv_slide_jc_nb">
                                    <a href="#" class="jcarousel-control-prev" data-jcarouselcontrol="true">&#xf104;</a>
                                    <a href="#" class="jcarousel-control-next" data-jcarouselcontrol="true">&#xf105;</a>
                                </div>
                                <div class="bv_contain bv_slide_jc_page">
                                    <p class="jcarousel-pagination" data-jcarouselpagination="true"></p>
                                </div>
                            {/if}
                        </div>
                    </div>
                    {literal}
                        <script type="text/javascript">
                            shop.ready.add(function () {
                                var jcarousel = $('.jcarousel');
                                jcarousel
                                        .on('jcarousel:reload jcarousel:create', function () {
                                            jcarousel.jcarousel('items').width(jcarousel.innerWidth());
                                        })
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
                                    interval: 6000,
                                    target: '+=1',
                                    autostart: true
                                });

                                $('.jcarousel-control-prev')
                                        .on('jcarouselcontrol:active', function () {
                                            $(this).removeClass('inactive');
                                        })
                                        .on('jcarouselcontrol:inactive', function () {
                                            $(this).addClass('inactive');
                                        })
                                        .jcarouselControl({
                                            target: '-=1'
                                        });

                                $('.jcarousel-control-next')
                                        .on('jcarouselcontrol:active', function () {
                                            $(this).removeClass('inactive');
                                        })
                                        .on('jcarouselcontrol:inactive', function () {
                                            $(this).addClass('inactive');
                                        })
                                        .on('click', function (e) {
                                            e.preventDefault();
                                        })
                                        .jcarouselControl({
                                            target: '+=1'
                                        });

                                $('.jcarousel-pagination')
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
                        </script>
                    {/literal}
                </div>
                {elseif $page=='category'|| $page=='dichvuthe'}

                    <div class="bv_slide_home">
                        <div class="jcarousel-wrapper">
                            <div class="jcarousel jcarousel_dvthe">
                                <ul>
                                    {foreach from=$banner2 item=entry name=i}
                                        <li>
                                            <a href="{$entry.link}" target="_blank">
                                                <img style="background: transparent url('{$entry.image}') no-repeat center center;width: 100%;background-size: cover" src="style/images/blank.gif"/>
                                            </a>
                                            {if $entry.body}
                                                <div class="bv_contain bv_slide_jc" style="z-index: 2">
                                                    {$entry.body}
                                                </div>{/if}
                                            </li>
                                            {/foreach}
                                            </ul>
                                        </div>
                                    </div>

                                    {literal}
                                        <script type="text/javascript">
                                            shop.ready.add(function () {
                                                $(function () {
                                                    var jcarousel = $('.jcarousel');

                                                    jcarousel
                                                            .on('jcarousel:reload jcarousel:create', function () {
                                                                jcarousel.jcarousel('items').width(jcarousel.innerWidth());
                                                            })
                                                            .jcarousel({
                                                                wrap: 'circular',
                                                                transitions: Modernizr.csstransitions ? {
                                                                    transforms: Modernizr.csstransforms,
                                                                    transforms3d: Modernizr.csstransforms3d,
                                                                    easing: 'ease'
                                                                } : false
                                                            });

                                                    $('.jcarousel-pagination')
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
                                            });
                                        </script>
                                    {/literal}
                                    <div class="bv_title_box">
                                        <div class="bv_contain">
                                            <div class="bv_title_box_padding">
                                                <ul class="bv_title_box_mnu">
                                                    <li><a href="{$base_url}"> Trang chủ&nbsp;&nbsp;<i class="bv_ic_arrmenu"></i></a></li>
                                                    <li><a href="javascript:void(0)"> Dịch vụ thẻ&nbsp;&nbsp;<i class="bv_ic_arrmenu"></i></a></li>
                                                </ul>
                                                <div class="c"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                {else if $page=='favorable'||$page=='detailspromotion'||$page=='news'||$page=='details'||$page=='newspromotion'||$page=='faqs'}
                                    <div class="bv_slide_home">
                                        {if $page=='favorable'||$page=='detailspromotion'||$page=='news'||$page=='newspromotion'||$page=='details'||$page=='newsdetailpromotion'||$page=='faqs'}
                                            <div class="jcarousel-wrapper">
                                                <div class="jcarousel jcarousel_uudai">
                                                    <ul>
                                                        {foreach from=$banner2 item=entry name=i}
                                                            <li>
                                                                <a href="{$entry.link}" target="_blank">
                                                                    <img height="358" style="background: transparent url('{$entry.image}') no-repeat center center;width: 100%;background-size: cover" src="style/images/blank.gif"/>
                                                                </a>
                                                                {if $entry.body}
                                                                    <div class="bv_contain bv_slide_jc" style="z-index: 2">
                                                                        {$entry.body}
                                                                    </div>{/if}
                                                                </li>
                                                                {/foreach}
                                                                </ul>
                                                                {if $banner_total>1}
                                                                    <div class="bv_contain bv_slide_jc_page">
                                                                        <p class="jcarousel-pagination" data-jcarouselpagination="true"></p>
                                                                    </div>
                                                                {/if}
                                                            </div>
                                                        </div>
                                                        {literal}
                                                            <script type="text/javascript">
                                                                shop.ready.add(function () {
                                                                    $(function () {
                                                                        var jcarousel = $('.jcarousel');

                                                                        jcarousel
                                                                                .on('jcarousel:reload jcarousel:create', function () {
                                                                                    jcarousel.jcarousel('items').width(jcarousel.innerWidth());
                                                                                })
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
                                                                            interval: 6000,
                                                                            target: '+=1',
                                                                            autostart: true
                                                                        });

                                                                        $('.jcarousel-pagination')
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
                                                                });
                                                            </script>
                                                        {/literal}
                                                        {/if}
                                                            <div class="bv_title_box">
                                                                <div class="bv_contain">
                                                                    <div class="bv_title_box_padding">
                                                                        {if $page=='favorable'||$page=='detailspromotion'}
                                                                            <ul class="bv_title_box_mnu">
                                                                                <li><a href="{$base_url}"> Trang chủ&nbsp;&nbsp;<i class="bv_ic_arrmenu"></i></a></li>
                                                                                        {if $page=='detailspromotion'}
                                                                                    <li><a href="uu-dai-vang.html"> Ưu đãi vàng&nbsp;&nbsp;<i class="bv_ic_arrmenu"></i></a></li>
                                                                                        {/if}
                                                                            </ul>
                                                                        {elseif $page=='news'||$page=='details'}
                                                                            <ul class="bv_title_box_mnu">
                                                                                <li><a href="{$base_url}"> Trang chủ&nbsp;&nbsp;<i class="bv_ic_arrmenu"></i></a></li>
                                                                                        {if $page=='details'}
                                                                                    <li><a href="tin-tuc.html"> Tin tức&nbsp;&nbsp;<i class="bv_ic_arrmenu"></i></a></li>
                                                                                        {/if}
                                                                            </ul>
                                                                        {elseif $page=='tags'}
                                                                            <ul class="bv_title_box_mnu">
                                                                                <li><a href="{$base_url}"> Trang chủ&nbsp;&nbsp;<i class="bv_ic_arrmenu"></i></a></li>
                                                                                        {if $page=='details'}
                                                                                    <li><a href="javascript:void(0)"> Tag&nbsp;&nbsp;<i class="bv_ic_arrmenu"></i></a></li>
                                                                                        {/if}
                                                                            </ul>
                                                                        {elseif $page=='newspromotion'||$page=='newsdetailpromotion'}

                                                                            <ul class="bv_title_box_mnu">
                                                                                <li><a href="{$base_url}"> Trang chủ&nbsp;&nbsp;<i class="bv_ic_arrmenu"></i></a></li>
                                                                                        {if $page=='newsdetailpromotion'}
                                                                                    <li><a href="khuyen-mai.html"> Tin khuyến mãi&nbsp;&nbsp;<i class="bv_ic_arrmenu"></i></a></li>
                                                                                        {/if}
                                                                            </ul>
                                                                        {elseif $page=='faqs'}

                                                                            <ul class="bv_title_box_mnu">
                                                                                <li><a href="{$base_url}"> Trang chủ&nbsp;&nbsp;<i class="bv_ic_arrmenu"></i></a></li>
                                                                                <li><a href="cau-hoi-thuong-gap.html"> Câu hỏi thường gặp&nbsp;&nbsp;<i class="bv_ic_arrmenu"></i></a></li>
                                                                            </ul>
                                                                        {/if}

                                                                        <div class="c"></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        {/if}
