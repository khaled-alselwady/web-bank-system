using BankSystem.Business.Services;
using BankSystem.Mappers.PersonMappers;
using BankSystem.Validators.ClientValidators;
using BankSystem.Validators.PersonValidators;
using BankSystemBusiness.Services;
using BankSystemDataAccess.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;


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
}

// Add Entity Services
{
    builder.Services.AddScoped<PersonService>();
    builder.Services.AddScoped<ClientService>();
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
