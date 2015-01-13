create table UserAccount
(
	Id int not null identity (1, 1),
	Email nvarchar(250) not null,
	DisplayName nvarchar(250) not null,
	Password nvarchar(50) not null,

	constraint PK_UserAccount primary key (Id),
	constraint UQ_UserAccount_Email unique (Email)
)
