using System.ComponentModel.DataAnnotations;

namespace AppointmentsAPI.Models;

public class AppointmentRequest
{
    [Required]
    public string? ClientName { get; set; }
    [Required]
    public int ClientAge { get; set; }
    [Required]
    public string? ClientRUT { get; set; }
    [Required]
    public string? ClientEmail { get; set; }
    [Required]
    public string? ClientPhone { get; set; }
    [Required]
    public string? Goals { get; set; }
    [Required]
    public string? PrevDiagnostic { get; set; }
    [Required]
    public DateTime AppointmentDateTime { get; set; }
}