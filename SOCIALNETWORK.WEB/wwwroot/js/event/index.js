var myeventsIndex = function () {
    var events = {
        onEventDetails: function () {
            $("#myevents-content").on("click", ".m-nav_details", function () {
                var id = $(this).data("id");
                window.location.href = `/evento/${id}/detalle`;
            })
        },
        onEventReport: function () {
            $("#myevents-content").on("click", ".m-nav_report", function () {
                toastr.warning("Opción en mantenimiento", "Información");
            })
        },
        getData: function () {
            $.ajax({
                url: "/evento/get-mis-eventos",
                type: "get"
            })
                .done(function (data) {
                    const div = $("#myevents-content");
                    let newHtml = "";

                    if (data.length === 0)
                        newHtml += "<h5 style='font-weight: 100;'>Aún no tiene eventos registrados.</h5>";

                    $.each(data, function (i, v) {
                        newHtml += `  <section class="m-portlet m-portlet--brand m-portlet--head-solid-bg">
        <div class="m-portlet__head">
            <div class="m-portlet__head-caption">
                <div class="m-portlet__head-title">
                    <span class="m-portlet__head-icon">
                        <i class="flaticon-exclamation"></i>
                    </span>
                    <h3 class="m-portlet__head-text">
                        ${v.Name}<small>Publicado por ${v.UserOwnerName}</small>
                    </h3>
                </div>
            </div>
            <div class="m-portlet__head-tools">
                <ul class="m-portlet__nav">
                    <li class="m-portlet__nav-item m-dropdown m-dropdown--inline m-dropdown--arrow m-dropdown--align-right m-dropdown--align-push" m-dropdown-toggle="hover">
                        <a href="#" class="m-portlet__nav-link m-portlet__nav-link--icon m-portlet__nav-link--icon-xl">
                            <i class="la la-ellipsis-h m--font-light"></i>
                        </a>
                        <div class="m-dropdown__wrapper">
                            <span class="m-dropdown__arrow m-dropdown__arrow--right m-dropdown__arrow--adjust"></span>
                            <div class="m-dropdown__inner">
                                <div class="m-dropdown__body">
                                    <div class="m-dropdown__content">
                                        <ul class="m-nav">
                                            <li class="m-nav__section m-nav__section--first">
                                                <span class="m-nav__section-text">Acciones</span>
                                            </li>
                                            <li class="m-nav__item">
                                                <a href="javascript:;" data-id='${v.Id}' class="m-nav__link m-nav_details">
                                                    <i class="m-nav__link-icon flaticon-eye"></i>
                                                    <span class="m-nav__link-text">Detalles</span>
                                                </a>
                                            </li>
                                            <li class="m-nav__item">
                                                <a href="javascript:;" data-id='${v.Id}' class="m-nav__link m-nav_report">
                                                    <i class="m-nav__link-icon flaticon-danger"></i>
                                                    <span class="m-nav__link-text">Reportar</span>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <div class="m-portlet__body">
            ${v.Description}
        </div>
    </section>`
                    })

                    div.html(newHtml);
                })
                .fail(function (jqXHR) {
                    toastr.error("Error al cargar los eventos.", "Error");
                })
                .always(function () {
                    $('#myevents-content').easyPaginate({
                        paginateElement: 'section',
                        elementsPerPage: 5,
                        effect: 'climb'
                    });
                })
        },
        init: function () {
            this.onEventDetails();
            this.onEventReport();
            this.getData();
        }
    }

    return {
        init: function () {
            events.init();
        }
    }
}();

$(() => myeventsIndex.init());