{if $items}
    <div class="bv_news" style="background-color: #FFF;padding: 30px 0">
    <div class="bv_contain">
         <ul class="bv_news_list">
                {foreach from=$items item=new name=i}
                    <li>
                        <a style="width: 100%;margin: 0 0 15px 0;height: auto" class="bv_news_list_tit" href="{$new.link}">{$new.title}</a>
                         <div class="c"></div>
                         <a style="width:50%" class="bv_news_list_img_m" href="{$new.link}">
                            <img height="110" width="100%" src="style/images/blank.gif" style="background-image: url('{$new.image}');background-size:cover"/>
                        </a>
                        <a style="width:45%" class="bv_news_list_tit_m" href="{$new.link}">
                            <span style="text-transform: none " class="bv_news_list_tit_m_s">{$new.sort_body}</span>
                            <span class="view"><i class="bv_ic_views"></i>{$new.view}</span>
                            <span class="time">{$new.dateof|date_format:"%d/%m/%Y"}</span>
                        </a>
                        <div class="c"></div>
                    </li>
                {/foreach}
            </ul>
        <div class="c"></div>
        <div class="bv_pagging">
            {$pagging}
        </div>
    </div>
</div>
{/if}