<?php

class mFaqs extends Module {

    private $cmd = 'faqs', $action = '', $id = 0, $table = T_FEATURE;

    static function permission() {
        
    }

    function __construct($row) {
        Module::Module($row);
        require_once 'forms/shopFaqs.php';
        $this->add_form(new shopFaqsForm());
    }

}
