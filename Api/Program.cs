using WebFinal.Data;
using Microsoft.EntityFrameworkCore;
using Api.Services;
using Api.Interfaces;
using WebFinal.Data.Manager;
using WebFinal.Data.Base;
using WebFinal.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHealthChecks();
// Controlling reference loop 
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Web";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();

ApplicationDbContext.ConnectionString = builder.Configuration.GetConnectionString("WebFinalContext");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
