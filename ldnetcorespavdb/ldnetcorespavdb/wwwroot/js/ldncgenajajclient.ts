
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

class accounttypes
{
    AccountTypeID: number; 
    Name: string; 
}

class admins
{
    AdminID: number; 
    UserID: number; 
    IndividualID: number; 
    IsDeleted: number; 
}

class clientprogramenlistments
{
    RecipientProgramEnlistmentID: number; 
    ClientID: number; 
    ProgramID: number; 
}

class clientprogrameventenlistments
{
    ClientProgramEventEnlistementID: number; 
    ClientID: number; 
    ProgramEventID: number; 
}

class clientrequests
{
    ClientRequestID: number; 
    ClientID: number; 
    RequestInformation: string; 
    ProgramID: number; 
}

class clients
{
    ClientID: number; 
    UserID: number; 
    IndividualID: number; 
    IsDeleted: number; 
}

class donorfundscommitments
{
    DonorFundsCommitmentID: number; 
    Occured: Date; 
    DonorID: number; 
    Amount: number; 
    Received: Date; 
}

class donors
{
    DonorID: number; 
    UserID: number; 
    IndividualID: number; 
    IsDeleted: number; 
}

class fileuploads
{
    FileUploadID: number; 
    Filename: string; 
    Size: number; 
    Created: Date; 
    Data: any; 
}

class happyclientpictures
{
    HappyClientPictureID: number; 
    ProgramID: number; 
    RecipientClientID: number; 
    FileUploadID: number; 
}

class individualpictures
{
    IndividualPictureID: number; 
    IndividualID: number; 
    FileUploadID: number; 
}

class individuals
{
    IndividualID: number; 
    LastName: string; 
    MiddleName: string; 
    FirstName: string; 
    FatherIndividualID: number; 
    MotherIndividualID: number; 
    Birthdate: Date; 
    MobilePhoneNumber: string; 
    HomePhoneNumber: string; 
    AddressLine1: string; 
    AddressLine2: string; 
    City: string; 
    State: string; 
    Zip: string; 
}

class messagerecipients
{
    MessageRecipientID: number; 
    MessageID: number; 
    AccountID: number; 
    AccountTypeID: number; 
    MessageRead: Date; 
}

class messages
{
    MessageID: number; 
    FromAccountID: number; 
    FromAccountTypeID: number; 
    MessageSentDateTime: Date; 
    Subject: string; 
    Body: string; 
}

class moduleviews
{
    ModuleViewID: number; 
    Name: string; 
    Occurred: Date; 
    LoggedInSecurityUserID: number; 
}

class notificationsettings
{
    NotificationSettingID: number; 
    AccountID: number; 
    AccountTypeID: number; 
    EnableEmailMessages: number; 
    EnableSMSMessages: number; 
}

class programdonorcommitments
{
    ProgramDonorCommitmentID: number; 
    DonorID: number; 
    CommitmentDateTime: Date; 
    ClientRequestID: number; 
    ReceivedAtCollectionPoint: number; 
    DistributedToRecipient: number; 
}

class programevents
{
    ProgramEventID: number; 
    ProgramID: number; 
    IsSingleDate: number; 
    FromDate: Date; 
    ToDate: Date; 
    Description: string; 
    Name: string; 
}

class programs
{
    ProgramID: number; 
    Name: string; 
    Description: string; 
    IsPublished: number; 
    Year: number; 
}

class settings
{
    SettingID: number; 
    Name: string; 
    Value: string; 
}

class users
{
    UserID: number; 
    Email: string; 
    Password: string; 
    IsActive: number; 
    RegistrationCode: string; 
    PasswordResetCode: string; 
    AccountTypeID: number; 
}


class ProtocolPacket
{
    protocol: number;
    transactionid: string;
    payload: any;
}

class AjajClient
{

    private postAjaj(objectToSend, cb)
    {

        $.ajax({
                type: "POST",
                url: 'api/Main',
                data: JSON.stringify(objectToSend),
                contentType: "application/json; charset=utf-8", 
                dataType: "json",
                timeout: 500000,
                success: function(data) {
                     cb(data)
                }
        });
    };

    StartTransaction(cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eStartTransaction;
        objectToSend.payload = null;
        this.postAjaj(objectToSend, cb);
    }

