using System.IO.Compression;
using AppointmentsAPI.Context;
using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Models;
using AppointmentsAPI.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<AppointmentRequestValidation>();
builder.Services.AddFluentValidationAutoValidation(); // the same old MVC pipeline behavior
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// add the context using SQL Server and setting the connection string
builder.Services.AddDbContext<AppointmentsDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// register the service in the DI Container
// as a Transient lifecycle (each time is instatiated)
builder.Services.AddTransient<IAppointmentService, AppointmentServices>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();