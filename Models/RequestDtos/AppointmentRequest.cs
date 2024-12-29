namespace AppointmentsAPI.Models.ResquestDtos;

public class AppointmentRequest
{
    public required string ClientName { get; set; }
    public required int ClientAge { get; set; }
    public required string? ClientRUT { get; set; }
    public required string? ClientEmail { get; set; }
    public required string? ClientPhone { get; set; }
    public required string? Goals { get; set; }
    public required string? PrevDiagnostic { get; set; }
    public required DateTime AppointmentDateTime { get; set; }
}
