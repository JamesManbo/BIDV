
<div class="bv_dvthe_top">
    <div class="bv_contain">
        <div class="bv_dvthe_top_title">{$data_card_one.title}</div>
        <div class="bv_dvthe_top_note">{$data_card_one.title_seo}</div>
        <ul class="bv_dvthe_top_build">
            {if $data_card_one.slogan_arr}
                {foreach from=$data_card_one.slogan_arr item=slogan name=s}
                    {if $smarty.foreach.s.first}
                        <li class="bv_dvthe_top_build_li">
                            <img id="card_show" src="{$data_card_one.images}" style="margin-bottom: 15px;width:258px" />
                            {if $data_group}
                                <ul class="bv_dvthe_top_build_menu">
                                    <li><a onclick="change_card('{$data_card_one.images}', this)" class="active" href="javascript:void(0)">{$data_card_one.title_text}</a></li>
                                        {foreach from=$data_group item=sgroup name=g}
                                        <li><a onclick="change_card('{$sgroup.images}', this)" href="javascript:void(0)">{$sgroup.title_text}</a></li>
                                        {/foreach}
                                </ul>
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
        <div style="text-align: center">
            <a style="padding: 15px" class="bv_dvthe_top_build_more" href="{$data_card_one.link}">CHI TIẾT</a>
        </div>
    </div>
</div>
{if $data.sosanh}
    <div style="cursor: pointer" class="bv_dvthe_ss" id="bv_dvthe_ss">
        <div class="bv_contain">
            {$data.sosanh}
        </div>
    </div>
{else}
    <div class="bv_dvthe_iinfo_den">
        <div class="bv_contain">
            <ul class="bv_dvthe_iinfo_den_menu">
                <li>
                    <a href="javascript:void(0)" onclick="scrolldow('.bv_dvthe_iinfo_b_content', this)">
                        <i class="bv_ic_tinhnang"></i>
                        <span>tính năng</span>
                    </a>
                </li>
                <li>
                    <a href="javascript:void(0)" onclick="scrolldow('.bv_dvthe_iinfo_b_content', this)">
                        <i class="bv_ic_mota"></i>
                        <span>MÔ TẢ</span>
                    </a>
                </li>
                <li>
                    <a href="javascript:void(0)" onclick="scrolldow('.bv_dvthe_iinfo_b_content', this)">
                        <i class="bv_ic_giaodich"></i>
                        <span>GIAO DỊCH</span>
                    </a>
                </li>
                <li>
                    <a href="javascript:void(0)" onclick="scrolldow('.bv_dvthe_iinfo_xam', this)">
                        <i class="bv_ic_bieuphi"></i>
                        <span>BIỂU PHÍ DỊCH VỤ</span>
                    </a>
                </li>
                <li>
                    <a href="javascript:void(0)" onclick="scrolldow('.bv_dvthe_iinfo_trang', this)">
                        <i class="bv_ic_hanmuc"></i>
                        <span>Hạn mức giao dịch</span>
                    </a>
                </li>
            </ul>
            <div class="c"></div>
            <ul class="bv_dvthe_iinfo_b_content">
                <li class="content">
                    <div class="title"><i class="bv_ic_tinhnangbig"></i>tính năng</div>
                    {$data.tinh_nang}
                </li>
                <li class="content">
                    <div class="title"><i class="bv_ic_motabig"></i>Mô tả</div>
                    {$data.mota}
                </li>
                <li class="content">
                    <div class="title"><i class="bv_ic_giaodichbig"></i>Giao dịch</div>
                    {$data.diem_gd}
                </li>
            </ul>
            <div class="c"></div>
        </div>
    </div>
    {if $data_card_one.bieuphi}
        <div class="bv_dvthe_iinfo_xam">
            <div class="bv_contain">
                {$data_card_one.bieuphi}
                <div class="c"></div>
            </div>
        </div>
    {/if}
    {if $data_card_one.hanmuc}
        <div class="bv_dvthe_iinfo_trang">
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
{literal}<script type="text/javascript">
    shop.ready.add(function () {
        shop.attach.loadByObject({/literal}{$data.id}{literal}, 'category', function (data) {
            for (var i in data['data']) {
                //console.log(data['data'][i]);
                var link = data['data'][i].download;
                jQuery('#bv_dvthe_ss').attr('onclick', 'window.open("' + link + '","_blank");');
            }

        });
    });
    </script>{/literal}