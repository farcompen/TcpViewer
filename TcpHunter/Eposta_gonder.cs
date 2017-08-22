using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TcpHunter
{
    class Eposta_gonder
    {
        public void send_mail(string subject,string body)
        {

            try
            {

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("from mail adress");
                mail.To.Add("to mail adress");
                mail.Subject = subject;
                mail.IsBodyHtml = true;

                mail.Body = body;

                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.Host = "smtp.live.com";
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("from mail adress username ", "from mail adress password ");


                smtp.Send(mail);







            }
            catch (Exception hata)
            {
                throw hata;

            }
           


        }
    }
}
