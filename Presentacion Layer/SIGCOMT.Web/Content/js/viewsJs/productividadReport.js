var productividadReportJs = function () {
    var _urlExportar;

    var aplicarHandlers = function() {
        $("#btnLimpiar").on("click", function() {
            webApp.clearForm("#formBusqueda");
        });

        $("#btnExportar").on("click", function() {
            var filters = new Object();
            var value = $("#txtMes").datepicker("getFormattedDate", "dd/mm/yyyy");

            if (value !== '') {
                filters.fechaIni = value;
            }

            value = $("#txtAnio").datepicker("getFormattedDate", "yyyy");

            if (value !== '') {
                filters.AnioProyeccion = value;
            }

            webApp.Ajax({
                url: _urlExportar,
                parametros: filters,
                success: function(response) {
                    if (response.Success) {
                        window.location.href = response.Data;
                    } else {
                        $.gritter.add({
                            title: 'Error',
                            text: 'Error al exportar la informaci&oacute;n',
                            class_name: 'gritter-error'
                        });
                    }
                }
            });
        });
    }

    return {
        //main function to initiate page
        init: function(parametros) {
            _urlExportar = parametros.urlExportar;

            webApp.datepickerMonth();
            webApp.datepickerYear();
            aplicarHandlers();
        }
    };
}();