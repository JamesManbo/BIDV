var roles = function () {
    return {
        init: function () {
        },
        onloaduserindgroup:function(id,name) {
            modal.Render("/roles/AddUserToGroup?id=" + id, "Thêm người dùng vào nhóm: " + name, "modal-lg");
        }
    };
}();
$(function () { roles.init(); });


