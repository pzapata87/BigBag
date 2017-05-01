var loginJs = function () {
    var aplicarHandlers = function () {
        $('#frmLogin').on('submit', function (e) {
            if (!$(this).valid()) {
                e.preventDefault();
            } else {
                webApp.showLoading();
            }
            return true;
        });
    }

    return {
        //main function to initiate page
        init: function () {
            aplicarHandlers();
        }
    };
}();