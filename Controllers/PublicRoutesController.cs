using System.Net;
using AppointmentsAPI.Core;
using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Models.ResquestDtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsAPI.Controllers;

[ApiController]
[Route("api/public/appointment")]
public class PublicRoutesController : ControllerBase
{
    private readonly IValidator<AppointmentRequest> _appointmentValidator;
    private readonly IAppointmentService _appointmentService;

    public PublicRoutesController(
        IValidator<AppointmentRequest> appointmentValidator,
        IAppointmentService appointmentService
	)
    {
        _appointmentValidator = appointmentValidator;
        _appointmentService = appointmentService;
    }


    [HttpGet("{publicId}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ResponseResult<bool>>> GetOneByCheckingId(Guid publicId)
    {
	var result = await _appointmentService.GetOneByPublicIdAsync(publicId);
	return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ResponseResult<Guid>>> Create(
	    [FromBody] AppointmentRequest appointmentRequest
	    )
    {
        // Validating data in Dto with Fluent Validation
        var validation = await _appointmentValidator
				    .ValidateAsync(appointmentRequest);
	// Check validation and return if invalid
        if(!validation.IsValid)
	{
	    BadRequest(ResponseResult<Guid>
			.Failure(
			    validation.Errors.ToString()!,
			    (int)HttpStatusCode.BadRequest)
			); 
	}
	// call the create service
	var response = await _appointmentService
				.CreateOneAsync(appointmentRequest);
	// handle response
	return response.IsSuccess 
	    //? Ok(response.Value)
	    ? CreatedAtAction(
		    nameof(GetOneByCheckingId),
		    new { publicId = response.Data }, response)
	    : BadRequest();
    
    }
}
