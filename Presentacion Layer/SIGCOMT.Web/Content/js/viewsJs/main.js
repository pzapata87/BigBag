var mainJs = function () {
    var _pageBlocked = false;

    function activarMenu() {
        var $link = $("#ulMenu a[href='" + window.location.pathname + "']");
        var $parent = $link.parents("li:eq(1)");

        // Activar los menus seleccionados
        $parent.addClass('active open');
        $link.closest('li').addClass("active");

        // Colocar texto a la sección breadcrumb
        var value = $parent.find(".menu-text").text().trim();
        if (value !== '') {
            $("#link2").text(value);
        }
        else {
            $("#link2").text($link.text().trim());
            $("#link3").hide();
            return;
        }

        value = $link.text().trim();
        var $linkSig = $("#link3");
        if (value == '') {
            $linkSig.hide();
        }
        else {
            $linkSig.text(value);
            $linkSig.show();
        }
    }

    var aplicarHandlers = function () {
        $(document).ready(function() {
            $(document).ajaxSend(function() {
                if (!_pageBlocked) {
                    $.blockUI({
                        message: "Espere un momento por favor",
                        css: {
                            border: 'none',
                            padding: '15px',
                            backgroundColor: '#000',
                            '-webkit-border-radius': '10px',
                            '-moz-border-radius': '10px',
                            opacity: 1,
                            color: '#fff'
                        },
                        onBlock: function() {
                            _pageBlocked = true;
                        }
                    });
                }
            }).ajaxStop(function() {
                jQuery.unblockUI();
                _pageBlocked = false;
            });
        });
    }

    return {
        //main function to initiate page
        init: function () {
            activarMenu();
            aplicarHandlers();
        }
    };
}();