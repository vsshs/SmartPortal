var dummyPatient = [
    {
        id: "0",
        name: "name0",
        cpr: "0000000000",
        location: "p_location",
        recordLocation: "r_location",
        procedure: "none",
        color: "rgb(255, 255, 0)",
        rfid: ""
    },
    {
        id: "1",
        name: "name1",
        cpr: "0000000001",
        location: "p_location",
        recordLocation: "r_location",
        procedure: "none",
        color: "rgb(255, 255, 255)",
        rfid: ""
    }
];

var PatientsViewModel = function () {
    var self = this;
    




    var url = "/api/patient/GetPatients";
    var refresh = function () {
        
        $.getJSON(url, {}, function(data) {
            self.Patients(data);
        });
        //self.Patients(dummyPatient);
    };

    // Public data properties
    self.Patients = ko.observableArray([]);
    refresh();
};
ko.applyBindings(new PatientsViewModel());