using Department.Shared.Data;
using Department.Shared.Model;
using DepartmentAPI.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAPI.Extensions
{
    public static class DepartmentExtension
    {
        public static void MapDepartmentEndpoints(this WebApplication app)
        {
            app.MapGet("/departments", ([FromServices] DAL<DepartmentEntity> departmentDal) =>
            {
                var departments = departmentDal.Read()
                    .Select(d => new DepartmentRequest(d.Name, d.Location) { Name = d.Name, Location = d.Location });
                return Results.Ok(departments);
            });

            app.MapGet("/department/{id:int}", ([FromServices] DAL<DepartmentEntity> departmentDal, int id) =>
            {
                var department = departmentDal.ReadBy(d => d.Id == id);
                if (department is null)
                    return Results.NotFound();

                var dto = new DepartmentRequest(department.Name, department.Location) { Name = department.Name, Location = department.Location };
                return Results.Ok(dto);
            });

            app.MapPost("/department", ([FromServices] DAL<DepartmentEntity> departmentDal, DepartmentRequest departmentRequest) =>
            {
                var department = new DepartmentEntity(departmentRequest.Name, departmentRequest.Location);
                departmentDal.Create(department);
                var dto = new DepartmentRequest(department.Name, department.Location) { Name = department.Name, Location = department.Location };
                return Results.Created($"/department/{department.Id}", dto);
            });

            app.MapPut("/department/{id:int}", ([FromServices] DAL<DepartmentEntity> departmentDal, int id, DepartmentRequest departmentRequest) =>
            {
                var existing = departmentDal.ReadBy(d => d.Id == id);
                if (existing is null)
                    return Results.NotFound();

                existing.Name = departmentRequest.Name;
                existing.Location = departmentRequest.Location;
                departmentDal.Update(existing);

                return Results.NoContent();
            });

            app.MapDelete("/department/{id:int}", ([FromServices] DAL<DepartmentEntity> departmentDal, int id) =>
            {
                var department = departmentDal.ReadBy(d => d.Id == id);
                if (department is null)
                    return Results.NotFound();

                departmentDal.Delete(department);
                return Results.NoContent();
            });
        }
    }

}
