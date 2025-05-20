using DepartmentAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 Rota Padrão
app.MapGet("/", () => "Hello World!");

// 🔹 Mapeamento dos Endpoints
app.MapDepartmentEndpoints();
app.MapEmployeeEndpoints();
app.MapProjectEndpoints();

//app.MapGroup("auth")
//    .MapIdentityApi<AccessUser>();

// 🔹 Swagger Configuration
app.UseSwagger();


app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DepAPIV0)");
    c.RoutePrefix = "";
});
// 🔹 Inicializa o aplicativo
app.Run();
