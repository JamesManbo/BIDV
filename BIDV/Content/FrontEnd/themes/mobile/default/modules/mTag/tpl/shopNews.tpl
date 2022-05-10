{if $items}
    <div class="bv_news" style="background-color: #FFF">
    <div class="bv_contain">
        <ul class="bv_news_list" style="padding-top: 70px">
            {foreach from=$items item=new name=i}
            {if $new.type==3}
                <li class="bv_uudai_build_item {$new.class}">
                    <a class="bv_uudai_build_item_img" href="{$new.link}">
                        <img width="100%" height="138" src="style/images/blank.gif" style="background:#fff url({$new.image}) no-repeat center center" />
                        {if $new.promo_per == 100}
                            <span>Miễn phí</span>
                        {elseif $new.promo_per == 0}
                            <span>Ưu đãi</span>
                        {else}
                            <span>{$new.promo_per_f}{if $item.promo_per < 100}<sub>%</sub>{else}{$currency}{/if}</span>
                        {/if}
                    </a>
                    <div class="bv_uudai_build_item_info">
                        <a class="title" href="{$new.link}">{$new.title}</a>
                        <div class="time">{$new.time_from|date_format:"%d/%m/%Y"} - {$new.time_to|date_format:"%d/%m/%Y"}</div>
                        <div class="des">{$new.sort}</div>
                        <div class="build_bot">
                            <ul>
                                <li style="border: none"><a href="{$new.share.fb}" target="_blank">&#xf09a;</a></li>
                                <li style="border: none"><a href="{$new.share.tw}" target="_blank">&#xf099;</a></li>
                                <li style="border: none"><a href="{$new.share.gg}" target="_blank">&#xf0d5;</a></li>
                                <li style="border: none"><a href="{$new.share.li}" target="_blank">&#xf0e1;</a></li>
                            </ul>
                            <div class="c"></div>
                        </div>
                    </div>
                    <div class="c"></div>
                </li>
                
            {else}
            <li>
                <div class="bv_contain">
                <a style="width: 100%;margin: 0 0 15px 0;font-size: 17px;line-height: 20px;height: auto" class="bv_news_list_tit" href="{$new.link}">{$new.title}</a>
                <div class="c"></div>
                <a style="width:50%" class="bv_news_list_img_m" href="{$new.link}">
                    <img height="110" width="100%" src="style/images/blank.gif" style="background-image: url('{$new.image}');background-size:cover"/>
                </a>
                <a style="width:45%" class="bv_news_list_tit_m" href="{$new.link}">
                    <span style="text-transform: none " class="bv_news_list_tit_m_s">{$new.sort_body}</span>
                    <span class="time">{$new.time_from|date_format:"%d/%m"}-{$new.time_to|date_format:"%d/%m"}</span>
                </a>
                <div class="c"></div>
                </div>
            </li>
            {/if}
            {/foreach}
        </ul>
        <div class="c"></div>
        <div class="bv_pagging">
            {$pagging}
        </div>
    </div>
</div>
{/if}