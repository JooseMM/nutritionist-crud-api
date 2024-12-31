using System.Net;
using AppointmentsAPI.Core;
using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Models.ResquestDtos;
using AppointmentsAPI.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsAPI.Controllers;

[Authorize(Policy = PolicyMaster.ADMIN_ONLY)]
[ApiController]
[Route("api/auth")]
public class AdminsitrationAuthController : ControllerBase
{
    private readonly IValidator<RegisterAppUser> _validator;
    private readonly IAuthenticationService _authService;

    public AdminsitrationAuthController(
	    IValidator<RegisterAppUser> validator,
	    IAuthenticationService authenticationService
	    )
    {
	_validator = validator;
	_authService = authenticationService;
    }

    [HttpPost("admin")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    public async Task<ActionResult<ResponseResult<bool>>> RegisterAdmin(
	    [FromBody] RegisterAppUser user
	    )
    {
        // Validating data in Dto with Fluent Validation
        var validation = await _validator
				    .ValidateAsync(user);
	// Check validation and return if invalid
        if(!validation.IsValid)
	{
	    BadRequest(ResponseResult<bool>
			.Failure(
			    validation.Errors.ToString()!,
			    (int)HttpStatusCode.BadRequest)
			); 
	}
	// call the create service
	var response = await _authService
				.Register(user);
	// handle response
	return response.IsSuccess 
	    ? Ok(response)
	    : BadRequest(response);
    }
}
