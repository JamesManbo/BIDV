<?php

class shopDetailPromotionForm extends Form {
    var $data;
    function __construct() {
        $this->link_js(WEB_THEMES . 'website/default/javascript/select/classie.js', true);
        $this->link_js(WEB_THEMES . 'website/default/javascript/select/selectFx.js', true);
        $this->link_css(WEB_THEMES . 'website/default/javascript/select/cs-select.css', true);
        $this->link_css(WEB_THEMES . 'website/default/javascript/select/cs-skin-border.css', true);
        
        $this->getData();
        if(empty($this->data)){
            Url::not_found();
        }
        
        FunctionLib::facebookMetaDataSet(array(
            'title' => "Ưu đãi vàng: ".$this->data['title'],
            'link' => $this->data['link'],
            'image' => $this->data['image'],
            'des' => $this->data['sort']
        ));
    }

    function draw() {
        global $display;
        $id = $this->data['id'];
        $data = $this->data;
        
        //update luot xem
        DB::query("UPDATE ".T_PROMOTION." SET view=view+1 WHERE id = $id");
        
        //bo loc gia giam
        $promo_per_option = DB::fetch_all("SELECT promo_per FROM " . T_PROMOTION . " WHERE status=1 GROUP BY promo_per");
        $option_pp = '';
        $promo_per = 0; #mac dinh
        if ($promo_per_option) {
            foreach ($promo_per_option as $key) {
                if($key['promo_per'] <= 100){
                    $option_pp .= '<option value="' . $key['promo_per'] . '"';
                    $option_pp .= '>' . ($key['promo_per'] != 0 ? $key['promo_per'].' %' : 'Có ưu đãi') . '</option>';
                }
            }
            $option_pp.= '<option value="-2">Khác</option>';
        }

        //bo lo the
        $option_card = '';
        foreach (Category::getCategoryArr(0) as $key) {
            if($key['data']['active'] == 0){
                $option_card .= '<option value="' . $key['data']['id'] . '"';
                $option_card .= '>' . $key['data']['title'] . '</option>';
            }
        }

        $display->add('data', $data);
        $display->add('link_cate', Url::build('favorable'));
        $display->add('cat', $data['cat_id']);
        $display->add('promo_per', $option_pp);
        $display->add('card', $option_card);
        $display->add('city', FunctionLib::getOptionCity(0, true));

        #Uu dai noi bat
        $cond = array();
        $cond[] = "status = 1 AND is_hot = 1";
        $search_value = FunctionLib::addCondition($cond);
        $search_value = ($search_value != '') ? ' WHERE ' . $search_value : '';
        $item_per_page = 8;
        $order = ' ORDER BY time_from DESC';
        $sql = 'SELECT * FROM  ' . T_PROMOTION . $search_value . $order. ' LIMIT 4';
        $r_nb = DB::query($sql);
        $item_nb = array();
        if ($r_nb) {
            while ($r = mysql_fetch_assoc($r_nb)) {
                $r['image'] = $r['image_ngang'];
                if(empty($r['image'])){
                    $r['image'] = $r['image_doc'];
                }
                $r['image'] = Promotion::getPromotionImage($r['image'], $r['created'], 80);
                $item_nb[$r['id']] = Promotion::parser_promotion($r);
            }
        }
        
        #Tu khoa
        $arrTag =$arrTagID = array();
        $res = DB::query("SELECT * FROM " . T_TAG_ITEM . " WHERE item_id = ".$id." AND type = 2");
        while ($t = @mysql_fetch_assoc($res)) {
            $arrTagID[$t['tag_id']] = $t['tag_id'];
        }
        if (!empty($arrTagID)) {
            $res = DB::query("SELECT * FROM " . T_TAG . " WHERE status = 1 AND id IN (" . implode(',', $arrTagID) . ")");
            while ($tag = @mysql_fetch_assoc($res)) {
                $arrTag[$tag['id']] = Tags::parser_Tag($tag);
            }
        }
        $display->add('time_now', TIME_NOW);
        $display->add('arrTag', $arrTag);
        $display->add('item_nb', $item_nb);
        $display->output('shopDetailPromotion');
    }

    function getData(){
        $id = Url::getParamInt('id', 0);
        $cond[] = "id = " . $id;
        $cond[] = "status = 1";
        $search_value = FunctionLib::addCondition($cond);
        $search_value = ($search_value != '') ? ' WHERE ' . $search_value : '';
        $data = DB::fetch('SELECT * FROM  ' . T_PROMOTION . $search_value);
        if(!empty($data)){
            $data = Promotion::parser_promotion($data);
            $limit = 8;
            $data['image'] = array();
            if(!empty($data['image_ngang'])){
                $data['image'][] = array(
                    'big' => Promotion::getPromotionImage($data['image_ngang'], $data['created'], 670),
                    'small' => Promotion::getPromotionImage($data['image_ngang'], $data['created'], 80)
                );
                $limit--;
            }
            $res = DB::query("SELECT * FROM " . T_PROMOTION_IMG . " WHERE promo_id =" . $id . " ORDER BY type DESC LIMIT 0, $limit");
            while ($r = @mysql_fetch_assoc($res)) {
                $data['image'][] = array(
                    'big' => Promotion::getPromotionImage($r['image'], $data['created'], 670),
                    'small' => Promotion::getPromotionImage($r['image'], $data['created'], 80)
                );
            }
            $res = DB::query("SELECT * FROM " . T_PROMOTION_CITY . " WHERE promo_id =" . $id);
            while ($r = @mysql_fetch_assoc($res)) {
                if (!isset($data['city'])) {
                    $data['city'] = array();
                }
                $data['city'][] = $r['city_id'];
            }
            $res = DB::query("SELECT * FROM " . T_PROMOTION_CARD . " WHERE promo_id =" . $id);
            while ($r = @mysql_fetch_assoc($res)) {
                if (!isset($data[$r['promo_id']]['card'])) {
                    $data['card'] = array();
                }
                $data['card'][] = $r['card_id'];
            }
            $res = DB::query("SELECT * FROM " . T_PROMOTION_DES . " WHERE promo_id =" . $id);
            while ($r = @mysql_fetch_assoc($res)) {
                $data['body'] = StringLib::post_db_parse_html($r['body']);
                $data['sort'] = StringLib::post_db_parse_html($r['sort']);
                $data['location'] = StringLib::post_db_parse_html($r['location']);
            }
        }
        $this->data = $data;
    }
}
