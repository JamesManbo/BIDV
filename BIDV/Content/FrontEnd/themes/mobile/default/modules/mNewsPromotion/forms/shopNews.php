<?php

class shopNewsForm extends Form {

    function __construct() {
       
    }

    function draw() {
        
        global $display;
        
        $key_tag =  Url::getParam('key','');
        if ($key_tag != '') {
            $db_tag_get = DB::fetch("SELECT * FROM " . T_TAG . " WHERE status =1 AND type=0 AND tag='" . $key_tag . "'");
            if (!empty($db_tag_get)) {
                $db_tag_get_id = DB::query("SELECT * FROM " . T_TAG_ITEM . " WHERE type=0 AND tag_id=" . $db_tag_get['id']);
                $id_Tag_get = array();
                if ($db_tag_get_id) {
                    while ($r_t_i = mysql_fetch_assoc($db_tag_get_id)) {
                        $id_Tag_get[$r_t_i['item_id']] = $r_t_i['item_id'];
                    }
                    if (!empty($id_Tag_get)) {
                         $cond[] = ' id IN (' . implode(',', $id_Tag_get).')';
                    }
                }
            }
        }
        $cond[] = "time_to > " . TIME_NOW;
        $cond[] = 'status = 1';
        $cond[] = 'type = 1'; #Quan tri cho tin tuc
        $search_value = FunctionLib::addCondition($cond);
        $search_value = ($search_value != '') ? ' WHERE ' . $search_value : '';
        $sql = 'SELECT  * FROM  ' . T_NEWS . $search_value . ' ORDER BY dateof DESC';
        $re = Pagging::pager_query($sql,9);
        $pagging = '';
        $items = array();
        if ($re) {
            while ($item = mysql_fetch_assoc($re)) {
                $items[$item['id']] = News::parser_new($item);
            }
            if (!empty($items)) {
                $res = DB::query("SELECT * FROM (SELECT * FROM " . T_NEWS_IMAGE . " WHERE news_id IN (" . implode(',', array_keys($items)) . ") ORDER BY type ASC) as A GROUP BY A.news_id");
                while ($r = @mysql_fetch_assoc($res)) {
                    $items[$r['news_id']]['image'] = News::getNewsImage($r['image'], $items[$r['news_id']]['created'], 306);
                }
            }
            $pagging = Pagging::getPager(3, false, 'page_no', true);
        }
        $display->add('items', $items);
        $display->add('pagging', $pagging);
        $display->output('shopNews');
    }

}
