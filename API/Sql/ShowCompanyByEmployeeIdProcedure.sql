CREATE PROCEDURE [dbo].[ShowCompanyByEmployeeId]
	@EmpId int
AS
	SELECT c.Id, c.Name, c.Address, c.Country
	FROM Companies c join Employees e on c.Id = e.CompanyId
	WHERE e.Id = @EmpId
