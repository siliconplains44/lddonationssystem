function ViewModule() { }

ViewModule.Title = "Leopard Data Minimal Home";

ViewModule.minimal = null;

// page vars

// page controls

ViewModule.linkmanagedonors = $('#linkmanagedonors');

ViewModule.textlastname = $('#textlastname');
ViewModule.textmiddlename = $("#textmiddlename");
ViewModule.textfirstname = $("#textfirstname");
ViewModule.datebirthday = $("#datebirthday");
ViewModule.textmobilephonenumber = $("#textmobilephonenumber");
ViewModule.texthomephonenumber = $("#texthomephonenumber");
ViewModule.textaddresslineone = $("#textaddresslineone");
ViewModule.textaddresslinetwo = $("#textaddresslinetwo");
ViewModule.textcity = $("#textcity");
ViewModule.textstate = $("#textstate");
ViewModule.textzip = $("#textzip");

ViewModule.buttonsavepersonalinformation = $("#buttonsavepersonalinformation");

ViewModule.programselector = $("#programselector");
ViewModule.enlistedclientrequestsgrid = $('#enlistedclientrequestsgrid');
ViewModule.unenlistedclientrequestsgrid = $('#unenlistedclientrequestsgrid');

ViewModule.buttonsendmessagetodonor = $('#buttonsendmessagetodonor');

ViewModule.buttondeactivatedonorfromsystem = $('#buttondeactivatedonorfromsystem');

ViewModule.donorinformationsaveddialog = $('#donorinformationsaveddialog');

// dialogs

// send message to donor

ViewModule.sendmessagetodonordialog = $("#sendmessagetodonordialog");
ViewModule.textmessagesubject = $("#textmessagesubject");
ViewModule.textmessagebody = $("#textmessagebody");
ViewModule.buttonsendmessagetodonorfromdialog = $("#buttonsendmessagetodonorfromdialog");

ViewModule.loadEnlistedClientRequests = function() {

    objectToSend = allocObjectToSend();
    objectToSend.payload.donorid = parseInt(application.objecttomanageid);    

    runCustom(RetrieveAllCommittedClientRequestsByDonorId,
        objectToSend,
        function (returnPayload) {

            var tableHtml = '<table id="theenlistedclientrequestsgrid" class="display" width="100%">';
            tableHtml += '</table>';

            ViewModule.enlistedclientrequestsgrid.html(tableHtml);

            // build our table data

            var tableData = [];

            for (var i = 0; i < returnPayload.payload.length; i++) {
                var row = {
                    "clientrequestid": returnPayload.payload[i].ClientRequestID,
                    "clientrequest": returnPayload.payload[i].ClientRequest,
                    "programdonorcommittmentid": returnPayload.payload[i].ProgramDonorCommittmentID,
                    "programname": returnPayload.payload[i].ProgramName,
                    "programdescription": returnPayload.payload[i].ProgramDescription,
                    "programyear": returnPayload.payload[i].ProgramYear,
                    "clientlastname": returnPayload.payload[i].ClientLastName,
                    "clientmiddlename": returnPayload.payload[i].ClientMiddleName,
                    "clientfirstname": returnPayload.payload[i].ClientFirstName,
                    "clientbirthdate": returnPayload.payload[i].ClientBirthDate,
                    "clientcity": returnPayload.payload[i].ClientCity,
                    "clientzip": returnPayload.payload[i].ClientZip
                };

                tableData.push(row);
            }

            // load up our data table

            $('#theenlistedclientrequestsgrid').dataTable({
                responsive: true,
                "data": tableData,
                "bDestroy": true,
                "deferRender": true,
                rowId: 'programid',
                "columnDefs": [
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false
                    }
                ],
                "columns": [
                    {
                        title: "ClientRequestID",
                        data: "clientrequestid"
                    },
                    {
                        title: "ClientRequest",
                        data: "clientrequest"
                    },
                    {
                        title: "ProgramName",
                        data: "programname"
                    },
                    {
                        title: "ProgramDescription",
                        data: "programdescription"
                    },
                    {
                        title: "ProgramYear",
                        data: "programyear"
                    },
                    {
                        title: "ClientLastName",
                        data: "clientlastname"
                    },
                    {
                        title: "ClientMiddleName",
                        data: "clientmiddlename"
                    },
                    {
                        title: "ClientFirstName",
                        data: "clientfirstname"
                    },
                    {
                        title: "ClientBirthDate",
                        data: "clientbirthdate"
                    },
                    {
                        title: "ClientCity",
                        data: "clientcity"
                    },
                    {
                        title: "ClientZip",
                        data: "clientzip"
                    },
                    {
                        title: "Unenlist",
                        "render": function (data, type, row, meta) {
                            return '<a data-id="' +
                                row.programdonorcommittmentid +
                                '" onclick="unenlistdonorfromclientrequest(event, ' +
                                row.programdonorcommittmentid +
                                ')" href="#">unenlist</a>';
                        }
                    }
                ]

            });
        });

};

