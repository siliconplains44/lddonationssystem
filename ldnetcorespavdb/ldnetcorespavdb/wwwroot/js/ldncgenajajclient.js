/// <reference path ="../../node_modules/@types/jquery/index.d.ts"/>
var eStartTransaction = 100;
var eCommittTransaction = 101;
var eRollbackTransaction = 102;
var eCreateaccounttypes = 1000;
var eUpdateaccounttypes = 2000;
var eDeleteaccounttypes = 3000;
var eRetrieveByIDaccounttypes = 4000;
var eRetrieveWithWhereClauseaccounttypes = 5000;
var eCreateadmins = 6000;
var eUpdateadmins = 7000;
var eDeleteadmins = 8000;
var eRetrieveByIDadmins = 9000;
var eRetrieveWithWhereClauseadmins = 10000;
var eCreateclientprogramenlistments = 11000;
var eUpdateclientprogramenlistments = 12000;
var eDeleteclientprogramenlistments = 13000;
var eRetrieveByIDclientprogramenlistments = 14000;
var eRetrieveWithWhereClauseclientprogramenlistments = 15000;
var eCreateclientprogrameventenlistments = 16000;
var eUpdateclientprogrameventenlistments = 17000;
var eDeleteclientprogrameventenlistments = 18000;
var eRetrieveByIDclientprogrameventenlistments = 19000;
var eRetrieveWithWhereClauseclientprogrameventenlistments = 20000;
var eCreateclientrequests = 21000;
var eUpdateclientrequests = 22000;
var eDeleteclientrequests = 23000;
var eRetrieveByIDclientrequests = 24000;
var eRetrieveWithWhereClauseclientrequests = 25000;
var eCreateclients = 26000;
var eUpdateclients = 27000;
var eDeleteclients = 28000;
var eRetrieveByIDclients = 29000;
var eRetrieveWithWhereClauseclients = 30000;
var eCreatedonorfundscommitments = 31000;
var eUpdatedonorfundscommitments = 32000;
var eDeletedonorfundscommitments = 33000;
var eRetrieveByIDdonorfundscommitments = 34000;
var eRetrieveWithWhereClausedonorfundscommitments = 35000;
var eCreatedonors = 36000;
var eUpdatedonors = 37000;
var eDeletedonors = 38000;
var eRetrieveByIDdonors = 39000;
var eRetrieveWithWhereClausedonors = 40000;
var eCreatefileuploads = 41000;
var eUpdatefileuploads = 42000;
var eDeletefileuploads = 43000;
var eRetrieveByIDfileuploads = 44000;
var eRetrieveWithWhereClausefileuploads = 45000;
var eCreatehappyclientpictures = 46000;
var eUpdatehappyclientpictures = 47000;
var eDeletehappyclientpictures = 48000;
var eRetrieveByIDhappyclientpictures = 49000;
var eRetrieveWithWhereClausehappyclientpictures = 50000;
var eCreateindividualpictures = 51000;
var eUpdateindividualpictures = 52000;
var eDeleteindividualpictures = 53000;
var eRetrieveByIDindividualpictures = 54000;
var eRetrieveWithWhereClauseindividualpictures = 55000;
var eCreateindividuals = 56000;
var eUpdateindividuals = 57000;
var eDeleteindividuals = 58000;
var eRetrieveByIDindividuals = 59000;
var eRetrieveWithWhereClauseindividuals = 60000;
var eCreatemessagerecipients = 61000;
var eUpdatemessagerecipients = 62000;
var eDeletemessagerecipients = 63000;
var eRetrieveByIDmessagerecipients = 64000;
var eRetrieveWithWhereClausemessagerecipients = 65000;
var eCreatemessages = 66000;
var eUpdatemessages = 67000;
var eDeletemessages = 68000;
var eRetrieveByIDmessages = 69000;
var eRetrieveWithWhereClausemessages = 70000;
var eCreatemoduleviews = 71000;
var eUpdatemoduleviews = 72000;
var eDeletemoduleviews = 73000;
var eRetrieveByIDmoduleviews = 74000;
var eRetrieveWithWhereClausemoduleviews = 75000;
var eCreatenotificationsettings = 76000;
var eUpdatenotificationsettings = 77000;
var eDeletenotificationsettings = 78000;
var eRetrieveByIDnotificationsettings = 79000;
var eRetrieveWithWhereClausenotificationsettings = 80000;
var eCreateprogramdonorcommitments = 81000;
var eUpdateprogramdonorcommitments = 82000;
var eDeleteprogramdonorcommitments = 83000;
var eRetrieveByIDprogramdonorcommitments = 84000;
var eRetrieveWithWhereClauseprogramdonorcommitments = 85000;
var eCreateprogramevents = 86000;
var eUpdateprogramevents = 87000;
var eDeleteprogramevents = 88000;
var eRetrieveByIDprogramevents = 89000;
var eRetrieveWithWhereClauseprogramevents = 90000;
var eCreateprograms = 91000;
var eUpdateprograms = 92000;
var eDeleteprograms = 93000;
var eRetrieveByIDprograms = 94000;
var eRetrieveWithWhereClauseprograms = 95000;
var eCreatesettings = 96000;
var eUpdatesettings = 97000;
var eDeletesettings = 98000;
var eRetrieveByIDsettings = 99000;
var eRetrieveWithWhereClausesettings = 100000;
var eCreateusers = 101000;
var eUpdateusers = 102000;
var eDeleteusers = 103000;
var eRetrieveByIDusers = 104000;
var eRetrieveWithWhereClauseusers = 105000;
var accounttypes = /** @class */ (function () {
    function accounttypes() {
    }
    return accounttypes;
}());
var admins = /** @class */ (function () {
    function admins() {
    }
    return admins;
}());
var clientprogramenlistments = /** @class */ (function () {
    function clientprogramenlistments() {
    }
    return clientprogramenlistments;
}());
var clientprogrameventenlistments = /** @class */ (function () {
    function clientprogrameventenlistments() {
    }
    return clientprogrameventenlistments;
}());
var clientrequests = /** @class */ (function () {
    function clientrequests() {
    }
    return clientrequests;
}());
var clients = /** @class */ (function () {
    function clients() {
    }
    return clients;
}());
var donorfundscommitments = /** @class */ (function () {
    function donorfundscommitments() {
    }
    return donorfundscommitments;
}());
var donors = /** @class */ (function () {
    function donors() {
    }
    return donors;
}());
var fileuploads = /** @class */ (function () {
    function fileuploads() {
    }
    return fileuploads;
}());
var happyclientpictures = /** @class */ (function () {
    function happyclientpictures() {
    }
    return happyclientpictures;
}());
var individualpictures = /** @class */ (function () {
    function individualpictures() {
    }
    return individualpictures;
}());
var individuals = /** @class */ (function () {
    function individuals() {
    }
    return individuals;
}());
var messagerecipients = /** @class */ (function () {
    function messagerecipients() {
    }
    return messagerecipients;
}());
var messages = /** @class */ (function () {
    function messages() {
    }
    return messages;
}());
var moduleviews = /** @class */ (function () {
    function moduleviews() {
    }
    return moduleviews;
}());
var notificationsettings = /** @class */ (function () {
    function notificationsettings() {
    }
    return notificationsettings;
}());
var programdonorcommitments = /** @class */ (function () {
    function programdonorcommitments() {
    }
    return programdonorcommitments;
}());
var programevents = /** @class */ (function () {
    function programevents() {
    }
    return programevents;
}());
var programs = /** @class */ (function () {
    function programs() {
    }
    return programs;
}());
var settings = /** @class */ (function () {
    function settings() {
    }
    return settings;
}());
var users = /** @class */ (function () {
    function users() {
    }
    return users;
}());
var ProtocolPacket = /** @class */ (function () {
    function ProtocolPacket() {
    }
    return ProtocolPacket;
}());
var AjajClient = /** @class */ (function () {
    function AjajClient() {
    }
    AjajClient.prototype.postAjaj = function (objectToSend, cb) {
        $.ajax({
            type: "POST",
            url: 'api/Main',
            data: JSON.stringify(objectToSend),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            timeout: 500000,
            success: function (data) {
                cb(data);
            }
        });
    };
    ;
    AjajClient.prototype.StartTransaction = function (cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eStartTransaction;
        objectToSend.payload = null;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.CommitTransaction = function (transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCommittTransaction;
        objectToSend.payload = transactionId;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RollbackTransaction = function (transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRollbackTransaction;
        objectToSend.payload = transactionId;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createaccounttypes = function (newaccounttypes, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateaccounttypes;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newaccounttypes;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifyaccounttypes = function (existingaccounttypes, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateaccounttypes;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingaccounttypes;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deleteaccounttypes = function (existingaccounttypes, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteaccounttypes;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingaccounttypes;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDaccounttypes = function (existingaccounttypesid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDaccounttypes;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingaccounttypesid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClauseaccounttypes = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseaccounttypes;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createadmins = function (newadmins, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateadmins;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newadmins;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifyadmins = function (existingadmins, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateadmins;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingadmins;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deleteadmins = function (existingadmins, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteadmins;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingadmins;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDadmins = function (existingadminsid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDadmins;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingadminsid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClauseadmins = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseadmins;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createclientprogramenlistments = function (newclientprogramenlistments, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateclientprogramenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newclientprogramenlistments;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifyclientprogramenlistments = function (existingclientprogramenlistments, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateclientprogramenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientprogramenlistments;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deleteclientprogramenlistments = function (existingclientprogramenlistments, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteclientprogramenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientprogramenlistments;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDclientprogramenlistments = function (existingclientprogramenlistmentsid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDclientprogramenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientprogramenlistmentsid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClauseclientprogramenlistments = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseclientprogramenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createclientprogrameventenlistments = function (newclientprogrameventenlistments, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateclientprogrameventenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newclientprogrameventenlistments;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifyclientprogrameventenlistments = function (existingclientprogrameventenlistments, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateclientprogrameventenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientprogrameventenlistments;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deleteclientprogrameventenlistments = function (existingclientprogrameventenlistments, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteclientprogrameventenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientprogrameventenlistments;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDclientprogrameventenlistments = function (existingclientprogrameventenlistmentsid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDclientprogrameventenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientprogrameventenlistmentsid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClauseclientprogrameventenlistments = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseclientprogrameventenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createclientrequests = function (newclientrequests, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateclientrequests;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newclientrequests;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifyclientrequests = function (existingclientrequests, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateclientrequests;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientrequests;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deleteclientrequests = function (existingclientrequests, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteclientrequests;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientrequests;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDclientrequests = function (existingclientrequestsid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDclientrequests;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientrequestsid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClauseclientrequests = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseclientrequests;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createclients = function (newclients, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateclients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newclients;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifyclients = function (existingclients, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateclients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclients;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deleteclients = function (existingclients, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteclients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclients;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDclients = function (existingclientsid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDclients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientsid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClauseclients = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseclients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createdonorfundscommitments = function (newdonorfundscommitments, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatedonorfundscommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newdonorfundscommitments;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifydonorfundscommitments = function (existingdonorfundscommitments, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatedonorfundscommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingdonorfundscommitments;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deletedonorfundscommitments = function (existingdonorfundscommitments, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletedonorfundscommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingdonorfundscommitments;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDdonorfundscommitments = function (existingdonorfundscommitmentsid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDdonorfundscommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingdonorfundscommitmentsid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClausedonorfundscommitments = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausedonorfundscommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createdonors = function (newdonors, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatedonors;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newdonors;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifydonors = function (existingdonors, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatedonors;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingdonors;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deletedonors = function (existingdonors, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletedonors;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingdonors;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDdonors = function (existingdonorsid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDdonors;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingdonorsid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClausedonors = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausedonors;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createfileuploads = function (newfileuploads, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatefileuploads;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newfileuploads;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifyfileuploads = function (existingfileuploads, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatefileuploads;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingfileuploads;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deletefileuploads = function (existingfileuploads, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletefileuploads;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingfileuploads;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDfileuploads = function (existingfileuploadsid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDfileuploads;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingfileuploadsid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClausefileuploads = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausefileuploads;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createhappyclientpictures = function (newhappyclientpictures, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatehappyclientpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newhappyclientpictures;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifyhappyclientpictures = function (existinghappyclientpictures, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatehappyclientpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existinghappyclientpictures;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deletehappyclientpictures = function (existinghappyclientpictures, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletehappyclientpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existinghappyclientpictures;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDhappyclientpictures = function (existinghappyclientpicturesid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDhappyclientpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existinghappyclientpicturesid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClausehappyclientpictures = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausehappyclientpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createindividualpictures = function (newindividualpictures, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateindividualpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newindividualpictures;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifyindividualpictures = function (existingindividualpictures, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateindividualpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingindividualpictures;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deleteindividualpictures = function (existingindividualpictures, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteindividualpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingindividualpictures;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDindividualpictures = function (existingindividualpicturesid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDindividualpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingindividualpicturesid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClauseindividualpictures = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseindividualpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createindividuals = function (newindividuals, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateindividuals;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newindividuals;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifyindividuals = function (existingindividuals, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateindividuals;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingindividuals;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deleteindividuals = function (existingindividuals, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteindividuals;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingindividuals;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDindividuals = function (existingindividualsid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDindividuals;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingindividualsid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClauseindividuals = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseindividuals;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createmessagerecipients = function (newmessagerecipients, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatemessagerecipients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newmessagerecipients;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifymessagerecipients = function (existingmessagerecipients, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatemessagerecipients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmessagerecipients;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deletemessagerecipients = function (existingmessagerecipients, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletemessagerecipients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmessagerecipients;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDmessagerecipients = function (existingmessagerecipientsid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDmessagerecipients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmessagerecipientsid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClausemessagerecipients = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausemessagerecipients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createmessages = function (newmessages, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatemessages;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newmessages;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifymessages = function (existingmessages, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatemessages;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmessages;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deletemessages = function (existingmessages, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletemessages;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmessages;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDmessages = function (existingmessagesid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDmessages;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmessagesid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClausemessages = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausemessages;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createmoduleviews = function (newmoduleviews, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatemoduleviews;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newmoduleviews;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifymoduleviews = function (existingmoduleviews, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatemoduleviews;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmoduleviews;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deletemoduleviews = function (existingmoduleviews, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletemoduleviews;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmoduleviews;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDmoduleviews = function (existingmoduleviewsid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDmoduleviews;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmoduleviewsid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClausemoduleviews = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausemoduleviews;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createnotificationsettings = function (newnotificationsettings, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatenotificationsettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newnotificationsettings;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifynotificationsettings = function (existingnotificationsettings, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatenotificationsettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingnotificationsettings;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deletenotificationsettings = function (existingnotificationsettings, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletenotificationsettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingnotificationsettings;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDnotificationsettings = function (existingnotificationsettingsid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDnotificationsettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingnotificationsettingsid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClausenotificationsettings = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausenotificationsettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createprogramdonorcommitments = function (newprogramdonorcommitments, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateprogramdonorcommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newprogramdonorcommitments;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifyprogramdonorcommitments = function (existingprogramdonorcommitments, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateprogramdonorcommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprogramdonorcommitments;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deleteprogramdonorcommitments = function (existingprogramdonorcommitments, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteprogramdonorcommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprogramdonorcommitments;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDprogramdonorcommitments = function (existingprogramdonorcommitmentsid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDprogramdonorcommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprogramdonorcommitmentsid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClauseprogramdonorcommitments = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseprogramdonorcommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createprogramevents = function (newprogramevents, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateprogramevents;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newprogramevents;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifyprogramevents = function (existingprogramevents, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateprogramevents;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprogramevents;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deleteprogramevents = function (existingprogramevents, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteprogramevents;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprogramevents;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDprogramevents = function (existingprogrameventsid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDprogramevents;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprogrameventsid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClauseprogramevents = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseprogramevents;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createprograms = function (newprograms, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateprograms;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newprograms;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifyprograms = function (existingprograms, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateprograms;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprograms;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deleteprograms = function (existingprograms, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteprograms;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprograms;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDprograms = function (existingprogramsid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDprograms;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprogramsid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClauseprograms = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseprograms;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createsettings = function (newsettings, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatesettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newsettings;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifysettings = function (existingsettings, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatesettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingsettings;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deletesettings = function (existingsettings, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletesettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingsettings;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDsettings = function (existingsettingsid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDsettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingsettingsid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClausesettings = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausesettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Createusers = function (newusers, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateusers;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newusers;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Modifyusers = function (existingusers, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateusers;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingusers;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.Deleteusers = function (existingusers, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteusers;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingusers;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveByIDusers = function (existingusersid, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDusers;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingusersid;
        this.postAjaj(objectToSend, cb);
    };
    AjajClient.prototype.RetrieveWithWhereClauseusers = function (WhereClause, transactionId, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseusers;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    };
    return AjajClient;
}());
