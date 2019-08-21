namespace Utility
{
    using Domain.Entities.General;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using System.Web.Configuration;

    public static class Utilities
    {   
        public static async Task EnviarCorreo(SendEmail email)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(email.To));
            message.From = new MailAddress(WebConfigurationManager.AppSettings["AdminUser"]);
            message.Subject = email.Subject;
            message.Body = email.Body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credentials = new NetworkCredential
                {
                    UserName = WebConfigurationManager.AppSettings["AdminUser"],
                    Password = WebConfigurationManager.AppSettings["AdminPassWord"]
                };
                smtp.Credentials = credentials;
                smtp.Host = WebConfigurationManager.AppSettings["SMTPName"];
                smtp.Port = int.Parse(WebConfigurationManager.AppSettings["SMTPPort"]);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);

            }
        }

    }
}
