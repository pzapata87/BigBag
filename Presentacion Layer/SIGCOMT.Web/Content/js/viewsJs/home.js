var homeJs = function () {
    var _dtHistorial;
    var _urlLanguage;
    var _urlUltimaCarga;
    var _urlHistorialCarga;
    var _tipoArchivo;
    var _estadoClass = ["warning", "success", "danger"];

    var ultimaCarga = function () {
        webApp.Ajax({
            url: _urlUltimaCarga,
            success: function(response) {
                if (response.Success) {
                    var html = '';
                    for (var i = 0; i < response.Data.length; i++) {
                        var item = response.Data[i];
                        html +=
                            '<tr>\
                            <td>\
                                <div class="hidden-sm hidden-xs action-buttons">\
								<a class="greenFalabella" href="javascript:void(0)" data-info="' + item.TipoArchivo + "_" + item.DescripcionTipoArchivo + '">\
									<i class="ace-icon fa fa-database bigger-130"></i>\
								</a>\
							    </div>\
                            </td>\
                            <td>' + item.DescripcionTipoArchivo + '</td>\
                            <td>' + item.FechaArchivo + '</td>\
                            <td>' + item.FechaCargaIni + '</td>\
                            <td class="hidden-480">' + item.TiempoCarga + '</td>\
                            <td class="hidden-480">\
                                <span class="label label-' + _estadoClass[item.EstadoCarga] + ' arrowed-right arrowed-in">' + item.DescripcionEstadoCarga + '</span>\
                            </td>\
                       </tr>';
                    }
                    $('#tableCarga tbody').html(html);
                }
            }
        });
    }

    var initializeGrid = function (data) {
        _dtHistorial = $('#dtHistorial').DataTable({
            responsive: true,
            "bAutoWidth": false,
            language: {
                "url": _urlLanguage
            },
            data: data,
            columns: [
	            { data: "FechaArchivo", width: "15%" },
	            { data: "FechaCargaIni", width: "20%" },
	            { data: "TiempoCarga", width: "15%" },
	            {
	                data: function (oObj) {
	                    return '<td class="hidden-480">\
	                        		<span class="label label-' + _estadoClass[oObj.EstadoCarga] + ' arrowed-right arrowed-in">' + oObj.DescripcionEstadoCarga + '</span>\
                        		</td>';
	                },
	                orderable: false,
	                searchable: false,
	                width: "5%"
	            }
            ],
            columnDefs: [
	            {
	                targets: [2, 3],
	                className: "hidden-480"
	            }
            ],
            order: [[0, 'desc']]
        });
    }

    var cargaDatos = function () {
        webApp.Ajax({
            url: _urlHistorialCarga,
            parametros: {
                tipoArchivo: _tipoArchivo.id
            },
            success: function (response) {
                initializeGrid(response.Data);
            }
        });
    }

    var aplicarHandlers = function () {
        $("#linkReloadCarga").click(function (e) {
            e.stopPropagation();
            ultimaCarga();
        });

        $("#linkReloadHistorial").click(function (e) {
            e.stopPropagation();
            _dtHistorial.clear();
            _dtHistorial.destroy();
            cargaDatos();
        });

        $("#tableCarga").on("click", 'a[data-info]', function () {
            var tipoArchivo = $(this).data("info").split("_");
            _tipoArchivo = {
                id: tipoArchivo[0],
                nombre: tipoArchivo[1]
            };

            $('#spnTipoArchivo').text('- ' + _tipoArchivo.nombre);
            _dtHistorial.clear();
            _dtHistorial.destroy();
            cargaDatos();
        });
    }

    return {
        //main function to initiate page
        init: function (parametros) {
            _urlLanguage = parametros.urlLanguage;
            _urlUltimaCarga = parametros.urlUltimaCarga;
            _urlHistorialCarga = parametros.urlHistorialCarga;
            aplicarHandlers();
            ultimaCarga();
            initializeGrid([]);
        }
    };
}();