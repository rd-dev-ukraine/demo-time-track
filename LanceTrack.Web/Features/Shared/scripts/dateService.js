﻿var LanceTrack;
(function (LanceTrack) {
    function datesFactory() {
        return new Dates();
    }
    LanceTrack.datesFactory = datesFactory;

    var Dates = (function () {
        function Dates() {
        }
        Dates.prototype.format = function (date) {
            return this.parseMoment(date).format(Dates.DateFormat);
        };

        Dates.prototype.parse = function (date) {
            return this.parseMoment(date).toDate();
        };

        Dates.prototype.startOfCurrentWeek = function () {
            return this.format(moment().startOf("week"));
        };

        Dates.prototype.endOfCurrentWeek = function () {
            return this.format(moment().endOf("week"));
        };

        Dates.prototype.allDateInRange = function (startDate, endDate) {
            var range = this.getValidRange(startDate, endDate);

            var start = moment(range.start);
            var end = moment(range.end);

            var result = [];

            while (start.unix() <= end.unix()) {
                result.push(start.clone().toDate());

                start.add(1, "day");
            }

            return result;
        };

        /*
        * From two dates selects max and uses it as range end.
        * Then makes range 31 day long max.
        *
        */
        Dates.prototype.getValidRange = function (startDate, endDate) {
            var d1 = this.parseMoment(startDate);
            var d2 = this.parseMoment(endDate);

            var end = moment.max([d1, d2]);
            var start = moment.max([
                end.clone().subtract(31, "day"),
                moment.min([d1, d2])]);

            return {
                start: start.toDate(),
                end: end.toDate()
            };
        };

        Dates.prototype.eq = function (date1, date2) {
            var d1 = moment(this.parse(date1));
            var d2 = moment(this.parse(date2));

            return d1.year() === d2.year() && d1.dayOfYear() === d2.dayOfYear();
        };

        Dates.prototype.parseMoment = function (date) {
            if (moment.isMoment(date))
                return date;

            if (typeof date == "string")
                return moment(date, Dates.DateFormat);

            return moment(date);
        };
        Dates.DateFormat = "YYYY-MM-DD";
        return Dates;
    })();
    LanceTrack.Dates = Dates;
})(LanceTrack || (LanceTrack = {}));
//# sourceMappingURL=dateService.js.map