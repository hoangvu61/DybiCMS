window.insertTableToQuill = function (elementId) {
    var quill = Quill.find('#' + elementId);

    if (quill) {
        var tableModule = quill.getModule('better-table');

        // Insert a 3x3 table by default
        tableModule.insertTable(3, 3);
    }
};