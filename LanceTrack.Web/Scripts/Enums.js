var LanceTrack;
(function (LanceTrack) {
    (function (Domain) {
        (function (Projects) {
            (function (ProjectStatus) {
                ProjectStatus[ProjectStatus["Disabled"] = 0] = "Disabled";
                ProjectStatus[ProjectStatus["Active"] = 1] = "Active";
                ProjectStatus[ProjectStatus["Completed"] = 2] = "Completed";
            })(Projects.ProjectStatus || (Projects.ProjectStatus = {}));
            var ProjectStatus = Projects.ProjectStatus;
        })(Domain.Projects || (Domain.Projects = {}));
        var Projects = Domain.Projects;
    })(LanceTrack.Domain || (LanceTrack.Domain = {}));
    var Domain = LanceTrack.Domain;
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=Enums.js.map
