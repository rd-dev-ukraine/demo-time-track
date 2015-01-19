create table InvoiceEvents
(
	Id int not null identity (1, 1),
	ProjectId int not null,
	UserId int not null,	
	At datetimeoffset not null,

	EventType int not null, -- 1: Billing, 2: Payment.
	InvoiceNum nvarchar(50) not null, -- Number must match for billing and payment event pairs.
	InvoiceSum decimal(18, 2) not null default(1),
	Hours decimal (18,2) not null,	
	RegisteredByUserId int not null,
	RegisteredAt datetimeoffset not null,

	constraint PK_InvoiceEvents primary key nonclustered (Id),	
	constraint FK_InvoiceEvents_ProjectId foreign key (ProjectId) references Project(Id),
	constraint FK_InvoiceEvents_UserAccount_User foreign key (UserId) references UserAccount(Id),	
	constraint FK_InvoiceEvents_UserAccount_RegisteredByUser foreign key (RegisteredByUserId) references UserAccount(Id)	
)

go

create clustered index IDX_InvoiceEvents on InvoiceEvents (ProjectId asc, Id asc);