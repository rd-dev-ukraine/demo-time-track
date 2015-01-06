var LanceTrack;
(function (LanceTrack) {
    function datesFactory() {
        return new Dates();
    }
    LanceTrack.datesFactory = datesFactory;

    var Dates = (function () {
        function Dates() {
        }
        Dates.prototype.format = function (date) {
            return moment(this.parse(date)).format(LanceTrack.DateFormat);
        };

        Dates.prototype.parse = function (date) {
            if (moment.isMoment(date))
                return date;

            if (typeof date == "string")
                return moment(date, LanceTrack.DateFormat).toDate();

            return moment(date).toDate();
        };

        Dates.prototype.startOfCurrentWeek = function () {
            return this.format(moment().startOf("week"));
        };

        Dates.prototype.endOfCurrentWeek = function () {
            return this.format(moment().endOf("week"));
        };

        Dates.prototype.dateRange = function (startDate, endDate) {
            var start = this.parse(startDate);
            var end = this.parse(endDate);

            var result = [];

            while (start <= end) {
                result.push(start);

                start = moment(start).add(1, "day").toDate();
            }

            return result;
        };

        Dates.prototype.eq = function (date1, date2) {
            var d1 = moment(this.parse(date1));
            var d2 = moment(this.parse(date2));

            return d1.year() === d2.year() && d1.dayOfYear() === d2.dayOfYear();
        };
        Dates.DateFormat = "YYYY-MM-DD";
        return Dates;
    })();
    LanceTrack.Dates = Dates;
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=dateService.js.map
