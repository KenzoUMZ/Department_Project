using Department.Shared.Data;
using Department.Shared.Model;
using Microsoft.AspNetCore.Builder;

namespace DepartmentAPI.Extensions
{
    public static class ProjectExtension
    {
        public static void MapProjectEndpoints(this WebApplication app)
        {
            var projectDal = new DAL<Project>();

            app.MapGet("/project", () =>
            {
                var projects = projectDal.Read();
                return Results.Ok(projects);
            });

            app.MapGet("/project/{id:int}", (int id) =>
            {
                var project = projectDal.ReadBy(project => project.Id == id);
                return project is not null ? Results.Ok(project) : Results.NotFound();
            });

            app.MapPost("/project", (Project project) =>
            {
                projectDal.Create(project);
                return Results.Created($"/project/{project.Id}", project);
            });

            app.MapPut("/project/{id:int}", (int id, Project project) =>
            {
                if (project.Id != id)
                    return Results.BadRequest("ID da URL não corresponde ao ID do objeto.");

                var existing = projectDal.ReadBy(project => project.Id == id);
                if (existing is null)
                    return Results.NotFound();

                projectDal.Update(project);
                return Results.NoContent();
            });

            app.MapDelete("/project/{id:int}", (int id) =>
            {
                var project = projectDal.ReadBy(project => project.Id == id);
                if (project is null)
                    return Results.NotFound();

                projectDal.Delete(project);
                return Results.NoContent();
            });
        }
    }
}
