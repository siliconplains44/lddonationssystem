function ViewModule() { }

ViewModule.Title = "Leopard Data Minimal Home";

ViewModule.minimal = null;

// page vars

// page controls

ViewModule.linkmanageclients = $('#linkmanageclients');

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

ViewModule.pfathername = $("#pfathername");
ViewModule.buttonsetfather = $("#buttonsetfather");

ViewModule.pmothername = $("#pmothername");
ViewModule.buttonsetmother = $("#buttonsetmother");

ViewModule.pisassociatedtouseraccount = $("#pisassociatedtouseraccount");
ViewModule.pidnotassociatedtouseraccount = $("#pidnotassociatedtouseraccount");
ViewModule.buttonsetupuseraccountforclient = $("#buttonsetupuseraccountforclient");
ViewModule.buttondeactivateuseraccountforclient = $("#buttondeactivateuseraccountforclient");

ViewModule.buttonenlistclientinprogram = $("#buttonenlistclientinprogram");
ViewModule.enlistedprogramsgrid = $("#enlistedprogramsgrid");

ViewModule.programselector = $("#programselector");
ViewModule.buttonenlistclientinprogramevent = $("#buttonenlistclientinprogramevent");
ViewModule.enlistedprogrameventsgrid = $("#enlistedprogrameventsgrid");

ViewModule.programselectorrequests = $('#programselectorrequests');
ViewModule.programclientrequestsgrid = $('#programclientrequestsgrid');
ViewModule.buttonaddclientrequest = $("#buttonaddclientrequest");

ViewModule.buttonsendmessagetoclient = $("#buttonsendmessagetoclient");

ViewModule.programselectorhappy = $('#programselectorhappy');
ViewModule.programclienthappyphotosgrid = $('#programclienthappyphotosgrid');
ViewModule.buttonuploadhappyclientphoto = $("#buttonuploadhappyclientphoto");

ViewModule.buttondeactivateclientfromsystem = $("#buttondeactivateclientfromsystem");

// dialogs

// client information saved dialog

ViewModule.clientinformationsaveddialog = $('#clientinformationsaveddialog');

// choose mother/father dialog

ViewModule.choosemotherfatherdialog = $("#choosemotherfatherdialog");
ViewModule.choosemotherfatherdialogtitle = $("#choosemotherfatherdialogtitle");
ViewModule.clientselectiongrid = $("#clientselectiongrid");

// setup user account dialog

ViewModule.setupclientuseraccountdialog = $("#setupclientuseraccountdialog");
ViewModule.textemailaddress = $("#textemailaddress");
ViewModule.passwordpassword = $("#passwordpassword");
ViewModule.passwordconfirmpassword = $("#passwordconfirmpassword");
ViewModule.buttonsaveclientuseraccount = $("#buttonsaveclientuseraccount");
ViewModule.passwordhelp = $("#passwordhelp");

// user account association deleted dialog

ViewModule.deactivateclientuseraccountdialog = $("#deactivateclientuseraccountdialog");

// choose program to enlist in dialog

ViewModule.chooseprogramdialog = $("#chooseprogramdialog");
ViewModule.programsnotenlistedingrid = $("#programsnotenlistedingrid");

// choose program event to enlist in dialog

ViewModule.chooseprogrameventdialog = $("#chooseprogrameventdialog");
ViewModule.programeventsnotenlistedin = $("#programeventsnotenlistedin");

// add client request dialog

ViewModule.addclientrequestdialog = $("#addclientrequestdialog");
ViewModule.textrequestinformation = $("#textrequestinformation");
ViewModule.buttonaddclientrequestfromdialog = $("#buttonaddclientrequestfromdialog");

// send message dialog

ViewModule.sendmessagetoclientdialog = $("#sendmessagetoclientdialog");
ViewModule.textmessagesubject = $("#textmessagesubject");
ViewModule.textmessagebody = $("#textmessagebody");
ViewModule.buttonsendmessagetoclientfromdialog = $("#buttonsendmessagetoclientfromdialog");

// upload happy client photo dialog

ViewModule.uploadhappyclientphotodialog = $("#uploadhappyclientphotodialog");
ViewModule.filehappyclient = $("#filehappyclient");
ViewModule.buttonuploadhappyclientphotofromdialog = $("#buttonuploadhappyclientphotofromdialog");

