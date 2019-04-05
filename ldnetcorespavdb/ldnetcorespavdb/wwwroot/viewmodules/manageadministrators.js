function ViewModule() { };

ViewModule.Title = "Manage Administrators";

ViewModule.minimal = null;

// page vars

// page controls

ViewModule.administratorstable = $('#administratorstable');
ViewModule.buttonaddadministrator = $('#buttonaddadministrator');
ViewModule.buttonaddtheadministrator = $("#buttonaddtheadministrator");
ViewModule.deleteconfirmationdialog = $('#deleteconfirmationdialog');
ViewModule.addadministratordialog = $('#addadministratordialog');
ViewModule.linkdashboard = $('#linkdashboard');
ViewModule.textlastname = $('#textlastname');
ViewModule.textfirstname = $('#textfirstname');
ViewModule.textphonenumber = $('#textphonenumber');
ViewModule.textemailaddress = $('#textemailaddress');
ViewModule.passwordpassword = $('#passwordpassword');
ViewModule.passwordverifypassword = $('#passwordverifypassword');
ViewModule.buttondeleteadministrator = $('#buttondeleteadministrator');
ViewModule.dialogtitle = $("#dialogtitle");
ViewModule.passworddiv = $("#passworddiv");

ViewModule.LoadAdministrators = function() {

    var objectToSend = {};
    objectToSend.protocol = RetrieveAllAdministrators;  

    postAjaj("api/Custom", objectToSend, function (data) {
        returnPayload = JSON.parse(data);

        if (returnPayload.succeeded === true) {

            var tableHtml = '<table id="theadministratorstable" class="display" width="100%">';
            tableHtml += '</table>';

            ViewModule.administratorstable.html(tableHtml);

            // build our table data

            var tableData = [];

            for (var i = 0; i < returnPayload.payload.length; i++)
            {               
                var row = {
                    "adminid": returnPayload.payload[i].AdminID.toString(),
                    "lastname": returnPayload.payload[i].LastName,
                    "firstname": returnPayload.payload[i].FirstName,
                    "phonenumber": returnPayload.payload[i].PhoneNumber,
                    "emailaddress": returnPayload.payload[i].EmailAddress
                };                                           

                tableData.push(row);
            }

            // load up our data table

            $('#theadministratorstable').dataTable({
                responsive: true,
               "data": tableData,
               "bDestroy": true,
               "deferRender": true,
               rowId: 'adminid',
               "columnDefs" : [
                {
                    "targets": [0],
                    "visible" : false,
                    "searchable": false
                }
               ],
               "columns": [
                   {
                       title: "AdminId",
                       data: "adminid"
                   },
                  {
                      title: "Last Name",
                      data: "lastname"
                  },
                  {
                      title: "First Name",
                      data: "firstname"
                  },
                  {
                      title: "Phone Number",
                      data : "phonenumber"
                  },
                  {
                      title: "Email Address",
                      data: "emailaddress"
                  }/*,
                  { title: "Edit", 
                      "render": function (data, type, row, meta){
                          return '<a data-id="' + row.adminid + '" class="administratoredit" href="#">edit</a>';
                        } 
                       }*/,
                  { title: "Delete",
                      "render": function (data, type, row, meta){                          
                              return '<a data-id="' + row.adminid + '" class="administratordelete" href="#">delete</a>';                          
                        }
                      }
                ]
            });


            $('.administratoredit').click(function (event) {
                event.preventDefault();
                application.isadd = false;
                application.objecttoeditid = $(this).attr("data-id");

            });

            $('.administratordelete').click(function (event) {
                event.preventDefault();
                application.objecttodeleteid = $(this).attr("data-id");

                ViewModule.deleteconfirmationdialog.modal({
                    keyboard: false
                });
            });            
        }
    });
};

ViewModule.Initialize = function (minimal, url, cb) {
    var self = this;

    self.minimal = minimal;

    // load page control references

    // initialize initial control state

    ViewModule.LoadAdministrators();

    // load up events

    ViewModule.buttonaddadministrator.click(function (event) {
        event.preventDefault();
        
        ViewModule.addadministratordialog.modal({
            keyboard: false
        });
    });

    ViewModule.buttonaddtheadministrator.click(function (event) {

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

        if (ViewModule.textphonenumber.val().length === 0) {
            ViewModule.textphonenumber.addClass('is-invalid');
            formValidated = false;
        } else {
            ViewModule.textphonenumber.removeClass('is-invalid');
        }

        if (ViewModule.textemailaddress.val().length=== 0) {
            ViewModule.textemailaddress.addClass('is-invalid');
            formValidated = false;
        } else {
            ViewModule.textemailaddress.removeClass('is-invalid');
        }

        if (ViewModule.passwordpassword.val().length < 7) {
            ViewModule.passwordpassword.addClass('is-invalid');
            formValidated = false;
        } else {
            ViewModule.passwordpassword.removeClass('is-invalid');
        }

        if (ViewModule.passwordverifypassword.val().length < 7 ||
            ViewModule.passwordverifypassword.val() !== ViewModule.passwordpassword.val()) {
            ViewModule.passwordverifypassword.addClass('is-invalid');
            formValidated = false;
        } else {
            ViewModule.passwordverifypassword.removeClass('is-invalid');
        }

        if (formValidated === false)
            return;
        else {

            var objectToSend = {};
            objectToSend.protocol = AddAdministrator;
            objectToSend.payload = new displayadministrator();            

            objectToSend.payload.LastName = ViewModule.textlastname.val();
            objectToSend.payload.FirstName = ViewModule.textfirstname.val();
            objectToSend.payload.PhoneNumber = ViewModule.textphonenumber.val();
            objectToSend.payload.EmailAddress = ViewModule.textemailaddress.val();
            objectToSend.payload.Password = ViewModule.passwordpassword.val();

            postAjaj("api/Custom", objectToSend, function (data) {
                returnPayload = JSON.parse(data);

                if (returnPayload.succeeded === true) {
                    ViewModule.LoadAdministrators();
                }                
            });

        }
    });

    ViewModule.linkdashboard.click(function(event) {
        event.preventDefault();
        minimal.navigateToPage("dashboard", true);
    });

    ViewModule.buttondeleteadministrator.click(function() { 
        var objectToSend = {};
        objectToSend.protocol = DeleteAdministrator;
        objectToSend.payload = {};
        objectToSend.payload.adminid = application.objecttodeleteid;         

        postAjaj("api/Custom", objectToSend, function (data) {
            returnPayload = JSON.parse(data);

            if (returnPayload.succeeded === true) {
                ViewModule.LoadAdministrators();
            }
        });
    });

    cb();
};

ViewModule.Deinitialize = function () {
    var self = this;
};
