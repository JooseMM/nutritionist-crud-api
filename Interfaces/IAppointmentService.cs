using AppointmentsAPI.Core;
using AppointmentsAPI.Models;
using AppointmentsAPI.Models.ResponseDtos;
using AppointmentsAPI.Models.ResquestDtos;

namespace AppointmentsAPI.Interfaces;

public interface IAppointmentService
{
    Task<ResponseResult<List<AppointmentResponse>>> GetAllAsync();
    Task<Appointment> GetOneAsync(Guid Id);
    Task<Appointment> GetOneByTrackinIDAsync(Guid TrackingID);
    Task<Appointment> UpdateOneAsync(Appointment newAppointment);
    Task<ResponseResult<Guid>> CreateOneAsync(AppointmentRequest newAppointment);
    Task<ResponseResult<bool>> DeleteOneAsync(Guid ID);
}
