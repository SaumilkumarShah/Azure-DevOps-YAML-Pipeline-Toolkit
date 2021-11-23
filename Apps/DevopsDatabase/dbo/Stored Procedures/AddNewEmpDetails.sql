
CREATE procedure [dbo].[AddNewEmpDetails]
(
@EmpId int,
@Name varchar (50),
@City varchar (50),
@Address varchar (50)
)
as
begin
Insert into Employee values(@Name,@City,@Address)
End