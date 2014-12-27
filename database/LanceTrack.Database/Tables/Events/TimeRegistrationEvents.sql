create table TimeRegistrationEvents
(
	Id int not null identity (1, 1),
	UserId int not null,
	ProjectId int not null,
	At datetimeoffset not null,
	Hours decimal (18,2) not null,
	RegisteredByUserId int not null,
	RegisteredAt datetimeoffset not null,

	constraint PK_TimeRegistrationEvents primary key (Id),	
	constraint FK_TimeRegistrationEvents_ProjectId foreign key (ProjectId) references Project(Id),
	constraint FK_TimeRegistrationEvents_UserAccount_User foreign key (UserId) references UserAccount(Id),	
	constraint FK_TimeRegistrationEvents_UserAccount_RegisteredByUser foreign key (RegisteredByUserId) references UserAccount(Id)
)
