using ReviewHubAPI.Extensions;
using ReviewHubAPI.Middleware;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Repositories;
using ReviewHubAPI.Services.Interface;
using ReviewHubAPI.Services;
using Serilog;
using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Mappers;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;
using ReviewHubAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// serilog
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

// Dependency Injection Reg
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMapper<UserEntity, UserDTO>, UserMapper>();

// Middleware, Extensions
builder.Services.AddTransient<GlobalExceptionMiddleware>();
builder.RegisterMappers();

// EF Database Config
builder.Services.AddDbContext<ReviewHubDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0))));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
