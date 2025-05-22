using Department.Shared.Data;
using Department.Shared.Model;

namespace DepartmentAPI.Extensions
{
    public static class DepartmentExtension
    {
        public static void MapDepartmentEndpoints(this WebApplication app)
        {
            DAL<DepartmentEntity> departmentDal = new DAL<DepartmentEntity>();

            app.MapGet("/department", () =>
            {
                var departments = departmentDal.Read();
                return Results.Ok(departments);
            });

            app.MapGet("/department/{id:int}", (int id) =>
            {
                var department = departmentDal.ReadBy(department => department.Id == id);
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

                //var existing = departmentDal.ReadById(id);
                var existing = departmentDal.ReadBy(department => department.Id == id);
                if (existing is null)
                    return Results.NotFound();

                departmentDal.Update(department);
                return Results.NoContent();
            });

            app.MapDelete("/department/{id:int}", (int id) =>
            {

                //var department = departmentDal.ReadById(id);
                var department = departmentDal.ReadBy(department => department.Id == id);
                if (department is null)
                    return Results.NotFound();

                departmentDal.Delete(department);
                return Results.NoContent();
            });
        }
    }

}
