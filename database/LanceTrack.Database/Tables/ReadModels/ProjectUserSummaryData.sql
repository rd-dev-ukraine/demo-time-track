/*
 * Read model (read model tables ends with Data) 
 * Contains hours and money summary data for user per project
 *
 */
create table ProjectUserSummaryData
(
	Id int not null identity(1, 1),
	ProjectId int not null,
	UserId int not null,
	ProjectTotalHoursReported decimal(18, 2),
	UserTotalHoursReported decimal(18, 2),
	ProjectTotalAmountEarned decimal(18, 2),
	UserTotalAmountEarned decimal(18, 2)

	constraint PK_ProjectUserSummaryData primary key (ProjectId, UserId)
)
