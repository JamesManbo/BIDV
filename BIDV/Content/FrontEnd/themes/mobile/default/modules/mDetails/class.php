<?php

class mDetails extends Module {

    static function permission() {
        return array();
    }

    function __construct($row) {
        Module::Module($row);

        require_once 'forms/shopDetails.php';
        $this->add_form(new shopDetailsForm());
    }

}