ViewModule.loadUnenlistedClientRequests = function() {

    objectToSend = allocObjectToSend();    

    runCustom(RetrieveAllNonCommittedClientRequests,
        objectToSend,
        function (returnPayload) {

            var tableHtml = '<table id="theunenlistedclientrequestsgrid" class="display" width="100%">';
            tableHtml += '</table>';

            ViewModule.unenlistedclientrequestsgrid.html(tableHtml);

            // build our table data

            var tableData = [];

            for (var i = 0; i < returnPayload.payload.length; i++) {
                var row = {
                    "clientrequestid": returnPayload.payload[i].ClientRequestID,
                    "programdonorcommittmentid": returnPayload.payload[i].ProgramDonorCommittmentID,
                    "clientrequest": returnPayload.payload[i].ClientRequest,
                    "programname": returnPayload.payload[i].ProgramName,
                    "programdescription": returnPayload.payload[i].ProgramDescription,
                    "programyear": returnPayload.payload[i].ProgramYear,
                    "clientlastname": returnPayload.payload[i].ClientLastName,
                    "clientmiddlename": returnPayload.payload[i].ClientMiddleName,
                    "clientfirstname": returnPayload.payload[i].ClientFirstName,
                    "clientbirthdate": returnPayload.payload[i].ClientBirthDate,
                    "clientcity": returnPayload.payload[i].ClientCity,
                    "clientzip": returnPayload.payload[i].ClientZip
                };

                tableData.push(row);
            }

            // load up our data table

            $('#theunenlistedclientrequestsgrid').dataTable({
                responsive: true,
                "data": tableData,
                "bDestroy": true,
                "deferRender": true,
                rowId: 'programid',
                "columnDefs": [
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false
                    }
                ],
                "columns": [
                    {
                        title: "ClientRequestID",
                        data: "clientrequestid"
                    },
                    {
                        title: "ClientRequest",
                        data: "clientrequest"
                    },
                    {
                        title: "ProgramName",
                        data: "programname"
                    },
                    {
                        title: "ProgramDescription",
                        data: "programdescription"
                    },
                    {
                        title: "ProgramYear",
                        data: "programyear"
                    },
                    {
                        title: "ClientLastName",
                        data: "clientlastname"
                    },
                    {
                        title: "ClientMiddleName",
                        data: "clientmiddlename"
                    },
                    {
                        title: "ClientFirstName",
                        data: "clientfirstname"
                    },
                    {
                        title: "ClientBirthDate",
                        data: "clientbirthdate"
                    },
                    {
                        title: "ClientCity",
                        data: "clientcity"
                    },
                    {
                        title: "ClientZip",
                        data: "clientzip"
                    },
                    {
                        title: "Enlist",
                        "render": function (data, type, row, meta) {
                            return '<a data-id="' +
                                row.clientrequestid +
                                '" onclick="enlistdonorinclientrequest(event, ' +
                                row.clientrequestid +
                                ')" href="#">enlist</a>';
                        }
                    }
                ]

            });
        });


};

