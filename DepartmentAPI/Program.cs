using Department.Shared.Data;
using System.Text.Json.Serialization;
using DepartmentAPI.Extensions;
using Department.Shared.Model;
using Department.Shared.Data.Models;
using DepartmentAPI.Requests;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<DepartmentContext>();
builder.Services
    .AddIdentityApiEndpoints<AccessUser>()
    .AddEntityFrameworkStores<DepartmentContext>();
builder.Services.AddAuthorization();
builder.Services.AddTransient<DAL<DepartmentEntity>>();
builder.Services.AddTransient<DAL<Employee>>();
builder.Services.AddTransient<DAL<Project>>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 Rota Padrão
app.MapGet("/hello", () => "Hello World!");

// 🔹 Mapeamento dos Endpoints
app.MapDepartmentEndpoints();
app.MapEmployeeEndpoints();
app.MapProjectEndpoints();

// 🔹 Configuração de Autenticação e Autorização
app.MapGroup("auth")
   .MapIdentityApi<AccessUser>();

app.MapPost("auth/logout", async (HttpContext httpContext) =>
{
    var signInManager = httpContext.RequestServices.GetRequiredService<SignInManager<AccessUser>>();
    await signInManager.SignOutAsync();
    return Results.Ok();
})
.RequireAuthorization()
.WithTags("Authorization");

// 🔹 Swagger Configuration
app.UseSwagger();


app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DepAPIV0)");
    c.RoutePrefix = "";
});
// 🔹 Inicializa o aplicativo
app.Run();
