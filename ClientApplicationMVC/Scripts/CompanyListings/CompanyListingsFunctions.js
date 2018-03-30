/**
*   This function automatically runs when this javascript file is loaded.
*   This function sets the on click function of the send button to be the validate and send message function below.
*/
$(function () {
    $("#SendMessageButton").click(validateAndSendMessage);
});

/**
 * Ensures that the user has entered a valid message, then sends it.
 * If successful, the user will be forewarded to their message inbox
 * This function will fail to work until the Chat Service is implemented.
 */
function checkInputs() {

    var review = document.getElementById("review").value;
    var stars = document.getElementById("stars").value;

    if (review.length == 0 || stars.length == 0) {
        alert("Please make sure all fields are filled.");
        return false;
    }

    var starRegex = /^[1-5]{1}$/;

    if (stars.length != 1 || !starRegex.test(stars)) {
        alert("Please ensure that the value entered into the 'Star' textbook is an integer between 1 and 5");
        return false;
    }

    return true;
}