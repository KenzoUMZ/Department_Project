using Department.Shared.Data;
using Department.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAPI.Extensions
{
    public static class ProjectExtension
    {
        public static void MapProjectEndpoints(this WebApplication app)
        {
            app.MapGet("/project", ([FromServices] DAL<Project> projectDal) =>
            {
                var projects = projectDal.Read();
                return Results.Ok(projects);
            });

            app.MapGet("/project/{id:int}", ([FromServices] DAL<Project> projectDal, int id) =>
            {
                var project = projectDal.ReadBy(project => project.Id == id);
                return project is not null ? Results.Ok(project) : Results.NotFound();
            });

            app.MapPost("/project", ([FromServices] DAL<Project> projectDal, Project project) =>
            {
                projectDal.Create(project);
                return Results.Created($"/project/{project.Id}", project);
            });

            app.MapPut("/project/{id:int}", ([FromServices] DAL<Project> projectDal, int id, Project project) =>
            {
                if (project.Id != id)
                    return Results.BadRequest("ID da URL não corresponde ao ID do objeto.");

                var existing = projectDal.ReadBy(project => project.Id == id);
                if (existing is null)
                    return Results.NotFound();

                projectDal.Update(project);
                return Results.NoContent();
            });

            app.MapDelete("/project/{id:int}", ([FromServices] DAL<Project> projectDal, int id) =>
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
