<?php

class mCategory extends Module {

    static function permission() {
        return array();
    }

    function __construct($row) {
        Module::Module($row);

        require_once 'forms/shopCategory.php';
        $this->add_form(new shopCategoryForm());
    }

}
