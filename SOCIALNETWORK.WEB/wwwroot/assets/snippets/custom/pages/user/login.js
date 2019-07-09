//== Class Definition
var SnippetLogin = function() {

    var login = $('#m_login');

    var showErrorMsg = function(form, type, msg) {
        var alert = $('<div class="m-alert m-alert--outline alert alert-' + type + ' alert-dismissible" role="alert">\
			<button type="button" class="close" data-dismiss="alert" aria-label="Close"></button>\
			<span></span>\
		</div>');

        form.find('.alert').remove();
        alert.prependTo(form);
        //alert.animateClass('fadeIn animated');
        mUtil.animateClass(alert[0], 'fadeIn animated');
        alert.find('span').html(msg);
    }

    //== Private Functions

    var displaySignUpForm = function() {
        login.removeClass('m-login--forget-password');
        login.removeClass('m-login--signin');

        login.addClass('m-login--signup');
        mUtil.animateClass(login.find('.m-login__signup')[0], 'flipInX animated');
    }

    var displaySignInForm = function() {
        login.removeClass('m-login--forget-password');
        login.removeClass('m-login--signup');

        login.addClass('m-login--signin');
        mUtil.animateClass(login.find('.m-login__signin')[0], 'flipInX animated');
        //login.find('.m-login__signin').animateClass('flipInX animated');
    }

    var displayForgetPasswordForm = function() {
        login.removeClass('m-login--signin');
        login.removeClass('m-login--signup');

        login.addClass('m-login--forget-password');
        //login.find('.m-login__forget-password').animateClass('flipInX animated');
        mUtil.animateClass(login.find('.m-login__forget-password')[0], 'flipInX animated');

    }

    var handleFormSwitch = function() {
        $('#m_login_forget_password').click(function(e) {
            e.preventDefault();
            displayForgetPasswordForm();
        });

        $('#m_login_forget_password_cancel').click(function(e) {
            e.preventDefault();
            displaySignInForm();
        });

        $('#m_login_signup').click(function(e) {
            e.preventDefault();
            displaySignUpForm();
        });

        $('#m_login_signup_cancel').click(function(e) {
            e.preventDefault();
            displaySignInForm();
        });
    }

    var handleSignInFormSubmit = function() {
        $('#m_login_signin_submit').click(function(e) {
            e.preventDefault();
            var btn = $(this);
            var form = $(this).closest('form');

            form.validate({
                rules: {
                    email: {
                        required: true,
                        email: true
                    },
                    password: {
                        required: true
                    }
                },
                messages: {
                    email: {
                        required: "Es necesario ingresar su correo electronico.",
                        email : "Ingresar un correo electronico valido."
                    },
                    password: {
                        required: "Es necesario ingresar una contrase&ntildea.",
                    }
                }
            });

            if (!form.valid()) {
                return;
            }

            btn.addClass('m-loader m-loader--right m-loader--light').attr('disabled', true);

            form.ajaxSubmit({
                url: '/ingreso',
                type: "post",
                data: $("#form_login").serialize(),
                success: function (response, status, xhr, $form) {
                    window.location.href = "/inicio";
                },
                error: function (data) {
                    console.log(data);
                    showErrorMsg(form, 'danger', data.statusText);
                    btn.removeClass('m-loader m-loader--right m-loader--light').attr('disabled', false);
                }
            });
        });
    }

    var handleSignUpFormSubmit = function() {
        $('#m_login_signup_submit').click(function(e) {
            e.preventDefault();

            var btn = $(this);
            var form = $(this).closest('form');

            form.validate({
                rules: {
                    Name: {
                        required: true
                    },
                    PatSurname: {
                        required: true
                    },
                    Password: {
                        required: true
                    },
                    PasswordVerifier: {
                        required: true
                    },
                    Email: {
                        required: true,
                        email: true
                    }
                },
                messages: {
                    Email: {
                        required: "Es necesario ingresar su correo electronico.",
                        email: "Ingresar un correo electronico valido."
                    },
                    Password: {
                        required: "Es necesario ingresar una contrase&ntildea."
                    },
                    PasswordVerifier: {
                        required: "Es necesario ingresar una contrase&ntildea."
                    },
                    PatSurname: {
                        required: "Este campo es requerido."
                    },
                    Name: {
                        required: "Este campo es requerido."
                    }
                }
            });

            if (!form.valid()) {
                return;
            }

            btn.addClass('m-loader m-loader--right m-loader--light').attr('disabled', true);

            $.ajax({
                url: '/registro',
                type: "post",
                data: $("#form_register").serialize()
            })
                .done(function (data) {
                    btn.removeClass('m-loader m-loader--right m-loader--light').attr('disabled', false);
                    form.clearForm();
                    form.validate().resetForm();
                    displaySignInForm();
                    var signInForm = login.find('.m-login__signin form');
                    showErrorMsg(signInForm, 'success', 'Usuario registrado, se ha enviado un correo de confirmacion.');
                    signInForm.clearForm();
                    signInForm.validate().resetForm();
                })
                .fail(function (jqXHR) {
                    showErrorMsg($("#form_register"), 'error', `Error. ${jqXHR.statusText}`);
                })
                .always(function () {
                    btn.removeClass('m-loader m-loader--right m-loader--light').attr('disabled', false);
                })
            

        });
    }

    var handleForgetPasswordFormSubmit = function() {
        $('#m_login_forget_password_submit').click(function(e) {
            e.preventDefault();

            var btn = $(this);
            var form = $(this).closest('form');

            form.validate({
                rules: {
                    email: {
                        required: true,
                        email: true
                    }
                },
                messages: {
                    email: {
                        required: "Es necesario ingresar su correo electronico.",
                        email: "Ingresar un correo electronico valido."
                    }
                }
            });

            if (!form.valid()) {
                return;
            }

            btn.addClass('m-loader m-loader--right m-loader--light').attr('disabled', true);

            form.ajaxSubmit({
                url: '',
                success: function(response, status, xhr, $form) { 
                	// similate 2s delay
                	setTimeout(function() {
                		btn.removeClass('m-loader m-loader--right m-loader--light').attr('disabled', false); // remove 
	                    form.clearForm(); // clear form
	                    form.validate().resetForm(); // reset validation states

	                    // display signup form
	                    displaySignInForm();
	                    var signInForm = login.find('.m-login__signin form');
	                    signInForm.clearForm();
	                    signInForm.validate().resetForm();

	                    showErrorMsg(signInForm, 'success', 'Se han enviado las intrucciones para recuperar la contrase&ntildea');
                	}, 2000);
                }
            });
        });
    }

    //== Public Functions
    return {
        // public functions
        init: function() {
            handleFormSwitch();
            handleSignInFormSubmit();
            handleSignUpFormSubmit();
            handleForgetPasswordFormSubmit();
        }
    };
}();

//== Class Initialization
jQuery(document).ready(function() {
    SnippetLogin.init();
});