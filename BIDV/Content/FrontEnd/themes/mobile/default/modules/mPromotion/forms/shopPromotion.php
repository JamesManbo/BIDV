<?php

class shopPromotionForm extends Form {

    function __construct() {
        $this->link_js(WEB_THEMES . 'website/default/javascript/select/classie.js', true);
        $this->link_js(WEB_THEMES . 'website/default/javascript/select/selectFx.js', true);
        $this->link_css(WEB_THEMES . 'website/default/javascript/select/cs-select.css', true);
        $this->link_css(WEB_THEMES . 'website/default/javascript/select/cs-skin-border.css', true);
    }

    function draw() {
        global $display;

        $cat_id = Url::getParamInt('cat', 0);
        $card = Url::getParamInt('card', 0);
        $city_id = Url::getParamInt('city_id', 0);
        $promo_per = Url::getParamInt('promo_per', -1);
        $sort = Url::getParamInt('sort', 0);
        $key = isset(CGlobal::$urlArgs[1])?CGlobal::$urlArgs[1]:'';
        
        $data = array();
        $cond = array("status = 1", "time_to > " . TIME_NOW);
        $per_cond = array("status = 1", "time_to > " . TIME_NOW);

        $paging = '';
        if ($cat_id > 0) {
            $cond[] = "cat_id = " . $cat_id;
            $per_cond[] = "cat_id = " . $cat_id;
        }
        if ($promo_per >= 0) {
            $cond[] = "promo_per = " . $promo_per;
        }elseif($promo_per == -2){
            $cond[] = "promo_per > 100";
        }
        
        $arr_q = array();
        if ($city_id > 0) {
            $res = DB::query("SELECT * FROM " . T_PROMOTION_CITY . " WHERE city_id =".$city_id);
            while ($r = @mysql_fetch_assoc($res)) {
                $arr_q[$r['promo_id']] = $r['promo_id'];
            }
        }
        if($card>0){
            $res = DB::query("SELECT * FROM " . T_PROMOTION_CARD . " WHERE card_id =".$card);
            while ($r = @mysql_fetch_assoc($res)) {
                $arr_q[$r['promo_id']] = $r['promo_id'];
            }
        }
        if($key!=''){
            $tag = Tags::getTagByKey($key);
            $res = DB::query("SELECT * FROM " . T_TAG_ITEM . " WHERE tag_id = ".$tag['id']." AND type = 2");
            while ($t = @mysql_fetch_assoc($res)) {
                $arr_q [$t['item_id']] = $t['item_id'];
            }
        }
        if(!empty($arr_q)){
            $cond[] = "id IN (" . implode(',', $arr_q).")";
            $per_cond[] = "id IN (" . implode(',', $arr_q).")";
        }
        
        $search_value = FunctionLib::addCondition($cond);
        $search_value = ($search_value != '') ? ' WHERE ' . $search_value : '';
        $item_per_page = 8;
        $order = 'time_from DESC';
        if($sort==2){
            $order = 'time_from ASC';
        }
        $sql = 'SELECT * FROM  ' . T_PROMOTION . $search_value . ' ORDER BY '.$order;
        $re = Pagging::pager_query($sql, $item_per_page);
        if ($re) {
            while ($r = mysql_fetch_assoc($re)) {
                $r['image'] = $r['image_ngang'];
                if(empty($r['image'])){
                    $r['image'] = $r['image_doc'];
                }
                $r['image'] = Promotion::getPromotionImage($r['image'], $r['created'], 222);
                $data[$r['id']] = Promotion::parser_promotion($r);
            }
            $paging = Pagging::getPager(3, false, 'page_no', true);
        }
        if (!empty($data)) {
            //lay tinh thanh
            $res = DB::query("SELECT * FROM " . T_PROMOTION_CITY . " WHERE promo_id IN (" . implode(',', array_keys($data)) . ")");
            while ($r = @mysql_fetch_assoc($res)) {
                if (!isset($data[$r['promo_id']]['city'])) {
                    $data[$r['promo_id']]['city'] = array();
                }
                $data[$r['promo_id']]['city'][] = $r['city_id'];
            }
            //lay danh sach the
            $res = DB::query("SELECT * FROM " . T_PROMOTION_CARD . " WHERE promo_id IN (" . implode(',', array_keys($data)) . ")");
            while ($r = @mysql_fetch_assoc($res)) {
                if (!isset($data[$r['promo_id']]['card'])) {
                    $data[$r['promo_id']]['card'] = array();
                }
                $data[$r['promo_id']]['card'][] = $r['card_id'];
            }
            //lay thong tin chi tiet
            $res = DB::query("SELECT * FROM " . T_PROMOTION_DES . " WHERE promo_id IN (" . implode(',', array_keys($data)) . ")");
            while ($r = @mysql_fetch_assoc($res)) {
                $data[$r['promo_id']]['sort'] = $r['sort'];
            }
        }
        $search_value = FunctionLib::addCondition($per_cond);
        $search_value = ($search_value != '') ? ' WHERE ' . $search_value : '';
        $promo_per_option = DB::fetch_all("SELECT promo_per FROM " . T_PROMOTION . " $search_value GROUP BY promo_per");
        
        //Phan tram khuyen mai
        $option_pp = '';
        if ($promo_per_option) {
            foreach ($promo_per_option as $key) {
                if($key['promo_per'] <= 100){
                    $option_pp .= '<option value="' . $key['promo_per'] . '"';
                    if ($key['promo_per'] == $promo_per) {
                        $option_pp .= ' selected';
                    }
                    $option_pp .= '>' . ($key['promo_per'] != 0 ? $key['promo_per'].' %' : 'Có ưu đãi') . '</option>';
                }
            }
            $option_pp.= '<option value="-2"'.($promo_per == -2 ? ' selected': '').'>Khác</option>';
        }

        //loai the
        $option_card = '';
        foreach (Category::getCategoryArr(0) as $key) {
            if($key['data']['active'] == 0){
                $option_card .= '<option value="' . $key['data']['id'] . '"';
                if ($key['data']['id'] == $card) {
                    $option_card .= ' selected';
                }
                $option_card .= '>' . $key['data']['title'] . '</option>';
            }
        }
        $city = CGlobal::$province_active;
        $res = DB::query("SELECT * FROM ". T_PROVINCE." WHERE status = 0 ORDER BY status DESC, safe_title ASC");
        while($r = @mysql_fetch_assoc($res)){
            $city[$r['id']] = $r;
        }
        
        $display->add('paging', $paging);
        $display->add('data', $data);
        $display->add('link_cate', Url::build('favorable'));
        $display->add('cat', $cat_id);
        $display->add('sort', $sort);
        $display->add('promo_per', $option_pp);
        $display->add('card', $option_card);
        $display->add('city', FunctionLib::getOptionHasIdTitle($city, $city_id));
        $display->add('currency', FunctionLib::getConfigFromDB('currency', 'đ', false, 'site_configs'));

        $this->beginForm(false, "GET");
        $display->output('shopPromotion');
        $this->endForm();
    }

}
