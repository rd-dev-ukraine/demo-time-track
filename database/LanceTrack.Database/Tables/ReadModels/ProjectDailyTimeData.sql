/*
 * Read model (read model tables ends with Data) 
 * Contains hours reported per day by project and user 
 *
 */
create table ProjectDailyTimeData
(
	Id int not null identity(1, 1),
	ProjectId int not null,
	UserId int not null,
	Date date not null,
	TotalHours decimal(18, 2) not null,
	BilledHours decimal(18, 2) not null constraint DF_ProjectDailyTimeData_BilledHours default(0),
	HourlyRate decimal(18, 2) not null default (0),

	constraint PK_ProjectTimeData primary key (Id)
)
