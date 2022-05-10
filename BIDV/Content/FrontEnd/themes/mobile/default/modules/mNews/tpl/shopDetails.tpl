<div class="bv_box_details">
    <div class="bv_contain">
    <div class="bv_box_details_padding">
        <div class="bv_box_details_tit">
            {$data.title}
        </div>
        <div style="width:100%" class="bv_news_list_tit_m mBottom15" href="{$data.link}">
            <span class="view"><i class="bv_ic_views"></i>{$data.view}</span>
            <span class="time">{$data.dateof|date_format:"%d/%m/%Y"}</span>
        </div>
        <div class="bv_box_details_des">
            {$data.sort_body}
        </div>
        <div id="popup_galery" class="bv_box_details_info">
            {$data.body}
        </div>
        <div id="attach-con" class="mTop20"></div>
    </div>
    <div class="bv_box_details_Lienquan">
        {if $arrTag}
        <div class="bv_box_details_Lienquan_left">
            <span><span></span> Từ khóa liên quan:</span> 
            {foreach from=$arrTag item=tag name=i}
                <a target="_blank" href="{$tag.link}">{$tag.title}</a>{if not $smarty.foreach.i.last}, {/if}
            {/foreach}
        </div>
        {/if}
        <ul class="bv_box_details_Lienquan_right">
            <li><a href="{$data.share.fb}" target="_blank">&#xf09a;</a></li>
            <li><a href="{$data.share.tw}" target="_blank">&#xf099;</a></li>
            <li><a href="{$data.share.gg}" target="_blank">&#xf0d5;</a></li>
            <li><a href="{$data.share.li}" target="_blank">&#xf0e1;</a></li>
        </ul>
        <div class="c"></div>
    </div>
    <div class="bv_box_details_padding">
        {if $arrlinequan}
        <div class="bv_box_details_padding_lq">Tin liên quan</div>
        <ul class="bv_box_details_padding_lq_link">
            {foreach from=$arrlinequan item=lquan name=l}
            <li><i></i>&nbsp;&nbsp;<a href="{$lquan.link}">{$lquan.title}</a></li>
            {/foreach}
        </ul>
        {/if}
    </div>
    </div>
</div>

{literal}<script type="text/javascript">
    shop.ready.add(function(){
        shop.attach.loadByObject({/literal}{$data.id}{literal}, 'news', function(data){
            jQuery('#attach-con').html(data.html);
        });
    });
</script>{/literal}