// confirm deactive client from system dialog

ViewModule.confirmclientdeactivatedialog = $("#confirmclientdeactivatedialog");
ViewModule.buttonconfirmclientdeactivation = $("#buttonconfirmclientdeactivation");

// client deactivated confirmation dialog

ViewModule.deactivateclientdialog = $("#deactivateclientdialog");

ViewModule.loadClientProgramEnlistments = function() {

    objectToSend = allocObjectToSend();
    objectToSend.payload.clientid = parseInt(application.objecttomanageid);

    runCustom(RetrieveListOfProgramsClientIsEnlistedIn,
        objectToSend,
        function (returnPayload) {

            var tableHtml = '<table id="theprogramenlistmentsgrid" class="display" width="100%">';
            tableHtml += '</table>';

            ViewModule.enlistedprogramsgrid.html(tableHtml);

            // build our table data

            var tableData = [];

            for (var i = 0; i < returnPayload.payload.length; i++) {
                var row = {
                    "programid": returnPayload.payload[i].ProgramID.toString(),
                    "name": returnPayload.payload[i].Name,
                    "description": returnPayload.payload[i].Description,
                    "year": returnPayload.payload[i].Year,                   
                };

                tableData.push(row);
            }

            // load up our data table

            $('#theprogramenlistmentsgrid').dataTable({
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
                        title: "ProgramID",
                        data: "programid"
                    },
                    {
                        title: "Name",
                        data: "name"
                    },
                    {
                        title: "Description",
                        data: "description"
                    },
                    {
                        title: "Year",
                        data: "year"
                    },
                    {
                        title: "Unenlist",
                        "render": function (data, type, row, meta) {
                            return '<a data-id="' + row.programid + '" onclick="unenlistclientfromprogram(event, ' + row.programid + ')" href="#">unenlist</a>';
                        }
                    }
                ]
               
            });

            var fieldspec = [
                { name: 'programid', text: 'Id' },
                { name: 'name', text: 'Name' },
                { name: 'description', text: 'Description' },
                { name: 'year', text: 'Year' }
                ];

            ViewModule.programselector.inputpicker({
                data: tableData,
                fields: fieldspec,
                autoOpen: true,
                headShow: true,
                fieldText: 'name',
                fieldValue: 'programid',
                responsive: true
            });

            ViewModule.programselector.change(function (input) {
                ViewModule.buttonenlistclientinprogramevent.show();
                application.selectedprogramid = parseInt(ViewModule.programselector.val());
                ViewModule.loadClientProgramEventEnlistments();
            });

            ViewModule.programselectorrequests.inputpicker({
                data: tableData,
                fields: fieldspec,
                autoOpen: true,
                headShow: true,
                fieldText: 'name',
                fieldValue: 'programid',
                responsive: true
            });

            ViewModule.programselectorrequests.change(function (input) {
                ViewModule.buttonaddclientrequest.show();
                application.selectedprogramrequestid = parseInt(ViewModule.programselectorrequests.val());
                ViewModule.loadClientProgramRequests();
            });

            ViewModule.programselectorhappy.inputpicker({
                data: tableData,
                fields: fieldspec,
                autoOpen: true,
                headShow: true,
                fieldText: 'name',
                fieldValue: 'programid',
                responsive: true
            });

            ViewModule.programselectorhappy.change(function (input) {
                ViewModule.buttonuploadhappyclientphoto.show();
                application.selectedprogramhappyid = parseInt(ViewModule.programselectorhappy.val());
                ViewModule.loadClientHappyPhotos();
            });

        });

};

