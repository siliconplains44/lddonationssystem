/**
 * Created by Allan on 1/27/2015.
 */

function ViewModule() { }

ViewModule.Title = "Add/Edit Program Event";

ViewModule.minimal = null;

// page vars

// page controls

ViewModule.linkmanageprograms = $('#linkmanageprograms');

ViewModule.textname = $('#textname');
ViewModule.textdescription = $('#textdescription');
ViewModule.datefrom = $('#datefrom');
ViewModule.dateto = $('#dateto');
ViewModule.buttonsubmit = $('#buttonsubmit');

ViewModule.Initialize = function (minimal, url, cb) {
    var self = this;

    self.minimal = minimal;

    // load page control references

    // initialize initial control state

    ViewModule.buttonsubmit.text('Add');

    if (application.isadd === false) {

        ViewModule.buttonsubmit.text('Edit');

        var client = new AjajClient();

        client.RetrieveByIDprogramevents(application.objecttoeditid, null, function (ret) {
            var protret = JSON.parse(ret);

            if (protret.succeeded === true) {

                ViewModule.textname.val(protret.payload.Name);
                ViewModule.textdescription.val(protret.payload.Description);

                var FromMoment = moment(protret.payload.FromDate);
                var ToMoment = moment(protret.payload.ToDate);

                ViewModule.datefrom.val(FromMoment.format("YYYY-MM-DD"));
                ViewModule.dateto.val(ToMoment.format("YYYY-MM-DD"));
            }
        });
    }

    // load up events

    ViewModule.linkmanageprograms.click(function (event) {
        event.preventDefault();
        ViewModule.minimal.navigateToPage('manageprograms', true);
    });

    cb();
};

ViewModule.Deinitialize = function () {
    var self = this;
};

ViewModule.buttonsubmit.click(function (event) {
    event.preventDefault();

    var client = new AjajClient();
    var aprogramevent = new programevents();

    if (application.isadd === false) {        

        aprogramevent.ProgramEventID = application.objecttoeditid;
        aprogramevent.ProgramID = application.selectedprogramid;
        aprogramevent.Name = ViewModule.textname.val();
        aprogramevent.Description = ViewModule.textdescription.val();
        aprogramevent.FromDate = ViewModule.datefrom.val();
        aprogramevent.ToDate = ViewModule.dateto.val();

        if (aprogramevent.From === aprogramevent.To)
            aprogramevent.IsSingleDate = 1;
        else
            aprogramevent.IsSingleDate = 0;

        client.Modifyprogramevents(aprogramevent, null, function (ret) {
            var protret = JSON.parse(ret);

            if (protret.succeeded === true) {

                // return to calling page
                minimal.navigateToPage("manageprograms", true);
            }
        });
    }
    else {                
        aprogramevent.ProgramID = application.selectedprogramid;
        aprogramevent.Name = ViewModule.textname.val();
        aprogramevent.Description = ViewModule.textdescription.val();
        aprogramevent.FromDate = ViewModule.datefrom.val();
        aprogramevent.ToDate = ViewModule.dateto.val();

        if (aprogramevent.From === aprogramevent.To)
            aprogramevent.IsSingleDate = 1;
        else
            aprogramevent.IsSingleDate = 0;

        client.Createprogramevents(aprogramevent, null, function (ret) {
            var protret = JSON.parse(ret);

            if (protret.succeeded === true) {

                // return to calling page
                minimal.navigateToPage("manageprograms", true);
            }
        });
    }
});