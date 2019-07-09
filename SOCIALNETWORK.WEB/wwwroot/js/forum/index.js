var forumIndex = function () {
    var messages = {
        instance: false,
        count : 0,
        getData: function () {
            mApp.block($(".m-portlet__body"), {
                overlayColor: '#000000',
                type: 'loader',
                state: 'primary',
                message: 'Cargando mensajes...'
            });

            var eventId = $("#eventId").val();
            $.ajax({
                url: "/foro/get-mensajes?eventId="+eventId,
                type:"get"
            })
                .done(function (data) {
                    if (data.length > messages.count) {
                        const div = $("#message_container");
                        const userId = $("#userId").val();
                        let newHtml = "";
                        $.each(data, function (i, v) {
                            if (userId !== v.UserId) {
                                newHtml += `<div class="m-messenger__wrapper">
                    <div class="m-messenger__message m-messenger__message--in">
                        <div class="m-messenger__message-pic">
                            <img src="https://stafforgserv.com.au/wp-content/uploads/2018/09/user-img.png" alt="" />
                        </div>
                        <div class="m-messenger__message-body">
                            <div class="m-messenger__message-arrow"></div>
                            <div class="m-messenger__message-content">
                                <div class="m-messenger__message-username">
                                    ${v.Name} ${v.PatSurname} - ${v.CreatedAt}
                                </div>
                                <div class="m-messenger__message-text">
                                    ${v.Message}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`
                            } else {
                                newHtml += `<div class="m-messenger__wrapper">
                    <div class="m-messenger__message m-messenger__message--out">
                        <div class="m-messenger__message-body">
                            <div class="m-messenger__message-arrow"></div>
                            <div class="m-messenger__message-content">
                                <div class="m-messenger__message-text">
                                    ${v.Message}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`
                            }
                        })
                        div.html(newHtml);
                        messages.count = data.length;
                    }
                })
                .fail(function (jqXHR) {
                    toastr.error("Error al cargar la pagina.", "Error");
                })
                .always(function () {
                    mApp.unblock($(".m-portlet__body"));
                })
        },
        getData2: function () {
            var eventId = $("#eventId").val();
            $.ajax({
                url: "/foro/get-mensajes?eventId=" + eventId,
                type: "get"
            })
                .done(function (data) {
                    if (data.length > messages.count) {
                        const div = $("#message_container");
                        const userId = $("#userId").val();
                        let newHtml = "";
                        $.each(data, function (i, v) {
                            if (userId !== v.UserId) {
                                newHtml += `<div class="m-messenger__wrapper">
                    <div class="m-messenger__message m-messenger__message--in">
                        <div class="m-messenger__message-pic">
                            <img src="https://stafforgserv.com.au/wp-content/uploads/2018/09/user-img.png" alt="" />
                        </div>
                        <div class="m-messenger__message-body">
                            <div class="m-messenger__message-arrow"></div>
                            <div class="m-messenger__message-content">
                                <div class="m-messenger__message-username">
                                    ${v.Name} ${v.PatSurname} - ${v.CreatedAt}
                                </div>
                                <div class="m-messenger__message-text">
                                    ${v.Message}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`
                            } else {
                                newHtml += `<div class="m-messenger__wrapper">
                    <div class="m-messenger__message m-messenger__message--out">
                        <div class="m-messenger__message-body">
                            <div class="m-messenger__message-arrow"></div>
                            <div class="m-messenger__message-content">
                                <div class="m-messenger__message-text">
                                    ${v.Message}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`
                            }
                        })
                        div.html(newHtml);
                        messages.count = data.length;
                    }
                })
                .fail(function (jqXHR) {
                    toastr.error("Error al cargar la pagina.", "Error");
                })
        },
        newMessage: function () {
            $(".btn_newMessage").on("click", function () {
                var userId = $("#userId").val();
                var eventId = $("#eventId").val();
                var message = $("#new_message").val();

                if (message === null || message === "") {
                    toastr.error("Ingresar mensaje.", "Error");
                    return;
                }

                var data = {
                    UserId : userId,
                    EventId : eventId,
                    Message : message
                }
                
                mApp.block($("#message_container"), {
                    overlayColor: '#000000',
                    type: 'loader',
                    state: 'primary',
                    message: 'Cargando mensajes...'
                });

                $.ajax({
                    type: "post",
                    url: "/foro/nuevo-mensaje",
                    data: data
                })
                    .done(function (data) {
                        messages.getData();
                        $("#new_message").val("");
                    })
                    .fail(function (jqXHR) {
                        toastr.error("Error al enviar el mensaje.", "Error");
                    })
                    .always(function () {
                        mApp.unblock($("#message_container"));
                    })
            })
        },
        fake: function () {
            if (messages.instance === false) {
                messages.instance = true;
                messages.getData2();
                messages.fake();
            } else {
                setTimeout(function () {
                    messages.instance = false;
                    messages.fake();
                }, 7000);
            }
        },
        init: function () {
            this.getData();
            this.newMessage();
            this.fake();
        }
    }

    var textArea = {
        init: function () {
            var textArea = $("#new_message");
            autosize(textArea);
        }
    }

    return {
        init: function () {
            messages.init();
            textArea.init();
        }
    }
}();

$(() => forumIndex.init());