﻿module LanceTrack {
    export function datesFactory() {
        return new Dates();
    }

    export class Dates {
        static DateFormat: string = "YYYY-MM-DD";

        format(date: any) {
            return this.parseMoment(date).format(Dates.DateFormat);
        }

        parse(date: any): Date {
            return this.parseMoment(date).toDate();
        }

        startOfCurrentWeek(): string {
            return this.format(moment().startOf("week"));
        }

        endOfCurrentWeek(): string {
            return this.format(moment().endOf("week"));
        }

        allDateInRange(startDate: any, endDate: any): Date[]{
            var range = this.getValidRange(startDate, endDate);

            var start = moment(range.start);
            var end = moment(range.end);

            var result = [];

            while (start.unix() <= end.unix()) {
                result.push(start.clone().toDate());

                start.add(1, "day");
            }

            return result;
        }

        /*
         * From two dates selects max and uses it as range end.
         * Then makes range 31 day long max.
         * 
         */
        getValidRange(startDate: any, endDate: any): DateRange {
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
        }

        eq(date1: any, date2: any): boolean {
            var d1 = moment(this.parse(date1));
            var d2 = moment(this.parse(date2));

            return d1.year() === d2.year() && d1.dayOfYear() === d2.dayOfYear();
        }

        private parseMoment(date: any): Moment {
            if (moment.isMoment(date))
                return date;

            if (typeof date == "string")
                return moment(date, Dates.DateFormat);

            return moment(date);
        }
    }

    export interface DateRange {
        start: Date;
        end: Date;
    }
} 