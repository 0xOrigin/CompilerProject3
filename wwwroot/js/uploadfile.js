var fileContent;

function onFileLoad(elementId, event) {
    fileContent = event.target.result;
}

function onChooseFile(event, onLoadFileHandler) {
    if (typeof window.FileReader !== 'function')
        throw ("The file API isn't supported on this browser.");
    var input = event.target;
    if (!input)
        throw ("The browser does not properly implement the event object");
    if (!input.files)
        throw ("This browser does not support the `files` property of the file input.");
    if (!input.files[0])
        return undefined;
    var file = input.files[0];
    var fr = new FileReader();
    fr.onload = onLoadFileHandler;
    fr.readAsText(file);
}

function IsFileLoaded() {
    return (document.getElementById('browseButton').files.length != 0);
}

function getFileName() {
    return document.getElementById('browseButton').files[0].name;
}

function getFileContent() {
    return fileContent;
}