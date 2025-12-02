using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Service;
using UserRepository;
using WebApiShop.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IUserRepositories, UserRepositories>();

builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddScoped<IPasswordServices, PasswordServices>();

builder.Services.AddScoped<IUsersController, UsersController>();

builder.Services.AddScoped<IPasswordController, PasswordController>();

builder.Services.AddDbContext<ShopContext>(option => option.UseSqlServer(
    "Data Source=srv2\\pupils;Initial Catalog=Shop;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
