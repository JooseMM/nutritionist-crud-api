using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Models;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public async Task<ActionResult<List<Appointment>>> GetAll()
    {
        var queryResult = await _appointmentService.GetAllAsync();
        if(queryResult is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,"Error ocurrido en la comunicacion con la base de datos");
        }
        return Ok(queryResult);
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentResponse>> Create([FromBody] AppointmentRequest appointmentRequest)
    {
        // Validating data in Dto with Fluent Validation
        var validation = await _validator.ValidateAsync(appointmentRequest);

        if(!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }

        // Map the Dto to an Appointment class using AutoMapper
        var newAppointment = _mapper.Map<Appointment>(appointmentRequest);

        var result = await _appointmentService.CreateOneAsync(newAppointment);

        if(result is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error ocurrido en la comunicacion con la base de datos");
        }
        return Ok(result);
    }
}