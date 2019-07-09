var detailEvent = function () {
    var events = {
        onJoin: function () {
            $("#btn_join_event").on("click", function (e) {
                var id = $(this).data("id");

                mApp.block($("#portlet_detail"), {
                    overlayColor: '#000000',
                    type: 'loader',
                    state: 'primary',
                    message: 'Procesando...'
                });

                $.ajax({
                    url: `/evento/${id}/unirse`,
                    type:"get"
                })
                    .done(function (data) {
                        swal({
                            title: "Hecho!",
                            text: data,
                            type: "success",
                            showCancelButton: false,
                        }).then((result) => {
                            if (result.value) {
                                window.location.reload();
                            }
                        })
                    })
                    .fail(function (jqXHR) {
                        swal("Error", "Error al unirse al evento.", "error");
                    })
                    .always(function () {
                        mApp.unblock($("#portlet_detail"));
                    })
            })
        },
        onGoOut: function () {
            $("#btn_goout_event").on("click", function (e) {
                var id = $(this).data("id");

                mApp.block($("#portlet_detail"), {
                    overlayColor: '#000000',
                    type: 'loader',
                    state: 'primary',
                    message: 'Procesando...'
                });

                $.ajax({
                    url: `/evento/${id}/salir`,
                    type: "get"
                })
                    .done(function (data) {
                        swal({
                            title: "Hecho!",
                            text: data,
                            type: "success",
                            showCancelButton: false,
                        }).then((result) => {
                            if (result.value) {
                                window.location.reload();
                            }
                        })
                    })
                    .fail(function (jqXHR) {
                        swal("Error", "Error al salirse al evento.", "error");
                    })
                    .always(function () {
                        mApp.unblock($("#portlet_detail"));
                    })
            })
        },
        onDelete: function () {
            $("#btn_delete_event").on("click", function () {
                var id = $(this).data("id");

                mApp.block($("#portlet_detail"), {
                    overlayColor: '#000000',
                    type: 'loader',
                    state: 'primary',
                    message: 'Procesando...'
                });

                $.ajax({
                    url: `/evento/${id}/eliminar`,
                    type:"GET"
                })
                    .done(function (data) {
                        swal("Hecho!", data, "success");
                        window.location.href = "/inicio";
                    })
                    .fail(function (jqXHR) {
                        swal("Hecho!", "Error al eliminar el evento.", "error");
                    })
                    .always(function () {
                        mApp.unblock($("#portlet_detail"));
                    })

            })
        },
        onReport: function () {
            $("#opt_details").on("click", ".report_event", function () {
                console.log("test");
                toastr.warning("Opción en mantenimiento", "Información");
            })
        },
        onMembersShow: function () {
            $("#opt_details").on("click", ".modal_members", function () {
                $('#modal_members').modal('show');
                var id = $(this).data("id");
                membersModal.loadDatatable(id);
            })
        },
        onForum: function () {
            $("#opt_details").on("click", ".foro_event", function () {
                var id = $(this).data("id");
                window.location.href = `/foro/${id}`;
            })
        },
        init: function () {
            this.onJoin();
            this.onDelete();
            this.onGoOut();
            this.onReport();
            this.onMembersShow();
            this.onForum();
        }
    }

    var membersModal = {
        dataTableObject: null,
        loadDatatable: function (eventId) {
            if (this.dataTableObject !== null && this.dataTableObject !== undefined)
                this.dataTableObject.destroy();

            this.dataTableObject = $("#datatable_members").mDatatable({
                data: {
                    type: "remote",
                    source: {
                        read: {
                            url: `/evento/${eventId}/participantes`
                        }
                    },
                },
                columns: [
                    {
                        field: "Email",
                        title: "Correo Electónico",
                        textAlign: 'center'
                    },
                    {
                        field: 'FullName',
                        title: 'Nombre Completo',
                        textAlign: 'center',
                        template: function (row) {
                            return "<a href='https://joinus2k19.azurewebsites.net/evento/usuario/"+row.Id+"' target='_blank'>" + row.Name + " "+row.PatSurname+"</a>";
                        },
                    },
                    {
                        field: 'Estado',
                        title: 'Estado',
                        textAlign: 'center',
                        template: function (row) {
                            var status = {
                                0: { 'title': 'Desconectado', 'class': ' m-badge--metal' },
                                1: { 'title': 'En linea', 'class': ' m-badge--success' },
                            };
                            //return '<span class="m-badge ' + status[row.Status].class + ' m-badge--wide">' + status[row.Status].title + '</span>';
                            return '<span class="m-badge ' + status[0].class + ' m-badge--wide">' + status[0].title + '</span>';
                        },
                    }
                ]
            })
        }
    }

    return {
        init: function () {
            events.init();
        }
    }
}();

$(() => detailEvent.init());