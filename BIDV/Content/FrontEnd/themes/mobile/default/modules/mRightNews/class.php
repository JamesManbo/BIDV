<?php

class mRightNews extends Module {

    static function permission() {
        return array();
    }

    function __construct($row) {
        Module::Module($row);
        $page = CGlobal::$current_page;
        if ($page == 'newsdetailpromotion') {
            require_once 'forms/shopRightNewsPro.php';
            $this->add_form(new shopRightNewsProForm());
        } else {
            require_once 'forms/shopRightNews.php';
            $this->add_form(new shopRightNewsForm());
        }
    }

}
