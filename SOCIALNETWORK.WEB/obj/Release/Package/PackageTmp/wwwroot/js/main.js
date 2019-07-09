
//Select2----
(function () {
    if (jQuery && jQuery.fn && jQuery.fn.select2 && jQuery.fn.select2.amd) {
        var e = jQuery.fn.select2.amd;

        return e.define("select2/i18n/es", [], function () {
            return {
                errorLoading: function () {
                    return "No se pudieron cargar los resultados";
                },
                inputTooLong: function (e) {
                    var t = e.input.length - e.maximum,
                        n = "Por favor, elimine " + t + " car";

                    return t === 1 ? n += "ácter" : n += "acteres", n;
                },
                inputTooShort: function (e) {
                    var t = e.minimum - e.input.length,
                        n = "Por favor, introduzca " + t + " car";

                    return t === 1 ? n += "ácter" : n += "acteres", n;
                },
                loadingMore: function () {
                    return "Cargando más resultados…";
                },
                maximumSelected: function (e) {
                    var t = "Sólo puede seleccionar " + e.maximum + " elemento";

                    return e.maximum !== 1 && (t += "s"), t;
                },
                noResults: function () {
                    return "No se encontraron resultados";
                },
                searching: function () {
                    return "Buscando…";
                }
            };
        }), { define: e.define, require: e.require };
    }
})();
$.fn.select2.defaults.set("language", "es");

// --------
// JQuery Validate options
// --------

$.extend($.validator.messages, {
    email: "Por favor, escriba un correo electrónico válido.",
    required: "Este campo es obligatorio.",
});

$.validator.addMethod("isDni", function (value) {
    return /^\d{8}$/.test(value);
}, "N&uacute;mero de documento incorrecto.");

$.validator.addMethod("phoneNumber", function (value) {
    return /^(\d{9})?$/.test(value);
}, "N&uacute;mero de celular incorrecto.");

