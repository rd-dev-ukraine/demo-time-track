declare module Api {
    export interface ProjectTimeInfoResult {
        startDate: string;
        endDate: string;
        time: ProjectTimeInfo[];
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
}