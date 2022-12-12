using System.Net;
using System.Net.Mail;

namespace Note.Web.Helper
{
    public static class EmailConfirmation
    {
        public static void EmailConfirmSendEmail(string link, string email, string name)
        {
            MailMessage mailMessage = new MailMessage();

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");


            mailMessage.From = new MailAddress("nilsuacar97@gmail.com");
            mailMessage.To.Add($"{email}");
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.Subject = "E-posta Doğruluma Bağlantısı";
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.Body += $"Merhaba {name} <br/>";
            mailMessage.Body += "E-postanı doğrulamak için lütfen butona tıklayanız<hr/><br/>";
            mailMessage.Body += $"<a  style=\"background-color: #f44336; color: white; padding: 15px 25px; text-align: center;  text-decoration: none; display: inline-block;\" href={link} target=\"_blank\" > E-posta Doğrulama</a><br/><br/>";

            mailMessage.Body += "Eğer buton çalışmıyorsa aşağıdaki linki kullana bilirisiniz<br/><br/>";
            mailMessage.Body += link;


            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("nilsuacar97@gmail.com", "xxvksinrkafovglj"); 
            smtpClient.Send(mailMessage);

        }
    }
}
