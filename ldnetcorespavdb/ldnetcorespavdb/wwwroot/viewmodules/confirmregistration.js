/**
 * Created by Allan on 1/27/2015.
 */

function ViewModule() { };

ViewModule.Title = "Leopard Data Minimal Confirm Registration";

ViewModule.minimal = null;

// page vars

// page controls

ViewModule.didconfirmsuccessfully = $('#didconfirmsuccessfully');
ViewModule.loginnow = $('#loginnow');

ViewModule.Initialize = function (minimal, url, cb) {
    var self = this;

    self.minimal = minimal;

    // load page control references

    // initialize initial control state

    ViewModule.didconfirmsuccessfully.hide();
    ViewModule.loginnow.hide();

    // load up events

    // split url to get registerId

    var registerId = url.split("=");
    var registerId = registerId[1];

    var objectToSend = {};
    objectToSend.protocol = RegisterAccountByRegistrationId;
    objectToSend.payload = registerId;

    postAjaj("api/Custom", objectToSend, function (data) {
        returnPayload = JSON.parse(data);

        if (returnPayload.succeeded == true) {
            ViewModule.didconfirmsuccessfully.text("Your account has been confirmed, you can now login to this site!");
            ViewModule.didconfirmsuccessfully.show();
            ViewModule.loginnow.show();
        }        
        cb();
    });     
};

ViewModule.Deinitialize = function () {
    var self = this;
};



