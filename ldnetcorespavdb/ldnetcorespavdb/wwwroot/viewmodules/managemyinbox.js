function ViewModule() { }

ViewModule.Title = "Leopard Data Minimal Home";

ViewModule.minimal = null;

// page vars

// page controls

ViewModule.linkdashboard = $('#linkdashboard');

ViewModule.inboxgrid = $('#inboxgrid');

ViewModule.buttonsendmessage = $('#buttonsendmessage');

// dialogs

ViewModule.readmessagedialog = $('#readmessagedialog');

ViewModule.buildmessagedialog = $('#buildmessagedialog');

// page controls - dialogs

ViewModule.paragraphsent = $('#paragraphsent');
ViewModule.paragraphfrom = $('#paragraphfrom');
ViewModule.paragraphmessagesubject = $('#paragraphmessagesubject');
ViewModule.paragraphbody = $('#paragraphbody');
ViewModule.buttonreplytomessage = $('#buttonreplytomessage');

ViewModule.screenwidthdiv = $('#screenwidthdiv');

ViewModule.availablerecipientsgrid = $('#availablerecipientsgrid');
ViewModule.recipientsgrid = $('#recipientsgrid');
ViewModule.textmessagesubject = $('#textmessagesubject');
ViewModule.textmessagebody = $('#textmessagebody');
ViewModule.buttonsendmessagefromdialog = $('#buttonsendmessagefromdialog');

ViewModule.inboxTableData = [];
ViewModule.currentMessageRecipients = [];
ViewModule.AvailableRecipients = [];

ViewModule.loadAvailableRecipientsGrid = function() {

    var objectToSend = allocObjectToSend();
    objectToSend.payload.messagerecipients = [];

    // build our exclusion list

    for (var i = 0; i < ViewModule.currentMessageRecipients.length; i++) {
        objectToSend.payload.messagerecipients.push({
            AccountID: ViewModule.currentMessageRecipients[i].accountid,
            AccountTypeID:  ViewModule.currentMessageRecipients[i].accounttypeid });
    }

    runCustom(RetrieveAllAvailableMessageRecipientsWithExclusionList,
        objectToSend,
        function (returnPayload) {

            var tableHtml = '<table id="theavailablerecipientsgrid" class="display" width="100%">';
            tableHtml += '</table>';

            ViewModule.availablerecipientsgrid.html(tableHtml);

            // build our table data

            ViewModule.AvailableRecipients = [];

            for (var i = 0; i < returnPayload.payload.length; i++) {
                var row = {                    
                    "accounttypeid": returnPayload.payload[i].AccountTypeID,
                    "accountid": returnPayload.payload[i].AccountID,
                    "accounttypename": returnPayload.payload[i].AccountTypeName,
                    "lastname": returnPayload.payload[i].LastName,
                    "middlename": returnPayload.payload[i].MiddleName,
                    "firstname": returnPayload.payload[i].FirstName,
                    "birthdate": returnPayload.payload[i].BirthDate,
                    "city": returnPayload.payload[i].City,
                    "state": returnPayload.payload[i].State,
                    "zip": returnPayload.payload[i].Zip
                };

                ViewModule.AvailableRecipients.push(row);
            }

            // load up our data table

            $('#theavailablerecipientsgrid').dataTable({
                responsive: true,
                "data": ViewModule.AvailableRecipients,
                "bDestroy": true,
                "deferRender": true,
                rowId: 'messageid',
                "columnDefs": [
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false
                    }
                ],
                "columns": [
                    {
                        title: "Account ID",
                        data: "accountid"
                    },
                    {
                        title: "Account Type",
                        data: "accounttypename"
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
                        title: "Birth Date",
                        data: "birthdate"
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
                        title: "add to recipient list",
                        "render": function (data, type, row, meta) {
                            return '<a data-accountid="' +
                                row.accountid +
                                'data-accounttypeid=' + 
                                row.accounttypeid +
                                '" onclick="addtorecipientlist(event, ' +
                                row.accountid +
                                "," + row.accounttypeid +
                                ')" href="#">add to recipientlist</a>';
                        }
                    }
                ],
                "initComplete": function (settings, json) {
                    this.api().columns.adjust();
                    ViewModule.buildmessagedialog.modal('handleUpdate');
                }

            });
        });

};

