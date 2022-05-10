<?php

class shopHomeForm extends Form {

    function __construct() {
        $this->link_js(WEB_THEMES . 'website/default/modules/shopSubscribe/js/shopSubscribe.js', true);
    }

    function draw() {
        global $display;

        #Tin khuyến mại
        $cond = array();
        $cond[] = 'status = 1';
        $cond[] = 'is_home = 1';
        $cond[] = 'type = 1'; #Quan tri cho tin khuyen mai
        $cond[] = "time_to > " . TIME_NOW;
        $search_value = FunctionLib::addCondition($cond);
        $search_value = ($search_value != '') ? ' WHERE ' . $search_value : '';
        $sql = 'SELECT  * FROM  ' . T_NEWS . $search_value . ' ORDER BY dateof DESC LIMIT 8';
        $re = DB::query($sql);
        $newspromotion = array();
        if ($re) {
            while ($item = mysql_fetch_assoc($re)) {
                $newspromotion[$item['id']] = News::parser_new($item);
            }
            if (!empty($newspromotion)) {
                $res = DB::query("SELECT * FROM " . T_NEWS_IMAGE . " WHERE news_id IN (" . implode(',', array_keys($newspromotion)) . ") GROUP BY news_id ORDER BY type DESC");
                while ($r = @mysql_fetch_assoc($res)) {
                    $size = $r['type'] == 1 ? 286 : 245;
                    $newspromotion[$r['news_id']]['image'] = News::getNewsImage($r['image'], $newspromotion[$r['news_id']]['created'], 286);
                }
            }
        }
        $display->add('newspromotion', $newspromotion);

        #Tin tuc
        $cond = array();
        $cond[] = 'status = 1';
        $cond[] = 'is_home = 1';
        $cond[] = 'type = 0'; #Quan tri cho tin tuc
        $search_value = FunctionLib::addCondition($cond);
        $search_value = ($search_value != '') ? ' WHERE ' . $search_value : '';
        $sql = 'SELECT  * FROM  ' . T_NEWS . $search_value . ' ORDER BY dateof DESC LIMIT 6';
        $re = DB::query($sql);
        $news = array();
        if ($re) {
            while ($item = mysql_fetch_assoc($re)) {
                $news[$item['id']] = News::parser_new($item);
            }
            if (!empty($news)) {
                $res = DB::query("SELECT * FROM " . T_NEWS_IMAGE . " WHERE news_id IN (" . implode(',', array_keys($news)) . ") GROUP BY news_id ORDER BY type DESC");
                while ($r = @mysql_fetch_assoc($res)) {
                    $news[$r['news_id']]['image'] = News::getNewsImage($r['image'], $news[$r['news_id']]['created'], 306);
                }
            }
        }
        $display->add('news', $news);
        #End Tin tuc
        #Ưu đại vàng
        $cond = array();
        $cond[] = 'status = 1';
        $cond[] = 'is_home = 1';
        $cond[] = "time_to > " . TIME_NOW;
        $search_value = FunctionLib::addCondition($cond);
        $search_value = ($search_value != '') ? ' WHERE ' . $search_value : '';
        $sql = 'SELECT * FROM  ' . T_PROMOTION . $search_value . ' ORDER BY time_from DESC LIMIT 8';
        $re = DB::query($sql);
        $data_promotion = array();
        if ($re) {
            while ($r = mysql_fetch_assoc($re)) {
                $size = 286;
                $r['image'] = $r['image_doc'];
                if(empty($r['image'])){
                    $r['image'] = $r['image_ngang'];
                    $size = 245;
                }
                $r['image'] = Promotion::getPromotionImage($r['image'], $r['created'], $size);
                $data_promotion[$r['id']] = Promotion::parser_promotion($r);
            }
        }
        $display->add('data_promotion', $data_promotion);
        ##
        $color_menu = array(
            1 => '#d70c0c',
            2 => '#d59314',
            3 => '#4fa218',
            4 => '#06abce',
            5 => '#3c57a6',
            14 => '#a0a0a0',
            15 => '#a0a0a0',
        );

        $menu_build = array();
        $category = CGlobal::get('category', array());
        if (isset($category[0])) {
            foreach ($category[0] as $key => $value) {
                $value['data']['color'] = $color_menu[$value['data']['id']];
                if ($value['data']['active'] == 0) {
                    $menu_build[] = $value['data'];
                } else {
                    $value['data']['title'] = 'Dịch vụ khác';
                    $menu_build[88] = $value['data'];
                }
            }
        }
        $display->add('menu_build', $menu_build);
        ###
        $display->output('shopHome');
    }

}
