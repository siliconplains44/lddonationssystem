function ViewModule() { };

ViewModule.Title = "Leopard Data Minimal Home";

ViewModule.minimal = null;

// page vars

ViewModule.linkdashboard = $('#linkdashboard');

ViewModule.emailmessagesenableccheckbox = $('#emailmessagesenableccheckbox');
ViewModule.smsmessagesenableccheckbox = $('#smsmessagesenableccheckbox');

ViewModule.buttonsavesettings = $('#buttonsavesettings');

// dialogs

ViewModule.saveconfirmeddialog = $('#saveconfirmeddialog');

ViewModule.Initialize = function (minimal, url, cb) {
    var self = this;

    self.minimal = minimal;

    // load page control references

    objectToSend = allocObjectToSend();
    objectToSend.payload.accountid = application.loggedInUserID;
    objectToSend.payload.accounttypeid = application.AccountTypeID;

    runCustom(RetrieveNotificationSettingsByAccountIdAndAccountTypeId,
        objectToSend,
        function (returnPayload) {
            if (returnPayload.succeeded === true) {
                ViewModule.emailmessagesenableccheckbox.attr("checked", returnPayload.payload.EmailMessagesEnabled);
                ViewModule.smsmessagesenableccheckbox.attr("checked", returnPayload.payload.SmsMessagesEnabled);                
            }
        });

    // initialize initial control state

    // load up events

    ViewModule.linkdashboard.click(function (event) {
        event.preventDefault();
        minimal.navigateToPage("dashboard", true);
    });

    ViewModule.buttonsavesettings.click(function(event) {
        event.preventDefault();

        var objectToSend = allocObjectToSend();
        objectToSend.payload.accountid = application.loggedInUserID;
        objectToSend.payload.accounttypeid = application.AccountTypeID;
        objectToSend.payload.enableemailmessages = ViewModule.emailmessagesenableccheckbox.prop('checked');
        objectToSend.payload.enablesmsmessages = ViewModule.smsmessagesenableccheckbox.prop('checked');

        runCustom(SaveNotificationSettingsByAccountIdAndAccountTypeId,
            objectToSend,
            function (returnPayload) {
                if (returnPayload.succeeded === true) {
                    ViewModule.saveconfirmeddialog.modal({
                        keyboard: false
                    });

                    setTimeout(function () {
                        ViewModule.saveconfirmeddialog.hide();
                        ViewModule.saveconfirmeddialog.removeData();
                        $('body').removeClass('modal-open');
                        $('.modal-backdrop').remove();
                    }, 3000);
                }
            });        
    });

    cb();
};

ViewModule.Deinitialize = function () {
    var self = this;
};

