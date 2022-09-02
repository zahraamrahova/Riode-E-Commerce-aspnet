using System.Net;
using System.Net.Mail;

namespace Riode.WebUI.AppCode.Extensions
{
    static public partial class Extension
    {
        static public bool SendEmail(this IConfiguration _configuration, string to, string subject, string body, bool appendCC=false)
        {
            try
            {
                string fromMail = _configuration["emailAccount:userName"];
                string displayName = _configuration["emailAccount:displayName"];
                string smtpServer = _configuration["emailAccount:smtpServer"];
                int smtpPort = Convert.ToInt32(_configuration["emailAccount:smtpPort"]);
                string password = _configuration["emailAccount:password"];
                string cc = _configuration["emailAccount:cc"];
                using (MailMessage message = new MailMessage(new MailAddress(fromMail, displayName), new MailAddress(to))
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    if (!string.IsNullOrEmpty(cc) && appendCC)
                        message.CC.Add(cc);
                    SmtpClient smptClient = new SmtpClient(smtpServer, smtpPort);
                    smptClient.UseDefaultCredentials = false;
                    smptClient.Credentials = new NetworkCredential(fromMail, password);
                    smptClient.EnableSsl = true;
                    smptClient.Send(message);
                }
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }
    }
}
