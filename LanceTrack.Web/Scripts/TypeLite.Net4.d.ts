
 
 

 


declare module Api {
	interface ProjectTimeInfoResult {
		currentUserId: number;
		startDate: Date;
		endDate: Date;
		projects: Api.Project[];
		time: Api.ProjectDailyTime[];
		users: Api.UserAccount[];
	}
	interface Project {
		id: number;
		name: string;
		status: LanceTrack.Domain.Projects.ProjectStatus;
		startDate: Date;
		endDate: Date;
		maxTotalHoursPerDay: number;
		maxTotalHours: number;
	}
	interface ProjectDailyTime {
		projectId: number;
		userId: number;
		date: Date;
		totalHours: number;
	}
	interface UserAccount {
		id: number;
		email: string;
		displayName: string;
	}
}


