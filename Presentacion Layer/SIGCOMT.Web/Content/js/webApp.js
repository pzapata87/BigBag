/*
 *  Document   : webApp.js
 *  Description: Custom scripts and plugin initializations
 */

var webApp = function () {
    var popupMensaje = "popupMensaje";
    var popupConfirmacion = "popupConfirmacion";
    var mensajeEspera = "";
    var tituloPopupMensaje = "";
    var tituloPopupComfirmacion = "";
    var mensajePopupConfirmacion = "";
    var btnCancelar = "";
    var btnAceptar = "";
    
    var createMessageDialog = function () {
        var dialogMessage = "<div id='" + popupMensaje + "' tabindex='-1' role='dialog' aria-hidden='true' class='modal fade' data-backdrop='static' style='z-index:100000;'>";
        dialogMessage += "<div class='modal-dialog'>";
        dialogMessage += "<div class='modal-content'>";
        dialogMessage += "<div class='modal-header'>";
        dialogMessage += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
        dialogMessage += "<h4 class='modal-title'>" + tituloPopupMensaje + "</h4>";
        dialogMessage += "</div>";
        dialogMessage += "<div class='modal-body'></div>";
        dialogMessage += "<div class='modal-footer' style='margin-top: 0px; margin-bottom: 0px;'>";
        dialogMessage += "<button class='btn btn-success btn-aceptar' data-dismiss='modal'><i class='fa fa-thumbs-o-up'></i> " + btnAceptar + "</button>";
        dialogMessage += "</div>";
        dialogMessage += "</div>";
        dialogMessage += "</div>";

        $("body").append(dialogMessage);
    };
    
    var createConfirmDialog = function () {
        var dialogConfirm = "<div id='" + popupConfirmacion + "' tabindex='-1' role='dialog' aria-hidden='true' class='modal fade' data-backdrop='static' style='z-index:100000;'>";
        dialogConfirm += "<div class='modal-dialog'>";
        dialogConfirm += "<div class='modal-content'>";
        dialogConfirm += "<div class='modal-header'><h4 class='modal-title'>" + tituloPopupComfirmacion + "</h4></div>";
        dialogConfirm += "<div class='modal-body'><p></p></div>";
        dialogConfirm += "<div class='modal-footer' style='margin-top: 0px; margin-bottom: 0px;'>";
        dialogConfirm += "<button class='btn btn-danger' data-dismiss='modal'><i class='fa fa-remove'></i> " + btnCancelar + "</button> ";
        dialogConfirm += "<button class='btn btn-success' data-dismiss='modal'><i class='fa fa-thumbs-o-up'></i> " + btnAceptar + "</button>";
        dialogConfirm += "</div>";
        dialogConfirm += "</div>";
        dialogConfirm += "</div>";

        $("body").append(dialogConfirm);
    };

    var blockUISettings = function () {
        $.blockUI.defaults.baseZ = 100000;
        $.blockUI.defaults.overlayCSS.opacity = 0.4;
        $.blockUI.defaults.css.backgroundColor = "transparent";
        $.blockUI.defaults.css.border = '0px none';
        $.blockUI.defaults.fadeIn = 100;
        $.blockUI.defaults.fadeOut = 100;
    }
    
    var getDataForm = function (form) {
        var that = $(form);
        var url = that.attr('action');
        var type = that.attr('method');
        var data = {};

        var namex = "";
        that.find('[name]').each(function (index, value) {
            var that = $(this);
            var name = that.attr('name');
            var value = that.val();

            if (that.attr('type') === 'radio') {
                if (that.is(':checked')) {
                    data[name] = value;
                }
            } else if (that.attr('type') === 'checkbox') {
                if (that.is(':checked') && namex != name) {
                    data[name] = value;
                    namex = name;
                } else if (namex == name) {
                    namex = "";
                } 
            }
            else if (namex == name && that.attr('type') === 'hidden') {
                namex = "";
            }
            else {
                data[name] = value;
            }
        });

        var obj = {
            url: url,
            type: type,
            data: data
        };

        return obj;
    };

    var disableAllFormElements = function (formId) {
        $('#' + formId).find('input, textarea, button, select').attr('disabled', 'disabled');
    };
    
    var jsonToCsvConvertor = function (jsonData, reportTitle, showLabel, labelNames) {
        //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
        var arrData = typeof jsonData != 'object' ? JSON.parse(jsonData) : jsonData;
        
        //Set Report title in first row or line
        var csv = "," + reportTitle + '\r\n\n';        

        //This condition will generate the Label/Header
        if (showLabel) {
            var row = "";
            
            //This loop will extract the label from 1st index of on array
            for (var index in labelNames) {
                //Now convert each value to string and comma-seprated
                row += labelNames[index] + ',';
            }

            row = row.slice(0, -1);
            
            //append Label row with line break
            csv += row + '\r\n';
        }
        
        //1st loop is to extract each row
        for (var i = 0; i < arrData.length; i++) {
            var row = "";
            
            //2nd loop will extract each column and convert it in string comma-seprated
            for (var index in labelNames) {
                row += '"' + arrData[i][index] + '",';
            }

            row.slice(0, row.length - 1);
            
            //add a line break after each row
            csv += row + '\r\n';
        }
        
        //Generate a file name
        var fileName = "LogContable";
        //this will remove the blank-spaces from the title and replace it with an underscore
       // fileName += reportTitle.replace(/ /g,"_");   
        
        //Initialize file format you want csv or xls
        var uri = 'data:text/csv;charset=utf-8,' + escape(csv);
        
        // Now the little tricky part.
        // you can use either>> window.open(uri);
        // but this will not work in some browsers
        // or you will not get the correct file extension    
        
        //this trick will generate a temp <a /> tag
        var link = document.createElement("a");    
        link.href = uri;
        
        //set the visibility hidden so it will not effect on your web-layout
        link.style = "visibility:hidden";
        link.download = fileName + ".csv";
        
        //this part will append the anchor tag and remove it after automatic click
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }

    return {
        init: function (parametros) {
            if (parametros) {
                mensajeEspera = parametros.mensajeEspera;
                tituloPopupMensaje = parametros.tituloPopupMensaje;
                tituloPopupComfirmacion = parametros.tituloPopupComfirmacion;
                mensajePopupConfirmacion = parametros.mensajePopupConfirmacion;
                btnCancelar = parametros.btnCancelar;
                btnAceptar = parametros.btnAceptar;
            }
            blockUISettings();
            createMessageDialog();
            createConfirmDialog();
        },
        getDataForm: getDataForm,
        showLoading: function () {
            jQuery.blockUI({
                message: "Espere un momento por favor",
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: 1,
                    color: '#fff'
                }
            });
        },
        hideLoading: function () {
            jQuery.unblockUI();
        },
        showMessageDialog: function (message, fnAceptar) {
            $('#' + popupMensaje + ' .modal-body').html(message);
            $('#' + popupMensaje).modal('show');

            if ($.isFunction(fnAceptar)) {
                $('#' + popupMensaje + ' .btn-aceptar').off('click');
                $('#' + popupMensaje + ' .close').off('click');
                $('#' + popupMensaje + ' .btn-aceptar').on('click', fnAceptar);
                $('#' + popupMensaje + ' .close').on('click', fnAceptar);
            }
        },
        showConfirmDialog: function (fnSuccess, message, fnCancel) {        	
            var popup = $('#' + popupConfirmacion);
            var btnSuccess = $(popup).find('.btn-success');
            var btnCancel = $(popup).find('.btn-danger');

            btnSuccess.off('click');
            if ($.isFunction(fnSuccess)) {
                btnSuccess.on('click', function() { fnSuccess(); });
            }

            btnCancel.off('click');
            if ($.isFunction(fnCancel)) {
                btnCancel.on('click', function() { fnCancel(); });
            }

            if (message != null && message != '') {
                $('#' + popupConfirmacion + ' .modal-body p').html(message);
            } else {
                $('#' + popupConfirmacion + ' .modal-body p').html(mensajePopupConfirmacion);
            }
            
            popup.modal('show');
        },       
        formatResponse: function (respuesta, contenedor) {
            var estado = "";
            if (respuesta.Success) {
                if (!respuesta.Warning) {
                    estado = "alert-success";
                }
            } else {
                estado = "alert-danger";
            }
            $("#" + contenedor).append("<div class='alert " + estado + "'>" + respuesta.Message + "</div>");
        },
        clearResponse: function (contenedor) {
            $("#" + contenedor).html('');
        },
        clearForm: function(form) {
            $(":input", form)
                .not(":button, :submit, :reset, [type=hidden]")
                .val("");

            $(".select2-hidden-accessible", form).each(function() {
                var $this = $(this);
                if (!$this.hasClass("select-select2"))
                    $this.empty();
                $this.select2("val", "");
            });
            
            $(".date-picker", form).each(function() {
                $(this).datepicker("setDate", "");
            });

            $(".field-validation-error").empty();
        },
        Ajax: function (opciones) {
            if (opciones.url == null)
                opciones.url = "";

            if (opciones.parametros == null)
                opciones.parametros = {};

            if (opciones.async == null)
                opciones.async = true;

            if (opciones.datatype == null)
                opciones.datatype = "json";

            if (opciones.contentType == null)
                opciones.contentType = "application/json; charset=utf-8";

            if (opciones.type == null)
                opciones.type = "POST";

            $.ajax({
                type: opciones.type,
                url: opciones.url,
                contentType: opciones.contentType,
                dataType: opciones.datatype,
                async: opciones.async,
                data: (opciones.datatype == "json" && (opciones.convertToJson == null || opciones.convertToJson === true))
                    ? JSON.stringify(opciones.parametros)
                    : opciones.parametros,
                success: function (response) {
                    if (opciones.success != null && typeof (opciones.success) == "function")
                    	opciones.success(response);
                },
                failure: function (msg) {
                    if (opciones.failure != null && typeof (opciones.failure) == "function")
                    	opciones.failure(msg);
                },
                error: function (xhr, status, error) {
                    if (opciones.error != null && typeof (opciones.error) == "function")
                    	opciones.error(xhr);
                }
            });
        },
        datepicker: function() {
            $('.date-picker').datepicker({
                autoclose: true,
                todayHighlight: true,
                language: "es"
            })
            //show datepicker when clicking on the icon
            .next().on(ace.click_event, function() {
                $(this).prev().focus();
            });
        },
        datepickerMonth: function() {
            $('.date-picker-month').datepicker({
                autoclose: true,
                todayHighlight: true,
                minViewMode: 1,
                format: "mm/yyyy",
                language: "es"
            })
            //show datepicker when clicking on the icon
            .next().on(ace.click_event, function() {
                $(this).prev().focus();
            });
        },
        datepickerYear: function () {
            $('.date-picker-year').datepicker({
                autoclose: true,
                todayHighlight: true,
                minViewMode: 2,
                format: "yyyy",
                language: "es"
            })
            //show datepicker when clicking on the icon
            .next().on(ace.click_event, function () {
                $(this).prev().focus();
            });
        },
        daterange: function(datePickerIni, datePickerFin) {
            $(datePickerIni).datepicker().on("changeDate", function() {
                $(datePickerFin).datepicker("setStartDate", $(this).val());
            });

            $(datePickerFin).datepicker().on("changeDate", function() {
                $(datePickerIni).datepicker("setEndDate", $(this).val());
            });
        },
        disableAllFormElements: disableAllFormElements,
        jsonToCsvConvertor: jsonToCsvConvertor
    }
}();