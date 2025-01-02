using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace AppointmentsAPI.Context;

public class DataSeeder
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;

    public DataSeeder(
	    UserManager<IdentityUser> userManager,
	    RoleManager<IdentityRole> roleManager,
	    IConfiguration config)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
    }

    public async Task SeedDataAsync()
    {
	var adminEmail = _config.GetValue<string>("ADMIN_EMAIL");
	var adminUsername = _config.GetValue<string>("ADMIN_USERNAME");
	var adminPassword = _config.GetValue<string>("ADMIN_PASSWORD");

	// Ensure the roles are created 
	if (!await _roleManager.RoleExistsAsync("Admin"))
	{
	    await _roleManager.CreateAsync(new IdentityRole("Admin")); 
	} 
	if (!await _roleManager.RoleExistsAsync("User"))
	{
	    await _roleManager.CreateAsync(new IdentityRole("User")); 
	} 
	// Ensure the admin user is created 

	if (await _userManager.FindByEmailAsync(adminEmail!) == null)
	{
	    var adminUser = new IdentityUser 
	    { 
		Email = adminEmail,
		UserName = adminUsername,
		EmailConfirmed = true 
	    };
	    var result = await _userManager.CreateAsync(
		    adminUser,
		    adminPassword!
		);
	    if (result.Succeeded) 
	    {
		await _userManager.AddToRoleAsync(adminUser, "Admin");
		await _userManager.AddClaimAsync(adminUser, new Claim("Role", "Admin")); 
	    }
	}
    }
}
