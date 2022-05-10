<div class="logo">
    <div class="fl">
        <a href="{$base_url}?r=home" style="background-image:url({$logo});background-color:#fff;width:{$logo_size.width}px" title="{$logo_title|t}">
            <img src="{$blank_image}" alt="{$site_name|t}" border="0" width="100%" height="100%" /></a>
    </div>
    {if $canSetMode}
        <div class="fl mLeft30 mTop10">
            <a href="javascript:shop.changeMode(0)">{'Phiên bản di động'|t}</a>
        </div>
    {/if}

    <div class="fr mTop10">
        {foreach from=$langs item=item key=k name=i}
            {if $k == $cur_lang}
                <div class="fl" style="color: red">{$item}</div>
            {else}
                <a href="javascript:void(0)" onclick="shop.lang.change('{$k}')" class="fl">{$item}</a>
            {/if}
            {if !$smarty.foreach.i.last} <div class="fl">&nbsp;|&nbsp;</div>{/if}
        {/foreach}
    </div>
    <div class="c"></div>
</div>
