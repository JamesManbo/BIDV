shop.string={safeTitle:function(s){s=s.toLowerCase();s=shop.string.stripUnicode(s);s=shop.string.str_replace(" ","-",s);s=s.replace(/[^a-zA-Z0-9_]/g,'-');s=shop.string.str_replace("----","-",s);s=shop.string.str_replace("---","-",s);s=shop.string.str_replace("--","-",s);return shop.string.trim(s,'-')},trim:function(s,a){var i=0;s+='';if(!a){whitespace=" \n\r\t\f\x0b\xa0\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u200b\u2028\u2029\u3000"}else{a+='';whitespace=a.replace(/([\[\]\(\)\.\?\/\*\{\}\+\$\^\:])/g,'$1')}l=s.length;for(i=0;i<l;i++){if(whitespace.indexOf(s.charAt(i))===-1){s=s.substring(i);break}}l=s.length;for(i=l-1;i>=0;i--){if(whitespace.indexOf(s.charAt(i))===-1){s=s.substring(0,i+1);break}}return whitespace.indexOf(s.charAt(0))===-1?s:''},stripUnicode:function(s){if(!s)return false;var a=["à","á","ạ","ả","ã","â","ầ","ấ","ậ","ẩ","ẫ","ă","ằ","ắ","ặ","ẳ","ẵ","è","é","ẹ","ẻ","ẽ","ê","ề","ế","ệ","ể","ễ","ì","í","ị","ỉ","ĩ","ò","ó","ọ","ỏ","õ","ô","ồ","ố","ộ","ổ","ỗ","ơ","ờ","ớ","ợ","ở","ỡ","ù","ú","ụ","ủ","ũ","ư","ừ","ứ","ự","ử","ữ","ỳ","ý","ỵ","ỷ","ỹ","đ","À","Á","Ạ","Ả","Ã","Â","Ầ","Ấ","Ậ","Ẩ","Ẫ","Ă","Ằ","Ắ","Ặ","Ẳ","Ẵ","È","É","Ẹ","Ẻ","Ẽ","Ê","Ề","Ế","Ệ","Ể","Ễ","Ì","Í","Ị","Ỉ","Ĩ","Ò","Ó","Ọ","Ỏ","Õ","Ô","Ồ","Ố","Ộ","Ổ","Ỗ","Ơ","Ờ","Ớ","Ợ","Ở","Ỡ","Ù","Ú","Ụ","Ủ","Ũ","Ư","Ừ","Ứ","Ự","Ử","Ữ","Ỳ","Ý","Ỵ","Ỷ","Ỹ","Đ"],marKoDau=["a","a","a","a","a","a","a","a","a","a","a","a","a","a","a","a","a","e","e","e","e","e","e","e","e","e","e","e","i","i","i","i","i","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","u","u","u","u","u","u","u","u","u","u","u","y","y","y","y","y","d","A","A","A","A","A","A","A","A","A","A","A","A","A","A","A","A","A","E","E","E","E","E","E","E","E","E","E","E","I","I","I","I","I","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","U","U","U","U","U","U","U","U","U","U","U","Y","Y","Y","Y","Y","D"];return shop.string.str_replace(a,marKoDau,s)},str_replace:function(a,b,c,d){var j=0,temp='',repl='',sl=0,fl=0,f=[].concat(a),r=[].concat(b),s=c,ra=Object.prototype.toString.call(r)==='[object Array]',sa=Object.prototype.toString.call(s)==='[object Array]';s=[].concat(s);if(d)this.window[d]=0;for(i=0,sl=s.length;i<sl;i++){if(s[i]==='')continue;for(j=0,fl=f.length;j<fl;j++){temp=s[i]+'';repl=ra?(r[j]!==undefined?r[j]:''):r[0];s[i]=(temp).split(f[j]).join(repl);if(d&&s[i]!==temp){this.window[d]+=(temp.length-s[i].length)/f[j].length}}}return sa?s:s[0]}};