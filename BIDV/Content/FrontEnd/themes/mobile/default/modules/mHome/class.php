<?php

class mHome extends Module {

    static function permission() {
        return array();
    }

    function __construct($row) {
        Module::Module($row);

        require_once 'forms/shopHome.php';
        $this->add_form(new shopHomeForm());
    }

}
