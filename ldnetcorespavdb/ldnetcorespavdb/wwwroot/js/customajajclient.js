/// <reference path ="../../node_modules/@types/jquery/index.d.ts"/>
// enumerations
var RegisterAccountByRegistrationId = 1000;
var CreateUserAccount = 2000;
var LoginSystemUser = 3000;
var GetSystemUserAccountType = 4000;
var SendPasswordResetEmail = 5000;
var UpdateUserPassword = 6000;
var CreateModuleView = 7000;
var RetrieveIndividualInformation = 8000;
var SaveIndividualInformation = 9000;
var RetrieveAllPrograms = 10000;
var RetrieveProgramEventsByProgram = 11000;
var RetrieveAllAdministrators = 12000;
var AddAdministrator = 13000;
var DeleteAdministrator = 14000;
var RetrieveAllClients = 15000;
var AddClient = 16000;
var ModifyClient = 17000;
var DeleteClient = 18000;
var RetrieveClientInformationByClientID = 19000;
var SaveClientInformation = 20000;
var RetrieveClientsNotInExclusionList = 21000;
var RetrieveClientFatherByClientID = 22000;
var SaveClientFather = 23000;
var RetrieveClientMotherByClientId = 24000;
var SaveClientMother = 25000;
var DoesClientHaveSystemUserAccount = 26000;
var CreateClientSystemUserAccount = 27000;
var RetrieveListOfProgramsClientIsEnlistedIn = 28000;
var RetrieveListOfProgramEventsClientIsEnlistedIn = 29000;
var EnlistClientInProgram = 30000;
var UnenlistClientInProgram = 31000;
var EnlistClientInProgramEvent = 32000;
var UnenlistClientInProgramEvent = 33000;
var RetrieveDonorAssignedClientRequests = 34000;
var RetrieveAllDonors = 35000;
var AddDonor = 36000;
var ModifyDonor = 37000;
var DeleteDonor = 38000;
var RetrieveListOfProgramsDonorIsEnlistedIn = 39000;
var EnlistDonorInProgram = 40000;
var UnenlistDonorFromProgram = 41000;
var RetrieveClientRequestsByProgramId = 42000;
var SendInternalMessage = 43000;
var RetrieveAllInboxMessages = 44000;
var RetrieveDonorInformationByDonorID = 46000;
var SaveDonorInformation = 48000;
var DeactivateClientSystemUserAccount = 50000;
var RetrieveClientProgramsNotEnlistedIn = 52000;
var RetrieveClientProgramEventsNotEnlistedIn = 54000;
var RetrieveDonorProgramsNotEnlistedIn = 56000;
var RetrieveDonorProgramEventsNotEnlistedIn = 58000;
var RetrieveHappyClientPhotos = 60000;
var AddHappyClientPhoto = 62000;
var DeleteHappyClientPhoto = 64000;
var DeactivateClient = 66000;
var RetrieveAllNonCommittedClientRequests = 68000;
var RetrieveAllCommittedClientRequestsByDonorId = 70000;
var DeactivateDonor = 72000;
var SetMessageRecipientMessageRead = 74000;
var RetrieveAllAvailableMessageRecipientsWithExclusionList = 76000;
var RetrieveMessageRecipientByAccountIDAndAccountTypeID = 78000;
var RetrieveNotificationSettingsByAccountIdAndAccountTypeId = 80000;
var SaveNotificationSettingsByAccountIdAndAccountTypeId = 82000;
var RetrieveAllChildrenByParentClientID = 84000;
var RetrieveAllProgramsChildIsEnlistedIn = 86000;
var RetrieveAllNonDonorCommittedClientRequestsByClientIdAndProgramId = 88000;
var RetrieveAllDonorCommittedClientRequestsByClientIdAndProgramId = 90000;
var RetrieveAllNonDonorCommittedClientRequestsByProgramID = 92000;
var RetrieveAllclientRequestsCommittedToByDonorID = 94000;
var RetrieveDisplayAccountByLoggedInUserID = 96000;
var RetrieveWelcomeMessageByLoggedInUserID = 98000;
var displayadministrator = /** @class */ (function () {
    function displayadministrator() {
    }
    return displayadministrator;
}());
var displayclient = /** @class */ (function () {
    function displayclient() {
    }
    return displayclient;
}());
var AjajClientCustom = /** @class */ (function () {
    function AjajClientCustom() {
    }
    AjajClientCustom.prototype.postAjaj = function (objectToSend, cb) {
        $.ajax({
            type: "POST",
            url: 'api/Custom',
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
    return AjajClientCustom;
}());
