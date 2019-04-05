/**
 * Created by Allan on 1/27/2015.
 */

function ViewModule() { };

ViewModule.Title = "Dashboard";
ViewModule.minimal = null;

// page vars

// page controls

// all account types

// admin

ViewModule.adminwelcome = $('#adminwelcome');

ViewModule.buttonmanageprograms = $("#buttonmanageprograms");
ViewModule.buttonmanagesystemadministrators = $("#buttonmanagesystemadministrators");
ViewModule.buttonmanagedonors = $("#buttonmanagedonors");
ViewModule.buttonmanageclients = $("#buttonmanageclients");
ViewModule.buttongotomyinbox = $("#buttongotomyinbox");
ViewModule.buttongotomysettings = $("#buttongotomysettings");

// donor

ViewModule.donorwelcome = $('#donorwelcome');

ViewModule.buttondonormanagemyprogramdonations = $('#buttondonormanagemyprogramdonations');
ViewModule.buttondonordonatemoney = $('#buttondonordonatemoney');
ViewModule.buttonviewhappygiftrecipientphotos = $('#buttonviewhappygiftrecipientphotos');
ViewModule.buttondonorgotomyinbox = $('#buttondonorgotomyinbox');
ViewModule.buttondonorgotomysettings = $('#buttondonorgotomysettings');
ViewModule.buttondonormanagepersonalinformation = $('#buttondonormanagepersonalinformation');

// client

ViewModule.clientwelcome = $('#clientwelcome');

ViewModule.buttonclientmanagemyfamily = $('#buttonclientmanagemyfamily');
ViewModule.buttonclientmanagemyprograms = $('#buttonclientmanagemyprograms');
ViewModule.buttonclientgotomyinbox = $('#buttonclientgotomyinbox');
ViewModule.buttonclientgotomysettings = $('#buttonclientgotomysettings');
ViewModule.buttonclientmanagepersonalinformation = $('#buttonclientmanagepersonalinformation');

// everyone

ViewModule.LinkLogout = $('#LinkLogout');

ViewModule.LinkModifyMyAccountLogin = $('#LinkModifyMyAccountLogin');
ViewModule.LinkReportABug = $('#LinkReportABug');
ViewModule.LinkReportASiteProblem = $('#LinkReportASiteProblem');
ViewModule.LinkRequestASalesCall = $('#LinkRequestASalesCall');
ViewModule.LinkTermsOfService = $('#LinkTermsOfService');
ViewModule.buttonmanagepersonalinformation = $('#buttonmanagepersonalinformation');

// administrator 

ViewModule.DivAdministrator = $('#administrator');

// donor

ViewModule.DivDonor = $('#donor');

// client

ViewModule.DivClient = $('#client');

