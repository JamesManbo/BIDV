<ul class="do_right_bo_cate">
    {foreach from=$cats item=cat name=c}
        <li class="hide_active {$cat.data.active}">
            <a {if !$cat.items}data-child='2'{/if} data-id='{$cat.data.id}'  href="javascript:void(0)">
                <span class="do_right_bo_cate_text">{$cat.data.title|t}</span>
            </a>
            {if $cat.items}
                <ul id='do_right_bo_cate_child_{$cat.data.id}' class="do_right_bo_cate_child">
                    {foreach from=$cat.items item=item name=i}
                        <li>
                            <a data-child='1' data-id='{$item.id}' class="remove_activechild {$item.active}" href="javascript:void(0)">
                                {$item.title|t}
                            </a>
                        </li>
                    {/foreach}
                </ul>
            {/if}
        </li>
    {/foreach}
</ul>
