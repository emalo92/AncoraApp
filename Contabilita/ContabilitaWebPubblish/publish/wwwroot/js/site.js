// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function showLoading() {
    document.getElementById("divPageLoading").style.display = "block";
}
function hideLoading() {
    document.getElementById("divPageLoading").style.display = "none";
}

/*Per disabilitare il loading commentare questa parte */
//*******************************************************
$(document).ready(function () {
    $("form").submit(function () {
        var error = $(".field-validation-error");
        var formAction = $(this).attr("action");
        if (error.length === 0 && formAction.indexOf('ExportTable') === -1) {
            showLoading();
        }

    });
    $("a").click(function (e) {
        var href = $(this).attr('href');
        var str = e.currentTarget.innerText;

        if (href !== '#' && href.indexOf('#') !== 0 && href.indexOf('ExportTable') === -1 && href !== 'javascript:void(0)') {
            showLoading();
        }
    });
    $(document).ajaxSend(function () {
        showLoading();
    });
    $(document).ajaxComplete(function () {
        hideLoading();
    })
})
//**************************************************************
//*********** EVENTS ***************
const openPopupCustomEvent = document.createEvent('Event');
const closedPopupCustomEvent = document.createEvent('Event');

//Metodo per notificare l'apertura del popup
function dispatchOpenPopupCustomEvent(dataSender) {
    openPopupCustomEvent.initEvent('openPopup_CustomEvent', true, true);
    openPopupCustomEvent.Sender = dataSender;
    document.dispatchEvent(openPopupCustomEvent);
}

//Metodo per notificare la chiusura del popup
function dispatchClosedPopupCustomEvent(dataSender) {
    closedPopupCustomEvent.initEvent('closedPopup_CustomEvent', true, true);
    closedPopupCustomEvent.Sender = dataSender;
    document.dispatchEvent(closedPopupCustomEvent);
}

function dispatchClosedPopupCustomEventWithCallback(dataSender, callbackMethod, data) {
    closedPopupCustomEvent.initEvent('closedPopup_CustomEvent', true, true);
    closedPopupCustomEvent.Sender = dataSender;
    callbackMethod(data);
    document.dispatchEvent(closedPopupCustomEvent);
}

function GetSafeDataValue(data) {
    var dataSafe = data.substring(6, 10) + "-" + data.substring(3, 5) + "-" + data.substring(0, 2);
    return dataSafe;
}