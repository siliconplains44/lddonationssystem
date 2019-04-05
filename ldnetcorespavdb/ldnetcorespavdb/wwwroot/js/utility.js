/**
 * Created by Allan on 1/27/2015.
 */

function loadjscssfile(filename, filetype, cb){
    if (filetype==="js"){ //if filename is a external JavaScript file
        var fileref=document.createElement('script');
        fileref.setAttribute("type","text/javascript");
        fileref.setAttribute("src", filename);
    }
    else if (filetype==="css"){ //if filename is an external CSS file
        fileref=document.createElement("link");
        fileref.setAttribute("rel", "stylesheet");
        fileref.setAttribute("type", "text/css");
        fileref.setAttribute("href", filename);
    }
    if (typeof fileref!=="undefined") {

        fileref.onreadystatechange = fileref.onload = function() {
            var state = fileref.readyState;
            if (!cb.done && (!state || /loaded|complete/.test(state))) {
                cb.done = true;
                cb();
            }
        };

        document.getElementsByTagName("head")[0].appendChild(fileref);
    }
};

var allocObjectToSend = function() {
    var objectToSend = {};
    objectToSend.payload = {};
    return objectToSend;
};

var postAjaj = function(protocol, objectToSend, cb) {

    $.ajax({
        type: "POST",
        url: protocol,
        data: JSON.stringify(objectToSend),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 500000,
        success: function(data) {
            cb(data);
        },
        failure: function(errMsg) {
            alert(errMsg);
        }
    });
};

var runProtocol = function (endpoint, protocol, objectToSend, cb) {
    objectToSend.protocol = protocol;

    $.ajax({
        type: "POST",
        url: endpoint,
        data: JSON.stringify(objectToSend),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        timeout: 500000,
        success: function (data) {
            returnPayload = JSON.parse(data);
            cb(returnPayload);
        },
        failure: function (errMsg) {
            alert(errMsg);
        }
    });
};

var runGen = function (protocol, objectToSend, cb) {
    runProtocol("api/Main", protocol, objectToSend, cb);
};

var runCustom = function (protocol, objectToSend, cb) {
    runProtocol("api/Custom", protocol, objectToSend, cb);
};

function validateEmail(email) {
    var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    return re.test(email);
};

function getBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
    });
};