    CommitTransaction(transactionId: string, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCommittTransaction;
        objectToSend.payload = transactionId;
        this.postAjaj(objectToSend, cb);
    }

    RollbackTransaction(transactionId: string, cb) {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRollbackTransaction;
        objectToSend.payload = transactionId;
        this.postAjaj(objectToSend, cb);
    }

    Createaccounttypes(newaccounttypes: accounttypes, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateaccounttypes;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newaccounttypes;
        this.postAjaj(objectToSend, cb);
    }

    Modifyaccounttypes(existingaccounttypes: accounttypes, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateaccounttypes;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingaccounttypes;
        this.postAjaj(objectToSend, cb);
    }

    Deleteaccounttypes(existingaccounttypes: accounttypes, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteaccounttypes;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingaccounttypes;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDaccounttypes(existingaccounttypesid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDaccounttypes;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingaccounttypesid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClauseaccounttypes(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseaccounttypes;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createadmins(newadmins: admins, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateadmins;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newadmins;
        this.postAjaj(objectToSend, cb);
    }

    Modifyadmins(existingadmins: admins, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateadmins;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingadmins;
        this.postAjaj(objectToSend, cb);
    }

    Deleteadmins(existingadmins: admins, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteadmins;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingadmins;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDadmins(existingadminsid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDadmins;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingadminsid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClauseadmins(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseadmins;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createclientprogramenlistments(newclientprogramenlistments: clientprogramenlistments, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateclientprogramenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newclientprogramenlistments;
        this.postAjaj(objectToSend, cb);
    }

    Modifyclientprogramenlistments(existingclientprogramenlistments: clientprogramenlistments, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateclientprogramenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientprogramenlistments;
        this.postAjaj(objectToSend, cb);
    }

    Deleteclientprogramenlistments(existingclientprogramenlistments: clientprogramenlistments, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteclientprogramenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientprogramenlistments;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDclientprogramenlistments(existingclientprogramenlistmentsid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDclientprogramenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientprogramenlistmentsid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClauseclientprogramenlistments(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseclientprogramenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createclientprogrameventenlistments(newclientprogrameventenlistments: clientprogrameventenlistments, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateclientprogrameventenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newclientprogrameventenlistments;
        this.postAjaj(objectToSend, cb);
    }

    Modifyclientprogrameventenlistments(existingclientprogrameventenlistments: clientprogrameventenlistments, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateclientprogrameventenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientprogrameventenlistments;
        this.postAjaj(objectToSend, cb);
    }

    Deleteclientprogrameventenlistments(existingclientprogrameventenlistments: clientprogrameventenlistments, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteclientprogrameventenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientprogrameventenlistments;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDclientprogrameventenlistments(existingclientprogrameventenlistmentsid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDclientprogrameventenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientprogrameventenlistmentsid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClauseclientprogrameventenlistments(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseclientprogrameventenlistments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createclientrequests(newclientrequests: clientrequests, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateclientrequests;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newclientrequests;
        this.postAjaj(objectToSend, cb);
    }

    Modifyclientrequests(existingclientrequests: clientrequests, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateclientrequests;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientrequests;
        this.postAjaj(objectToSend, cb);
    }

    Deleteclientrequests(existingclientrequests: clientrequests, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteclientrequests;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientrequests;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDclientrequests(existingclientrequestsid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDclientrequests;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientrequestsid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClauseclientrequests(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseclientrequests;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createclients(newclients: clients, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateclients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newclients;
        this.postAjaj(objectToSend, cb);
    }

    Modifyclients(existingclients: clients, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateclients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclients;
        this.postAjaj(objectToSend, cb);
    }

    Deleteclients(existingclients: clients, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteclients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclients;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDclients(existingclientsid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDclients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingclientsid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClauseclients(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseclients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createdonorfundscommitments(newdonorfundscommitments: donorfundscommitments, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatedonorfundscommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newdonorfundscommitments;
        this.postAjaj(objectToSend, cb);
    }

    Modifydonorfundscommitments(existingdonorfundscommitments: donorfundscommitments, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatedonorfundscommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingdonorfundscommitments;
        this.postAjaj(objectToSend, cb);
    }

    Deletedonorfundscommitments(existingdonorfundscommitments: donorfundscommitments, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletedonorfundscommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingdonorfundscommitments;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDdonorfundscommitments(existingdonorfundscommitmentsid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDdonorfundscommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingdonorfundscommitmentsid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClausedonorfundscommitments(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausedonorfundscommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createdonors(newdonors: donors, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatedonors;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newdonors;
        this.postAjaj(objectToSend, cb);
    }

    Modifydonors(existingdonors: donors, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatedonors;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingdonors;
        this.postAjaj(objectToSend, cb);
    }

    Deletedonors(existingdonors: donors, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletedonors;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingdonors;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDdonors(existingdonorsid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDdonors;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingdonorsid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClausedonors(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausedonors;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createfileuploads(newfileuploads: fileuploads, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatefileuploads;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newfileuploads;
        this.postAjaj(objectToSend, cb);
    }

    Modifyfileuploads(existingfileuploads: fileuploads, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatefileuploads;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingfileuploads;
        this.postAjaj(objectToSend, cb);
    }

    Deletefileuploads(existingfileuploads: fileuploads, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletefileuploads;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingfileuploads;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDfileuploads(existingfileuploadsid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDfileuploads;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingfileuploadsid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClausefileuploads(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausefileuploads;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createhappyclientpictures(newhappyclientpictures: happyclientpictures, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatehappyclientpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newhappyclientpictures;
        this.postAjaj(objectToSend, cb);
    }

    Modifyhappyclientpictures(existinghappyclientpictures: happyclientpictures, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatehappyclientpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existinghappyclientpictures;
        this.postAjaj(objectToSend, cb);
    }

    Deletehappyclientpictures(existinghappyclientpictures: happyclientpictures, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletehappyclientpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existinghappyclientpictures;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDhappyclientpictures(existinghappyclientpicturesid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDhappyclientpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existinghappyclientpicturesid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClausehappyclientpictures(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausehappyclientpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createindividualpictures(newindividualpictures: individualpictures, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateindividualpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newindividualpictures;
        this.postAjaj(objectToSend, cb);
    }

    Modifyindividualpictures(existingindividualpictures: individualpictures, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateindividualpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingindividualpictures;
        this.postAjaj(objectToSend, cb);
    }

    Deleteindividualpictures(existingindividualpictures: individualpictures, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteindividualpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingindividualpictures;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDindividualpictures(existingindividualpicturesid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDindividualpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingindividualpicturesid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClauseindividualpictures(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseindividualpictures;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createindividuals(newindividuals: individuals, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateindividuals;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newindividuals;
        this.postAjaj(objectToSend, cb);
    }

    Modifyindividuals(existingindividuals: individuals, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateindividuals;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingindividuals;
        this.postAjaj(objectToSend, cb);
    }

    Deleteindividuals(existingindividuals: individuals, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteindividuals;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingindividuals;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDindividuals(existingindividualsid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDindividuals;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingindividualsid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClauseindividuals(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseindividuals;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createmessagerecipients(newmessagerecipients: messagerecipients, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatemessagerecipients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newmessagerecipients;
        this.postAjaj(objectToSend, cb);
    }

    Modifymessagerecipients(existingmessagerecipients: messagerecipients, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatemessagerecipients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmessagerecipients;
        this.postAjaj(objectToSend, cb);
    }

    Deletemessagerecipients(existingmessagerecipients: messagerecipients, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletemessagerecipients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmessagerecipients;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDmessagerecipients(existingmessagerecipientsid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDmessagerecipients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmessagerecipientsid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClausemessagerecipients(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausemessagerecipients;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createmessages(newmessages: messages, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatemessages;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newmessages;
        this.postAjaj(objectToSend, cb);
    }

    Modifymessages(existingmessages: messages, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatemessages;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmessages;
        this.postAjaj(objectToSend, cb);
    }

    Deletemessages(existingmessages: messages, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletemessages;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmessages;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDmessages(existingmessagesid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDmessages;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmessagesid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClausemessages(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausemessages;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createmoduleviews(newmoduleviews: moduleviews, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatemoduleviews;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newmoduleviews;
        this.postAjaj(objectToSend, cb);
    }

    Modifymoduleviews(existingmoduleviews: moduleviews, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatemoduleviews;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmoduleviews;
        this.postAjaj(objectToSend, cb);
    }

    Deletemoduleviews(existingmoduleviews: moduleviews, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletemoduleviews;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmoduleviews;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDmoduleviews(existingmoduleviewsid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDmoduleviews;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingmoduleviewsid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClausemoduleviews(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausemoduleviews;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createnotificationsettings(newnotificationsettings: notificationsettings, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatenotificationsettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newnotificationsettings;
        this.postAjaj(objectToSend, cb);
    }

    Modifynotificationsettings(existingnotificationsettings: notificationsettings, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatenotificationsettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingnotificationsettings;
        this.postAjaj(objectToSend, cb);
    }

    Deletenotificationsettings(existingnotificationsettings: notificationsettings, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletenotificationsettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingnotificationsettings;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDnotificationsettings(existingnotificationsettingsid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDnotificationsettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingnotificationsettingsid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClausenotificationsettings(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausenotificationsettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createprogramdonorcommitments(newprogramdonorcommitments: programdonorcommitments, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateprogramdonorcommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newprogramdonorcommitments;
        this.postAjaj(objectToSend, cb);
    }

    Modifyprogramdonorcommitments(existingprogramdonorcommitments: programdonorcommitments, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateprogramdonorcommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprogramdonorcommitments;
        this.postAjaj(objectToSend, cb);
    }

    Deleteprogramdonorcommitments(existingprogramdonorcommitments: programdonorcommitments, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteprogramdonorcommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprogramdonorcommitments;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDprogramdonorcommitments(existingprogramdonorcommitmentsid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDprogramdonorcommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprogramdonorcommitmentsid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClauseprogramdonorcommitments(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseprogramdonorcommitments;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createprogramevents(newprogramevents: programevents, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateprogramevents;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newprogramevents;
        this.postAjaj(objectToSend, cb);
    }

    Modifyprogramevents(existingprogramevents: programevents, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateprogramevents;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprogramevents;
        this.postAjaj(objectToSend, cb);
    }

    Deleteprogramevents(existingprogramevents: programevents, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteprogramevents;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprogramevents;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDprogramevents(existingprogrameventsid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDprogramevents;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprogrameventsid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClauseprogramevents(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseprogramevents;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createprograms(newprograms: programs, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateprograms;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newprograms;
        this.postAjaj(objectToSend, cb);
    }

    Modifyprograms(existingprograms: programs, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateprograms;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprograms;
        this.postAjaj(objectToSend, cb);
    }

    Deleteprograms(existingprograms: programs, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteprograms;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprograms;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDprograms(existingprogramsid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDprograms;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingprogramsid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClauseprograms(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseprograms;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createsettings(newsettings: settings, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreatesettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newsettings;
        this.postAjaj(objectToSend, cb);
    }

    Modifysettings(existingsettings: settings, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdatesettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingsettings;
        this.postAjaj(objectToSend, cb);
    }

    Deletesettings(existingsettings: settings, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeletesettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingsettings;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDsettings(existingsettingsid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDsettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingsettingsid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClausesettings(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClausesettings;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

    Createusers(newusers: users, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eCreateusers;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = newusers;
        this.postAjaj(objectToSend, cb);
    }

    Modifyusers(existingusers: users, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eUpdateusers;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingusers;
        this.postAjaj(objectToSend, cb);
    }

    Deleteusers(existingusers: users, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eDeleteusers;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingusers;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveByIDusers(existingusersid: Number, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveByIDusers;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = existingusersid;
        this.postAjaj(objectToSend, cb);
    }

    RetrieveWithWhereClauseusers(WhereClause: string, transactionId: string, cb)
    {
        var objectToSend = new ProtocolPacket();
        objectToSend.protocol = eRetrieveWithWhereClauseusers;
        objectToSend.transactionid = transactionId;
        objectToSend.payload = WhereClause;
        this.postAjaj(objectToSend, cb);
    }

}
