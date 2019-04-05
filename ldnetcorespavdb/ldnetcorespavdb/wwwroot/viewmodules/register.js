/**
 * Created by Allan on 1/27/2015.
 */

function ViewModule() { };

ViewModule.Title = "Self Register";

ViewModule.minimal = null;

// page vars

// page controls

ViewModule.optiondonor = $('#optiondonor');
ViewModule.optionclient = $('#optionclient');
ViewModule.selectclientordonordiv = $('#selectclientordonordiv');
ViewModule.email = $('#email');
ViewModule.password = $('#password');
ViewModule.password_confirm = $('#password_confirm');
ViewModule.invalidemail = $('#invalidemail');
ViewModule.invalidpassword = $('#invalidpassword');
ViewModule.invalidconfirmpassword = $('#invalidconfirmpassword'); 
ViewModule.buttonregister = $('#buttonregister');
ViewModule.accountcreatedmodal = $('#accountcreatedmodal');
ViewModule.accountcreationfailedmodal = $('#accountcreationfailedmodal');

ViewModule.ValidateForm = function () {
    var ret = true;

    if (false === ViewModule.optiondonor.is(":checked") &&
        false === ViewModule.optionclient.is(":checked")) {
        ret = false;
        ViewModule.selectclientordonordiv.show();
    }

    if (ViewModule.email.val().length === 0) {
        ret = false;
        ViewModule.email.addClass('is-invalid');
    }

    if (ViewModule.password.val().length === 0) {
        ret = false;
        ViewModule.password.addClass('is-invalid');
    }

    if (ViewModule.password.val().length < 8) {
        ret = false;
        ViewModule.password.addClass('is-invalid');
    }

    if (ViewModule.password_confirm.val().length === 0) {
        ret = false;
        ViewModule.password_confirm.addClass('is-invalid');
    }

    if (ViewModule.password_confirm.val() !== ViewModule.password.val()) {
        ret = false;
        ViewModule.password_confirm.addClass('is-invalid');
    }

    return ret;
};

ViewModule.Initialize = function (minimal, url, cb) {
    var self = this;

    self.minimal = minimal;

    // load page control references

    // initialize initial control state

    ViewModule.optiondonor.button('toggle');

    ViewModule.selectclientordonordiv.hide();

    // load up events

    ViewModule.buttonregister.click(function () {

        ViewModule.invalidemail.hide();
        ViewModule.invalidpassword.hide();
        ViewModule.invalidconfirmpassword.hide();

        if (true === ViewModule.ValidateForm()) {

            var objectToSend = {};
            objectToSend.protocol = CreateUserAccount;
            objectToSend.payload = {};

            if (true === ViewModule.optiondonor.is(":checked")) {
                objectToSend.payload.accounttype = 2;
            }
            else {
                objectToSend.payload.accounttype = 3;
            }

            objectToSend.payload.email = ViewModule.email.val();
            objectToSend.payload.password = ViewModule.password.val();

            postAjaj("api/Custom", objectToSend, function (data) {
                returnPayload = JSON.parse(data);

                if (returnPayload.succeeded === true) {

                    ViewModule.accountcreatedmodal.modal({
                        keyboard: false
                    });
                }
                else {
                    ViewModule.accountcreationfailedmodal.modal({
                        keyboard: false
                    });
                }
            });
        }
    });

    cb();
};

ViewModule.Deinitialize = function () {
    var self = this;
};