$.validator.addMethod("isFacebook", function (value) {
    return /(?:https?:\/\/)?(?:www\.)?facebook\.com\/.(?:(?:\w)*#!\/)?(?:pages\/)?(?:[\w\-]*\/)*([\w\-\.]*)/.test(value);
}, "Url de Facebook incorrecta.");

$.validator.addMethod("isTwitter", function (value) {
    return /(?:http:\/\/)?(?:www\.)?twitter\.com\/(?:(?:\w)*#!\/)?(?:pages\/)?(?:[\w\-]*\/)*([\w\-]*)/.test(value);
}, "Url de Twitter incorrecta.");

$.validator.addMethod("isLinkedin", function (value) {
    return /^https?:\/\/((www|\w\w)\.)?linkedin.com\/((in\/[^\/]+\/?)|(pub\/[^\/]+\/((\w|\d)+\/?){3}))$/.test(value);
}, "Url de Linkedin incorrecta.");

toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "1000",
    "hideDuration": "1000",
    "timeOut": "2000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

$.fn.mDatatable.defaults = {
    data: {
        type: "remote",
        source: null,
        pageSize: 10,
        saveState: {
            cookie: !1, //!0 ver anterior
            webstorage: !0
        },
        serverPaging: !1, //!0 ver anterio
        serverFiltering: !1, //!0 ver anterior
        serverSorting: !1, //!0 ver anterior
        autoColumns: !1,
        attr: {
            rowProps: []
        }
    },
    layout: {
        theme: "default",
        class: "m-datatable--brand",//        class: "", // custom wrapper class
        scroll: !1,
        height: null,
        footer: !1,
        header: !0,
        smoothScroll: {
            scrollbarShown: !0
        },
        spinner: {},
        icons: {
            sort: {
                asc: "la la-arrow-up",
                desc: "la la-arrow-down"
            },
            pagination: {
                next: "la la-angle-right",
                prev: "la la-angle-left",
                first: "la la-angle-double-left",
                last: "la la-angle-double-right",
                more: "la la-ellipsis-h"
            },
            rowDetail: {
                expand: "fa fa-caret-down",
                collapse: "fa fa-caret-right"
            }
        }
    },
    sortable: !0,
    resizable: !1,
    filterable: !1,
    pagination: !0,
    editable: !1,
    columns: [],
    search: {
        input: $("#generalSearch"),
        onEnter: !1,
        delay: 400
    },
    rows: {
        callback: function () { },
        beforeTemplate: function () { },
        afterTemplate: function () { },
        autoHide: !1
    },
    toolbar: {
        layout: ["pagination", "info"],
        placement: ["bottom"],
        items: {
            pagination: {
                type: "default",
                pages: {
                    desktop: {
                        layout: "default",
                        pagesNumber: 6
                    },
                    tablet: {
                        layout: "default",
                        pagesNumber: 3
                    },
                    mobile: {
                        layout: "compact"
                    }
                },
                navigation: {
                    prev: !0,
                    next: !0,
                    first: !0,
                    last: !0
                },
                pageSizeSelect: [10, 20, 30, 50, 100]
            },
            info: !0
        }
    },
    translate: {
        records: {
            noRecords: "No se encontraron registros",
            processing: "Cargando..."
        },
        toolbar: {
            pagination: {
                items: {
                    default: {
                        first: "Primero",
                        last: "Último",
                        more: "Más páginas",
                        next: "Siguiente",
                        prev: "Anterior",
                        input: "Nro. página",
                        select: "Seleccionar tamaño de página"
                    },
                    info: "Viendo {{start}} - {{end}} de {{total}} registros"
                }
            }
        }
    },
    extensions: {}
};

var main = function () {
    var events = {
        signOut: function () {
            $("#btn--main--singout").click(function () {
                window.location.href = "/cerrar-sesion";
            })
        },
        init: function () {
            this.signOut();
        }
    }

    var newEvent = {
        select2: function () {
            console.log($("#select-staff"));
            $("#select-staff").select2({
                placeholder: "-- Seleccionar personal --",
                language: "es",
                width: '100%',
                dropdownParent: $("#m_modal_4"),
            });
        },
        onDeleteRepeater: function () {
            $("#repeater-create").on("click", function () {
                $(".select-staff").each(function (index, element) {
                    console.log(element);
                    $("#form_new_event").find("[name='["+index+"][Staff]']").select2({
                        placeholder: "-- Seleccionar personal --",
                        language: "es",
                        width: '100%',
                        dropdownParent: $("#m_modal_4"),
                    })
                })
            });
        },
        m_repeater: function () {
            $('#m_repeater_1').repeater({
                initEmpty: false,

                defaultValues: {
                    'text-input': 'foo'
                },

                show: function () {
                    $(this).slideDown();
                },

                hide: function (deleteElement) {
                    $(this).slideUp(deleteElement);
                }
            });
        },
        form : {
            object: $("#form_new_event"),
            validate: function () {
                this.object.validate({
                    rules: {
                        Name: {
                            required: true
                        },
                        Description: {
                            required: true
                        }
                    },
                    submitHandler: function (f, e) {
                        e.preventDefault();
                        newEvent.form.submit();
                    }
                })
            },
            submit: function () {
                var peopleToNeedInput = $(".input-staff");
                

                var people = "";
                var quantity = 0;

                for (var i = 0; i < peopleToNeedInput.length; i++) {
                    people += $(peopleToNeedInput[i]).val()+"|";
                    quantity++;
                }

                if (quantity === 0) {
                    toastr.error("Debe ingresar por lo menos un personal.", "Error");
                    return;
                }

                var data = {
                    Name: this.object.find("[name='Name']").val(),
                    Description: this.object.find("[name='Description']").val(),
                    PeopleToNeed: people
                }

                mApp.block(this.object, {
                    overlayColor: '#000000',
                    type: 'loader',
                    state: 'primary',
                    message: 'Procesando...'
                });

                $.ajax({
                    type: "post",
                    url: "/evento/nuevo",
                    data: data
                })
                    .done(function (data) {
                        swal("Hecho!", data, "success");
                    })
                    .fail(function (jqXHR) {
                        swal("Error", "Error al crear el evento.", "error");
                    })
                    .always(function () {
                        mApp.unblock(newEvent.form.object);
                        $("#m_modal_4").modal("hide");
                        newEvent.form.object[0].reset();
                        location.reload();
                    })
            },
            init: function () {
                this.validate();
            }
        },
        init: function () {
            this.m_repeater();
            this.form.init();
            //this.select2();
            //this.onDeleteRepeater();
        }
    }

    return {
        init: function () {
            events.init();
            newEvent.init();
        }
    };
}();

$(() => main.init());