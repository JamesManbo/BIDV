<div class="bv_menucate_uudai">
    <div class="bv_contain">
        <ul class="bv_menucate_uudai_build">
            <li>
                <a class="amthuc {if $cat==6}active{/if}" href="{$link_cate}?cat=6"><i class="bv_ic_amthuc"></i><br/>ẨM THỰC</a>
            </li>
            <li>
                <a class="muasam {if $cat==7}active{/if}" href="{$link_cate}?cat=7"><i class="bv_ic_muasam"></i><br/>MUA SẮM</a>
            </li>
            <li>
                <a class="dulich {if $cat==8}active{/if}" href="{$link_cate}?cat=8"><i class="bv_ic_dulich"></i><br/>Du LỊCH</a>
            </li>
            <li>
                <a class="suckhoe {if $cat==9}active{/if}" href="{$link_cate}?cat=9"><i class="bv_ic_suckhoe"></i><br/>SỨC KHỎE</a>
            </li>
            <li>
                <a class="doisong {if $cat==10}active{/if}" href="{$link_cate}?cat=10"><i class="bv_ic_doisong"></i><br/>ĐỜI SỐNG</a>
            </li>
        </ul>
        <div class="c"></div>
    </div>
</div>
<input type="hidden" name="cat" id='cat' value="{$cat}"/>
<div class="bv_menucate_listnews">
    <div class="bv_contain">
        <div class="bv_menucate_listnews_fiter">
            <div class="text">SẮP XẾP ƯU ĐÃI THEO</div>
            <div class="c"></div>
            <div class="bv_menucate_b_s">
                <select id='sort' name="sort">
                    <option value="0" >MẶC ĐỊNH</option>
                    <option value="1" {if $sort==1} selected{/if}>Mới nhất</option>
                    <option value="2" {if $sort==2} selected{/if}>Cũ nhất</option>
                </select>
            </div>
            <div class="bv_menucate_b_s">
                <select id="card" name="card">
                    <option value="0">Loại thẻ</option>
                    {$card}
                </select>
            </div>

            <div class="bv_menucate_b_s">
                <select id="promo_per" name="promo_per">
                    <option value="-1">Mức giảm giá</option>
                    {$promo_per}
                </select>
            </div>
            <div class="bv_menucate_b_s">
                <select id='city_id' name="city_id">
                    <option value="0">Địa điểm</option>
                    {$city}
                </select>
            </div>
            <input type="submit" value="LỌC" class="bv_menucate_btn"/>
            <div class="c"></div>
            {literal}
                <script type="text/javascript">
                    shop.ready.add(function () {
                        (function () {
                            [].slice.call(document.querySelectorAll('select.cs-select')).forEach(function (el) {
                                new SelectFx(el);
                            });
                        })();
                    });
                </script>
            {/literal}
        </div>
        {if $data}
            <ul class="bv_uudai_build">
                {foreach from=$data item=item name=i}
                    <li class="bv_uudai_build_item {$item.class}">
                        <a class="bv_uudai_build_item_img" href="{$item.link}">
                            <img width="100%" height="138" src="style/images/blank.gif" style="background:#fff url({$item.image}) no-repeat center center" />
                            {if $item.promo_per == 100}
                                <span>Miễn phí</span>
                            {elseif $item.promo_per == 0}
                                <span>Ưu đãi</span>
                            {else}
                                <span>{$item.promo_per_f}{if $item.promo_per < 100}<sub>%</sub>{else}{$currency}{/if}</span>
                            {/if}
                        </a>
                        <div class="bv_uudai_build_item_info">
                            <a class="title" href="{$item.link}">{$item.title}</a>
                            <div class="time">{$item.time_from|date_format:"%d/%m/%Y"} - {$item.time_to|date_format:"%d/%m/%Y"}</div>
                            <div class="des">{$item.sort}</div>
                            <div class="build_bot">
                                <ul>
                                    <li><a href="{$item.share.fb}" target="_blank">&#xf09a;</a></li>
                                    <li><a href="{$item.share.tw}" target="_blank">&#xf099;</a></li>
                                    <li><a href="{$item.share.gg}" target="_blank">&#xf0d5;</a></li>
                                    <li><a href="{$item.share.li}" target="_blank">&#xf0e1;</a></li>
                                </ul>
                                <div class="c"></div>
                            </div>
                        </div>
                        <div class="c"></div>
                    </li>
                {/foreach}
            </ul>
            <div class="c"></div>
            <div class="bv_pagging">
                {$paging}
            </div>
        {/if}
    </div>
</div>