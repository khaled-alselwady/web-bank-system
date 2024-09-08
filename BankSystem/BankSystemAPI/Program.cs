using BankSystem.Business.Services;
using BankSystem.Mappers.PersonMappers;
using BankSystem.Validators.ClientValidators;
using BankSystem.Validators.PersonValidators;
using BankSystemBusiness.Services;
using BankSystemDataAccess.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
        options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

// Configure Serilog
{
    var eventSourceName = builder.Configuration["Serilog:WriteTo:0:Args:source"];
    // Ensure the event source exists before setting up Serilog
    EnsureEventSourceExists(eventSourceName); // Ensure this matches the source name in your appsettings.json

    Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Read Serilog configuration from appsettings.json
    .CreateLogger();

    builder.Host.UseSerilog();
}

// Add AutoMapper services
{
    builder.Services.AddAutoMapper(typeof(PersonMapper).Assembly);
}

// Add FluentValidation services
{
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddFluentValidationClientsideAdapters();

    builder.Services.AddValidatorsFromAssemblyContaining<PersonValidator>();
    builder.Services.AddValidatorsFromAssemblyContaining<ClientValidator>();
    builder.Services.AddValidatorsFromAssemblyContaining<UserService>();
}

// Add Entity Services
{
    builder.Services.AddScoped<PersonService>();
    builder.Services.AddScoped<ClientService>();
    builder.Services.AddScoped<UserService>();
}

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

// Helper method to ensure the event source exists
static void EnsureEventSourceExists(string sourceName)
{
    // Only create the event source if it doesn't exist
    if (!EventLog.SourceExists(sourceName))
    {
        EventLog.CreateEventSource(sourceName, "Application");
    }
}