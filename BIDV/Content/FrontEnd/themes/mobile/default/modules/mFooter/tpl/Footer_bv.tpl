<div class="bv_info_foot">
    <div class="bv_box_cus">
    <ul>
        <li>
            <a target="_blank" href="http://bidv.com.vn/chinhanh/Ban-do.aspx"><i class="bv_ic_map"></i>&nbsp;&nbsp;&nbsp;&nbsp;Điểm đặt ATM</a>
        </li>
        <li>
            <a target="_blank" href="https://ebank.bidv.com.vn/DKT/"><i class="bv_ic_note"></i>&nbsp;&nbsp;&nbsp;Đăng ký phát hành thẻ</a>
        </li>
        <li>
            <a target="_blank" href="{if $is_mobile}tel:{$phonesupport}{else}javascript:void(0){/if}"><i class="bv_ic_phone_big"></i>&nbsp;&nbsp;&nbsp;&nbsp;Hỗ trợ 24/7{if !$is_mobile} (Hotline: {$phonesupport}){/if}</a>
        </li>
        <li>
            <a href="cau-hoi-thuong-gap.html"><i class="bv_ic_comment"></i>&nbsp;&nbsp;&nbsp;Câu hỏi thường gặp</a>
        </li>
    </ul>
</div>
    <div class="bv_info_foot_build">
        <div class="bv_contain">

            <div class="bv_info_foot_build_left">
                <a class="bv_info_foot_build_l_link" href="{$base_url}">
                    <img src="dev/Logo-Dichvuthe-03.png"/>
                </a>
                <div class="c"></div>
                <ul class="bv_info_foot_build_l_infoo">
                    <li><span><i class="bv_ic_comment_adr"></i></span><span class="bv_ic_comment_adr_text">{$add}</span></li>
                    <li><span><i class="bv_ic_comment_homefasch"></i></span>{$hotlinefax}   |  {$phonesupport}</li>
                    <li><span><i class="bv_ic_comment_homephone"></i></span>{$hotlinefax}</li>
                    <li><span><i class="bv_ic_comment_emailtwo"></i></span>{$email}</li>
                </ul>
            </div>
            <div class="c"></div>
        </div>
        <div class="bv_info_foot_build_right">
            <div id="map-canvas" style="height:204px;width:100%"></div>
        </div>
    </div>
</div>
<div class="bv_info_foot_bot">
    <div class="bv_info_foot_bot_padding bv_info_foot_bot_padding_m">
        <a href="javascript:shop.changeMode(1)" class="bv_phienbandex">
            <i class="material-icons">tv</i>&nbsp;&nbsp;&nbsp;Xem bản Desktop
        </a>
        <a onclick="{literal}jQuery('html,body').animate({'scrollTop': 0},1000);{/literal}"  class="bv_phienbandex_top" href="javascript:void(0)">&#xf106;</a>
    </div>
    <div class="bv_info_foot_bot_padding">
        <div class="bv_contain">
            <div class="bv_info_foot_bot_copy">Copyright © 2016 BIDV</div>
            <div class="c"></div>
        </div>
    </div>
</div>
</div>
<div id="blueimp-gallery" class="blueimp-gallery" data-use-bootstrap-modal="false" data-transition-speed="0" data-full-screen="false">
    <div class="slides"></div>
    <h3 class="title"></h3>
    <div class="box_prev"></div>
    <a class="prev">‹</a>
    <div class="box_next"></div>
    <a class="next">›</a>
    <a class="close">×</a>
    <a class="play-pause"></a>
    <ol class="indicator"></ol>
    <div class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" aria-hidden="true">&times;</button>
                    <h4 class="modal-title"></h4>
                </div>
                <div class="modal-body next"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left prev">
                        <i class="glyphicon glyphicon-chevron-left"></i>
                        Previous
                    </button>
                    <button type="button" class="btn btn-primary next">
                        Next
                        <i class="glyphicon glyphicon-chevron-right"></i>
                    </button>
                </div>
            </div>
        </div>
    </div>
</div>
{literal}<script type="text/javascript">
    function mapInitialize() {
        var geocoder = new google.maps.Geocoder(),
                address = '{/literal}{$add}{literal}',
                contentString = '<div class="map-detail"><p><b>Trung tâm Thẻ - Ngân hàng TMCP Đầu tư và Phát triển Việt Nam</b></p><p>' + address + '</p></div>';
        geocoder.geocode({'address': address}, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                var latitude = results[0].geometry.location.lat(),
                        longitude = results[0].geometry.location.lng(),
                        myLatlng = new google.maps.LatLng(latitude, longitude),
                        mapOptions = {
                            zoom: 17,
                            center: myLatlng,
                            streetViewControl: false,
                            mapTypeControl: false
                        };
                var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions),
                        infowindow = new google.maps.InfoWindow({
                            content: contentString
                        }),
                        marker = new google.maps.Marker({
                            position: myLatlng,
                            map: map,
                            title: 'Trung Tâm Thẻ BIDV'
                        });
                google.maps.event.addListener(marker, 'click', function () {
                    infowindow.open(map, marker);
                });
            } else {
                alert('Google Maps had some trouble finding the address. Status: ' + status);
            }
        });
    }
    shop.ready.add(function () {
        google.maps.event.addDomListener(window, 'load', mapInitialize);
    });
    </script>{/literal}