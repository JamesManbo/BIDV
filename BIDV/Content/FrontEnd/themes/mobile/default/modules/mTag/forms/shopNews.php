<?php

class shopNewsForm extends Form {

    function __construct() {
        
    }

    function draw() {

        global $display;
        $cond = $cond_promotion = array();
        $key_tag = Url::getParam('key', '');
        if ($key_tag != '') {
            $db_tag_get = DB::fetch("SELECT * FROM " . T_TAG . " WHERE type = 0 AND status =1 AND tag='" . $key_tag . "'");
            if (!empty($db_tag_get)) {
                $db_tag_get_id = DB::query("SELECT * FROM " . T_TAG_ITEM . " WHERE type = 0 AND tag_id=" . $db_tag_get['id']);
                $id_Tag_get = array();
                if ($db_tag_get_id) {
                    while ($r_t_i = mysql_fetch_assoc($db_tag_get_id)) {
                        $id_Tag_get[$r_t_i['item_id']] = $r_t_i['item_id'];
                    }
                    if (!empty($id_Tag_get)) {
                        $cond[] = ' id IN (' . implode(',', $id_Tag_get) . ')';
                    }
                }
            }
            $db_tag_get_promotion = DB::fetch("SELECT * FROM " . T_TAG . " WHERE type = 2 AND status =1 AND tag='" . $key_tag . "'");
            if (!empty($db_tag_get_promotion)) {
                $db_tag_get_promotion = DB::query("SELECT * FROM " . T_TAG_ITEM . " WHERE type = 2 AND tag_id=" . $db_tag_get_promotion['id']);
                $id_Tag_promotion = array();
                if ($db_tag_get_promotion) {
                    while ($r_t_i = mysql_fetch_assoc($db_tag_get_promotion)) {
                        $id_Tag_promotion[$r_t_i['item_id']] = $r_t_i['item_id'];
                    }
                    if (!empty($id_Tag_promotion)) {
                        $cond_promotion[] = ' id IN (' . implode(',', $id_Tag_promotion) . ')';
                    }
                }
            }
        }
        $items = array();

        if (!empty($cond)) {
            $cond[] = 'status = 1';
            #$cond[] = 'type = 0'; 
            $search_value = FunctionLib::addCondition($cond);
            $search_value = ($search_value != '') ? ' WHERE ' . $search_value : '';
            $sql = 'SELECT  * FROM  ' . T_NEWS . $search_value . ' ORDER BY created DESC';
            $re = DB::query($sql);
            $pagging = '';
            if ($re) {
                while ($item = mysql_fetch_assoc($re)) {
                    $items[$item['id']] = News::parser_new($item);
                }
                if (!empty($items)) {
                    $res = DB::query("SELECT * FROM (SELECT * FROM " . T_NEWS_IMAGE . " WHERE news_id IN (" . implode(',', array_keys($items)) . ") ORDER BY type ASC) as A GROUP BY news_id");
                    while ($r = @mysql_fetch_assoc($res)) {
                        $items[$r['news_id']]['image'] = News::getNewsImage($r['image'], $items[$r['news_id']]['created'], 306);
                    }
                }
                //$pagging = Pagging::getPager(3, false, 'page_no', true);
            }
        }
        $data = array();
        if(!empty($cond_promotion)){
            $cond_promotion[] = "status = 1";
            $search_value = FunctionLib::addCondition($cond_promotion);
            $search_value = ($search_value != '') ? ' WHERE ' . $search_value : '';
            $order = 'created DESC';
            $sql = 'SELECT * FROM  ' . T_PROMOTION . $search_value . ' ORDER BY '.$order;
            $re = DB::query($sql);
            if ($re) {
                while ($r = mysql_fetch_assoc($re)) {
                    $r['image'] = $r['image_ngang'];
                    if(empty($r['image'])){
                        $r['image'] = $r['image_doc'];
                    }
                    $r['image'] = Promotion::getPromotionImage($r['image'], $r['created'], 306);
                    $r['type'] = 3;
                    $data[$r['id']] = Promotion::parser_promotion($r);
                }
            }
            if (!empty($data)) {
                $res = DB::query("SELECT * FROM " . T_PROMOTION_CITY . " WHERE promo_id IN (" . implode(',', array_keys($data)) . ")");
                while ($r = @mysql_fetch_assoc($res)) {
                    if (!isset($data[$r['promo_id']]['city'])) {
                        $data[$r['promo_id']]['city'] = array();
                    }
                    $data[$r['promo_id']]['city'][] = $r['city_id'];
                }
                $res = DB::query("SELECT * FROM " . T_PROMOTION_CARD . " WHERE promo_id IN (" . implode(',', array_keys($data)) . ")");
                while ($r = @mysql_fetch_assoc($res)) {
                    if (!isset($data[$r['promo_id']]['card'])) {
                        $data[$r['promo_id']]['card'] = array();
                    }
                    $data[$r['promo_id']]['card'][] = $r['card_id'];
                }
                $res = DB::query("SELECT * FROM " . T_PROMOTION_DES . " WHERE promo_id IN (" . implode(',', array_keys($data)) . ")");
                while ($r = @mysql_fetch_assoc($res)) {
                    $data[$r['promo_id']]['sort'] = $r['sort'];
                }
            }
        }
        if(!empty($data)){
            $items = array_merge($data,$items);
        }
        
        $display->add('items', $items);
        $display->add('time_now', TIME_NOW);
        $display->output('shopNews');
    }

}
