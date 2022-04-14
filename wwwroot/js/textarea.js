// textarea field ID
var tagID = "#lined";
// -----------------------------------------------------------------------------

$(function () {
    $(tagID).linedtextarea(
        { selectedLine: 1 }
    );
});

document.getElementById(tagID.substring(1)).addEventListener('keydown', function (e) {

    if (e.key == 'Tab') {

        e.preventDefault();
        var start = this.selectionStart;
        var end = this.selectionEnd;

        // set textarea value to: text before caret + tab + text after caret
        this.value = this.value.substring(0, start) +
            "\t" + this.value.substring(end);

        // put caret at right position again
        this.selectionStart = this.selectionEnd = start + 1;
    }
});

function getSelectedLine() {

    return parseInt($(tagID)[0].value.substr(0, $(tagID)[0].selectionStart).split("\n").length);
}

function highlightSelectedLine() {

    // Remove highlight from all lines
    $(".codelines").children(".lineno").removeClass("lineselect");
    
    // Highlight the selected line
    $(".codelines").children(".lineno").eq(getSelectedLine()-1).addClass("lineselect");
}