using AppointmentsAPI.Core;
using AppointmentsAPI.Models.ResponseDtos;
using AppointmentsAPI.Models.ResquestDtos;

namespace AppointmentsAPI.Interfaces;

public interface IAppointmentService
{
    Task<ResponseResult<List<AppointmentResponse>>> GetAllAsync();
    Task<ResponseResult<AppointmentResponse>> GetOneAsync(Guid Id);
    Task<ResponseResult<AppointmentResponse>> GetOneByPublicIdAsync(Guid publicId);
    Task<ResponseResult<AppointmentResponse>> UpdateOneAsync(AppointmentUpdateRequest newAppointment);
    Task<ResponseResult<Guid>> CreateOneAsync(AppointmentRequest newAppointment);
    Task<ResponseResult<bool>> DeleteOneAsync(Guid ID);
}
