create table ProjectUserInfo
(
	Id int not null identity(1, 1),
	ProjectId int not null,
	UserId int not null,
	UserPermissions int not null, 
	HourlyRate numeric(18, 2) not null default(1),
	MaxDailyHours numeric(18, 2) null,
	MaxProjectHours numeric(18, 2) null,

	constraint PK_ProjectUserInfo primary key (ProjectId, UserId),
	constraint FK_ProjectUserInfo_Project foreign key (ProjectId) references Project(Id),
	constraint FK_ProjectUserInfo_UserAccount foreign key (UserId) references UserAccount(Id)
)
