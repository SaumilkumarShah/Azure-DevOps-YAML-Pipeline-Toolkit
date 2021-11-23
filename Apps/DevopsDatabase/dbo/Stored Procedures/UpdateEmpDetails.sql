Create procedure [dbo].[UpdateEmpDetails]
(
@EmpId int,
@Name varchar (50),
@City varchar (50),
@Address varchar (50)
)
as
begin
Update Employee
set Name=@Name,
City=@City,
Address=@Address
where Id=@EmpId
End