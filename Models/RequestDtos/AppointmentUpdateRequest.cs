namespace AppointmentsAPI.Models.ResquestDtos;

public class AppointmentUpdateRequest
{
    public required Guid Id {get;set;}
    public required Guid PublicId {get;set;}
    public required string ClientName { get; set; }
    public required int ClientAge { get; set; }
    public required string? ClientRUT { get; set; }
    public required string? ClientEmail { get; set; }
    public required string? ClientPhone { get; set; }
    public required string? Goals { get; set; }
    public required string? PrevDiagnostic { get; set; }
    public required DateTime AppointmentDateTime { get; set; }
    public required bool IsCompleted {get;set;}
}
