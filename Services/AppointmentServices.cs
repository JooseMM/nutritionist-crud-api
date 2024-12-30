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

	    if(rawResult.Count > 0)
	    {
		var appointmentList = rawResult
		    .Select(element => _mapper.Map<AppointmentResponse>(element))
		    .ToList();
		return ResponseResult<List<AppointmentResponse>>
			.Success(appointmentList, (int)HttpStatusCode.OK);
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
	// apply changes, this returns the number of sucessful changes in newData.ase
	var result = await _context.SaveChangesAsync() > 0; 
	// handle the result
	return result
	    ? ResponseResult<Guid>.Success(appointment.PublicId, (int)HttpStatusCode.Created)
	    : ResponseResult<Guid>.Failure("No se pudo insertar la cita", (int) HttpStatusCode.GatewayTimeout);
    }

    public async Task<ResponseResult<bool>> DeleteOneAsync(Guid id)
    {
	// Check if the appointment exists
	var targetToRemove = await _context.Appointments!.FirstOrDefaultAsync(appointment => appointment.Id == id);
	// Check validation
	if (targetToRemove is null)
	{
	    return ResponseResult<bool>.Failure("No se encontro la cita", (int)HttpStatusCode.NotFound);
	}
	// Mark for future remove
	_context.Appointments!.Remove(targetToRemove);
	// Apply changes to newData.ase
	var result = await _context.SaveChangesAsync() > 0;
	// Handle result
	return result 
	    ? ResponseResult<bool>.Success(true, (int)HttpStatusCode.NoContent)
	    : ResponseResult<bool>.Failure("No se pudo eliminar la cita", (int)HttpStatusCode.InternalServerError);
    }

    public async Task<ResponseResult<AppointmentResponse>> GetOneAsync(Guid id)
    {
	var queryResult = await _context.Appointments!.FirstOrDefaultAsync(row => row.Id == id);
	if(queryResult is not null)
	{
	    var response = _mapper.Map<AppointmentResponse>(queryResult);
	    return ResponseResult<AppointmentResponse>.Success(response, (int) HttpStatusCode.OK);
	}
	return ResponseResult<AppointmentResponse>.Failure("No se pudo encontrar el recurso", (int)HttpStatusCode.NotFound);
    }

    public async Task<ResponseResult<AppointmentResponse>> GetOneByPublicIdAsync(Guid publicId)
    {
	var queryResult = await _context.Appointments!.FirstOrDefaultAsync(row => row.PublicId == publicId);
	if(queryResult is not null)
	{
	    // Map the Dto to an Appointment class using AutoMapper
	    var response = _mapper.Map<AppointmentResponse>(queryResult);
	    return ResponseResult<AppointmentResponse>.Success(response, (int) HttpStatusCode.OK);
	}
	return ResponseResult<AppointmentResponse>.Failure("No se pudo encontrar el recurso que corresponda con el public id proporcionado", (int)HttpStatusCode.NotFound);
    }
    public async Task<ResponseResult<AppointmentResponse>> UpdateOneAsync(AppointmentUpdateRequest newData)
    {
	var oldData = await _context.Appointments!.FirstOrDefaultAsync(row => row.Id == newData.Id);

	if(oldData is null)
	{
	    return ResponseResult<AppointmentResponse>
		.Failure("No se pudo encontrar el recurso que corresponda con el id proporcionado", (int)HttpStatusCode.NotFound);
	}
	    // update the appointment object
	    var updatedAppointment = UpdateAppointmentData(newData, oldData);
	    // mark it as a modified entity
	    _context.Entry(updatedAppointment).State = EntityState.Modified;
	    // apply changes, this returns the number of sucessful changes in newData.ase
	    var result = await _context.SaveChangesAsync() > 0; 
	    // Map the Dto to an Appointment class using AutoMapper
	    var response = _mapper.Map<AppointmentResponse>(updatedAppointment);
	    return result 
		    ? ResponseResult<AppointmentResponse>
			.Success(response, (int)HttpStatusCode.OK)
		    : ResponseResult<AppointmentResponse>
			.Failure(
			    "No se pudo modificar la base de datos",
			    (int)HttpStatusCode.InternalServerError
			);
    }
    private static Appointment UpdateAppointmentData(AppointmentUpdateRequest newData, Appointment oldData)
    {
	    oldData.ClientName = newData.ClientName;
	    oldData.ClientEmail = newData.ClientEmail!;
	    oldData.ClientAge = newData.ClientAge;
	    oldData.ClientRUT = newData.ClientRUT!;
	    oldData.ClientPhone = newData.ClientPhone!;
	    oldData.IsCompleted = newData.IsCompleted!;
	    oldData.PrevDiagnostic = newData.PrevDiagnostic!;
	    oldData.AppointmentDateTime = newData.AppointmentDateTime!;
	    oldData.UpdateDateTime = DateTime.UtcNow;

	    return oldData;
    }
}
