function ViewModule() { };

ViewModule.Title = "Leopard Data Minimal Home";

ViewModule.minimal = null;

// page vars

// page controls

ViewModule.paragraph = $('#data');

ViewModule.Initialize = function (minimal, url, cb) {
    var self = this;

    self.minimal = minimal;

    // load page control references

    // initialize initial control state

    // load up events

    cb();
};

ViewModule.Deinitialize = function () {
    var self = this;
};
