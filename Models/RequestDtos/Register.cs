namespace AppointmentsAPI.Models.ResquestDtos;

public class RegisterAppUser
{
    public required string? UserName {get;set;}
    public required string? Name {get;set;}
    public required string? Password {get;set;}
    public required string? Email {get;set;}
    public required string? Career {get;set;}
    public required string? Phone {get;set;}
}
