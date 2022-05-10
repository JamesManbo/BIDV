<div class="bv_box_search_page">
    <div class="bv_contain">
        <div class="bv_box_search_title bv_box_search_title_list">
            <div class="bv_box_search_title_text">
                Tìm thấy {$total} kết quả
            </div>
            {if $items}
            <ul class="bv_box_search_title_buld">
                {foreach from=$items item=new name=i}
                <li>
                    <a class="img" href="{$new.link}">
                        <img src="{$new.image}"/>
                    </a>
                    <div class="texxt">
                        <a href="{$new.link}">
                            {$new.title}
                        </a>
                        <div class="des">
                            {$new.sort_body}
                        </div>
                    </div>
                    <div class="c"></div>
                </li>
                {/foreach}
                
            </ul>
            {/if}
        </div>
        <div class="bv_pagging">
            {$pagging}
        </div>
    </div>
</div>