CREATE VIEW V_USER AS
select 
	u.Id, 
	u.Email, 
    u.Lastname, 
    u.Surname, 
    u.FacebookAccessToken, 
    r.Id as RoleId, 
    r.Name as RoleName, 
    r.Description as RoleDescription 
from 
	APPUSER u 
    inner join ROLE r on u.RoleId=r.Id;
