declare module Api {
    export interface ProjectTimeInfoResult {
        startDate: string;
        endDate: string;
        projects: ProjectTimeInfo[];
    }

    export interface ProjectTimeInfo {
        projectId: number;
        projectTitle: string;
        time: TimeRecord[];
    }

    export interface TimeRecord {
        date: string;
        hours: number;
    }

    export interface ProjectUserSummaryData {
        id: number;
        projectId: number;
        userId: number;
        projectTotalHoursReported: number;
        userTotalHoursReported: number;
        projectTotalAmountEarned: number;
        userTotalAmountEarned: number;
    }

    export interface StatisticsResult {
        totalHours: number;
        totalEarnings: number;
        projectStatistics: ProjectUserSummaryData[];
    }
}