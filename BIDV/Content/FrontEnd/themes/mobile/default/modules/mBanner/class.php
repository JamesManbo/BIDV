<?php

class mBanner extends Module {

    static function permission() {
        return array();
    }

    function __construct($row) {
        Module::Module($row);
        require_once 'forms/shopBanner.php';
        $this->add_form(new shopBannerForm());
    }

}
