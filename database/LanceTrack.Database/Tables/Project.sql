create table Project
(
	Id int not null identity(1, 1),
	Name nvarchar(150) not null,
	Status int not null, -- 0=Disabled, 1=Active, 2=Completed
	StartDate datetimeoffset not null,
	EndDate datetimeoffset null,
	MaxTotalHours decimal(18, 2) null,

	constraint PK_Project primary key (Id)
)
