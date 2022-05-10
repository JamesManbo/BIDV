var gallery = function () {
    return {
        init: function () {
            this.pageIndex = 1;
        },
        loadData: function (pageindex) {
            var self = this;
            $.get("/AdminImagesLibrary/_ListData?id=" + $("#cat_id").val() + "&page=" + pageindex, function (result) {
                $("#lstdata").html(result.viewContent);
                $('#paginationholder').html('');
                $('#paginationholder').html('<ul id="pagination" class="pagination-sm"></ul>');
                if (result.totalPages > 1) {
                    $('#pagination').twbsPagination({
                        startPage: self.pageIndex,
                        totalPages: result.totalPages,
                        visiblePages: 5,
                        onPageClick: function (event, page) {
                            self.pageIndex = page;
                            gallery.loadData(self.pageIndex);
                        }
                    });
                }
            });

        },
        loadformcat: function () {
            $.get("/AdminImagesLibrary/_FormCategory", function (result) {
                $("#formcategory").html(result);
            });
        },
        onloaddetail:function(id) {
            modal.Render("/AdminImagesLibrary/_View/" + id, "Thông tin ảnh", "modal-lg");
        },
        onloadformedit: function (id) {
            modal.Render("/AdminImagesLibrary/_Edit/" + id, "Sửa ảnh", "modal-lg");
        },
        onUpdateSuccess: function (res) {
            $("#lstdata").html(res);
            modal.hide();
            alertmsg.success("Cập nhật thành công");
            gallery.pageIndex = 1;
            gallery.loadData(1);
        },
        ondelete:function(id) {
            bootbox.confirm({
                message: "Bạn có muốn xóa không?",
                buttons: {
                    confirm: {
                        label: 'Có',
                        className: 'btn-success'
                    },
                    cancel: {
                        label: 'Không',
                        className: 'btn-danger'
                    }
                },
                callback: function (result) {
                    if (result) {
                        $.get("/AdminImagesLibrary/Delete", { id: id }).done(function (res) {
                            $("#lstdata").html(res);
                            alertmsg.success("Xóa thành công");
                            gallery.loadData(self.pageIndex);
                            modal.hide();
                        });
                    }
                }
            });
        },
        onloadAddCat: function() {
            modal.Render("/AdminImagesLibrary/_AddCategory/", "Thêm mới danh mục ảnh", "modal-medium");
        },
        onAddCatSuccess: function(result) {
            $("#formcategory").html(result);
            alertmsg.success("Thêm mới danh mục thành công");
            modal.hide();
        },
        onloadEditCat: function (id) {
            modal.Render("/AdminImagesLibrary/_EditCategory/"+id, "Cập nhật danh mục ảnh", "modal-medium");
        },
        onEditCatSuccess: function (result) {
            $("#formcategory").html(result);
            alertmsg.success("Cập nhật danh mục thành công");
            modal.hide();
        },
        deleteCategory: function (id) {
            bootbox.confirm({
                message: "Bạn có chắc muốn xóa không, nó sẽ làm mất dữ liệu của bạn?",
                buttons: {
                    confirm: {
                        label: 'Có',
                        className: 'btn-success'
                    },
                    cancel: {
                        label: 'Không',
                        className: 'btn-danger'
                    }
                },
                callback: function (result) {
                    if (result) {
                        $.get("/AdminImagesLibrary/DeleteCategory/" + id, function (res) {
                            if (res == true) {
                                alertmsg.success("Xóa danh mục thành công");
                                gallery.loadformcat();
                            } else {
                                alertmsg.error("Xóa danh mục thất bại");
                            }
                        });
                    }
                }
            });
            
        }
    };
}();
$(function () { gallery.init(); });


