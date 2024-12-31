using Microsoft.IdentityModel.Tokens;
using AppointmentsAPI.Context;
using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Middleware;
using AppointmentsAPI.Models.Validation;
using AppointmentsAPI.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AppointmentsAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

//Adding Fluent Validation to DI Container
builder.Services.AddFluentValidationAutoValidation(); 
builder.Services.AddValidatorsFromAssemblyContaining<AppointmentRequestValidation>();

// Adding AutoMapper to DI Container
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// add the context using SQL Server and setting the connection string
builder.Services.AddDbContext<ApplicationDbContext>(
	opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
		.LogTo(
		    Console.WriteLine,
		    new[] { DbLoggerCategory.Database.Command.Name },
		    Microsoft.Extensions.Logging.LogLevel.Information
		).EnableSensitiveDataLogging()
	);

/*
builder.Services.AddAuthentication(options => 
	{
	    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
	}).AddJwtBearer(options => {
	    options.TokenValidationParameters = new TokenValidationParameters
		{
		    ValidateIssuer = true,
		    ValidIssuer = builder.Configuration["JWT:Issuer"],
		    ValidateAudience = true,
		    ValidAudience = builder.Configuration["JWT:Audience"],
		    ValidateIssuerSigningKey = true,
		    IssuerSigningKey = new SymmetricSecurityKey(
			    System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]!)
			    )
		};
	    });
	    */
// register the service in the DI Container
// as a Transient lifecycle (each time is instatiated)
builder.Services.AddTransient<IAppointmentService, AppointmentService>(); 
builder.Services.AddTransient<IAuthenticationService, AuthenticationServices>(); 

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
