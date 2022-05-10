<?php

class mPromotion extends Module {

    static function permission() {
        
    }

    function __construct($row) {
        Module::Module($row);

        $page = CGlobal::$current_page;
        if ($page == 'detailspromotion') {
            require_once 'forms/shopDetailPromotion.php';
            $this->add_form(new shopDetailPromotionForm());
        } else {
            require_once 'forms/shopPromotion.php';
            $this->add_form(new shopPromotionForm());
        }
    }

}
