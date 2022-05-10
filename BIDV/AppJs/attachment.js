var attachment = function () {
    return {
        init: function () {
            this.pageIndex = 1;
            this.DataSearch = {};
            this.catid = 1;
        },
        loadData: function (pageindex, catid) {            
            var self = this;
            this.catid = catid;
            AjaxService.GET("/AdminAttachment/ListData?page=" + pageindex + "&catid=" + catid, attachment.DataSearch, function (res) {
                $("#gridData").html(res.viewContent);
                if (res.totalPages > 1) {
                    $('#paginationholder').html('');
                    $('#paginationholder').html('<ul id="pagination" class="pagination-sm"></ul>');
                    $('#pagination').twbsPagination({
                        startPage: self.pageIndex,
                        totalPages: res.totalPages,
                        visiblePages: 5,
                        onPageClick: function (event, page) {
                            self.pageIndex = page;
                            attachment.loadData(self.pageIndex, catid);
                        }
                    });
                }
            });
        },
        onSearchSuccess: function (res) {
            var self = this;            
            $('#paginationholder').html('');
            $('#paginationholder').html('<ul id="pagination" class="pagination-sm"></ul>');
            $("#gridData").html(res.viewContent);
            attachment.DataSearch = {
                filename: $("#filename").val(),
                catid: attachment.catid,
            };
            if (res.totalPages > 1) {
                $('#pagination').twbsPagination({
                    startPage: 1,
                    totalPages: res.totalPages,
                    visiblePages: 5,
                    onPageClick: function (event, page) {
                        attachment.pageIndex = page;
                        attachment.loadData(page, attachment.catid);
                    }
                });
            }
        },
        onloadformupload: function (id) {
            attachment.DataSearch = {};
            modal.Render("/AdminAttachment?id=" + id, "Upload file đính kèm", "modal-lg");
        }
    };
}();
$(function () { attachment.init(); });


