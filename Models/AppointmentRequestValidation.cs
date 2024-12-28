using System.Data;
using System.Globalization;
using FluentValidation;

namespace AppointmentsAPI.Models;

public class AppointmentRequestValidation : AbstractValidator<AppointmentRequest>
{
    public AppointmentRequestValidation()
    {
        RuleFor(appointment => appointment.ClientAge).InclusiveBetween(1,100);

        RuleFor(appointment => appointment.ClientName)
            .Matches(@"^[a-zA-Z]+$").WithMessage("El nombre es requerido y debe de contener solo letras");

        RuleFor(appointment => appointment.ClientEmail)
            .NotEmpty().WithMessage("Correo Electronico Requerido")
            .EmailAddress().WithMessage("Formato de correo electronico invalido");

        RuleFor(appointment => appointment.ClientPhone)
            .Matches(@"^\d{10}$").WithMessage("Formato de correo electronico invalido");

        RuleFor(appointment => appointment.ClientRUT)
            .Matches(@"^\d{1,2}\d{3}\d{3}-[0-9kK]$").WithMessage("Formato de RUT invalido");

        RuleFor(appointment => appointment.AppointmentDateTime)
            .GreaterThan(DateTime.Today)
            .WithMessage("Formato de fecha invalido, solo puedes agendar en fechas futuras");

        RuleFor(appointment => appointment.Goals)
            .NotEmpty().WithMessage("")
            .MaximumLength(255).WithMessage("Las metas no pueden pasar de 255 caracteres");

        RuleFor(appointment => appointment.PrevDiagnostic)
            .NotEmpty().WithMessage("")
            .MaximumLength(255).WithMessage("Los diagnosticos previos no pueden pasar de 255 caracteres");
        
            
            
    }
}