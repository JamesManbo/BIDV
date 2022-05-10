<div class="bv_menucate_uudai">
    <div class="bv_contain">
        <ul class="bv_menucate_uudai_build">
            <li>
                <a class="amthuc {if $cat==6}active{/if}" href="{$link_cate}?cat=6"><i class="bv_ic_amthuc"></i><br/>ẨM THỰC</a>
            </li>
            <li>
                <a class="muasam {if $cat==7}active{/if}" href="{$link_cate}?cat=7"><i class="bv_ic_muasam"></i><br/>MUA SẮM</a>
            </li>
            <li>
                <a class="dulich {if $cat==8}active{/if}" href="{$link_cate}?cat=8"><i class="bv_ic_dulich"></i><br/>Du LỊCH</a>
            </li>
            <li>
                <a class="suckhoe {if $cat==9}active{/if}" href="{$link_cate}?cat=9"><i class="bv_ic_suckhoe"></i><br/>SỨC KHỎE</a>
            </li>
            <li>
                <a class="doisong {if $cat==10}active{/if}" href="{$link_cate}?cat=10"><i class="bv_ic_doisong"></i><br/>ĐỜI SỐNG</a>
            </li>
        </ul>
        <div class="c"></div>
    </div>
</div>
<input type="hidden" name="cat" id='cat' value="{$cat}"/>
<div class="bv_menucate_listnews">
    <div class="bv_contain">
        <div class="bv_menucate_listnews_fiter">
            <form name="shopPromotionForm" id="shopPromotionForm" method="GET" action='{$link_cate}'>
                <div class="text">SẮP XẾP ƯU ĐÃI THEO</div>
                <div class="c"></div>
                <div class="bv_menucate_b_s">
                    <select id='sort' name="sort">
                        <option value="0" >MẶC ĐỊNH</option>
                        <option value="1" {if $sort==1} selected{/if}>Mới nhất</option>
                        <option value="2" {if $sort==2} selected{/if}>Cũ nhất</option>
                    </select>
                </div>
                <div class="bv_menucate_b_s">
                    <select id="card" name="card">
                        <option value="0">Loại thẻ</option>
                        {$card}
                    </select>
                </div>

                <div class="bv_menucate_b_s">
                    <select id="promo_per" name="promo_per">
                        <option value="-1">Mức giảm giá</option>
                        {$promo_per}
                    </select>
                </div>
                <div class="bv_menucate_b_s">
                    <select id='city_id' name="city_id">
                        <option value="0">Địa điểm</option>
                        {$city}
                    </select>
                </div>
                <input type="submit" value="LỌC" class="bv_menucate_btn"/>
                <div class="c"></div>
            </form>
        </div>
        <div class="bv_menucate_newsmore">
            <div class="bv_menucate_content_news_title">
                {$data.title}
            </div>
            <div class="bv_menucate_newsmore_left">
                <div class="bv_menucate_newsmore_img">
                    <div class="jcarousel-wrapper">
                        <div class="jcarousel jcarousel_detail">
                            <ul>
                                {foreach from=$data.image item=item name=i}
                                    <li><img id="page_{$smarty.foreach.i.iteration}" alt="{$item.small}" height="240" width="100%" src="style/images/blank.gif" style="background:url({$item.big}) no-repeat center center" /></li>
                                    {/foreach}
                            </ul>
                            <div class="bv_contain bv_slide_jc_page">
                                <p class="jcarousel-two-pagination" data-jcarouselpagination="true"></p>
                            </div>
                        </div>
                    </div>
                    {literal}
                        <script type="text/javascript">
                            shop.ready.add(function () {
                                $(function () {
                                    var jcarousel_2 = $('.jcarousel_detail');
                                    jcarousel_2
                                            .on('jcarousel:reload jcarousel:create', function () {
                                                jcarousel_2.jcarousel('items').width(jcarousel_2.innerWidth());
                                            })
                                            .jcarousel({
                                                wrap: 'circular',
                                                transitions: Modernizr.csstransitions ? {
                                                    transforms: Modernizr.csstransforms,
                                                    transforms3d: Modernizr.csstransforms3d,
                                                    easing: 'ease'
                                                } : false
                                            });

                                    $('.jcarousel-two-pagination')
                                            .on('jcarouselpagination:active', 'a', function () {
                                                $(this).addClass('active');
                                            })
                                            .on('jcarouselpagination:inactive', 'a', function () {
                                                $(this).removeClass('active');
                                            })
                                            .on('click', function (e) {
                                                e.preventDefault();
                                            })
                                            .jcarouselPagination({
                                                item: function (page) {
                                                    return '<a href="#' + page + '">' + page + '</a>';
                                                }
                                            });
                                });
                            });
                        </script>
                    {/literal}
                </div>
            </div>
            <div class="c"></div>
        </div>
        <div class="bv_menucate_content_news">
            <div class="cate mTop10">
                {if $time_now>$data.time_to}<div style="color: #ff0000"> Đã hết hạn </div>{/if}
            </div>
            <div class="bv_menucate_newsmore_left">
                <div class="bv_menucate_newsmore_left_l">
                    <div class="bv_menucate_cn_title_box">
                        Thông tin ưu đãi
                    </div>
                    {if $data.company}<div class="bv_menucate_cn_title_list">
                            <span>Công ty:</span>{$data.company}
                        </div>{/if}
                        {if $data.website}<div class="bv_menucate_cn_title_list">
                                <span>Website:</span><a href="{$data.website}">{$data.website}</a>
                            </div>{/if}
                            {*<div class="bv_menucate_cn_title_list">
                            <span>Đối tượng:</span>Chủ thẻ BIDV
                            </div>*}
                            <div class="bv_menucate_cn_title_list">
                                <span>Sản phẩm:</span>{$data.title}
                            </div>
                            <div class="bv_menucate_cn_title_list">
                                <span>Thời hạn:</span>{$data.time_from|date_format:"%d/%m/%Y"} - {$data.time_to|date_format:"%d/%m/%Y"}
                            </div>
                            {if $phonesupport}<div class="bv_menucate_cn_title_list">
                                    <span>Hotline:</span>{$phonesupport}
                                </div>{/if}
                                <div class="bv_menucate_cn_title_list">
                                    <span>Chia sẻ:</span>
                                    <ul>
                                        <li><a href="{$data.share.fb}" target="_blank">&#xf09a;</a></li>
                                        <li><a href="{$data.share.tw}" target="_blank">&#xf099;</a></li>
                                        <li><a href="{$data.share.gg}" target="_blank">&#xf0d5;</a></li>
                                        <li><a href="{$data.share.li}" target="_blank">&#xf0e1;</a></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="bv_menucate_newsmore_left_r" style="overflow:hidden">
                                <div class="bv_menucate_cn_title_box">
                                    Địa điểm
                                </div>
                                <div >
                                    {$data.location}
                                </div>
                            </div>
                            <div class="c"></div>
                            <div style="width:100%;overflow:hidden">
                                <div class="bv_menucate_cn_title_box">
                                    Nội dung
                                </div>
                                <div id="popup_galery">
                                    {$data.body}
                                </div>
                            </div>
                        </div>
                        {if $arrTag}
                            <div class="bv_menucate_newsmore_right_new">
                                <div class="bv_menucate_cn_title_box">
                                    Từ KHÓA
                                </div>
                                <ul class="bv_menucate_list_key">
                                    {foreach from=$arrTag item=tag name=t}
                                        <li><a target="_blank" href="{$tag.link}">{$tag.title}</a></li>
                                        {/foreach}
                                </ul>
                            </div>
                        {/if}
                        <div class="c"></div>
                    </div>
                    <div>
                        <ul class="bv_build_link" style="margin: 30px auto">
                            <li><a href="dich-vu-the/the-tin-dung-ca-nhan-3.html">ĐĂNG KÝ MỞ THẺ NGAY</a></li>
                        </ul>
                    </div>
                    <div class="c"></div>
                </div>
            </div>
            <div style='background-color: #ddddde;padding: 20px 0' >
                <div class="bv_contain">
                    <div class="bv_menucate_newsmore_right_build">
                        {if $item_nb}
                            <div class="bv_menucate_newsmore_right_title">
                                ưu đãi nổi bật
                            </div>
                            <ul class="bv_news_list">
                                {foreach from=$item_nb item=new name=i}
                                    <li>
                                        <a class="bv_news_list_img_m" href="{$new.link}">
                                            <img height="70" width="100%" src="style/images/blank.gif" style="background-image: url('{$new.image}');background-size:cover"/>
                                        </a>
                                        <a class="bv_news_list_tit_m" href="{$new.link}">
                                            <span class="bv_news_list_tit_m_s">{$new.title}</span>
                                            <span class="time">{$new.time_from|date_format:"%d/%m/%Y"} - {$new.time_to|date_format:"%d/%m/%Y"}</span>
                                        </a>
                                        <div class="c"></div>
                                    </li>
                                {/foreach}
                            </ul>

                        {/if}
                    </div>