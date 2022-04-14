var scriptPath1 = "js/textarea-utility.js";
var scriptPath2 = "js/commentInfo.js";

$.getScript(scriptPath1, function () {

    $.getScript(scriptPath2, function () {

        $("#commentButton").click(function () {
            
            if (getSelectionFirstLineNumber(getCaretStart()) == getSelectionLastLineNumber(getCaretEnd()))
                performSingleLineComment($(tagID)[0].value);
            else
                performMultiLineComment($(tagID)[0].value);

        });

        function performSingleLineComment(textValue) { 

            var lengthBeforeLine = getLengthBeforeFirstLine(getSelectionFirstLineNumber(getCaretStart()));

            var firstPart = textValue.substring(0, lengthBeforeLine);

            var secondPart = textValue.substring(lengthBeforeLine);

            setTextareaValue(firstPart + LINE_COMMENT_NOTATION + secondPart);
        }

        function performMultiLineComment(textValue) {
        
            var lengthBeforeLine = getLengthBeforeFirstLine(getSelectionFirstLineNumber(getCaretStart()));
            var lengthAfterLine = getLengthBetweenTwoLines(getSelectionFirstLineNumber(getCaretStart()), getSelectionLastLineNumber(getCaretEnd())) + lengthBeforeLine - 1; // -1 for \n

            var firstPart = textValue.substring(0, lengthBeforeLine);
            var secondPart = textValue.substring(lengthBeforeLine, lengthAfterLine);
            var thirdPart = textValue.substring(lengthAfterLine);

            setTextareaValue(firstPart + MULTI_LINE_COMMENT_START + secondPart + MULTI_LINE_COMMENT_END + thirdPart);
        }
    });
});