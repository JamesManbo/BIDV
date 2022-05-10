<?php

class shopIntroForm extends Form {

    function __construct() {
        
    }

    function draw() {
        global $display;
        $display->add("base_themes", WEB_THEMES . 'website/default/');
        $display->output('shopIntro');
    }

}
