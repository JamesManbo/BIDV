<?php

class shopDetailsForm extends Form {
    var $dg;
    function __construct() {
        $id = Url::getParamInt('id', 0);
        $this->dg = DB::fetch("SELECT * FROM " . T_CARD . " WHERE id=" . $id);
        if($this->dg['status']==-1){
            Url::redirect('home');
        }
    }

    function draw() {
        global $display;
        $id = Url::getParamInt('id', 0);

        //$dg = DB::fetch("SELECT * FROM " . T_CARD . " WHERE id=" . $id);
        $data_card_one = Card::parser_card($this->dg);
        
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
                if ($data_card_one['type_id'] == $value['data']['id']) {
                    $data = $value['data'];
                }
            }
        }
        $display->add('menu_build', $menu_build);
        #Ghet list
        if($data_card_one['type_id']==15||$data_card_one['type_id']==14){
            $sql = 'SELECT * FROM ' . T_CARD . ' WHERE status = 1 AND pid=0 AND type_id IN (14,15) ORDER BY weight ASC';
        }else{
            $sql = 'SELECT * FROM ' . T_CARD . ' WHERE status = 1 AND pid=0 AND type_id=' . $data_card_one['type_id'].' ORDER BY weight ASC';
        }
        $re = DB::query($sql);
        $data_card = array();
        while ($r = @mysql_fetch_assoc($re)) {
            $data_card[$r['id']] = Card::parser_card($r);
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
        
        $display->output('shopDetails_g');
    }

}
