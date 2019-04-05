/**
 * Created by Allan on 1/27/2015.
 */

function ViewModule() { }

ViewModule.Title = "Leopard Data Minimal Home";

ViewModule.minimal = null;

// page vars

// page controls


ViewModule.linkdashboard = $('#linkdashboard');

ViewModule.Initialize = function (minimal, url, cb) {
    var self = this;

    self.minimal = minimal;

    // load page control references

    // initialize initial control state

    // load up events

    ViewModule.linkdashboard.click(function (event) {
        event.preventDefault();

        minimal.navigateToPage("dashboard", true);
    });

    cb();
};

ViewModule.Deinitialize = function () {
    var self = this;
};