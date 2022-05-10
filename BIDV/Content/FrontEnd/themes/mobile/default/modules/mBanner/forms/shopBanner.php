<?php

class shopBannerForm extends Form {

    function __construct() {
        
    }

    function draw() {
        global $display;
        $page = CGlobal::$current_page;
        if ($page == 'gioithieu') {
            $page = 'home';
        }
        if ($page == 'category') {
            $id= Url::getParamInt('id',0);
            $con[] = '`status` = 1';
            $con[] = '`id` = '.$id;
            $ser = FunctionLib::addCondition($con);
            $res = DB::query("SELECT * FROM " . T_CATEGORY . " WHERE  " . $ser );
            $data = array();
            while ($row = @mysql_fetch_assoc($res)) {
                $row['image'] = Category::getCategoryImage($row['image'], $row['created'], 1500);
                $row['link'] = 'javascript:void(0)';
                $row['body'] = StringLib::post_db_parse_html($row['nut']);
                if($id == 15){
                    $row['title'] = 'Dịch vụ khác';
                }
                $data[] = $row;
            }
        } elseif ($page == 'dichvuthe') {
            $id= Url::getParamInt('id',0);
            $feach =DB::fetch("SELECT type_id FROM " . T_CARD . " WHERE  id=" . $id,'type_id');
            $con[] = '`status` = 1';
            $con[] = '`id` = '.$feach;
            $ser = FunctionLib::addCondition($con);
            $res = DB::query("SELECT * FROM " . T_CATEGORY . " WHERE  " . $ser );
            $data = array();
            while ($row = @mysql_fetch_assoc($res)) {
                $row['image'] = Category::getCategoryImage($row['image'], $row['created'], 1500);
                $row['link'] = 'javascript:void(0)';
                $row['body'] = StringLib::post_db_parse_html($row['nut']);
                $data[] = $row;
            }
        } else {
            $con[] = '`status` = 1';
            $con[] = '`show` = 1';
            $con[] = 'type = 1';
            $con[] = "url_show = '$page'";
            $ser = FunctionLib::addCondition($con);
            $res = DB::query("SELECT * FROM " . T_FEATURE . " WHERE  " . $ser . " ORDER BY weight ASC, created DESC");
            $data = array();
            while ($row = @mysql_fetch_assoc($res)) {
                $row['image'] = Feature::getFeatureImage($row['image'], $row['created'], 1300);
                $row['link'] = !empty($row['link']) ? $row['link'] : '';
                $row['body'] = StringLib::post_db_parse_html($row['body']);
                $data[] = $row;
            }
        }
        $display->add("banner2", $data);
        $display->add("id", Url::getParamInt('id', 0));
        $display->add("banner_total", count($data));
        $display->add("page_dvthe", Url::build('category'));
        $display->add('page', CGlobal::$current_page);
        $display->output('shopBanner');
    }

}
