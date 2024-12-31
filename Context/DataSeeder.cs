using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace AppointmentsAPI.Context;

public class DataSeeder
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DataSeeder(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedDataAsync()
    {
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
	var adminEmail = "admin@example.com";

	if (await _userManager.FindByEmailAsync(adminEmail) == null)
	{
	    var adminUser = new IdentityUser 
	    { 
		UserName = adminEmail,
		Email = adminEmail,
		EmailConfirmed = true 
	    };
	    var result = await _userManager.CreateAsync(
		    adminUser,
		    "AdminPassword123!"
		);
	    if (result.Succeeded) 
	    {
		await _userManager.AddToRoleAsync(adminUser, "Admin");
		await _userManager.AddClaimAsync(adminUser, new Claim("Role", "Admin")); 
	    }
	}
    }
}
