/**
 * Created by Allan on 1/27/2015.
 */

function ViewModule() { };

ViewModule.Title = "Add/Edit Program";

ViewModule.minimal = null;

// page vars

// page controls

ViewModule.linkmanageprograms = $('#linkmanageprograms');

ViewModule.textname = $('#textname');
ViewModule.textdescription = $('#textdescription');
ViewModule.checkboxispublished = $('#checkboxispublished');
ViewModule.selectyear = $('#selectyear');
ViewModule.buttonsubmit = $('#buttonsubmit');

ViewModule.Initialize = function (minimal, url, cb) {
    var self = this;

    self.minimal = minimal;

    // load page control references

    // initialize initial control state

    ViewModule.buttonsubmit.text('Add');

    if (application.isadd == false) {

        ViewModule.buttonsubmit.text('Edit');

        var client = new AjajClient();             

        client.RetrieveByIDprograms(application.objecttoeditid, null, function (ret) {
            var protret = JSON.parse(ret);

            if (protret.succeeded == true) {

                ViewModule.textname.val(protret.payload.Name);
                ViewModule.textdescription.val(protret.payload.Description);

                if (protret.payload.IsPublished == 0) {
                    ViewModule.checkboxispublished.prop("checked", false); 
                    ViewModule.checkboxispublished.attr('disabled', false);
                }
                else {
                    ViewModule.checkboxispublished.prop("checked", true); 
                    ViewModule.checkboxispublished.attr('disabled', true);
                }

                ViewModule.selectyear.val(protret.payload.Year);
            }
        });
    }

    // load up events

    ViewModule.linkmanageprograms.click(function(event) {
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
    if (application.isadd === false) {

        var client = new AjajClient();

        var aprogram = new programs();

        aprogram.ProgramID = application.objecttoeditid;
        aprogram.Name = ViewModule.textname.val();
        aprogram.Description = ViewModule.textdescription.val();
        if (ViewModule.checkboxispublished.is(":checked") === true) {
            aprogram.IsPublished = 1;            
        }
        else {
            aprogram.IsPublished = 0;            
        }
        aprogram.Year = ViewModule.selectyear.val();

        client.Modifyprograms(aprogram, null, function (ret) {
            var protret = JSON.parse(ret);

            if (protret.succeeded === true) {

                // return to calling page
                minimal.navigateToPage("manageprograms", true);
            }
        });
    }
    else {
        var client = new AjajClient();

        var aprogram = new programs();
        
        aprogram.Name = ViewModule.textname.val();
        aprogram.Description = ViewModule.textdescription.val();
        if (ViewModule.checkboxispublished.is(":checked") === true) {
            aprogram.IsPublished = 1;
        }
        else {
            aprogram.IsPublished = 0;
        }
        aprogram.Year = ViewModule.selectyear.val();

        client.Createprograms(aprogram, null, function (ret) {
            var protret = JSON.parse(ret);

            if (protret.succeeded === true) {

                // return to calling page
                minimal.navigateToPage("manageprograms", true);
            }
        });
    }
});