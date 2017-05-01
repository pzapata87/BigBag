var carteraCastigadaReportJs = function () {
    var _dtEstudios;
    var _urlLanguage;
    var _urlExportar;
    var _urlEstudios;

    var initializeGrid = function (data) {
        _dtEstudios = $('#dtEstudios').DataTable({
            responsive: true,
            "bAutoWidth": false,
            language: {
                "url": _urlLanguage
            },
            data: data,
            columns: [
                 {
                     data: function (oObj) {
                         return '<label class="pos-rel">\
									<input type="checkbox" class="ace" value="' + oObj.Codigo + '" />\
									<span class="lbl"></span>\
								</label>';
                     },
                     orderable: false,
                     searchable: false,
                     width: "5%"
                 },
	            { data: "Codigo", width: "15%" },
	            { data: "Nombre", width: "20%" },
	            { data: "Region", width: "15%" }
            ],
            columnDefs: [
                {
                    targets: [0],
                    className: "center"
                },
	            {
	                targets: [2],
	                className: "hidden-480"
	            }
            ],
            lengthMenu: [25, 50, 100],
            order: [[3, 'asc']]
        });
    }

    var cargaDatos = function () {
        webApp.Ajax({
            url: _urlEstudios,
            success: function (response) {
                initializeGrid(response.Data);
            }
        });
    }

    var aplicarHandlers = function() {
        $("#btnLimpiar").on("click", function() {
            webApp.clearForm("#formBusqueda");
        });

        $('#dtEstudios > thead > tr > th input[type=checkbox]').eq(0).on('click', function () {
            // Get all rows with search applied
            var rows = _dtEstudios.rows({ 'search': 'applied' }).nodes();
            // Check/uncheck checkboxes for all rows in the table
            $('input[type="checkbox"]', rows).prop('checked', this.checked);
        });

        $('#dtEstudios').on('change', 'tbody input[type="checkbox"]', function () {
            // If checkbox is not checked
            if (!this.checked) {
                var el = $('#dtEstudios > thead > tr > th input[type=checkbox]').eq(0);
                // If "Select all" control is checked and has 'indeterminate' property
                if (el && el.checked && ('indeterminate' in el)) {
                    // Set visual state of "Select all" contro66l 
                    // as 'indeterminate'
                    el.indeterminate = true;
                }
            }
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
            _urlLanguage = parametros.urlLanguage;
            _urlEstudios = parametros.urlEstudios;

            webApp.datepickerMonth();
            webApp.datepickerYear();
            aplicarHandlers();
            cargaDatos();
        }
    };
}();