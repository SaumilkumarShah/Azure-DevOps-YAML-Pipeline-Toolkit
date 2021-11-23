Create procedure [dbo].[DeleteEmpById]
(
@EmpId int
)
as
begin
Delete from Employee where Id=@EmpId
End