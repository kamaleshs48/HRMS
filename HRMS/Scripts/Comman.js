

function setCookie(cname, cvalue) {
    var d = new Date();
    d.setTime(d.getTime() + (1 * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
function DeleteCookie(cname) {
    //var expires = ";expires=Thu, 01 Jan 1970 00:00:01 GMT;";
    //document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    document.cookie = cname + '=;expires=Thu, 01 Jan 1970 00:00:01 GMT;path=/';
}
function deleteAllCookies() {
    var cookies = document.cookie.split(";");

    for (var i = 0; i < cookies.length; i++) {

        var cookie = cookies[i];
        var eqPos = cookie.indexOf("=");
        var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
        if (name != 'PageIndex') {
            document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT;path=/";
        }

    }
}
function ValidateEmail(Email) {
    var filter = /^\w+([\.\-+!#$%&'*/=?^_`{|}~]?\w+)*@\w+([-.]?\w+)*(\.\w{2,3})+$/

    //if it's valid email
    if (Email.match(filter)) {

        return true;
    }
        //if it's NOT valid
    else {

        return false;
    }
}

function isValidNhsNumber() {
    var isValid = false;
    var txtNhsNumber = $("#NHSNo").val();

    if (txtNhsNumber != "") {
        if (txtNhsNumber.match("^0")) {
            ShowAlertF('Please enter a valid NHS number.', 'NHSNo')
        }
        else {
            if (txtNhsNumber.length == 10) {
                var total = 0;
                var i = 0;
                for (i = 0; i <= 8; i++) {
                    var digit = txtNhsNumber.substr(i, 1);
                    var factor = 10 - i;
                    total += (digit * factor);
                }
                var checkDigit = (11 - (total % 11));
                if (checkDigit == 11) { checkDigit = 0; }
                if (checkDigit == txtNhsNumber.substr(9, 1)) { isValid = true; }
            }
            if (isValid == false) {
                ShowAlertF('Please enter a valid NHS number.', 'NHSNo')
            }
        }
    }
    else {
        ShowAlertF('Please enter NHS number.', 'NHSNo')
        //alert("Please enter NHS number.");
        $("#NHSNo").focus();
    }

    ////////Dob Validation

    var dateEntered = $("#DOB").val();
    var date = dateEntered.substring(0, 2);
    var month = dateEntered.substring(3, 5);
    var year = dateEntered.substring(6, 10);
    var dateToCompare = new Date(year, month - 1, date);
    var currentDate = new Date();
    console.log(dateToCompare.getDate());
    console.log(currentDate.getDate());


    //var dateObj = new Date();
    //var month = dateObj.getUTCMonth() + 1; //months from 1-12
    //var day = dateObj.getUTCDate();
    //var year = dateObj.getUTCFullYear();

    //newdate = year + "/" + month + "/" + day;
    //currentDate = newdate;


    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    var today = dd + '/' + mm + '/' + yyyy;
    console.log(Date.parse(dateToCompare));
    console.log(Date.parse(currentDate));
    if (Date.parse(dateToCompare) >= Date.parse(today)) {
        // alert("DOB must be lesser than current date.");
        ShowAlert('DOB must be lesser than current date.', 'DOB')
        //  $("#DOB").focus();
        return false;
    }

    return isValid;
}


function isValidNhsNumberByValue(NhsNumber) {
    var isValid = false;
    //  alert('NhsNumber-------' + NhsNumber);

    if (NhsNumber != "") {
        if (NhsNumber.match("^0")) {
            isValid = false;
        }
        else {
            if (NhsNumber.length == 10) {
                var total = 0;
                var i = 0;
                for (i = 0; i <= 8; i++) {
                    var digit = NhsNumber.substr(i, 1);
                    var factor = 10 - i;
                    total += (digit * factor);
                }
                var checkDigit = (11 - (total % 11));
                if (checkDigit == 11) { checkDigit = 0; }
                if (checkDigit == NhsNumber.substr(9, 1)) { isValid = true; }
            }

        }
    }
    else {
        return isValid;
    }


    return isValid;
}


/* For Ajax*/
function ShowLoder() {
    $("#AjaxPopUP").modal({
        backdrop: 'static'
    });
}
function HideLoader() {
    // $("#LodingModel").modal('hide');
    $("#AjaxPopUP").modal('hide');
}

$(document).ajaxSend(function () {
    //$( ".log" ).text( "Triggered ajaxSend handler." );
    $("#AjaxPopUP").modal({
        backdrop: 'static'
    });
});
$(document).ajaxSuccess(function () {
    $("#AjaxPopUP").modal('hide');
});
$(document).bind("contextmenu", function (e) {
    return true;
});
function RemoveActiveClass() {
    $("a").blur();
}

function IsNumeric(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;

}
/*Focus */
function SetFocus(c) {
    try {
        var SearchInput = $(c);
        SearchInput.val(SearchInput.val());
        var strLength = SearchInput.val().length;
        SearchInput.focus();
        SearchInput[0].setSelectionRange(strLength, strLength);
    }
    catch (ee) {

    }
}


$(document).ready(function () {

    // $('input').attr('autocomplete', 'off');
    $("input[type='text']").focus(function () {

    });
})

/* KAMLESH SINGH SWAL FUNCTION */

function ShowAlertF(Message, FocuseID) {
    console.log(FocuseID);
    swal({
        title: "",
        text: Message,
        allowOutsideClick: false,

    }).then(function (result) {
        $('#' + FocuseID).focus();
    }).catch(swal.noop);
}
function ShowAlert(Message) {
    swal({
        title: "",
        text: Message,
        allowOutsideClick: false,

    }).then(function (result) {
        $('select:visible,input:visible, .focus:visible, textarea:visible').eq(0).focus();
    }).catch(swal.noop);

}
function ShowAlertWithBack(Message) {
    swal({
        title: "",
        text: Message,
        allowOutsideClick: false,
    }).then(
function (result) {
    setTimeout('GoToBack()', 50);
}).catch(swal.noop);
}
function ShowAlertWithFun(Message, fun) {
    swal({
        title: "",
        text: Message,
        allowOutsideClick: false,

    }).then(
function (result) {
    fun();
    return false;
}).catch(swal.noop);
}

