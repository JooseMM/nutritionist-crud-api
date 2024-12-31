using AppointmentsAPI.Models.ResponseDtos;
using AppointmentsAPI.Models.ResquestDtos;
using AutoMapper;

namespace AppointmentsAPI.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Appointment, AppointmentResponse>();
        CreateMap<AppointmentRequest, Appointment>();
        CreateMap<RegisterAppUser, AppUser>();
        CreateMap<AppUser, UserResponse>();
    }
}
