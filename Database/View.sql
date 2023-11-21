create or alter view V_UserRole as
select anr.Id,anr.Name, anur.UserId, p.PersonID 
from AspNetRoles anr, AspNetUserRoles anur,Person p
where anr.Id = anur.RoleId and anur.UserId = p.AccountID