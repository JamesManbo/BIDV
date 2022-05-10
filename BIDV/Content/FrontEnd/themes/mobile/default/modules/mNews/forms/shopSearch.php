<?php

class shopSearchForm extends Form {

    function __construct() {
        
    }

    function draw() {

        global $display;

        $optioncheck = Url::getParamInt('optioncheck', 0);
        $search_key = Url::getParam('search_key', '');
        $cond[] = 'status = 1';
        $pagging = '';
        $items = array();
        if ($optioncheck == 3) {
            $cond[] = 'title LIKE "%' . $search_key . '%"';
            $search_value = FunctionLib::addCondition($cond);
            $search_value = ($search_value != '') ? ' WHERE ' . $search_value : '';
            $item_per_page = 10;
            $order = 'time_from DESC';
            $sql = 'SELECT * FROM  ' . T_PROMOTION . $search_value . ' ORDER BY ' . $order;
            $re = Pagging::pager_query($sql, $item_per_page);
            if ($re) {
                while ($r = mysql_fetch_assoc($re)) {
                    $r['image'] = $r['image_ngang'];
                    if (empty($r['image'])) {
                        $r['image'] = $r['image_doc'];
                    }
                    $r['image'] = Promotion::getPromotionImage($r['image'], $r['created'], 222);
                    $r['sort_body'] = 'Ninh';
                    $items[$r['id']] = Promotion::parser_promotion($r);
                }
                $pagging = Pagging::getPager(3, false, 'page_no', true);
            }
            if (!empty($items)) {
                //lay thong tin chi tiet
                $res = DB::query("SELECT * FROM " . T_PROMOTION_DES . " WHERE promo_id IN (" . implode(',', array_keys($items)) . ")");
                while ($r = @mysql_fetch_assoc($res)) {
                    $items[$r['promo_id']]['sort_body'] = $this->makeHighlighter($search_key, html_entity_decode($r['sort'], ENT_COMPAT, 'UTF-8'));
                }
            }
        } else {
            $cond[] = 'title LIKE "%' . $search_key . '%" OR sort_body LIKE "%' . $search_key . '%"';
            $cond[] = 'type = ' . $optioncheck;
            $search_value = FunctionLib::addCondition($cond);
            $search_value = ($search_value != '') ? ' WHERE ' . $search_value : '';
            $sql = 'SELECT  * FROM  ' . T_NEWS . $search_value . ' ORDER BY created DESC';
            $re = Pagging::pager_query($sql, 10);

            if ($re) {
                while ($item = mysql_fetch_assoc($re)) {
                    $items[$item['id']] = News::parser_new($item);
                    $items[$item['id']]['sort_body'] = $this->makeHighlighter($search_key, html_entity_decode($items[$item['id']]['sort_body'], ENT_COMPAT, 'UTF-8'));

                    $items[$item['id']]['title'] = preg_replace('@' . preg_quote(mb_strtoupper($search_key, 'UTF-8')) . '@i', '<span style="background-color:yellow">$0</span>', mb_strtoupper($items[$item['id']]['title'], 'UTF-8'));
                }
                if (!empty($items)) {
                    $res = DB::query("SELECT * FROM (SELECT * FROM " . T_NEWS_IMAGE . " WHERE news_id IN (" . implode(',', array_keys($items)) . ") ORDER BY type ASC) as A GROUP BY A.news_id");
                    while ($r = @mysql_fetch_assoc($res)) {
                        $items[$r['news_id']]['image'] = News::getNewsImage($r['image'], $items[$r['news_id']]['created'], 306);
                    }
                }
                $pagging = Pagging::getPager(3, false, 'page_no', true);
            }
        }

        $display->add('search_key', $search_key);
        $display->add('items', $items);
        $display->add('time_now', TIME_NOW);
        $display->add('pagging', $pagging);
        $display->add('total', Pagging::$totalResult);
        $display->output('shopSearch');
    }

    function makeHighlighter($keyword, $field) {
        $i = strripos($field, $keyword);
        if ($i !== false) {
            $keyword = str_ireplace($keyword, substr($field, $i, (strlen($keyword))), $keyword);
        } else {
            return FunctionLib::truncate($field, 3800);
        }
        $as_unm_split = explode($keyword, $field);
        $string_hig = "";
        if (isset($as_unm_split[0])) {
            $string_hig = '...';
            $string_hig .= $this->trun_htmlRight($as_unm_split[0], 20);
            $string_hig .= "<span style=\"background-color:yellow\"> " . $keyword . "</span>";
            if (isset($as_unm_split[1])) {
                $string_hig .= FunctionLib::truncate($as_unm_split[1], 3800);
            }
        }
//		for ($i = 0; $i < count($as_unm_split); $i++) {
//			if ($i < count($as_unm_split) - 1)
//				$string_hig.=$as_unm_split[$i] . "<span style=\"color: red\">" . $keyword . "</span>";
//			else
//				$string_hig.=$as_unm_split[$i];
//		}
        unset($as_unm_split, $keyword, $i);
        return $string_hig;
    }

    function trun_htmlRight($string, $limit) {
        $string = strip_tags($string);
        $arr_space = explode(' ', $string);
        if (count($arr_space) > $limit) {
            $temp = '';
            for ($index = count($arr_space) - $limit; $index < count($arr_space); $index++) {
                $temp .= ' ' . $arr_space[$index];
            }
            return $temp;
        } else {
            return $string;
        }
    }

}