ViewModule.Initialize = function(minimal, url, cb) {
    var self = this;

    self.minimal = minimal;

    // load page control references

    // initialize initial control state

    // load up events

    // admin

    ViewModule.buttonmanageprograms.click(function(event) {
        event.preventDefault(); 
        ViewModule.minimal.navigateToPage('manageprograms', true);
    });

    ViewModule.buttonmanagepersonalinformation.click(function(event) {
        event.preventDefault();
        ViewModule.minimal.navigateToPage('managepersonalinformation', true);
    });

    ViewModule.buttonmanagesystemadministrators.click(function(event){
        event.preventDefault();
        ViewModule.minimal.navigateToPage("manageadministrators", true);
    });

    ViewModule.buttonmanagedonors.click(function(event){
        event.preventDefault();
        ViewModule.minimal.navigateToPage("managedonors", true);
    });

    ViewModule.buttonmanageclients.click(function(event){
        event.preventDefault();
        ViewModule.minimal.navigateToPage("manageclients", true);
    });

    ViewModule.buttongotomyinbox.click(function(event) {
        event.preventDefault();
        ViewModule.minimal.navigateToPage("managemyinbox", true);
    });

    ViewModule.buttongotomysettings.click(function(event) {
        event.preventDefault();
        ViewModule.minimal.navigateToPage("managemysettings", true);
    });

    // donor

    ViewModule.buttondonormanagemyprogramdonations.click(function (event) {
        event.preventDefault();
        ViewModule.minimal.navigateToPage("donormanagemyprogramdonations", true);
    });

    ViewModule.buttondonordonatemoney.click(function (event) {
        event.preventDefault();
        ViewModule.minimal.navigateToPage("donordonatemoney", true);
    });

    ViewModule.buttonviewhappygiftrecipientphotos.click(function (event) {
        event.preventDefault();
        ViewModule.minimal.navigateToPage("donorviewgiftrecipientphotos", true);
    });

    ViewModule.buttondonorgotomyinbox.click(function (event) {
        event.preventDefault();
        ViewModule.minimal.navigateToPage("managemyinbox", true);
    });

    ViewModule.buttondonorgotomysettings.click(function (event) {
        event.preventDefault();
        ViewModule.minimal.navigateToPage("managemysettings", true);
    });

    ViewModule.buttondonormanagepersonalinformation.click(function (event) {
        event.preventDefault();
        ViewModule.minimal.navigateToPage("managepersonalinformation", true);
    });

    // client    

    ViewModule.buttonclientmanagemyprograms.click(function (event) {
        event.preventDefault();
        ViewModule.minimal.navigateToPage("clientmanagemyprograms", true);
    });

    ViewModule.buttonclientgotomyinbox.click(function (event) {
        event.preventDefault();
        ViewModule.minimal.navigateToPage("managemyinbox", true);
    });

    ViewModule.buttonclientgotomysettings.click(function (event) {
        event.preventDefault();
        ViewModule.minimal.navigateToPage("managemysettings", true);
    });

    ViewModule.buttonclientmanagepersonalinformation.click(function (event) {
        event.preventDefault();
        ViewModule.minimal.navigateToPage("managepersonalinformation", true);
    });


    ViewModule.LinkLogout.click(function(event) {
        ViewModule.minimal.navigateToPage('loggedout', true);
        application.LogOut();
    });

    ViewModule.LinkModifyMyAccountLogin.click(function() {
        minimal.navigateToPage('modifyldlogin', true);
    });

    ViewModule.LinkReportABug.click(function() {
        minimal.navigateToPage('reportabug', true);
    });

    ViewModule.LinkReportASiteProblem.click(function() {
        minimal.navigateToPage('reportsiteproblem', true);
    });

    ViewModule.LinkRequestASalesCall.click(function() {
        minimal.navigateToPage('requestasalescall', true);
    });

    ViewModule.LinkTermsOfService.click(function() {
        minimal.navigateToPage('termsofservice', true);
    });   

    var objectToSend = {};
    objectToSend.protocol = GetSystemUserAccountType;
    objectToSend.payload = {};
    objectToSend.payload.userid = application.loggedInUserID;

    postAjaj("api/Custom", objectToSend, function (data) {
        returnPayload = JSON.parse(data);
        var usertype = returnPayload.payload;

        var objectToS = allocObjectToSend();
        objectToS.payload.loggedinuserid = application.loggedInUserID;

        runCustom(RetrieveWelcomeMessageByLoggedInUserID, objectToS,
            function (returnPayld) {

                if (returnPayload.succeeded === true) {
                    if (usertype === 1) {
                        ViewModule.DivAdministrator.show();
                        ViewModule.DivDonor.hide();
                        ViewModule.DivClient.hide();
                        ViewModule.adminwelcome.text("Welcome " + returnPayld.payload);
                    }
                    else if (usertype === 2) {
                        ViewModule.DivAdministrator.hide();
                        ViewModule.DivDonor.show();
                        ViewModule.DivClient.hide();
                        ViewModule.donorwelcome.text("Welcome " + returnPayld.payload);
                    }
                    else {
                        ViewModule.DivAdministrator.hide();
                        ViewModule.DivDonor.hide();
                        ViewModule.DivClient.show();
                        ViewModule.clientwelcome.text("Welcome " + returnPayld.payload);
                    }
                }
                cb();
            });
    });
           
};

ViewModule.Deinitialize = function() {
    var self = this;
};