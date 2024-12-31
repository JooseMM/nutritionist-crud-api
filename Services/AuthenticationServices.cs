using AppointmentsAPI.Context;
using AppointmentsAPI.Core;
using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Models;
using AppointmentsAPI.Models.ResponseDtos;
using AppointmentsAPI.Models.ResquestDtos;
using AutoMapper;
using System.Net;

namespace AppointmentsAPI.Services;

public class AuthenticationServices : IAuthenticationService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AuthenticationServices(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseResult<UserResponse>> Register(RegisterAppUser registerRequest)
    {
	registerRequest.Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

	var appUser = _mapper.Map<AppUser>(registerRequest);
	_context.Users!.Add(appUser);

	if(await _context.SaveChangesAsync() > 0)
	{
	    var response = _mapper.Map<UserResponse>(appUser);
	    return ResponseResult<UserResponse>
			.Success(response, (int)HttpStatusCode.OK);
	}
	return ResponseResult<UserResponse>
		.Failure(
		    "No se pudo modificar la base de datos",
		    (int)HttpStatusCode.InternalServerError
		);
    }
}
