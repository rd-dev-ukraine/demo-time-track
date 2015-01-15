create table InvoiceDetailsData 
(
	Id int not null identity (1, 1),
	InvoiceNum nvarchar(50) not null constraint FK_Invoice_InvoiceNum foreign key references InvoiceData(InvoiceNum),
	UserId int not null constraint FK_InvoiceData_User foreign key references UserAccount (Id),
	UserSum numeric(18, 2) not null,
	UserHours numeric(18, 2) not null,
	UserReceivedSum numeric(18, 2) null,
	
	constraint PK_InvoiceDetailsData primary key (InvoiceNum, UserId)
)