var Api;
(function (Api) {
    (function (ProjectStatus) {
        ProjectStatus[ProjectStatus["Disabled"] = 0] = "Disabled";
        ProjectStatus[ProjectStatus["Active"] = 1] = "Active";
        ProjectStatus[ProjectStatus["Completed"] = 2] = "Completed";
    })(Api.ProjectStatus || (Api.ProjectStatus = {}));
    var ProjectStatus = Api.ProjectStatus;
})(Api || (Api = {}));
//# sourceMappingURL=Enums.js.map
