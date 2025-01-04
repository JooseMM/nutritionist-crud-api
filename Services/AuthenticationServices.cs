using AppointmentsAPI.Context;
using AppointmentsAPI.Core;
using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Models.ResponseDtos;
using AppointmentsAPI.Models.ResquestDtos;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AppointmentsAPI.Services;

public class AuthenticationServices : IAuthenticationService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthenticationServices(
	    ApplicationDbContext context,
	    IMapper mapper,
	    ITokenService tokenService,
	    UserManager<IdentityUser> userManager
	    )
    {
        _context = context;
        _mapper = mapper;
	_tokenService = tokenService;
	_userManager = userManager;
    }

    public async Task<ResponseResult<string>> Login(LoginRequest userData)
    {
	var user = await _userManager.FindByEmailAsync(userData.Email!);
	if(user == null || !await _userManager.CheckPasswordAsync(user, userData.Password!))
	{
	    Console.Write("user is: " + user!.Email);
	    return ResponseResult<string>.Failure("Usuario no encontrado", (int)HttpStatusCode.Unauthorized);
	}

	var token = _tokenService.CreateToken(user!);

	if(String.IsNullOrEmpty(token))
	{
	    return ResponseResult<string>.Failure("creacion de jwt fallida", (int)HttpStatusCode.InternalServerError);
	}
	return ResponseResult<string>.Success(token, (int)HttpStatusCode.OK);
    }

    public async Task<ResponseResult<string>> EmailVerication(EmailVerificationRequest request)
    {

	var user = await _context.Appointments!
			    .FirstOrDefaultAsync(appointment => 
				appointment.EmailVerificationCode == request.EmailVerification && appointment.IsEmailVerified == false
				);
	if(user is null)
	{
	    return ResponseResult<string>
		    .Failure("Codigo erroneo", (int)HttpStatusCode.NotFound);
	}
	user.IsEmailVerified = true; 
	_context.Entry(user).State = EntityState.Modified;
	var result = await _context.SaveChangesAsync() > 0;
	return result 
		? ResponseResult<string>
		    .Success("Usuario validado", (int)HttpStatusCode.OK)
		: ResponseResult<string>
		    .Failure(
			"No se pudo validar el usuario",
			(int)HttpStatusCode.InternalServerError
		    );
		
    }
}
