create table Project
(
	Id int not null identity(1, 1),
	Name nvarchar(150) not null,
	StartDate datetimeoffset not null,
	EndDate datetimeoffset null,
	MaxHoursPerDay int null,
	MaxTotalHours int null,

	constraint PK_Project primary key (Id)
)
