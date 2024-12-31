using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AppointmentsAPI.Services;

public class TokenServices : ITokenService
{
    private readonly JwtConfig _config;

    public TokenServices(JwtConfig config)
    {
        _config = config;
    }

    public string CreateToken(IdentityUser user)
    {
	var claims = new List<Claim>
	{
	    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
	    new Claim(JwtRegisteredClaimNames.Email, user.Email!),
	    new Claim(ClaimType.ROLE, CustomClaims.ADMIN)
	};
	var secretKey = new SymmetricSecurityKey(
		Encoding.UTF8.GetBytes(_config.SigningKey!)
		);
	var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
	var token = new JwtSecurityToken(
		issuer: "AppointmentsAPI",
		audience: "",
		claims: claims,
		expires: DateTime.UtcNow.AddDays(1),
		signingCredentials: creds
		);
	var tokeHandler =  new JwtSecurityTokenHandler();
	return tokeHandler.WriteToken(token);
    }
}
/*
IssuerSigningKey = new SymmetricSecurityKey(
System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]!)
			    )
			    */
