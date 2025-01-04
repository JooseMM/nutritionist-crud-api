using AppointmentsAPI.Core;
using AppointmentsAPI.Models.ResquestDtos;

namespace AppointmentsAPI.Interfaces;

public interface IAuthenticationService
{
    Task<ResponseResult<string>> Login(LoginRequest userData);
    Task<ResponseResult<string>> EmailVerication(EmailVerificationRequest request);
}
