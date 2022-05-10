<?php

class mNews extends Module {

    static function permission() {
        
    }

    function __construct($row) {
        Module::Module($row);

        //public module
        $page = CGlobal::$current_page;
        if ($page == 'details') {
            require_once 'forms/shopDetails.php';
            $this->add_form(new shopDetailsForm());
        } else if ($page == 'search') {
            require_once 'forms/shopSearch.php';
            $this->add_form(new shopSearchForm());
        } else {
            require_once 'forms/shopNews.php';
            $this->add_form(new shopNewsForm());
        }
    }

}
