using BlogApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);

var app = builder.Build();

await app.InitialiseDatabaseAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
