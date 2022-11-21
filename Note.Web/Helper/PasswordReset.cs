using System.Net;
using System.Net.Mail;

namespace Note.Web.Helper
{
    public static class PasswordReset
    {
        public static void PasswordResetSendEmail(string link, string email, string username)
        {
            MailMessage mailMessage = new MailMessage();

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");


            mailMessage.From = new MailAddress("nilsuacar97@gmail.com");
            mailMessage.To.Add($"{email}");
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;  //Türkçe karakterleri tanımak için
            mailMessage.Subject = "Şifre sıfırlama bağlantısı";
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;     //Türkçe karakterleri tanımak için
            mailMessage.Body += $"Merhaba {username} <br/>";
            mailMessage.Body += "Şifrenizi yenilemek için lütfen butona tıklayanız<hr/><br/>";
            mailMessage.Body += $"<a  style=\"background-color: #f44336; color: white; padding: 15px 25px; text-align: center;  text-decoration: none; display: inline-block;\" href={link} target=\"_blank\" > Şifre Yenileme</a><br/><br/>";

            mailMessage.Body += "Eğer buton çalışmıyorsa aşağıdaki linki kullana bilirisiniz<br/><br/>";
            mailMessage.Body += link;


            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("nilsuacar97@gmail.com", "xxvksinrkafovglj");
            smtpClient.Send(mailMessage);

        }
    }
}
