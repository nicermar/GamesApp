
var GTSectionModel = function (data) {
    var self = this;
    self.Alert = function (alertType, message) {
        $(".alert-success").clearQueue();
        $(".alert-warning").clearQueue();
        $(".alert-info").clearQueue();
        $(".alert-danger").clearQueue();

        $(".alert-success").hide();
        $(".alert-warning").hide();
        $(".alert-info").hide();
        $(".alert-danger").hide();

        $(".alert-" + alertType + " .alert-text").text(message);

        $(".alert-" + alertType).show().delay(3500).queue(function (next) {
            $(".alert-" + alertType).hide();
            next();
        });
    };
    self.validationMessage = ko.observable("");
    self.confirmModalModel = ko.observable(new ConfirmModalModel());
    self.GTItems = ko.observableArray([]);
    self.GTModel = ko.observable();
    self.EditedGTModel = ko.observable();
    self.Init = function () {
        $.ajax({
            url: "/api/ApiGamesToys",
            Method: "get",
            data: data,
            async: true,
            cache: false,
            success: function (items) {
                if (items) {
                    self.GTItems( ($.map(items, function (item) {
                        return new GTModel(item);
                    })));
                }
            }
        });
    }
   
    self.addEdit = function (item) {
        if (item.Id) {
            self.GTModel(new GTModelEdit(item));
            self.EditedGTModel(item);
        }
        else {
            self.GTModel(new GTModel());
        }
    }

    self.Save = function (item) {
        self.validationMessage(self.Validate(item))
        
        if (self.validationMessage() == "") {
            $.ajax({
                url: '/api/ApiGamesToys',
                type: 'Post',
                data: ko.toJSON(item),
                contentType: 'application/json',
                success: function (data) {
                    if (item.Id() == 0) {
                        item.Id(data.Id);
                        self.GTItems.push(item);
                        self.Alert("success", "Game or toy added successfully.");
                        $("#AddEditGTModal").modal('hide');
                    }
                    else {
                        self.GTItems.replace(self.EditedGTModel(), item);
                        self.Alert("success", "Game or toy edited successfully.");
                        $("#AddEditGTModal").modal('hide');
                    }
                }
            });
        }
        else {
            self.Alert("danger", self.validationMessage());
        }
    }

    self.Validate = function (item) {
        var message = "";
        if (item.Name() == "" ) {
            message ="Name is a mandatory field. ";
        }
        if (item.Company() == "") {
            message += "Company is a mandatory field. ";
        }
        if (item.Price() == 0.00) {
            message += "Price is a mandatory field and cannot be 0.";
        }

        return message;
    }
    self.Remove = function (item) {
        self.confirmModalModel().title("Confirm");
        self.confirmModalModel().contentText("Are you sure you want to delete '" + item.Name() + "' from your Games & Toys?");
        self.confirmModalModel().executable = function () {
            $.ajax({
                url: '/api/ApiGamesToys/' + item.Id(),
                type: 'delete',
                contentType: 'application/json',
                success: function (data) {
                    self.GTItems.remove(item);
                    self.Alert("success", "Game or toy deleted succesfully.");
                    $("#deleteModal").modal('hide');
                }
            });
            
        };
       
    }
};

