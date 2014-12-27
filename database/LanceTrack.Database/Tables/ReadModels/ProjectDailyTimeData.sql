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
	Date datetime not null,
	TotalHours decimal(18, 2) not null,

	constraint PK_ProjectTimeData primary key (Id)
)
