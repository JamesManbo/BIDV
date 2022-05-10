<?php

class HeaderForm extends Form {

    function __construct() {
        $this->link_css("https://fonts.googleapis.com/icon?family=Material+Icons", true);
        $this->link_js(WEB_THEMES . 'website/default/javascript/jcarousel-0.3.1/jquery.jcarousel.min.js', true);
        $this->link_js(WEB_THEMES . 'website/default/javascript/jcarousel-0.3.1/modernizr.js', true);
        $this->link_css(WEB_THEMES . 'website/default/javascript/blueimp-gallery.min.css', true);
        $this->link_js(WEB_THEMES . 'website/default/javascript/blueimp-gallery.min.js', true);
        $this->link_js(WEB_THEMES . 'website/default/javascript/jquery.blueimp-gallery.min.js', true);
        $this->link_js(WEB_THEMES . 'mobile/default/javascript/bidv.js', true);
    }

    function draw() {
        global $display;

        #menu
        //get menu
        $page = CGlobal::$urlArgs[0];
        $menu = FunctionLib::getMenu(1);
        foreach ($menu as $k => $v) {
            if ($menu[$k]['parent'] == 0 && ($v['link'] == $page || stripos($v['link'], $page . '/') !== false || stripos($v['link'], $page . '.html') !== false)) {
                $menu[$k]['active'] = true;
            }
            //check menu con xem co active khong
            if (!empty($v['items'])) {
                foreach ($v['items'] as $kk => $vv) {
                    if (($vv['link'] == $page || stripos($vv['link'], $page . '/') !== false || stripos($vv['link'], $page . '.html') !== false)) {
                        $menu[$k]['active'] = true;
                    }
                }
            }
        }
        $display->add('menu', $menu);
        
        #Load info
        $site_configs = isset(CGlobal::$configs['site_configs']) ? @unserialize(CGlobal::$configs['site_configs']) : array();
        $display->add('link_facebook',isset($site_configs['link_facebook']) ? $site_configs['link_facebook'] : '');
        $display->add('add',isset($site_configs['add']) ? $site_configs['add'] : '');
        $display->add('link_in',isset($site_configs['link_in']) ? $site_configs['link_in'] : '');
        $display->add('link_youtube',isset($site_configs['link_youtube']) ? $site_configs['link_youtube'] : '');
        $display->add('email',isset($site_configs['email']) ? $site_configs['email'] : '');
        $display->add('phonesupport',isset($site_configs['email_order']) ? $site_configs['email_order'] : '');
        $display->add('hotlinefax',isset($site_configs['email_marketing']) ? $site_configs['email_marketing'] : '');
        
        $is_mobile = mobile_device_detect();

        $display->add('cur_page', CGlobal::$current_page);
        $display->add('cur_lang', Language::$activeLang);
        $display->add('langs', Language::$listLangOptions);

        //chi cho phep chon option nay khi dang vao web bang mobile
        $display->add('canSetMode', is_array($is_mobile) && $is_mobile[0] == true && CookieLib::get_cookie('websiteMode', '') != '');
        $display->add('is_mobile', $is_mobile);
        
        $display->add('search_key', Url::getParam("search_key",""));
        $optioncheck = Url::getParamInt('optioncheck', 0);
        $display->add('optioncheck', $optioncheck);
        //config default for all site content
        $display->add('base_url', WEB_ROOT);
        $display->add('site_name', CGlobal::$site_name);
        $display->add('site_title', CGlobal::$website_title);
        $display->add('logo', CGlobal::$logo);
        $display->add('logo_size', CGlobal::$logo_size);
        $display->add('logo_title', CGlobal::$logo_title != '' ? CGlobal::$logo_title : '');
        $display->add('blank_image', 'style/images/blank.gif');
        
        $display->output("Header_bv");
    }

}
