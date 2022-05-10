<?php

class shopDetailsForm extends Form {
    var $data;
    function __construct() {
        $this->link_js(WEB_THEMES . 'website/default/modules/ShopAttach/js/attach.js', true);
        //$this->link_js(WEB_THEMES.'website/default/modules/shopNews/js/news.js', true);
        $this->getData();
        if(empty($this->data)){
            Url::not_found();
        }
        $this->data['des'] = $this->data['sort_body'];
        FunctionLib::facebookMetaDataSet($this->data);
    }

    function draw() {
        global $display;
        $id = $this->data['id'];
        $data = $this->data;

        //update luot xem
        DB::query("UPDATE ".T_NEWS." SET view=view+1 WHERE id = $id");
        
        $arrlinequan = array();
        $arrTag = array();
        $arrTagID = array();
        if (!empty($data)) {
            $res = DB::query("SELECT * FROM " . T_TAG_ITEM . " WHERE item_id = " . $data['id'] . " AND type = 0");
            while ($t = @mysql_fetch_assoc($res)) {
                $arrTagID[$t['tag_id']] = $t['tag_id'];
            }
            $tag_promotion = array();
            if (!empty($arrTagID)) {
                $res = DB::query("SELECT * FROM " . T_TAG . " WHERE status = 1 AND id IN (" . implode(',', $arrTagID) . ")");
                while ($tag = @mysql_fetch_assoc($res)) {
                    $tag_promotion[$tag['tag']] = $tag['tag'];
                    $arrTag[$tag['id']] = Tags::parser_Tag($tag);
                }
                $id_tag_promotion = array();
                #Tin uu dai
                if (!empty($tag_promotion)) {
                    $res = DB::query("SELECT * FROM " . T_TAG . " WHERE status = 1 AND type=2 AND tag IN ('" . implode("','", $tag_promotion) . "')");
                    if ($res) {
                        while ($tag = @mysql_fetch_assoc($res)) {
                            $id_tag_promotion[$tag['id']] = $tag['id'];
                        }
                        if (!empty($id_tag_promotion)) {
                            $arrTag_idpromotion = array();
                            $res = DB::query("SELECT * FROM " . T_TAG_ITEM . " WHERE tag_id IN ( " . implode(',', $id_tag_promotion) . ") AND type = 2");
                            if ($res) {
                                while ($t = @mysql_fetch_assoc($res)) {
                                    $arrTag_idpromotion[$t['item_id']] = $t['item_id'];
                                }
                                if (!empty($arrTag_idpromotion)) {
                                    $res = DB::query("SELECT * FROM " . T_PROMOTION . " WHERE status > 0 AND id IN ( " . implode(',', $arrTag_idpromotion) . ")");
                                    while ($t = @mysql_fetch_assoc($res)) {
                                        $arrlinequan[$t['created']] = Promotion::parser_promotion($t);
                                    }
                                }
                            }
                        }
                    }
                }
                #Tin lien quan
                $arrTag_iditem = array();
                $res = DB::query("SELECT * FROM " . T_TAG_ITEM . " WHERE tag_id IN ( " . implode(',', $arrTagID) . ") AND type = 0");
                while ($t = @mysql_fetch_assoc($res)) {
                    $arrTag_iditem[$t['item_id']] = $t['item_id'];
                }
                if (!empty($arrTag_iditem)) {
                    $res = DB::query("SELECT * FROM " . T_NEWS . " WHERE status > 0 AND id IN ( " . implode(',', $arrTag_iditem) . ") AND id!=" . $data['id']." ORDER BY dateof DESC");
                    while ($t = @mysql_fetch_assoc($res)) {
                        $arrlinequan[$t['created']] = News::parser_new($t);
                    }
                }
            }
        }
        $display->add('data', $data);
        $display->add('arrTag', $arrTag);
        rsort($arrlinequan);
        $display->add('arrlinequan', $arrlinequan);
        $display->add('time_now', TIME_NOW);
        $display->output('shopDetails');
    }

    function getData(){
        $id = Url::getParamInt('id', 0);
        $data = DB::fetch('SELECT * FROM  ' . T_NEWS . ' WHERE id = '.$id.' AND status = 1');
        if(!empty($data)){
            $this->data = News::parser_new($data);
        }
    }
}
