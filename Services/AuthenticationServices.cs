using AppointmentsAPI.Context;
using AppointmentsAPI.Core;
using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Models.ResponseDtos;
using AppointmentsAPI.Models.ResquestDtos;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

    public async Task<ResponseResult<UserResponse>> Register(RegisterAppUser registerRequest)
    {
	// To do: later
	throw new Exception();
    }

    public async Task<ResponseResult<string>> Login(LoginRequest userData)
    {
	var user = await _userManager.FindByEmailAsync(userData.Email!);
	if(user == null || !await _userManager.CheckPasswordAsync(user, userData.Password!))
	{
	    return ResponseResult<string>.Failure("Usuario no encontrado", (int)HttpStatusCode.Unauthorized);
	}

	var token = _tokenService.CreateToken(user!);

	if(String.IsNullOrEmpty(token))
	{
	    return ResponseResult<string>.Failure("creacion de jwt fallida", (int)HttpStatusCode.InternalServerError);
	}
	return ResponseResult<string>.Success(token, (int)HttpStatusCode.OK);
    }
}
