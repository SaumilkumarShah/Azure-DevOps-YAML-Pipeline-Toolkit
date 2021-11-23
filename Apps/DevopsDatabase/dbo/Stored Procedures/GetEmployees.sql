CREATE Procedure [dbo].[GetEmployees]  
as  
begin  
select Id as Empid,Name,City,Address from Employee
End