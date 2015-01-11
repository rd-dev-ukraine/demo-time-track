create table InvoiceData 
(
	Id int not null identity (1, 1),
	InvoiceNum nvarchar(50) not null,
	ProjectId int not null constraint FK_InvoiceData_Project foreign key references Project(Id),
	UserId int not null constraint FK_InvoiceData_User foreign key references UserAccount (Id),
	At datetimeoffset not null,
	IsPaid bit not null,
	InvoiceSum numeric(18, 2) not null,
	InvoiceHours numeric(18, 2) not null,
	
	constraint PK_InvoiceData primary key (InvoiceNum)
)