ViewModule.loadClientProgramEventEnlistments = function() {

    objectToSend = allocObjectToSend();
    objectToSend.payload.programid = parseInt(application.selectedprogramid);
    objectToSend.payload.clientid = parseInt(application.objecttomanageid);

    runCustom(RetrieveListOfProgramEventsClientIsEnlistedIn,
        objectToSend,
        function(returnPayload) {

            var tableHtml = '<table id="theprogrameventenlistmentsgrid" class="display" width="100%">';
            tableHtml += '</table>';

            ViewModule.enlistedprogrameventsgrid.html(tableHtml);

            // build our table data

            var tableData = [];

            for (var i = 0; i < returnPayload.payload.length; i++) {
                var row = {
                    "programeventid": returnPayload.payload[i].ProgramID.toString(),
                    "name": returnPayload.payload[i].Name,
                    "description": returnPayload.payload[i].Description,
                    "fromdate": returnPayload.payload[i].FromDate,
                    "todate": returnPayload.payload[i].ToDate
                };

                tableData.push(row);
            }

            // load up our data table

            $('#theprogrameventenlistmentsgrid').dataTable({
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
                        title: "ProgramEventID",
                        data: "programeventid"
                    },
                    {
                        title: "Name",
                        data: "name"
                    },
                    {
                        title: "Description",
                        data: "description"
                    },
                    {
                        title: "FromDate",
                        data: "fromdate"
                    },
                    {
                        title: "ToDate",
                        data: "todate"
                    },
                    {
                        title: "Unenlist",
                        "render": function(data, type, row, meta) {
                            return '<a data-id="' +
                                row.programeventid +
                                '" onclick="unenlistclientfromprogramevent(event, ' +
                                row.programeventid +
                                ')" href="#">unenlist</a>';
                        }
                    }
                ]

            });
        });

};

ViewModule.loadClientProgramRequests = function() {

    objectToSend = allocObjectToSend();
    objectToSend.payload.programid = parseInt(application.selectedprogramrequestid);
    objectToSend.payload.clientid = parseInt(application.objecttomanageid);

    runCustom(RetrieveClientRequestsByProgramId,
        objectToSend,
        function (returnPayload) {

            var tableHtml = '<table id="theprogramrequestsgrid" class="display" width="100%">';
            tableHtml += '</table>';

            ViewModule.programclientrequestsgrid.html(tableHtml);

            // build our table data

            var tableData = [];

            for (var i = 0; i < returnPayload.payload.length; i++) {
                var row = {
                    "clientrequestid": returnPayload.payload[i].ClientRequestID.toString(),
                    "requestinformation": returnPayload.payload[i].RequestInformation                    
                };

                tableData.push(row);
            }

            // load up our data table

            $('#theprogramrequestsgrid').dataTable({
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
                        title: "Request Information",
                        data: "requestinformation"
                    },
                    {
                        title: "Delete",
                        "render": function (data, type, row, meta) {
                            return '<a data-id="' +
                                row.clientrequestid +
                                '" onclick="deleteclientrequest(event, ' +
                                row.clientrequestid +
                                ')" href="#">delete</a>';
                        }
                    }
                ]

            });
        });

};

