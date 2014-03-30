
//-------------------------------------
//var patients = [];

function ItemViewModel(id, name, cpr, location, recordLocation, procedure, color, lastMessage, buzz, lastUpdated) {
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
    self.LastUpdated = ko.observable(lastUpdated);
    self.WithColor = ko.computed(function() {
        return (
            self.Color() === 'rgb(255, 255, 255)' ||
            self.Color() === 'rgb(255, 255, 0)' ||
            self.Color() === 'rgb(0, 255, 255)' ||
            self.Color() === 'rgb(0, 255, 0)' ? "black" : "whitesmoke");
    });
}

var PatientsViewModel = function() {
    var self = this;
    self.items = ko.observableArray([]);
    
};

function updateFromExisting(itemFound, itemToUpdate) {
    
    return itemFound;
}
var vm = new PatientsViewModel();

ko.applyBindings(vm);


