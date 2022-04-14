// textarea field ID
var tagID = "#lined";
// -----------------------------------------------------------------------------

function getCaretStart() {

    // Get start of the selection {Start of selection}.
    return parseInt($(tagID)[0].selectionStart);
}

function getCaretEnd() {

    // Get end of the selection {End of selection}.
    return parseInt($(tagID)[0].selectionEnd);
}

function setTextareaValue(value) {

    $(tagID).val(value);
}

function getSelectionFirstLineNumber(start) {

    return parseInt($(tagID)[0].value.substr(0, start).split("\n").length);
}

function getSelectionLastLineNumber(end) {

    return parseInt($(tagID)[0].value.substr(0, end).split("\n").length);
}

function getLengthBeforeFirstLine(lineNumber) {

    let sumOfLinesLengths = 0;
    let lines = $(tagID)[0].value.split("\n");

    for (i = 0; i < lineNumber; i++) {

        if (i == lineNumber - 1)
            return sumOfLinesLengths;

        sumOfLinesLengths += lines[i].length + 1; // +1 for \n
    }
}

function getLengthBetweenTwoLines(firstlineNumber, lastlineNumber) {

    let sumOfLinesLengths = 0;
    let lines = $(tagID)[0].value.split("\n");

    for (i = firstlineNumber - 1; i <= lastlineNumber - 1; i++)
        sumOfLinesLengths += lines[i].length + 1; // +1 for \n

    return sumOfLinesLengths;
}

function getNumOfRows() {

    return parseInt($(tagID)[0].value.split("\n").length);
}

function getStartingIndexOfPhrase(phrase) {

    return $(tagID)[0].value.indexOf(phrase);
}

function jumpToLine(lineNumber) {

    var lineHeight = $(tagID)[0].scrollHeight / getNumOfRows();

    var jump = (lineNumber - 1) * lineHeight;

    $(tagID).scrollTop(jump);
}

function moveCaretTo(index) {

    $(tagID)[0].selectionStart = index;
    $(tagID)[0].selectionEnd = index;

    highlightSelectedLine();

    jumpToLine(getSelectedLine());
}