using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Infrastructure.Data;
using API.Helpers;
using API.Middleware;
using Microsoft.AspNetCore.Mvc;
using API.Errors;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using API.Extenstions;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var services = builder.Services;

// Add services to the container.
services.AddAutoMapper(typeof(MappingProfiles).Assembly);
services.AddControllers();
services.AddDbContext<StoreContext>(opts =>
    opts.UseSqlServer(config.GetConnectionString("DefaultConnection"))
);
services.AddApplicationServices();
services.AddSwaggerDocumentation();


var app = builder.Build();

app.AddSeedData();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
