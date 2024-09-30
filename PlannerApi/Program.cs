using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
    new MySqlServerVersion(new Version(8, 0, 25))));

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapTarefaEndpoints();

app.Run();
