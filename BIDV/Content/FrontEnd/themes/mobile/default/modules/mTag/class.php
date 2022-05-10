<?php

class mTag extends Module {

    static function permission() {
        
    }

    function __construct($row) {
        Module::Module($row);
        require_once 'forms/shopNews.php';
        $this->add_form(new shopNewsForm());
    }

}
