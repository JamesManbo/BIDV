<div style='background-color: #ddddde;padding: 20px 0' >
    <div class="bv_contain">
        <div class="bv_menucate_newsmore_right_build">
            {if $news}
                <div class="bv_menucate_newsmore_right_title">
                    Tin tức Nổi BẬT
                </div>
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

            {/if}
        </div>
        {if $arrTag}
            <div class="bv_menucate_cn_title_box">
                Từ KHÓA Nổi bật
            </div>
            <ul class="bv_menucate_list_key" style="margin-bottom: 30px">
                {foreach from=$arrTag item=tag name=i}
                    <li><a target="_blank" href="{$tag.link}">{$tag.title}</a></li>
                    {/foreach}
            </ul>
        {/if}
    </div>
</div>