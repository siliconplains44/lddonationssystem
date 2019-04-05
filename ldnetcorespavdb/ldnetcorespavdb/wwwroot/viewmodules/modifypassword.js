/**
 * Created by Allan on 1/27/2015.
 */

function ViewModule() {
};

ViewModule.Title = "Modify Password";
ViewModule.minimal = null;

// page vars

// page controls

ViewModule.TextNewPassword = $('#TextNewPassword');
ViewModule.TextRepeatNewPassword = $('#TextRepeatNewPassword');

ViewModule.AlertTextNewPassword = $('#AlertTextNewPassword');
ViewModule.AlertTextRepeatNewPassword = $('#AlertTextRepeatNewPassword');

ViewModule.ButtonSubmit = $('#ButtonSubmit');

ViewModule.AccountChangedModal = $('#AccountChangedModal');
ViewModule.AccountChangedFailedModal = $('#AccountChangedFailedModal');

ViewModule.Initialize = function (minimal, url, cb) {
    var self = this;

    self.minimal = minimal;

    var passwordResetCode = url.split("=");
    var passwordResetCode = passwordResetCode[1];

    // load page control references

    // initialize initial control state    

    // load up events    

    ViewModule.ButtonSubmit.click(function (e) {

        var validationFailed = false;
       
        if (ViewModule.TextNewPassword.val().length == 0) {
            validationFailed = true;
            ViewModule.AlertTextNewPassword.show();
        }

        if (ViewModule.TextNewPassword.val().length < 8) {
            validationFailed = true;
            ViewModule.AlertTextNewPassword.show();
        }

        if (ViewModule.TextRepeatNewPassword.val().length == 0) {
            validationFailed = true;
            ViewModule.AlertTextRepeatNewPassword.show();
        }

        if (ViewModule.TextRepeatNewPassword.val() != ViewModule.TextNewPassword.val()) {
            validationFailed = true;
            ViewModule.AlertTextRepeatNewPassword.show();
        }

        if (false == validationFailed) {

            var objectToSend = {};
            objectToSend.payload = {};

            // security service
            objectToSend.protocol = UpdateUserPassword;
            objectToSend.payload.passwordresetcode = passwordResetCode;
            objectToSend.payload.password = ViewModule.TextNewPassword.val();

            postAjaj("api/Custom", objectToSend, function (data) {
                returnPayload = JSON.parse(data);

                if (returnPayload.succeeded == true) {
                    ViewModule.AccountChangedModal.modal({
                        keyboard: false
                    });

                    setTimeout(function () {
                        ViewModule.AccountChangedModal.hide();
                        ViewModule.AccountChangedModal.removeData();
                    }, 3000);
                }
                else {

                    ViewModule.AccountChangedFailedModal.modal({
                        keyboard: false
                    });

                    setTimeout(function () {
                        ViewModule.AccountChangedFailedModal.hide();
                        ViewModule.AccountChangedFailedModal.removeData();
                    }, 5000);
                }
            });

        }
    });

    cb();
};

ViewModule.Deinitialize = function () {
    var self = this;
};