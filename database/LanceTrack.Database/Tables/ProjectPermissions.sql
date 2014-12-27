create table ProjectPermissions
(
	Id int not null identity(1, 1),
	ProjectId int not null,
	UserId int not null,
	UserPermissions int not null, -- 1 = View Time, 2 = Register Time, 4 = Register Time for other user

	constraint PK_ProjectPermissions primary key (ProjectId, UserId),
	constraint FK_ProjectPermissions_Project foreign key (ProjectId) references Project(Id),
	constraint FK_ProjectPermissions_UserAccount foreign key (UserId) references UserAccount(Id)
)
