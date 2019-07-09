var userIndex = function () {
    var form = {
        object: $("#form_profile"),
        validate: function () {
            this.object.validate({
                rules: {
                    Name: {
                        required: true
                    },
                    PhoneNumber: {
                        required: true,
                        phoneNumber: true
                    },
                    Dni: {
                        required: true, 
                        isDni: true
                    },
                    StudyCenterId: {
                        required: true
                    },
                    DegreeId: {
                        required: true
                    },
                    //FacebookUrl: {
                    //    isFacebook: true
                    //},
                    //TwitterUrl: {
                    //    isTwitter: true
                    //},
                    //LinkendlnUrl: {
                    //    isLinkedin : true
                    //}
                },
                submitHandler: function (f, e) {
                    e.preventDefault();
                    form.submit();
                }
            })
        },
        submit: function () {

            mApp.block(form.object, {
                overlayColor: '#000000',
                type: 'loader',
                state: 'primary',
                message: 'Procesando...'
            });

            $.ajax({
                type: "post",
                url: "/perfil/actualizar-perfil",
                data: form.object.serialize()
            })
                .done(function (data) {
                    swal("Hecho!", data, "success");
                })
                .fail(function (jqXHR) {
                    swal("Error", "Error al actualizar los datos.", "error");
                })
                .always(function () {
                    mApp.unblock(form.object);
                })
        },
        init: function () {
            this.validate();
        }
    }

    var profilePicture = {
        events : {
            imageClick: function () {
                $("#image_profile").on("click", function () {
                    $("#input_image_profile").click();
                })
            },
            onChange: function () {
                $("#input_image_profile").on("change", function () {
                    if (this.files && this.files[0]) {
                        const file = this.files[0];
                        const fileType = file["type"];
                        const validFormats = ["image/gif", "image/jpeg", "image/png"];

                        if ($.inArray(fileType, validFormats) < 0) {
                            e.preventDefault();
                            $(this).val(null);
                            $(".custom-file-label").text("Subir imagen del producto...");
                            toastr.error("La imagen no cuenta con el formato adecuado.", "Error");
                            return;
                        }
                    }
                    const reader = new FileReader();
                    reader.onload = function (e) {
                        $("#image_profile").attr("src", e.target.result);
                    }
                    reader.readAsDataURL(this.files[0]);

                    var file = this.files[0];
                    var formData = new FormData();
                    formData.append("file", file);

                    mApp.block(form.object, {
                        overlayColor: '#000000',
                        type: 'loader',
                        state: 'primary',
                        message: 'Procesando...'
                    });

                    $.ajax({
                        url: "/perfil/actualizar-foto",
                        type: "post",
                        data: formData,
                        dataType: 'json',
                        contentType: false,
                        processData: false
                    })
                        .done(function (data) {
                            toastr.success(data, "Hecho !");
                        })
                        .fail(function (jqXHR) {
                            toastr.error("Error al actualiar", "Error");
                        })
                        .always(function () {
                            mApp.unblock(form.object);
                        })
                })
            },
            init: function () {
                this.onChange();
                this.imageClick();
            }
        },
        init: function () {
            this.events.init();
        }
       
    }

    var select2 = {
        studyCenter: {
            id: form.object.find("[name='StudyCenterId']").val(),
            object: form.object.find("[name='StudyCenterId']"),
            events: {
                onChange: function () {
                    select2.studyCenter.object.on("change", function () {
                        select2.studyCenter.id = select2.studyCenter.object.val();
                        if (select2.studyCenter.id !== null && select2.studyCenter.id !== undefined) {
                            $.ajax({
                                url: `/perfil/listar-carreras?studyCenterId=` + select2.studyCenter.id,
                                type: "get",
                            })
                                .done(function (data) {
                                    select2.degree.object.empty();
                                    data.forEach(function (e) {
                                        var newOption = new Option(e.Text, e.Id, false, false);
                                        select2.degree.object.append(newOption).trigger("change");
                                    })
                                })
                                .fail(function (jqXHR) {
                                    console.log(jqXHR);
                                });
                        }
                    });
                },
                init: function () {
                    this.onChange();
                }
            },
            init: function () {
                this.object.select2({
                    placeholder: "-- Seleccionar centro de estudios --",
                    language : 'es'
                });
                this.events.init();
            }
        },
        degree: {
            object: form.object.find("[name='DegreeId']"),
            init: function () {
                this.object.select2({
                    placeholder: "-- Seleccionar carrea --",
                    language : 'es'
                });
            }
        },
        init: function () {
            this.studyCenter.init();
            this.degree.init();
        }
    }

    return {
        init: function () {
            select2.init();
            form.init();
            profilePicture.init();
        }
    }
}();

$(() => userIndex.init());