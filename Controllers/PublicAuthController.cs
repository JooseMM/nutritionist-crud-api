using System.Net;
using AppointmentsAPI.Core;
using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Models.ResquestDtos;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class PublicAuthController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public PublicAuthController(IAuthenticationService authenticationService)
    {
	_authService = authenticationService;
    }

    [HttpPost("/email/{emailCode}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ResponseResult<bool>>> VerifyEmail(Guid emailCode)
    {
	var result = await _authService.EmailVerication(emailCode);
	return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpPost("/login")]
    public async Task<ActionResult<ResponseResult<string>>> Login(LoginRequest request)
    {
	var response = await _authService.Login(request);

	return response.IsSuccess
		    ? Ok(response)
		    : Unauthorized(response);
    }
}
