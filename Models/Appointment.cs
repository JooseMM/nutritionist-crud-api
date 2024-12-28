using System.IO.Compression;

namespace AppointmentsAPI.Models;

public class Appointment
{
   public required Guid Id {get;set;} = Guid.NewGuid();
   public required Guid TrackingId {get;set;} = Guid.NewGuid();
   public required string ClientName {get;set;}
   public required int ClientAge {get;set;}
   public required string ClientRUT {get;set;}
   public required string ClientEmail {get;set;}
   public required string ClientPhone {get;set;}
   public required string Goals {get;set;}
   public required bool IsEmailVerified {get;set;} = false;
   public required string PrevDiagnostic {get;set;}
   public required bool IsCompleted {get;set;} = false;
   public required DateTime CreationDateTime {get;set;} = DateTime.UtcNow;
   public required DateTime UpdateDateTime {get;set;} = DateTime.UtcNow;
   public required DateTime AppointmentDateTime {get;set;}
}