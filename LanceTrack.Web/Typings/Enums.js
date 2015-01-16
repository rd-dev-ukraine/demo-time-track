var Api;
(function (Api) {
    (function (ProjectStatus) {
        ProjectStatus[ProjectStatus["Disabled"] = 0] = "Disabled";
        ProjectStatus[ProjectStatus["Active"] = 1] = "Active";
        ProjectStatus[ProjectStatus["Completed"] = 2] = "Completed";
    })(Api.ProjectStatus || (Api.ProjectStatus = {}));
    var ProjectStatus = Api.ProjectStatus;
})(Api || (Api = {}));
var LanceTrack;
(function (LanceTrack) {
    (function (Domain) {
        (function (Projects) {
            (function (ProjectPermissions) {
                ProjectPermissions[ProjectPermissions["None"] = 0] = "None";
                ProjectPermissions[ProjectPermissions["View"] = 1] = "View";
                ProjectPermissions[ProjectPermissions["TrackSelf"] = 2] = "TrackSelf";
                ProjectPermissions[ProjectPermissions["TrackAsOtherUser"] = 6] = "TrackAsOtherUser";
                ProjectPermissions[ProjectPermissions["ViewTotalAmount"] = 8] = "ViewTotalAmount";
                ProjectPermissions[ProjectPermissions["ViewProjectTotalHours"] = 16] = "ViewProjectTotalHours";
                ProjectPermissions[ProjectPermissions["BillProject"] = 32] = "BillProject";
            })(Projects.ProjectPermissions || (Projects.ProjectPermissions = {}));
            var ProjectPermissions = Projects.ProjectPermissions;
        })(Domain.Projects || (Domain.Projects = {}));
        var Projects = Domain.Projects;
    })(LanceTrack.Domain || (LanceTrack.Domain = {}));
    var Domain = LanceTrack.Domain;
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=Enums.js.map
