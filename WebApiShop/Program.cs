using Microsoft.Extensions.DependencyInjection;
using Service;
using UserRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IUserRepositories, UserRepositories>();

builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddScoped<IPasswordServices, PasswordServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
