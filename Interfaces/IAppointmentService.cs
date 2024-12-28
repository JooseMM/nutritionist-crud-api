using AppointmentsAPI.Models;

namespace AppointmentsAPI.Interfaces;

public interface IAppointmentService
{
    Task<List<Appointment>?> GetAllAsync();
    Task<Appointment?> GetOneAsync(Guid Id);
    Task<Appointment?> GetOneByTrackinIDAsync(Guid TrackingID);
    Task<Appointment> UpdateOneAsync(Appointment newAppointment);
    Task<AppointmentResponse?> CreateOneAsync(AppointmentRequest newAppointment);
    Task<bool> DeleteOneAsync(Guid ID);
}