ViewModule.loadClientHappyPhotos = function() {

    objectToSend = allocObjectToSend();
    objectToSend.payload.programid = parseInt(application.selectedprogramrequestid);
    objectToSend.payload.clientid = parseInt(application.objecttomanageid);

    runCustom(RetrieveHappyClientPhotos,
        objectToSend,
        function (returnPayload) {

            var tableHtml = '<table id="theprogramhappyphotosgrid" class="display" width="100%">';
            tableHtml += '</table>';

            ViewModule.programclienthappyphotosgrid.html(tableHtml);

            // build our table data

            var tableData = [];

            for (var i = 0; i < returnPayload.payload.length; i++) {
                var row = {
                    "happyclientpictureid": returnPayload.payload[i].aHappyClientPicture.HappyClientPictureID.toString(),                    
                    "programid": returnPayload.payload[i].aHappyClientPicture.ProgramID,
                    "recipientclientid": returnPayload.payload[i].aHappyClientPicture.RecipientClientID,
                    "fileuploadid": returnPayload.payload[i].aHappyClientPicture.FileUploadID,
                    "filename": returnPayload.payload[i].aFileUpload.Filename,
                    "size": returnPayload.payload[i].aFileUpload.Size,
                    "created": returnPayload.payload[i].aFileUpload.Created,
                    "data": returnPayload.payload[i].base64Photo
                };

                tableData.push(row);
            }

            // load up our data table

            $('#theprogramhappyphotosgrid').dataTable({
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
                        title: "HappyClientPictureID",
                        data: "happyclientpictureid"
                    },
                    {
                        title: "Photo",
                        "render": function(data, type, row, meta) {
                            return "<img src='data:image/jpeg;base64," + row.data +  "'></img>";
                        }
                    },
                    {
                        title: "Delete",
                        "render": function (data, type, row, meta) {
                            return '<a data-id="' +
                                row.happyclientpictureid +
                                '" onclick="deletehappyclientphoto(event, ' +
                                row.happyclientpictureid +
                                ')" href="#">delete</a>';
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

    ViewModule.buttonenlistclientinprogramevent.hide();
    ViewModule.buttonaddclientrequest.hide();
    ViewModule.buttonuploadhappyclientphoto.hide();

    /////////////////////////////////////////////////////////////////////////
    // initialize initial control state

    // personal information

    var objectToSend = {};
    objectToSend.protocol = RetrieveClientInformationByClientID;
    objectToSend.payload = {};

    objectToSend.payload.clientid = parseInt(application.objecttomanageid);

    postAjaj("api/Custom",
        objectToSend,
        function(data) {
            returnPayload = JSON.parse(data);

            if (returnPayload.succeeded === true) {

                ViewModule.textemailaddress.val(returnPayload.payload.EmailAddress);
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

    // father

    objectToSend = allocObjectToSend();
    objectToSend.payload.clientid = parseInt(application.objecttomanageid);

    runCustom(RetrieveClientFatherByClientID,
        objectToSend,
        function(returnPayload) {
            if (returnPayload.succeeded === true) {
                if (returnPayload.payload === null) {
                    ViewModule.pfathername.text("not assigned");
                } else {
                    ViewModule.pfathername.text(returnPayload.payload.FirstName + " " + returnPayload.payload.LastName);
                }
            }
        });

    // mother

    objectToSend = allocObjectToSend();
    objectToSend.payload.clientid = parseInt(application.objecttomanageid);

    runCustom(RetrieveClientMotherByClientId,
        objectToSend,
        function (returnPayload) {
            if (returnPayload.succeeded === true) {
                if (returnPayload.payload === null) {
                    ViewModule.pmothername.text("not assigned");
                } else {
                    ViewModule.pmothername.text(returnPayload.payload.FirstName + " " + returnPayload.payload.LastName);
                }
            }
        });

    // system user account

    objectToSend = allocObjectToSend();
    objectToSend.payload.clientid = parseInt(application.objecttomanageid);

    runCustom(DoesClientHaveSystemUserAccount,
        objectToSend,
        function(returnPayload) {
            if (returnPayload === true) {
                ViewModule.pisassociatedtouseraccount.hide();
                ViewModule.pidnotassociatedtouseraccount.show();
                ViewModule.buttonsetupuseraccountforclient.show();
                ViewModule.buttondeactivateuseraccountforclient.hide();
            } else {
                ViewModule.pisassociatedtouseraccount.show();
                ViewModule.pidnotassociatedtouseraccount.hide();
                ViewModule.buttonsetupuseraccountforclient.hide();
                ViewModule.buttondeactivateuseraccountforclient.show();
            }
        });

    // enlisted client programs

    ViewModule.loadClientProgramEnlistments();

    /////////////////////////////////////////////////////////////////////////
    // load up events

    // navigate back to client management

    ViewModule.linkmanageclients.click(function(event) {
        event.preventDefault();

        self.minimal.navigateToPage("manageclients", true);
    });

    // save personal information

    ViewModule.buttonsavepersonalinformation.click(function(event) {
        event.preventDefault();

        var objectToSend = {};
        objectToSend.protocol = SaveClientInformation;
        objectToSend.payload = {};

        objectToSend.payload.ClientId = parseInt(application.objecttomanageid);
        objectToSend.payload.EmailAddress = ViewModule.textemailaddress.val();
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
                ViewModule.clientinformationsaveddialog.modal({
                    keyboard: false
                });

                setTimeout(function () {
                    ViewModule.clientinformationsaveddialog.hide();
                    ViewModule.clientinformationsaveddialog.removeData();
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                }, 3000);
            }
        });
    });

    // set father/mother common functionlity

    ViewModule.setmotherfatherbuttonclicked = function() {

        var objectToSend = allocObjectToSend();
        objectToSend.payload.clientexclusionid = parseInt(application.objecttomanageid);

        runCustom(RetrieveClientsNotInExclusionList, objectToSend, function (returnPayload) {
            if (returnPayload.succeeded === true) {

                var tableHtml = '<table id="theclientselectiongrid" class="display" width="100%">';
                tableHtml += '</table>';

                ViewModule.clientselectiongrid.html(tableHtml);

                // build our table data

                var tableData = [];

                for (var i = 0; i < returnPayload.payload.length; i++) {
                    var row = {
                        "clientid": returnPayload.payload[i].ClientID.toString(),
                        "emailaddress": returnPayload.payload[i].EmailAddress,
                        "lastname": returnPayload.payload[i].LastName,
                        "middlename": returnPayload.payload[i].MiddleName,
                        "firstname": returnPayload.payload[i].FirstName,
                        "addressline1": returnPayload.payload[i].AddressLine1,
                        "addressline2": returnPayload.payload[i].AddressLine2,
                        "city": returnPayload.payload[i].City,
                        "state": returnPayload.payload[i].State,
                        "zip": returnPayload.payload[i].Zip
                    };

                    tableData.push(row);
                }

                // load up our data table

                $('#theclientselectiongrid').dataTable({
                    responsive: true,
                    "data": tableData,
                    "bDestroy": true,
                    "deferRender": true,
                    rowId: 'clientid',
                    "columnDefs": [
                        {
                            "targets": [0],
                            "visible": false,
                            "searchable": false
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
                        },
                        {
                            title: "Select",
                            "render": function (data, type, row, meta) {
                                return '<a data-id="' + row.clientid + '" onclick="onselectmotherfather(event, ' + row.clientid + ')" href="#">select</a>';
                            }
                        }
                    ],
                    "initComplete": function (settings, json) {
                        ViewModule.choosemotherfatherdialog.modal({
                            keyboard: false
                        });
                    }
                });

            }
        });

    };

    // set father

    ViewModule.buttonsetfather.click(function(event) {
        event.preventDefault();
        application.setfather = 1;

        ViewModule.setmotherfatherbuttonclicked();

    });

    // set mother

    ViewModule.buttonsetmother.click(function (event) {
        event.preventDefault();
        application.setfather = 0;

        ViewModule.setmotherfatherbuttonclicked();

    });  

    // setup user account for client

    ViewModule.buttonsetupuseraccountforclient.click(function(event) {
        event.preventDefault();

        ViewModule.setupclientuseraccountdialog.modal({
            keyboard: false
        });
    });

    ViewModule.buttonsaveclientuseraccount.click(function(event) {

        var formValidated = true;

        if (ViewModule.textemailaddress.val().length === 0) {
            ViewModule.textemailaddress.addClass('is-invalid');
            formValidated = false;
            return;
        } else {
            ViewModule.textemailaddress.removeClass('is-invalid');
        }

        if (ViewModule.passwordpassword.val().length < 8) {
            ViewModule.passwordpassword.addClass('is-invalid');
            formValidated = false;
            return;
        } else {
            ViewModule.passwordpassword.removeClass('is-invalid');
        }

        if (ViewModule.passwordconfirmpassword.val().length < 8) {
            ViewModule.passwordconfirmpassword.addClass('is-invalid');
            formValidated = false;
            return;
        } else {
            ViewModule.passwordconfirmpassword.removeClass('is-invalid');
        }

        if (ViewModule.passwordpassword.val() !== ViewModule.passwordconfirmpassword.val()) {
            ViewModule.passwordpassword.addClass('is-invalid');
            ViewModule.passwordconfirmpassword.addClass('is-invalid');
            formValidated = false;
            return;
        } else {
            ViewModule.passwordpassword.removeClass('is-invalid');
            ViewModule.passwordconfirmpassword.removeClass('is-invalid');
        }

        if (formValidated === false)
            return;
        else {

            var objectToSend = allocObjectToSend();

            objectToSend.payload.clientid = parseInt(application.objecttomanageid);
            objectToSend.payload.password = ViewModule.passwordpassword.val();
            objectToSend.payload.emailaddress = ViewModule.textemailaddress.val();
            
            runCustom(CreateClientSystemUserAccount, objectToSend, function (returnPayload) {
                if (returnPayload.succeeded === true) {
                    ViewModule.pisassociatedtouseraccount.show();
                    ViewModule.pidnotassociatedtouseraccount.hide();
                    ViewModule.buttonsetupuseraccountforclient.hide();
                    ViewModule.buttondeactivateuseraccountforclient.show();

                    ViewModule.setupclientuseraccountdialog.hide();
                    ViewModule.setupclientuseraccountdialog.removeData();
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                }
            });

        }

    });

    // deactivate user account for client

    ViewModule.buttondeactivateuseraccountforclient.click(function(event) {
        event.preventDefault();

        var objectToSend = allocObjectToSend();

        objectToSend.payload.clientid = parseInt(application.objecttomanageid);

        runCustom(DeactivateClientSystemUserAccount, objectToSend, function (returnPayload) {
            if (returnPayload.succeeded === true) {
                ViewModule.pisassociatedtouseraccount.hide();
                ViewModule.pidnotassociatedtouseraccount.show();
                ViewModule.buttonsetupuseraccountforclient.show();
                ViewModule.buttondeactivateuseraccountforclient.hide();
            }
        });
    });

    // enlist client in a program

    ViewModule.buttonenlistclientinprogram.click(function(event) {
        event.preventDefault();

        var objectToSend = allocObjectToSend();
        objectToSend.payload.clientid = parseInt(application.objecttomanageid);

        runCustom(RetrieveClientProgramsNotEnlistedIn, objectToSend, function (returnPayload) {
            if (returnPayload.succeeded === true) {

                var tableHtml = '<table id="theclientprogramsselectiongrid" class="display" width="100%">';
                tableHtml += '</table>';

                ViewModule.programsnotenlistedingrid.html(tableHtml);

                // build our table data

                var tableData = [];

                for (var i = 0; i < returnPayload.payload.length; i++) {
                    var row = {
                        "programid": returnPayload.payload[i].ProgramID.toString(),
                        "name": returnPayload.payload[i].Name,
                        "description": returnPayload.payload[i].Description                        
                    };

                    tableData.push(row);
                }

                // load up our data table

                $('#theclientprogramsselectiongrid').dataTable({
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
                            title: "ProgramID",
                            data: "programid"
                        },
                        {
                            title: "Name",
                            data: "name"
                        },
                        {
                            title: "Description",
                            data: "description"
                        },
                        {
                            title: "Select",
                            "render": function (data, type, row, meta) {
                                return '<a data-id="' + row.programid + '" onclick="onselectprogram(event, ' + row.programid + ')" href="#">select</a>';
                            }
                        }
                    ],
                    "initComplete": function (settings, json) {
                        ViewModule.chooseprogramdialog.modal({
                            keyboard: false
                        });
                    }
                });

            }
        });

    });


    // enlist client in program event

    ViewModule.buttonenlistclientinprogramevent.click(function(event) {
        event.preventDefault();

        var objectToSend = allocObjectToSend();
        objectToSend.payload.clientid = parseInt(application.objecttomanageid);
        objectToSend.payload.programid = application.selectedprogramid;

        runCustom(RetrieveClientProgramEventsNotEnlistedIn, objectToSend, function (returnPayload) {
            if (returnPayload.succeeded === true) {

                var tableHtml = '<table id="theclientprogrameventsselectiongrid" class="display" width="100%">';
                tableHtml += '</table>';

                ViewModule.programeventsnotenlistedin.html(tableHtml);

                // build our table data

                var tableData = [];

                for (var i = 0; i < returnPayload.payload.length; i++) {
                    var row = {
                        "programeventid": returnPayload.payload[i].ProgramEventID.toString(),
                        "name": returnPayload.payload[i].Name,
                        "description": returnPayload.payload[i].Description
                    };

                    tableData.push(row);
                }

                // load up our data table

                $('#theclientprogrameventsselectiongrid').dataTable({
                    responsive: true,
                    "data": tableData,
                    "bDestroy": true,
                    "deferRender": true,
                    rowId: 'programeventid',
                    "columnDefs": [
                        {
                            "targets": [0],
                            "visible": false,
                            "searchable": false
                        }
                    ],
                    "columns": [
                        {
                            title: "ProgramEventID",
                            data: "programeventid"
                        },
                        {
                            title: "Name",
                            data: "name"
                        },
                        {
                            title: "Description",
                            data: "description"
                        },
                        {
                            title: "Select",
                            "render": function (data, type, row, meta) {
                                return '<a data-id="' + row.programeventid + '" onclick="onselectprogramevent(event, ' + row.programeventid + ')" href="#">select</a>';
                            }
                        }
                    ],
                    "initComplete": function (settings, json) {
                        ViewModule.chooseprogrameventdialog.modal({
                            keyboard: false
                        });
                    }
                });

            }
        });        

    });

    // client requests by program

    ViewModule.buttonaddclientrequest.click(function(event) {
        event.preventDefault();

        ViewModule.addclientrequestdialog.modal({
            keyboard: false
        });

    });

    ViewModule.buttonaddclientrequestfromdialog.click(function(event) {
        event.preventDefault();

        var objectToSend = allocObjectToSend();
        objectToSend.payload.ClientID = parseInt(application.objecttomanageid);
        objectToSend.payload.ProgramID = application.selectedprogramrequestid;
        objectToSend.payload.RequestInformation = ViewModule.textrequestinformation.val();
        objectToSend.transactionid = null;

        runGen(eCreateclientrequests,
            objectToSend,
            function(returnPayload) {
                if (returnPayload.succeeded === true) {
                    ViewModule.loadClientProgramRequests();
                }
            });

    });

    // send message to client

    ViewModule.buttonsendmessagetoclient.click(function(event) {
        event.preventDefault();

        ViewModule.sendmessagetoclientdialog.modal({
            keyboard: false
        });

    });

    ViewModule.buttonsendmessagetoclientfromdialog.click(function(event) {
        event.preventDefault();

        var objectToSend = allocObjectToSend();
        objectToSend.payload.loggedInUserID = application.loggedInUserID;        
        objectToSend.payload.Subject = ViewModule.textmessagesubject.val();
        objectToSend.payload.Body = ViewModule.textmessagebody.val();
        objectToSend.payload.messagerecipients = [];

        var messagerecipient = {};

        messagerecipient.AccountID = parseInt(application.objecttomanageid);
        messagerecipient.AccountTypeID = 3;

        objectToSend.payload.messagerecipients.push(messagerecipient);

        runCustom(SendInternalMessage,
            objectToSend,
            function (returnPayload) {
                if (returnPayload.succeeded === true) {
                    ViewModule.sendmessagetoclientdialog.hide();
                    ViewModule.sendmessagetoclientdialog.removeData();
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                }
            });

    });

    ViewModule.buttonuploadhappyclientphoto.click(function(event) {
        event.preventDefault();

        ViewModule.uploadhappyclientphotodialog.modal({
            keyboard: false
        });

    });

    ViewModule.buttonuploadhappyclientphotofromdialog.click(function(event) {
        event.preventDefault();

        var happyPhotoFile = ViewModule.filehappyclient.prop('files');

        getBase64(happyPhotoFile[0]).then(
            data => {
                var objectToSend = allocObjectToSend();
                objectToSend.payload.clientid = parseInt(application.objecttomanageid);
                objectToSend.payload.programid = ViewModule.programselectorhappy.val();
                objectToSend.payload.photodata = data.split("base64,")[1];
                objectToSend.payload.filename = happyPhotoFile[0].name;
                objectToSend.payload.filesize = happyPhotoFile[0].size;

                runCustom(AddHappyClientPhoto,
                    objectToSend,
                    function (returnPayload) {
                        if (returnPayload.succeeded === true) {

                            ViewModule.uploadhappyclientphotodialog.hide();
                            ViewModule.uploadhappyclientphotodialog.removeData();
                            $('body').removeClass('modal-open');
                            $('.modal-backdrop').remove();

                            ViewModule.loadClientHappyPhotos();
                        }
                    });
            }
        );        

    });   

    // deactivate client account from system

    ViewModule.buttondeactivateclientfromsystem.click(function(event) {
        event.preventDefault();

        var objectToSend = allocObjectToSend();

        objectToSend.payload.clientid = parseInt(application.objecttomanageid);        

        runCustom(DeactivateClient,
            objectToSend,
            function (returnPayload) {

                if (returnPayload.succeeded === true) {
                    self.minimal.navigateToPage("manageclients", true);
                }
            });        
    });

    cb();
};

ViewModule.Deinitialize = function () {
    var self = this;
};

var onselectmotherfather = function (event, clientid) {

    event.preventDefault();

    ViewModule.choosemotherfatherdialog.hide();
    ViewModule.choosemotherfatherdialog.removeData();
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();

    if (application.setfather === 1) {
        var objectToSend = allocObjectToSend();

        objectToSend.payload.clientid = parseInt(application.objecttomanageid);
        objectToSend.payload.clientfatherid = clientid;

        runCustom(SaveClientFather,
            objectToSend,
            function (returnPayload) {

                if (returnPayload.succeeded === true) {
                    objectToSend = allocObjectToSend();
                    objectToSend.payload.clientid = parseInt(application.objecttomanageid);

                    runCustom(RetrieveClientFatherByClientID,
                        objectToSend,
                        function (returnPayload) {
                            if (returnPayload.succeeded === true) {
                                if (returnPayload.payload === null) {
                                    ViewModule.pfathername.text("not assigned");
                                } else {
                                    ViewModule.pfathername.text(returnPayload.payload.FirstName + " " + returnPayload.payload.LastName);
                                }
                            }
                        });
                }
            });
    } else {
        objectToSend = allocObjectToSend();

        objectToSend.payload.clientid = parseInt(application.objecttomanageid);
        objectToSend.payload.clientmotherid = clientid;

        runCustom(SaveClientMother,
            objectToSend,
            function (returnPayload) {

                if (returnPayload.succeeded === true) {
                    objectToSend = allocObjectToSend();
                    objectToSend.payload.clientid = parseInt(application.objecttomanageid);

                    runCustom(RetrieveClientMotherByClientId,
                        objectToSend,
                        function (returnPayload) {
                            if (returnPayload.succeeded === true) {
                                if (returnPayload.payload === null) {
                                    ViewModule.pmothername.text("not assigned");
                                } else {
                                    ViewModule.pmothername.text(returnPayload.payload.FirstName + " " + returnPayload.payload.LastName);
                                }
                            }
                        });
                }
            });
    }

};

var onselectprogram = function(event, programid) {

    event.preventDefault();

    ViewModule.chooseprogramdialog.hide();
    ViewModule.chooseprogramdialog.removeData();
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();

    var objectToSend = allocObjectToSend();

    objectToSend.payload.clientid = parseInt(application.objecttomanageid);
    objectToSend.payload.programid = programid;

    runCustom(EnlistClientInProgram,
        objectToSend,
        function(returnPayload) {
            ViewModule.loadClientProgramEnlistments();
        });

};

var onselectprogramevent = function (event, programeventid) {

    event.preventDefault();

    ViewModule.chooseprogrameventdialog.hide();
    ViewModule.chooseprogrameventdialog.removeData();
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();

    var objectToSend = allocObjectToSend();

    objectToSend.payload.clientid = parseInt(application.objecttomanageid);
    objectToSend.payload.programeventid = programeventid;

    runCustom(EnlistClientInProgramEvent,
        objectToSend,
        function (returnPayload) {
            ViewModule.loadClientProgramEventEnlistments();
        });

};

var unenlistclientfromprogram = function(event, programid) {

    event.preventDefault();

    var objectToSend = allocObjectToSend();

    objectToSend.payload.clientid = parseInt(application.objecttomanageid);
    objectToSend.payload.programid = programid;

    runCustom(UnenlistClientInProgram,
        objectToSend,
        function(returnPayload) {
            ViewModule.loadClientProgramEnlistments();
        });


};

var unenlistclientfromprogramevent = function (event, programeventid) {

    event.preventDefault();

    var objectToSend = allocObjectToSend();

    objectToSend.payload.clientid = parseInt(application.objecttomanageid);
    objectToSend.payload.programeventid = programeventid;

    runCustom(UnenlistClientInProgramEvent,
        objectToSend,
        function (returnPayload) {
            ViewModule.loadClientProgramEventEnlistments();
        });


};

var deleteclientrequest = function(event, clientrequestid) {

    event.preventDefault();

    var objectToSend = allocObjectToSend();

    objectToSend.payload.ClientRequestID = clientrequestid;   
    objectToSend.transactionid = null;

    runGen(eDeleteclientrequests,
        objectToSend,
        function (returnPayload) {
            ViewModule.loadClientProgramRequests();
        });

};

var deletehappyclientphoto = function(event, happyclientpictureid) {

    event.preventDefault();

    var objectToSend = allocObjectToSend();

    objectToSend.payload.happyclientphotoid = happyclientpictureid;
    objectToSend.transactionid = null;

    runCustom(DeleteHappyClientPhoto,
        objectToSend,
        function (returnPayload) {
            ViewModule.loadClientHappyPhotos();
        });

};


