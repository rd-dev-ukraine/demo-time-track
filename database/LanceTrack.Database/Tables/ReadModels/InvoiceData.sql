create table InvoiceData 
(
	Id int not null identity (1, 1),
	InvoiceNum nvarchar(50) not null,
	ProjectId int not null constraint FK_InvoiceData_Project foreign key references Project(Id),
	At datetimeoffset not null,
	IsPaid bit not null,
	IsCancelled bit not null constraint DF_Invoice_IsCancelled default (0),
	InvoiceTotalSum numeric(18, 2) not null,
	InvoiceTotalHours numeric(18, 2) not null,
	ReceivedSum numeric(18, 2) null,
	BilledByUserId int not null constraint FK_InvoiceData_BilledByUser foreign key references UserAccount(Id),
	
	
	constraint PK_InvoiceData primary key (InvoiceNum)
)