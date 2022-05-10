<?php

class FooterForm extends Form {

    function __construct() {
        $this->link_js_footer('https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false', true);
    }

    function draw() {
        global $display;
        $page = CGlobal::$current_page;
        $menu = FunctionLib::getMenu(2);
        foreach ($menu as $k => $v) {
            if ($menu[$k]['parent'] == 0 && ($v['link'] == $page || stripos($v['link'], $page . '/') !== false || stripos($v['link'], $page . '.html') !== false)) {
                $menu[$k]['active'] = true;
                break;
            }
        }
        $display->add('footer_menu', $menu);
        $display->output('Footer_bv');
    }

}
