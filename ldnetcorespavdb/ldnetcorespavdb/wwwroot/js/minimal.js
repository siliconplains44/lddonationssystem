/**
 * Created by Allan on 1/26/2015.
 */

function minimal() {
};

minimal.currentViewModule = null;
minimal.bodyElement = null;
minimal.rootViewModulesPath = null;
minimal.attemptedLink = null;

minimal.registerViewModules = function (viewModules) {
    this.viewModules = viewModules;
};

minimal.registerRootViewModulesPath = function (rootViewModulesPath) {
    this.rootViewModulesPath = rootViewModulesPath;
};

minimal.navigateToPage = function (moduleName, addToHistory) {
    var self = this;

    var urlpieces = moduleName.split("?");

    for (var i = 0; i < this.viewModules.length; i++) {

        if (self.viewModules[i].id === urlpieces[0]) {

            // unload existing module first

            if (null !== self.currentViewModule) {
                self.currentViewModule.Deinitialize();
            }

            // remove all html content from body
            self.bodyElement.empty();

            if (self.viewModules[i].requiressession === true && application.loggedInUserID === null) {
                self.attemptedLink = moduleName;
                self.navigateToPage("login", true);
            }
            else {
                // load all html content to body for new view module
                self.bodyElement.load(self.rootViewModulesPath + '/' + self.viewModules[i].path + '.html', function () {

                    // initialize new view module via javascript
                    loadjscssfile(self.rootViewModulesPath + '/' + self.viewModules[i].path + '.js', 'js', function () {

                        self.currentViewModule = ViewModule;

                        self.currentViewModule.Initialize(self, urlpieces[1], function () {

                            var objectToSend = {};
                            objectToSend.payload = {};
                            objectToSend.protocol = CreateModuleView;
                            objectToSend.payload.loggedinsecurityuserid = application.loggedInUserID;
                            objectToSend.payload.name = moduleName;

                            // turn this back on after you are plugged back into a database
                            postAjaj("api/Custom", objectToSend, function (data) { });

                            if (true === addToHistory) {
                                history.pushState({ modulename: moduleName }, null, "#" + moduleName);
                            }

                            // to do:  add back in google analytics
                            /*var track = { page: moduleName, title: moduleName };
                            ga('set', track);
                            ga('send', 'pageview');*/

                        });                        
                    });
                });
            }

            break;
        }
    }

};

window.addEventListener("popstate", function(event) {

    if (event.state) {
        if (event.state.modulename) {
            minimal.navigateToPage(event.state.modulename, false);
        }
    }
});