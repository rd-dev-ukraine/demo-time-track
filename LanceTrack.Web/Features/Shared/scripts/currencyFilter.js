var LanceTrack;
(function (LanceTrack) {
    function currencyFilter() {
        return function (value) {
            if (!value)
                return "-";
            return "$" + value;
        };
    }
    LanceTrack.currencyFilter = currencyFilter;
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=currencyFilter.js.map