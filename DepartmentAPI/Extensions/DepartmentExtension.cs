using Department.Shared.Data;
using Department.Shared.Model;

namespace DepartmentAPI.Extensions
{
    public static class DepartmentExtension
    {
        public static void MapDepartmentEndpoints(this WebApplication app)
        {
            var departmentDal = new DepartmentDAL();

            app.MapGet("/department", () =>
            {
                var departments = departmentDal.Read();
                return Results.Ok(departments);
            });

            app.MapGet("/department/{id:int}", (int id) =>
            {
                var department = departmentDal.ReadById(id);
                return department is not null ? Results.Ok(department) : Results.NotFound();
            });

            app.MapPost("/department", (DepartmentEntity department) =>
            {
                departmentDal.Create(department);
                return Results.Created($"/department/{department.Id}", department);
            });

            app.MapPut("/department/{id:int}", (int id, DepartmentEntity department) =>
            {
                if (department.Id != id)
                    return Results.BadRequest("ID da URL não corresponde ao ID do objeto.");

                var existing = departmentDal.ReadById(id);
                if (existing is null)
                    return Results.NotFound();

                departmentDal.Update(department);
                return Results.NoContent();
            });

            app.MapDelete("/department/{id:int}", (int id) =>
            {
                var department = departmentDal.ReadById(id);
                if (department is null)
                    return Results.NotFound();

                departmentDal.Delete(department);
                return Results.NoContent();
            });
        }
    }

}
