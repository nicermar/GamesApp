var ConfirmModalModel = function() {
    var self = this;

    self.title = ko.observable();

    self.contentText = ko.observable();

    self.executable = null;

    self.executeFunc = function (item, event) {
        self.executable(item, event);
    }
}