ViewModule.Initialize = function (minimal, url, cb) {
    var self = this;

    self.minimal = minimal;

    // load page control references

    var objectToSend = {};
    objectToSend.protocol = RetrieveDonorInformationByDonorID;
    objectToSend.payload = {};

    objectToSend.payload.donorid = parseInt(application.objecttomanageid);

    postAjaj("api/Custom", objectToSend, function (data) {
        returnPayload = JSON.parse(data);

        if (returnPayload.succeeded === true) {
            
            ViewModule.textlastname.val(returnPayload.payload.LastName);
            ViewModule.textmiddlename.val(returnPayload.payload.MiddeName);
            ViewModule.textfirstname.val(returnPayload.payload.FirstName);
            ViewModule.textaddresslineone.val(returnPayload.payload.AddressLine1);
            ViewModule.textaddresslinetwo.val(returnPayload.payload.AddressLine2);
            ViewModule.textmobilephonenumber.val(returnPayload.payload.MobilePhoneNumber);
            ViewModule.texthomephonenumber.val(returnPayload.payload.HomePhoneNumber);
            var birthdaymoment = moment(returnPayload.payload.BirthDate);
            ViewModule.datebirthday.val(birthdaymoment.format("YYYY-MM-DD"));
            ViewModule.textcity.val(returnPayload.payload.City);
            ViewModule.textstate.val(returnPayload.payload.State);
            ViewModule.textzip.val(returnPayload.payload.Zip);
        }
    });

    // initialize initial control state

    // load up events

    ViewModule.linkmanagedonors.click(function (event) {
        event.preventDefault();

        self.minimal.navigateToPage("managedonors", true);
    });

    ViewModule.buttonsavepersonalinformation.click(function (event) {
        event.preventDefault();

        var objectToSend = {};
        objectToSend.protocol = SaveDonorInformation;
        objectToSend.payload = {};

        objectToSend.payload.DonorId = parseInt(application.objecttomanageid);        
        objectToSend.payload.LastName = ViewModule.textlastname.val();
        objectToSend.payload.MiddleName = ViewModule.textmiddlename.val();
        objectToSend.payload.FirstName = ViewModule.textfirstname.val();
        objectToSend.payload.AddressLine1 = ViewModule.textaddresslineone.val();
        objectToSend.payload.AddressLine2 = ViewModule.textaddresslinetwo.val();
        objectToSend.payload.MobilePhoneNumber = ViewModule.textmobilephonenumber.val();
        objectToSend.payload.HomePhoneNumber = ViewModule.texthomephonenumber.val();
        objectToSend.payload.Birthdate = ViewModule.datebirthday.val();
        objectToSend.payload.City = ViewModule.textcity.val();
        objectToSend.payload.State = ViewModule.textstate.val();
        objectToSend.payload.Zip = ViewModule.textzip.val();

        postAjaj("api/Custom", objectToSend, function (data) {
            returnPayload = JSON.parse(data);

            if (returnPayload.succeeded === true) {
                ViewModule.donorinformationsaveddialog.modal({
                    keyboard: false
                });

                setTimeout(function () {
                    ViewModule.donorinformationsaveddialog.hide();
                    ViewModule.donorinformationsaveddialog.removeData();
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                }, 3000);
            }
        });
    });

    // requests that this donor has enlisted in

    ViewModule.loadEnlistedClientRequests();

    // requests this donor could enlist in

    ViewModule.loadUnenlistedClientRequests();

    // send message to client

    ViewModule.buttonsendmessagetodonor.click(function (event) {
        event.preventDefault();

        ViewModule.sendmessagetodonordialog.modal({
            keyboard: false
        });

    });

    ViewModule.buttonsendmessagetodonorfromdialog.click(function (event) {
        event.preventDefault();

        var objectToSend = allocObjectToSend();
        objectToSend.payload.loggedInUserID = application.loggedInUserID;
        objectToSend.payload.Subject = ViewModule.textmessagesubject.val();
        objectToSend.payload.Body = ViewModule.textmessagebody.val();
        objectToSend.payload.messagerecipients = [];

        var messagerecipient = {};

        messagerecipient.AccountID = parseInt(application.objecttomanageid);
        messagerecipient.AccountTypeID = 2;

        objectToSend.payload.messagerecipients.push(messagerecipient);

        runCustom(SendInternalMessage,
            objectToSend,
            function (returnPayload) {
                if (returnPayload.succeeded === true) {
                    ViewModule.sendmessagetodonordialog.hide();
                    ViewModule.sendmessagetodonordialog.removeData();
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                }
            });

    });

    ViewModule.buttondeactivatedonorfromsystem.click(function(event) {
        event.preventDefault();

        var objectToSend = allocObjectToSend();
        objectToSend.payload.donorid = parseInt(application.objecttomanageid);
        
        runCustom(DeleteDonor,
            objectToSend,
            function (returnPayload) {
                if (returnPayload.succeeded === true) {
                    self.minimal.navigateToPage("managedonors", true);
                }
            });

    });

    cb();
};

ViewModule.Deinitialize = function () {
    var self = this;
};

var unenlistdonorfromclientrequest = function (event, programdonorcommittmentid) {

    event.preventDefault();

    var objectToSend = allocObjectToSend();
    objectToSend.payload.ProgramDonorCommitmentID = programdonorcommittmentid;    
    objectToSend.transactionid = null;

    runGen(eDeleteprogramdonorcommitments,
        objectToSend,
        function (returnPayload) {
            if (returnPayload.succeeded === true) {
                ViewModule.loadEnlistedClientRequests();
                ViewModule.loadUnenlistedClientRequests();
            }
        });

};

var enlistdonorinclientrequest = function(event, clientrequestid) {

    event.preventDefault();

    var FromMoment = moment();

    var objectToSend = allocObjectToSend();
    objectToSend.payload.DonorID = parseInt(application.objecttomanageid);
    objectToSend.payload.CommitmentDateTime = FromMoment.format("MM-DD-YYYY");
    objectToSend.payload.ClientRequestID = clientrequestid;
    objectToSend.payload.ReceivedAtCollectionPoint = 0;
    objectToSend.payload.DistributedToRecipient = 0;
    objectToSend.transactionid = null;

    runGen(eCreateprogramdonorcommitments,
        objectToSend,
        function (returnPayload) {
            if (returnPayload.succeeded === true) {
                ViewModule.loadEnlistedClientRequests();
                ViewModule.loadUnenlistedClientRequests();
            }
        });

};

