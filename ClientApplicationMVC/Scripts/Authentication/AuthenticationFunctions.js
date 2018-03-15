// To be completed by students in milestone 2

function checkInputs() {
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    var address = document.getElementById("address").value;
    var phonenumber = document.getElementById("phonenumber").value;
    var email = document.getElementById("email").value;

    // Checks to make sure that none of the fields are empty 
    if (username.length == 0 || password.length == 0 || address.length == 0 || phonenumber.length == 0 || email.length == 0) {
        alert("Please make sure all fields are filled");
        return false;
    }

    // Ensures that the password has at least one capital letter and at least one number
    if (!(/\d/.test(password)) || !(/[A-Z]/.test(password))) {
        alert("Please make sure your password contains at least one uppercase letter and one number");
        return false;
    }

    // Checks to make sure the phone number is formatted properly
    var phoneRegex = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
    if (!phoneRegex.test(phonenumber)) {
        alert("Please enter your number in the format 'XXX-XXX-XXXX' or 'XXX XXX XXXX'");
        return false;
    }

    // Checks to make sure that the email follows the proper format
    var emailRegex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (!emailRegex.test(email)) {
        alert("Please enter a valid email address");
        return false;
    }

    // Return true if all of the aforementioned conditions are satisifed
    return true;
}