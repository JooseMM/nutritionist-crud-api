using System.Security.Authentication.ExtendedProtection;
using AppointmentsAPI.Context;
using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

    public async Task<List<Appointment>?> GetAllAsync()
    {
        try {
            var queryResult = await _context.Appointments!.ToListAsync();
            return queryResult;
        }
        catch
        {
            return null!;
        }
    }

    public async Task<AppointmentResponse?> CreateOneAsync(Appointment newAppointment)
    {
        try {
            await _context.AddAsync(newAppointment);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                var createdAppointment = _mapper.Map<AppointmentResponse>(newAppointment); 
                return createdAppointment;
            }
            else {
                return null;
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }

    public async Task<bool> DeleteOneAsync(Guid id)
    {
        // To Do: implement validation here
        var markToRemove = await _context.Appointments!.FindAsync(id);

        if(markToRemove is null)
        {
            return false;
        }

        _context.Appointments.Remove(markToRemove);
        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<Appointment?> GetOneAsync(Guid id)
    {
        // To Do: implement validation here
        var queryResult = await _context.Appointments!.FirstAsync(row => row.Id == id);
        return queryResult is null ? null : queryResult;
    }

    public async Task<Appointment?> GetOneByTrackinIDAsync(Guid trackingID)
    {
        // To Do: implement validation here
        var queryResult = await _context.Appointments!.FirstOrDefaultAsync(row => row.TrackingId == trackingID );
        return queryResult is null ? null : queryResult;
    }

    public Task<Appointment> UpdateOneAsync(Appointment newAppointment)
    {
        throw new NotImplementedException();
    }

    // private static Appointment CreateAppointment(AppointmentRequest newAppointmentData)
    // {
    //     // implement FluentValidation and Automapper
    //     var newId = Guid.NewGuid();
    //     var newTrackingId = Guid.NewGuid();
    //     var currentDateTime = DateTime.UtcNow;

    //     var newAppointment = new Appointment
    //     {
    //         Id = newId,
    //         TrackingId = newTrackingId,
    //         ClientName = newAppointmentData.ClientName,
    //         ClientAge = newAppointmentData.ClientAge,
    //         ClientRUT = newAppointmentData.ClientRUT,
    //         ClientEmail = newAppointmentData.ClientEmail,
    //         IsEmailVerified = false,
    //         PrevDiagnostic = newAppointmentData.PrevDiagnostic,
    //         Goals = newAppointmentData.Goals,
    //         ClientPhone = newAppointmentData.ClientPhone,
    //         CreationDateTime = currentDateTime,
    //         UpdateDateTime = currentDateTime,
    //         AppointmentDateTime = newAppointmentData.AppointmentDateTime,
    //         IsCompleted = false
    //     };

    //     return newAppointment;
    // }
}