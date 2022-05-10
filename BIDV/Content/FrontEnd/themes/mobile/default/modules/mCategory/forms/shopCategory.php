<?php

class shopCategoryForm extends Form {

    function __construct() {
        //dinh kem file
        Attachs::addJS($this);
    }

    function draw() {
        global $display;

        $id = Url::getParamInt('id', 0);

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
                //$value['data']['color'] = $color_menu[$value['data']['id']];
                if ($value['data']['active'] == 0) {
                    $menu_build[] = $value['data'];
                } else {
                    $value['data']['title'] = 'Dịch vụ khác';
                    $menu_build[88] = $value['data'];
                }
                if ($id == $value['data']['id']) {
                    $data = $value['data'];
                }
            }
        }
        $display->add('menu_build', $menu_build);
        #Ghet list
        if($id==15||$id==14){
            $sql = 'SELECT * FROM ' . T_CARD . ' WHERE status = 1 AND pid=0 AND type_id IN (14,15) ORDER BY weight ASC';
        }else{
            $sql = 'SELECT * FROM ' . T_CARD . ' WHERE status = 1 AND pid=0 AND type_id=' . $id.' ORDER BY weight ASC';
        }
        $re = DB::query($sql);
        $data_card = array();
        $data_card_one = array();
        while ($r = @mysql_fetch_assoc($re)) {
            $data_card[$r['id']] = Card::parser_card($r);
            if (empty($data_card_one)) {
                $data_card_one = Card::parser_card($r);
            }
        }
        $data_group = array();
        if (!empty($data_card_one)) {
            $dg = DB::query("SELECT * FROM " . T_CARD . " WHERE status = 1 AND pid=" . $data_card_one['id']." ORDER BY weight ASC");

            if ($dg) {
                while ($r = @mysql_fetch_assoc($dg)) {
                    $data_group[$r['id']] = Card::parser_card($r);
                }
            }
        }
        ###
        $display->add('data', $data);
        $display->add('data_group', $data_group);
        $display->add('data_card', $data_card);
        $display->add('data_card_one', $data_card_one);
        $display->output('shopCategory_g');
    }

}