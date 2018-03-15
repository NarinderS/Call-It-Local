// To be completed by students in milestone 2

function checkInputs() {

    var accountInfo = $('form').serialize();

    var splitString = accountInfo.split("&");

    if (splitString.length !== 6) {
        alert("Please make sure all fields are filled");
    }

    var userName = splitString[1];
    var pass = splitString[2];
    var address = splitString[3];
    var phoneNumber = splitString[4];
    var email = splitString[5];

    alert(email);
}