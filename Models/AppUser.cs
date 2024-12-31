namespace AppointmentsAPI.Models;

public class AppUser 
{
    public required Guid Id {get;set;} = Guid.NewGuid();
    public required string? Name {get;set;}
    public required string? Username {get;set;}
    public required string? Email {get;set;}
    public required string? Phone {get;set;}
    public required string? Password {get;set;}
    public required string? Career {get;set;}
}
