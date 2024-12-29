using System.Net;
using AppointmentsAPI.Context;
using AppointmentsAPI.Core;
using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Models;
using AppointmentsAPI.Models.ResponseDtos;
using AppointmentsAPI.Models.ResquestDtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsAPI.Services;

public class AppointmentServices : IAppointmentService
{
    private readonly AppointmentsDbContext _context;
    private readonly IMapper _mapper;

    public AppointmentServices(AppointmentsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseResult<List<AppointmentResponse>>> GetAllAsync()
    {
            var rawResult = await _context.Appointments!.ToListAsync();
	    var data = rawResult
			    .Select(element => _mapper.Map<AppointmentResponse>(element))
			    .ToList();

	    if(rawResult.Count > 0)
	    {
		return ResponseResult<List<AppointmentResponse>>
			.Success(data, (int)HttpStatusCode.OK);
	    }
	    return ResponseResult<List<AppointmentResponse>>
		    .Success(new List<AppointmentResponse>(), (int)HttpStatusCode.OK);
    }

    public async Task<ResponseResult<Guid>> CreateOneAsync(AppointmentRequest newAppointment)
    {
	// Map the Dto to an Appointment class using AutoMapper
	var appointment = _mapper.Map<Appointment>(newAppointment);
	// Add mark to add 
	await _context.AddAsync(appointment);
	// apply changes, this returns the number of sucessful changes in database
	var result = await _context.SaveChangesAsync() > 0; 
	// handle the result
	return result
	    ? ResponseResult<Guid>.Success(appointment.TrackingId, (int)HttpStatusCode.Created)
	    : ResponseResult<Guid>.Failure("No se pudo insertar la cita", (int) HttpStatusCode.GatewayTimeout);
    }

    public async Task<ResponseResult<bool>> DeleteOneAsync(Guid id)
    {
	// Check if the appointment exists
	var targetToRemove = await _context.Appointments!.FindAsync(id);
	// Check validation
	if (targetToRemove is null)
	{
	    return ResponseResult<bool>.Failure("No se encontro la cita", (int)HttpStatusCode.NotFound);
	}
	// Mark for future remove
	_context.Appointments.Remove(targetToRemove);
	// Apply changes to database
	var result = await _context.SaveChangesAsync() > 0;
	// Handle result
	return result 
	    ? ResponseResult<bool>.Success(true, (int)HttpStatusCode.NoContent)
	    : ResponseResult<bool>.Failure("No se pudo eliminar la cita", (int)HttpStatusCode.GatewayTimeout);
    }

    public async Task<Appointment> GetOneAsync(Guid id)
    {
	// To Do: implement validation here
	//var queryResult = await _context.Appointments!.FirstAsync(row => row.Id == id);
	throw new Exception();
    }

    public async Task<Appointment> GetOneByTrackinIDAsync(Guid trackingID)
    {
	// To Do: implement validation here
	//var queryResult = await _context.Appointments!.FirstOrDefaultAsync(row => row.TrackingId == trackingID);
	throw new Exception();
    }

    public Task<Appointment> UpdateOneAsync(Appointment newAppointment)
    {
	throw new NotImplementedException();
    }
}
