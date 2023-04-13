using MonitoringApp.UI.Interfaces;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace MonitoringApp.UI.InterfaceClasses
{
    public class Notify : INotify
    {
        public bool NotifyAppStatus(int IntegrationTypeId, string AppName, string NotifyList)
        {
            if (IntegrationTypeId == 1)
            {
                return SendMail(AppName, NotifyList);
            }
            return false;
        }

        bool SendMail(string AppName, string NotifyList)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("monitoring.application.acerpro@gmail.com", "1Q2W3e4r-");
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Timeout = 600000;
            smtp.EnableSsl = true;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("monitoring.application.acerpro@gmail.com", "Down Notifier");

            mail.To.Add(NotifyList);

            mail.Subject = AppName + " Has Down";
            mail.Body = AppName + " has down. You should control your application..";
            mail.IsBodyHtml = true;

            mail.SubjectEncoding = Encoding.UTF8;
            mail.BodyEncoding = Encoding.UTF8;

            try
            {
                smtp.Send(mail);
                mail.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
