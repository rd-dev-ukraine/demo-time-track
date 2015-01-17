create table TimeRegistrationEvents
(
	Id int not null identity (1, 1),
	ProjectId int not null,
	UserId int not null,	
	At datetime not null,
	Hours decimal (18,2) not null,
	HourlyRate decimal(18, 2) not null default(1),
	RegisteredByUserId int not null,
	RegisteredAt datetimeoffset not null,

	constraint PK_TimeRegistrationEvents primary key nonclustered (Id),	
	constraint FK_TimeRegistrationEvents_ProjectId foreign key (ProjectId) references Project(Id),
	constraint FK_TimeRegistrationEvents_UserAccount_User foreign key (UserId) references UserAccount(Id),	
	constraint FK_TimeRegistrationEvents_UserAccount_RegisteredByUser foreign key (RegisteredByUserId) references UserAccount(Id)	
)

go

create clustered index IDX_TimeRegistrationEvents on TimeRegistrationEvents (ProjectId asc, At asc);