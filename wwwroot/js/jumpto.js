function performJump() {

    var phrase = $("#selectbox").val();;
    
    var scriptPath = "js/textarea-utility.js";

    $.getScript(scriptPath, function () {

        moveCaretTo(getStartingIndexOfPhrase(phrase));
    });

}