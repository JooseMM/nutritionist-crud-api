using Microsoft.AspNetCore.Identity;

namespace AppointmentsAPI.Interfaces;

public interface ITokenService
{
    string CreateToken(IdentityUser user);
}

