using AppointmentsAPI.Models;

namespace AppointmentsAPI.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}

