<div class="footerContent">
  <ul class="fl">
	{foreach from=$footer_menu item=entry name=i}
	<li{if $smarty.foreach.i.first} class="first"{/if}>
	  <a href="{$entry.link}"{if $smarty.foreach.i.first} class="active"{/if}{if $entry.no_follow} rel="nofollow"{/if}{if $entry.target} target="_blank"{/if}>{$entry.title|t}</a></li>
	{/foreach}
	<li class="last">&copy;&nbsp;Powered by <a href="mailto:lymanhha@gmail.com">Superman team</a>. All rights reserved 2013</li>
  </ul>
  <div id="histats_counter" class="fr mTop20"></div>
  <div class="c"></div>
</div>