<?php

class shopRightNewsProForm extends Form {

    function __construct() {
        
    }

    function draw() {
        global $display;
        $id = Url::getParamInt('id',0);
        $arrTag = array();
        $res = DB::query("SELECT * FROM " . T_TAG . " WHERE status = 1 AND is_hot=1 AND type=1");
        while ($tag = @mysql_fetch_assoc($res)) {
            $arrTag[$tag['id']] = Tags::parser_Tag($tag);
        }
        $display->add('arrTag', $arrTag);
        #Tin tuc
        $cond = array();
        $cond[] = 'status = 1';
        $cond[] = 'is_hot = 1';
        $cond[] = 'id != '.$id;
        $cond[] = 'type = 1'; #Quan tri cho tin tuc
        $cond[] = "time_to > " . TIME_NOW;
        $search_value = FunctionLib::addCondition($cond);
        $search_value = ($search_value != '') ? ' WHERE ' . $search_value : '';
        $sql = 'SELECT  * FROM  ' . T_NEWS . $search_value . ' ORDER BY dateof DESC LIMIT 4';
        $re = DB::query($sql);
        $news = array();
        if ($re) {
            while ($item = mysql_fetch_assoc($re)) {
                $news[$item['id']] = News::parser_new($item);
            }
            if (!empty($news)) {
                $res = DB::query("SELECT * FROM (SELECT * FROM " . T_NEWS_IMAGE . " WHERE news_id IN (" . implode(',', array_keys($news)) . ") ORDER BY type ASC) as A GROUP BY A.news_id");
                while ($r = @mysql_fetch_assoc($res)) {
                    $news[$r['news_id']]['image'] = News::getNewsImage($r['image'], $news[$r['news_id']]['created'], 80);
                }
            }
        }
        $display->add('news', $news);
        $display->output('shopRightNews');
    }
}
