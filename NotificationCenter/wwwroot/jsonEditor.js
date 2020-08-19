
window.jsonEditor = null;

window.initJsonEditor = function (json) {
    // create the editor
    const initialJson = JSON.parse(json) || {};
    const container = document.getElementById("jsoneditor")
    const options = { 'language' : 'en-EN' }
    const editor = new JSONEditor(container, options, initialJson)

    window.jsonEditor = editor
}

window.getJsonStringFromEditor = function () {
    return JSON.stringify(window.jsonEditor.get(), null, 2)
}
