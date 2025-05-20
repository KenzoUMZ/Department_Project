using Department.Shared.Data;
using Department.Shared.Model;
using Microsoft.AspNetCore.Builder;

namespace DepartmentAPI.Extensions
{
    public static class EmployeeExtension
    {
        public static void MapEmployeeEndpoints(this WebApplication app)
        {
            var employeeDal = new EmployeeDAL();

            app.MapGet("/employee", () =>
            {
                var employees = employeeDal.Read();
                return Results.Ok(employees);
            });

            app.MapGet("/employee/{id:int}", (int id) =>
            {
                var employee = employeeDal.ReadById(id);
                return employee is not null ? Results.Ok(employee) : Results.NotFound();
            });

            app.MapPost("/employee", (Employee employee) =>
            {
                employeeDal.Create(employee);
                return Results.Created($"/employee/{employee.EmployeeId}", employee);
            });

            app.MapPut("/employee/{id:int}", (int id, Employee employee) =>
            {
                if (employee.EmployeeId != id)
                    return Results.BadRequest("ID da URL não corresponde ao ID do objeto.");

                var existing = employeeDal.ReadById(id);
                if (existing is null)
                    return Results.NotFound();

                employeeDal.Update(employee);
                return Results.NoContent();
            });

            app.MapDelete("/employee/{id:int}", (int id) =>
            {
                var employee = employeeDal.ReadById(id);
                if (employee is null)
                    return Results.NotFound();

                employeeDal.Delete(employee);
                return Results.NoContent();
            });
        }
    }
}
