using AppointmentsAPI.Context;
using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Middleware;
using AppointmentsAPI.Models.Validation;
using AppointmentsAPI.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

//Adding Fluent Validation to DI Container
builder.Services.AddFluentValidationAutoValidation(); 
builder.Services.AddValidatorsFromAssemblyContaining<AppointmentRequestValidation>();

// Adding AutoMapper to DI Container
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// add the context using SQL Server and setting the connection string
builder.Services.AddDbContext<AppointmentsDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// register the service in the DI Container
// as a Transient lifecycle (each time is instatiated)
builder.Services.AddTransient<IAppointmentService, AppointmentServices>(); 

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(opts => 
	    {
		opts.SwaggerEndpoint("/openapi/v1.json", "Swagger Demo");
	    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
