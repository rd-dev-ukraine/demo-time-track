module LanceTrack {
    export function datesFactory() {
        return new Dates();
    }

    export class Dates {
        static DateFormat: string = "YYYY-MM-DD";

        format(date: any) {
            return moment(this.parse(date)).format(DateFormat);
        }

        parse(date: any): Date {
            if (moment.isMoment(date))
                return date;

            if (typeof date == "string")
                return moment(date, DateFormat).toDate();

            return moment(date).toDate();
        }

        startOfCurrentWeek(): string {
            return this.format(moment().startOf("week"));
        }

        endOfCurrentWeek(): string {
            return this.format(moment().endOf("week"));
        }

        dateRange(startDate: any, endDate: any): Date[] {
            var start = this.parse(startDate);
            var end = this.parse(endDate);

            var result = [];

            while (start <= end) {
                result.push(start);

                start = moment(start).add(1, "day").toDate();
            }

            return result;
        }

        eq(date1: any, date2: any): boolean {
            var d1 = moment(this.parse(date1));
            var d2 = moment(this.parse(date2));

            return d1.year() === d2.year() && d1.dayOfYear() === d2.dayOfYear();
        }
    }
} 