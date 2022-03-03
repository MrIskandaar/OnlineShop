global using MediatR;
global using Microsoft.AspNetCore.Authorization; //for [Authorize] Attribute
global using Microsoft.AspNetCore.Mvc; // for ControllerBase
using Microsoft.AspNetCore.Identity;
using Root;
using Root.Extensions;
using System.Reflection;
using static OnlineShop.Logics.CategoryManager.Commands.AddCategoryCommand;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<ShopContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureJwtAuthService();
// Add services to the container.
builder.Services.AddScoped<ShopContext, ShopContext>();
//AddNewtonsoftJson is from Microsoft.AspNetCore.Mvc.NewtonsoftJson package
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddPostgresDbContext(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.SetSwaggerGen(Assembly.GetExecutingAssembly().GetName().Name);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(typeof(AddCategoryCommandHandler).Assembly);
builder.Services.SetCors();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(a =>
    {
        a.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Shop API");
    });
}
app.SetCors();

app.UpdateDatabase();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.UseHttpsRedirection();

app.MapControllers();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();