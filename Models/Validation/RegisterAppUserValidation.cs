using AppointmentsAPI.Models.ResquestDtos;
using FluentValidation;

namespace AppointmentsAPI.Models.Validation;

public class RegisterAppUserValidation : AbstractValidator<RegisterAppUser>
{
    public RegisterAppUserValidation()
    {
        RuleFor(user => user.Name)
            .Matches(@"^[a-zA-Z\s]+$")
	    .WithMessage("El nombre es requerido y debe de contener solo letras");
        RuleFor(user => user.UserName)
	    .NotEmpty()
	    .WithMessage("El nombre de usuario es requerido y debe de contener solo letras");
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Correo Electronico Requerido")
            .EmailAddress().WithMessage("Formato de correo electronico invalido");
        RuleFor(user => user.Career)
            .Matches(@"^[a-zA-Z\s]+$")
	    .WithMessage("La carrera es requerida y debe de contener solo letras");
	RuleFor(user => user.Password)
	    .NotEmpty()
	    .WithMessage("La contraseña no puede estar vacia");
	RuleFor(user => user.Phone)
            .Matches(@"^\d{9}$")
	    .WithMessage("Formato de numero telefonico invalido");
    }
}
