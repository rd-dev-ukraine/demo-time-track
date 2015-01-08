create table ProjectUserData
(
	Id int not null identity(1, 1),
	ProjectId int not null,
	UserId int not null,
	UserPermissions int not null, -- 1 = View Time, 2 = Register Time, 4 = Register Time for other user
	HourlyRate numeric(18, 2) not null default(1),

	constraint PK_ProjectUserData primary key (ProjectId, UserId),
	constraint FK_ProjectUserData_Project foreign key (ProjectId) references Project(Id),
	constraint FK_ProjectUserData_UserAccount foreign key (UserId) references UserAccount(Id)
)
