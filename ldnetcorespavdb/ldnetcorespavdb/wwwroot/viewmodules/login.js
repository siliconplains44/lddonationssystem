/**
 * Created by Allan on 1/27/2015.
 */

function ViewModule() { };

ViewModule.Title = "Leopard Data Services Store Login";
ViewModule.minimal = null;

// page vars

// page controls

ViewModule.SignInButton = $('#SignInButton');
ViewModule.CreateAccountButton = $('#CreateAccountButton');
ViewModule.ResetPasswordButton = $('#ResetPasswordButton');
ViewModule.LinkHome = $('#LinkHome');
ViewModule.TextUsername = $('#TextUsername');
ViewModule.TextPassword = $('#TextPassword');
ViewModule.LoginModal = $('#LoginModal');
ViewModule.ParagraphLoginStatus = $('#ParagraphLoginStatus');

ViewModule.Initialize = function(minimal, url, cb) {
    var self = this;

    self.minimal = minimal;

    // load page control references

    // initialize initial control state

    ViewModule.TextUsername.focus();

    // load up events

    self.SignInButton.click(function(e) {

        e.preventDefault();        
       
        var objectToSend = {};
        objectToSend.protocol = LoginSystemUser;
        objectToSend.payload = {};
        objectToSend.payload.email = ViewModule.TextUsername.val();
        objectToSend.payload.password = ViewModule.TextPassword.val();       

        postAjaj("api/Custom", objectToSend, function (data) {
            returnPayload = JSON.parse(data);

            if (returnPayload.succeeded === true) {

                application.loggedInUserID = returnPayload.payload.SystemUserID;
                application.AccountTypeID = returnPayload.payload.AccountTypeID;

                ViewModule.ParagraphLoginStatus.text('You have successfully logged in, we are now redirecting you to your home page!');

                ViewModule.LoginModal.modal({
                    keyboard: false
                });

                setTimeout(function () {
                    ViewModule.LoginModal.hide();
                    ViewModule.LoginModal.removeData();
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();                    

                    application.login();

                    if (minimal.attemptedLink !== null) {
                        self.minimal.navigateToPage(minimal.attemptedLink, true);
                    }
                    else {
                        self.minimal.navigateToPage('dashboard', true);
                    }
                }, 3000);
            }
            else {
                ViewModule.ParagraphLoginStatus.text('Login failed... please call Leopard Data at 806-305-0223 for support!');

                ViewModule.LoginModal.modal({
                    keyboard: false
                });

                setTimeout(function () {
                    ViewModule.LoginModal.hide();
                    ViewModule.LoginModal.removeData();
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                }, 5000);
            }
        });        
    });

    self.CreateAccountButton.click(function(event) {
        self.minimal.navigateToPage('register', true);
    });

    self.ResetPasswordButton.click(function (event) {
        self.minimal.navigateToPage('resetpassword', true);
    });

    self.LinkHome.click(function() {
        minimal.navigateToPage('home', true);
    });


    // TODO ->  switch this out before ship to prod, just comment out

    //ViewModule.TextUsername.val('inquiries@leoparddata.local');
    //ViewModule.TextPassword.val('aaaaaaaa');

    cb();

};

ViewModule.Deinitialize = function() {
    var self = this;
};
