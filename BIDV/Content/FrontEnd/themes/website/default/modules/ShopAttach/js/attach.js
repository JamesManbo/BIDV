shop.attach = {
    pop_id: 'upload-file-attach',
    admin:0,
	rec:8,
	cb:null,
    loadByObject:function(obj_id, obj_type, cb){
        if (obj_id > 0) {
            shop.ajax_popup('act=attach&code=load', "POST", {obj_id: obj_id, obj_type: obj_type},
                function (j) {
                    if (j.err == 0) {
                        j.html = '';
                        for(var i in j.data){
                            j.html += shop.attach.themeOne(j.data[i], j.path, obj_id, obj_type);
                        }
                        if(cb){
							shop.attach.cb = cb;
							shop.attach.cb(j);
                        }
                    } else {
                        alert(j.msg);
                    }
                }
            );
        }
    },
	closePop:function(){
        shop.hide_overlay_popup(shop.attach.pop_id);
        jQuery('#upload-file-form').remove();
    },
	themeOne:function(data, path, obj_id, obj_type){
        var del = '', edit = '', icon = shop.attach.getTypeImg(data.ext);
        if(shop.attach.admin == 1){
            del = '<a href="javascript:void(0)" onclick="shop.attach.removeFromObject('+data.id+', '+obj_id+', \''+obj_type+'\')" class="fl mLeft10"><img src="'+path+'icons/delete_button.gif" width="16" height="16" /></a>';
			edit = '<a href="javascript:void(0)" onclick="shop.attach.editUploadFile('+data.id+', '+obj_id+', \''+obj_type+'\')" class="fl mLeft10"><img src="'+path+'icons/edit.png" width="16" height="16" /></a>';
        }
        return shop.join
        ('<div class="mTop5" id="attach-'+data.id+'">')
            ('<img src="'+path+icon+'" height="16" class="fl attach-img" />')
            ('<div class="fl mLeft5 attach-link"><a href="javascript:void(0)" onclick="shop.attach.preview('+data.id+')"><b>'+data.name+'</b></a> (<span>'+data.size+'</span>)</div>')
            (edit+' '+del+'<div class="c"></div>')
        ('</div>')();
    },
	preview:function(id){
		shop.ajax_popup('act=attach&code=preview', "POST", {id: id},
			function (j) {
				if (j.err == 0) {
					var w = 700, html = '', data = j.data;
					switch(data.type2){
						case 'image':
							html = shop.join('<div align="center">')
								('<div style="padding:10px 0 10px"><img src="'+data.link+'" width="'+(data.info[0] > 680 ? 680 : data.info[0])+'" /></div>')
								('<div style="padding:10px;width:680px"><table width="100%">')
									('<tr><td><b>Tên file:</b></td><td>'+data.name+'</td></tr>')
									('<tr><td><b>Kích thước:</b></td><td>'+(data.info[0]?data.info[0]:0)+'x'+(data.info[1]?data.info[1]:0)+' px</td></tr>')
									('<tr><td><b>Dung lượng:</b></td><td>'+data.size+'</td></tr>')
									('<tr><td><b>Tải xuống:</b></td><td><a href="'+data.download+'" title="download">'+data.link+'</a></td></tr>')
								('</table></div>')
							('</div>')();
							break;
						case 'music':
							html = shop.join('<div align="center">')
								('<div style="padding:20px 0 10px"><audio src="'+data.link+'" id="audio-play" controls>Trình duyệt không hỗ trợ file '+data.ext+'</audio></div>')
								('<div style="padding:10px;width:680px"><table width="100%">')
									('<tr><td><b>Tên file:</b></td><td>'+data.name+'</td></tr>')
									('<tr><td><b>Dung lượng:</b></td><td>'+data.size+'</td></tr>')
									('<tr><td><b>Tải xuống:</b></td><td><a href="'+data.download+'" title="download">'+data.link+'</a></td></tr>')
								('</table></div>')
							('</div>')();
							break;
						case 'video':
							html = shop.join('<div align="center">')
								('<div style="padding:20px 0 10px">')
									('<video width="320" height="240" controls>')
										('<source src="'+data.link+'" type="video/'+data.ext+'">')
										('Trình duyệt không hỗ trợ file '+data.ext)
									('</video>')
								('</div>')
								('<div style="padding:10px;width:680px"><table width="100%">')
									('<tr><td><b>Tên file:</b></td><td>'+data.name+'</td></tr>')
									('<tr><td><b>Dung lượng:</b></td><td>'+data.size+'</td></tr>')
									('<tr><td><b>Tải xuống:</b></td><td><a href="'+data.download+'" title="download">'+data.link+'</a></td></tr>')
								('</table></div>')
							('</div>')();
							break;
						case 'fvideo':
							html = shop.join('<div align="center">')
								('<div style="padding:20px 0 10px">')
									('<embed src="http://www.aip.de/People/AKhalatyan/MOV/flvplayer.swf?file='+data.link+'" width="320" height="240" quality="high" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" wmode="transparent" align="middle"  />')
								('</div>')
								('<div style="padding:10px;width:680px"><table width="100%">')
									('<tr><td><b>Tên file:</b></td><td>'+data.name+'</td></tr>')
									('<tr><td><b>Dung lượng:</b></td><td>'+data.size+'</td></tr>')
									('<tr><td><b>Tải xuống:</b></td><td><a href="'+data.download+'" title="download">'+data.link+'</a></td></tr>')
								('</table></div>')
							('</div>')();
							break;
						case 'flash':
							html = shop.join('<div align="center">')
								('<div style="padding:20px 0 10px">')
									('<embed src="'+data.link+'" width="320" height="240" wmode="transparent" align="middle" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" allowscriptaccess="always" />')
								('</div>')
								('<div style="padding:10px;width:680px"><table width="100%">')
									('<tr><td><b>Tên file:</b></td><td>'+data.name+'</td></tr>')
									('<tr><td><b>Dung lượng:</b></td><td>'+data.size+'</td></tr>')
									('<tr><td><b>Tải xuống:</b></td><td><a href="'+data.download+'" title="download">'+data.link+'</a></td></tr>')
								('</table></div>')
							('</div>')();
							break;
						default:
							html = shop.join('<div align="center">')
								('<div style="padding:10px;width:680px"><table width="100%">')
									('<tr><td><b>Tên file:</b></td><td>'+data.name+'</td></tr>')
									('<tr><td><b>Dung lượng:</b></td><td>'+data.size+'</td></tr>')
									('<tr><td><b>Tải xuống:</b></td><td><a href="'+data.download+'" title="download">'+data.link+'</a></td></tr>')
								('</table></div>')
							('</div>')();
							break;
					}
					shop.show_overlay_popup('preview-attach', 'Thư viện File đính kèm', html,{
						content:{'width' : w+'px'},
						onclose:function(){
							var obj = shop.get_ele('audio-play');
							if(obj){
								obj.pause();
							}
						}
					});
				} else {
					alert(j.msg);
				}
			}
		);
	},
	getTypeImg:function(ext){
		var icons = {
            word: 'icons/word.png',
            excel:'icons/excel.png',
            point:'icons/powerpoint.png',
            image:'icons/picture.png',
            pdf:'icons/pdf.png',
            zip:'icons/zip.png',
            rar:'icons/rar.gif',
            txt:'icons/txt.png',
			video:'icons/video.png',
			flash:'icons/flash.png',
            music:'icons/music.png',
            unknow: 'icons/unknow.png'
        }, key = '';
		switch(ext){
            case 'xls':case 'xlsx': key = 'excel'; break;
            case 'doc':case 'docx':case 'odt': key = 'word'; break;
            case 'pdf': key = 'pdf'; break;
            case 'ppt': case 'pptx':case 'pps':case 'ppsx': key = 'point'; break;
            case 'jpg':case 'jpeg':case 'gif':case 'png':case 'bmp': key = 'image'; break;
            case 'zip':case 'cab':case 'jar':case 'tar':case '7z':case 'tar.gz': key = 'zip'; break;
            case 'rar': key = 'rar'; break;
            case 'txt': case 'rtf': key = 'txt'; break;
			case '3gp': case 'avi':case 'flv':case 'm4v':case 'mkv':case 'mov':case 'mp4':case 'mpeg':case 'wmv': key = 'video'; break;
			case 'mp3': case 'wav': case 'wma':case 'flac':case 'mka':case 'ogg':case 'ac3':case 'aac': key = 'music'; break;
			case 'swf': key = 'flash'; break;
			default: key = 'unknow';
        }
		return icons[key];
	},
	editUploadFile: function (id, obj_id, obj_type) {
		if (id > 0 && obj_id > 0 && obj_type != '') {
            shop.attach.upload(id, obj_id, obj_type, function(j){
				var data = j.data, err= '';
				for(var i in data){
					if(data[i].err == 0){
						jQuery('#attach-'+data[i].id+' .attach-img').attr('src', j.path + shop.attach.getTypeImg(data[i].ext));
						jQuery('#attach-'+data[i].id+' .attach-link a').attr('href', data[i].link);
						jQuery('#attach-'+data[i].id+' .attach-link b').html(data[i].name);
						jQuery('#attach-'+data[i].id+' .attach-link span').html(data[i].size);
						
						shop.attach.closePop();
					}else{
						switch(data[i].msg){
							case 'existed':
								err += 'File '+data[i].name+' đã tồn tại';
								break;
							case 'notSup':
								err += 'File '+data[i].name+' lỗi, không hỗ trợ định dạng '+data[i].tail;
								break;
							case 'maxSize':
								err += 'File '+data[i].name+' vượt quá dung lượng cho phép ('+data[i].max+')';
								break;
							case 'data':
								err += 'Lỗi đường truyền mạng, vui lòng thử lại';
								break;
						}
					}
				}
				if(err != ''){
					alert(err);
				}
			});
        }
	},
    upload: function (id, obj_id, obj_type, cb) {
		var curFile = '',title = 'Upload file đính kèm', choose = '';
		if(id > 0){
			var tmp = jQuery('#attach-'+id+' .attach-link').html();
			title = 'Cập nhật file đính kèm';
			if(tmp){
				curFile = shop.join('<div class="mTop20">')
					('File hiện tại: ' + tmp)
				('</div>')();
			}
		}else{
			choose = shop.join('<tr><td colspan="2"><div class="mTop10">')
				('<div><a href="javascript:void(0)" onclick="shop.attach.choose.listFile('+obj_id+', \''+obj_type+'\')">Chọn từ thư viện đã upload</a></div>')
				('<div id="attach-lib"></div>')
			('</div></td></tr>')();
		}
        var pop_id = shop.attach.pop_id,
        html = shop.join
        ('<form id="upload-file-form" enctype="multipart/form-data" method="POST" action="'+BASE_URL+'ajax.php?act=attach&code=add">')
            ('<div id="popup-form">')
                ('<table id="pass-changed" width="100%">')
                    ('<tr>')
                        ('<td width="100">Tải lên từ máy</td>')
                        ('<td><input type="file" name="attach'+(id > 0 ? '' : '[]')+'" id="attach" class="needRelease" '+(id > 0 ? '' : 'multiple')+' /></td>')
                    ('</tr>')
					('<tr><td colspan="2"><hr style="margin-top:20px" />'+curFile+'</td></tr>'+choose)
                ('</table>')
            ('</div>')
            ('<div class="popup-footer" align="right">'+(choose!=''?'<button type="button" onclick="shop.attach.choose.done('+obj_id+', \''+obj_type+'\')">Chọn từ thư viện</button>':''))
                ('<button type="button" onclick="shop.attach.doUpload(\''+pop_id+'\', '+id+', '+obj_id+', \''+obj_type+'\', '+cb+')">'+(id ? 'Cập nhật' : 'Upload')+'</button>')
                ('<button type="button" onclick="shop.hide_overlay_popup(\''+pop_id+'\');">Hủy bỏ</button>')
            ('</div>')
        ('</form>')();
		
		shop.show_overlay_popup(pop_id, title, html,
        {
            content: {
                'width' : '800px'
            },
			release:function(){
				jQuery('.needRelease').uniform();
			}
        });
        shop._store.variable['ajax-running'] = false;
    },
    doUpload:function(pop_id, id, obj_id, obj_type, cb){
        if(shop._store.variable['ajax-running']){
        }else{
            var data = {id: id, obj_id: obj_id, obj_type: obj_type};
            data[''+BASE_TOKEN_NAME] = shop.getCSRFToken();
    
            jQuery('#upload-file-form').ajaxSubmit({
                beforeSubmit:function(){
                    shop.show_loading();
                    shop._store.variable['ajax-running'] = true;
                },
                data:data,
                dataType: 'json',
                success:function(j){
                    shop.hide_loading();
                    shop._store.variable['ajax-running'] = false;
                    if(j.err == 0){
                        if(cb){
                            cb(j);
                        }else{
                            shop.hide_overlay_popup(pop_id);
                            jQuery('#upload-file-form').remove();
                            alert('Cập nhật thành công');
                            shop.reload();
                        }
                    }else{
                        alert(j.msg);
                    }
                }
            });
        }
    },
	afterAttach:function(data, path, obj_id, obj_type){
		var html = '', err = '';
		for(var i in data){
			if(data[i].err == 0){
				html += shop.attach.themeOne(data[i], path, obj_id, obj_type);
			}else{
				switch(data[i].msg){
					case 'existed':
						err += shop.join('<div>')
							('File <b>'+data[i].name+'</b> đã tồn tại')
						('</div>')();
						break;
					case 'notSup':
						err += shop.join('<div>')
							('File <b>'+data[i].name+'</b> lỗi, không hỗ trợ định dạng <b>'+data[i].tail+'</b>')
						('</div>')();
						break;
					case 'maxSize':
						err += shop.join('<div>')
							('File <b>'+data[i].name+'</b> vượt quá dung lượng cho phép ('+data[i].max+')')
						('</div>')();
						break;
					case 'data':
						err += shop.join('<div>')
							('Lỗi đường truyền mạng, vui lòng thử lại')
						('</div>')();
						break;
				}
				
			}
		}
		if(err){
			shop.show_popup_message(err, 'Thông báo lỗi', -1, 700, 500);
		}
		return html;
	},
    remove: function (id) {
        if (id > 0) {
            if(confirm('Bạn chắc chắn xóa file này?')){
                shop.ajax_popup('act=attach&code=del', "POST", {id: id},
                    function (j) {
                        if (j.err == 0) {
                            shop.reload();
                        } else {
                            alert(j.msg);
                        }
                    }
                );
            }
        }
    },
    removeFromObject: function(id, obj_id, obj_type){
        if (id > 0 && obj_id > 0 && obj_type != '') {
			if(confirm('Bạn chắc chắn bỏ file đính kèm?')){
				shop.ajax_popup('act=attach&code=del-obj', "POST", {id: id, obj_id: obj_id, obj_type: obj_type},
					function (j) {
						if (j.err == 0) {
							jQuery('#attach-'+id).remove();
						} else {
							alert(j.msg);
						}
					}
				);
			}
        }
    },
	choose:{
		ids:{},
		listFile:function(obj_id, obj_type, page, recperpage, search){
			page = page > 0 ? page : 1;
			search = search ? search : '';
			recperpage = recperpage > 0 ? recperpage : shop.attach.rec;
			shop.ajax_popup('act=attach&code=list', "POST", {page_no: page, obj_id: obj_id, obj_type: obj_type, search: search, recperpage: recperpage},
				function (j) {
					if (j.err == 0) {
						jQuery('#attach-lib').html(shop.attach.choose.theme.listFile(j, obj_id, obj_type));
						if(search){
							jQuery('#lib-search').val(search);
						}
					} else {
						alert(j.msg);
					}
				}
			);
		},
		action:function(id, status){
			if(status){
				shop.attach.choose.ids[id] = 1;
			}else{
				shop.attach.choose.ids[id] = null;
				delete shop.attach.choose.ids[id];
			}
		},
		search:function(obj_id, obj_type, page){
			page = page > 0 ? page : 1;
			var q = jQuery('#lib-search').val();
			shop.attach.choose.listFile(obj_id, obj_type,page,shop.attach.rec,q);
		},
		done:function(obj_id, obj_type){
			var ids = '';
			for(var i in shop.attach.choose.ids){
				ids += (ids==''?'':',')+i;
			}
			if(ids != ''){
				shop.ajax_popup('act=attach&code=lib-add', "POST", {obj_id: obj_id, obj_type: obj_type, ids: ids},
					function (j) {
						if (j.err == 0) {
							shop.attach.loadByObject(obj_id, obj_type, shop.attach.cb);
							shop.attach.closePop();
						} else {
							alert(j.msg);
						}
					}
				);
			}
		},
		theme:{
			listFile:function(data, obj_id, obj_type){
				var arr = data.data, html = '';
				for(var i in arr){
					html += shop.attach.choose.theme.oneFile(arr[i], obj_id, obj_type, data.path);
				}
				if(html != ''){
					html = shop.join
					('<div>'+shop.attach.choose.theme.header(obj_id, obj_type)+'</div>')
					('<div class="mTop10 price">Tìm thấy <b>'+data.total+'</b> file</div>')
					('<div class="widget" id="mTop10"><table width="100%" class="tDefault">')
						('<tr>')
							('<td width="40"><b>Chọn</b></td>')
							('<td><b>Tên</b></td>')
							('<td width="30"><b>Loại</b></td>')
							('<td width="90"><b>Dung lượng</b></td>')
							('<td><b>Xem trước</b></td>')
						('</tr>'+html)
					('</table></div>'+shop.attach.choose.theme.footer(obj_id, obj_type, data.totalPage, data.page))();
				}
				return html;
			},
			oneFile:function(data, obj_id, obj_type, path){
				var icon = shop.attach.getTypeImg(data.ext);
				if(shop.is_exists(shop.attach.choose.ids[data.id])){
					data.active = true;
				}else if(data.active){
					shop.attach.choose.ids[data.id] = 1;
				}
				return shop.join('<tr>')
					('<td><input type="checkbox" value="1" id="lib-check-'+data.id+'" '+(data.active?'checked':'')+' onclick="shop.attach.choose.action('+data.id+', this.checked)" /></td>')
					('<td>'+data.name+'</td>')
					('<td><img src="'+path+icon+'" /></td>')
					('<td>'+data.size+'</td>')
					('<td></td>')
				('</tr>')();
			},
			header:function(obj_id, obj_type){
				return shop.join('<table><tr>')
					('<td><input type="text" id="lib-search" placeholder="Nhập tên file cần tìm" style="width:300px" /></td>')
					('<td><div style="padding-left:20px"><input type="button" value="Tìm kiếm" class="buttonS bGreen" onclick="shop.attach.choose.search('+obj_id+', \''+obj_type+'\')" /></div></td>')
				('</tr></table>')();
			},
			footer:function(obj_id, obj_type, total_page, page){
				var html = '<div class="mTop10">';
				for(var i=1;i<=total_page;i++){
					html += '<a href="javascript:shop.attach.choose.search('+obj_id+', \''+obj_type+'\', '+i+')" style="display:block;padding:3px 10px;float:left;margin:0 5px;border:1px solid #2b6893;'+(i==page?'background:#2b6893;color:#fff':'')+'"><b>'+i+'</b></a>';
				}
				html+='</div>';
				return html;
			}
		}
	}
};
//shop.ready.add(function(){
//    shop.attach.upload(0, 1, 'news', function(j){
//        alert(j.msg);
//    });
//}, true);