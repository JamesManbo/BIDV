<?php

class mIntro extends Module {

    static function permission() {
        return array();
    }

    function __construct($row) {
        Module::Module($row);

        require_once 'forms/shopIntro.php';
        $this->add_form(new shopIntroForm());
    }

}
