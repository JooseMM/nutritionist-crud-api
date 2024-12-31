using AppointmentsAPI.Core;
using AppointmentsAPI.Models.ResponseDtos;
using AppointmentsAPI.Models.ResquestDtos;

namespace AppointmentsAPI.Interfaces;

public interface IAuthenticationService
{
    Task<ResponseResult<UserResponse>> Register(RegisterAppUser request);
    Task<ResponseResult<string>> Login(LoginRequest userData);
}
