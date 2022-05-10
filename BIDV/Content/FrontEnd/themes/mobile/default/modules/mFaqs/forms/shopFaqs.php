<?php

class shopFaqsForm extends Form {

    function __construct() {
        $this->link_js(WEB_THEMES . 'mobile/default/modules/mFaqs/js/shop_faqs.js', true);
    }

    function draw() {
        global $display;
        $category = CGlobal::get('category', array());
        $cat_id = Url::getParamInt('cat_id', 0);
        $cond = array();
        $cond[] = 'status = 1';
        $cond[] = "lang = '" . Language::$activeLang . "'";
        if ($cat_id > 0) {
            if (isset($category[4])) {
                $cat = $category[4];
                if (isset($cat[$cat_id])) {
                    $cat_all = DB::fetch_all("SELECT * FROM " . T_CATEGORY . " WHERE parent_id=" . $cat_id);
                    $query[$cat_id] = $cat_id;
                    if (!empty($cat_all)) {
                        foreach ($cat_all as $key => $value) {
                            $query[$value['id']] = $value['id'];
                        }
                    }
                    $cond[] = 'cat_id IN (' . implode(',', $query) . ')';
                } else {
                    foreach ($cat as $key => $value) {
                        if (isset($value['items'])) {
                            foreach ($value['items'] as $k => $val) {
                                if ($val['id'] == $cat_id) {
                                    $cond[] = 'cat_id = ' . $cat_id;
                                }
                            }
                        }
                    }
                }
            }
        }
        $items = array();
        $search_value = FunctionLib::addCondition($cond);
        $search_value = ($search_value != '') ? ' WHERE ' . $search_value : '';
        $sql = 'SELECT  * FROM  ' . T_FAQS . $search_value . ' ORDER BY created DESC LIMIT 200';
        $paging = '';
        $re = DB::query($sql);
        if ($re) {
            while ($item = mysql_fetch_assoc($re)) {
                $item['answer'] = StringLib::post_db_parse_html($item['answer']);
                $items[$item['id']] = $item;
            }
        }
        $display->add('news', $items);
        $display->add('paging', $paging);


        $cat = array();
        $query = array();
        if (isset($category[4])) {
            $cat = $category[4];
            foreach ($cat as $k => &$val) {
                $query[$val['data']['id']][] = $val['data']['id'];
                if ($val['data']['id'] == $cat_id) {
                    $val['data']['active'] = 'active';
                }
                $val['data']['link'] = Url::build('faqs', false, '?cat_id=' . $val['data']['id']);
                if (isset($val['items'])) {
                    foreach ($val['items'] as $v => $item) {
                        if ($item['id'] == $cat_id) {
                            $val['items'][$item['id']]['active'] = 'active';
                            $val['data']['active'] = 'active';
                        }
                        $val['items'][$item['id']]['link'] = Url::build('faqs', false, '?cat_id=' . $item['id']);
                        $query[$val['data']['id']][] = $item['id'];
                    }
                }
            }
        }

        $display->add('cats', $cat);
        $display->output('shopFaqs');
    }

}
