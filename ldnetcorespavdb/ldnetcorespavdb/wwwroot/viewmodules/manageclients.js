function ViewModule() { };

ViewModule.Title = "Manage Administrators";

ViewModule.minimal = null;

// page vars

// page controls

ViewModule.clientstable = $('#clientstable');
ViewModule.buttonaddclient = $('#buttonaddclient');
ViewModule.buttonaddtheclient = $("#buttonaddtheclient");
ViewModule.deleteconfirmationdialog = $('#deleteconfirmationdialog');
ViewModule.addclientdialog = $('#addclientdialog');
ViewModule.linkdashboard = $('#linkdashboard');
ViewModule.textemailaddress = $('#textemailaddress');
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
ViewModule.buttondeleteclient = $('#buttondeleteclient');
ViewModule.dialogtitle = $("#dialogtitle");

ViewModule.LoadClients = function () {

    var objectToSend = {};
    objectToSend.protocol = RetrieveAllClients;

    postAjaj("api/Custom", objectToSend, function (data) {
        returnPayload = JSON.parse(data);

        if (returnPayload.succeeded === true) {

            var tableHtml = '<table id="theclientstable" class="display" width="100%">';
            tableHtml += '</table>';

            ViewModule.clientstable.html(tableHtml);

            // build our table data

            var tableData = [];          

            for (var i = 0; i < returnPayload.payload.length; i++) {
                var row = {
                    "clientid": returnPayload.payload[i].ClientID.toString(),
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

            $('#theclientstable').dataTable({
                responsive: true,
                "data": tableData,
                "bDestroy": true,
                "deferRender": true,
                rowId: 'clientid',
                "columnDefs": [
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false,                        
                    }                    
                ],
                "columns": [
                    {
                        title: "ClientID",
                        data: "clientid"
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
                    }/*,
                  { title: "Edit", 
                      "render": function (data, type, row, meta){
                          return '<a data-id="' + row.adminid + '" class="administratoredit" href="#">edit</a>';
                        } 
                       }*/,
                    {
                        title: "Manage",
                        "render": function (data, type, row, meta) {
                            return '<a data-id="' + row.clientid + '" onclick="clientmanageclick(event, ' + row.clientid + ')" href="#">manage</a>';
                        }
                    }
                ]
            });
          
            
        }
    });
};

clientmanageclick = function (event, clientid) {
    event.preventDefault();
    application.objecttomanageid = clientid;

    minimal.navigateToPage('manageclient', true);
};

ViewModule.Initialize = function (minimal, url, cb) {
    var self = this;

    self.minimal = minimal;

    // load page control references

    // initialize initial control state

    ViewModule.LoadClients();

    // load up events

    ViewModule.buttonaddclient.click(function (event) {
        event.preventDefault();
        
        ViewModule.birthdate.val('0001-01-01');

        ViewModule.addclientdialog.modal({
            keyboard: false
        });
    });

    ViewModule.buttonaddtheclient.click(function (event) {

        var formValidated = true;

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

        if (formValidated === false)
            return;
        else {

            var objectToSend = {};
            objectToSend.protocol = AddClient;
            objectToSend.payload = new displayclient();
           
            objectToSend.payload.EmailAddress = ViewModule.textemailaddress.val();
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

            objectToSend.payload.FatherIndividualID = -1;
            objectToSend.payload.MotherIndividualID = -1;

            postAjaj("api/Custom", objectToSend, function (data) {
                returnPayload = JSON.parse(data);

                if (returnPayload.succeeded === true) {
                    ViewModule.LoadClients();
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
