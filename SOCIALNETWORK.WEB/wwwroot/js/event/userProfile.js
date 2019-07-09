var userProfile = function () {

    var dataTable = {
        object: null,
        options: {
            data: {
                type: "remote",
                source: {
                    read: {
                        url: `/evento/get-eventos-perfil?userId=` + $("#userId").val()
                    }
                },
            },
            columns: [
                {
                    field: "Name",
                    title: "Nombre",
                    textAlign: 'center'
                },
                {
                    field: 'UserOwnerName',
                    title: 'Publicado por',
                    textAlign: 'center'
                },
                {
                    field: 'Estado',
                    title: 'Estado',
                    textAlign: 'center',
                    template: function (row) {
                        return '<button type="button" class="btn m-btn--pill btn-primary btn-sm btn-view" data-id='+row.Id+'>Ver Evento</button>';
                    },
                }
            ]
        },
        events: {
            loadDatatable: function () {
                dataTable.object = $("#ajax_data").mDatatable(dataTable.options);
            },
            onView: function () {
                $("#ajax_data").on("click", ".btn-view", function () {
                    var id = $(this).data("id");
                    window.location.href = "/evento/" + id+"/detalle";
                })
            },
            init: function () {
                this.onView();
                this.loadDatatable();
            }
        },
        init: function () {
            this.events.init();
        }
    }

    return {
        init: function () {
            dataTable.init();
        }
    }
}();

$(() => userProfile.init());