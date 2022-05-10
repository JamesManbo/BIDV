shop.subscribe = {
    export: function () {
        var data = {
            name: jQuery('#name').val(),
            email: jQuery('#email').val(),
            phone: jQuery('#phone').val(),
            age: jQuery('#age').val(),
            status: jQuery('#status').val(),
            created_time: jQuery('#created_time').val(),
            created_time_to: jQuery('#created_time_to').val(),
            city_id: jQuery('#city_id').val()
        };
        window.location = BASE_URL + 'export.html?cmd=subscribe' + shop.admin.system.fetchDataToUrl(data);
    },
    reg_home: function () {
        var email = jQuery('#email_reg').val();
        if (email == '') {
            alert('Bạn chưa nhập email');
            jQuery('#email_reg').focus();
            return;
        }
        if (!shop.is_email(email)) {
            alert('Email bạn nhập không đúng định dạng.');
            jQuery('#email_reg').focus();
            return;
        }
        shop.ajax_popup("act=subscribe&code=add_reg", 'POST', {email: email},
                function (json) {
                    if (json.msg == 'success') {
                        alert(json.msg);
                    } else {
                        alert(json.msg);
                        jQuery('#email_reg').focus();
                    }
                }, "json"
                );
    }
};
