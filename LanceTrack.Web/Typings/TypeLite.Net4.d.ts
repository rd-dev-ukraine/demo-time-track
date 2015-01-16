
 
 

 


declare module Api {
	interface PrepareInvoiceParams {
		projectId: number;
		invoiceUserRequests: Api.InvoiceUserRequest[];
	}
	interface InvoiceUserRequest {
		userId: number;
		hours: number;
	}
	interface PrepareInvoiceModel {
		invoice: Api.InvoiceRecalculationResult[];
		project: Api.Project;
		users: Api.UserAccount[];
	}
	interface InvoiceRecalculationResult {
		userId: number;
		maxHours: number;
		billingHours: number;
		sum: number;
	}
	interface Project extends LanceTrack.Domain.Projects.ProjectBase {
		permissions: Api.ProjectPermissions;
	}
	interface UserAccount {
		id: number;
		email: string;
		displayName: string;
	}
	interface Urls {
		data: Api.DataUrls;
		templates: Api.TemplatesUrls;
	}
	interface DataUrls {
		loadProjectTime: string;
		recalculateInvoice: string;
		statistics: string;
		track: string;
		prepareInvoice: string;
	}
	interface TemplatesUrls {
		trackTimeBase: string;
		timeCell: string;
		trackMyTime: string;
		usersTime: string;
		invoiceBase: string;
		billProject: string;
	}
	interface ProjectTimeInfoResult {
		currentUserId: number;
		startDate: string;
		endDate: string;
		projects: Api.Project[];
		time: Api.ProjectDailyTime[];
		users: Api.UserAccount[];
	}
	interface ProjectDailyTime {
		projectId: number;
		userId: number;
		date: string;
		totalHours: number;
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
declare module LanceTrack.Domain.Projects {
	interface ProjectBase {
		id: number;
		name: string;
		status: Api.ProjectStatus;
		startDate: Date;
		endDate: Date;
		maxTotalHoursPerDay: number;
		maxTotalHours: number;
	}
}


