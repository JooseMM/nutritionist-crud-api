using System.Net;
using System.Net.Mail;
using AppointmentsAPI.Interfaces;
using AppointmentsAPI.Models;

namespace AppointmentsAPI.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailVerificationCode(Appointment appointmentData)
    {
	var emailSender = _configuration.GetValue<string>("ADMIN_EMAIL");
	var emailSenderPass = _configuration.GetValue<string>("ADMIN_EMAIL_PASSWORD");
	var emailSenderHost = _configuration.GetValue<string>("EMAIL_HOST");
	var emailSenderPort = _configuration.GetValue<int>("EMAIL_PORT");
	string template = EmailTemplate(appointmentData);

	var target = appointmentData.ClientEmail;
	var subject = "Confirmacion de Cita [Prueba]";

	var smtpClient = new SmtpClient(emailSenderHost, emailSenderPort);
	smtpClient.EnableSsl = true;
	smtpClient.UseDefaultCredentials = false;
	smtpClient.Credentials = new NetworkCredential(emailSender, emailSenderPass);

	var message = new MailMessage(emailSender!, target, subject, template);
	message.IsBodyHtml = true;
	await smtpClient.SendMailAsync(message);

    }

    private string EmailTemplate(Appointment appointment)
    {
	return @$"
	    <!DOCTYPE html>
	    <html lang=""en"">
		{TemplateHeader()}
		{TemplateBody(appointment)}
	    </html>
	    ";
    }
    private string TemplateBody(Appointment appointment)
    {
	var chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
	var localDateTime = TimeZoneInfo.ConvertTimeFromUtc(appointment.AppointmentDateTime, chileTimeZone);
	var localDate = localDateTime.Date.ToString("d");
	var localTime = localDateTime.TimeOfDay.ToString(@"hh\:mm");

	return @$"
	<body>
	    <table style=""width:100%;max-width:550px;margin:auto;color:#162e2b;margin-top:4rem;"" width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"">
		<tr style=""background-color:#e0f3f7;"">
		    <td style=""font-size:16px;color:#162e2b;padding-left:2.5rem;padding-right:2.5rem;padding-top:0.8rem;padding-bottom:0.8rem;border-radius:5px;border:1px solid #9dadb0;"">
			<p>Estimado/a, <b>{appointment.ClientName}</b></p>
			<p>Espero que este mensaje le encuentre bien.</p>
			<p>Quisiera confirmar nuestra cita programada para el <b>{localDate}</b> a las <b>{localTime}</b>. Por favor, asegure su asistencia visitando el siguiente enlace.</p>
			<p>Si necesita modificar algo, no dude en contactarme.</p>
			<p>Saludos Cordiales</p>
			<p>Pia Ulloa</p>
		    </td> 
		</tr>
		<tr style=""margin:auto;text-align:center;"">
		    <td style=""padding-top:2.5rem;""Input string was not in a correct format.>
			<a style=""font-size:20px;background-color:#469e95;padding-left:2rem;padding-right:2rem;padding-top:1rem;padding-bottom:1rem;border-radius:50px;color:white;text-decoration:none;"" href=""http://localhost:5158/api/auth/email/{appointment.EmailVerificationCode}"" class=""cta"" target=""_blank"">Confirmar Reserva</a>
		    </td>
		</tr>
		<tr>
		    <td style=""padding-top:2rem;"">
			<p style=""margin:0;padding:0;""><b>Contacto:</b> (+56) 9 3454 4321</p>
			<p style=""margin:0;padding:0;margin-top:5px;""><b>Email:</b> pia.nutricionista@gmail.com</p>
		    </td>
		</tr>
	    </table>
	</body>
	    ";
    }
    private string TemplateHeader()
    {
	return @$"
	<head>
	    <meta charset=""UTF-8"">
	    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
	    <title>Email Template</title>
    </head>";
    }
}
