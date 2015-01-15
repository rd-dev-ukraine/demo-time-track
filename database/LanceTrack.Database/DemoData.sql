set identity_insert UserAccount on;

merge UserAccount as t 
using 
(
	values (1, 'Admin', 'admin@test.com', 'admin'),
		   (2, 'User', 'user@test.com', '1')


) as s (Id, DisplayName, Email, Password)
on
	s.Id = t.Id
when matched then
	update set
		t.DisplayName = s.DisplayName,
		t.Email = s.Email,
		t.Password = s.Password		
when not matched by target then
	insert (Id, DisplayName, Email, Password)
	values (Id, DisplayName, Email, Password);

set identity_insert UserAccount off;


set identity_insert Project on;

merge Project as t 
using 
(
	values (1, 'Building', 8, 200),
		   (2, 'Destroying', 12, null)


) as s (Id, Name, MaxTotalHoursPerDay, MaxTotalHours)
on
	s.Id = t.Id
when matched then
	update set
		t.Name = s.Name,
		t.Status = 1,
		t.StartDate = DATEADD(DAY, -10 * s.Id, GETDATE()),
		t.EndDate = null,
		t.MaxTotalHoursPerDay = s.MaxTotalHoursPerDay,
		t.MaxTotalHours = s.MaxTotalHours		
when not matched by target then
	insert (Id, Name, Status, StartDate, EndDate, MaxTotalHoursPerDay, MaxTotalHours)
	values (Id, Name, 1, DATEADD(DAY, -10 * Id, GETDATE()), null, MaxTotalHoursPerDay, MaxTotalHours);


set identity_insert Project off;

merge ProjectUserInfo as t 
using 
(
	values (1, 1, 34, 20),
		   (1, 2, 33, 22),
		   (2, 1, 2, 24)

) as s (ProjectId, UserId, UserPermissions, HourlyRate)
on
	s.ProjectId = t.ProjectId and
	s.UserId = t.UserId
when matched then
	update set
		t.UserPermissions = s.UserPermissions,
		t.HourlyRate = s.HourlyRate
when not matched by target then
	insert (ProjectId, UserId, UserPermissions, HourlyRate)
	values (ProjectId, UserId, UserPermissions, HourlyRate);
