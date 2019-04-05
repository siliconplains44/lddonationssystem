/**
 * Created by Allan on 1/27/2015.
 */

function ViewModule() { };

ViewModule.Title = "Manage My Personal Information";

ViewModule.minimal = null;

// page vars

// page controls

ViewModule.linkdashboard = $('#linkdashboard');

ViewModule.TextLastName = $('#TextLastName');
ViewModule.TextMiddleName = $('#TextMiddleName');
ViewModule.TextFirstName = $('#TextFirstName');
ViewModule.TextAddressLine1 = $('#TextAddressLine1');
ViewModule.TextAddressLine2 = $('#TextAddressLine2');
ViewModule.TextMobilePhoneNumber = $('#TextMobilePhoneNumber');
ViewModule.TextHomePhoneNumber = $('#TextHomePhoneNumber');
ViewModule.TextBirthdate = $('#TextBirthdate');
ViewModule.TextCity = $('#TextCity');
ViewModule.SelectState = $('#SelectState');
ViewModule.TextZip = $('#TextZip');

ViewModule.SignInButton = $("#SignInButton");

ViewModule.SavedPersonalInformationModal = $('#SavedPersonalInformationModal');

ViewModule.Initialize = function (minimal, url, cb) {
    var self = this;

    self.minimal = minimal;

    // load page control references

    // initialize initial control state

    var objectToSend = {};
    objectToSend.protocol = RetrieveIndividualInformation;
    objectToSend.payload = {};
    objectToSend.payload.loggedinsecurityuserid = application.loggedInUserID;

    postAjaj("api/Custom", objectToSend, function (data) {
        returnPayload = JSON.parse(data);

        if (returnPayload.succeeded == true) {

            var birthDateString = moment(returnPayload.payload.Birthdate);

            ViewModule.TextLastName.val(returnPayload.payload.LastName);
            ViewModule.TextMiddleName.val(returnPayload.payload.MiddleName);
            ViewModule.TextFirstName.val(returnPayload.payload.FirstName);
            ViewModule.TextAddressLine1.val(returnPayload.payload.AddressLine1);
            ViewModule.TextAddressLine2.val(returnPayload.payload.AddressLine2);
            ViewModule.TextMobilePhoneNumber.val(returnPayload.payload.MobilePhoneNumber);
            ViewModule.TextHomePhoneNumber.val(returnPayload.payload.HomePhoneNumber);
            ViewModule.TextBirthdate.val(birthDateString.format("MM/DD/YYYY"));
            ViewModule.TextCity.val(returnPayload.payload.City);
            ViewModule.SelectState.val(returnPayload.payload.State);
            ViewModule.TextZip.val(returnPayload.payload.Zip);

        }
    });

    // load up events

    ViewModule.SignInButton.click(function () {

        var objectToSend = {};
        objectToSend.protocol = SaveIndividualInformation;
        objectToSend.payload = {};
        objectToSend.payload.loggedinsecurityuserid = application.loggedInUserID;

        objectToSend.payload.LastName = ViewModule.TextLastName.val();
        objectToSend.payload.MiddleName = ViewModule.TextMiddleName.val();
        objectToSend.payload.FirstName = ViewModule.TextFirstName.val();
        objectToSend.payload.AddressLine1 = ViewModule.TextAddressLine1.val();
        objectToSend.payload.AddressLine2 = ViewModule.TextAddressLine2.val();
        objectToSend.payload.MobilePhoneNumber = ViewModule.TextMobilePhoneNumber.val();
        objectToSend.payload.HomePhoneNumber = ViewModule.TextHomePhoneNumber.val();
        objectToSend.payload.Birthdate = ViewModule.TextBirthdate.val();
        objectToSend.payload.City = ViewModule.TextCity.val();
        objectToSend.payload.State = ViewModule.SelectState.val();
        objectToSend.payload.Zip = ViewModule.TextZip.val();

        postAjaj("api/Custom", objectToSend, function (data) {
            returnPayload = JSON.parse(data);

            if (returnPayload.succeeded == true) {
                ViewModule.SavedPersonalInformationModal.modal({
                    keyboard: false
                });

                setTimeout(function () {
                    ViewModule.SavedPersonalInformationModal.hide();
                    ViewModule.SavedPersonalInformationModal.removeData();
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                }, 3000);
            }
        });

    });

    ViewModule.linkdashboard.click(function(event){
        event.preventDefault();
        ViewModule.minimal.navigateToPage('dashboard', true);
    });

    cb();
};

ViewModule.Deinitialize = function () {
    var self = this;
};