/**
 * Created by Allan on 1/27/2015.
 */

function ViewModule() { }

ViewModule.Title = "Leopard Data Minimal Home";

ViewModule.minimal = null;

// page vars

// page controls

ViewModule.linkdashboard = $('#linkdashboard');

ViewModule.childselector = $('#childselector');
ViewModule.programselector = $('#programselector');

ViewModule.donornotcommittedrequestsgrid = $('#donornotcommittedrequestsgrid');
ViewModule.donorcommittedrequestsgrid = $('#donorcommittedrequestsgrid');

ViewModule.buttonaddclientrequest = $('#buttonaddclientrequest');

// dialogs

ViewModule.addclientrequestdialog = $('#addclientrequestdialog');
ViewModule.three = $('#three');
ViewModule.textrequestinformation = $('#textrequestinformation');

ViewModule.buttonsaveclientrequest = $('#buttonsaveclientrequest');

ViewModule.loadClientChildren = function () {

    ViewModule.buttonaddclientrequest.hide();
    ViewModule.three.hide();

    objectToSend = allocObjectToSend();
    objectToSend.payload.clientid = parseInt(application.loggedInUserID);

    runCustom(RetrieveAllChildrenByParentClientID,
        objectToSend,
        function (returnPayload) {
            
            // build our table data

            var tableData = [];

            for (var i = 0; i < returnPayload.payload.length; i++) {
                var row = {
                    "clientid": returnPayload.payload[i].clientId.toString(),
                    "lastname": returnPayload.payload[i].LastName,
                    "middlename": returnPayload.payload[i].MiddleName,
                    "firstname": returnPayload.payload[i].FirstName
                };

                tableData.push(row);
            }

            var fieldspec = [
                { name: 'clientid', text: 'Id' },
                { name: 'lastname', text: 'Last Name' },
                { name: 'middlename', text: 'Middle Name' },
                { name: 'firstname', text: 'First Name' }
            ];

            ViewModule.childselector.inputpicker({
                data: tableData,
                fields: fieldspec,
                autoOpen: true,
                headShow: true,
                fieldText: 'firstname',
                fieldValue: 'clientid',
                responsive: true
            });

            ViewModule.childselector.change(function (input) {                
                var selectedchildId = parseInt(ViewModule.childselector.val());
                ViewModule.loadChildEnlistedPrograms(selectedchildId);
                application.clientmanageprogramsselectedchildid = selectedchildId;
            });

        });

};

ViewModule.loadChildEnlistedPrograms = function (selectedChildId) {

    ViewModule.buttonaddclientrequest.hide();
    ViewModule.three.hide();

    objectToSend = allocObjectToSend();
    objectToSend.payload.clientid = parseInt(selectedChildId);

    runCustom(RetrieveAllProgramsChildIsEnlistedIn,
        objectToSend,
        function (returnPayload) {

            // build our table data

            var tableData = [];

            for (var i = 0; i < returnPayload.payload.length; i++) {
                var row = {
                    "programid": returnPayload.payload[i].ProgramID.toString(),
                    "name": returnPayload.payload[i].Name,
                    "description": returnPayload.payload[i].Description,
                    "year": returnPayload.payload[i].Year
                };

                tableData.push(row);
            }

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
                var selectedProgramId = parseInt(ViewModule.programselector.val());
                ViewModule.buttonaddclientrequest.show();
                ViewModule.three.show();
                application.clientmanageprogramsselectedprogramid = selectedProgramId;

                ViewModule.loadDonorCommitedRequests();
                ViewModule.loadDonorUnCommitedRequests();
            });

        });

};

ViewModule.loadDonorCommitedRequests = function () {

    objectToSend = allocObjectToSend();
    objectToSend.payload.programid = parseInt(application.clientmanageprogramsselectedprogramid);
    objectToSend.payload.clientid = parseInt(application.clientmanageprogramsselectedchildid);

    runCustom(RetrieveAllDonorCommittedClientRequestsByClientIdAndProgramId,
        objectToSend,
        function (returnPayload) {

            var tableHtml = '<table id="thedonorcommittedrequestsgrid" class="display" width="100%">';
            tableHtml += '</table>';

            ViewModule.donorcommittedrequestsgrid.html(tableHtml);

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

            $('#thedonorcommittedrequestsgrid').dataTable({
                responsive: true,
                "data": tableData,
                "bDestroy": true,
                "deferRender": true,
                rowId: 'clientrequestid',
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
                    }
                ]

            });
        });

};

ViewModule.loadDonorUnCommitedRequests = function () {

    objectToSend = allocObjectToSend();
    objectToSend.payload.programid = parseInt(application.clientmanageprogramsselectedprogramid);
    objectToSend.payload.clientid = parseInt(application.clientmanageprogramsselectedchildid);

    runCustom(RetrieveAllNonDonorCommittedClientRequestsByClientIdAndProgramId,
        objectToSend,
        function (returnPayload) {

            var tableHtml = '<table id="thedonornotcommittedrequestsgrid" class="display" width="100%">';
            tableHtml += '</table>';

            ViewModule.donornotcommittedrequestsgrid.html(tableHtml);

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

            $('#thedonornotcommittedrequestsgrid').dataTable({
                responsive: true,
                "data": tableData,
                "bDestroy": true,
                "deferRender": true,
                rowId: 'clientrequestid',
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
                        title: "delete",
                        "render": function (data, type, row, meta) {
                            return '<a data-id="' + row.clientrequestid + '" onclick="deleteclientrequest(event, ' + row.clientrequestid + ')" href="#">delete</a>';
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

    // initialize initial control state

    ViewModule.buttonaddclientrequest.hide();
    ViewModule.three.hide();

    // load up events

    ViewModule.loadClientChildren();

    ViewModule.linkdashboard.click(function (event) {
        event.preventDefault();

        minimal.navigateToPage("dashboard", true);
    });

    ViewModule.buttonaddclientrequest.click(function (event) {
        event.preventDefault();

        ViewModule.addclientrequestdialog.modal({
            keyboard: false
        });
    });

    ViewModule.buttonsaveclientrequest.click(function (event) {
        event.preventDefault();

        var objectToSend = allocObjectToSend();
        objectToSend.payload.ClientID = parseInt(application.clientmanageprogramsselectedchildid);
        objectToSend.payload.ProgramID = application.clientmanageprogramsselectedprogramid;
        objectToSend.payload.RequestInformation = ViewModule.textrequestinformation.val();
        objectToSend.transactionid = null;

        runGen(eCreateclientrequests,
            objectToSend,
            function (returnPayload) {
                if (returnPayload.succeeded === true) {
                    ViewModule.loadDonorCommitedRequests();
                    ViewModule.loadDonorUnCommitedRequests();
                }
            });
    });

    cb();
};

ViewModule.Deinitialize = function () {
    var self = this;
};

var deleteclientrequest = function (event, clientrequestid) {

    event.preventDefault();

    var objectToSend = allocObjectToSend();

    objectToSend.payload.ClientRequestID = clientrequestid;
    objectToSend.transactionid = null;

    runGen(eDeleteclientrequests,
        objectToSend,
        function (returnPayload) {
            ViewModule.loadDonorCommitedRequests();
            ViewModule.loadDonorUnCommitedRequests();
        });

};