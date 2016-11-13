var GTModel = function(data)
{
    var self = this;
    self.Id = ko.observable(data ? data.Id : 0);
    self.Name = ko.observable(data ? data.Name : "").extend({ required: "" });
    self.Description = ko.observable(data ? data.Description : "");
    self.AgeRestriction = ko.observable(data ? data.AgeRestriction : 0).extend({ numeric: { precision: 0, showZeroes: false, maxValue: 100} });
    self.Company = ko.observable(data ? data.Company : "").extend({ required: "" });
    self.Price = ko.observable(data ? data.Price : 0.00).extend({ numeric: { precision: 2, showZeroes: true , maxValue: 1000} });

}

var GTModelEdit = function (data) {
    var self = this;
    self.Id = ko.observable(data ? data.Id() : 0);
    self.Name = ko.observable(data ? data.Name() : "").extend({ required: "" });
    self.Description = ko.observable(data ? data.Description() : "");
    self.AgeRestriction = ko.observable(data ? data.AgeRestriction() : 0).extend({ numeric: { precision: 0, showZeroes: false, maxValue: 100 } });
    self.Company = ko.observable(data ? data.Company() : "").extend({ required: "" });
    self.Price = ko.observable(data ? data.Price() : 0.00).extend({ numeric: { precision: 2, showZeroes: true, maxValue: 1000 } });

}