
 
 

 


declare module Api {
	interface Urls {
		data: Api.DataUrls;
		templates: Api.TemplatesUrls;
	}
	interface DataUrls {
		loadProjectTime: string;
		recalculate: string;
		recalculateInvoice: string;
		statistics: string;
		track: string;
	}
	interface TemplatesUrls {
		trackMyTime: string;
		timeCell: string;
	}
	interface ProjectTimeInfoResult {
		currentUserId: number;
		startDate: string;
		endDate: string;
		projects: Api.Project[];
		time: Api.ProjectDailyTime[];
		users: Api.UserAccount[];
	}
	interface Project {
		id: number;
		name: string;
		status: Api.ProjectStatus;
		startDate: Date;
		endDate: Date;
		maxTotalHoursPerDay: number;
		maxTotalHours: number;
	}
	interface ProjectDailyTime {
		projectId: number;
		userId: number;
		date: string;
		totalHours: number;
	}
	interface UserAccount {
		id: number;
		email: string;
		displayName: string;
	}
	interface StatisticsResult {
		totalHours: number;
		totalEarnings: number;
		projectStatistics: Api.ProjectUserSummary[];
	}
	interface ProjectUserSummary {
		id: number;
		projectId: number;
		userId: number;
		projectTotalHoursReported: number;
		userTotalHoursReported: number;
		projectTotalAmountEarned: number;
		userTotalAmountEarned: number;
	}
}


