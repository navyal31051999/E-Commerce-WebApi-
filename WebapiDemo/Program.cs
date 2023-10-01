using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using Core.Entities;
using WebapiDemo.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using WebapiDemo.Middleware;
using Microsoft.AspNetCore.Mvc;
using WebapiDemo.Errors;
using WebapiDemo.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddswaggerDocumentions();

builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddApplicationServices();
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.


app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();
app.UseSwaggerDocumentation();

app.MapControllers();


using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var loggerFactory = services.GetRequiredService<ILoggerFactory>();
var logger = loggerFactory.CreateLogger<Program>();
try
{
    await context.Database.MigrateAsync();
    // await identityContext.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context, loggerFactory);
    // await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}
app.Run();