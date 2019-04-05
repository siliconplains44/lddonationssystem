function ViewModule() { };

ViewModule.Title = "Manage Administrators";

ViewModule.minimal = null;

// page vars

// page controls

ViewModule.donorstable = $('#donorstable');
ViewModule.buttonadddonor = $('#buttonadddonor');
ViewModule.buttonaddthedonor = $("#buttonaddthedonor");
ViewModule.adddonordialog = $('#adddonordialog');
ViewModule.linkdashboard = $('#linkdashboard');
ViewModule.textemailaddress = $('#textemailaddress');
ViewModule.passwordpassword = $("#passwordpassword");
ViewModule.textlastname = $('#textlastname');
ViewModule.textmiddlename = $('#textmiddlename');
ViewModule.textfirstname = $('#textfirstname');
ViewModule.birthdate = $('#datebirthday');
ViewModule.textmobilephonenumber = $('#textmobilephonenumber');
ViewModule.texthomephonenumber = $('#texthomephonenumber');
ViewModule.textaddressline1 = $('#textaddresslineone');
ViewModule.textaddressline2 = $('#textaddresslinetwo');
ViewModule.textcity = $('#textcity');
ViewModule.textstate = $('#textstate');
ViewModule.textzip = $('#textzip');
ViewModule.buttondeleteclient = $('#buttondeletedonor');
ViewModule.dialogtitle = $("#dialogtitle");

ViewModule.LoadDonors = function () {

    var objectToSend = {};
    objectToSend.protocol = RetrieveAllDonors;

    postAjaj("api/Custom", objectToSend, function (data) {
        returnPayload = JSON.parse(data);

        if (returnPayload.succeeded === true) {

            var tableHtml = '<table id="thedonorstable" class="display" width="100%">';
            tableHtml += '</table>';

            ViewModule.donorstable.html(tableHtml);

            // build our table data

            var tableData = [];

            for (var i = 0; i < returnPayload.payload.length; i++) {
                var row = {
                    "donorid": returnPayload.payload[i].DonorID.toString(),
                    "emailaddress": returnPayload.payload[i].EmailAddress,
                    "lastname": returnPayload.payload[i].LastName,
                    "middlename": returnPayload.payload[i].MiddleName,
                    "firstname": returnPayload.payload[i].FirstName,
                    "birthdate": returnPayload.payload[i].BirthDate,
                    "mobilephonenumber": returnPayload.payload[i].MobilePhoneNumber,
                    "homephonenumber": returnPayload.payload[i].HomePhoneNumber,
                    "addressline1": returnPayload.payload[i].AddressLine1,
                    "addressline2": returnPayload.payload[i].AddressLine2,
                    "city": returnPayload.payload[i].City,
                    "state": returnPayload.payload[i].State,
                    "zip": returnPayload.payload[i].Zip
                };

                tableData.push(row);
            }

            // fix dates

            for (var i = 0; i < tableData.length; i++) {
                var humanreadable = moment(tableData[i].birthdate);
                tableData[i].birthdate = humanreadable.format("MM-DD-YYYY");
            }

            // load up our data table

            $('#thedonorstable').dataTable({
                responsive: true,
                "data": tableData,
                "bDestroy": true,
                "deferRender": true,
                rowId: 'donorid',
                "columnDefs": [
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false
                    }
                ],
                "columns": [
                    {
                        title: "DonorID",
                        data: "donorid"
                    },
                    {
                        title: "Email Address",
                        data: "emailaddress"
                    },
                    {
                        title: "Last Name",
                        data: "lastname"
                    },
                    {
                        title: "Middle Name",
                        data: "middlename"
                    },
                    {
                        title: "First Name",
                        data: "firstname"
                    },
                    {
                        title: "Birthday",
                        data: "birthdate"
                    },
                    {
                        title: "Mobile Phone Number",
                        data: "mobilephonenumber"
                    },
                    {
                        title: "Home Phone Number",
                        data: "homephonenumber"
                    },
                    {
                        title: "Address Line 1",
                        data: "addressline1"
                    },
                    {
                        title: "Address Line 2",
                        data: "addressline2"
                    },
                    {
                        title: "City",
                        data: "city"
                    },
                    {
                        title: "State",
                        data: "state"
                    },
                    {
                        title: "Zip",
                        data: "zip"
                    } /*,
                  { title: "Edit", 
                      "render": function (data, type, row, meta){
                          return '<a data-id="' + row.adminid + '" class="administratoredit" href="#">edit</a>';
                        } 
                       }*/,
                    {
                        title: "Manage",
                        "render": function (data, type, row, meta) {
                            return '<a data-id="' + row.donorid + '" onclick="donormanageclick(event, ' + row.donorid + ')" href="#">manage</a>';
                        }
                    }
                ]
            });                            
        }
    });
};

