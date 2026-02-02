using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using Repositories;
using Service;
using WebApiShop.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddScoped<IPasswordServices, PasswordServices>();

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<ShopContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("School")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Host.UseNLog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
