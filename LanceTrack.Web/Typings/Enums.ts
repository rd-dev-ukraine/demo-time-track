module Api {
	export enum ProjectStatus {
		Disabled = 0,
		Active = 1,
		Completed = 2
	}
	export enum ProjectPermissions {
		None = 0,
		View = 1,
		TrackSelf = 2,
		TrackAsOtherUser = 6,
		ViewTotalAmount = 8,
		ViewProjectTotalHours = 16,
		BillProject = 32
	}
}