donormanageclick = function (event, donorid) {
    event.preventDefault();
    application.objecttomanageid = donorid;

    minimal.navigateToPage('managedonor', true);
};

ViewModule.Initialize = function (minimal, url, cb) {
    var self = this;

    self.minimal = minimal;

    // load page control references

    // initialize initial control state

    ViewModule.LoadDonors();

    // load up events

    ViewModule.buttonadddonor.click(function (event) {
        event.preventDefault();

        ViewModule.birthdate.val('0001-01-01');

        ViewModule.adddonordialog.modal({
            keyboard: false
        });
    });

    ViewModule.buttonaddthedonor.click(function (event) {

        var formValidated = true;

        if (ViewModule.textemailaddress.val().length === 0) {
            ViewModule.textemailaddress.addClass('is-invalid');
            formValidated = false;
        } else {
            ViewModule.textemailaddress.removeClass('is-invalid');
        }

        if (ViewModule.passwordpassword.val().length === 0) {
            ViewModule.passwordpassword.addClass('is-invalid');
            formValidated = false;
        } else {
            ViewModule.passwordpassword.removeClass('is-invalid');
        }

        if (ViewModule.textlastname.val().length === 0) {
            ViewModule.textlastname.addClass('is-invalid');
            formValidated = false;
        } else {
            ViewModule.textlastname.removeClass('is-invalid');
        }

        if (ViewModule.textfirstname.val().length === 0) {
            ViewModule.textfirstname.addClass('is-invalid');
            formValidated = false;
        } else {
            ViewModule.textfirstname.removeClass('is-invalid');
        }   

        if (ViewModule.birthdate.val() === null) {
            ViewModule.birthdate.addClass('is-invalid');
            formValidated = false;
        } else {
            ViewModule.birthdate.removeClass('is-invalid');
        }   

        if (formValidated === false)
            return;
        else {

            var objectToSend = {};
            objectToSend.protocol = AddDonor;
            objectToSend.payload = new displayclient();

            objectToSend.payload.EmailAddress = ViewModule.textemailaddress.val();
            objectToSend.payload.Password = ViewModule.passwordpassword.val();
            objectToSend.payload.LastName = ViewModule.textlastname.val();
            objectToSend.payload.MiddleName = ViewModule.textmiddlename.val();
            objectToSend.payload.FirstName = ViewModule.textfirstname.val();
            objectToSend.payload.BirthDate = ViewModule.birthdate.val();
            objectToSend.payload.MobilePhoneNumber = ViewModule.textmobilephonenumber.val();
            objectToSend.payload.HomePhoneNumber = ViewModule.texthomephonenumber.val();
            objectToSend.payload.AddressLine1 = ViewModule.textaddressline1.val();
            objectToSend.payload.AddressLine2 = ViewModule.textaddressline2.val();
            objectToSend.payload.City = ViewModule.textcity.val();
            objectToSend.payload.State = ViewModule.textstate.val();
            objectToSend.payload.Zip = ViewModule.textzip.val();            

            postAjaj("api/Custom", objectToSend, function (data) {
                returnPayload = JSON.parse(data);

                if (returnPayload.succeeded === true) {
                    ViewModule.LoadDonors();
                }
            });

        }
    });

    ViewModule.linkdashboard.click(function (event) {
        event.preventDefault();
        minimal.navigateToPage("dashboard", true);
    });

    cb();
};

ViewModule.Deinitialize = function () {
    var self = this;
};

