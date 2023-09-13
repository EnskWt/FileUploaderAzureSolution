using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FileUploaderAzure.Functions.ServiceContracts;

namespace FileUploaderAzure.Functions.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public Task SendEmail(string email, string blobUri)
        {
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("testtask.dummy@outlook.com", "SuperStrongPassword");
            client.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("testtask.dummy@outlook.com");
            mailMessage.To.Add(email);
            mailMessage.Body = $"Your file is successfully uploaded. You can download it from this link: {blobUri}";
            mailMessage.Subject = "Your file is uploaded";
            client.Send(mailMessage);

            return Task.CompletedTask;
        }
    }
}
