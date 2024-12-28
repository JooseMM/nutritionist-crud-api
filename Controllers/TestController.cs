using AppointmentsAPI.Context;
using AppointmentsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsAPI.Controllers;

[ApiController]
[Route("api/")]
public class TestController : ControllerBase
{
    private readonly AppointmentsDbContext _context;

    public TestController(AppointmentsDbContext context)
    {
        _context = context;
    }

    [HttpGet("test")]
    public async Task<ActionResult<List<Appointment>>> Test()
    {
        var newAppointment = new Appointment
        {
            Id = Guid.NewGuid(),
            ClientName = "Jose Moreno",
            ClientAge = 24,
            ClientRUT = "26702868-K",
            ClientEmail = "jamm.webdev@gmail.com",
            ClientPhone = "932445456",
            Goals = "Bajar de peso",
            IsEmailVerify = false,
            IsCompleted = false,
            Date = DateTime.UtcNow.AddDays(2).AddHours(15)

        };

        var appointmentList = await _context.AddAsync(newAppointment);
        var updateDbResult = await _context.SaveChangesAsync();
        if(updateDbResult > 0)
        {
            var getList = await _context.Appointments!.ToListAsync();
            return Ok(getList);
        }
        return NotFound("There are not items to show");
    }
}