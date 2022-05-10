<?php

class mNewsPromotion extends Module {

    static function permission() {
        
    }

    function __construct($row) {
        Module::Module($row);
        //public module
        $page = CGlobal::$current_page;
        if ($page == 'newsdetailpromotion') {
            require_once 'forms/shopDetails.php';
            $this->add_form(new shopDetailsForm());
        } else {
            require_once 'forms/shopNews.php';
            $this->add_form(new shopNewsForm());
        }
    }

}
