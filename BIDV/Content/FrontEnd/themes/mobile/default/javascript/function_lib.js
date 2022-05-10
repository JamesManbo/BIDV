shop.captcha = {
  img_id:'captcha',
  txt_id:'captcha_txt',
  reloadCaptcha:function(){
	var captcha = shop.get_ele(shop.captcha.img_id);
	if(captcha){
	  captcha.src = BASE_URL + 'captcha.php?w=80&h=28&l=5&r='+Math.random();
	}
  },
  validCaptcha:function(cb){
	var captcha = shop.get_ele(shop.captcha.txt_id);
	if(captcha){
	  if(shop.util_trim(captcha.value) != ''){
		shop.ajax_popup("act=index&code=valid-captcha",'POST', {str: captcha.value},
		  function(json){
			if(json.err == 0){
			  if(cb) cb()
			}else{
			  switch(json.msg){
				case 1: json.msg = 'Captcha chưa được khởi tạo'; break;
				case 2:
				  json.msg = 'Sai captcha quá nhiều, vui lòng nhập mã khác';
				  shop.captcha.reloadCaptcha();
				  break;
				case 3: json.msg = 'Captcha đã hết hạn! Vui lòng nhập mã khác';
				  shop.captcha.reloadCaptcha();
				  break;
				case 4: json.msg = 'Sai captcha! Vui lòng nhập lại'; break;
			  }
			  alert(json.msg);
			}
		  }
		);
	  }else{
		alert('Vui lòng nhập captcha để tiếp tục');
		captcha.focus();
	  }
	}else{
	  alert('Không tồn tại ô nhập captcha');
	}
  }
};

shop.button = {
  id:"btLoading",
  hide:function(id){
	var bt = shop.get_ele(shop.button.id);
	if(bt){
	  jQuery(bt).show();
	}else{
	  var obj = jQuery(id).parent();
	  obj.append('<img src="'+BASE_URL+'style/images/ajax-loader.gif" />');
	}
	jQuery(id).hide();
  },
  show:function(id){
	jQuery('#'+shop.button.id).hide();
	jQuery(id).show();
  },
  preload:function(){
	jQuery('body').append('<img class="hidden" src="'+BASE_URL+'style/images/ajax-loader.gif" />');
  }
};