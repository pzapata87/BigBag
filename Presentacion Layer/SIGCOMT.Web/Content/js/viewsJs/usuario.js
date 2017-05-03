var usuarioJs = function () {
    var dtUsuario;
    var urlRoles;
    var urlListar;
    var urlLanguage;
    var urlGetUser;
	var roles;
	var actions = {
	    crear: { codigo: "1", uri: ""},
	    editar: { codigo: "2", uri: "" },
	    eliminar: { codigo: "3", uri: "" }
	};

	function initializeGrid() {
	    dtUsuario = $('#dtUsuario').DataTable({
	        responsive: true,
	        "bAutoWidth": false,
	        language: {
	            "url": urlLanguage
	        },
	        processing: true,
	        serverSide: true,
	        ajax: {
	            url: urlListar,
	            type: "POST",
	            dataType: 'json',
	            processData: false,
	            contentType: "application/json;charset=UTF-8",
	            data: function (data) {
	                return JSON.stringify(data);
	            }
	        },
	        columns: [
                {
                    data: function (oObj) {
                        return '<div class="hidden-sm hidden-xs action-buttons">\
									<a class="green" href="javascript:void(0)" data-action="' + actions.editar.codigo + '" data-info="' + oObj.Id + '">\
										<i class="ace-icon fa fa-pencil bigger-130"></i>\
									</a>\
									<a class="red" href="javascript:void(0)" data-action="' + actions.eliminar.codigo + '" data-info="' + oObj.Id + '">\
										<i class="ace-icon fa fa-trash-o bigger-130"></i>\
									</a>\
								</div>\
								<div class="hidden-md hidden-lg">\
									<div class="inline pos-rel">\
										<button class="btn btn-minier btn-yellow dropdown-toggle" data-toggle="dropdown" data-position="auto">\
											<i class="ace-icon fa fa-caret-down icon-only bigger-120"></i>\
										</button>\
										<ul class="dropdown-menu dropdown-only-icon dropdown-yellow dropdown-menu-right dropdown-caret dropdown-close">\
											<li>\
												<a href="javascript:void(0)" class="tooltip-success" data-action="' + actions.editar.codigo + '" data-info="' + oObj.Id + '" data-rel="tooltip" title="Editar">\
													<span class="green">\
														<i class="ace-icon fa fa-pencil-square-o bigger-120"></i>\
													</span>\
												</a>\
											</li>\
											<li>\
												<a href="javascript:void(0)" class="tooltip-error" data-action="' + actions.eliminar.codigo + '" data-info="' + oObj.Id + '" data-rel="tooltip" title="Eliminar">\
													<span class="red">\
														<i class="ace-icon fa fa-trash-o bigger-120"></i>\
													</span>\
												</a>\
											</li>\
										</ul>\
									</div>\
								</div>';
                    },
                    orderable: false,
                    searchable: false,
                    width: "10%"
                },
                { data: "Id", visible: false, searchable: false },
                { data: "UserName", width: "15%" },
                { data: "Nombre" },
                { data: "Apellido" },
                { data: "Email" }
	        ],
	        columnDefs: [
                {
                    targets: [3, 4, 5],
                    className: "hidden-480",
                    width: "20%"
                }
	        ],
	        order: [[2, 'desc']]
	    });
	};

	function getDatosUsuario(usuarioId) {
	    webApp.Ajax({
	        url: urlGetUser,
	        parametros: { usuarioId: usuarioId },
	        success: function (response) {
	            webApp.clearForm("#frmUsuario");
	            if (response.success) {
	                $("#frmUsuario #usuarioId").val(response.data.id);
	                $("#frmUsuario #userName").val(response.data.userName);
	                $("#frmUsuario #nombre").val(response.data.nombre);
	                $("#frmUsuario #apellido").val(response.data.apellido);
	                $("#frmUsuario #rolId").val(response.data.rol.id);

	                $("#divUsuario").modal("show");
	                $('#frmUsuario').valid();
	            } else {
	                $.gritter.add({
	                    title: 'Error',
	                    text: 'Error al cargar la informaci&oacute;n',
	                    class_name: 'gritter-error'
	                });
	            }
	        }
	    });
	}

	function getRoles(opciones) {
	    webApp.Ajax({
	        url: urlRoles,
	        success: function (response) {
	            if (response.success) {
	                roles = response.data;
	                if (roles) {
	                    $.each(roles, function (index, item) {
	                        $("#frmUsuario #rolId")
	                            .append("<option>", {
	                                value: item.id,
	                                text: item.nombre
	                            });
	                    });
	                }
	                if (opciones.success != null && typeof (opciones.success) == "function") {
	                    opciones.success();
	                }
	            }
	        }
	    });
	}
	
	var aplicarHandlers = function() {
        $("#btnAgregar").on("click", function () {
        	webApp.clearForm("#frmUsuario");
        	if(roles == null) {
        		getRoles({
        			success: function (){
        				$("#divUsuario").modal("show");
        			}
        		});
        	} else {
        		$('#frmUsuario #rolId').val("");
        		$("#divUsuario").modal("show");
        	}
        	$("#btnGuardar").data('action', actions.crear);
        });
        
        $("#usuarioDataTable").on("click", "a[data-info]", function () {
        	var usuarioId = $(this).data('info');
        	
        	if($(this).data('action') === actions.editar){
	        	if(roles == null) {
	        		getRoles({
	        			success: function (){
	        				getDatosUsuario(usuarioId);
	        			}
	        		});
	        	} else {
	        		getDatosUsuario(usuarioId);
	        	}	        	
	        	$("#btnGuardar").data('action', actions.editar);
        	} else {
        		webApp.showConfirmDialog(function () {
        			webApp.Ajax({
        			    url: actions.eliminar.uri,
                        parametros: { usuarioId: usuarioId },
                        success: function(response) {
                            if (response.success) {
                            	$.gritter.add({
            	    				title: 'Informaci&oacute;n',
            	    				text: 'Los datos se eliminaron correctamente.',
            	    				class_name: 'gritter-success'
            	    			});
                            } else {
                            	$.gritter.add({
            	    				title: 'Error',
            	    				text: 'Error al guardar la informaci&oacute;n',
            	    				class_name: 'gritter-error'
            	    			});                            
                            }
                        }
                    });
                }, 'Se eliminar&aacute; el registro. Est&aacute;s seguro que desea continuar?');
        	}
        });
        
        $("#btnGuardar").on("click", function (e) {
        	var $form = $('#frmUsuario');
        	if(!$form.valid()) {
        		e.preventDefault();
                return;
        	}        	
        	
        	var form = webApp.getDataForm($form);
            var url = $(this).data('action') === actions.crear.codigo ? actions.crear.uri : actions.editar.uri;
        	
        	webApp.showConfirmDialog(function() {
        		webApp.Ajax({
        		    url: url,
                    parametros: form.data,
                    success: function(response) {
                        if (response.success) {
                        	$.gritter.add({
        	    				title: 'Informaci&oacute;n',
        	    				text: 'Los datos se guardaron correctamente.',
        	    				class_name: 'gritter-success'
        	    			});
                        	
                        	$("#divUsuario").modal("hide");
                        } else {
                        	$.gritter.add({
        	    				title: 'Error',
        	    				text: 'Error al guardar la informaci&oacute;n',
        	    				class_name: 'gritter-error'
        	    			});                            
                        }
                    }
                });
            });
        });
	}
    
    return {
        //main function to initiate page
        init: function (parametros) {
            urlListar = parametros.urlListar;
            urlLanguage = parametros.urlLanguage;
            urlGetUser = parametros.urlGetUser;
            urlRoles = parametros.urlRoles;
            actions.crear.uri = parametros.urlCrear;
            actions.editar.uri = parametros.urlEditar;
            actions.eliminar.uri = parametros.urlEliminar;
            initializeGrid();
        	aplicarHandlers();
        }
    };
}();