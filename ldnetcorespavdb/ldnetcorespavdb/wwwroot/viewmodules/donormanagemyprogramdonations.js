/**
 * Created by Allan on 1/27/2015.
 */

function ViewModule() { }

ViewModule.Title = "Leopard Data Minimal Home";

ViewModule.minimal = null;

// page vars

// page controls


ViewModule.linkdashboard = $('#linkdashboard');

ViewModule.donorcommittedclientrequestsgrid = $('#donorcommittedclientrequestsgrid');

ViewModule.programselector = $('#programselector');

ViewModule.availableclientrequestsgrid = $('#availableclientrequestsgrid');

ViewModule.loadProgramsSelector = function () {

    objectToSend = allocObjectToSend();
    objectToSend.payload = ' ProgramID > 0 ';
    objectToSend.transactionid = null;

    runGen(eRetrieveWithWhereClauseprograms,
        objectToSend,
        function (returnPayload) {
          

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
                application.donormanageprogramdonationspageprogramidselected =
                    ViewModule.programselector.val();
                ViewModule.loadAvailableClientRequestsGrid();
            });           

        });

};

ViewModule.loadDonorComittedClientRequestsGrid = function () {

    objectToSend = allocObjectToSend();
    objectToSend.payload.donorid = parseInt(application.loggedInUserID);    

    runCustom(RetrieveAllclientRequestsCommittedToByDonorID,
        objectToSend,
        function (returnPayload) {

            var tableHtml = '<table id="thedonorcommittedclientrequestsgrid" class="display" width="100%">';
            tableHtml += '</table>';

            ViewModule.donorcommittedclientrequestsgrid.html(tableHtml);

            // build our table data

            var tableData = [];

            for (var i = 0; i < returnPayload.payload.length; i++) {
                var row = {
                    "clientrequestid": returnPayload.payload[i].ClientRequestID.toString(),
                    "requestinformation": returnPayload.payload[i].ClientRequest,
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

            $('#thedonorcommittedclientrequestsgrid').dataTable({
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
                        title: "ClientRequest",
                        data: "requestinformation"
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
                    }
                ]

            });
        });


};

ViewModule.loadAvailableClientRequestsGrid = function () {

    objectToSend = allocObjectToSend();
    objectToSend.payload.programid = parseInt(application.donormanageprogramdonationspageprogramidselected );

    runCustom(RetrieveAllNonDonorCommittedClientRequestsByProgramID,
        objectToSend,
        function (returnPayload) {

            var tableHtml = '<table id="theavailableclientrequestsgrid" class="display" width="100%">';
            tableHtml += '</table>';

            ViewModule.availableclientrequestsgrid.html(tableHtml);

            // build our table data

            var tableData = [];

            for (var i = 0; i < returnPayload.payload.length; i++) {
                var row = {
                    "clientrequestid": returnPayload.payload[i].ClientRequestID.toString(),
                    "requestinformation": returnPayload.payload[i].ClientRequest,
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

            $('#theavailableclientrequestsgrid').dataTable({
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
                        title: "ClientRequest",
                        data: "requestinformation"
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

    ViewModule.loadProgramsSelector();
    ViewModule.loadDonorComittedClientRequestsGrid();

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

var enlistdonorinclientrequest = function (event, clientrequestid) {

    event.preventDefault();

    var FromMoment = moment();

    var objectToSend = allocObjectToSend();
    objectToSend.payload.loggedinuserid = application.loggedInUserID;

    runCustom(RetrieveDisplayAccountByLoggedInUserID,
        objectToSend,
        function (returnPayload) {
            var objectToSend = allocObjectToSend();
            objectToSend.payload.DonorID = parseInt(returnPayload.payload);
            objectToSend.payload.CommitmentDateTime = FromMoment.format("MM-DD-YYYY");
            objectToSend.payload.ClientRequestID = clientrequestid;
            objectToSend.payload.ReceivedAtCollectionPoint = 0;
            objectToSend.payload.DistributedToRecipient = 0;
            objectToSend.transactionid = null;

            runGen(eCreateprogramdonorcommitments,
                objectToSend,
                function (returnPayload) {
                    if (returnPayload.succeeded === true) {
                        ViewModule.loadDonorComittedClientRequestsGrid();
                        ViewModule.loadAvailableClientRequestsGrid();
                    }
                });
        });    

};