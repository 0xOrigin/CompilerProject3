var scriptPath1 = "js/textarea-utility.js";
var scriptPath2 = "js/commentInfo.js";

$.getScript(scriptPath1, function () {

    $.getScript(scriptPath2, function () {

        $("#uncommentButton").click(function () {

            if (getSelectionFirstLineNumber(getCaretStart()) == getSelectionLastLineNumber(getCaretEnd()))
                performSingleLineUncomment($(tagID)[0].value);
            else
                performMultiLineUncomment($(tagID)[0].value);

        });

        function performSingleLineUncomment(textValue){

            var lengthBeforeLine = getLengthBeforeFirstLine(getSelectionFirstLineNumber(getCaretStart()));

            var firstPart = textValue.substring(0, lengthBeforeLine);
            
            var commentPart = textValue.substring(lengthBeforeLine, (+lengthBeforeLine + +LINE_COMMENT_LENGTH));

            var secondPart = textValue.substring(+lengthBeforeLine + +LINE_COMMENT_LENGTH);

            if (commentPart == LINE_COMMENT_NOTATION) {

                setTextareaValue(firstPart + secondPart);
            }
        }

        function performMultiLineUncomment(textValue){

            var lengthBeforeLine = getLengthBeforeFirstLine(getSelectionFirstLineNumber(getCaretStart()));
            var lengthAfterLine = getLengthBetweenTwoLines(getSelectionFirstLineNumber(getCaretStart()), getSelectionLastLineNumber(getCaretEnd())) + lengthBeforeLine - 1; // -1 for \n

            var firstPart = textValue.substring(0, lengthBeforeLine);
            let commentStartPart = textValue.substring(lengthBeforeLine, (+lengthBeforeLine + +MULTI_LINE_COMMENT_LENGTH));
            
            var secondPart = textValue.substring((+lengthBeforeLine + +MULTI_LINE_COMMENT_LENGTH), (+lengthAfterLine - +MULTI_LINE_COMMENT_LENGTH));
            var commentEndPart = textValue.substr((+lengthAfterLine - +MULTI_LINE_COMMENT_LENGTH), (+MULTI_LINE_COMMENT_LENGTH));
            
            var thirdPart = textValue.substring(lengthAfterLine);

            if (commentStartPart == MULTI_LINE_COMMENT_START && commentEndPart == MULTI_LINE_COMMENT_END){

                setTextareaValue(firstPart + secondPart + thirdPart);
            }
        }
    });
});