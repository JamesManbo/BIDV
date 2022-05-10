<div class="bv_dvthe_top">
    <div class="bv_contain">
        <div class="bv_dvthe_top_title">{$data_card_one.title}</div>
        <div class="bv_dvthe_top_note">{$data_card_one.title_seo}</div>
        <ul class="bv_dvthe_top_build">
            {if $data_card_one.slogan_arr}
                {foreach from=$data_card_one.slogan_arr item=slogan name=s}
                    {if $smarty.foreach.s.first}
                        <li class="bv_dvthe_top_build_li" style="text-align: center">
                            {if $data_group}
                                <img id="card_show" src="{$data_card_one.images}" style="margin-bottom: 15px"/>
                                <ul class="bv_dvthe_top_build_menu">
                                    <li><a onclick="change_card('{$data_card_one.images}', this)" class="active" href="javascript:void(0)">{$data_card_one.title_text}</a></li>
                                        {foreach from=$data_group item=sgroup name=g}
                                        <li><a onclick="change_card('{$sgroup.images}', this)" href="javascript:void(0)">{$sgroup.title_text}</a></li>
                                        {/foreach}
                                </ul>
                            {else}
                                {assign var='mycount' value=$data_card|@count}
                                <div class="bv_slide_home bv_slide_home_show2">
                                    <div class="jcarousel-wrapper">
                                        <div class="jcarousel jcarousel_m">
                                            <ul>
                                                {foreach from=$data_card item=dcard name=c}
                                                    <li class="{if $dcard.id==$data_card_one.id} active{/if}">
                                                        <a href="{$dcard.link}"><img width="100%" height="125" src="{$dcard.images}"/></a>
                                                    </li>
                                                {/foreach}
                                            </ul>
                                            {if $mycount>1}
                                                <div class="bv_contain">
                                                    <a href="#" class="jcarousel-control-prev" data-jcarouselcontrol="true">&#xf104;</a>
                                                    <a href="#" class="jcarousel-control-next" data-jcarouselcontrol="true">&#xf105;</a>
                                                </div>
                                            {/if}
                                        </div>
                                    </div>
                                    {literal}
                                        <script type="text/javascript">
                                            shop.ready.add(function () {
                                                var jcarousel2 = $('.jcarousel_m');
                                                jcarousel2
                                                        .on('jcarousel:reload jcarousel:create', function () {
                                                            jcarousel2.jcarousel('items').width(jcarousel2.innerWidth());
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
                                                $(".jcarousel_m ul li").each(function (index) {
                                                    if ($(this).hasClass('active')) {
                                                        $('.jcarousel_m').jcarousel('scroll', index);
                                                    }
                                                });
                                            });
                                        </script>
                                    {/literal}
                                </div>
                            {/if}
                        </li>
                    {/if}
                    <li class="bv_dvthe_top_build_li">
                        {foreach from=$slogan item=slo name=sn}
                            <div style="color:{$slo.color}" class="bv_dvthe_top_build_tet">{$slo.title}</div>
                            <div class="bv_dvthe_top_build_note">{$slo.slogan}</div>
                        {/foreach}
                    </li>
                {/foreach}
            {/if}
        </ul>
        <div class="c"></div>

    </div>
</div>
<div class="bv_dvthe_iinfo_den">
    <div class="bv_contain">
        <ul class="bv_dvthe_iinfo_b_content">
            {if $data.tinh_nang}
                <li class="content">
                    <div class="title"><i class="bv_ic_tinhnangbig"></i>tính năng</div>
                    {$data.tinh_nang}
                </li>
            {/if}
            {if $data.mota}
                <li class="content">
                    <div class="title"><i class="bv_ic_motabig"></i>Mô tả</div>
                    {$data.mota}
                </li>
            {/if}
            {if $data.diem_gd}
                <li class="content">
                    <div class="title"><i class="bv_ic_giaodichbig"></i>Giao dịch</div>
                    {$data.diem_gd}
                </li>
            {/if}
        </ul>
        <div class="c"></div>
    </div>
</div>
{if $data_card_one.bieuphi}
    <div class="bv_dvthe_iinfo_den">
        <div class="bv_contain">
            {$data_card_one.bieuphi}
            <div class="c"></div>
        </div>
    </div>
{/if}
{if $data_card_one.hanmuc}
    <div class="bv_dvthe_iinfo_den">
        <div class="bv_contain">
            <div class="bv_dvthe_iinfo_trang_title">
                <i class="bv_ic_hanmuc_big"></i>
                <div class="title_3">
                    Hạn mức<br/>giao dịch (VNĐ)
                </div>
                <div class="c"></div>
            </div>
            {$data_card_one.hanmuc}
            <div class="c"></div>
        </div>
    </div>
{/if}
{literal}
    <script>

        function change_card(src, obj) {
            jQuery('.bv_dvthe_top_build_menu li a').removeClass('active');
            jQuery('#card_show').attr('src', src);
            jQuery(obj).addClass('active');
        }

        function scrolldow(bclass, obj) {
            $('html, body').stop().animate({
                scrollTop: $(bclass).offset().top - 180
            }, 400);
            $('.bv_dvthe_iinfo_den_menu li a').removeClass('active');
            $(obj).addClass('active');
        }
    </script>
{/literal}