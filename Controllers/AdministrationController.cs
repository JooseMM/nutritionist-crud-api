using System.Net;
using AppointmentsAPI.Core;
using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Models.ResponseDtos;
using AppointmentsAPI.Models.ResquestDtos;
using AppointmentsAPI.Utils;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsAPI.Controllers;

[Authorize(Policy = PolicyMaster.ADMIN_ONLY)]
[ApiController]
[Route("api/appointment")]
public class AppointmentController : ControllerBase
{
    private readonly IValidator<AppointmentUpdateRequest> _appointmentUpdateValidator;
    private readonly IAppointmentService _appointmentService;
    private readonly IMapper _mapper;

    public AppointmentController(
        IValidator<AppointmentUpdateRequest> appointmentUpdateValidator,
        IAppointmentService appointmentService,
        IMapper mapper)
    {
        _appointmentUpdateValidator = appointmentUpdateValidator;
        _appointmentService = appointmentService;
        _mapper = mapper;
    }

    [HttpGet("admin")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<AppointmentResponse>>> GetAll()
    {
        return Ok(await _appointmentService.GetAllAsync());
    }

    [HttpGet("admin/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ResponseResult<bool>>> GetOne(Guid id)
    {
	var result = await _appointmentService.GetOneAsync(id);
	return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpPut("admin")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ResponseResult<Guid>>> UpdateOne(
	    [FromBody] AppointmentUpdateRequest updateAppointment
	    )
    {
	// Check if is a valid appointment
        var validation = await _appointmentUpdateValidator
				    .ValidateAsync(updateAppointment);
	// Check validation and return if invalid
        if(!validation.IsValid)
	{
	    BadRequest(ResponseResult<Guid>
			.Failure(
			    validation.Errors.ToString()!,
			    (int)HttpStatusCode.BadRequest)
			); 
	}
	var result = await _appointmentService
				.UpdateOneAsync(updateAppointment);
	return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpDelete("admin/{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ResponseResult<HttpStatusCode>>> DeleteOne(Guid id)
    {
	// call the delete service and pass the target id
	var response = await _appointmentService.DeleteOneAsync(id);
	// handle result response
	return response.IsSuccess ? NoContent() : NotFound(response);
    }
}