ViewModule.loadRecipientsGrid = function() {

    var tableHtml = '<table id="therecipientsgrid" class="display" width="100%">';
    tableHtml += '</table>';

    ViewModule.recipientsgrid.html(tableHtml);

    // load up our data table

    $('#therecipientsgrid').dataTable({
        responsive: true,
        "data": ViewModule.currentMessageRecipients,
        "bDestroy": true,
        "deferRender": true,
        rowId: 'messageid',
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }
        ],
        "columns": [
            {
                title: "Account ID",
                data: "accountid"
            },
            {
                title: "Account Type",
                data: "accounttypename"
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
                title: "Birthdate",
                data: "birthdate"
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
                title: "remove",
                "render": function (data, type, row, meta) {
                    return '<a data-accountid="' +
                        row.accountid +
                        'data-accounttypeid=' +
                        row.accounttypeid +
                        '" onclick="removemessagerecipient(event, ' +
                        row.accountid +
                        "," + row.accounttypeid +
                        ')" href="#">remove from recipient list</a>';
                }
            }
        ],
        "initComplete": function (settings, json) {
            this.api().columns.adjust();
            ViewModule.buildmessagedialog.modal('handleUpdate');
        }

    });

};

ViewModule.loadInboxGrid = function() {

    var objectToSend = allocObjectToSend();
    objectToSend.payload.accountid = application.loggedInUserID;
    objectToSend.payload.accounttypeid = application.AccountTypeID;

    runCustom(RetrieveAllInboxMessages,
        objectToSend,
        function (returnPayload) {

            var tableHtml = '<table id="theinboxgrid" class="display" width="100%">';
            tableHtml += '</table>';

            ViewModule.inboxgrid.html(tableHtml);

            // build our table data

            ViewModule.inboxTableData = [];

            for (var i = 0; i < returnPayload.payload.length; i++) {
                var row = {
                    "messageid": returnPayload.payload[i].MessageID.toString(),
                    "messagerecipientid": returnPayload.payload[i].messageRecipientId.toString(),
                    "fromaccountid": returnPayload.payload[i].FromAccountID,
                    "fromaccounttypeid": returnPayload.payload[i].FromAccountTypeID.toString(),
                    "messagesentdatetime": returnPayload.payload[i].MessageSentDateTime.toString(),
                    "subject": returnPayload.payload[i].Subject,
                    "body": returnPayload.payload[i].Body,
                    "messageread": returnPayload.payload[i].messageRead,
                    "fromlastname": returnPayload.payload[i].FromLastName,
                    "fromfirstname": returnPayload.payload[i].FromFirstName
                };

                ViewModule.inboxTableData.push(row);
            }

            // fix dates

            for (i = 0; i < ViewModule.inboxTableData.length; i++) {
                var humanreadable = moment(ViewModule.inboxTableData[i].messagesentdatetime);
                ViewModule.inboxTableData[i].messagesentdatetime = humanreadable.format("MM-DD-YYYY hh:MM A");
            }

            for (i = 0; i < ViewModule.inboxTableData.length; i++) {
                if (ViewModule.inboxTableData[i].messageread !== null) {
                    humanreadable = moment(ViewModule.inboxTableData[i].messageread);
                    ViewModule.inboxTableData[i].messageread = humanreadable.format("MM-DD-YYYY hh:MM A");
                }
                else {
                    ViewModule.inboxTableData[i].messageread = "NO";
                }
            }

            // load up our data table

            $('#theinboxgrid').dataTable({
                responsive: true,
                "data": ViewModule.inboxTableData,
                "bDestroy": true,
                "deferRender": true,
                rowId: 'messageid',
                "columnDefs": [
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false
                    }
                ],
                "columns": [
                    {
                        title: "MessageRecipientID",
                        data: "messagerecipientid"
                    },
                    {
                        title: "Sent",
                        data: "messagesentdatetime"
                    },
                    {
                        title: "Subject",
                        data: "subject"
                    },                    
                    {
                        title: "From Last Name",
                        data: "fromlastname"
                    },                    
                    {
                        title: "From First Name",
                        data: "fromfirstname"
                    },
                    {
                        title: "Is Read",
                        data: "messageread"
                    },
                    {
                        title: "read",
                        "render": function (data, type, row, meta) {
                            return '<a data-id="' +
                                row.messagerecipientid +
                                '" onclick="readmessage(event, ' +
                                row.messagerecipientid +
                                ')" href="#">read</a>';
                        }
                    }
                ]

            });
        });

    ViewModule.buttonreplytomessage.unbind().click(function(event) {

        event.preventDefault();

        ViewModule.readmessagedialog.hide();
        ViewModule.readmessagedialog.removeData();
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();

        ViewModule.currentMessageRecipients = [];

        var objectToSend = allocObjectToSend();
        objectToSend.payload.accountid = ViewModule.currentaccountid;
        objectToSend.payload.accounttypeid = ViewModule.currentaccounttypeid;

        runCustom(RetrieveMessageRecipientByAccountIDAndAccountTypeID,
            objectToSend,
            function (returnPayload) {

                var row = {
                    "accounttypename": returnPayload.payload.AccountTypeName,
                    "accounttypeid": returnPayload.payload.AccountTypeID,
                    "accountid": returnPayload.payload.AccountID,
                    "lastname": returnPayload.payload.LastName,
                    "middlename": returnPayload.payload.MiddleName,
                    "firstname": returnPayload.payload.FirstName,
                    "birthdate": returnPayload.payload.BirthDate,
                    "city": returnPayload.payload.City,
                    "state": returnPayload.payload.State,
                    "zip": returnPayload.payload.Zip
                };

                ViewModule.currentMessageRecipients.push(row);

                ViewModule.loadAvailableRecipientsGrid();
                ViewModule.loadRecipientsGrid();

                ViewModule.buildmessagedialog.modal({
                    keyboard: false
                });
            });     
    });

};

