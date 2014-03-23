
//-------------------------------------
//var patients = [];

function ItemViewModel(id, name, cpr, location, recordLocation, procedure, color, lastMessage, buzz) {
    var self = this;

    self.Id = ko.observable(id);
    self.Name = ko.observable(name);
    self.Cpr = ko.observable(cpr);
    self.Location = ko.observable(location);
    self.RecordLocation = ko.observable(recordLocation);
    self.Procedure = ko.observable(procedure);
    self.Color = ko.observable(color);
    self.LastMessage = ko.observable(lastMessage);
    self.Buzz = ko.observable(buzz);

}

var PatientsViewModel = function() {
    var self = this;
    self.items = ko.observableArray([]);
    
};

var vm = new PatientsViewModel();

ko.applyBindings(vm);