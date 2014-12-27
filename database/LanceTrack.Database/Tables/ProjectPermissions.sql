create table ProjectPermissions
(
	Id int not null identity(1, 1),
	ProjectId int not null,
	UserId int not null,
	UserPermissions int not null, -- 1 = View, 2 = Edit

	constraint PK_ProjectPermissions primary key (Id),
	constraint FK_ProjectPermissions_Project foreign key (ProjectId) references Project(Id),
	constraint FK_ProjectPermissions_UserAccount foreign key (UserId) references UserAccount(Id)
)
