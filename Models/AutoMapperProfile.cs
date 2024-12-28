using AutoMapper;

namespace AppointmentsAPI.Models;

public class AutoMapperProfile : Profile
{
    protected AutoMapperProfile()
    {
        CreateMap<Appointment, AppointmentResponse>();
        CreateMap<AppointmentRequest, Appointment>();
    }
}