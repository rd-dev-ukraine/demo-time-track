var LanceTrack;
(function (LanceTrack) {
    function hoursFilter() {
        return function (value) {
            if (!value)
                return "-";
            return value + " hrs";
        };
    }
    LanceTrack.hoursFilter = hoursFilter;
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=hoursFilter.js.map