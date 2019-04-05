/**
 * Created by Allan on 5/19/2015.
 */

function ViewModule() { };

ViewModule.Title = "Password Retrieval";
ViewModule.minimal = null;

// page vars

// page controls

ViewModule.ButtonRetrieve = null;
ViewModule.LinkHome = $('#LinkHome');
ViewModule.TextEmail = $('#TextEmail');
ViewModule.AlertTextEmail = $('#AlertTextEmail');
ViewModule.ButtonReset = $('#ButtonReset');

ViewModule.AccountResetModal = $('#AccountResetModal');
ViewModule.AccountResetFailedModal = $('#AccountResetFailedModal');
ViewModule.ParagraphLoginResetStatus = $('#ParagraphLoginResetStatus');

ViewModule.Initialize = function(minimal, url, cb) {
    var self = this;

    self.minimal = minimal;

    // load page control references

    // initialize initial control state

    ViewModule.AlertTextEmail.hide();    

    // load up events

    ViewModule.LinkHome.click(function() {
        minimal.navigateToPage('home', true);
    });

    ViewModule.ButtonReset.click(function(e) {

        e.preventDefault();

        var validationFailed = false;   

        if (ViewModule.TextEmail.val().length == 0) {
            ViewModule.AlertTextEmail.show();
            validationFailed = true;
        }
        
        if (false == validationFailed) {

            var objectToSend = {};
            objectToSend.payload = {};

            // security service
            objectToSend.protocol = SendPasswordResetEmail;
            objectToSend.payload.email = ViewModule.TextEmail.val();

            postAjaj("api/Custom", objectToSend, function (data) {
                returnPayload = JSON.parse(data);

                if (returnPayload.succeeded == true) {                    
                    ViewModule.AccountResetModal.modal({
                        keyboard: false
                    });

                    setTimeout(function () {
                        ViewModule.AccountResetModal.hide();
                        ViewModule.AccountResetModal.removeData();
                        minimal.navigateToPage('login', true);
                    }, 3000);
                }
                else {                    
                    ViewModule.AccountResetFailedModal.modal({
                        keyboard: false
                    });

                    setTimeout(function () {
                        ViewModule.AccountResetModal.hide();
                        ViewModule.AccountResetModal.removeData();
                    }, 3000);
                }
            });
        }

    });

    cb();

};

ViewModule.Deinitialize = function() {
    var self = this;
};