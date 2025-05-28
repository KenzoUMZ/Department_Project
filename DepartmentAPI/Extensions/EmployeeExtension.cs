using Department.Shared.Data;
using Department.Shared.Model;
using DepartmentAPI.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAPI.Extensions
{
    public static class EmployeeExtension
    {
        public static void MapEmployeeEndpoints(this WebApplication app)
        {
            app.MapGet("/employee", ([FromServices] DAL<Employee> employeeDal) =>
            {
                var employees = employeeDal.Read().Select(e => new EmployeeRequest(e.Name, e.Position, e.EmployeeId, e.DepartmentId, e.ProjectId)
                {
                    Name = e.Name,
                    Position = e.Position,
                    EmployeeId = e.EmployeeId,
                    DepartmentId = e.DepartmentId,
                    ProjectId = e.ProjectId
                });
                return Results.Ok(employees);
            });

            app.MapGet("/employee/{id:int}", ([FromServices] DAL<Employee> employeeDal, int id) =>
            {
                var employee = employeeDal.ReadBy(e => e.EmployeeId == id);
                if (employee is null)
                    return Results.NotFound();

                var dto = new EmployeeRequest(employee.Name, employee.Position, employee.EmployeeId, employee.DepartmentId, employee.ProjectId)
                {
                    Name = employee.Name,
                    Position = employee.Position,
                    EmployeeId = employee.EmployeeId,
                    DepartmentId = employee.DepartmentId,
                    ProjectId = employee.ProjectId
                };
                return Results.Ok(dto);
            });

            app.MapPost("/employee", ([FromServices] DAL<Employee> employeeDal, EmployeeRequest employeeRequest) =>
            {
                var employee = new Employee
                {
                    Name = employeeRequest.Name,
                    Position = employeeRequest.Position,
                    EmployeeId = employeeRequest.EmployeeId,
                    DepartmentId = employeeRequest.DepartmentId,
                    ProjectId = employeeRequest.ProjectId
                };
                employeeDal.Create(employee);

                var dto = new EmployeeRequest(employee.Name, employee.Position, employee.EmployeeId, employee.DepartmentId, employee.ProjectId)
                {
                    Name = employee.Name,
                    Position = employee.Position,
                    EmployeeId = employee.EmployeeId,
                    DepartmentId = employee.DepartmentId,
                    ProjectId = employee.ProjectId
                };
                return Results.Created($"/employee/{employee.EmployeeId}", dto);
            });

            app.MapPut("/employee/{id:int}", ([FromServices] DAL<Employee> employeeDal, int id, EmployeeRequest employeeRequest) =>
            {
                if (employeeRequest.EmployeeId != id)
                    return Results.BadRequest("ID da URL não corresponde ao ID do objeto.");

                var existing = employeeDal.ReadBy(e => e.EmployeeId == id);
                if (existing is null)
                    return Results.NotFound();

                existing.Name = employeeRequest.Name;
                existing.Position = employeeRequest.Position;
                existing.DepartmentId = employeeRequest.DepartmentId;
                existing.ProjectId = employeeRequest.ProjectId;
                employeeDal.Update(existing);

                return Results.NoContent();
            });

            app.MapDelete("/employee/{id:int}", ([FromServices] DAL<Employee> employeeDal, int id) =>
            {
                var employee = employeeDal.ReadBy(e => e.EmployeeId == id);
                if (employee is null)
                    return Results.NotFound();

                employeeDal.Delete(employee);
                return Results.NoContent();
            });
        }
    }
}
