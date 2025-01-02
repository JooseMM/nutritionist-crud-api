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

	var target = appointmentData.ClientEmail;
	var subject = "Confirmacion de Cita [Prueba]";
	var body = $"Hola {appointmentData.ClientName}, esto es una prueba de confirmacion de cita";

	var smtpClient = new SmtpClient(emailSenderHost, emailSenderPort);
	smtpClient.EnableSsl = true;
	smtpClient.UseDefaultCredentials = false;
	smtpClient.Credentials = new NetworkCredential(emailSender, emailSenderPass);

	var message = new MailMessage(emailSender!, target, subject, body);
	await smtpClient.SendMailAsync(message);

    }
}
