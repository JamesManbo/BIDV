<div class="bv_header_bot">
    <div class="bv_contain checkshow" style="position: relative">
        <a class="btn_menu" href="javascript:void(0)" onclick="show_hidendiv('bv_menu', this);jQuery('.bv_box_search').addClass('hidden');"></a>
        <ul class="bv_menu _os_Gen hidden">
            {foreach from=$menu item=entry name=i}
                <li class="bv_menu_li" {if $smarty.foreach.i.last} style="margin:0" {/if}>
                    <a {if $entry.items} onclick="show_hidendiv('bv_menu_two_{$smarty.foreach.i.iteration}', this)"{/if} class="bv_menu_li_a {if $entry.active} active{/if}" href="{if $entry.items}javascript:void(0){else}{$entry.link}{/if}">{$entry.title}&nbsp;&nbsp;<span>{if $entry.items}&#xf107;{else}&nbsp;{/if}</span></a>
                    {if $entry.items}
                        <ul class="bv_menu_two _os_Gen hidden bv_menu_two_{$smarty.foreach.i.iteration}">
                            {foreach from=$entry.items item=chir name=j}
                                <li>
                                    <a class="bv_menu_two_a"  href="{if $chir.link==''}{$base_url}{else}{$chir.link}{/if}">{$chir.title}</a>
                                </li>
                            {/foreach}
                        </ul>
                    {/if}
                </li>
            {/foreach}
        </ul>
        <a class="bv_logo" href="{$base_url}" title="{$logo_title}">
            <img src="{$logo}" alt="{$logo_title}" title="{$logo_title}"/>
        </a>
        <a class="btn_serach_m {if $cur_page=='search'}active{/if}" href="javascript:void(0)" onclick="show_hidendiv('bv_box_search', this);jQuery('.bv_menu').addClass('hidden');"></a>
        <div class="bv_box_search _os_Gen {if $cur_page!='search'}hidden{/if}">
            <form method="get" action="tim-kiem.html" name="searchbox" id="searchbox">
                <input id="search_key" name="search_key" type="text" value="{$search_key}" placeholder="Tìm kiếm"/>
                <a href="javascript:void(0)" onclick="search_dm(document.searchbox)">&#xf002;</a>
                <div>
                    <label for="optioncheck1"><input type="radio" value="0" {if $optioncheck==0}checked{/if} id="optioncheck1" name="optioncheck"/>
                        Tin tức</label>
                    <label for="optioncheck2"><input type="radio" value="1" {if $optioncheck==1}checked{/if} id="optioncheck2" name="optioncheck"/>
                        Chương trình khuyến mãi</label>
                    <label for="optioncheck3"><input type="radio" value="3" {if $optioncheck==3}checked{/if} id="optioncheck3" name="optioncheck"/>
                        Ưu đãi vàng</label>
                </div>
            </form>
        </div>
        <div class="c"></div>
    </div>
</div>{literal}
<script>

    function search_dm(frm) {
        var search_query = frm.search_key.value;
        if (search_query == '') {
            alert(t('Vui lòng nhập từ khóa tìm kiếm.'));
            frm.search_key.focus();
            return false;
        }
        frm.submit();
    }

</script> 
{/literal}