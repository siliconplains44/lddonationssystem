/**
 * Created by Allan on 1/27/2015.
 */

function ViewModule() { };

ViewModule.Title = "Manage Programs";

ViewModule.minimal = null;

// page vars

// page controls

ViewModule.programstable = $('#programstable');
ViewModule.programeventstable = $('#programeventstable');
ViewModule.buttonaddprogram = $('#buttonaddprogram');
ViewModule.buttonaddprogramevent = $('#buttonaddprogramevent');
ViewModule.buttondeleteprogram = $('#buttondeleteprogram');
ViewModule.buttondeleteprogramevent = $('#buttondeleteprogramevent');
ViewModule.deleteconfirmationdialog = $('#deleteconfirmationdialog');
ViewModule.deleteprogrameventconfirmationdialog = $('#deleteprogrameventconfirmationdialog');
ViewModule.linkdashboard = $('#linkdashboard');

ViewModule.LoadPrograms = function() {

    var objectToSend = {};
    objectToSend.protocol = RetrieveAllPrograms;  

    postAjaj("api/Custom", objectToSend, function (data) {
        returnPayload = JSON.parse(data);

        if (returnPayload.succeeded === true) {

            var tableHtml = '<table id="theprogramstable" class="display" width="100%">';
            tableHtml += '</table>';

            ViewModule.programstable.html(tableHtml);

            // build our table data

            var tableData = [];

            for (var i = 0; i < returnPayload.payload.length; i++)
            {
                var published = "";

                if (0 === returnPayload.payload[i].IsPublished) {
                    published = "No";
                }
                else {
                    published = "Yes";
                }

                var row = {
                    "programid": returnPayload.payload[i].ProgramID.toString(),
                    "name": returnPayload.payload[i].Name,
                    "ispublished": published,
                    "year": returnPayload.payload[i].Year.toString(),
                    "description": returnPayload.payload[i].Description,
                };                                           

                tableData.push(row);
            }

            // load up our data table

            $('#theprogramstable').dataTable({
                responsive: true,
               "data": tableData,
               "bDestroy": true,
               "deferRender": true,
               rowId: 'programid',
               "columnDefs" : [
                {
                    "targets": [0],
                    "visible" : false,
                    "searchable": false
                }
               ],
               "columns": [
                   {
                       title: "ProgramId",
                       data: "programid"
                   },
                  {
                      title: "Name",
                      data: "name"
                  },
                  {
                      title: "IsPublished",
                      data: "ispublished"
                  },
                  {
                      title: "Year",
                      data : "year"
                  },
                  {
                      title: "Description",
                      data: "description"
                  },
                  { title: "View Events",
                    "render": function(data, type, row, meta){
                        return '<a data-id="' + row.programid + '" class="programviewevents" href="#">view events</a>';
                        }
                      },
                  { title: "Edit", 
                      "render": function (data, type, row, meta){
                          return '<a data-id="' + row.programid + '" class="programedit" href="#">edit</a>';
                        } 
                       },
                  { title: "Delete",
                      "render": function (data, type, row, meta){
                          if (row.ispublished === "No") {
                              return '<a data-id="' + row.programid + '" class="programdelete" href="#">delete</a>';
                          } else {
                              return '<p></p>';
                          }

                        }
                      }
                ]
            });

            // add event listeners for table link clicks
            $('.programviewevents').click(function (event) {
                event.preventDefault();
                var programid = $(this).attr("data-id");
                application.selectedprogramid = programid;

                ViewModule.buttonaddprogramevent.show();

                var objectToSend = {};
                objectToSend.protocol = RetrieveProgramEventsByProgram;
                objectToSend.payload = {};
                objectToSend.payload.programid = programid;

                postAjaj("api/Custom", objectToSend, function (data) {
                    returnPayload = JSON.parse(data);

                    if (returnPayload.succeeded === true) {

                        var tableHtml = '<table id="theprogrameventstable" class="display" width="100%">';
                        tableHtml += '</table>';

                        ViewModule.programeventstable.html(tableHtml);

                        // build our table data

                        var tableData = [];

                        for (var i = 0; i < returnPayload.payload.length; i++) {
                            var issingledate = "";

                            if (0 === returnPayload.payload[i].IsSingleDate) {
                                issingledate = "No";
                            }
                            else {
                                issingledate = "Yes";
                            }

                            var FromMoment = moment(returnPayload.payload[i].From);
                            var ToMoment = moment(returnPayload.payload[i].To);

                            var row = {
                                "programeventid": returnPayload.payload[i].ProgramEventID.toString(),
                                "programid": returnPayload.payload[i].ProgramID.toString(),
                                "name": returnPayload.payload[i].Name,
                                "issingledate": issingledate,
                                "from": FromMoment.format("MM-DD-YYYY"),
                                "to": ToMoment.format("MM-DD-YYYY"),
                                "description": returnPayload.payload[i].Description,
                            };

                            tableData.push(row);
                        }

                        // load up our data table

                        $('#theprogrameventstable').dataTable({
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
                                },
                                {
                                    "targets": [1],
                                    "visible": false,
                                    "searchable": false
                                }
                            ],
                            "columns": [
                                {
                                    title: "ProgramEventId",
                                    data: "programeventid"
                                },
                                {
                                    title: "ProgramId",
                                    data: "programid"
                                },
                                {
                                    title: "Name",
                                    data: "name"
                                },
                                {
                                    title: "IsSingleDate",
                                    data: "issingledate"
                                },
                                {
                                    title: "From",
                                    data: "from"
                                },
                                {
                                    title: "To",
                                    data: "to"
                                },
                                {
                                    title: "Description",
                                    data: "description"
                                },
                                {
                                    title: "Edit",
                                    "render": function (data, type, row, meta) {
                                        return '<a data-id="' + row.programeventid + '" class="programeventedit" href="#">edit</a>';
                                    }
                                },
                                {
                                    title: "Delete",
                                    "render": function (data, type, row, meta) {
                                        return '<a data-id="' + row.programeventid + '" class="programeventdelete" href="#">delete</a>';
                                    }
                                }
                            ]
                        });

                        $('.programeventedit').click(function (event) {
                            event.preventDefault();
                            application.isadd = false;
                            application.objecttoeditid = $(this).attr("data-id");
                            minimal.navigateToPage("addeditprogramevent", true);
                        });

                        $('.programeventdelete').click(function (event) {
                            event.preventDefault();

                            application.objecttoeditid = $(this).attr("data-id");

                            ViewModule.deleteprogrameventconfirmationdialog.modal({
                                keyboard: false
                            });
                        });
                    }
                });                
            });

            $('.programedit').click(function (event) {
                event.preventDefault();
                application.isadd = false;
                application.objecttoeditid = $(this).attr("data-id");
                minimal.navigateToPage("addeditprogram", true);
            });

            $('.programdelete').click(function (event) {
                event.preventDefault();
                application.objecttoeditid = $(this).attr("data-id");

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

    ViewModule.buttonaddprogramevent.hide();

    ViewModule.LoadPrograms();

    // load up events

    ViewModule.buttonaddprogram.click(function (event) {
        event.preventDefault();
        application.isadd = true;
        minimal.navigateToPage("addeditprogram", true);
    });

    ViewModule.buttonaddprogramevent.click(function (event) {
        event.preventDefault();
        application.isadd = true;
        minimal.navigateToPage('addeditprogramevent', true);
    });

    ViewModule.linkdashboard.click(function(event) {
        event.preventDefault();
        minimal.navigateToPage("dashboard", true);
    });

    ViewModule.buttondeleteprogram.click(function() { 
        var client = new AjajClient();

        var program = {};
        program.ProgramID = application.objecttoeditid;

        client.Deleteprograms(program, null, function() {
            ViewModule.deleteconfirmationdialog.hide();
            ViewModule.deleteconfirmationdialog.removeData();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
          
            ViewModule.LoadPrograms();
        });
    });

    ViewModule.buttondeleteprogramevent.click(function () {
        var client = new AjajClient();

        var programevent = {};
        programevent.ProgramEventID = application.objecttoeditid;

        client.Deleteprogramevents(programevent, null, function () {
            ViewModule.deleteprogrameventconfirmationdialog.hide();
            ViewModule.deleteprogrameventconfirmationdialog.removeData();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();

            $('#theprogrameventstable').hide();
            ViewModule.LoadPrograms();
        });
    });

    cb();
};

ViewModule.Deinitialize = function () {
    var self = this;
};