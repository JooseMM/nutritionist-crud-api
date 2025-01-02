using AppointmentsAPI.Models;

namespace AppointmentsAPI.Interfaces;

public interface IEmailService 
{
    public Task SendEmailVerificationCode(Appointment appointmentData);
}
