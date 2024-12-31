using AppointmentsAPI.Context;
using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Middleware;
using AppointmentsAPI.Models.Validation;
using AppointmentsAPI.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AppointmentsAPI.Utils;
using Microsoft.Extensions.Options;

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

// Add the ASP.NET Identity services
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

// add the options to handle Authentication with JWT
builder.Services.AddAuthentication(options => 
	{
	    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

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

// registering the configuration class
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JWT"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtConfig>>().Value);
// add authorization policies
builder.Services.AddAuthorization(options => 
	{
	    options.AddPolicy(PolicyMaster.ADMIN_ONLY, policy =>
		    policy.RequireClaim(ClaimType.ROLE, CustomClaims.ADMIN));
	});
// register the service in the DI Container
// as a Transient lifecycle (each time is instatiated)
builder.Services.AddTransient<IAppointmentService, AppointmentService>(); 
builder.Services.AddTransient<IAuthenticationService, AuthenticationServices>(); 
builder.Services.AddTransient<ITokenService, TokenServices>(); 

builder.Services.AddTransient<DataSeeder>();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<DataSeeder>();
    await seeder.SeedDataAsync();
}

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