ViewModule.Initialize = function (minimal, url, cb) {
var self = this;

self.minimal = minimal;

    // load page control references

    // initialize initial control state

    ViewModule.loadInboxGrid();

    // load up events

    ViewModule.buttonsendmessage.click(function(event) {
        event.preventDefault();

        ViewModule.currentMessageRecipients = [];

        ViewModule.loadAvailableRecipientsGrid();
        ViewModule.loadRecipientsGrid();

        var widthwindowinpixels = $(document).width() * .9;
        ViewModule.screenwidthdiv.css("width", widthwindowinpixels);

        ViewModule.buildmessagedialog.modal({
            keyboard: false
        });

    });

    ViewModule.linkdashboard.click(function (event) {
        event.preventDefault();
        minimal.navigateToPage("dashboard", true);
    });

    ViewModule.buttonsendmessagefromdialog.click(function(event) {
        event.preventDefault();

        var objectToSend = allocObjectToSend();
        objectToSend.payload.loggedInUserID = application.loggedInUserID;
        objectToSend.payload.Subject = ViewModule.textmessagesubject.val();
        objectToSend.payload.Body = ViewModule.textmessagebody.val();
        objectToSend.payload.messagerecipients = [];

        for (var i = 0; i < ViewModule.currentMessageRecipients.length; i++) {

            var messagerecipient = {};

            messagerecipient.AccountID = ViewModule.currentMessageRecipients[i].accountid;
            messagerecipient.AccountTypeID = ViewModule.currentMessageRecipients[i].accounttypeid;

            objectToSend.payload.messagerecipients.push(messagerecipient);
        }

        runCustom(SendInternalMessage,
            objectToSend,
            function (returnPayload) {
                if (returnPayload.succeeded === true) {
                    ViewModule.buildmessagedialog.hide();
                    ViewModule.buildmessagedialog.removeData();
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                }
            });
    });

    cb();
};

ViewModule.Deinitialize = function () {
    var self = this;
};

var readmessage = function (event, messagerecipientid) {

    event.preventDefault();

    var objectToSend = allocObjectToSend();

    objectToSend.payload.messagerecipientid = messagerecipientid;

    runCustom(SetMessageRecipientMessageRead,
        objectToSend,
        function (returnPayload) {

            // load dialog with message information
            for (var i = 0; i < ViewModule.inboxTableData.length; i++) {
                if (parseInt(ViewModule.inboxTableData[i].messagerecipientid) === messagerecipientid) {
                    ViewModule.paragraphsent.text(ViewModule.inboxTableData[i].messagesentdatetime);
                    ViewModule.paragraphfrom.text(ViewModule.inboxTableData[i].fromfirstname + ViewModule.inboxTableData[i].fromlastname);
                    ViewModule.paragraphmessagesubject.text(ViewModule.inboxTableData[i].subject);
                    ViewModule.paragraphbody.text(ViewModule.inboxTableData[i].body);
                    ViewModule.currentaccountid = ViewModule.inboxTableData[i].fromaccountid;
                    ViewModule.currentaccounttypeid = ViewModule.inboxTableData[i].fromaccounttypeid;
                    break;
                }
            }


            ViewModule.readmessagedialog.modal({
                keyboard: false
            });

            ViewModule.loadInboxGrid();

        });

    
};

var addtorecipientlist = function(event, accountid, accounttypeid) {

    event.preventDefault();

    for (var i = 0; i < ViewModule.AvailableRecipients.length; i++) {
        if (ViewModule.AvailableRecipients[i].accountid === accountid &&
            ViewModule.AvailableRecipients[i].accounttypeid === accounttypeid) {

            ViewModule.currentMessageRecipients.push(ViewModule.AvailableRecipients[i]);

            break;
        }
    }    

    ViewModule.loadAvailableRecipientsGrid();
    ViewModule.loadRecipientsGrid();

};

var removemessagerecipient = function(event, accountid, accounttypeid) {

    event.preventDefault();

    for (var i = 0; i < ViewModule.currentMessageRecipients.length; i++) {
        if (ViewModule.currentMessageRecipients[i].accountid === accountid &&
            ViewModule.currentMessageRecipients[i].accounttypeid === accounttypeid) {
            ViewModule.currentMessageRecipients.splice(i, 1);
            ViewModule.loadAvailableRecipientsGrid();
            ViewModule.loadRecipientsGrid();
            break;
        }
    }
};
