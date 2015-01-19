
 
 

 


declare module Api {
	interface PrepareInvoiceParams {
		invoiceUserRequests: Api.InvoiceUserRequest[];
		projectId: number;
	}
	interface InvoiceUserRequest {
		userId: number;
		hours: number;
	}
	interface InvoiceModel {
		invoice: Api.Invoice;
		details: Api.InvoiceDetails[];
		project: Api.Project;
		users: Api.UserAccount[];
	}
	interface Invoice {
		invoiceNum: string;
		projectId: number;
		at: Date;
		isPaid: boolean;
		sum: number;
		hours: number;
		receivedSum: number;
		billedByUserId: number;
	}
	interface InvoiceDetails {
		invoiceNum: string;
		userId: number;
		userSum: number;
		userHours: number;
		userReceivedSum: number;
	}
	interface Project extends LanceTrack.Domain.Projects.ProjectBase {
		permissions: Api.ProjectPermissions;
	}
	interface UserAccount {
		id: number;
		email: string;
		displayName: string;
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
	interface Urls {
		data: Api.DataUrls;
		templates: Api.TemplatesUrls;
	}
	interface DataUrls {
		loadProjectTime: string;
		prepareInvoice: string;
		recalculateInvoice: string;
		statistics: string;
		track: string;
		bill: string;
		invoiceDetails: string;
	}
	interface TemplatesUrls {
		billProject: string;
		invoiceBase: string;
		invoiceDetails: string;
		timeCell: string;
		trackMyTime: string;
		trackTimeBase: string;
		usersTime: string;
	}
	interface ProjectTimeInfoResult {
		currentUserId: number;
		startDate: string;
		endDate: string;
		projects: Api.Project[];
		time: Api.DailyTime[];
		users: Api.UserAccount[];
	}
	interface DailyTime {
		projectId: number;
		userId: number;
		date: string;
		totalHours: number;
		billedHours: number;
		paidHours: number;
		hourlyRate: number;
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


