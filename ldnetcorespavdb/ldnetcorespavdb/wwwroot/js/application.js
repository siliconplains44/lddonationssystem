/**
 * Created by Allan on 1/26/2015.
 */

application = {};

application.loggedInUserID = null;
application.Username = null;
application.timeout = null;
application.minimal = null;

application.initialize = function(bodyElement) {

    minimal.bodyElement = bodyElement;

    minimal.registerRootViewModulesPath('viewmodules');

    minimal.registerViewModules(
        [
            // non session

            { id: "register", title: "Register", path: "register", requiresession: false },
            { id: "confirmregistration", title: "Confirm Registration", path: "confirmregistration", requiresession: false },
            { id: "home", title: "Home", path: "home", requiressession: false },
            { id: "about", title: "About", path: "about", requiressession: false },
            { id: "contact", title: "Contact", path: "contact", requiressession: false },
            { id: "login", title: "Login", path: "login", requiressession: false },            
            { id: "loggedout", title: "LoggedOut", path: "loggedout", requiressession: false },
            { id: "modifypassword", title: "Modify Password", path: "modifypassword", requiressession: false },
            { id: "resetpassword", title: "Reset Password", path: "resetpassword", requiressession: false },

            // requires session

            { id: "dashboard", title: "Dashboard", path: "dashboard", requiressession: true },
            { id: "managepersonalinformation", title: "ManagePersonalInformation", path: "managepersonalinformation", requiresession: true },
            { id: "manageprograms", title: "Manage Programs", path: "manageprograms", requiressession: true },
            { id: "addeditprogram", title: "Add/Edit Program", path: "addeditprogram", requiressession: true },
            { id: "addeditprogramevent", title: "Add/Edit Program Event", path: "addeditprogramevent", requiressession: true },
            { id: "manageadministrators", title: "Manage Administrators", path: "manageadministrators", requiressession: true },
            { id: "managedonors", title: "Manage Donors", path: "managedonors", requiressession: true },
            { id: "manageclients", title: "Manage Clients", path: "manageclients", requiressession: true },
            { id: "manageclient", title: "Manage Client", path: "manageclient", requiressession: true },
            { id: "managedonor", title: "Manage Donor", path: "managedonor", requiressession: true },
            { id: "managemyinbox", title: "Manage My Inbox", path: "managemyinbox", requiressession: true },
            { id: "managemysettings", title: "Manage My settings", path: "managemysettings", requiressession: true },
            { id: "clientmanagemyprograms", title: "Client Manage My Programs", path: "clientmanagemyprograms", requiressession: true },
            { id: "donormanagemyprogramdonations", title: "Donor Manage My Program Donations", path: "donormanagemyprogramdonations", requiressession: true },
            { id: "donordonatemoney", title: "Donor Donate Money", path: "donordonatemoney", requiressession: true },
            { id: "donorviewgiftrecipientphotos", title: "Donor View Gift Recipient Photos", path: "donorviewgiftrecipientphotos", requiressession: true }
        ]
    );

    $(document).click(function (event) {
        application.onClick();
    });

    minimal.navigateToPage("home", true);

    return application.minimal;
};

application.onClick = function () {
    if (application.loggedInUserID !== null)
        application.timeout = moment().add(2, 'm');
};

application.login = function () {

    $('#menulogin').html("Logout");
    $('#menulogin').attr("href", "#loggedout");

    application.timeout = moment().add(2, 'm');

    setInterval(function () {
        if (moment().isAfter(application.timeout)) {            
            application.LogOut(true);
        }
    },
        5000);
};

application.LogOut = function (fromTimer) {

    clearInterval();

    application.loggedInUserID = null;
    application.timeout = null;

    $('#menulogin').html("Login");
    $('#menulogin').attr("href", "#login");

    if (fromTimer === true)
        minimal.navigateToPage("loggedout");
};

window.onbeforeunload = function() {
    application.LogOut();
};
