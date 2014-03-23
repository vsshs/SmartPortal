
//-------------------------------------
//var patients = [];

function ItemViewModel(id, name, cpr, location, recordLocation, procedure, color, lastMessage) {
    var self = this;

    self.Id = ko.observable(id);
    self.Name = ko.observable(name);
    self.Cpr = ko.observable(cpr);
    self.Location = ko.observable(location);
    self.RecordLocation = ko.observable(recordLocation);
    self.Procedure = ko.observable(procedure);
    self.Color = ko.observable(color);
    self.LastMessage = ko.observable(lastMessage);
   
}

var PatientsViewModel = function() {
    var self = this;
    self.items = ko.observableArray([]);
    
};

var vm = new PatientsViewModel();

ko.applyBindings(vm);











/*

var items = [{
    Id: 1,
    Text: 'First item'
}, {
    Id: 2,
    Text: 'Second item'
}];



var viewModel = function (items) {
    var self = this;
    self.items = ko.observableArray(items);
    self.selectedItemId = ko.observable();
    self.item = ko.observable();
    self.selectItem = function (item) {
        for (var i = 0; i < self.items().length; i++) {
            if (self.items()[i].Id() === self.selectedItemId()) {
                self.item(self.items()[i]);
                break;
            }
        }
    };
};

ko.applyBindings(new viewModel(observableItems));


*/