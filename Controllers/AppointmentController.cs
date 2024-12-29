using System.Net;
using AppointmentsAPI.Core;
using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Models.ResponseDtos;
using AppointmentsAPI.Models.ResquestDtos;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentsAPI.Controllers;

[ApiController]
[Route("api/appointment")]
public class AppointmentController : ControllerBase
{
    private readonly IValidator<AppointmentRequest> _validator;
    private readonly IAppointmentService _appointmentService;
    private readonly IMapper _mapper;

    public AppointmentController(
        IValidator<AppointmentRequest> validator,
        IAppointmentService appointmentService,
        IMapper mapper)
    {
        _validator = validator;
        _appointmentService = appointmentService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<AppointmentResponse>>> GetAll()
    {
        return Ok(await _appointmentService.GetAllAsync());
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.GatewayTimeout)]
    public async Task<ActionResult<ResponseResult<Guid>>> Create([FromBody] AppointmentRequest appointmentRequest)
    {
        // Validating data in Dto with Fluent Validation
        var validation = await _validator.ValidateAsync(appointmentRequest);
	// Check validation and return if invalid
        if(!validation.IsValid)
	{
	    BadRequest(ResponseResult<Guid>
			.Failure(validation.Errors.ToString()!, (int)HttpStatusCode.BadRequest)); 
	}
	// call the create service
	var response = await _appointmentService.CreateOneAsync(appointmentRequest);
	// handle response
	return response.IsSuccess 
	    //? Ok(response.Value)
	    ? CreatedAtAction(nameof(GetOneByTrackingId), new { id = response.Data }, response)
	    : BadRequest();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseResult<bool>>> GetOneByTrackingId(Guid id)
    {
	throw new Exception();
    }
    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ResponseResult<bool>>> DeleteOne(Guid id)
    {
	// call the delete service and pass the target id
	var response = await _appointmentService.DeleteOneAsync(id);
	// handle result response
	return response.IsSuccess 
	    ? NoContent()
	    : BadRequest();
    }
}
