using Department.Shared.Data;
using Department.Shared.Model;
using DepartmentAPI.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAPI.Extensions
{
    public static class InfrastructureExtension
    {
        public static void MapInfrastructureEndpoints(this WebApplication app)
        {
            app.MapGet("/infrastructures", ([FromServices] DAL<Infrastructure> infrastructureDal) =>
            {
                var infrastructures = infrastructureDal.Read()
                    .Select(i => new InfrastructureRequest(i.OfficeLocation, i.NumberOfDesks, i.NumberOfComputers, i.HasMeetingRooms, i.DepartmentId));

                return Results.Ok(infrastructures);
            });

            app.MapGet("/infrastructure/{id:int}", ([FromServices] DAL<Infrastructure> infrastructureDal, int id) =>
            {
                var infrastructure = infrastructureDal.ReadBy(i => i.InfrastructureId == id);
                if (infrastructure is null)
                    return Results.NotFound();

                var dto = new InfrastructureRequest(infrastructure.OfficeLocation, infrastructure.NumberOfDesks, infrastructure.NumberOfComputers, infrastructure.HasMeetingRooms, infrastructure.DepartmentId);
                return Results.Ok(dto);
            });

            app.MapPost("/infrastructure", ([FromServices] DAL<Infrastructure> infrastructureDal, InfrastructureRequest infrastructureRequest) =>
            {
                var infrastructure = new Infrastructure(
                    infrastructureRequest.OfficeLocation,
                    infrastructureRequest.NumberOfDesks,
                    infrastructureRequest.NumberOfComputers,
                    infrastructureRequest.HasMeetingRooms
                )
                {
                    DepartmentId = infrastructureRequest.DepartmentId
                };

                infrastructureDal.Create(infrastructure);
                return Results.Created($"/infrastructure/{infrastructure.InfrastructureId}", infrastructure);
            });

            app.MapPut("/infrastructure/{id:int}", ([FromServices] DAL<Infrastructure> infrastructureDal, int id, InfrastructureRequest infrastructureRequest) =>
            {
                var existing = infrastructureDal.ReadBy(i => i.InfrastructureId == id);
                if (existing is null)
                    return Results.NotFound();

                existing.OfficeLocation = infrastructureRequest.OfficeLocation;
                existing.NumberOfDesks = infrastructureRequest.NumberOfDesks;
                existing.NumberOfComputers = infrastructureRequest.NumberOfComputers;
                existing.HasMeetingRooms = infrastructureRequest.HasMeetingRooms;
                existing.DepartmentId = infrastructureRequest.DepartmentId;

                infrastructureDal.Update(existing);
                return Results.NoContent();
            });

            app.MapDelete("/infrastructure/{id:int}", ([FromServices] DAL<Infrastructure> infrastructureDal, int id) =>
            {
                var infrastructure = infrastructureDal.ReadBy(i => i.InfrastructureId == id);
                if (infrastructure is null)
                    return Results.NotFound();

                infrastructureDal.Delete(infrastructure);
                return Results.NoContent();
            });
        }
    }
}